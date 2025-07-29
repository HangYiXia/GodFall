using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

//public class WaveSpawner : MonoBehaviour
//{
//    public static int EnemiesAlive = 0;

//    public Wave[] waves;

//    public Transform spawnPoint;

//    public float timeBetweenWaves = 5f;
//    private float countdown = 2f;
//    private int waveIndex = 0;

//    public TextMeshProUGUI waveCountdownText;

//    public GameManager gameManager;

//    public 


//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (EnemiesAlive > 0) return;

//        if (waveIndex == waves.Length)
//        {
//            gameManager.WinLevel();
//            this.enabled = false;
//        }

//        if (countdown <= 0f)
//        {
//            StartCoroutine(SpawnWave());
//            countdown = timeBetweenWaves;
//            return;
//        }
//        countdown -= Time.deltaTime;

//        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

//        waveCountdownText.text = string.Format("Time Next Wave: {0:00.00}", countdown);
//    }
//    IEnumerator SpawnWave()
//    {
//        PlayerStats.Rounds++;
//        Wave wave = waves[waveIndex];

//        EnemiesAlive = wave.count;
//        for (int i = 0; i < wave.count; i++)
//        {
//            SpawnEnemy(wave.enemy);
//            yield return new WaitForSeconds(1f / wave.rate);
//        }
//        waveIndex++;
//    }
//    void SpawnEnemy(GameObject enemy)
//    {
//        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
//    }
//}

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    public Wave[] waves;

    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;

    // waveIndex 现在会无限增长，用来计算循环和乘数
    private int waveIndex = 0;

    public TextMeshProUGUI waveCountdownText;

    public GameManager gameManager;

    // Update is called once per frame
    void Update()
    {
        if (EnemiesAlive > 0)
        {
            return;
        }

        // 移除了原来在这里的游戏胜利判断逻辑
        // if (waveIndex == waves.Length)
        // {
        //     gameManager.WinLevel();
        //     this.enabled = false;
        // }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }
        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("Time Next Wave: {0:00.00}", countdown);
    }
    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;

        // --- 核心修改逻辑开始 ---

        // 1. 使用取模运算 (%) 来循环获取预设的波次。
        // 例如，当 waveIndex 是 5 时, 5 % 5 = 0, 会重新使用第一个波次(waves[0])的设置
        // 当 waveIndex 是 6 时, 6 % 5 = 1, 会使用第二个波次(waves[1])
        Wave wave = waves[waveIndex % waves.Length];

        // 2. 计算当前是第几次循环。
        // C#中两个整数相除会自动取整，这正是我们需要的。
        // 0-4波 / 5 = 0 (第一次循环)
        // 5-9波 / 5 = 1 (第二次循环)
        int cycle = waveIndex / waves.Length;

        // 3. 计算敌人数量的乘数。
        // Mathf.Pow(2, cycle) 会计算2的cycle次方 (2^0=1, 2^1=2, 2^2=4, ...)
        // 这样就实现了每次循环敌人数量翻倍的效果
        int multiplier = (int)Mathf.Pow(2, cycle);

        // 4. 计算当前波次实际需要生成的敌人总数
        int enemiesToSpawn = wave.count * multiplier;
        EnemiesAlive = enemiesToSpawn;
        float currentSpawnRate = wave.rate * multiplier;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy(wave.enemy);
            // 这里我们依然使用预设波次的生成速率
            yield return new WaitForSeconds(1f / currentSpawnRate);
        }

        // --- 核心修改逻辑结束 ---

        waveIndex++;
    }
    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
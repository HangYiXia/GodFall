using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public EnemyData enemyData;
        public int count;   // 一波内敌人数量
        public float rate;  // 一波内敌人的生成速率
    }

    public static int EnemiesAlive = 0;

    public Wave[] waves;

    public WayPoints[] availablePaths;

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
        Wave wave = waves[waveIndex % waves.Length];
        
        int cycle = waveIndex / waves.Length;
        int multiplier = (int)Mathf.Pow(2, cycle);
        
        int enemiesToSpawn = wave.count * multiplier;
        EnemiesAlive = enemiesToSpawn;
        float currentSpawnRate = wave.rate * multiplier;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            WayPoints chosenPath = availablePaths[Random.Range(0, availablePaths.Length)];
            SpawnEnemy(wave.enemyData, waveIndex + 1, chosenPath); // waveIndex+1 确保第一波也是1
            yield return new WaitForSeconds(1f / currentSpawnRate);
        }

        waveIndex++;
    }
    void SpawnEnemy(EnemyData data, int waveIndex, WayPoints path)
    {
        // 路径的第一个点就是出生点
        Transform spawnPoint = path.getPoints()[0];
        GameObject enemyGO = Instantiate(data.Prefab, spawnPoint.position, spawnPoint.rotation);
        Enemy enemy = enemyGO.GetComponent<Enemy>();
        enemy.Setup(data, waveIndex, path); // 把路径传给敌人
    }

    void OnEnable()
    {
        Enemy.OnEnemyDied += HandleEnemyDeath;
        Enemy.OnEnemyReachedEnd += HandleEnemyReachedEnd; // 订阅新事件
    }
    void OnDisable()
    {
        Enemy.OnEnemyDied -= HandleEnemyDeath;
        Enemy.OnEnemyReachedEnd -= HandleEnemyReachedEnd; // 取消订阅
    }
    void HandleEnemyDeath(Enemy deadEnemy)
    {
        EnemiesAlive--;
    }
    void HandleEnemyReachedEnd(Enemy enemy)
    {
        EnemiesAlive--;
    }
}


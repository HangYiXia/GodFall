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

    // waveIndex ���ڻ�������������������ѭ���ͳ���
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

        // �Ƴ���ԭ�����������Ϸʤ���ж��߼�
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

        // --- �����޸��߼���ʼ ---

        // 1. ʹ��ȡģ���� (%) ��ѭ����ȡԤ��Ĳ��Ρ�
        // ���磬�� waveIndex �� 5 ʱ, 5 % 5 = 0, ������ʹ�õ�һ������(waves[0])������
        // �� waveIndex �� 6 ʱ, 6 % 5 = 1, ��ʹ�õڶ�������(waves[1])
        Wave wave = waves[waveIndex % waves.Length];

        // 2. ���㵱ǰ�ǵڼ���ѭ����
        // C#����������������Զ�ȡ����������������Ҫ�ġ�
        // 0-4�� / 5 = 0 (��һ��ѭ��)
        // 5-9�� / 5 = 1 (�ڶ���ѭ��)
        int cycle = waveIndex / waves.Length;

        // 3. ������������ĳ�����
        // Mathf.Pow(2, cycle) �����2��cycle�η� (2^0=1, 2^1=2, 2^2=4, ...)
        // ������ʵ����ÿ��ѭ����������������Ч��
        int multiplier = (int)Mathf.Pow(2, cycle);

        // 4. ���㵱ǰ����ʵ����Ҫ���ɵĵ�������
        int enemiesToSpawn = wave.count * multiplier;
        EnemiesAlive = enemiesToSpawn;
        float currentSpawnRate = wave.rate * multiplier;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy(wave.enemy);
            // ����������Ȼʹ��Ԥ�貨�ε���������
            yield return new WaitForSeconds(1f / currentSpawnRate);
        }

        // --- �����޸��߼����� ---

        waveIndex++;
    }
    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
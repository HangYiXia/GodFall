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
        public int count;   // һ���ڵ�������
        public float rate;  // һ���ڵ��˵���������
    }

    public static int EnemiesAlive = 0;

    public Wave[] waves;

    public WayPoints[] availablePaths;

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
        Wave wave = waves[waveIndex % waves.Length];
        
        int cycle = waveIndex / waves.Length;
        int multiplier = (int)Mathf.Pow(2, cycle);
        
        int enemiesToSpawn = wave.count * multiplier;
        EnemiesAlive = enemiesToSpawn;
        float currentSpawnRate = wave.rate * multiplier;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            WayPoints chosenPath = availablePaths[Random.Range(0, availablePaths.Length)];
            SpawnEnemy(wave.enemyData, waveIndex + 1, chosenPath); // waveIndex+1 ȷ����һ��Ҳ��1
            yield return new WaitForSeconds(1f / currentSpawnRate);
        }

        waveIndex++;
    }
    void SpawnEnemy(EnemyData data, int waveIndex, WayPoints path)
    {
        // ·���ĵ�һ������ǳ�����
        Transform spawnPoint = path.getPoints()[0];
        GameObject enemyGO = Instantiate(data.Prefab, spawnPoint.position, spawnPoint.rotation);
        Enemy enemy = enemyGO.GetComponent<Enemy>();
        enemy.Setup(data, waveIndex, path); // ��·����������
    }

    void OnEnable()
    {
        Enemy.OnEnemyDied += HandleEnemyDeath;
        Enemy.OnEnemyReachedEnd += HandleEnemyReachedEnd; // �������¼�
    }
    void OnDisable()
    {
        Enemy.OnEnemyDied -= HandleEnemyDeath;
        Enemy.OnEnemyReachedEnd -= HandleEnemyReachedEnd; // ȡ������
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


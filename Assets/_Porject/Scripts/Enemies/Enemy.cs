using UnityEngine;
using System;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    [Header("��������")]
    [Tooltip("�õ��˵�������ͼ")]
    public EnemyData enemyData; // ������Inspector��ֱ����ק���ɴ��붯̬����

    // ��������ʱ�����ľ�̬�¼�
    // �����κνű������Լ�������¼�������������߼�����Ǯ��������������ȣ�
    public static event Action<Enemy> OnEnemyDied;
    public static event Action<Enemy> OnEnemyReachedEnd; // �����¼�

    // --- ״̬���� ---
    private double maxHealth; // ��ǰ���εĵ����������ֵ
    [SerializeField] private double currentHealth;
    public float currentSpeed;
    private bool isDead = false;

    private Transform targetWaypoint;
    private int waypointIndex = 0;
    private WayPoints path; // ·�������ⲿ����

    [Header("��Ч")]
    public GameObject deathEffectPrefab;
    [Header("UI���")]
    public Image healthBar;


    public void Setup(EnemyData data, int waveNumber, WayPoints pathToFollow)
    {
        // ս�����Գ�ʼ��
        enemyData = data;
        switch (enemyData.HealthScalingType)
        {
            case ScalingType.Linear:
                maxHealth = enemyData.BaseHealth + enemyData.HealthScalingFactor * (waveNumber - 1);
                break;
            case ScalingType.Exponential:
            default:
                maxHealth = enemyData.BaseHealth * Mathf.Pow(enemyData.HealthScalingFactor, waveNumber - 1);
                break;
        }
        currentHealth = maxHealth;
        currentSpeed = enemyData.BaseSpeed;
        if (healthBar != null) healthBar.fillAmount = 1f;

        // �ƶ�·����ʼ��
        this.path = pathToFollow;
        if (this.path != null && this.path.getPoints().Length > 0)
        {
            targetWaypoint = this.path.getPoints()[0];
        }
    }

    // --- �ƶ��߼� (��EnemyMovement����) ---
    void Update()
    {
        if (isDead || targetWaypoint == null) return;

        Vector3 dir = targetWaypoint.position - transform.position;
        transform.Translate(dir.normalized * currentSpeed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.5f)
        {
            GetNextWayPoint();
        }
    }

    private void GetNextWayPoint()
    {
        if (waypointIndex >= path.getPoints().Length - 1)
        {
            EndPath();
            return;
        }
        waypointIndex++;
        targetWaypoint = path.getPoints()[waypointIndex];
    }

    private void EndPath()
    {
        OnEnemyReachedEnd?.Invoke(this);
        Destroy(gameObject);
    }

    /// <summary>
    /// ����˺�
    /// </summary>
    /// <param name="damage">�ܵ����˺�ֵ</param>
    public void TakeDamage(double damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (healthBar != null)
        {
            healthBar.fillAmount = (float)(currentHealth / maxHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// ����Ч��
    /// </summary>
    /// <param name="pct">���ٰٷֱ� (0��1)</param>
    public void Slow(float pct)
    {
        currentSpeed *= (1f - pct);
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        // --- ���ģ��㲥�����¼� ---
        // ���ǽ�������Ϊ�������ݳ�ȥ���Ա�����߿��Ի�ȡ������������˵�������Ϣ
        OnEnemyDied?.Invoke(this);

        // ����������Ч
        if (deathEffectPrefab != null)
        {
            GameObject deathEffect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(deathEffect, 2f);
        }

        // ��������
        Destroy(gameObject);
    }


}

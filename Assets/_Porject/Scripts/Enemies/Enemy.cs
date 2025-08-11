using UnityEngine;
using System;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    [Header("数据引用")]
    [Tooltip("该敌人的数据蓝图")]
    public EnemyData enemyData; // 用于在Inspector中直接拖拽或由代码动态赋予

    // 敌人死亡时触发的静态事件
    // 其他任何脚本都可以监听这个事件，来处理后续逻辑（加钱、减数量、掉落等）
    public static event Action<Enemy> OnEnemyDied;
    public static event Action<Enemy> OnEnemyReachedEnd; // 新增事件

    // --- 状态变量 ---
    private double maxHealth; // 当前波次的敌人最大生命值
    [SerializeField] private double currentHealth;
    public float currentSpeed;
    private bool isDead = false;

    private Transform targetWaypoint;
    private int waypointIndex = 0;
    private WayPoints path; // 路径将由外部传入

    [Header("特效")]
    public GameObject deathEffectPrefab;
    [Header("UI组件")]
    public Image healthBar;


    public void Setup(EnemyData data, int waveNumber, WayPoints pathToFollow)
    {
        // 战斗属性初始化
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

        // 移动路径初始化
        this.path = pathToFollow;
        if (this.path != null && this.path.getPoints().Length > 0)
        {
            targetWaypoint = this.path.getPoints()[0];
        }
    }

    // --- 移动逻辑 (从EnemyMovement移入) ---
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
    /// 造成伤害
    /// </summary>
    /// <param name="damage">受到的伤害值</param>
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
    /// 减速效果
    /// </summary>
    /// <param name="pct">减速百分比 (0到1)</param>
    public void Slow(float pct)
    {
        currentSpeed *= (1f - pct);
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        // --- 核心：广播死亡事件 ---
        // 我们将自身作为参数传递出去，以便监听者可以获取到关于这个敌人的所有信息
        OnEnemyDied?.Invoke(this);

        // 生成死亡特效
        if (deathEffectPrefab != null)
        {
            GameObject deathEffect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(deathEffect, 2f);
        }

        // 销毁自身
        Destroy(gameObject);
    }


}

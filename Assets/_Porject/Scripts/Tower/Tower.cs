using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Tower : MonoBehaviour
{
    [Header("数据配置 (必填)")]
    [Tooltip("将对应塔的TowerData配置文件拖到此处")]
    public TowerData towerData;
    public string selectEnemyStrategy = "Nearest";

    private double nextUpgradeCost;
    private int currentLevel = 1;

    // 内部引用
    private SphereCollider rangeCollider;
    private List<Transform> enemiesInRange = new List<Transform>();
    private float attackCooldown = 0f;

    // === 核心：攻击策略 ===
    // 这是一个接口引用，它将指向附加到这个GameObject上的具体攻击策略脚本
    private IAttackStrategy attackStrategy;
    private IAimStrategy aimStrategy;
    public Transform CurrentTarget { get; private set; }
    public double CurrentDamage { get; private set; }
    public float CurrentAttackRate { get; private set; }
    public float CurrentAttackRange { get; private set; }

    void Awake()
    {
        attackStrategy = GetComponent<IAttackStrategy>();
        if (attackStrategy == null)
        {
            Debug.LogError("这个塔忘记附加攻击策略脚本了! (例如 ProjectileAttack)", this);
        }

        // 自动获取瞄准策略组件
        aimStrategy = GetComponent<IAimStrategy>();
        if (aimStrategy == null)
        {
            Debug.LogError("这个塔忘记附加瞄准策略脚本了!", this);
        }

        rangeCollider = GetComponent<SphereCollider>();
        if (rangeCollider == null)
        {
            Debug.LogError("这个塔没有球形碰撞体", this);
        }
    }

    void Start()
    {
        // 游戏开始时，从TowerData初始化塔的属性
        InitializeStats();
    }

    void Update()
    {
        if (attackCooldown > 0f) attackCooldown -= Time.deltaTime;
        SelectTarget();
        if (CurrentTarget == null)
        {
            aimStrategy.Aim(null);
            return;
        }

        aimStrategy.Aim(CurrentTarget);

        if (attackCooldown <= 0f)
        {
            attackCooldown = 1f / CurrentAttackRate;
            PerformAttack();
        }
    }

    /// <summary>
    /// 从TowerData加载属性。未来应用Buff也在这里处理。
    /// </summary>
    private void InitializeStats()
    {
        if (towerData == null)
        {
            Debug.LogError("TowerData 未在Inspector中指定!", this);
            this.enabled = false; // 如果没有数据，禁用此塔
            return;
        }

        // 从TowerData读取基础属性
        CurrentDamage = towerData.initialDamage;
        CurrentAttackRate = towerData.initialAttackRate;
        CurrentAttackRange = towerData.initialAttackRange;
        rangeCollider.radius = CurrentAttackRange;
        currentLevel = 1;
        CalculateStats();
        aimStrategy?.Initialize(this);
    }


    /// <summary>
    /// 根据当前等级和TowerData的公式，计算所有属性
    /// </summary>
    private void CalculateStats()
    {
        if (towerData == null) return;

        // --- 核心计算逻辑 ---
        // 使用double来计算，防止数值过大导致溢出
        nextUpgradeCost = towerData.initialCost * Mathf.Pow(towerData.costGrowthFactor, currentLevel);
        CurrentDamage = towerData.initialDamage * Mathf.Pow(towerData.damageGrowthFactor, currentLevel - 1);

        CurrentAttackRate = towerData.initialAttackRate + towerData.attackRateGrowthFactor * (currentLevel - 1);

        // 计算范围：(当前等级-1) / 间隔，得到应该增加几次
        int rangeIncreases = (currentLevel - 1) / towerData.rangeIncreaseInterval;
        CurrentAttackRange = towerData.initialAttackRange + rangeIncreases * towerData.rangeIncreaseAmount;

        // 将计算出的属性应用到组件
        rangeCollider.radius = CurrentAttackRange;
        attackCooldown = 1f / CurrentAttackRate;

        // TODO: 更新UI显示 (显示伤害、攻速、下一级升级费用等)
        // 例如: uiManager.UpdateTowerInfo(this);
    }

    /// <summary>
    /// 命令攻击策略组件执行攻击
    /// </summary>
    private void PerformAttack()
    {
        attackStrategy.ExecuteAttack(CurrentTarget, CurrentDamage);
    }

    /// <summary>
    /// 通过Collider Trigger来判断一个敌人是否在攻击范围内
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Add(other.transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.transform);
        }
    }
    void SelectTarget()
    {
        enemiesInRange.RemoveAll(item => item == null);
        if (enemiesInRange.Count > 0)
        {
            CurrentTarget = FindTarget(selectEnemyStrategy);
            //LockOnTarget(currentTarget);
        }
        else CurrentTarget = null;
    }
    Transform FindTarget(string selectEnemyStrategy)
    {
        if (selectEnemyStrategy == "Nearest")
        {
            float minDistance = float.MaxValue;
            Transform nearestEnemy = null;
            foreach (Transform enemy in enemiesInRange)
            {
                float distance = Vector3.Distance(transform.position, enemy.position);
                if (distance < minDistance)
                {
                    minDistance = distance; nearestEnemy = enemy;
                }
            }
            return nearestEnemy;
        }
        return enemiesInRange[0];
    }


    // (可选) 在编辑器中绘制攻击范围，非常方便调试
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, CurrentAttackRange);
    }
}


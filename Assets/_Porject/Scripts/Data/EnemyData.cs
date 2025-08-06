using UnityEngine;

/// <summary>
/// 定义敌人类型的数据资产。
/// 包含敌人的基础属性、模型、奖励等信息。
/// 这些是基础数据，实际游戏中的敌人属性会根据波数等因素进行动态计算。
/// </summary>
[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Tower Defense/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("基础信息 (Basic Info)")]
    [Tooltip("敌人的显示名称，例如：哥布林、地狱犬")]
    [SerializeField] private string _enemyName = "New Enemy";

    [Tooltip("敌人的游戏对象预制体（Prefab），应包含模型、动画、EnemyController脚本等组件")]
    [SerializeField] private GameObject _prefab;

    [Tooltip("该敌人的稀有度数据资产，直接拖拽一个RarityData.asset文件到这里")]
    [SerializeField] private RarityData _rarity;

    [Header("基础战斗属性 (Base Combat Stats)")]
    [Tooltip("敌人的基础生命值。这是未受任何加成（如波数难度）影响的原始值")]
    [SerializeField] private float _baseHealth = 50f;

    [Tooltip("敌人的基础移动速度")]
    [SerializeField] private float _baseSpeed = 2f;

    [Tooltip("护甲值。可以用于后续的伤害减免计算")]
    [SerializeField] private float _armor = 0f;

    [Header("击杀奖励 (Kill Rewards)")]
    [Tooltip("击杀该敌人后，玩家获得的基础金钱数量")]
    [SerializeField] private int _moneyReward = 5;

    [Tooltip("击杀该敌人后，玩家获得的经验值")]
    [SerializeField] private int _experienceReward = 10;

    [Header("掉落物 (Loot)")]
    [Tooltip("该敌人的掉落表")]
    [SerializeField] private LootTableData _lootTable;

    [Header("随波数成长参数 (Per-Wave Scaling)")]

    [Tooltip("生命值成长的类型：Linear(线性) 或 Exponential(指数)")]
    [SerializeField] private ScalingType _healthScalingType = ScalingType.Exponential;

    [Tooltip("生命值成长因子。线性：每波增加的值；指数：每波乘以的系数(例如1.1)")]
    [SerializeField] private float _healthScalingFactor = 1.15f;

    // 你也可以为速度等其他属性添加类似的成长参数
    // [Tooltip("速度成长的类型")]
    // public ScalingType speedScalingType = ScalingType.Linear;
    // [Tooltip("速度成长因子")]
    // public float speedScalingFactor = 0.05f;




    #region Public Accessors (公共访问器)

    public string EnemyName => _enemyName;
    public GameObject Prefab => _prefab;
    public RarityData Rarity => _rarity;
    public float BaseHealth => _baseHealth;
    public float BaseSpeed => _baseSpeed;
    public float Armor => _armor;
    public int MoneyReward => _moneyReward;
    public int ExperienceReward => _experienceReward;
    public LootTableData LootTable => _lootTable;
    public ScalingType HealthScalingType => _healthScalingType;
    public float HealthScalingFactor => _healthScalingFactor;


    #endregion
}

/// <summary>
/// 定义数值的成长方式
/// </summary>
public enum ScalingType
{
    Linear,      // 线性增长: final = base + factor * (wave-1)
    Exponential  // 指数增长: final = base * (factor ^ (wave-1))
}
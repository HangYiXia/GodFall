using UnityEngine;

[CreateAssetMenu(fileName = "NewProgressionTowerData", menuName = "Tower Defense/Progression Tower Data")]
public class TowerData : ScriptableObject
{
    [Header("基础信息")]
    public string towerName = "新塔";
    public GameObject basePrefab; // 基础模型 (Lvl 1)

    [Tooltip("该敌人的稀有度数据资产，直接拖拽一个RarityData.asset文件到这里")]
    [SerializeField] private RarityData _rarity;

    //// 我们可以定义一些里程碑式的Prefab，比如每10级变个外观
    //[System.Serializable]
    //public struct MilestonePrefab
    //{
    //    public int levelReached;
    //    public GameObject newPrefab;
    //}
    //public MilestonePrefab[] milestonePrefabs;

    [Header("初始属性 (Level 1)")]
    public int initialCost = 100;
    public float initialDamage = 10f;
    public float initialAttackRate = 1f;
    public float initialAttackRange = 15f;

    [Header("成长公式参数 (每次升级)")]
    // 你可以选择简单的线性增长或更刺激的指数增长
    public float costGrowthFactor = 1.15f; // 指数增长: cost = initialCost * (factor ^ (level-1))
    public float damageGrowthFactor = 1.2f; // 指数增长
    public float attackRateGrowthFactor = 0.05f; // 线性增长: rate = initialRate + factor * (level-1)

    // 攻击范围可以设置为每N级才成长1次，或者不成长
    [Tooltip("每升多少级，攻击范围才增加一次")]
    public int rangeIncreaseInterval = 5;
    [Tooltip("每次范围增加的值")]
    public float rangeIncreaseAmount = 2f;
}
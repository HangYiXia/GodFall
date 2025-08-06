using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 辅助类，定义了掉落表中的单项掉落物及其权重。
/// </summary>
[System.Serializable]
public class LootDropItem
{
    [Tooltip("可能掉落的装备")]
    public EquipmentData equipment;

    [Tooltip("掉落权重。权重越高的物品，被选中的几率越大。")]
    [Min(1)] // 确保权重至少为1
    public int weight;
}


/// <summary>
/// 定义一个掉落表的数据资产。
/// 它可以被敌人、宝箱等任何需要掉落物品的系统使用。
/// </summary>
[CreateAssetMenu(fileName = "New Loot Table", menuName = "Tower Defense/Loot Table Data")]
public class LootTableData : ScriptableObject
{
    [Header("掉落配置")]
    [Tooltip("包含所有可能掉落物品及其权重的列表")]
    [SerializeField]
    private List<LootDropItem> _possibleDrops;

    // 存储总权重，避免重复计算
    private int _totalWeight;
    private bool _isInitialized = false;

    /// <summary>
    /// 初始化，计算总权重。
    /// </summary>
    private void Initialize()
    {
        if (_isInitialized) return;

        _totalWeight = 0;
        foreach (var item in _possibleDrops)
        {
            _totalWeight += item.weight;
        }
        _isInitialized = true;
    }

    /// <summary>
    /// 根据权重随机返回一个掉落物。
    /// 如果掉落表为空或者所有权重加起来为0，则返回null。
    /// </summary>
    /// <returns>随机选中的EquipmentData，或null。</returns>
    public EquipmentData GetRandomDrop()
    {
        // OnValidate在编辑器中修改数据时调用，确保总权重实时更新。
        // 而Initialize()用于运行时第一次调用时计算。
        Initialize();

        if (_totalWeight <= 0)
        {
            return null; // 没有可掉落的物品
        }

        int randomValue = Random.Range(1, _totalWeight + 1);

        foreach (var item in _possibleDrops)
        {
            if (randomValue <= item.weight)
            {
                return item.equipment;
            }
            else
            {
                randomValue -= item.weight;
            }
        }

        return null; // 理论上不应该执行到这里，除非出现逻辑错误
    }

    /// <summary>
    /// OnValidate在编辑器中每次修改此资产数据时被调用。
    /// 我们用它来重置初始化状态，以便下次调用GetRandomDrop时能重新计算总权重。
    /// </summary>
    private void OnValidate()
    {
        _isInitialized = false;
    }
}
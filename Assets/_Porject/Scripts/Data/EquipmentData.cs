using System.Collections.Generic;
using UnityEngine;

#region Helper Enums and Classes (辅助枚举与类)

/// <summary>
/// 定义装备可以被装备在哪个槽位。
/// </summary>
public enum EquipmentSlot
{
    Weapon,   // 武器槽
    Core,     // 核心槽
    Chip,     // 芯片槽
    Armor     // 护甲槽
}

/// <summary>
/// 定义属性修改的方式。
/// </summary>
public enum ModifierType
{
    Flat,       // 固定值加成 (例如: 攻击力+10)
    PercentAdd, // 百分比加成 (例如: 攻击力+15%)
}

/// <summary>
/// 一个可序列化的类，用于定义单条属性修改规则。
/// </summary>
[System.Serializable]
public class AttributeModifier
{
    // --- 这里是唯一的修改 ---
    // 将原来的枚举类型改为了对AttributeData资产的直接引用
    [Tooltip("要修改的属性类型")]
    public AttributeData Attribute;
    // --- 修改结束 ---

    [Tooltip("修改的方式（固定值或百分比）")]
    public ModifierType Type;

    [Tooltip("修改的数值")]
    public float Value;
}

#endregion


/// <summary>
/// 定义一件装备的数据资产。
/// </summary>
[CreateAssetMenu(fileName = "New Equipment", menuName = "Tower Defense/Equipment Data")]
public class EquipmentData : ScriptableObject
{
    [Header("基础信息")]
    [Tooltip("装备的名称")]
    [SerializeField] private string _itemName = "New Equipment";

    [Tooltip("装备的详细描述")]
    [TextArea] // 让描述字段在Inspector里可以输入多行文本
    [SerializeField] private string _itemDescription = "Item Description";

    [Tooltip("装备在UI中显示的图标")]
    [SerializeField] private Sprite _icon;

    [Tooltip("装备的稀有度")]
    [SerializeField] private RarityData _rarity;

    [Tooltip("装备所属的槽位")]
    [SerializeField] private EquipmentSlot _slot;

    [Header("属性加成")]
    [Tooltip("该装备提供的所有属性修改列表")]
    [SerializeField] private List<AttributeModifier> _modifiers;


    #region Public Accessors

    public string ItemName => _itemName;
    public string ItemDescription => _itemDescription;
    public Sprite Icon => _icon;
    public RarityData Rarity => _rarity;
    public EquipmentSlot Slot => _slot;
    public IReadOnlyList<AttributeModifier> Modifiers => _modifiers;

    #endregion
}
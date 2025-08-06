using System.Collections.Generic;
using UnityEngine;

#region Helper Enums and Classes (����ö������)

/// <summary>
/// ����װ�����Ա�װ�����ĸ���λ��
/// </summary>
public enum EquipmentSlot
{
    Weapon,   // ������
    Core,     // ���Ĳ�
    Chip,     // оƬ��
    Armor     // ���ײ�
}

/// <summary>
/// ���������޸ĵķ�ʽ��
/// </summary>
public enum ModifierType
{
    Flat,       // �̶�ֵ�ӳ� (����: ������+10)
    PercentAdd, // �ٷֱȼӳ� (����: ������+15%)
}

/// <summary>
/// һ�������л����࣬���ڶ��嵥�������޸Ĺ���
/// </summary>
[System.Serializable]
public class AttributeModifier
{
    // --- ������Ψһ���޸� ---
    // ��ԭ����ö�����͸�Ϊ�˶�AttributeData�ʲ���ֱ������
    [Tooltip("Ҫ�޸ĵ���������")]
    public AttributeData Attribute;
    // --- �޸Ľ��� ---

    [Tooltip("�޸ĵķ�ʽ���̶�ֵ��ٷֱȣ�")]
    public ModifierType Type;

    [Tooltip("�޸ĵ���ֵ")]
    public float Value;
}

#endregion


/// <summary>
/// ����һ��װ���������ʲ���
/// </summary>
[CreateAssetMenu(fileName = "New Equipment", menuName = "Tower Defense/Equipment Data")]
public class EquipmentData : ScriptableObject
{
    [Header("������Ϣ")]
    [Tooltip("װ��������")]
    [SerializeField] private string _itemName = "New Equipment";

    [Tooltip("װ������ϸ����")]
    [TextArea] // �������ֶ���Inspector�������������ı�
    [SerializeField] private string _itemDescription = "Item Description";

    [Tooltip("װ����UI����ʾ��ͼ��")]
    [SerializeField] private Sprite _icon;

    [Tooltip("װ����ϡ�ж�")]
    [SerializeField] private RarityData _rarity;

    [Tooltip("װ�������Ĳ�λ")]
    [SerializeField] private EquipmentSlot _slot;

    [Header("���Լӳ�")]
    [Tooltip("��װ���ṩ�����������޸��б�")]
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
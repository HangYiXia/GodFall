using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����࣬�����˵�����еĵ�������Ｐ��Ȩ�ء�
/// </summary>
[System.Serializable]
public class LootDropItem
{
    [Tooltip("���ܵ����װ��")]
    public EquipmentData equipment;

    [Tooltip("����Ȩ�ء�Ȩ��Խ�ߵ���Ʒ����ѡ�еļ���Խ��")]
    [Min(1)] // ȷ��Ȩ������Ϊ1
    public int weight;
}


/// <summary>
/// ����һ�������������ʲ���
/// �����Ա����ˡ�������κ���Ҫ������Ʒ��ϵͳʹ�á�
/// </summary>
[CreateAssetMenu(fileName = "New Loot Table", menuName = "Tower Defense/Loot Table Data")]
public class LootTableData : ScriptableObject
{
    [Header("��������")]
    [Tooltip("�������п��ܵ�����Ʒ����Ȩ�ص��б�")]
    [SerializeField]
    private List<LootDropItem> _possibleDrops;

    // �洢��Ȩ�أ������ظ�����
    private int _totalWeight;
    private bool _isInitialized = false;

    /// <summary>
    /// ��ʼ����������Ȩ�ء�
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
    /// ����Ȩ���������һ�������
    /// ��������Ϊ�ջ�������Ȩ�ؼ�����Ϊ0���򷵻�null��
    /// </summary>
    /// <returns>���ѡ�е�EquipmentData����null��</returns>
    public EquipmentData GetRandomDrop()
    {
        // OnValidate�ڱ༭�����޸�����ʱ���ã�ȷ����Ȩ��ʵʱ���¡�
        // ��Initialize()��������ʱ��һ�ε���ʱ���㡣
        Initialize();

        if (_totalWeight <= 0)
        {
            return null; // û�пɵ������Ʒ
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

        return null; // �����ϲ�Ӧ��ִ�е�������ǳ����߼�����
    }

    /// <summary>
    /// OnValidate�ڱ༭����ÿ���޸Ĵ��ʲ�����ʱ�����á�
    /// �������������ó�ʼ��״̬���Ա��´ε���GetRandomDropʱ�����¼�����Ȩ�ء�
    /// </summary>
    private void OnValidate()
    {
        _isInitialized = false;
    }
}
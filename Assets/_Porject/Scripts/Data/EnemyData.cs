using UnityEngine;

/// <summary>
/// ����������͵������ʲ���
/// �������˵Ļ������ԡ�ģ�͡���������Ϣ��
/// ��Щ�ǻ������ݣ�ʵ����Ϸ�еĵ������Ի���ݲ��������ؽ��ж�̬���㡣
/// </summary>
[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Tower Defense/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("������Ϣ (Basic Info)")]
    [Tooltip("���˵���ʾ���ƣ����磺�粼�֡�����Ȯ")]
    [SerializeField] private string _enemyName = "New Enemy";

    [Tooltip("���˵���Ϸ����Ԥ���壨Prefab����Ӧ����ģ�͡�������EnemyController�ű������")]
    [SerializeField] private GameObject _prefab;

    [Tooltip("�õ��˵�ϡ�ж������ʲ���ֱ����קһ��RarityData.asset�ļ�������")]
    [SerializeField] private RarityData _rarity;

    [Header("����ս������ (Base Combat Stats)")]
    [Tooltip("���˵Ļ�������ֵ������δ���κμӳɣ��粨���Ѷȣ�Ӱ���ԭʼֵ")]
    [SerializeField] private float _baseHealth = 50f;

    [Tooltip("���˵Ļ����ƶ��ٶ�")]
    [SerializeField] private float _baseSpeed = 2f;

    [Tooltip("����ֵ���������ں������˺��������")]
    [SerializeField] private float _armor = 0f;

    [Header("��ɱ���� (Kill Rewards)")]
    [Tooltip("��ɱ�õ��˺���һ�õĻ�����Ǯ����")]
    [SerializeField] private int _moneyReward = 5;

    [Tooltip("��ɱ�õ��˺���һ�õľ���ֵ")]
    [SerializeField] private int _experienceReward = 10;

    [Header("������ (Loot)")]
    [Tooltip("�õ��˵ĵ����")]
    [SerializeField] private LootTableData _lootTable;

    [Header("�沨���ɳ����� (Per-Wave Scaling)")]

    [Tooltip("����ֵ�ɳ������ͣ�Linear(����) �� Exponential(ָ��)")]
    [SerializeField] private ScalingType _healthScalingType = ScalingType.Exponential;

    [Tooltip("����ֵ�ɳ����ӡ����ԣ�ÿ�����ӵ�ֵ��ָ����ÿ�����Ե�ϵ��(����1.1)")]
    [SerializeField] private float _healthScalingFactor = 1.15f;

    // ��Ҳ����Ϊ�ٶȵ���������������Ƶĳɳ�����
    // [Tooltip("�ٶȳɳ�������")]
    // public ScalingType speedScalingType = ScalingType.Linear;
    // [Tooltip("�ٶȳɳ�����")]
    // public float speedScalingFactor = 0.05f;




    #region Public Accessors (����������)

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
/// ������ֵ�ĳɳ���ʽ
/// </summary>
public enum ScalingType
{
    Linear,      // ��������: final = base + factor * (wave-1)
    Exponential  // ָ������: final = base * (factor ^ (wave-1))
}
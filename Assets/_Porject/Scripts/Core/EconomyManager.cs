// EconomyManager.cs
using System;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager instance;

    [SerializeField] private int startMoney = 100;
    public int CurrentMoney { get; private set; }

    // ����Ǯ�仯ʱ�������¼�
    public static event Action<int> OnMoneyChanged;
    void Awake() { instance = this; }

    void Start()
    {
        CurrentMoney = startMoney;
        OnMoneyChanged?.Invoke(CurrentMoney); // ��Ϸ��ʼʱ֪ͨUI
    }

    public bool CanAfford(int amount)
    {
        return CurrentMoney >= amount;
    }

    public void SpendMoney(int amount)
    {
        if (CanAfford(amount))
        {
            CurrentMoney -= amount;
            OnMoneyChanged?.Invoke(CurrentMoney); // �㲥�¼�
        }
    }

    public void AddMoney(int amount)
    {
        CurrentMoney += amount;
        OnMoneyChanged?.Invoke(CurrentMoney); // �㲥�¼�
    }

    void OnEnable()
    {
        // ���κ�һ��Enemy����������OnEnemyDied.Invoke()ʱ�����ǵ�HandleEnemyDeath�����ͻᱻִ�С�
        Enemy.OnEnemyDied += HandleEnemyDeath;
    }

    void OnDisable()
    {
        // �ǳ���Ҫ��ȡ�����ģ���ֹ�ڳ����л���������ٺ������ڴ�й©�����
        Enemy.OnEnemyDied -= HandleEnemyDeath;
    }

    private void HandleEnemyDeath(Enemy killedEnemy)
    {
        if (killedEnemy != null && killedEnemy.enemyData != null)
        {
            AddMoney(killedEnemy.enemyData.MoneyReward);
        }
    }
}
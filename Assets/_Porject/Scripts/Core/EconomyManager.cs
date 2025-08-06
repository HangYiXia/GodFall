// EconomyManager.cs
using System;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager instance;

    [SerializeField] private int startMoney = 100;
    public int CurrentMoney { get; private set; }

    // 当金钱变化时触发的事件
    public static event Action<int> OnMoneyChanged;
    void Awake() { instance = this; }

    void Start()
    {
        CurrentMoney = startMoney;
        OnMoneyChanged?.Invoke(CurrentMoney); // 游戏开始时通知UI
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
            OnMoneyChanged?.Invoke(CurrentMoney); // 广播事件
        }
    }

    public void AddMoney(int amount)
    {
        CurrentMoney += amount;
        OnMoneyChanged?.Invoke(CurrentMoney); // 广播事件
    }

    void OnEnable()
    {
        // 当任何一个Enemy死亡并调用OnEnemyDied.Invoke()时，我们的HandleEnemyDeath方法就会被执行。
        Enemy.OnEnemyDied += HandleEnemyDeath;
    }

    void OnDisable()
    {
        // 非常重要：取消订阅，防止在场景切换或对象销毁后引发内存泄漏或错误。
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
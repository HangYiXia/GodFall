using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Lives;
    public int startLives = 10;
    public static int Rounds;

    private void Start()
    {
        Lives = startLives;
        Rounds = 0;
    }

    void OnEnable()
    {
        // 当脚本启用时，开始监听敌人死亡事件
        Enemy.OnEnemyDied += HandleEnemyDeath;
        Enemy.OnEnemyReachedEnd += HandleEnemyReachedEnd;
    }

    void OnDisable()
    {
        // 当脚本禁用时，取消监听，防止内存泄漏
        Enemy.OnEnemyDied -= HandleEnemyDeath;
        Enemy.OnEnemyReachedEnd -= HandleEnemyReachedEnd;
    }

    // 当敌人死亡事件发生时，这个方法会被自动调用
    void HandleEnemyDeath(Enemy deadEnemy)
    {
        // 从死亡的敌人数据中获取金钱奖励
        // 这里还可以加经验等...
    }
    void HandleEnemyReachedEnd(Enemy enemy)
    {
        Lives--;
    }
}

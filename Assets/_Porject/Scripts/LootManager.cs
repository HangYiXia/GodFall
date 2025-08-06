// 创建一个全新的 LootManager.cs 脚本，并把它放在场景中的一个管理器对象上
using UnityEngine;

public class LootManager : MonoBehaviour
{
    // 也可以在这里放一个掉落物的Prefab
    // public GameObject itemDropPrefab; 

    void OnEnable()
    {
        Enemy.OnEnemyDied += HandleEnemyDeath;
    }

    void OnDisable()
    {
        Enemy.OnEnemyDied -= HandleEnemyDeath;
    }

    void HandleEnemyDeath(Enemy deadEnemy)
    {
        // 检查敌人是否有掉落表
        if (deadEnemy.enemyData.LootTable == null) return;

        // 从掉落表中获取随机掉落物
        EquipmentData droppedItem = deadEnemy.enemyData.LootTable.GetRandomDrop();

        // 如果成功掉落了物品
        if (droppedItem != null)
        {
            Debug.Log($"敌人 {deadEnemy.enemyData.EnemyName} 掉落了 {droppedItem.ItemName}!");
            // 在这里写生成掉落物实体的代码
            // 例如: Instantiate(itemDropPrefab, deadEnemy.transform.position, Quaternion.identity);
            // 然后给这个掉落物实体赋予 droppedItem 的数据。
        }
    }
}
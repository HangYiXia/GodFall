// ����һ��ȫ�µ� LootManager.cs �ű������������ڳ����е�һ��������������
using UnityEngine;

public class LootManager : MonoBehaviour
{
    // Ҳ�����������һ���������Prefab
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
        // �������Ƿ��е����
        if (deadEnemy.enemyData.LootTable == null) return;

        // �ӵ�����л�ȡ���������
        EquipmentData droppedItem = deadEnemy.enemyData.LootTable.GetRandomDrop();

        // ����ɹ���������Ʒ
        if (droppedItem != null)
        {
            Debug.Log($"���� {deadEnemy.enemyData.EnemyName} ������ {droppedItem.ItemName}!");
            // ������д���ɵ�����ʵ��Ĵ���
            // ����: Instantiate(itemDropPrefab, deadEnemy.transform.position, Quaternion.identity);
            // Ȼ������������ʵ�帳�� droppedItem �����ݡ�
        }
    }
}
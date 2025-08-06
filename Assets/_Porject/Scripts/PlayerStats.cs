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
        // ���ű�����ʱ����ʼ�������������¼�
        Enemy.OnEnemyDied += HandleEnemyDeath;
        Enemy.OnEnemyReachedEnd += HandleEnemyReachedEnd;
    }

    void OnDisable()
    {
        // ���ű�����ʱ��ȡ����������ֹ�ڴ�й©
        Enemy.OnEnemyDied -= HandleEnemyDeath;
        Enemy.OnEnemyReachedEnd -= HandleEnemyReachedEnd;
    }

    // �����������¼�����ʱ����������ᱻ�Զ�����
    void HandleEnemyDeath(Enemy deadEnemy)
    {
        // �������ĵ��������л�ȡ��Ǯ����
        // ���ﻹ���ԼӾ����...
    }
    void HandleEnemyReachedEnd(Enemy enemy)
    {
        Lives--;
    }
}

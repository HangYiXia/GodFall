using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int wavepointIndex = 0;
    private Enemy enemy;
    private WayPoints WayPoint;
    private GameObject[] Ways;

    void Start()
    {
        GetPath();
        enemy = GetComponent<Enemy>();

        target = WayPoint.getPoints()[0];

    }
    void GetPath()
    {
        Ways = GameObject.FindGameObjectsWithTag("WayPoints");
        if (Ways.Length < 2)
        {
            Debug.LogError("WayPoinst�������� 2 ����");
            return;
        }

        if (RandomHelper.GetRandomZeroOrOne() == 0)
        {
            WayPoint = Ways[0].GetComponent<WayPoints>();
        }
        else
        {
            WayPoint = Ways[1].GetComponent<WayPoints>();
        }

        if (WayPoint == null || WayPoint.getPoints() == null || WayPoint.getPoints().Length == 0)
        {
            Debug.LogError("WayPointδ��ȷ��ȡ��û�е㣡");
            return;
        }
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            GetNextWayPoint();
        }
        enemy.speed = enemy.startSpeed;
    }
    private void GetNextWayPoint()
    {
        if (wavepointIndex >= WayPoint.getPoints().Length - 1)
        {
            EndPath();
            return;
        }
        wavepointIndex++;
        target = WayPoint.getPoints()[wavepointIndex];
    }
    void EndPath()
    {
        PlayerStats.Lives--;
        Destroy(gameObject);
        WaveSpawner.EnemiesAlive--;
    }
}

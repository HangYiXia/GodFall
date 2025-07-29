using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTower : MonoBehaviour
{
    private Transform target;

    [Header("Attributes")]
    public float range = 20f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    private float turnSpeed = 50f;
    public Transform topOfArrowTower;

    public GameObject arrowPrefab;
    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (fireCountdown > 0f) { fireCountdown -= Time.deltaTime; }
        if (target == null) { return; }

        // 1. ˮƽ��ת�������壬ֻ��y�ᣩ
        Vector3 towerToTarget = target.position - topOfArrowTower.position;
        Vector3 towerToTargetXZ = new Vector3(towerToTarget.x, 0f, towerToTarget.z);
        float yAngle = 0;
        if (towerToTargetXZ.sqrMagnitude > 0.001f)
        {
            yAngle = Mathf.Atan2(towerToTargetXZ.x, towerToTargetXZ.z) * Mathf.Rad2Deg;
            // ֻ����y��
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.Euler(0f, yAngle, 0f),
                Time.deltaTime * turnSpeed
            );
        }

        // 2. ������ת���ڹܣ�ֻ��x�ᣩ
        // ����Ŀ�����ڹܵĸ߶Ȳ��XZƽ�����
        float dy = target.position.y - topOfArrowTower.position.y;
        float dxz = new Vector2(
            target.position.x - topOfArrowTower.position.x,
            target.position.z - topOfArrowTower.position.z
        ).magnitude;
        float xAngle = Mathf.Atan2(dy, dxz) * Mathf.Rad2Deg;
        // ֻ����x��
        Quaternion elevation = Quaternion.Euler(xAngle, 0f, 0f);
        topOfArrowTower.localRotation = Quaternion.Lerp(
            topOfArrowTower.localRotation,
            elevation,
            Time.deltaTime * turnSpeed
        );

        // === �������ж��Ƿ��׼Ŀ�� ===
        float yError = Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, Quaternion.Euler(0f, yAngle, 0f).eulerAngles.y));
        float xError = Mathf.Abs(Mathf.DeltaAngle(topOfArrowTower.localEulerAngles.x, xAngle));
        float aimThreshold = 3f; // ��������Ƕ����ȣ�

        if (fireCountdown <= 0f && yError < aimThreshold && xError < aimThreshold)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        GameObject nearestEnemy = null;
        float shortestDistanceToEnemy = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistanceToEnemy)
            {
                shortestDistanceToEnemy = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistanceToEnemy <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void Shoot()
    {
        GameObject arrowGO = (GameObject)Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        Arrow arrow = arrowGO.GetComponent<Arrow>();

        if (arrow != null)
        {
            arrow.Seek(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

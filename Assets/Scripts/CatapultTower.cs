using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultTower : MonoBehaviour
{
    [Header("目标设置")]
    [Tooltip("索敌的目标标签")]
    public string enemyTag = "Enemy";
    private Transform target;

    [Header("投石机属性")]
    [Tooltip("攻击范围")]
    public float range = 15f;
    [Tooltip("攻击频率")]
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Unity组件设置")]
    [Tooltip("旋转速度")]
    [SerializeField] private float turnSpeed = 10f;
    [Tooltip("炮弹发射点 (你的 firePoint GameObject)")]
    [SerializeField] private Transform firePoint;
    [Tooltip("炮弹的Prefab (你的 Cannonball Prefab)")]
    [SerializeField] private GameObject cannonballPrefab;

    [Header("")]
    [Tooltip("")]
    [SerializeField] private Animator animator;
    [Tooltip("")]
    [SerializeField] private float baseAnimSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        LockOnTarget();
        if (fireCountdown > 0f) { fireCountdown -= Time.deltaTime; }
        if (target == null) { return; }
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
    }

    void LockOnTarget()
    {
        if (target == null) return;
        // --- 水平旋转 (Y轴) ---
        Vector3 towerToTarget = target.position - transform.position;
        Vector3 towerToTargetXZ = new Vector3(towerToTarget.x, 0f, towerToTarget.z);
        float yAngle = 0;
        if (towerToTargetXZ.sqrMagnitude > 0.001f)
        {
            yAngle = Mathf.Atan2(towerToTargetXZ.x, towerToTargetXZ.z) * Mathf.Rad2Deg;
            // 只设置y轴
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.Euler(0f, yAngle, 0f),
                Time.deltaTime * turnSpeed
            );
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
        animator.speed = fireRate * baseAnimSpeed;
        animator.SetTrigger("Fire");
    }

    public void LaunchProjectile()
    {
        if (target == null) return;
        GameObject cannonballGO = (GameObject)Instantiate(cannonballPrefab, firePoint.position, firePoint.rotation);
        Cannonball cannonball = cannonballGO.GetComponent<Cannonball>();
        if (cannonball != null)
        {
            cannonball.Seek(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}



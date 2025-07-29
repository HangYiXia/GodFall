using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTower : MonoBehaviour
{
    private Transform target;
    private Enemy targetEnemy;

    [Header("Attributes")]
    public int damage = 10;
    public float range = 30f;
    public float fireRate = 4f;
    public float slowPct = 0.5f;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public Transform firePoint;

    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.2f);
    }

    void Update()
    {
        if (target == null)
        {
            FireEffectOFF();
            return;
        }
        Shoot();
    }
    void Shoot()
    {
        targetEnemy.TakeDamage(damage * Time.deltaTime * fireRate);
        targetEnemy.Slow(slowPct);
        FireEffectON();
    }

    void FireEffectON()
    {
        if (!lineRenderer.enabled) {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.transform.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
        impactEffect.transform.position = target.position + dir.normalized * 1f;
    }

    void FireEffectOFF()
    {
        if (lineRenderer.enabled)
        {
            lineRenderer.enabled = false;
            impactEffect.Stop();
            impactLight.enabled = false;
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
            targetEnemy = target.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

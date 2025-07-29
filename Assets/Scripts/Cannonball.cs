using UnityEngine;

public class Cannonball : MonoBehaviour
{
    [Header("子弹属性")]
    [Tooltip("子弹伤害值")]
    public int damage = 50;

    [Tooltip("攻击半径")]
    public float attackRadius = 10f;

    [Header("追踪属性")]
    [Tooltip("子弹飞行速度")]
    public float speed = 10f;

    [Tooltip("子弹的转向速度")]
    public float turnSpeed = 30f;

    [Tooltip("子弹生存时间")]
    private float lifeTime = 5f;

    private Rigidbody rb;
    private Transform target;
    public GameObject cannonballImapctEffect;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }
    public void Seek(Transform _target)
    {
        target = _target;
        rb.isKinematic = false;
        Destroy(gameObject, lifeTime);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.up * 100;

        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 direction = (target.position - transform.position).normalized;
        Vector3 targetVelocity = direction * speed;
        Vector3 force = (targetVelocity - rb.velocity) * turnSpeed;
        rb.AddForce(force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        HitTarget(collision.contacts[0].point);
    }
    public void HitTarget(Vector3 hitPoint)
    {
        if (attackRadius > 0f)
        {
            Explode(hitPoint);
        }
        else
        {
            if (target != null)
            {
                Damage(target);
            }
        }
        GameObject effectIns = (GameObject)Instantiate(cannonballImapctEffect, transform.position, transform.rotation);
        effectIns.transform.localScale = Vector3.one * attackRadius;
        Destroy(gameObject);
        Destroy(effectIns, 1f);


    }

    void Explode(Vector3 explosionPoint)
    {
        Collider[] colliders = Physics.OverlapSphere(explosionPoint, attackRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Damage(collider.transform);
            }
        }
    }


    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
        {
            e.TakeDamage(damage);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

}
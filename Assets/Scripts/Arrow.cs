using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float damage = 30f;
    private Transform target;
    private Enemy enemyTraget;
    private float speed = 100f;
    public GameObject arrowImapctEffect;


    public void Seek(Transform _target)
    {
        target = _target;
        enemyTraget = target.GetComponent<Enemy>();
    }
    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude > distanceThisFrame)
        {
            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        }
        else
        {
            HitTarget();
            return;
        }


    }
    public void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(arrowImapctEffect, transform.position, transform.rotation);
        Destroy(effectIns, 1f);
        Destroy(gameObject);
        enemyTraget.TakeDamage(damage);
    }
}

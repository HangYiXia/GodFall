using UnityEngine;

public class Projectile : MonoBehaviour
{
    // --- 新增代码 ---
    private Transform target;
    private double damage;
    // --- 结束新增 ---

    public float speed = 70f;
    public GameObject impactEffect; // (可选) 子弹击中时的爆炸特效

    /// <summary>
    /// 设置子弹的目标和伤害。由发射塔在创建时调用。
    /// </summary>
    public void Seek(Transform _target, double _damage)
    {
        target = _target;
        damage = _damage;
    }

    void Update()
    {
        if (target == null)
        {
            // 目标丢失或死亡，子弹自我销毁
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // 判断子弹是否快要击中目标
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target); // 让子弹朝向目标
    }

    /// <summary>
    /// 子弹击中目标时触发
    /// </summary>
    void HitTarget()
    {
        // 播放击中特效
        if (impactEffect != null)
        {
            GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(effectIns, 2f); // 2秒后销毁特效
        }

        // --- 修改点 ---
        // 对目标造成伤害
        // 我们假设敌人身上有一个名为 "Enemy" 的脚本，且它有一个 TakeDamage 方法
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        // --- 结束修改 ---

        // 销毁子弹
        Destroy(gameObject);
    }
}
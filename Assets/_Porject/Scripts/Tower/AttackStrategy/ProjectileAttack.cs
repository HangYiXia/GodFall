using UnityEngine;

public class ProjectileAttack : MonoBehaviour, IAttackStrategy
{
    [Header("子弹设置")]
    [SerializeField]
    [Tooltip("子弹的预制体 (Prefab)")]
    private GameObject projectilePrefab;

    [SerializeField]
    [Tooltip("子弹生成的位置和朝向")]
    private Transform firePoint;

    public void ExecuteAttack(Transform target, double damage)
    {
        // 1. 在发射点(firePoint)实例化一个子弹Prefab
        GameObject projectileGO = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // 2. 获取子弹上的Projectile脚本组件
        Projectile projectile = projectileGO.GetComponent<Projectile>();

        // 3. 如果成功获取，就命令子弹去攻击目标
        if (projectile != null)
        {
            projectile.Seek(target, damage); // 调用我们刚才在Projectile中添加的Seek方法
        }
        else
        {
            Debug.LogError("子弹Prefab上没有挂载Projectile脚本!", projectilePrefab);
        }
    }
}
using UnityEngine;

public class AreaProjectileAttack : MonoBehaviour, IAttackStrategy
{
    [Header("组件引用")]
    [Tooltip("控制投石机动画的Animator组件")]
    [SerializeField] private Animator animator;

    [Header("投射物设置")]
    [SerializeField] private GameObject areaProjectilePrefab;
    [SerializeField] private Transform firePoint;

    [Header("范围伤害设置")]
    public float areaOfEffectRadius = 4f;

    // --- 新增变量，用于临时存储信息 ---
    private Transform currentTarget;
    private double currentDamage;

    public void ExecuteAttack(Transform target, double damage)
    {
        // 1. 保存目标和伤害信息，以便动画事件触发时能取到
        this.currentTarget = target;
        this.currentDamage = damage;

        // 2. 触发动画，而不是直接实例化
        if (animator != null)
        {
            // "Fire" 是我们稍后会在Animator中创建的一个触发器参数
            animator.SetTrigger("Fire");
        }
        else
        {
            Debug.LogError("没有在AreaProjectileAttack中指定Animator!", this);
        }
    }

    /// <summary>
    /// 这个方法现在由 CatapultAnimationHelper 在动画的特定帧调用
    /// </summary>
    public void SpawnProjectile()
    {
        if (currentTarget == null) return; // 如果在动画播放期间目标丢失了，就不发射

        GameObject projectileGO = Instantiate(areaProjectilePrefab, firePoint.position, firePoint.rotation);
        AreaProjectile areaProjectile = projectileGO.GetComponent<AreaProjectile>();
        if (areaProjectile != null)
        {
            areaProjectile.Seek(currentTarget, currentDamage, areaOfEffectRadius);
        }
    }
}
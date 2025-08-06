using UnityEngine;

public class LaserAttackStrategy : MonoBehaviour, IAttackStrategy
{
    [Header("激光组件引用")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform firePoint;
    [SerializeField] private ParticleSystem impactEffect;
    [SerializeField] private Light impactLight;

    // 内部状态
    private Tower tower; // 对主Tower脚本的引用
    private float damageTickCooldown = 0f; // 伤害计时器

    void Awake()
    {
        // 获取父对象上的Tower脚本
        tower = GetComponentInParent<Tower>();
        if (tower == null)
        {
            Debug.LogError("LaserAttackStrategy 必须作为Tower的子组件或同级组件!", this);
            this.enabled = false;
        }
    }

    void Start()
    {
        // 游戏开始时默认关闭所有效果
        DisableLaser();
    }

    void Update()
    {
        // 核心逻辑：每一帧都检查Tower是否锁定了目标
        if (tower.CurrentTarget != null)
        {
            EnableLaser();
            UpdateLaser();
        }
        else
        {
            DisableLaser();
        }
    }

    /// <summary>
    /// 当有目标时，每帧更新激光的位置和伤害计算
    /// </summary>
    private void UpdateLaser()
    {
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, tower.CurrentTarget.position);
        Vector3 dir = firePoint.position - tower.CurrentTarget.position;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
        impactEffect.transform.position = tower.CurrentTarget.position + dir.normalized * 1f;

        if (damageTickCooldown > 0f) damageTickCooldown -= Time.deltaTime;
        else
        {
            DealDamage();
            damageTickCooldown = 1f / tower.CurrentAttackRate;
        }
    }

    private void DealDamage()
    {
        // 注意：这里我们造成的是单次伤害，而不是持续伤害
        // 因为这个方法会根据攻速被周期性调用
        Enemy enemy = tower.CurrentTarget.GetComponent<Enemy>();
        if (enemy != null)
        {
            // 从Tower获取当前计算好的伤害值
            enemy.TakeDamage(tower.CurrentDamage);
        }
    }

    private void EnableLaser()
    {
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }
    }

    private void DisableLaser()
    {
        if (lineRenderer.enabled)
        {
            lineRenderer.enabled = false;
            impactEffect.Stop();
            impactLight.enabled = false;
        }
    }

    /// <summary>
    /// 这个方法对于持续性激光来说不是主要逻辑，可以留空。
    /// 但为了遵守接口规范，我们必须实现它。
    /// </summary>
    public void ExecuteAttack(Transform target, double damage)
    {
        // 留空，因为我们的主要逻辑在Update()中
    }
}
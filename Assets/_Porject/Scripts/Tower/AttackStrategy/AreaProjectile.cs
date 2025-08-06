using UnityEngine;

public class AreaProjectile : MonoBehaviour
{
    // --- 飞行相关 ---
    private Transform target;
    public float speed = 30f; // 投石机的弹药速度通常比子弹慢

    // --- 爆炸伤害相关 ---
    private double maxDamage; // 从塔传来的最高伤害（中心点伤害）
    private float areaOfEffectRadius; // 爆炸半径

    // --- 效果相关 ---
    public GameObject impactEffectPrefab; // 爆炸时播放的特效

    /// <summary>
    /// 初始化投射物，由发射塔在创建时调用。
    /// </summary>
    /// <param name="destination">目标落地位置</param>
    /// <param name="damage">中心点最高伤害</param>
    /// <param name="aoeRadius">爆炸半径</param>
    public void Seek(Transform _target, double damage, float aoeRadius)
    {
        this.target = _target;
        this.maxDamage = damage;
        this.areaOfEffectRadius = aoeRadius;
    }

    void Update()
    {
        // 计算朝向目标位置的向量
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // 如果这一帧的移动距离已经超过了到目标的距离，说明已经到达
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        // 朝目标位置移动
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

        // (可选) 可以让投射物有一个抛物线的效果，但这比较复杂，暂时先用直线
    }

    /// <summary>
    /// 投射物到达目标位置，触发爆炸
    /// </summary>
    void HitTarget()
    {
        // 1. 在落地点击播放爆炸特效
        if (impactEffectPrefab != null)
        {
            GameObject effectIns = Instantiate(impactEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effectIns, 2f); // 2秒后销毁特效
        }

        // 2. 触发爆炸伤害逻辑
        Explode();

        // 3. 销毁投射物自身
        Destroy(gameObject);
    }

    /// <summary>
    /// 核心：爆炸逻辑
    /// </summary>
    void Explode()
    {
        // 使用OverlapSphere高效地获取爆炸范围内的所有碰撞体
        Collider[] colliders = Physics.OverlapSphere(transform.position, areaOfEffectRadius);

        foreach (Collider hit in colliders)
        {
            // 检查这个碰撞体是不是敌人
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
                // 如果是敌人，就计算伤害衰减并施加伤害
                ApplyFalloffDamage(enemy);
            }
        }
    }

    /// <summary>
    /// 计算并施加伤害衰减
    /// </summary>
    void ApplyFalloffDamage(Enemy enemy)
    {
        // 1. 计算敌人与爆炸中心的距离
        float distance = Vector3.Distance(transform.position, enemy.transform.position);

        // 2. 计算伤害衰减比例 (线性衰减)
        // 距离越远，伤害越低。在最边缘时，伤害为0。
        // 公式: (爆炸半径 - 敌人距离) / 爆炸半径
        float falloffFactor = (areaOfEffectRadius - distance) / areaOfEffectRadius;

        // 确保比例在0到1之间
        falloffFactor = Mathf.Clamp01(falloffFactor);

        // 3. 计算最终伤害
        double finalDamage = maxDamage * falloffFactor;

        // 4. 对敌人造成伤害
        enemy.TakeDamage((float)finalDamage);
    }

    // 在编辑器中绘制爆炸范围，方便调试
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, areaOfEffectRadius);
    }
}
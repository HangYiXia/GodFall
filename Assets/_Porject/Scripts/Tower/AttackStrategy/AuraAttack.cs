using UnityEngine;

public class AuraAttack : MonoBehaviour, IAttackStrategy
{
    private float tickTimer = 0f;

    // AuraAttack不需要目标，所以ExecuteAttack可以空着，或者用于触发一次性的特效
    public void ExecuteAttack(Transform target, double damage)
    {
        // 这个模式下，Tower的Update攻击循环可能不适用，
        // 或者我们可以让它只在有敌人时才启动光环，以节省性能。
    }

    void Update()
    {
        // 这个策略自己管理攻击周期
        tickTimer -= Time.deltaTime;
        if (tickTimer <= 0)
        {
            // 这里 Tower 主脚本需要传递攻击频率和伤害
            Tower tower = GetComponent<Tower>();
            tickTimer = 1f / tower.towerData.initialAttackRate;
            ApplyAuraDamage(tower.towerData.initialAttackRange, tower.towerData.initialDamage);
        }
    }

    void ApplyAuraDamage(float range, float damage)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                hitCollider.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }
}
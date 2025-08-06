using UnityEngine;

// === 攻击策略接口 ===
public interface IAttackStrategy
{
    /// <summary>
    /// 执行一次攻击的逻辑
    /// </summary>
    /// <param name="target">要攻击的目标</param>
    /// <param name="damage">这次攻击造成的伤害</param>
    void ExecuteAttack(Transform target, double damage);
}
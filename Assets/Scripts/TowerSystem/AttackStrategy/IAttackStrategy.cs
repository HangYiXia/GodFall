using UnityEngine;

// === �������Խӿ� ===
public interface IAttackStrategy
{
    /// <summary>
    /// ִ��һ�ι������߼�
    /// </summary>
    /// <param name="target">Ҫ������Ŀ��</param>
    /// <param name="damage">��ι�����ɵ��˺�</param>
    void ExecuteAttack(Transform target, double damage);
}
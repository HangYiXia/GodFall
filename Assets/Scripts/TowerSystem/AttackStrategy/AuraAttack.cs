using UnityEngine;

public class AuraAttack : MonoBehaviour, IAttackStrategy
{
    private float tickTimer = 0f;

    // AuraAttack����ҪĿ�꣬����ExecuteAttack���Կ��ţ��������ڴ���һ���Ե���Ч
    public void ExecuteAttack(Transform target, double damage)
    {
        // ���ģʽ�£�Tower��Update����ѭ�����ܲ����ã�
        // �������ǿ�������ֻ���е���ʱ�������⻷���Խ�ʡ���ܡ�
    }

    void Update()
    {
        // ��������Լ�����������
        tickTimer -= Time.deltaTime;
        if (tickTimer <= 0)
        {
            // ���� Tower ���ű���Ҫ���ݹ���Ƶ�ʺ��˺�
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
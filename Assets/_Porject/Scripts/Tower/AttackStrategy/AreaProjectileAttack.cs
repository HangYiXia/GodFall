using UnityEngine;

public class AreaProjectileAttack : MonoBehaviour, IAttackStrategy
{
    [Header("�������")]
    [Tooltip("����Ͷʯ��������Animator���")]
    [SerializeField] private Animator animator;

    [Header("Ͷ��������")]
    [SerializeField] private GameObject areaProjectilePrefab;
    [SerializeField] private Transform firePoint;

    [Header("��Χ�˺�����")]
    public float areaOfEffectRadius = 4f;

    // --- ����������������ʱ�洢��Ϣ ---
    private Transform currentTarget;
    private double currentDamage;

    public void ExecuteAttack(Transform target, double damage)
    {
        // 1. ����Ŀ����˺���Ϣ���Ա㶯���¼�����ʱ��ȡ��
        this.currentTarget = target;
        this.currentDamage = damage;

        // 2. ����������������ֱ��ʵ����
        if (animator != null)
        {
            // "Fire" �������Ժ����Animator�д�����һ������������
            animator.SetTrigger("Fire");
        }
        else
        {
            Debug.LogError("û����AreaProjectileAttack��ָ��Animator!", this);
        }
    }

    /// <summary>
    /// ������������� CatapultAnimationHelper �ڶ������ض�֡����
    /// </summary>
    public void SpawnProjectile()
    {
        if (currentTarget == null) return; // ����ڶ��������ڼ�Ŀ�궪ʧ�ˣ��Ͳ�����

        GameObject projectileGO = Instantiate(areaProjectilePrefab, firePoint.position, firePoint.rotation);
        AreaProjectile areaProjectile = projectileGO.GetComponent<AreaProjectile>();
        if (areaProjectile != null)
        {
            areaProjectile.Seek(currentTarget, currentDamage, areaOfEffectRadius);
        }
    }
}
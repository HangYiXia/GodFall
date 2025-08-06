using UnityEngine;

public class LaserAttackStrategy : MonoBehaviour, IAttackStrategy
{
    [Header("�����������")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform firePoint;
    [SerializeField] private ParticleSystem impactEffect;
    [SerializeField] private Light impactLight;

    // �ڲ�״̬
    private Tower tower; // ����Tower�ű�������
    private float damageTickCooldown = 0f; // �˺���ʱ��

    void Awake()
    {
        // ��ȡ�������ϵ�Tower�ű�
        tower = GetComponentInParent<Tower>();
        if (tower == null)
        {
            Debug.LogError("LaserAttackStrategy ������ΪTower���������ͬ�����!", this);
            this.enabled = false;
        }
    }

    void Start()
    {
        // ��Ϸ��ʼʱĬ�Ϲر�����Ч��
        DisableLaser();
    }

    void Update()
    {
        // �����߼���ÿһ֡�����Tower�Ƿ�������Ŀ��
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
    /// ����Ŀ��ʱ��ÿ֡���¼����λ�ú��˺�����
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
        // ע�⣺����������ɵ��ǵ����˺��������ǳ����˺�
        // ��Ϊ�����������ݹ��ٱ������Ե���
        Enemy enemy = tower.CurrentTarget.GetComponent<Enemy>();
        if (enemy != null)
        {
            // ��Tower��ȡ��ǰ����õ��˺�ֵ
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
    /// ����������ڳ����Լ�����˵������Ҫ�߼����������ա�
    /// ��Ϊ�����ؽӿڹ淶�����Ǳ���ʵ������
    /// </summary>
    public void ExecuteAttack(Transform target, double damage)
    {
        // ���գ���Ϊ���ǵ���Ҫ�߼���Update()��
    }
}
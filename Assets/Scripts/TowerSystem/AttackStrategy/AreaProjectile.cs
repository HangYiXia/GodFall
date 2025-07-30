using UnityEngine;

public class AreaProjectile : MonoBehaviour
{
    // --- ������� ---
    private Transform target;
    public float speed = 30f; // Ͷʯ���ĵ�ҩ�ٶ�ͨ�����ӵ���

    // --- ��ը�˺���� ---
    private double maxDamage; // ��������������˺������ĵ��˺���
    private float areaOfEffectRadius; // ��ը�뾶

    // --- Ч����� ---
    public GameObject impactEffectPrefab; // ��ըʱ���ŵ���Ч

    /// <summary>
    /// ��ʼ��Ͷ����ɷ������ڴ���ʱ���á�
    /// </summary>
    /// <param name="destination">Ŀ�����λ��</param>
    /// <param name="damage">���ĵ�����˺�</param>
    /// <param name="aoeRadius">��ը�뾶</param>
    public void Seek(Transform _target, double damage, float aoeRadius)
    {
        this.target = _target;
        this.maxDamage = damage;
        this.areaOfEffectRadius = aoeRadius;
    }

    void Update()
    {
        // ���㳯��Ŀ��λ�õ�����
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // �����һ֡���ƶ������Ѿ������˵�Ŀ��ľ��룬˵���Ѿ�����
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        // ��Ŀ��λ���ƶ�
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

        // (��ѡ) ������Ͷ������һ�������ߵ�Ч��������Ƚϸ��ӣ���ʱ����ֱ��
    }

    /// <summary>
    /// Ͷ���ﵽ��Ŀ��λ�ã�������ը
    /// </summary>
    void HitTarget()
    {
        // 1. ����ص�����ű�ը��Ч
        if (impactEffectPrefab != null)
        {
            GameObject effectIns = Instantiate(impactEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effectIns, 2f); // 2���������Ч
        }

        // 2. ������ը�˺��߼�
        Explode();

        // 3. ����Ͷ��������
        Destroy(gameObject);
    }

    /// <summary>
    /// ���ģ���ը�߼�
    /// </summary>
    void Explode()
    {
        // ʹ��OverlapSphere��Ч�ػ�ȡ��ը��Χ�ڵ�������ײ��
        Collider[] colliders = Physics.OverlapSphere(transform.position, areaOfEffectRadius);

        foreach (Collider hit in colliders)
        {
            // ��������ײ���ǲ��ǵ���
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
                // ����ǵ��ˣ��ͼ����˺�˥����ʩ���˺�
                ApplyFalloffDamage(enemy);
            }
        }
    }

    /// <summary>
    /// ���㲢ʩ���˺�˥��
    /// </summary>
    void ApplyFalloffDamage(Enemy enemy)
    {
        // 1. ��������뱬ը���ĵľ���
        float distance = Vector3.Distance(transform.position, enemy.transform.position);

        // 2. �����˺�˥������ (����˥��)
        // ����ԽԶ���˺�Խ�͡������Եʱ���˺�Ϊ0��
        // ��ʽ: (��ը�뾶 - ���˾���) / ��ը�뾶
        float falloffFactor = (areaOfEffectRadius - distance) / areaOfEffectRadius;

        // ȷ��������0��1֮��
        falloffFactor = Mathf.Clamp01(falloffFactor);

        // 3. ���������˺�
        double finalDamage = maxDamage * falloffFactor;

        // 4. �Ե�������˺�
        enemy.TakeDamage((float)finalDamage);
    }

    // �ڱ༭���л��Ʊ�ը��Χ���������
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, areaOfEffectRadius);
    }
}
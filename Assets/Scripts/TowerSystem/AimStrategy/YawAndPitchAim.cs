using UnityEngine;

public class YawAndPitchAim : MonoBehaviour, IAimStrategy
{
    [Header("��Ҫ��ת�Ĳ���")]
    [SerializeField] private Transform bodyToRotateYaw; // ����Y��ˮƽ��ת
    [SerializeField] private Transform barrelToRotatePitch; // ����X�ḩ����ת

    private Tower tower;
    private float turnSpeed = 10f;

    public void Initialize(Tower owner)
    {
        this.tower = owner;
    }

    public void Aim(Transform target)
    {
        if (target == null || bodyToRotateYaw == null || barrelToRotatePitch == null) return;

        Vector3 dir = target.position - bodyToRotateYaw.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);

        // Y����ת (����)
        Vector3 yawRotation = Quaternion.Lerp(bodyToRotateYaw.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        bodyToRotateYaw.rotation = Quaternion.Euler(0f, yawRotation.y, 0f);

        // X����ת (�ڹ�)
        Vector3 pitchRotation = Quaternion.Lerp(barrelToRotatePitch.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        barrelToRotatePitch.rotation = Quaternion.Euler(pitchRotation.x, yawRotation.y, 0f);
    }
}
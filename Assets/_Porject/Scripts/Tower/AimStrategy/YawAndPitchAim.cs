using UnityEngine;

public class YawAndPitchAim : MonoBehaviour, IAimStrategy
{
    [Header("需要旋转的部件")]
    [SerializeField] private Transform bodyToRotateYaw; // 用于Y轴水平旋转
    [SerializeField] private Transform barrelToRotatePitch; // 用于X轴俯仰旋转

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

        // Y轴旋转 (塔身)
        Vector3 yawRotation = Quaternion.Lerp(bodyToRotateYaw.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        bodyToRotateYaw.rotation = Quaternion.Euler(0f, yawRotation.y, 0f);

        // X轴旋转 (炮管)
        Vector3 pitchRotation = Quaternion.Lerp(barrelToRotatePitch.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        barrelToRotatePitch.rotation = Quaternion.Euler(pitchRotation.x, yawRotation.y, 0f);
    }
}
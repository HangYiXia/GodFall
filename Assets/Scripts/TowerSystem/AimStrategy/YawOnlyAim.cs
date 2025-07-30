using UnityEngine;

public class YawOnlyAim : MonoBehaviour, IAimStrategy
{
    [Header("需要旋转的部件")]
    [SerializeField] private Transform bodyToRotateYaw; // 用于Y轴水平旋转

    private Tower tower;
    private float turnSpeed = 10f;

    public void Initialize(Tower owner)
    {
        this.tower = owner;
    }

    public void Aim(Transform target)
    {
        if (target == null || bodyToRotateYaw == null) return;

        Vector3 dir = target.position - bodyToRotateYaw.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(bodyToRotateYaw.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        bodyToRotateYaw.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
}
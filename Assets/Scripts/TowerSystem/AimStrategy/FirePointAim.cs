using UnityEngine;

public class FirePointAim : MonoBehaviour, IAimStrategy
{
    [Header("需要旋转的部件")]
    [SerializeField] private Transform firePoint; // 激光发射点

    public void Initialize(Tower owner) { } // 这个策略不需要Tower的引用

    public void Aim(Transform target)
    {
        if (firePoint == null) return;
        firePoint.LookAt(target);
    }
}
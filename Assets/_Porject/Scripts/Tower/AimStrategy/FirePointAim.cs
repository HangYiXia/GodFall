using UnityEngine;

public class FirePointAim : MonoBehaviour, IAimStrategy
{
    [Header("��Ҫ��ת�Ĳ���")]
    [SerializeField] private Transform firePoint; // ���ⷢ���

    public void Initialize(Tower owner) { } // ������Բ���ҪTower������

    public void Aim(Transform target)
    {
        if (firePoint == null) return;
        firePoint.LookAt(target);
    }
}
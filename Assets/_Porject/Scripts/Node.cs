using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor; // (��ѡ) ��Ǯ����ʱ����ɫ
    private Color startColor;

    private Renderer rend;

    // --- ɾ���� OnMouseDown, OnMouseEnter, OnMouseExit ---

    [HideInInspector]
    public GameObject turret; // ��ǰ�ڵ��ϵ���


    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position; // ����Ը�����Ҫ����ƫ����
    }

    /// <summary>
    /// �������ͣ������ʱ����BuildManager����
    /// </summary>
    public void OnHoverEnter(bool canAfford)
    {
        if (EventSystem.current.IsPointerOverGameObject()) return; // ��ֹ��͸UI
        if (turret != null) return; // ����Ѿ������ˣ����ı���ɫ

        if (canAfford)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }

    /// <summary>
    /// ������뿪ʱ����BuildManager����
    /// </summary>
    public void OnHoverExit()
    {
        rend.material.color = startColor;
    }
}

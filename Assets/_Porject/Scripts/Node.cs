using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor; // (可选) 金钱不足时的颜色
    private Color startColor;

    private Renderer rend;

    // --- 删除了 OnMouseDown, OnMouseEnter, OnMouseExit ---

    [HideInInspector]
    public GameObject turret; // 当前节点上的塔


    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position; // 你可以根据需要加上偏移量
    }

    /// <summary>
    /// 当鼠标悬停在上面时，由BuildManager调用
    /// </summary>
    public void OnHoverEnter(bool canAfford)
    {
        if (EventSystem.current.IsPointerOverGameObject()) return; // 防止穿透UI
        if (turret != null) return; // 如果已经有塔了，不改变颜色

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
    /// 当鼠标离开时，由BuildManager调用
    /// </summary>
    public void OnHoverExit()
    {
        rend.material.color = startColor;
    }
}

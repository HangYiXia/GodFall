using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    [Header("½»»¥ÊÓ¾õ·´À¡")]
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    private Color startColor;

    private Renderer rend;

    [HideInInspector]
    public GameObject turret;


    void Start()
    {
        rend = GetComponent<Renderer>();

        startColor = rend.material.color;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position;
    }

    public void OnHoverEnter(bool canAfford)
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (canAfford)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }

    public void OnHoverExit()
    {
        rend.material.color = startColor;
    }
}
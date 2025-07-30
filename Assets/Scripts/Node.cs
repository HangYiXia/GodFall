//using UnityEngine;
//using UnityEngine.EventSystems;

//public class Node : MonoBehaviour
//{
//    public Color hoverColor;
//    public Color notEnoughMoney;
//    private Color startColor;
//    private Renderer rend;
//    private Vector3 positionOffset = new Vector3(0, 0.5f, 0);

//    BuildManager buildManager;

//    [HideInInspector]
//    public GameObject turret;
//    [HideInInspector]
//    public TurretBlueprint turretBlueprint;
//    [HideInInspector]
//    public bool isUpgraded = false;

//    private void OnMouseDown()
//    {
//        //if (EventSystem.current.IsPointerOverGameObject()) { return; }
//        if (turret != null)
//        {
//            buildManager.SelectNode(this);
//            return;
//        }
//        if (!buildManager.CanBuild) { return; }
//        BuildTurret(buildManager.GetTurretToBuild());
//    }

//    private void OnMouseEnter()
//    {
//        //if (EventSystem.current.IsPointerOverGameObject()) { return; }
//        if (!buildManager.CanBuild) { return; }
//        if (!buildManager.HasMoney) { rend.material.color = notEnoughMoney; }
//        else { rend.material.color = hoverColor; }
//    }

//    private void OnMouseExit()
//    {
//        rend.material.color = startColor;
//    }

//    // Start is called before the first frame update
//    void Start()
//    {
//        rend = GetComponent<Renderer>();
//        startColor = rend.material.color;
//        buildManager = BuildManager.instance;
//    }

//    void BuildTurret(TurretBlueprint blueprint)
//    {
//        if (PlayerStats.Money < blueprint.cost)
//        {
//            Debug.Log("Not Enough Money To Build That!");
//            return;
//        }
//        PlayerStats.Money -= blueprint.cost;
//        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, transform.position, Quaternion.identity);
//        turret = _turret;
//        GameObject buildEffect = (GameObject)Instantiate(buildManager.buildEffectPrefab, transform.position, Quaternion.identity);
//        Destroy(buildEffect, 1f);
//        turretBlueprint = blueprint;
//    }

//    public void UpgradeTurret()
//    {
//        if (isUpgraded) return;
//        if (PlayerStats.Money < turretBlueprint.upgradeCost)
//        {
//            Debug.Log("Not Enough Money To Upgrade That!");
//            return;
//        }
//        PlayerStats.Money -= turretBlueprint.upgradeCost;
//        Destroy(turret);
//        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradePrefab, transform.position, Quaternion.identity);
//        turret = _turret;
//        GameObject buildEffect = (GameObject)Instantiate(buildManager.buildEffectPrefab, transform.position, Quaternion.identity);
//        Destroy(buildEffect, 1f);

//        isUpgraded = true;
//    }

//    public void SellTurret()
//    {
//        PlayerStats.Money += turretBlueprint.GetSellAmount();

//        GameObject sellEffect = (GameObject)Instantiate(buildManager.sellEffectPrefab, transform.position, Quaternion.identity);
//        Destroy(sellEffect, 1f);

//        Destroy(turret);
//        turretBlueprint = null;
//        isUpgraded = false;
//    }


//}

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

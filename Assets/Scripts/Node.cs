using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoney;
    private Color startColor;
    private Renderer rend;
    private Vector3 positionOffset = new Vector3(0, 0.5f, 0);

    BuildManager buildManager;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) { return; }
        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }
        if (!buildManager.CanBuild) { return; }
        BuildTurret(buildManager.GetTurretToBuild());
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject()) { return; }
        if (!buildManager.CanBuild) { return; }
        if (!buildManager.HasMoney) { rend.material.color = notEnoughMoney; }
        else { rend.material.color = hoverColor; }
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }

    // Update is called once per frame
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }
    
    void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not Enough Money To Build That!");
            return;
        }
        PlayerStats.Money -= blueprint.cost;
        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        GameObject buildEffect = (GameObject)Instantiate(buildManager.buildEffectPrefab, GetBuildPosition(), Quaternion.identity);
        Destroy(buildEffect, 1f);
        turretBlueprint = blueprint;
    }

    public void UpgradeTurret()
    {
        if (isUpgraded) return;
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not Enough Money To Upgrade That!");
            return;
        }
        PlayerStats.Money -= turretBlueprint.upgradeCost;
        Destroy(turret);
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradePrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        GameObject buildEffect = (GameObject)Instantiate(buildManager.buildEffectPrefab, GetBuildPosition(), Quaternion.identity);
        Destroy(buildEffect, 1f);

        isUpgraded = true;
    }

    public void SellTurret()
    {
        PlayerStats.Money += turretBlueprint.GetSellAmount();

        GameObject sellEffect = (GameObject)Instantiate(buildManager.sellEffectPrefab, GetBuildPosition(), Quaternion.identity);
        Destroy(sellEffect, 1f);

        Destroy(turret);
        turretBlueprint = null;
        isUpgraded = false;
    }


}

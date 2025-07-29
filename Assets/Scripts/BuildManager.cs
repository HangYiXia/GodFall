using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    public GameObject buildEffectPrefab;
    public GameObject sellEffectPrefab;

    private TurretBlueprint turretToBuild;
    private Node selectNode;
    public TurretUI turretUI;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    public void SelectNode(Node node)
    {
        if (selectNode == node)
        {
            DeselectNode();
            return;
        }
        selectNode = node;
        turretToBuild = null;

        turretUI.SetTarget(node);
    }
    public void DeselectNode()
    {
        selectNode = null;
        turretUI.Hide();
    }
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }
}

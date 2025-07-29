using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint arrowTower;
    public TurretBlueprint lightningTower;
    public TurretBlueprint catapultTower;

    BuildManager buildManager;
    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectArrowTurret()
    {
        Debug.Log("Arrow Tower Selected!");
        buildManager.SelectTurretToBuild(arrowTower);
    }
    public void SelectLightningTurret()
    {
        Debug.Log("Lightning Tower Selected!");
        buildManager.SelectTurretToBuild(lightningTower);
    }

    public void SelectCatapultTower()
    {
        Debug.Log("Catapult Tower Selected!");
        buildManager.SelectTurretToBuild(catapultTower);
    }

}

using UnityEngine;

public class Shop : MonoBehaviour
{
    public TowerData archerTowerData;
    public TowerData LaserTowerData;
    public TowerData CatapultTowerData;

    BuildManager buildManager;
    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectArrowTurret()
    {
        buildManager.SelectTowerToBuild(archerTowerData);
    }
    public void SelectLightningTurret()
    {
        Debug.Log("Lightning Tower Selected!");
        buildManager.SelectTowerToBuild(LaserTowerData);
    }

    public void SelectCatapultTower()
    {
        Debug.Log("Catapult Tower Selected!");
        buildManager.SelectTowerToBuild(CatapultTowerData);
    }

}

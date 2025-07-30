using UnityEngine;
using UnityEngine.UI;


public class TurretUI : MonoBehaviour
{
    public GameObject ui;
    private Node target;

    public Text upgradeCost;
    public Button upgradeButton;
    public Text sellValue;

    //public void SetTarget(Node _target)
    //{
    //    target = _target;
    //    transform.position = target.transform.position;
    //    if (!target.isUpgraded)
    //    {
    //        upgradeCost.text = "$" + target.turretBlueprint.upgradeCost.ToString();
    //        upgradeButton.interactable = true;
    //    } else
    //    {
    //        upgradeCost.text = "DONE";
    //        upgradeButton.interactable = false;
    //    }

    //    sellValue.text = "$" + target.turretBlueprint.GetSellAmount().ToString();

    //    ui.SetActive(true);
    //}

    //public void Hide()
    //{
    //    ui.SetActive(false);
    //}

    //public void Upgrade()
    //{
    //    target.UpgradeTurret();
    //    BuildManager.instance.DeselectNode();
    //}
    //public void Sell()
    //{
    //    target.SellTurret();
    //    BuildManager.instance.DeselectNode();
    //}
}

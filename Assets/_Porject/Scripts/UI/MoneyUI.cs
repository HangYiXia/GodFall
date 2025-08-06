using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    // Update is called once per frame
    //void Update()
    //{
    //    moneyText.text = "$" + PlayerStats.Money.ToString();    
    //}
    void OnEnable() { EconomyManager.OnMoneyChanged += UpdateMoneyText; }
    void OnDisable() { EconomyManager.OnMoneyChanged -= UpdateMoneyText; }

    void UpdateMoneyText(int newAmount)
    {
        moneyText.text = "$ " + newAmount;
    }
}

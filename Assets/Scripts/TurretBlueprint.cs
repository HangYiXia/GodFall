using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{
    public GameObject prefab;
    public GameObject upgradePrefab;
    public int cost;
    public int upgradeCost;
    private float depreciationRate = 0.8f;
    public int GetSellAmount()
    {
        return (int)Mathf.Floor(cost * depreciationRate);
    }
}

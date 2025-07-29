using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultFireAnimation : MonoBehaviour
{
    public CatapultTower Tower;
    public void LaunchProjectile()
    {
        if (Tower != null)
        {
            Tower.LaunchProjectile();
        }
        else
        {
            Debug.LogError("CatapultTower not assigned");
        }
    }
}

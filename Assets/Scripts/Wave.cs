using UnityEngine;

[System.Serializable]
public class Wave
{
    public GameObject enemy;
    public int count;   // 一波内敌人数量
    public float rate;  // 一波内敌人的生成速率
}

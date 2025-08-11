// 文件名: NodeTypeData.cs
using UnityEngine;

[CreateAssetMenu(fileName = "New Node Type", menuName = "Tower Defense/Node Type Data")]
public class NodeTypeData : ScriptableObject
{
    [Header("节点类型信息")]
    [Tooltip("节点类型名称，例如：普通地块、增幅地块、禁建区域")]
    public string typeName = "New Node Type";

    [Tooltip("该类型节点在游戏场景中显示的材质")]
    public Material nodeMaterial;

    [Tooltip("该类型节点上是否可以建造防御塔")]
    public bool canBuildOn = true;

    // 未来还可以扩展，比如建造在此类地块上的塔有何种特殊加成
    // public float damageBonus = 0f;
    // public float rangeBonus = 0f;
}
using UnityEngine;

/// <summary>
/// 用于定义物品、塔、装备等稀有度的数据资产。
/// 通过创建该对象的实例来定义不同的稀有度等级，例如：普通、稀有、史诗、传说。
/// </summary>
[CreateAssetMenu(fileName = "New Rarity", menuName = "Tower Defense/Rarity Data")]
public class RarityData : ScriptableObject
{
    [Tooltip("稀有度的名称，例如：普通、稀有、史诗")]
    [SerializeField]
    private string _rarityName = "New Rarity";

    [Tooltip("代表这个稀有度的颜色，可用于UI文本、物品边框光效等")]
    [SerializeField]
    private Color _rarityColor = Color.gray;

    [Tooltip("用于随机掉落计算的权重。数值越高，掉落概率越大（越常见）。例如：普通=100，稀有=50，史诗=10")]
    [SerializeField]
    private int _dropWeight = 100;

    public string RarityName => _rarityName;
    public Color RarityColor => _rarityColor;
    public int DropWeight => _dropWeight;
}
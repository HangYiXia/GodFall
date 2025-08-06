using UnityEngine;

/// <summary>
/// 定义一种“属性类型”的数据资产。
/// 它本身不包含数值，只作为属性的“定义”或“蓝图”。
/// 例如，你可以创建一个名为“攻击力”的资产，并描述它的作用。
/// </summary>
[CreateAssetMenu(fileName = "New Attribute", menuName = "Tower Defense/Attribute Data")]
public class AttributeData : ScriptableObject
{
    [Header("属性信息")]
    [Tooltip("属性的名称，例如：攻击力、攻击范围")]
    [SerializeField] private string _attributeName;

    [Tooltip("属性的详细描述，用于UI提示等")]
    [TextArea(3, 5)] // Inspector中显示为3到5行的文本框
    [SerializeField] private string _description;

    #region Public Accessors

    public string AttributeName => _attributeName;
    public string Description => _description;

    #endregion
}
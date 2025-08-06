using UnityEngine;

/// <summary>
/// ����һ�֡��������͡��������ʲ���
/// ������������ֵ��ֻ��Ϊ���Եġ����塱����ͼ����
/// ���磬����Դ���һ����Ϊ�������������ʲ����������������á�
/// </summary>
[CreateAssetMenu(fileName = "New Attribute", menuName = "Tower Defense/Attribute Data")]
public class AttributeData : ScriptableObject
{
    [Header("������Ϣ")]
    [Tooltip("���Ե����ƣ����磺��������������Χ")]
    [SerializeField] private string _attributeName;

    [Tooltip("���Ե���ϸ����������UI��ʾ��")]
    [TextArea(3, 5)] // Inspector����ʾΪ3��5�е��ı���
    [SerializeField] private string _description;

    #region Public Accessors

    public string AttributeName => _attributeName;
    public string Description => _description;

    #endregion
}
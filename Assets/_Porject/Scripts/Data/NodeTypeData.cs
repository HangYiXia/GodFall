// �ļ���: NodeTypeData.cs
using UnityEngine;

[CreateAssetMenu(fileName = "New Node Type", menuName = "Tower Defense/Node Type Data")]
public class NodeTypeData : ScriptableObject
{
    [Header("�ڵ�������Ϣ")]
    [Tooltip("�ڵ��������ƣ����磺��ͨ�ؿ顢�����ؿ顢��������")]
    public string typeName = "New Node Type";

    [Tooltip("�����ͽڵ�����Ϸ��������ʾ�Ĳ���")]
    public Material nodeMaterial;

    [Tooltip("�����ͽڵ����Ƿ���Խ��������")]
    public bool canBuildOn = true;

    // δ����������չ�����罨���ڴ���ؿ��ϵ����к�������ӳ�
    // public float damageBonus = 0f;
    // public float rangeBonus = 0f;
}
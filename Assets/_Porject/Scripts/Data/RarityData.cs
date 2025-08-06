using UnityEngine;

/// <summary>
/// ���ڶ�����Ʒ������װ����ϡ�жȵ������ʲ���
/// ͨ�������ö����ʵ�������岻ͬ��ϡ�жȵȼ������磺��ͨ��ϡ�С�ʷʫ����˵��
/// </summary>
[CreateAssetMenu(fileName = "New Rarity", menuName = "Tower Defense/Rarity Data")]
public class RarityData : ScriptableObject
{
    [Tooltip("ϡ�жȵ����ƣ����磺��ͨ��ϡ�С�ʷʫ")]
    [SerializeField]
    private string _rarityName = "New Rarity";

    [Tooltip("�������ϡ�жȵ���ɫ��������UI�ı�����Ʒ�߿��Ч��")]
    [SerializeField]
    private Color _rarityColor = Color.gray;

    [Tooltip("���������������Ȩ�ء���ֵԽ�ߣ��������Խ��Խ�����������磺��ͨ=100��ϡ��=50��ʷʫ=10")]
    [SerializeField]
    private int _dropWeight = 100;

    public string RarityName => _rarityName;
    public Color RarityColor => _rarityColor;
    public int DropWeight => _dropWeight;
}
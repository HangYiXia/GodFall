// �ļ���: MapThemeData.cs
using UnityEngine;

[CreateAssetMenu(fileName = "New Map Theme", menuName = "Tower Defense/Map Theme Data")]
public class MapThemeData : ScriptableObject
{
    [Header("������Ϣ")]
    [Tooltip("�������ƣ����磺ɭ�֡���ɽ��ѩ��")]
    public string themeName = "New Theme";

    [Header("�����뻷�� (Materials & Environment)")]
    [Tooltip("������Ҫ����Ĳ���")]
    public Material environmentMaterial;

    [Tooltip("�������ߵĵ�·�Ĳ���")]
    public Material roadMaterial;

    [Tooltip("�ó���ʹ�õ���պв���")]
    public Material skyboxMaterial;

    // δ����������չ��������볡���������֡���������ɫ��
    // public AudioClip backgroundMusic;
    // public Color ambientLightColor = Color.white;
}
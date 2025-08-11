// 文件名: MapThemeData.cs
using UnityEngine;

[CreateAssetMenu(fileName = "New Map Theme", menuName = "Tower Defense/Map Theme Data")]
public class MapThemeData : ScriptableObject
{
    [Header("主题信息")]
    [Tooltip("主题名称，例如：森林、火山、雪地")]
    public string themeName = "New Theme";

    [Header("材质与环境 (Materials & Environment)")]
    [Tooltip("场景主要地面的材质")]
    public Material environmentMaterial;

    [Tooltip("敌人行走的道路的材质")]
    public Material roadMaterial;

    [Tooltip("该场景使用的天空盒材质")]
    public Material skyboxMaterial;

    // 未来还可以扩展，比如加入场景背景音乐、环境光颜色等
    // public AudioClip backgroundMusic;
    // public Color ambientLightColor = Color.white;
}
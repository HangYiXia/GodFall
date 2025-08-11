using UnityEngine;
using System.IO;

public class MapLoader : MonoBehaviour
{
    public string mapFileName = "map_01.json"; // ���� Resources/Maps ��
    public float nodeSize = 1f;

    public GameObject groundPrefab;
    public GameObject roadPrefab;
    public GameObject blockPrefab;

    void Start()
    {
        LoadAndGenerate();
    }

    void LoadAndGenerate()
    {
        // �� Resources ����
        TextAsset mapJson = Resources.Load<TextAsset>("Maps/" + Path.GetFileNameWithoutExtension(mapFileName));
        if (mapJson == null)
        {
            Debug.LogError("��ͼ�ļ�δ�ҵ���" + mapFileName);
            return;
        }

        // JSON �� MapData
        MapData mapData = JsonUtility.FromJson<MapData>(mapJson.text);

        Debug.Log("mapData.layout.Length = " + mapData.layout.Length);
        for (int y = 0; y < mapData.height; y++)
        {
            for (int x = 0; x < mapData.width; x++)
            {
                int index = y * mapData.width + x;
                int type = mapData.layout[index]; // ��һά�����ȡ

                Vector3 worldPos = new Vector3(x * nodeSize, 0, y * nodeSize);

                GameObject prefab = null;
                switch (type)
                {
                    case 0: prefab = groundPrefab; break;
                    case 1: prefab = roadPrefab; break;
                    case 2: prefab = blockPrefab; break;
                }

                if (prefab != null)
                    Instantiate(prefab, worldPos, Quaternion.identity, this.transform);
            }
        }
    }
}

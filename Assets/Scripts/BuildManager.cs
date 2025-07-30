//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.EventSystems;

//public class BuildManager : MonoBehaviour
//{
//    public static BuildManager instance;

//    public GameObject buildEffectPrefab;
//    public GameObject sellEffectPrefab;

//    private TurretBlueprint turretToBuild;
//    private Node selectNode;
//    public TurretUI turretUI;

//    [Header("手动射线检测设置")]
//    [SerializeField] private LayerMask nodeLayerMask; // 在Inspector中选择Environment图层


//    void Update()
//    {
//        // 如果玩家点击了鼠标左键
//        if (Input.GetMouseButtonDown(0))
//        {
//            // 检查是否选择了要建造的塔，并且UI没有挡住鼠标
//            if (turretToBuild == null || EventSystem.current.IsPointerOverGameObject())
//            {
//                return;
//            }

//            // 从摄像机向鼠标位置发射一条射线
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            RaycastHit hit;

//            // 进行射线检测，但只检测在 nodeLayerMask 上的物体
//            if (Physics.Raycast(ray, out hit, 1000f, nodeLayerMask))
//            {
//                // 射线命中了某个物体，我们尝试从它身上获取Node脚本
//                Node node = hit.collider.GetComponent<Node>();
//                if (node != null)
//                {
//                    // 这就是我们命中的Node！在这里执行建造逻辑
//                    // 将之前在Node.OnMouseDown()里的逻辑搬到这里
//                    if (PlayerStats.Money < turretToBuild.cost)
//                    {
//                        Debug.Log("金钱不足!");
//                        return;
//                    }

//                    PlayerStats.Money -= turretToBuild.cost;

//                    GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.transform.position, Quaternion.identity);
//                    node.turret = turret;

//                    Debug.Log("建造成功! 剩余金钱: " + PlayerStats.Money);
//                }
//            }
//        }
//    }


//    private void Awake()
//    {
//        if (instance != null)
//        {
//            Debug.LogError("More than one BuildManager in scene!");
//            return;
//        }
//        instance = this;
//    }

//    public bool CanBuild { get { return turretToBuild != null; } }
//    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

//    public void SelectNode(Node node)
//    {
//        if (selectNode == node)
//        {
//            DeselectNode();
//            return;
//        }
//        selectNode = node;
//        turretToBuild = null;

//        turretUI.SetTarget(node);
//    }
//    public void DeselectNode()
//    {
//        selectNode = null;
//        turretUI.Hide();
//    }
//    public void SelectTurretToBuild(TurretBlueprint turret)
//    {
//        turretToBuild = turret;
//        DeselectNode();
//    }

//    public TurretBlueprint GetTurretToBuild()
//    {
//        return turretToBuild;
//    }
//}

using UnityEngine;

public class BuildManager : MonoBehaviour
{
    // --- 保留你已有的所有变量，比如单例，turretToBuild等 ---
    public static BuildManager instance;
    void Awake() { instance = this; }
    private TurretBlueprint turretToBuild;


    [Header("射线检测设置")]
    [SerializeField] private LayerMask nodeLayerMask;

    // --- 新增一个变量，用于跟踪当前悬停的节点 ---
    private Node hoveredNode;

    void Update()
    {
        // 这个顶部的判断保持不变，它工作得很好
        if (turretToBuild == null || UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            // 如果之前有高亮的节点，鼠标移开时要把它恢复原状
            if (hoveredNode != null)
            {
                hoveredNode.OnHoverExit();
                hoveredNode = null;
            }
            return;
        }

        // --- 以下是重构的核心逻辑 ---

        // 步骤 1: 找出鼠标下方到底有没有一个“有效”的Node
        Node nodeUnderMouse = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000f, nodeLayerMask))
        {
            // 即使射线命中了物体，我们也要再次确认它上面有Node脚本
            nodeUnderMouse = hit.collider.GetComponent<Node>();
        }

        // 步骤 2: 比较“当前找到的Node”和“上一帧的Node”，并更新高亮状态
        if (nodeUnderMouse != hoveredNode)
        {
            // 如果上一帧的Node是有效的，让它恢复原状
            if (hoveredNode != null)
            {
                hoveredNode.OnHoverExit();
            }

            // 如果当前找到的Node是有效的，让它高亮
            if (nodeUnderMouse != null && nodeUnderMouse.turret == null) // 同时检查节点上是否已有塔
            {
                nodeUnderMouse.OnHoverEnter(PlayerStats.Money >= turretToBuild.cost);
            }

            // 最后，更新我们的“跟踪器”
            hoveredNode = nodeUnderMouse;
        }

        // 步骤 3: 处理点击建造的逻辑（这部分逻辑不变，但现在更安全了）
        if (Input.GetMouseButtonDown(0) && hoveredNode != null && hoveredNode.turret == null)
        {
            BuildTurretOn(hoveredNode);
        }
    }

    void BuildTurretOn(Node node)
    {
        if (PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("金钱不足!");
            return;
        }

        PlayerStats.Money -= turretToBuild.cost;
        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;
        Debug.Log("建造成功!");
    }

    // 你的 SelectTurretToBuild 等其他方法保持不变
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }
}
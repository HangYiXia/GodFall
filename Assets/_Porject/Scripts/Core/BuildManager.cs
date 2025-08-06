using UnityEngine;
using UnityEngine.EventSystems;

public class BuildManager : MonoBehaviour
{
    // --- 保留你已有的所有变量，比如单例，turretToBuild等 ---
    public static BuildManager instance;
    void Awake() { instance = this; }
    public enum BuildManagerState { Idle, PlacingTurret, InspectingNode }
    public BuildManagerState currentState;

    private TowerData selectedTowerData;

    [Header("射线检测设置")]
    [SerializeField] private LayerMask nodeLayerMask;

    private Node hoveredNode;
    private Node selectedNode; // 用于升级/出售

    void Start()
    {
        currentState = BuildManagerState.Idle;
    }

    void Update()
    {
        // 通用的射线检测可以放在前面
        RaycastForNode();

        switch (currentState)
        {
            case BuildManagerState.Idle:
                HandleIdleState();
                break;
            case BuildManagerState.PlacingTurret:
                HandlePlacingState();
                break;
            case BuildManagerState.InspectingNode:
                HandleInspectingState();
                break;
        }
    }

    private void RaycastForNode()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            if (hoveredNode != null)
            {
                hoveredNode.OnHoverExit();
                hoveredNode = null;
            }
            return;
        }

        Node nodeUnderMouse = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000f, nodeLayerMask))
        {
            nodeUnderMouse = hit.collider.GetComponent<Node>();
        }

        if (nodeUnderMouse != hoveredNode)
        {
            if (hoveredNode != null)
            {
                hoveredNode.OnHoverExit();
            }

            if (nodeUnderMouse != null && nodeUnderMouse.turret == null)
            {
                bool canAfford = EconomyManager.instance.CanAfford(selectedTowerData.initialCost);
                nodeUnderMouse.OnHoverEnter(canAfford);
            }

            hoveredNode = nodeUnderMouse;
        }

    }

    private void HandleIdleState()
    {
        // 在空闲状态下，点击有塔的节点，则选中它
        if (Input.GetMouseButtonDown(0) && hoveredNode != null && hoveredNode.turret != null)
        {
            SelectNode(hoveredNode);
        }
    }

    private void HandlePlacingState()
    {
        // 放置逻辑
        if (Input.GetMouseButtonDown(0) && hoveredNode != null && hoveredNode.turret == null)
        {
            BuildTurretOn(hoveredNode);
        }
        // 按右键取消建造
        if (Input.GetMouseButtonDown(1))
        {
            DeselectTurretToBuild();
        }
    }

    private void HandleInspectingState()
    {
        // 在观察状态下，如果点击了其他地方，则取消选中
        if (Input.GetMouseButtonDown(0) && hoveredNode != selectedNode)
        {
            DeselectNode();
        }
    }

    public void SelectTowerToBuild(TowerData towerData)
    {
        selectedTowerData = towerData;
        currentState = BuildManagerState.PlacingTurret;
    }

    private void SelectNode(Node node)
    {
        DeselectTurretToBuild(); // 不能同时选中节点又准备造塔
        selectedNode = node;
        currentState = BuildManagerState.InspectingNode;
        // nodeUI.Show(selectedNode); // 显示UI
    }

    private void DeselectTurretToBuild()
    {
        selectedTowerData = null;
        if (hoveredNode != null) hoveredNode.OnHoverExit(); // 退出时恢复颜色
        currentState = BuildManagerState.Idle;
    }

    private void DeselectNode()
    {
        selectedNode = null;
        currentState = BuildManagerState.Idle;
        // nodeUI.Hide(); // 隐藏UI
    }

    void BuildTurretOn(Node node)
    {
        // 推荐使用独立的EconomyManager
        if (!EconomyManager.instance.CanAfford(selectedTowerData.initialCost))
        {
            Debug.Log("金钱不足!");
            return;
        }

        EconomyManager.instance.SpendMoney(selectedTowerData.initialCost);
        GameObject turret = (GameObject)Instantiate(selectedTowerData.basePrefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;
        // ... 其他建造效果 ...

        // 建造完后回到空闲状态
        DeselectTurretToBuild();
    }
}
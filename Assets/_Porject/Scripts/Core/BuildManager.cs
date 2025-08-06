using UnityEngine;
using UnityEngine.EventSystems;

public class BuildManager : MonoBehaviour
{
    // --- ���������е����б��������絥����turretToBuild�� ---
    public static BuildManager instance;
    void Awake() { instance = this; }
    public enum BuildManagerState { Idle, PlacingTurret, InspectingNode }
    public BuildManagerState currentState;

    private TowerData selectedTowerData;

    [Header("���߼������")]
    [SerializeField] private LayerMask nodeLayerMask;

    private Node hoveredNode;
    private Node selectedNode; // ��������/����

    void Start()
    {
        currentState = BuildManagerState.Idle;
    }

    void Update()
    {
        // ͨ�õ����߼����Է���ǰ��
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
        // �ڿ���״̬�£���������Ľڵ㣬��ѡ����
        if (Input.GetMouseButtonDown(0) && hoveredNode != null && hoveredNode.turret != null)
        {
            SelectNode(hoveredNode);
        }
    }

    private void HandlePlacingState()
    {
        // �����߼�
        if (Input.GetMouseButtonDown(0) && hoveredNode != null && hoveredNode.turret == null)
        {
            BuildTurretOn(hoveredNode);
        }
        // ���Ҽ�ȡ������
        if (Input.GetMouseButtonDown(1))
        {
            DeselectTurretToBuild();
        }
    }

    private void HandleInspectingState()
    {
        // �ڹ۲�״̬�£��������������ط�����ȡ��ѡ��
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
        DeselectTurretToBuild(); // ����ͬʱѡ�нڵ���׼������
        selectedNode = node;
        currentState = BuildManagerState.InspectingNode;
        // nodeUI.Show(selectedNode); // ��ʾUI
    }

    private void DeselectTurretToBuild()
    {
        selectedTowerData = null;
        if (hoveredNode != null) hoveredNode.OnHoverExit(); // �˳�ʱ�ָ���ɫ
        currentState = BuildManagerState.Idle;
    }

    private void DeselectNode()
    {
        selectedNode = null;
        currentState = BuildManagerState.Idle;
        // nodeUI.Hide(); // ����UI
    }

    void BuildTurretOn(Node node)
    {
        // �Ƽ�ʹ�ö�����EconomyManager
        if (!EconomyManager.instance.CanAfford(selectedTowerData.initialCost))
        {
            Debug.Log("��Ǯ����!");
            return;
        }

        EconomyManager.instance.SpendMoney(selectedTowerData.initialCost);
        GameObject turret = (GameObject)Instantiate(selectedTowerData.basePrefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;
        // ... ��������Ч�� ...

        // �������ص�����״̬
        DeselectTurretToBuild();
    }
}
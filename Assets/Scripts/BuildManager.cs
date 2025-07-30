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

//    [Header("�ֶ����߼������")]
//    [SerializeField] private LayerMask nodeLayerMask; // ��Inspector��ѡ��Environmentͼ��


//    void Update()
//    {
//        // �����ҵ����������
//        if (Input.GetMouseButtonDown(0))
//        {
//            // ����Ƿ�ѡ����Ҫ�������������UIû�е�ס���
//            if (turretToBuild == null || EventSystem.current.IsPointerOverGameObject())
//            {
//                return;
//            }

//            // ������������λ�÷���һ������
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            RaycastHit hit;

//            // �������߼�⣬��ֻ����� nodeLayerMask �ϵ�����
//            if (Physics.Raycast(ray, out hit, 1000f, nodeLayerMask))
//            {
//                // ����������ĳ�����壬���ǳ��Դ������ϻ�ȡNode�ű�
//                Node node = hit.collider.GetComponent<Node>();
//                if (node != null)
//                {
//                    // ������������е�Node��������ִ�н����߼�
//                    // ��֮ǰ��Node.OnMouseDown()����߼��ᵽ����
//                    if (PlayerStats.Money < turretToBuild.cost)
//                    {
//                        Debug.Log("��Ǯ����!");
//                        return;
//                    }

//                    PlayerStats.Money -= turretToBuild.cost;

//                    GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.transform.position, Quaternion.identity);
//                    node.turret = turret;

//                    Debug.Log("����ɹ�! ʣ���Ǯ: " + PlayerStats.Money);
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
    // --- ���������е����б��������絥����turretToBuild�� ---
    public static BuildManager instance;
    void Awake() { instance = this; }
    private TurretBlueprint turretToBuild;


    [Header("���߼������")]
    [SerializeField] private LayerMask nodeLayerMask;

    // --- ����һ�����������ڸ��ٵ�ǰ��ͣ�Ľڵ� ---
    private Node hoveredNode;

    void Update()
    {
        // ����������жϱ��ֲ��䣬�������úܺ�
        if (turretToBuild == null || UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            // ���֮ǰ�и����Ľڵ㣬����ƿ�ʱҪ�����ָ�ԭ״
            if (hoveredNode != null)
            {
                hoveredNode.OnHoverExit();
                hoveredNode = null;
            }
            return;
        }

        // --- �������ع��ĺ����߼� ---

        // ���� 1: �ҳ�����·�������û��һ������Ч����Node
        Node nodeUnderMouse = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000f, nodeLayerMask))
        {
            // ��ʹ�������������壬����ҲҪ�ٴ�ȷ����������Node�ű�
            nodeUnderMouse = hit.collider.GetComponent<Node>();
        }

        // ���� 2: �Ƚϡ���ǰ�ҵ���Node���͡���һ֡��Node���������¸���״̬
        if (nodeUnderMouse != hoveredNode)
        {
            // �����һ֡��Node����Ч�ģ������ָ�ԭ״
            if (hoveredNode != null)
            {
                hoveredNode.OnHoverExit();
            }

            // �����ǰ�ҵ���Node����Ч�ģ���������
            if (nodeUnderMouse != null && nodeUnderMouse.turret == null) // ͬʱ���ڵ����Ƿ�������
            {
                nodeUnderMouse.OnHoverEnter(PlayerStats.Money >= turretToBuild.cost);
            }

            // ��󣬸������ǵġ���������
            hoveredNode = nodeUnderMouse;
        }

        // ���� 3: ������������߼����ⲿ���߼����䣬�����ڸ���ȫ�ˣ�
        if (Input.GetMouseButtonDown(0) && hoveredNode != null && hoveredNode.turret == null)
        {
            BuildTurretOn(hoveredNode);
        }
    }

    void BuildTurretOn(Node node)
    {
        if (PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("��Ǯ����!");
            return;
        }

        PlayerStats.Money -= turretToBuild.cost;
        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;
        Debug.Log("����ɹ�!");
    }

    // ��� SelectTurretToBuild �������������ֲ���
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }
}
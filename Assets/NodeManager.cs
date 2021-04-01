using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;

    [SerializeField] private Node _clickedNode;

    [SerializeField] private GameObject _connectorPrefab;

    [SerializeField] private GameObject _connectorObject;
    [SerializeField] private Transform _clickedPosition;

    [SerializeField] private List<NodeConnector> _connectorsList = new List<NodeConnector>();
    [SerializeField] private List<OutputNode> _outputNodes = new List<OutputNode>();

    #region Unity Methods

    private void OnEnable()
    {
        InputManager.NodeClicked += OnNodeClicked;
        InputManager.MouseReleased += OnMouseRelease;

        OutputNode.DesiredColorAchieved += CheckAllOutputNodes;

        GameObject[] outputNodes = GameObject.FindGameObjectsWithTag("OutputNode");
        foreach (GameObject gameObject in outputNodes)
        {
            _outputNodes.Add(gameObject.GetComponent<OutputNode>());
        }
    }

    private void OnDisable()
    {
        InputManager.NodeClicked -= OnNodeClicked;
        InputManager.MouseReleased -= OnMouseRelease;
    }
    private void Update()
    {
        if(_clickedNode)
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 direction = position - _clickedPosition.position;

            Vector3 scale = _connectorObject.transform.localScale;
            scale.x = -direction.x;
            _connectorObject.transform.localScale = scale;

            direction = direction.normalized;
            _connectorObject.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
   
        }
    }

    #endregion Unity Methods

    private void OnNodeClicked(Node node)
    {
        /// TODO: Create Enum and use node.NodeType to check
        OutputNode outputNode = node as OutputNode;
        MixingNode mixingNode = node as MixingNode;

        if(!this._clickedNode)
        {
            if (outputNode)
            {
                Debug.Log("Clicked On Output Node");
                return;
            }

            if(mixingNode && mixingNode.InputColors <= 0)
            {
                Debug.Log("Clicked On Mixing Node and input colors are NULL");
                return;
            }
        }

        this._clickedNode = node;

        _connectorObject = Instantiate(_connectorPrefab);
        _connectorObject.transform.SetParent(node.gameObject.transform);
        _connectorObject.transform.position = node.gameObject.transform.position;
        _connectorObject.GetComponent<NodeConnector>().SetColor(node.NodeColor);

        _clickedPosition = node.transform;
    }


    private void OnMouseRelease()
    {
        /// TODO: Perform checks here as well.
        if (!_clickedNode)
        {
            Debug.Log("ret");
            return;
        }

        // Ray cast down to see the if we can connect to the node.
        RaycastHit2D hit;

        Vector3 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        hit = Physics2D.Raycast(origin, Vector3.forward, 10f);

        NodeConnector connector = _connectorObject.GetComponent<NodeConnector>();

        if (hit)
        {
            Debug.Log(hit.collider.gameObject.name);
            Node node = hit.collider.gameObject.GetComponent<Node>();

            node.SetColor(_clickedNode.NodeColor);

            Vector3 scale = NodeConnectionScale(node, _clickedPosition.position);

            _connectorObject.transform.localScale = scale;


            _connectorsList.Add(connector);
        }
        else
        {
            connector.Destroy();
        }

        this._connectorObject = null;
        this._clickedPosition = null;
        this._clickedNode = null;
    }

    private Vector3 NodeConnectionScale(Node node, Vector3 position)
    {
        Vector3 direction = node.transform.position - position;

        Vector3 scale = _connectorObject.transform.localScale;
        scale.x = -direction.x;
        return scale;
    }

    private void CheckAllOutputNodes()
    {
        foreach (OutputNode node in _outputNodes)
        {
            if (!node.IsColorSet)
                return;
        }

        Debug.Log("Level Completed");
    }
}

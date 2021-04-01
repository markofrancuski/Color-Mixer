using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    [SerializeField] private Node _clickedNode;

    [SerializeField] private InputManager _inputManager;

    [SerializeField] private List<OutputNode> _outputNodes = new List<OutputNode>();

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


    private void CheckAllOutputNodes()
    {
        foreach (OutputNode node  in _outputNodes)
        {
            if (!node.IsColorSet)
                return;
        }

        Debug.Log("Level Completed");
    }

    private void OnNodeClicked(Node node)
    {
        this._clickedNode = node;
    }

    private void OnMouseRelease()
    {
        // Ray cast down to see the if we can connect to the node.
        RaycastHit2D hit;

        Vector3 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        hit = Physics2D.Raycast(origin, Vector3.forward, 10f);

        if (hit)
        {
            Debug.Log(hit.collider.gameObject.name);
            Node node = hit.collider.gameObject.GetComponent<Node>();

            node.SetColor(_clickedNode.NodeColor);
        }

        this._clickedNode = null;
    }

}

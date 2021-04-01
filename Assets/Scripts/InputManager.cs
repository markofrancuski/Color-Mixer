using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static Action<Node> NodeClicked;
    public static Action MouseReleased;

    [SerializeField] private bool _isClicked;



    private void Update()
    {

        if (_isClicked)
        {
            if (!Input.GetMouseButton(0))
            {
                ReleaseClick();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit;

                Vector3 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                hit = Physics2D.Raycast(origin, Vector3.forward, 10f);

                if (hit)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    Node node = hit.collider.gameObject.GetComponent<Node>();

                    NodeClicked?.Invoke(node);
                    _isClicked = true;

                }

                Debug.DrawRay(origin, Vector3.forward, Color.red, 20f);
            }
        }
    }

    private void ReleaseClick()
    {
        _isClicked = false;
        MouseReleased?.Invoke();
    }
}

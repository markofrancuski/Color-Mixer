using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeConnector : MonoBehaviour
{

    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private Transform _from;
    [SerializeField] private Transform _to;

    public void SetColor(Color color)
    {
        _spriteRenderer.color = color;
    }
}

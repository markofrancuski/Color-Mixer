using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class Node : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer Renderer;

    [SerializeField]
    protected Color _nodeColor;
    public Color NodeColor => _nodeColor;

    public virtual int MaxInputColors => 2;

    private void Awake()
    {
        if(Renderer == null)
            Renderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
   
    }

    public virtual void SetColor(Color color)
    {
      
    }
}

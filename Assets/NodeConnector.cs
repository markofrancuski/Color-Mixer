using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeConnector : MonoBehaviour
{

    [SerializeField] private SpriteRenderer _spriteRenderer;

    public float Lenght = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (!_spriteRenderer)
            _spriteRenderer = GetComponent<SpriteRenderer>();

        Lenght = _spriteRenderer.sprite.bounds.size.x;
    }

    public void SetColor(Color color)
    {
        _spriteRenderer.color = color;
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

}

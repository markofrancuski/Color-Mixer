using UnityEngine;

public class NodeConnector : MonoBehaviour
{

    [SerializeField] private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        if (!_spriteRenderer)
            _spriteRenderer = GetComponent<SpriteRenderer>();
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

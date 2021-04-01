using System.Collections.Generic;
using UnityEngine;

public class MixingNode : Node
{

    [SerializeField] private Queue<Color> _inputColors = new Queue<Color>();
    [SerializeField] private List<Color> _inputColorsList = new List<Color>();

    public override void SetColor(Color color)
    {
        _nodeColor = color;

        _inputColors.Enqueue(color);
        _inputColorsList.Add(color);

        if (_inputColors.Count > MaxInputColors)
        {
            _inputColorsList.RemoveAt(0);
            _inputColors.Dequeue();
        }

        RecalculateColor();
    }

    private void RecalculateColor()
    {
        Vector3 mixedColor = new Vector3();
        float alpha = 0;

        foreach (Color color in _inputColors)
        {
            mixedColor.x += color.r;
            mixedColor.y += color.g;
            mixedColor.z += color.b;
            alpha += color.a;
        }

        mixedColor.x = Mathf.Clamp(mixedColor.x, 0, 255);
        mixedColor.y = Mathf.Clamp(mixedColor.y, 0, 255);
        mixedColor.z = Mathf.Clamp(mixedColor.z, 0, 255);
        alpha = Mathf.Clamp(alpha, 0, 1);

        Color newColor = new Color(mixedColor.x, mixedColor.y, mixedColor.z, alpha);
        _nodeColor = newColor;
        Renderer.color = newColor;
    }

}

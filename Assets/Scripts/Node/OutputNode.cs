using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputNode : Node
{
    public static Action DesiredColorAchieved;

    [SerializeField] private Color _desiredColor;

    public override int MaxInputColors => 1;

    public bool IsColorSet;

    protected override void Start()
    {
        _desiredColor = Renderer.color;
    }

    public void SetColor(Color color)
    {
        _nodeColor = color;

        if (IsDesiredColor(color))
        {
            IsColorSet = true;
            DesiredColorAchieved?.Invoke();
        }
    }

    private bool IsDesiredColor(Color color)
    {

        if (color.r != _desiredColor.r)
            return false;

        if (color.g != _desiredColor.g)
            return false;

        if (color.b != _desiredColor.b)
            return false;

        if (color.a != _desiredColor.a)
            return false;


        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputNode : Node 
{
    protected override void Start()
    {
        _nodeColor = Renderer.color;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableInspector : MonoBehaviour
{
    public static bool renderVariableGui = false;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            renderVariableGui = !renderVariableGui;
        }
    }
}

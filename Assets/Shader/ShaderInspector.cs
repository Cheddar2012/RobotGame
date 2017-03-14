using UnityEngine;
using UnityEditor;
using System;

public class ShaderInspector : ShaderGUI
{
    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        // Use the default fields
        base.OnGUI(materialEditor, properties);

        Material targetMat = materialEditor.target as Material;

        // Get whether the keyword exists
        bool orthographic = Array.IndexOf(targetMat.shaderKeywords, "ORTHOGRAPHIC_IMPLEMENTATION") != -1;

        EditorGUI.BeginChangeCheck();

        // Add a checkbox to toggle the keyword
        orthographic = EditorGUILayout.Toggle("Orthographic Implementation", orthographic);
        if (EditorGUI.EndChangeCheck())
        {
            // Toggle the keyword on or off based on the checkbox
            if (orthographic)
            {
                targetMat.EnableKeyword("ORTHOGRAPHIC_IMPLEMENTATION");
            }
            else
            {
                targetMat.DisableKeyword("ORTHOGRAPHIC_IMPLEMENTATION");
            }
        }
    }
}
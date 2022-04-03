using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TerrainForester))]
public class TerrainForesterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TerrainForester myScript = (TerrainForester)target;
        if(GUILayout.Button("Make Forest"))
        {
            myScript.makeForest();
        }

        if(GUILayout.Button("Clear Forest"))
        {
            myScript.clearForest();
        }
    }
}

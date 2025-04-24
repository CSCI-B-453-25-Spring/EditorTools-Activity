#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TreePlacer))]
public class TreePlacerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TreePlacer placer = (TreePlacer)target;
        if (GUILayout.Button("Place Trees"))
        {
            placer.PlaceTrees();
        }
    }
}
#endif
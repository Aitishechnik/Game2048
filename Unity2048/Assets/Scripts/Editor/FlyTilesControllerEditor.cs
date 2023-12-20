using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

[CustomEditor(typeof(FlyTilesController))]
public class FlyTilesControllerEditor : Editor
{
    private TileView from;
    private TileView to;
    private float time;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        from = EditorGUILayout.ObjectField(from, typeof(TileView), true) as TileView;
        to = EditorGUILayout.ObjectField(to, typeof(TileView), true) as TileView;
        time = EditorGUILayout.FloatField(time);
        if (GUILayout.Button("Fly"))
        {
            var flyTiles = target as FlyTilesController;
            flyTiles.Fly(from,to,time);
        }
    }
}

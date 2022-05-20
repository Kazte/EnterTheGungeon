
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        
        base.OnInspectorGUI();

        var mapGen = (MapGenerator) target;

        GUILayout.Space(8f);
        
        GUILayout.BeginHorizontal();
        
        if (GUILayout.Button("Generate"))
        {

            mapGen.ProceduralGeneration();
        }
        
        GUILayout.Label(mapGen.GetCurrentSeed().ToString());
        
        GUILayout.EndHorizontal();
        
        GUILayout.Space(8f);
        
        if (GUILayout.Button("Generate With Seed"))
        {
            mapGen.ProceduralGenerationWithSeed();
        }

        GUILayout.Space(8f);
        
        if (GUILayout.Button("Clear Room Spawned"))
        {
            mapGen.ClearRoomSpawnedList();
        }
        
    }
}

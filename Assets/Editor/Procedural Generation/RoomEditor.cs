
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Room))]
public class RoomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        var room = (Room) target;
        
        GUILayout.Space(8f);
        if (GUILayout.Button("Get Entraces Point"))
        {
            room.GetDoors();
        }
    }
}

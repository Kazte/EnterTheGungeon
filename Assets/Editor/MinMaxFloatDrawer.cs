using UnityEditor;
using UnityEngine;

// [CustomPropertyDrawer(typeof(MinMaxFloat))]
public class MinMaxFloatDrawer : PropertyDrawer
{
    private const int Y_DISTANCE = 20;
    private const int FIELD_HEIGHT = 16;

    private float minValue;
    private float maxValue;
    public override void OnGUI(Rect position, SerializedProperty property,
        GUIContent label)
    {
        // minValue = property.FindPropertyRelative("min").floatValue;
        // maxValue = property.FindPropertyRelative("max").floatValue;
        
        EditorGUILayout.LabelField("Min Val:", minValue.ToString());
        EditorGUILayout.LabelField("Max Val:", maxValue.ToString());
        
        // EditorGUILayout.MinMaxSlider(ref minValue, ref maxValue, 0f, 1f);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label);
    }
}
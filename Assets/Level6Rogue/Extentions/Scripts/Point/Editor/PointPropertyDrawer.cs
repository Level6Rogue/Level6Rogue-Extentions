using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Point))]
public class PointPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty xProperty = property.FindPropertyRelative("x");
        SerializedProperty yProperty = property.FindPropertyRelative("y");

        EditorGUI.LabelField(new Rect(position.x, position.y, EditorGUIUtility.labelWidth, 16), label);

        float width = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = 35;
        EditorGUI.PropertyField(new Rect(position.x + position.width / 2, position.y, position.width / 2, 16), xProperty);
        EditorGUI.PropertyField(new Rect(position.x + position.width / 2, position.y + 18, position.width / 2, 16), yProperty);
        EditorGUIUtility.labelWidth = width;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 34;
    }
}
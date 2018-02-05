using UnityEngine;
using UnityEditor;
using Level6Rogue.ReorderableList;

namespace Level6Rogue.SpriteRotating
{
    [CustomEditor(typeof(RotatedSpriteGroupControler))]
    public class RotatedSpriteGroupControler_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("IsRight"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("PixelSize"));
            SerializedProperty boneDataProperty = serializedObject.FindProperty("boneData");

            ReorderableListGUI.Title(boneDataProperty.displayName);
            ReorderableListGUI.ListField(boneDataProperty);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
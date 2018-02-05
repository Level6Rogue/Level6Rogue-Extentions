using UnityEngine;
using UnityEditor;
using Level6Rogue.Extentions;

namespace Level6Rogue.SpriteRotating
{
    [CustomPropertyDrawer(typeof(BoneChildData))]
    public class BoneChildData_PropertyDrawer : PropertyDrawer
    {
        private static readonly Color ACTIVE_COLOR = new Color(0.6f, 1f, 0.6f);
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty updateRecieverProperty = property.FindPropertyRelative("updateReciever");

            EditorGUI.PropertyField(new Rect(position.x, position.y, position.width, 16), updateRecieverProperty);
            if (updateRecieverProperty.objectReferenceValue != null)
            {
                using (new ColorBlock(ACTIVE_COLOR))
                EditorGUI.PropertyField(new Rect(position.x, position.y + 18, position.width, 16), property.FindPropertyRelative("offset"));
                EditorGUI.PropertyField(new Rect(position.x, position.y + 54, position.width, 16), property.FindPropertyRelative("RotationPassThrough"));
                EditorGUI.PropertyField(new Rect(position.x, position.y + 70, position.width, 16), property.FindPropertyRelative("ResetOffset"));
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty updateRecieverProperty = property.FindPropertyRelative("updateReciever");

            if (updateRecieverProperty.objectReferenceValue != null)
                return 86;
            return 16;
        }
    } 
}
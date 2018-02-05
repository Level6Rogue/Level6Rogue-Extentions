using UnityEngine;
using UnityEditor;
using Level6Rogue.ReorderableList;
using Level6Rogue.Extentions;

namespace Level6Rogue.SpriteRotating
{
    [CustomEditor(typeof(RotatedSprite))]
    public class RotatedSprite_Editor : Editor
    {
        RotatedSprite Target;
        private void OnEnable()
        {
            Target = target as RotatedSprite;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if (GUILayout.Button("Clear Dictionary", EditorStyles.miniButton))
            {
                Target.rotationData.Clear();
                Target.DoUpdate(Vector2.zero);
                EditorUtility.SetDirty(Target);
            }
            if (Target.spriteRenderer && Target.spriteRenderer.sprite)
                DrawRotatedSprite(Target);


            EditorGUILayout.PropertyField(serializedObject.FindProperty("ActiveSprite"));
            if (Target.ActiveSprite && 
            (Target.ActiveSprite.texture.width % 2 == 1 || Target.ActiveSprite.texture.height % 2 == 1 ||
            Target.ActiveSprite.texture.width  != Target.ActiveSprite.texture.height))
            {
                EditorGUILayout.HelpBox("Texture must have an odd width and height and must be square.", MessageType.Error);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("RotationAngle"));

            EditorGUILayout.PropertyField(serializedObject.FindProperty("BoneLength"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("BoneEndPosition"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("PivotOffset"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("OverideTransform"));

            using (new ChangeCheckBlock(() => EditorUtility.SetDirty(Target)))
                RotatedSprite.GizmoDrawTypes = (RotatedSprite.GIZMO_TYPES)EditorGUILayout.EnumMaskField(RotatedSprite.GizmoDrawTypes);

            SerializedProperty childBonesProperty = serializedObject.FindProperty("ChildBones");
            ReorderableListGUI.Title(childBonesProperty.displayName);
            ReorderableListGUI.ListField(childBonesProperty);

            serializedObject.ApplyModifiedProperties();
        }

        private static readonly int DISPAY_IMAGE_SIZE = 100;
        public static void DrawRotatedSprite(RotatedSprite rotatedSprite)
        {
            if (rotatedSprite.ActiveSprite == null)
                return;

            Rect imagePosition = GUILayoutUtility.GetRect(DISPAY_IMAGE_SIZE, DISPAY_IMAGE_SIZE);

            EditorGUI.DrawTextureAlpha(new Rect(imagePosition.x, imagePosition.y, imagePosition.width, imagePosition.height),
                rotatedSprite.spriteRenderer.sprite.texture, ScaleMode.ScaleToFit);

            EditorGUI.LabelField(new Rect(imagePosition.x, imagePosition.y, 100, 50), 
                string.Format("{0}x{1}\n{2}\n{3}", 
                rotatedSprite.ActiveSprite.texture.width,
                rotatedSprite.ActiveSprite.texture.height, 
                rotatedSprite.GetRotationAngle(),
                rotatedSprite.rotationData.Count));
        }
    } 
}
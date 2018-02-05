using UnityEngine;
using System.Collections;
using Level6Rogue.SpriteRotating.Internal;

namespace Level6Rogue.SpriteRotating
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(SpriteRenderer))]
    public class RotatedSprite : UpdateReciever
    {
        private Transform _thisTransform;
        public Transform ThisTransform
        {
            get
            {
                if (_thisTransform == null)
                    _thisTransform = GetComponent<Transform>();
                return _thisTransform;
            }
        }

        private SpriteRenderer _spriteRenderer;
        public SpriteRenderer spriteRenderer
        {
            get
            {
                if (_spriteRenderer == null)
                    _spriteRenderer = GetComponent<SpriteRenderer>();
                return _spriteRenderer;
            }
        }

        [Space]
        public Sprite ActiveSprite;
        public RotationData rotationData;
        public float RotationAngle = 0;
        public int GetRotationAngle()
        {
            return (int)RotationAngle;
        }

        public int BoneLength;
        public Vector2 BoneEndPosition;

        public Transform OverideTransform;

        public Point PivotOffset = Point.zero;

        public BoneChildData[] ChildBones;

        public virtual void OnEnable()
        {
            rotationData = new RotationData();
            if (ActiveSprite == null)
                ActiveSprite = spriteRenderer.sprite;
        }

        public override void DoUpdate(Vector2 position)
        {
            if (OverideTransform != null)
                position = PositionClamping.GetPositionClamped(OverideTransform.position);

            UpdateRotation();
            ThisTransform.position = PositionClamping.GetPositionClamped(position + GetPivotOffsetPosition());

            float boneLength = -(BoneLength / PositionClamping.PixelScale);
            BoneEndPosition = position + (Vector2)(Quaternion.Euler(0, 0, GetRotationAngle()) * new Vector2(0, boneLength));
            UpdateChildBones(BoneEndPosition);
        }
        public Vector2 GetPivotOffsetPosition()
        {
            Vector2 pivotOffset = PivotOffset.GetPixelPosition();
            return Quaternion.Euler(0, 0, GetRotationAngle()) * pivotOffset;
        }

        public Vector2 GetPositionOffset(Vector2 offset, bool isRight)
        {
            Vector2 position = Quaternion.Euler(0, 0, GetRotationAngle()) * (offset + PivotOffset.GetPixelPosition());
            if (!isRight)
                position.x = -position.x;
            return (Vector2)ThisTransform.position + position;
        }

        public void UpdateChildBones(Vector2 position)
        {
            if (ChildBones != null)
                for (int i = 0; i < ChildBones.Length; i++)
                {
                    ChildBones[i].UpdateRotation(GetRotationAngle());
                    ChildBones[i].Update(position + ChildBones[i].GetOffset(GetRotationAngle()), BoneLength);
                }
        }

        protected void UpdateRotation()
        {
            if (spriteRenderer && ActiveSprite)
                spriteRenderer.sprite = rotationData.RotateSprite(ActiveSprite, GetRotationAngle());   
        }

        #region Gizmos
        #if UNITY_EDITOR

        [System.Flags]
        public enum GIZMO_TYPES
        {
            DrawOutlines = 0x0001,
            DrawPivotPositions = 0x0002,
            DrawBoneEnds = 0x0004,
            DrawBoneLines = 0x0008,
            DrawIKLines = 0x0010
        }
        public static GIZMO_TYPES GizmoDrawTypes;

        private static readonly float GIZMOS_CENTER_CROSS_SIZE = 1f;
        private void OnDrawGizmos()
        {
            Vector3 pivotPosition = ThisTransform.position - (Vector3)GetPivotOffsetPosition();

            if ((GizmoDrawTypes & GIZMO_TYPES.DrawPivotPositions) != 0)
                DrawGizmoCross(pivotPosition, Color.red);

            if ((GizmoDrawTypes & GIZMO_TYPES.DrawBoneEnds) != 0)
                DrawGizmoCross(BoneEndPosition, Color.blue);

            if ((GizmoDrawTypes & GIZMO_TYPES.DrawOutlines) != 0)
                DrawSpriteBorders();

            if ((GizmoDrawTypes & GIZMO_TYPES.DrawBoneLines) != 0)
            {
                Gizmos.color = Color.grey;
                Gizmos.DrawLine((Vector2)ThisTransform.position - GetPivotOffsetPosition(), BoneEndPosition);
            }
        }

        private void DrawSpriteBorders()
        {
            Gizmos.color = new Color(0,0,0,0.1f);
            Vector2 size = SpriteRotation_Helpers.GetSpriteSize(ActiveSprite).GetPixelPosition();

            Gizmos.DrawLine(ThisTransform.position + new Vector3(-size.x / 2, -size.y / 2, 0), 
                            ThisTransform.position + new Vector3(-size.x / 2, size.y / 2, 0));
            Gizmos.DrawLine(ThisTransform.position + new Vector3(size.x / 2, -size.y / 2, 0),
                            ThisTransform.position + new Vector3(size.x / 2, size.y / 2, 0));
            Gizmos.DrawLine(ThisTransform.position + new Vector3(-size.x / 2, -size.y / 2, 0),
                            ThisTransform.position + new Vector3(size.x / 2, -size.y / 2, 0));
            Gizmos.DrawLine(ThisTransform.position + new Vector3(-size.x / 2, size.y / 2, 0),
                            ThisTransform.position + new Vector3(size.x / 2, size.y / 2, 0));
        }
        private void DrawGizmoCross(Vector3 position, Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawLine(position - new Vector3(GIZMOS_CENTER_CROSS_SIZE, 0, 0),
                            position + new Vector3(GIZMOS_CENTER_CROSS_SIZE, 0, 0));
            Gizmos.DrawLine(position - new Vector3(0, GIZMOS_CENTER_CROSS_SIZE, 0),
                            position + new Vector3(0, GIZMOS_CENTER_CROSS_SIZE, 0));
        }

        #endif
        #endregion Gizmos
    }
}

using UnityEngine;
using System.Collections;

namespace Level6Rogue.SpriteRotating
{
    public class IKControler : UpdateReciever
    {
        public RotatedSprite BoneOne, BoneTwo;
        public Transform Target;
        public bool Direction;

        public override void DoUpdate(Vector2 position)
        {
            UpdateIK(position);
        }

        private void UpdateIK(Vector3 position)
        {
            if (!Check())
                return;

            BoneOne.DoUpdate(position);

            for (int i = 0; i < 2; i++)
            {
                IKValues values = GetIKValues();
                BoneOne.RotationAngle = values.AngleA;
                BoneTwo.RotationAngle = values.AngleB;

                BoneOne.DoUpdate(position);
            }
        }

        IKValues GetIKValues()
        {
            float lengthA = 0, lengthB = 0, lengthC = 0;
            Vector3 targetPosition =  new Vector3(
                Mathf.Round(Target.position.x * PositionClamping.PixelScale) / PositionClamping.PixelScale, 
                Mathf.Round(Target.position.y * PositionClamping.PixelScale) / PositionClamping.PixelScale, 0);

            Vector3 boneOnePosition = PositionClamping.GetPositionClamped(BoneOne.ThisTransform.position - (Vector3)BoneOne.GetPivotOffsetPosition());
            Vector3 boneTwoPosition = BoneTwo.ThisTransform.position;

            lengthA = BoneOne.BoneLength / PositionClamping.PixelScale;
            lengthB = BoneTwo.BoneLength / PositionClamping.PixelScale;
            lengthC = Mathf.Clamp(
                Vector3.Distance(boneOnePosition, targetPosition) - Mathf.Abs(lengthA - lengthB), 
                5 / PositionClamping.PixelScale, 
                float.MaxValue);

            float angleOne = 0.1f, angleTwo = 0.1f;
            if (lengthC < lengthA + lengthB)
            {
                double angleB = TriangleHelpers.GetAngleB(lengthA, lengthB, lengthC);
                angleOne = (float)angleB;

                Vector3 secOneDiff = (boneOnePosition - targetPosition).normalized;
                double secOneRot = System.Math.Atan2(secOneDiff.y, secOneDiff.x) * Mathf.Rad2Deg;
                Vector3 secTwoDiff = (boneTwoPosition - targetPosition).normalized;
                double secTwoRot = System.Math.Atan2(secTwoDiff.y, secTwoDiff.x) * Mathf.Rad2Deg;

                angleB = !Direction ? -angleB : angleB;
                float angle = Mathf.Repeat((float)(angleB + (secOneRot - 90)), 360);
                if (float.IsNaN(angle))
                    angle = 0.1f;

                if (angle > 0.0001f)
                    angleOne = angle;
                angleTwo = (float)secTwoRot - 90;
            }
            else
            {
                Vector3 difference = (boneOnePosition - targetPosition).normalized;
                float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                angleOne = angle - 90;
                angleTwo = angleOne;
            }

            return new IKValues
            {
                AngleA = angleOne,
                AngleB = angleTwo
            };
        }

        public bool Check()
        {
            return BoneOne && BoneTwo && Target;
        }

        public struct IKValues
        {
            public float AngleA;
            public float AngleB;
        }

        #region Gizmos
        #if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            if ((RotatedSprite.GizmoDrawTypes & RotatedSprite.GIZMO_TYPES.DrawIKLines) != 0)
            {
                if (!Check())
                    return;

                Gizmos.color = Color.green;
                Vector3 boneOnePos = BoneOne.ThisTransform.position - (Vector3)BoneOne.GetPivotOffsetPosition();
                Gizmos.DrawLine(boneOnePos, BoneTwo.ThisTransform.position);
                Gizmos.DrawLine(BoneTwo.ThisTransform.position, Target.position);
                Gizmos.DrawLine(Target.position, boneOnePos);
            }
        }

        #endif
        #endregion Gizmos
    } 
}

using UnityEngine;
using System.Collections;

namespace Level6Rogue.SpriteRotating
{
    [System.Serializable]
    public struct BoneChildData
    {
        public UpdateReciever updateReciever;
        public Point offset;
        public RotationUpdateType RotationPassThrough;
        public bool ResetOffset;

        public void Update(Vector2 position, int parentLength)
        {
            if (updateReciever != null)
            {
                RotatedSprite sprite = updateReciever as RotatedSprite;
                if (sprite != null && ResetOffset)
                    position += (Vector2)(Quaternion.Euler(0, 0, sprite.GetRotationAngle()) * new Vector3(0, parentLength / PositionClamping.PixelScale, 0));

                updateReciever.DoUpdate(position);
            }

        }
        public void UpdateRotation(float rotationAngle)
        {
            if (RotationPassThrough == RotationUpdateType.Pass && updateReciever)
            {
                RotatedSprite sprite = updateReciever as RotatedSprite;
                if (sprite)
                    sprite.RotationAngle = rotationAngle;
            }
        }

        public Vector2 GetOffset(int rotationAngle)
        {
            return Quaternion.Euler(0, 0, rotationAngle) * offset.GetPixelPosition();
        }

        public enum RotationUpdateType
        {
            None,
            Pass
        }
    } 
}
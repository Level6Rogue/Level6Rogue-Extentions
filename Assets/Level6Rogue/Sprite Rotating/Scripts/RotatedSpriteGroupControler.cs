using UnityEngine;
using UnityEngine.Events;

namespace Level6Rogue.SpriteRotating
{
    [ExecuteInEditMode]
    public class RotatedSpriteGroupControler : MonoBehaviour
    {
        public bool IsRight = true;
        public int PixelSize = 1;
        public BoneChildData[] boneData;

        protected virtual void Update()
        {
            PositionClamping.PixelScale = PixelSize;
            transform.localScale = new Vector3(1, 1, 1);
        }

        protected virtual void LateUpdate()
        {
            DoSpriteRotationUpdates();

            if (boneData != null)
                for (int i = 0; i < boneData.Length; i++)
                    boneData[i].Update((Vector2)transform.position + boneData[i].offset.GetPixelPosition(), 0);
            transform.localScale = new Vector3(IsRight ? 1 : -1, 1, 1);
        }
       
        protected virtual void DoSpriteRotationUpdates()
        {

        }
    } 
}

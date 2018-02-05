using UnityEngine;
using System.Collections;

namespace Level6Rogue.SpriteRotating
{
    public static class PositionClamping
    {
        public static float PixelScale = 1;

        public static Vector2 GetPositionClamped(Vector3 position)
        {
            position = new Vector2(
                DoRoundX(position.x * PixelScale) / PixelScale,
                DoRound(position.y * PixelScale) / PixelScale);

            return position;
        }

        private static float DoRoundX(float value)
        {
            return Mathf.RoundToInt(value);
        }
        private static float DoRound(float value)
        {
            return Mathf.RoundToInt(value);
        }
    } 
}

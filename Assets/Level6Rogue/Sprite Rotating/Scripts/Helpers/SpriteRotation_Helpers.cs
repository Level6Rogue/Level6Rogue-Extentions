using UnityEngine;
using System.Collections;

namespace Level6Rogue.SpriteRotating
{
    public static class SpriteRotation_Helpers
    {
        public static Point GetSpriteSize(Sprite sprite)
        {
            if (sprite == null)
                return Point.zero;
            return new Point(sprite.texture.width, sprite.texture.height);
        }

        public static Vector2 GetPixelPosition(this Point point)
        {
            return (Vector2)point / PositionClamping.PixelScale;
        }
    }
}
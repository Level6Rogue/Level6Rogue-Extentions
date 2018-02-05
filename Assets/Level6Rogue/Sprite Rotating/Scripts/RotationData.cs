using UnityEngine;
using System.Collections.Generic;

namespace Level6Rogue.SpriteRotating.Internal
{
    [System.Serializable]
    public class RotationData
    {
        private Dictionary<int, Sprite> _spriteDictionary;
        public void Clear()
        {
            _spriteDictionary.Clear();
            _spriteDictionary = new Dictionary<int, Sprite>();
        }
        public int Count
        {
            get
            {
                return _spriteDictionary.Count;
            }
        }

        public RotationData()
        {
            _spriteDictionary = new Dictionary<int, Sprite>();
        }

        public Sprite RotateSprite(Sprite sprite, float rotateAngle)
        {
            return RotateSprite(sprite, (int)rotateAngle);
        }
        public Sprite RotateSprite(Sprite sprite, int rotateAngle)
        {
            if (sprite == null)
                return null;

            rotateAngle = (int)Mathf.Repeat(rotateAngle, 360);
            int SpriteKey = GetSpriteKey(sprite, rotateAngle);

            if (!_spriteDictionary.ContainsKey(SpriteKey))
                _spriteDictionary.Add(SpriteKey, GetRotatedSprite(sprite, rotateAngle));
            return _spriteDictionary[SpriteKey];
        }
        Sprite GetRotatedSprite(Sprite sprite, int rotateAngle)
        {
            Texture2D texture = sprite.texture;
            Color[] colors = texture.GetPixels();
            Color[] rotatedColors = new Color[colors.Length];

            float gr = Mathf.Deg2Rad * rotateAngle;
            float sin = Mathf.Sin(gr);
            float cos = Mathf.Cos(gr);
            Vector2 center = sprite.pivot;
            int centerX = (int)center.x;
            int centerY = (int)center.y;

            int width = texture.width;
            int height = texture.height;

            for (int y = 0; y < texture.height; y++)
            {
                for (int x = 0; x < texture.width; x++)
                {
                    int xPos = (int)(cos * (x - centerX) + sin * (y - centerY) + centerX);
                    int yPos = (int)(-sin * (x - centerX) + cos * (y - centerY) + centerY);

                    int oldPos = y * width + x;
                    int newPos = yPos * width + xPos;
                    rotatedColors[oldPos] = Color.clear;

                    rotatedColors[oldPos] = new Color32(0, 0, 0, 0);
                    if ((xPos > -1) && (xPos < width) && (yPos > -1) && (yPos < height))
                    {
                        rotatedColors[oldPos] = colors[newPos];
                    }
                }
            }

            Texture2D newTexture = new Texture2D(width, height);
            newTexture.SetPixels(rotatedColors);
            newTexture.filterMode = FilterMode.Point;
            newTexture.wrapMode = TextureWrapMode.Clamp;
            newTexture.Apply();
            Sprite rotatedSprite = Sprite.Create(newTexture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), sprite.pixelsPerUnit);
            rotatedSprite.name = string.Format("{0}-Rot:{1}", sprite.name, rotateAngle.ToString());

            return rotatedSprite;
        }
        int GetSpriteKey(Sprite sprite, int rotateAngle)
        {
            int SpriteKey = 64;
            unchecked
            {
                SpriteKey = SpriteKey + rotateAngle * 64;
                SpriteKey = SpriteKey + sprite.name.GetHashCode() * 64;
            }
            return SpriteKey; 
        }
    } 
}

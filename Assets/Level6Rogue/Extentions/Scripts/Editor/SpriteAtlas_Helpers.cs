using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Level6Rogue.Extentions;

namespace Level6Rogue.Extentions
{
    public static class SpriteAtlas_Helpers
    {
        public static void ImportTextureAsSpriteSheet(Texture2D texture, SpriteMetaData[] atlasData)
        {
            string path = AssetDatabase.GetAssetPath(texture);

            TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
            importer.textureType = TextureImporterType.Sprite;
            importer.wrapMode = TextureWrapMode.Clamp;
            importer.mipmapEnabled = false;
            importer.maxTextureSize = 2048;
            importer.filterMode = FilterMode.Point;
            importer.isReadable = true;
            importer.textureCompression = TextureImporterCompression.Uncompressed;

            SpriteMetaData[] data = new SpriteMetaData[atlasData.Length];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = new SpriteMetaData
                {
                    alignment = atlasData[i].alignment,
                    border = new Vector4(),
                    name = atlasData[i].name,
                    pivot = GetPivotValue((SpriteAlignment)atlasData[i].alignment, atlasData[i].pivot),
                    rect = GetRectScaled(texture, atlasData[i].rect)
                };
            }

            importer.spriteImportMode = SpriteImportMode.Multiple;
            importer.spritesheet = data;

            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
        }

        public static Rect GetRectScaled(Texture2D texture, Rect rect)
        {
            return new Rect(rect.x * texture.width, rect.y * texture.height, rect.width * texture.width, rect.height * texture.height);
        }

        public static Vector2 GetPivotValue(SpriteAlignment alignment, Vector2 customOffset)
        {
            switch (alignment)
            {
                case SpriteAlignment.Center:
                    return new Vector2(0.5f, 0.5f);
                case SpriteAlignment.TopLeft:
                    return new Vector2(0.0f, 1f);
                case SpriteAlignment.TopCenter:
                    return new Vector2(0.5f, 1f);
                case SpriteAlignment.TopRight:
                    return new Vector2(1f, 1f);
                case SpriteAlignment.LeftCenter:
                    return new Vector2(0.0f, 0.5f);
                case SpriteAlignment.RightCenter:
                    return new Vector2(1f, 0.5f);
                case SpriteAlignment.BottomLeft:
                    return new Vector2(0.0f, 0.0f);
                case SpriteAlignment.BottomCenter:
                    return new Vector2(0.5f, 0.0f);
                case SpriteAlignment.BottomRight:
                    return new Vector2(1f, 0.0f);
                case SpriteAlignment.Custom:
                    return customOffset;
                default:
                    return Vector2.zero;
            }
        }
        public static SpriteAlignment GetSpriteAlignment(Vector2 offset)
        {
            if (offset.Equals(new Vector2(0.5f, 0.5f)))
                return SpriteAlignment.Center;
            if (offset.Equals(new Vector2(0.0f, 1f)))
                return SpriteAlignment.TopLeft;
            if (offset.Equals(new Vector2(0.5f, 1f)))
                return SpriteAlignment.TopCenter;
            if (offset.Equals(new Vector2(0.5f, 1f)))
                return SpriteAlignment.TopRight;
            if (offset.Equals(new Vector2(0.0f, 0.5f)))
                return SpriteAlignment.LeftCenter;
            if (offset.Equals(new Vector2(1f, 0.5f)))
                return SpriteAlignment.RightCenter;
            if (offset.Equals(new Vector2(0.0f, 0.0f)))
                return SpriteAlignment.BottomLeft;
            if (offset.Equals(new Vector2(0.5f, 0f)))
                return SpriteAlignment.BottomCenter;
            if (offset.Equals(new Vector2(1f, 0f)))
                return SpriteAlignment.BottomRight;

            return SpriteAlignment.Custom;
        }

        public static SpriteAtlasGroupedData[] GetSpriteAtlasTexturesIsolated(Texture2D texture, TextureImporter importer)
        {
            SpriteAtlasGroupedData[] data = new SpriteAtlasGroupedData[importer.spritesheet.Length];
            for (int i = 0; i < importer.spritesheet.Length; i++)
            {
                data[i] = new SpriteAtlasGroupedData()
                {
                    texture = GetIsolatedTexture(texture, importer.spritesheet[i].rect),
                    data = new SpriteMetaData()
                    {
                        name = importer.spritesheet[i].name,
                        rect = importer.spritesheet[i].rect,
                        alignment = importer.spritesheet[i].alignment,
                        pivot = importer.spritesheet[i].pivot,
                        border = importer.spritesheet[i].border
                    }
                };
            }
            return data;
        }
        
        public static Texture2D GetIsolatedTexture(Texture2D atlasTexture, Rect rect)
        {
            int width = (int)rect.width;
            int height = (int)rect.height;
            Color[] colors = atlasTexture.GetPixels();
            Color[] newColors = Texture_Helpers.GetColoredArray(Color.clear, width, height);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    newColors[y * width + x] = GetColorClamped(colors, (int)rect.x + x, (int)rect.y + y, atlasTexture.width);
                }
            }
            return Texture_Helpers.GetTextureFromColors(newColors, width, height, atlasTexture.name);
        }
        static Color GetColorClamped(Color[] colors, int x, int y, int width)
        {
            int index = y * width + x;
            if (index >= 0 && index < colors.Length)
                return colors[index];
            return Color.clear;
        }
    }

    [System.Serializable]
    public struct SpriteAtlasGroupedData
    {
        public Texture2D texture;
        public SpriteMetaData data;
    }
}
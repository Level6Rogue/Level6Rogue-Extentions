using UnityEngine;
using UnityEditor;

namespace Level6Rogue.Extentions
{
    public static class AssetDatabaseHelpers
    {
        /// <summary>
        /// Imports a <see cref="Texture2D"/> from the Asset Database as a <see cref="Sprite"/>.
        /// </summary>
        /// <param name="texture"></param>
        public static void ImportTextureAsSprite(Texture2D texture)
        {
            string path = AssetDatabase.GetAssetPath(texture);

            TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
            if (importer)
            {
                importer.wrapMode = TextureWrapMode.Clamp;
                importer.mipmapEnabled = false;
                importer.maxTextureSize = 2048;
                importer.filterMode = FilterMode.Point;
                importer.isReadable = true;
                importer.textureCompression = TextureImporterCompression.Uncompressed;
                AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
            }
        }
    } 
}
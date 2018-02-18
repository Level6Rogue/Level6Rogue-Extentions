using UnityEngine;
using UnityEditor;

namespace Level6Rogue.Extentions
{
    public static class EditorTextureHelpers
    {
        public static void SetTextureReadable(Texture2D texture)
        {
            string path = AssetDatabase.GetAssetPath(texture);

            TextureImporter importer = (TextureImporter)TextureImporter.GetAtPath(path);
            if (importer)
            {
                importer.isReadable = true;
                AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
            }
        }
        public static void SetTextureFilterMode(Texture2D texture, FilterMode filterMode)
        {
            string path = AssetDatabase.GetAssetPath(texture);

            TextureImporter importer = (TextureImporter)TextureImporter.GetAtPath(path);
            if (importer)
            {
                importer.filterMode = filterMode;
                AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
            }
        }
        public static void SetTextureCompresion(Texture2D texture, TextureImporterCompression compresion)
        {
            string path = AssetDatabase.GetAssetPath(texture);

            TextureImporter importer = (TextureImporter)TextureImporter.GetAtPath(path);
            if (importer)
            {
                importer.textureCompression = compresion;
                AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
            }
        }


        public static bool TextureIsReadable(Texture2D texture)
        {
            bool isReadable = true;
            try
            {
                texture.GetPixels();
            }
            catch
            {
                isReadable = false;
            }
            return isReadable;
        }
    } 
}
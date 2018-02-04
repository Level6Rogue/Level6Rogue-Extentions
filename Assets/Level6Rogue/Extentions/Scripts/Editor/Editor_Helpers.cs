using UnityEngine;
using UnityEditor;
using System.IO;

namespace Level6Rogue.Extentions
{
    public static class Editor_Helpers
    {
        #region Textures

        /// <summary>
        /// Saves a <see cref="Texture2D"/> to file as a PNG.
        /// </summary>
        /// <param name="texture">The <see cref="Texture2D"/> to be saved.</param>
        /// <param name="name">The name of the texture in the file.</param>
        /// /// <returns>Retruns the texture saved to file as an asset.</returns>
        public static Object SaveTextureToFile(Texture2D texture, string name)
        {
            return SaveTextureToFile(texture, "Asset", name);
        }

        /// <summary>
        /// Saves a <see cref="Texture2D"/> to file as a PNG.
        /// </summary>
        /// <param name="texture">The <see cref="Texture2D"/> to be saved.</param>
        /// <param name="name">The name of the texture in the file.</param>
        /// <param name="filepath">The filepath that the save browser will open too.</param>
        /// /// <returns>Retruns the texture saved to file as an asset.</returns>
        public static Object SaveTextureToFile(Texture2D texture, string filepath, string name)
        {
            string path = EditorUtility.SaveFilePanel("Save", filepath, name, "png");
            if (!string.IsNullOrEmpty(path))
                return WriteTextureToFileAtPath(texture, path);
            return null;
        }

        /// <summary>
        /// Saves a <see cref="Texture2D"/> to file as a PNG.
        /// </summary>
        /// <param name="texture">The <see cref="Texture2D"/> to be saved.</param>
        /// <param name="name">The name of the texture in the file.</param>
        /// <param name="path">The file path to be saved at.</param>
        /// <returns>Retruns the texture saved to file as an asset.</returns>
        public static Object WriteTextureToFileAtPath(Texture2D texture, string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                byte[] bytes = texture.EncodeToPNG();
                FileStream stream = File.Open(path, FileMode.Create);
                using (BinaryWriter writer = new BinaryWriter(stream))
                    writer.Write(bytes);
                stream.Close();

                AssetDatabase.Refresh();
                if (path.StartsWith(Application.dataPath))
                    return AssetDatabase.LoadAssetAtPath(Path_Helpers.ApplicationPathToRelative(path), typeof(Texture2D));
            }
            return null;
        }

        #endregion Textures

        public static void DrawStyle(Rect position, string style)
        {
            GUIStyle guiStyle = GUI.skin.FindStyle(style);
            if (guiStyle != null)
            {
                if (Event.current.type == EventType.Repaint)
                    guiStyle.Draw(position, false, false, false, false);
            }
        }
    } 
}
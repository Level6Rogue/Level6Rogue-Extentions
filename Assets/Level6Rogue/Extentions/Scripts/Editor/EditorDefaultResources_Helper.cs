using UnityEngine;
using UnityEditor;
using System.IO;

/* Code by Level6Rogue (Xzavien Millward)
Copyright (c) 2017
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.*/

namespace Level6Rogue.Extentions
{
    /// <summary>
    /// A collection of methods used for creating Editor Scriptable Objects at the EditorDefaultResources filepath.
    /// </summary>
    public static class EditorDefaultResources_Helper
    {
        public static readonly string ASSETS_PATH = "Assets";
        public static readonly string EDITOR_DEFAULT_RESOURCES_PATH = "Editor Default Resources";
        public static readonly string EDITOR_DEFAULT_RESOURCES_FULL_PATH = Path.Combine("Assets", "Editor Default Resources");

        /// <summary>
        /// Checks and creates the Editor Default Resources Folder
        /// </summary>
        public static void CreateEditorDefaultResourcesFolder()
        {
            if (!AssetDatabase.IsValidFolder(EDITOR_DEFAULT_RESOURCES_FULL_PATH))
            {
                AssetDatabase.CreateFolder("Assets", EDITOR_DEFAULT_RESOURCES_PATH);
            }
        }

        /// <summary>
        /// Creates a subfolder inside of Editor Default Resources
        /// </summary>
        /// <param name="folderName">Name of the Sub-Folder</param>
        public static void CreateEditorDefaultResourcesSubFolder(string folderName)
        {
            CreateEditorDefaultResourcesFolder();
            if (!AssetDatabase.IsValidFolder(string.Format(GetEditorDefaultResourcesPath(folderName))))
            {
                AssetDatabase.CreateFolder(EDITOR_DEFAULT_RESOURCES_FULL_PATH, folderName);
            }
        }

        /// <summary>
        /// Returns the combined path to a Editor Default Resources sub folder
        /// </summary>
        /// <param name="folderName">Name of the Sub-Folderr</param>
        /// <returns></returns>
        public static string GetEditorDefaultResourcesPath(string folderName)
        {
            return Path.Combine(EDITOR_DEFAULT_RESOURCES_FULL_PATH, folderName);
        }

        /// <summary>
        /// Loads a asset of type T from an Editor Default Resources Sub-Folder
        /// </summary>
        /// <typeparam name="T">Asset Type Extending from ScriptableObject</typeparam>
        /// <param name="folderName">Editor Default Resources Sub Folder Name</param>
        /// <param name="fileName">Asset Name</param>
        /// <param name="extention">Asset Extention</param>
        /// <returns></returns>
        public static T LoadEditorDefaultResourcesAsset<T>(string folderName, string fileName, string extention) where T : ScriptableObject
        {
            T asset = EditorGUIUtility.Load(Path.Combine(GetEditorDefaultResourcesPath(folderName), EditorScriptableObject_Helpers.AppendExtention(fileName, extention))) as T;
            return asset;
        }

        /// <summary>
        /// Creates an asset of type T in an Editro Default Resources Sub-Folder
        /// </summary>
        /// <typeparam name="T">Asset Type Extending from ScriptableObject</typeparam>
        /// <param name="folderName">Editor Default Resources Sub-Folder Name</param>
        /// <param name="fileName">Asset Name</param>
        /// <param name="extention">Asset Extention</param>
        /// <returns></returns>
        public static T CreateEditorDefaultResourcesAsset<T>(string folderName, string fileName, string extention) where T : ScriptableObject
        {
            CreateEditorDefaultResourcesSubFolder(folderName);
            string name = EditorScriptableObject_Helpers.AppendExtention(fileName, extention);

            T asset = EditorScriptableObject_Helpers.CreateAsset<T>(Path.Combine(GetEditorDefaultResourcesPath(folderName), name));
            Debug.Log(fileName + " Asset Created");

            return asset;
        }
    }
}
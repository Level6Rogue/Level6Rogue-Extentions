using UnityEngine;
using UnityEditor;

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
    /// A collection of scripts for help with <see cref="ScriptableObject"/>
    /// </summary>
    public static class EditorScriptableObject_Helpers
    {
        /// <summary>
        /// Creates an Asset of type T at the path
        /// </summary>
        /// <typeparam name="T">Asset Type</typeparam>
        /// <param name="fullPath">Full Asset Path relative to the project</param>
        /// <returns>Created Asset</returns>
        public static T CreateAsset<T>(string fullPath) where T : ScriptableObject
        {
            T asset = ScriptableObject.CreateInstance<T>();
            return CreateAsset<T>(asset, fullPath); ;
        }

        /// <summary>
        /// Creates an asset at the filepath
        /// </summary>
        /// <typeparam name="T">Asset Type</typeparam>
        /// <param name="asset">Asset</param>
        /// <param name="fullPath">Full Asset Path relative to the project</param>
        /// <returns>Created Asset</returns>
        public static T CreateAsset<T>(T asset, string fullPath) where T : ScriptableObject
        {
            AssetDatabase.CreateAsset(asset, fullPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            return asset;
        }

        /// <summary>
        /// Focuses an asset in the Project Window
        /// </summary>
        /// <param name="asset">Asset to be focused</param>
        public static void FocusAsset(Object asset)
        {
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }

        /// <summary>
        /// Appends the Extention to a file name
        /// </summary>
        /// <param name="fileName">File Name</param>
        /// <param name="fileExtention">Extention</param>
        /// <returns>Appended String</returns>
        public static string AppendExtention(string fileName, string fileExtention)
        {
            return string.Format("{0}.{1}", fileName, fileExtention);
        }

        /// <summary>
        /// Attatches an asset to another asset.
        /// </summary>
        /// <typeparam name="T">Type extending from <see cref="ScriptableObject"/>.</typeparam>
        /// <param name="assetToAdd">Asset to be attatched.</param>
        /// <param name="asset">Parent asset.</param>
        public static void AddObjectToAsset<T>(T assetToAdd, ScriptableObject asset) where T : ScriptableObject
        {
            AssetDatabase.AddObjectToAsset(assetToAdd, asset);
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(asset));
            AssetDatabase.Refresh();
            EditorUtility.SetDirty(asset);
        }
    }
}
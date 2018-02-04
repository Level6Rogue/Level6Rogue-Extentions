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
    public class EditorUtilityWindow : EditorWindow
    {
        public System.Action GUICallback;

        public static void Open(out EditorUtilityWindow window, string title, Rect position, Vector2 minSize, Vector2 maxSize, System.Action guiCallback)
        {
            window = Open(title, position, minSize, maxSize, guiCallback);
        }

        public static EditorUtilityWindow Open(string title, Rect position, Vector2 minSize, Vector2 maxSize, System.Action guiCallback)
        {
            EditorUtilityWindow window = (EditorUtilityWindow)GetWindow(typeof(EditorUtilityWindow), true);
            window.titleContent = new GUIContent(title);
            window.minSize = minSize;
            window.maxSize = maxSize;
            window.GUICallback = guiCallback;
            window.position = position;
            window.Show();

            return window;
        }

        void OnGUI()
        {
            if (GUICallback != null)
                GUICallback.Invoke();
        }

        void OnLostFocus()
        {
            Close();
        }
    } 
}
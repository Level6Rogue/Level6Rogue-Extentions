using UnityEngine;
using UnityEditor;

/* Code by Level6Rogue (Xzavien Millward)
Copyright (c) 2018
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.*/

namespace Level6Rogue.Extentions
{
    public class HorizontalBlock : System.IDisposable
    {
        public HorizontalBlock(params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginHorizontal(options);
        }

        public HorizontalBlock(GUIStyle style, params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginHorizontal(style, options);
        }

        public void Dispose()
        {
            EditorGUILayout.EndHorizontal();
        }
    }

    public class VerticalBlock : System.IDisposable
    {
        public VerticalBlock(params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginVertical(options);
        }

        public VerticalBlock(GUIStyle style, params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginVertical(style, options);
        }

        public void Dispose()
        {
            EditorGUILayout.EndVertical();
        }
    }

    public class ColorBlock : System.IDisposable
    {
        public Color cachedColor;
        public ColorBlock(Color color)
        {
            cachedColor = GUI.color;
            GUI.color = color;
        }
        public ColorBlock(Color color, bool active)
        {
            cachedColor = GUI.color;
            GUI.color = active ? color : cachedColor;
        }
        public ColorBlock(Color color1, Color color2, bool active)
        {
            cachedColor = GUI.color;
            GUI.color = active ? color1 : color2;
        }

        public void Dispose()
        {
            GUI.color = cachedColor;
        }
    }

    public class LabelWidthBlock : System.IDisposable
    {
        float cachedLabelWidth;
        public LabelWidthBlock(float width)
        {
            cachedLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = width;
        }

        public void Dispose()
        {
            EditorGUIUtility.labelWidth = cachedLabelWidth;
        }
    }

    public class IndentBlock : System.IDisposable
    {
        int cachedIndent;
        public IndentBlock(int indent = 1)
        {
            cachedIndent = EditorGUI.indentLevel;
            EditorGUI.indentLevel += indent;
        }

        public void Dispose()
        {
            EditorGUI.indentLevel = cachedIndent;
        }
    }

    public class EnableBlock : System.IDisposable
    {
        bool cachedEnabled;
        public EnableBlock(bool enabled)
        {
            cachedEnabled = GUI.enabled;
            GUI.enabled = enabled;
        }

        public void Dispose()
        {
            GUI.enabled = cachedEnabled;
        }
    }

    public class ChangeCheckBlock : System.IDisposable
    {
        public System.Action Callback;
        public ChangeCheckBlock(System.Action callback)
        {
            Callback = callback;
            EditorGUI.BeginChangeCheck();
        }

        public void Dispose()
        {
            if (EditorGUI.EndChangeCheck() && Callback != null)
                Callback.Invoke();
        }
    }
}
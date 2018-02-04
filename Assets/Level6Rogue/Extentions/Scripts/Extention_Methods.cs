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
    public static class ExtensionMethods
    {
        #region Float

        public static float Remap(this float value, float fromLow, float fromHigh, float toLow, float toHigh)
        {
            return (value - fromLow) / (fromHigh - fromLow) * (toHigh - toLow) + toLow;
        }

        #endregion Float

        #region Rect

        public static Vector2 PositionInside(this Rect rect, Vector2 position)
        {
            return new Vector2(position.x.Remap(rect.x, rect.x + rect.width, 0F, 1F),
                               position.y.Remap(rect.y, rect.y + rect.height, 0F, 1F));
        }

        public static Rect Pinch(this Rect rect, float amount)
        {
            return rect.PinchWidth(amount).PinchHeight(amount);
        }
        public static Rect PinchWidth(this Rect rect, float amount)
        {
            rect.x += amount;
            rect.width -= amount * 2;
            return rect;
        }
        public static Rect PinchHeight(this Rect rect, float amount)
        {
            rect.y += amount;
            rect.height -= amount * 2;
            return rect;
        }

        #endregion Rect

        #region Generic Menu

        public static void OpenMenu(this GenericMenu menu, Rect position, bool showAsContext)
        {
            if (showAsContext)
                menu.ShowAsContext();
            else
                menu.DropDown(position);
        }
        public static void AddNewItem(this GenericMenu menu, GUIContent content, bool itemActive, bool itemSelected, GenericMenu.MenuFunction action)
        {
            if (itemActive)
                menu.AddItem(content, itemSelected, action);
            else
                menu.AddDisabledItem(content);
        }

        #endregion Generic Menu
    }
}
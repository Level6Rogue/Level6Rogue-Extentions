using UnityEngine;

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
    /// A collections if methods for creating textures.
    /// </summary>
    public static class TextureHelpers
    {
        public const string DEFAULT_NEW_TEXTURE_NAME = "(Generated) New Texture";

        #region Base-64
        
        /// <summary>
        /// Converts a base64 <see cref="string[]"/> array into a <see cref="Texture2D[]"/> array.
        /// </summary>
        /// <param name="stringArray">The <see cref="string[]"/> used to create the <see cref="Texture2D"/> array.</param>
        /// <returns>A <see cref="Texture2D[]"/> array from the given base64 <see cref="string[]"/> array.</returns>
        public static Texture2D[] DecodeBase64StringArray(string[] stringArray)
        {
            Texture2D[] textures = new Texture2D[stringArray.Length];
            for (int i = 0; i < textures.Length; i++)
                textures[i] = DecodeBase64String(stringArray[i]);
            return textures;
        }

        /// <summary>
        /// Converts a base64 <see cref="string"/> into a <see cref="Texture2D"/>.
        /// </summary>
        /// <param name="base64">The <see cref="string"/> used to create the texture.</param>
        /// <param name="name">The name of the returning <see cref="Texture2D"/>.</param>
        /// <returns>A <see cref="Texture2D"/> from the given base64 <see cref="string"/>.</returns>
        public static Texture2D DecodeBase64String(string base64, string name = DEFAULT_NEW_TEXTURE_NAME)
        {
            byte[] bytes = System.Convert.FromBase64String(base64);
            Texture2D texture = new Texture2D(1, 1, TextureFormat.ARGB32, false, true);
            texture.hideFlags = HideFlags.HideAndDontSave;
            texture.filterMode = FilterMode.Point;
            texture.LoadImage(bytes);
            texture.name = name;
            texture.Apply();

            return texture;
        }

        #endregion Base-64

        #region Texture Creation

        /// <summary>
        /// Returns a 1x1 <see cref="Texture2D"/> of the given <see cref="Color"/>.
        /// </summary>
        /// <param name="color">The color the returning <see cref="Texture2D"/>.</param>
        /// <param name="name">The name of the returning <see cref="Texture2D"/>.</param>
        /// <returns>A 1x1 <see cref="Texture2D"/> of the given color.</returns>
        public static Texture2D GetPixelTexture(Color color, string name = DEFAULT_NEW_TEXTURE_NAME)
        {
            return GetColoredTexture(color, 1, 1, name);
        }

        /// <summary>
        /// Returns a <see cref="Texture2D"/> of the given size and <see cref="Color"/>
        /// </summary>
        /// <param name="color">The color the returning <see cref="Texture2D"/>.</param>
        /// <param name="width">The width the returning <see cref="Texture2D"/>.</param>
        /// <param name="height">The height the returning <see cref="Texture2D"/>.</param>
        /// <param name="name">The name the returning <see cref="Texture2D"/>.</param>
        /// <returns>A <see cref="Texture2D"/> of the given size and <see cref="Color"/></returns>
        public static Texture2D GetColoredTexture(Color color, int width, int height, string name = DEFAULT_NEW_TEXTURE_NAME)
        {
            return GetTextureFromColors(GetColoredArray(color, width, height), width, height, name);
        }

        /// <summary>
        /// Returns a <see cref="Texture2D"/> from the given <see cref="Color[]"/>. 
        /// </summary>
        /// <param name="colors">The <see cref="Color[]"/> the returning <see cref="Texture2D"/>.</param>
        /// <param name="width">The width of the returning <see cref="Texture2D"/>.</param>
        /// <param name="height">The height of the returning <see cref="Texture2D"/>.</param>
        /// <param name="name">The name of the returning <see cref="Texture2D"/>.</param>
        /// <param name="filterMode">The filter mode of the returning <see cref="Texture2D"/>.</param>
        /// <returns>A <see cref="Texture2D"/> of the given <see cref="Color[]"/>.</returns>
        public static Texture2D GetTextureFromColors(Color[] colors, int width, int height, string name = DEFAULT_NEW_TEXTURE_NAME, FilterMode filterMode = FilterMode.Point)
        {
            Texture2D texture = new Texture2D(width, height);
            texture.name = name;
            texture.SetPixels(colors);
            texture.filterMode = filterMode;
            texture.Apply();
            return texture;
        }

        /// <summary>
        /// Returns a Checkered <see cref="Texture2D"/> of the given size and colors.
        /// </summary>
        /// <param name="color1">Color one of the returning <see cref="Texture2D"/>.</param>
        /// <param name="color2">Color two of the returning <see cref="Texture2D"/>.</param>
        /// <param name="width">The width of the returning <see cref="Texture2D"/>.</param>
        /// <param name="height">The height of the returning <see cref="Texture2D"/>.</param>
        /// <returns>A Checkered <see cref="Texture2D"/> of the given size and colors.</returns>
        public static Texture2D GenerateCheckerTexture(Color color1, Color color2, int width, int height)
        {
            Color[] colors = new Color[width * height];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    colors[y * width + x] = (((x + y) & 1) == 1) ? color1 : color2;

            return GetTextureFromColors(colors, width, height, "(Generated) Checker Pattern");
        }

        #endregion Texture Creation

        /// <summary>
        /// Returns a <see cref="Color[]"/> of the given color and size.
        /// </summary>
        /// <param name="color">The color the returning <see cref="Color[]"/>.</param>
        /// <param name="width">The width the returning <see cref="Color[]"/>.</param>
        /// <param name="height">The height the returning <see cref="Color[]"/>.</param>
        /// <returns>A <see cref="Color[]"/> of the given color and size.</returns>
        public static Color[] GetColoredArray(Color color, int width, int height)
        {
            Color[] colors = new Color[width * height];
            for (int i = 0; i < colors.Length; i++)
                colors[i] = color;
            return colors;
        }

        public static Color[] CopyColorArray(Color[] colors)
        {
            Color[] newColors = new Color[colors.Length];
            for (int i = 0; i < colors.Length; i++)
                newColors[i] = colors[i];
            return newColors;
        }
    }
}
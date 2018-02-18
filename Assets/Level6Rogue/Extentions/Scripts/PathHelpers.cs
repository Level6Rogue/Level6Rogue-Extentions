using UnityEngine;
using System.IO;

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
    /// <summary>
    /// A collection of methods for help with file paths.
    /// </summary>
    public static class PathHelpers
    {
        /// <summary>
        /// Converts a <see cref="string"/> application file path to a <see cref="string"/> relative file path.
        /// </summary>
        /// <param name="path">The input application file path.</param>
        /// <returns>An relative file path.</returns>
        public static string ApplicationPathToRelative(string path)
        {
            if (path.StartsWith(Application.dataPath))
                return string.Format("Assets{0}", path.Substring(Application.dataPath.Length));
            else
                throw new System.ArgumentException("Incorrect Path");
        }

        /// <summary>
        /// Converts a <see cref="string"/> relative file path to a <see cref="string"/> application file path.
        /// </summary>
        /// <param name="path">The input relative file path.</param>
        /// <returns>An application file path.</returns>
        public static string RelativePathToApplication(string path)
        {
            if (path.StartsWith(Application.dataPath))
                throw new System.ArgumentException("Incorrect Path");
            else
                return Path.Combine(Application.dataPath, path.Substring(7));
        }
    }
}
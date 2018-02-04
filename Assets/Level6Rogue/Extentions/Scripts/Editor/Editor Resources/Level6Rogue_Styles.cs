using UnityEngine;
using UnityEditor;

namespace Level6Rogue.Extentions.Internal
{
    public static class Level6Rogue_Styles
    {
        public static readonly string Name = "Level6Rogue";
        public static readonly string NameUppercase = Name.ToUpper();

        public static GUIStyle TitleLabel { get; private set; }
        public static GUIStyle SubLabel { get; private set; }
        public static GUIStyle OtherContentLabel { get; private set; }
        public static GUIStyle ContentLabel { get; private set; }

        static Level6Rogue_Styles()
        {
            TitleLabel = new GUIStyle();
            TitleLabel.fontSize = 20;
            TitleLabel.richText = true;
            TitleLabel.fontStyle = FontStyle.Bold;

            SubLabel = new GUIStyle();
            SubLabel.fontSize = 14;
            SubLabel.richText = true;

            ContentLabel = new GUIStyle();
            ContentLabel.wordWrap = true;
            ContentLabel.richText = true;

            OtherContentLabel = new GUIStyle();
        }
    } 
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level6Rogue.Extentions.Internal
{
    public enum ResourcesTextureType
    {
        Logo,
        Logo_Alt
    }

    public static class Level6Rogue_Resources
    {
        private static string[] encodedTextures = new string[]
        {
            /*Logo*/"iVBORw0KGgoAAAANSUhEUgAAABgAAAALCAYAAABlNU3NAAAA5UlEQVQ4EWNgoDFgRDffLNPmP0zs1PQjKPIwOVziMH0wGqSOCcYB0YaJlv9///jLAMZf/jIYxlrCLUNWRwqbBaYYZDiMjUyDLDm/+DiKT5DlkdnoPgPJwS1AVkguGxaEMP3wIEKXODfvGFjNuUUQGqaBHBrsA2YGYLhzQLR/OPyCQUlXCcwB0QK2EnBzQerwAVgQITsYHMkf3vxg+PX6B1gvsoEww7RDDLHGD0weH40SByBL2EQ5UFwNsxjZEMtMy/9/GZjBQjBXgzjILoepB1twffV5Rpgr0Q28uuY8USkIZiDdaQANXFOf2zPxnQAAAABJRU5ErkJggg==",
            /*Logo Alt*/ "iVBORw0KGgoAAAANSUhEUgAAABgAAAALCAYAAABlNU3NAAABQElEQVQ4Ec1RPUsDQRB9MXoazckZYqOIiPiBEFBTJSBWghBSWwuCjX/Czh8TbHLY+ydiaSFWNleJinF9s7d7t3tn7AQHXm7mzZuPnQB/bJVS/0OoKIw0ndwn5Xyp4Hdi2kub5sko8egs2IPS/gj+4F3DT2VKoKhBm6IDqGE8VFiGGtwOFDjQKQFsI49ksFHQ2UU8nTTjADQJDtDYh4rv4nzIJvmfbGUCT216Imn+yeiLWCNeCTG2q4XzqS+/b7nreVI3wdJbyubvVIhQYC58c36NxXqIYCbAxeVVRb/qxWadjk2ut4Aq5sjZ/+HB1fEUaBFbxDaxQ8hdV6G6J538LEvkXFs3cZF3Tpnuykaos1LKhfkwCPh9dDaJqJAXymEFs8QT8w3yY/pSWyXkJc9OHUNtx6dH2Yb9s17m2/y//H4DifRVlxFSOSQAAAAASUVORK5CYII=",
        };

        private static Texture2D[] textures;
        static Level6Rogue_Resources()
        {
            textures = Texture_Helpers.DecodeBase64StringArray(encodedTextures);
        }

        public static Texture2D GetTexture(ResourcesTextureType type)
        {
            return textures[(int)type];
        }
    } 
}

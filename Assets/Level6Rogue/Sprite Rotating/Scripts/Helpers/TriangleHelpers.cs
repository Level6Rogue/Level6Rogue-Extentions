using UnityEngine;
using System.Collections;

namespace Level6Rogue.SpriteRotating
{
    public static class TriangleHelpers
    {
        // (((b2) + (c2) - (a2)) / (2bc) )  * 180 / PI
        public static double GetAngleA(double lengthA, double lengthB, double lengthC)
        {
            if (CheckTriangleSides(lengthA, lengthB, lengthC))
                return System.Math.Acos(((lengthB * lengthB) + (lengthC * lengthC) - (lengthA * lengthA)) / (2 * lengthB * lengthC)) * 180.0 / Mathf.PI;
            return 0;
        }

        // (((a2) + (c2) - (b2)) / (2ac) )  * 180 / PI
        public static double GetAngleB(double lengthA, double lengthB, double lengthC)
        {
            if (CheckTriangleSides(lengthA, lengthB, lengthC))
                return System.Math.Acos(((lengthA * lengthA) + (lengthC * lengthC) - (lengthB * lengthB)) / (2 * lengthA * lengthC)) * 180.0 / Mathf.PI;
            return 0;
        }

        // (((a2) + (b2) - (b2)) / (2ac) )  * 180 / PI
        public static double GetAngleC(double lengthA, double lengthB, double lengthC)
        {
            if (CheckTriangleSides(lengthA, lengthB, lengthC))
                return System.Math.Acos(((lengthA * lengthA) + (lengthB * lengthB) - (lengthC * lengthC)) / (2 * lengthA * lengthB)) * 180.0 / Mathf.PI;
            return 0;
        }

        public static bool CheckTriangleSides(double lengthA, double lengthB, double lengthC)
        {
            return lengthC <= lengthA + lengthB;
        }
    }
}
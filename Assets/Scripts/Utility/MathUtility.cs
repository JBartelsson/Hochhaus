using UnityEngine;

namespace Utility
{
    public class MathUtility
    {
        public static float nfmod(float a,float b)
        {
            return a - b * Mathf.FloorToInt(a / b);
        }
    }
}
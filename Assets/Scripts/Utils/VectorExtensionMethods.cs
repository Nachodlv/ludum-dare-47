using UnityEngine;

namespace Utils
{
    public static class VectorExtensionMethods
    {
        public static Vector2 Rotate90CW(this Vector2 aDir)
        {
            return new Vector2(aDir.y, -aDir.x);
        }

        public static Vector2 Rotate90CCW(this Vector2 aDir)
        {
            return new Vector2(-aDir.y, aDir.x);
        }
    }
}

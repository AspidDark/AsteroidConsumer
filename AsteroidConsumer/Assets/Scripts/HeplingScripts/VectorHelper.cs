using UnityEngine;

namespace TimB
{
    public static class VectorHelper
    {
        public static Vector2 RotateExisting(this Vector2 v, float degrees)
        {
            float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
            float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

            float tx = v.x;
            float ty = v.y;
            v.x = (cos * tx) - (sin * ty);
            v.y = (sin * tx) + (cos * ty);
            return v;
        }

        public static Vector2 GetRotated(this Vector2 v, float degrees)
        {
            float radians = degrees * Mathf.Deg2Rad;
            float sin = Mathf.Sin(radians);
            float cos = Mathf.Cos(radians);

            float tx = v.x;
            float ty = v.y;

            return new Vector2(cos * tx - sin * ty, sin * tx + cos * ty);
        }
    }
}

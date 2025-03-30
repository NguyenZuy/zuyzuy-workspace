using UnityEngine;

namespace ZuyZuy.Workspace
{
    public static class ConvertUtils
    {
        public static Vector3 OnlyXY(this Vector3 vector)
        {
            return new Vector3(vector.x, vector.y, 0f);
        }

        public static Vector3 OnlyXZ(this Vector3 vector)
        {
            return new Vector3(vector.x, 0f, vector.z);
        }

        public static Vector3 OnlyYZ(this Vector3 vector)
        {
            return new Vector3(0f, vector.y, vector.z);
        }
    }
}

using UnityEngine;

namespace ZuyZuy.Workspace
{
    public static class ConvertUtils
    {
        public static Vector3 OnlyXY(this Vector3 value)
        {
            return new Vector3(value.x, value.y, 0f);
        }

        public static Vector3 OnlyXZ(this Vector3 value)
        {
            return new Vector3(value.x, 0f, value.z);
        }

        public static Vector3 OnlyYZ(this Vector3 value)
        {
            return new Vector3(0f, value.y, value.z);
        }
    }
}

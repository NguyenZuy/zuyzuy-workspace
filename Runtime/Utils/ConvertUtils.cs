using System.Numerics;

namespace ZuyZuy.Workspace
{
    public static class ConvertUtils
    {
        public static Vector3 OnlyXY(this Vector3 value)
        {
            return new Vector3(value.X, value.Y, 0f);
        }

        public static Vector3 OnlyXZ(this Vector3 value)
        {
            return new Vector3(value.X, 0f, value.Z);
        }

        public static Vector3 OnlyYZ(this Vector3 value)
        {
            return new Vector3(0f, value.Y, value.Z);
        }
    }
}

using UnityEngine;

namespace ZuyZuy.Workspace
{
    public static class CommonUtils
    {
        public static Vector3 AddX(this Vector3 vector, float valueX)
        {
            return new Vector3(vector.x + valueX, vector.y, vector.z);
        }
    }
}
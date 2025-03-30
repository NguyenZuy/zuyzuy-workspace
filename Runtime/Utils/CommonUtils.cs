using UnityEngine;

namespace ZuyZuy.Workspace
{
    public static class CommonUtils
    {
        #region Add Operations

        /// <summary>
        /// Adds a value to the X component of a Vector3.
        /// </summary>
        /// <param name="vector">The source Vector3 to modify</param>
        /// <param name="valueX">The value to add to the X component</param>
        /// <returns>A new Vector3 with the modified X component</returns>
        public static Vector3 AddX(this Vector3 vector, float valueX)
        {
            return new Vector3(vector.x + valueX, vector.y, vector.z);
        }

        /// <summary>
        /// Adds a value to the Y component of a Vector3.
        /// </summary>
        /// <param name="vector">The source Vector3 to modify</param>
        /// <param name="valueY">The value to add to the Y component</param>
        /// <returns>A new Vector3 with the modified Y component</returns>
        public static Vector3 AddY(this Vector3 vector, float valueY)
        {
            return new Vector3(vector.x, vector.y + valueY, vector.z);
        }

        /// <summary>
        /// Adds a value to the Z component of a Vector3.
        /// </summary>
        /// <param name="vector">The source Vector3 to modify</param>
        /// <param name="valueZ">The value to add to the Z component</param>
        /// <returns>A new Vector3 with the modified Z component</returns>
        public static Vector3 AddZ(this Vector3 vector, float valueZ)
        {
            return new Vector3(vector.x, vector.y, vector.z + valueZ);
        }

        #endregion

        #region Subtract Operations

        /// <summary>
        /// Subtracts a value from the X component of a Vector3.
        /// </summary>
        /// <param name="vector">The source Vector3 to modify</param>
        /// <param name="valueX">The value to subtract from the X component</param>
        /// <returns>A new Vector3 with the modified X component</returns>
        public static Vector3 SubtractX(this Vector3 vector, float valueX)
        {
            return new Vector3(vector.x - valueX, vector.y, vector.z);
        }

        /// <summary>
        /// Subtracts a value from the Y component of a Vector3.
        /// </summary>
        /// <param name="vector">The source Vector3 to modify</param>
        /// <param name="valueY">The value to subtract from the Y component</param>
        /// <returns>A new Vector3 with the modified Y component</returns>
        public static Vector3 SubtractY(this Vector3 vector, float valueY)
        {
            return new Vector3(vector.x, vector.y - valueY, vector.z);
        }

        /// <summary>
        /// Subtracts a value from the Z component of a Vector3.
        /// </summary>
        /// <param name="vector">The source Vector3 to modify</param>
        /// <param name="valueZ">The value to subtract from the Z component</param>
        /// <returns>A new Vector3 with the modified Z component</returns>
        public static Vector3 SubtractZ(this Vector3 vector, float valueZ)
        {
            return new Vector3(vector.x, vector.y, vector.z - valueZ);
        }

        #endregion

        #region Multiply Operations

        /// <summary>
        /// Multiplies the X component of a Vector3 by a value.
        /// </summary>
        /// <param name="vector">The source Vector3 to modify</param>
        /// <param name="valueX">The value to multiply the X component by</param>
        /// <returns>A new Vector3 with the modified X component</returns>
        public static Vector3 MultiplyX(this Vector3 vector, float valueX)
        {
            return new Vector3(vector.x * valueX, vector.y, vector.z);
        }

        /// <summary>
        /// Multiplies the Y component of a Vector3 by a value.
        /// </summary>
        /// <param name="vector">The source Vector3 to modify</param>
        /// <param name="valueY">The value to multiply the Y component by</param>
        /// <returns>A new Vector3 with the modified Y component</returns>
        public static Vector3 MultiplyY(this Vector3 vector, float valueY)
        {
            return new Vector3(vector.x, vector.y * valueY, vector.z);
        }

        /// <summary>
        /// Multiplies the Z component of a Vector3 by a value.
        /// </summary>
        /// <param name="vector">The source Vector3 to modify</param>
        /// <param name="valueZ">The value to multiply the Z component by</param>
        /// <returns>A new Vector3 with the modified Z component</returns>
        public static Vector3 MultiplyZ(this Vector3 vector, float valueZ)
        {
            return new Vector3(vector.x, vector.y, vector.z * valueZ);
        }

        #endregion

        #region Divide Operations

        /// <summary>
        /// Divides the X component of a Vector3 by a value.
        /// </summary>
        /// <param name="vector">The source Vector3 to modify</param>
        /// <param name="valueX">The value to divide the X component by</param>
        /// <returns>A new Vector3 with the modified X component</returns>
        public static Vector3 DivideX(this Vector3 vector, float valueX)
        {
            return new Vector3(vector.x / valueX, vector.y, vector.z);
        }

        /// <summary>
        /// Divides the Y component of a Vector3 by a value.
        /// </summary>
        /// <param name="vector">The source Vector3 to modify</param>
        /// <param name="valueY">The value to divide the Y component by</param>
        /// <returns>A new Vector3 with the modified Y component</returns>
        public static Vector3 DivideY(this Vector3 vector, float valueY)
        {
            return new Vector3(vector.x, vector.y / valueY, vector.z);
        }

        /// <summary>
        /// Divides the Z component of a Vector3 by a value.
        /// </summary>
        /// <param name="vector">The source Vector3 to modify</param>
        /// <param name="valueZ">The value to divide the Z component by</param>
        /// <returns>A new Vector3 with the modified Z component</returns>
        public static Vector3 DivideZ(this Vector3 vector, float valueZ)
        {
            return new Vector3(vector.x, vector.y, vector.z / valueZ);
        }

        #endregion
    }
}
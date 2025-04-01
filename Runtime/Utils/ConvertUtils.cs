using UnityEngine;

namespace ZuyZuy.Workspace
{
    /// <summary>
    /// Provides utility methods for converting and manipulating Vector3 values.
    /// </summary>
    public static class ConvertUtils
    {
        #region Component Filter Operations

        /// <summary>
        /// Creates a new Vector3 with only the X and Y components from the source vector (Z set to 0).
        /// </summary>
        /// <param name="vector">The source Vector3 to filter</param>
        /// <returns>A new Vector3 with only X and Y components</returns>
        public static Vector3 OnlyXY(this Vector3 vector)
        {
            return new Vector3(vector.x, vector.y, 0f);
        }

        /// <summary>
        /// Creates a new Vector3 with only the X and Z components from the source vector (Y set to 0).
        /// </summary>
        /// <param name="vector">The source Vector3 to filter</param>
        /// <returns>A new Vector3 with only X and Z components</returns>
        public static Vector3 OnlyXZ(this Vector3 vector)
        {
            return new Vector3(vector.x, 0f, vector.z);
        }

        /// <summary>
        /// Creates a new Vector3 with only the Y and Z components from the source vector (X set to 0).
        /// </summary>
        /// <param name="vector">The source Vector3 to filter</param>
        /// <returns>A new Vector3 with only Y and Z components</returns>
        public static Vector3 OnlyYZ(this Vector3 vector)
        {
            return new Vector3(0f, vector.y, vector.z);
        }

        #endregion

        #region Float to Vector3 Conversions

        /// <summary>
        /// Converts a float value to a Vector3 with the value in the X component (Y and Z set to 0).
        /// </summary>
        /// <param name="value">The value to set in the X component</param>
        /// <returns>A new Vector3 with the value in X component</returns>
        public static Vector3 ToXVector3(this float value)
        {
            return new Vector3(value, 0f, 0f);
        }

        /// <summary>
        /// Converts a float value to a Vector3 with the value in the Y component (X and Z set to 0).
        /// </summary>
        /// <param name="value">The value to set in the Y component</param>
        /// <returns>A new Vector3 with the value in Y component</returns>
        public static Vector3 ToYVector3(this float value)
        {
            return new Vector3(0f, value, 0f);
        }

        /// <summary>
        /// Converts a float value to a Vector3 with the value in the Z component (X and Y set to 0).
        /// </summary>
        /// <param name="value">The value to set in the Z component</param>
        /// <returns>A new Vector3 with the value in Z component</returns>
        public static Vector3 ToZVector3(this float value)
        {
            return new Vector3(0f, 0f, value);
        }

        #endregion

        #region Int to Vector3 Conversions

        /// <summary>
        /// Converts an integer value to a Vector3 with the value in the X component (Y and Z set to 0).
        /// </summary>
        /// <param name="value">The value to set in the X component</param>
        /// <returns>A new Vector3 with the value in X component</returns>
        public static Vector3 ToXVector3(this int value)
        {
            return new Vector3(value, 0f, 0f);
        }

        /// <summary>
        /// Converts an integer value to a Vector3 with the value in the Y component (X and Z set to 0).
        /// </summary>
        /// <param name="value">The value to set in the Y component</param>
        /// <returns>A new Vector3 with the value in Y component</returns>
        public static Vector3 ToYVector3(this int value)
        {
            return new Vector3(0f, value, 0f);
        }

        /// <summary>
        /// Converts an integer value to a Vector3 with the value in the Z component (X and Y set to 0).
        /// </summary>
        /// <param name="value">The value to set in the Z component</param>
        /// <returns>A new Vector3 with the value in Z component</returns>
        public static Vector3 ToZVector3(this int value)
        {
            return new Vector3(0f, 0f, value);
        }

        #endregion
    }
}

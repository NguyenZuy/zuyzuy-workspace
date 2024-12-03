using UnityEngine;

namespace com.zuyzuy.workspace
{
    /// <summary>
    /// Utility class for generating various types of data and performing random generations.
    /// </summary>
    public static class GenerateUtils
    {
        /// <summary>
        /// Generates a random color with optional alpha control.
        /// </summary>
        /// <param name="includeAlpha">Whether to include alpha channel randomization</param>
        /// <returns>A randomly generated Color</returns>
        public static Color RandomColor(bool includeAlpha = false)
        {
            return includeAlpha
                ? new Color(Random.value, Random.value, Random.value, Random.value)
                : new Color(Random.value, Random.value, Random.value);
        }

        /// <summary>
        /// Generates a random Vector3 within specified min and max bounds.
        /// </summary>
        /// <param name="min">Minimum bounds for x, y, and z</param>
        /// <param name="max">Maximum bounds for x, y, and z</param>
        /// <returns>A randomly generated Vector3</returns>
        public static Vector3 RandomVector3(float min, float max)
        {
            return new Vector3(
                Random.Range(min, max),
                Random.Range(min, max),
                Random.Range(min, max)
            );
        }

        /// <summary>
        /// Generates a random Vector3 within specified min and max vectors.
        /// </summary>
        /// <param name="minBounds">Minimum bounds vector</param>
        /// <param name="maxBounds">Maximum bounds vector</param>
        /// <returns>A randomly generated Vector3</returns>
        public static Vector3 RandomVector3(Vector3 minBounds, Vector3 maxBounds)
        {
            return new Vector3(
                Random.Range(minBounds.x, maxBounds.x),
                Random.Range(minBounds.y, maxBounds.y),
                Random.Range(minBounds.z, maxBounds.z)
            );
        }

        /// <summary>
        /// Generates a random string of specified length.
        /// </summary>
        /// <param name="length">Length of the string to generate</param>
        /// <param name="chars">Character set to use (defaults to alphanumeric)</param>
        /// <returns>A randomly generated string</returns>
        public static string RandomString(int length, string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789")
        {
            char[] stringChars = new char[length];
            for (int i = 0; i < length; i++)
            {
                stringChars[i] = chars[Random.Range(0, chars.Length)];
            }
            return new string(stringChars);
        }

        /// <summary>
        /// Generates a unique identifier based on current timestamp and random value.
        /// </summary>
        /// <returns>A unique string identifier</returns>
        public static string GenerateUniqueId()
        {
            long ticks = System.DateTime.UtcNow.Ticks;
            byte[] bytes = System.BitConverter.GetBytes(ticks);
            string ticksString = System.Convert.ToBase64String(bytes);

            // Remove any non-alphanumeric characters
            ticksString = ticksString.Replace("+", "").Replace("/", "").Replace("=", "");

            // Add some random characters to increase uniqueness
            return ticksString + RandomString(4);
        }
    }
}

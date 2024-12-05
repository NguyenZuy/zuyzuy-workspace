using System.Diagnostics;
using UnityEngine;

namespace Zuy.Workspace.Editor
{
    public static class ZuyLogger
    {
        /// <summary>
        /// Logs a message with a specified category and color.
        /// </summary>
        /// <param name="category">Category of the log (e.g., "UI", "Network").</param>
        /// <param name="message">The log message to display.</param>
        /// <param name="color">Optional. The color of the category in the log. Default is "white".</param>
        [Conditional("DEBUG")]
        public static void Log(string category, string message, string color = "white")
        {
            category = category.ToUpper();
            UnityEngine.Debug.Log($"[<color={color}>{category}] {message}");
        }

        /// <summary>
        /// Logs a warning message with a specified category and color.
        /// </summary>
        /// <param name="category">Category of the warning (e.g., "UI", "Network").</param>
        /// <param name="message">The warning message to display.</param>
        /// <param name="color">Optional. The color of the category in the warning. Default is "yellow".</param>
        [Conditional("DEBUG")]
        public static void LogWarning(string category, string message, string color = "yellow")
        {
            category = category.ToUpper();
            UnityEngine.Debug.LogWarning($"[<color={color}>{category}] {message}");
        }

        /// <summary>
        /// Logs an error message with a specified category and color.
        /// </summary>
        /// <param name="category">Category of the error (e.g., "UI", "Network").</param>
        /// <param name="message">The error message to display.</param>
        /// <param name="color">Optional. The color of the category in the error message. Default is "red".</param>
        [Conditional("DEBUG")]
        public static void LogError(string category, string message, string color = "red")
        {
            category = category.ToUpper();
            UnityEngine.Debug.LogError($"[<color={color}>{category}] {message}");
        }

        /// <summary>
        /// Logs the real-time elapsed since the game started.
        /// </summary>
        [Conditional("DEBUG")]
        public static void LogRealTimeSinceStartup()
        {
            UnityEngine.Debug.Log($"The elapsed time: " + Time.realtimeSinceStartup);
        }
    }
}

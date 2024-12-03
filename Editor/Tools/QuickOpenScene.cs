using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEditor.SceneManagement;

namespace com.zuyzuy.workspace.Editor
{
    public class QuickOpenScene : EditorWindow
    {
        private string[] scenePaths;
        private int selectedSceneIndex = 0;

        [MenuItem("Tools/0 - Quick Scene Opener")]
        public static void ShowWindow()
        {
            // Create or show the editor window
            GetWindow<QuickOpenScene>("Fast Scene Opener");
        }

        private void OnEnable()
        {
            // Load all scene paths in the "Assets/Scenes" directory (you can modify this path)
            string sceneDirectory = "Assets/Scenes";
            scenePaths = Directory.GetFiles(sceneDirectory, "*.unity", SearchOption.TopDirectoryOnly);

            // Sort the scene list (optional)
            System.Array.Sort(scenePaths);
        }

        private void OnGUI()
        {
            // Title
            GUILayout.Label("Quick Scene Opener", EditorStyles.boldLabel);

            // Dropdown menu for selecting a scene
            selectedSceneIndex = EditorGUILayout.Popup("Select Scene", selectedSceneIndex, scenePaths);

            // Button to open the selected scene
            if (GUILayout.Button("Open Scene"))
            {
                if (scenePaths.Length > 0)
                {
                    string scenePath = scenePaths[selectedSceneIndex];
                    EditorSceneManager.OpenScene(scenePath);
                    Debug.Log("Opening scene: " + scenePath);
                }
                else
                {
                    Debug.LogError("No scenes found in the specified directory.");
                }
            }
        }
    }
}
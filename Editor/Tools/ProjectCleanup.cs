using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Zuy.Workspace.Editor
{
    /// <summary>
    /// Project Cleanup and Organization Tool
    /// </summary>
    public class ProjectCleanup : EditorWindow
    {
        private bool _deleteEmptyFolders = true;
        private bool _findMissingReferences = true;
        private bool _findDuplicateAssets = true;

        [MenuItem("Tools/1 - Project Cleanup &1")]
        public static void ShowWindow()
        {
            GetWindow<ProjectCleanup>("Project Cleanup");
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Project Cleanup Utilities", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            // Cleanup options
            _deleteEmptyFolders = EditorGUILayout.Toggle("Delete Empty Folders", _deleteEmptyFolders);
            _findMissingReferences = EditorGUILayout.Toggle("Find Missing Script in Current Scene", _findMissingReferences);
            _findDuplicateAssets = EditorGUILayout.Toggle("Find Duplicate Assets", _findDuplicateAssets);

            EditorGUILayout.Space();

            if (GUILayout.Button("Run Cleanup"))
            {
                RunCleanup();
            }
        }

        private void RunCleanup()
        {
            if (_deleteEmptyFolders)
            {
                DeleteEmptyFolders();
            }

            if (_findMissingReferences)
            {
                FindMissingReferencesInCurrentScene();
            }

            if (_findDuplicateAssets)
            {
                FindDuplicateAssets();
            }

            AssetDatabase.Refresh();
        }

        private void DeleteEmptyFolders()
        {
            string[] folders = Directory.GetDirectories(Application.dataPath, "*", SearchOption.AllDirectories);

            foreach (string folder in folders)
            {
                if (Directory.GetFiles(folder).Length == 0 && Directory.GetDirectories(folder).Length == 0)
                {
                    try
                    {
                        // Uncomment to actually delete folders
                        // Directory.Delete(folder);
                        Debug.Log($"Empty folder: {folder}");
                    }
                    catch (System.Exception e)
                    {
                        Debug.LogWarning($"Could not delete folder {folder}: {e.Message}");
                    }
                }
            }
        }

        private void FindMissingReferencesInCurrentScene()
        {
            // Get the current active scene
            Scene currentScene = SceneManager.GetActiveScene();

            if (!currentScene.isLoaded)
            {
                Debug.LogWarning("No scene is currently loaded.");
                return;
            }

            // Find all root game objects in the current scene
            GameObject[] rootObjects = currentScene.GetRootGameObjects();

            // Track missing references
            var missingReferences = new List<(GameObject gameObject, int componentIndex)>();

            // Iterate through all root objects and their children
            foreach (GameObject rootObj in rootObjects)
            {
                // Get all components in this object and its children
                Component[] components = rootObj.GetComponentsInChildren<Component>();

                for (int i = 0; i < components.Length; i++)
                {
                    // If the component is null, it's a missing reference
                    if (components[i] == null)
                    {
                        missingReferences.Add((rootObj, i));
                    }
                }
            }

            // Log the results
            if (missingReferences.Any())
            {
                Debug.Log($"Found {missingReferences.Count} missing references in scene: {currentScene.name}");
                foreach (var (gameObject, componentIndex) in missingReferences)
                {
                    Debug.Log($"- Missing reference in GameObject: {gameObject.transform.gameObject.name} (Scene: {currentScene.name})");
                }
            }
            else
            {
                Debug.Log($"No missing references found in scene: {currentScene.name}");
            }
        }

        private void FindDuplicateAssets()
        {
            // Get all asset paths in the Assets directory (excluding Packages and other system folders)
            var allAssetPaths = AssetDatabase.GetAllAssetPaths()
                .Where(path => path.StartsWith("Assets/") && !path.StartsWith("Packages/"))
                .ToList();

            var duplicateGroups = allAssetPaths
                .GroupBy(path => Path.GetFileNameWithoutExtension(path))
                .Where(g => g.Count() > 1)
                .ToList();

            foreach (var group in duplicateGroups)
            {
                Debug.Log($"Potential duplicate assets with name: {group.Key}");
                foreach (var path in group)
                {
                    Debug.Log($" - {path}");
                }
            }
        }
    }
}
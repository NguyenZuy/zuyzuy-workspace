using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace com.zuyzuy.workspace.Editor
{
    public class UnusedAssetFinder : EditorWindow
    {
        [MenuItem("Tools/3 - Find Unused Assets &2")]
        public static void ShowWindow()
        {
            GetWindow<UnusedAssetFinder>("Unused Assets");
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Find Unused Assets"))
            {
                FindUnusedAssets();
            }
        }

        private void FindUnusedAssets()
        {
            // Define which asset types we want to consider
            string[] assetTypes = new string[]
            {
                "t:Texture",     // Textures (Images)
                "t:Sprite",      // Sprites
                "t:Model",       // 3D Models (Meshes)
                "t:Material",    // Materials
                "t:Shader",      // Shaders
                "t:AudioClip",   // Audio
                "t:Prefab"       // Prefabs
            };

            // Get all asset paths of the specified types that are in the "Assets/" folder only
            var allAssets = assetTypes
                .SelectMany(type => AssetDatabase.FindAssets(type))
                .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
                .Where(path => path.StartsWith("Assets/")) // Only include assets in the "Assets/" directory
                .ToList();

            var usedAssets = new HashSet<string>();

            // Collect references from scenes
            foreach (var scene in EditorBuildSettings.scenes)
            {
                if (scene.enabled)
                {
                    var dependencies = AssetDatabase.GetDependencies(scene.path);
                    foreach (var dep in dependencies)
                    {
                        usedAssets.Add(dep);
                    }
                }
            }

            // Collect references from prefabs (including dependencies)
            foreach (var prefabGuid in AssetDatabase.FindAssets("t:Prefab"))
            {
                var prefabPath = AssetDatabase.GUIDToAssetPath(prefabGuid);
                var dependencies = AssetDatabase.GetDependencies(prefabPath);
                foreach (var dep in dependencies)
                {
                    usedAssets.Add(dep);
                }
            }

            // Find unused assets (in "Assets/" directory)
            var unusedAssets = allAssets.Except(usedAssets);
            foreach (var unused in unusedAssets)
            {
                Debug.Log($"Unused Asset: {unused}");
            }
        }
    }
}

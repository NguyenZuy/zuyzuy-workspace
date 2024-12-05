using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Zuy.Workspace.Editor
{
    public class SceneQuickAccess : EditorWindow
    {
        private List<string> recentScenes = new List<string>();
        private List<string> favoriteScenes = new List<string>();
        private Vector2 scrollPosition;

        [MenuItem("Tools/0 - Quick Scene Opener &0")]
        public static void ShowWindow()
        {
            GetWindow<SceneQuickAccess>("Scene Quick Access");
        }

        private void OnEnable()
        {
            // Load saved favorite scenes
            LoadFavoriteScenes();
            TrackCurrentScene();
            // Register the scene change event and update recent scenes
            EditorApplication.update += UpdateRecentScenes;
        }

        private void OnDisable()
        {
            // Unsubscribe from update event
            EditorApplication.update -= UpdateRecentScenes;
        }

        private void OnGUI()
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Favorite Scenes", EditorStyles.boldLabel);

            // Favorite Scenes Section
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            for (int i = favoriteScenes.Count - 1; i >= 0; i--)
            {
                EditorGUILayout.BeginHorizontal();

                if (GUILayout.Button(Path.GetFileNameWithoutExtension(favoriteScenes[i]), GUILayout.Width(200)))
                {
                    OpenScene(favoriteScenes[i]);
                }

                if (GUILayout.Button("X", GUILayout.Width(30)))
                {
                    RemoveFavoriteScene(favoriteScenes[i]);
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();

            EditorGUILayout.Space();

            // Add Current Scene to Favorites
            if (GUILayout.Button("Add Current Scene to Favorites"))
            {
                AddCurrentSceneToFavorites();
            }

            // EditorGUILayout.Space();
            // EditorGUILayout.LabelField("Recent Scenes", EditorStyles.boldLabel);

            // // Recent Scenes Section
            // for (int i = 1; i < recentScenes.Count && i <= 3; i++)
            // {
            //     if (GUILayout.Button(Path.GetFileNameWithoutExtension(recentScenes[i])))
            //     {
            //         OpenScene(recentScenes[i]);
            //     }
            // }
        }

        private void TrackCurrentScene()
        {
            string currentScenePath = EditorSceneManager.GetActiveScene().path;
            if (!string.IsNullOrEmpty(currentScenePath) && !recentScenes.Contains(currentScenePath))
            {
                recentScenes.Insert(0, currentScenePath);

                // Limit recent scenes to last 10
                if (recentScenes.Count > 10)
                    recentScenes.RemoveAt(recentScenes.Count - 1);
            }
        }

        private void UpdateRecentScenes()
        {
            // Track current scene on editor update (real-time scene change tracking)
            TrackCurrentScene();

            // Force the window to refresh whenever recentScenes is updated
            Repaint();
        }

        private void OpenScene(string scenePath)
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorSceneManager.OpenScene(scenePath);
            }
        }

        private void AddCurrentSceneToFavorites()
        {
            string currentScenePath = EditorSceneManager.GetActiveScene().path;

            if (!string.IsNullOrEmpty(currentScenePath) && !favoriteScenes.Contains(currentScenePath))
            {
                favoriteScenes.Add(currentScenePath);
                SaveFavoriteScenes();
            }
        }

        private void RemoveFavoriteScene(string scenePath)
        {
            favoriteScenes.Remove(scenePath);
            SaveFavoriteScenes();
        }

        private void SaveFavoriteScenes()
        {
            EditorPrefs.SetString("ZuyTools_FavoriteScenes", string.Join("|", favoriteScenes));
        }

        private void LoadFavoriteScenes()
        {
            string savedScenes = EditorPrefs.GetString("ZuyTools_FavoriteScenes", "");
            favoriteScenes = new List<string>(savedScenes.Split(new[] { '|' }, System.StringSplitOptions.RemoveEmptyEntries));
        }

        // Track scene changes
        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            EditorSceneManager.activeSceneChangedInEditMode += OnSceneChanged;
        }

        private static void OnSceneChanged(UnityEngine.SceneManagement.Scene oldScene, UnityEngine.SceneManagement.Scene newScene)
        {
            SceneQuickAccess window = GetWindow<SceneQuickAccess>(typeof(SceneQuickAccess));
            if (window != null)
            {
                window.TrackCurrentScene();
                window.Repaint(); // Force window refresh on scene change
            }
        }
    }
}

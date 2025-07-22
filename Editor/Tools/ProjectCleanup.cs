using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZuyZuy.Workspace.Editor
{
    /// <summary>
    /// Enhanced Project Cleanup and Organization Tool
    /// </summary>
    public class ProjectCleanup : EditorWindow
    {
        #region Private Fields

        // Cleanup options
        private bool _deleteEmptyFolders = true;
        private bool _findMissingReferences = true;
        private bool _findDuplicateAssets = true;
        private bool _findUnusedAssets = false;
        private bool _optimizeTextures = false;
        private bool _cleanupMetaFiles = false;

        // UI State
        private Vector2 _scrollPosition;
        private bool _isProcessing = false;
        private bool _showAdvancedOptions = false;
        private bool _showResults = false;

        // Results
        private List<string> _emptyFolders = new List<string>();
        private List<string> _missingReferences = new List<string>();
        private List<string> _duplicateAssets = new List<string>();
        private List<string> _unusedAssets = new List<string>();
        private int _totalIssuesFound = 0;

        // Styles
        private static GUIStyle _headerStyle;
        private static GUIStyle _sectionStyle;
        private static GUIStyle _buttonStyle;
        private static GUIStyle _warningBoxStyle;
        private static GUIStyle _successBoxStyle;
        private static GUIStyle _resultBoxStyle;
        private static GUIStyle _foldoutStyle;

        #endregion

        #region Unity Methods

        [MenuItem("Tools/2 - Project Cleanup &2")]
        public static void ShowWindow()
        {
            var window = GetWindow<ProjectCleanup>("Project Cleanup");
            window.minSize = new Vector2(400, 500);
            window.Show();
        }

        private void OnEnable()
        {
            InitializeStyles();
        }

        private void OnGUI()
        {
            InitializeStyles();

            using (var scroll = new EditorGUILayout.ScrollViewScope(_scrollPosition))
            {
                _scrollPosition = scroll.scrollPosition;

                DrawHeader();
                DrawCleanupOptions();
                DrawAdvancedOptions();
                DrawActionButtons();
                DrawResults();
            }
        }

        #endregion

        #region UI Drawing Methods

        private void DrawHeader()
        {
            // Main header
            GUILayout.Space(10);
            EditorGUILayout.LabelField("🧹 PROJECT CLEANUP", _headerStyle);

            // Subtitle
            EditorGUILayout.LabelField("Keep your Unity project clean and organized", _sectionStyle);

            DrawSeparator();
        }

        private void DrawCleanupOptions()
        {
            EditorGUILayout.LabelField("🔧 CLEANUP OPTIONS", _sectionStyle);

            using (new EditorGUILayout.VerticalScope(_resultBoxStyle))
            {
                _deleteEmptyFolders = DrawToggleOption(
                    _deleteEmptyFolders,
                    "📁 Delete Empty Folders",
                    "Remove folders that contain no files or subdirectories"
                );

                _findMissingReferences = DrawToggleOption(
                    _findMissingReferences,
                    "🔍 Find Missing Script References",
                    "Locate GameObjects with missing script components in the current scene"
                );

                _findDuplicateAssets = DrawToggleOption(
                    _findDuplicateAssets,
                    "👥 Find Duplicate Assets",
                    "Identify assets with the same filename that might be duplicates"
                );
            }
        }

        private void DrawAdvancedOptions()
        {
            GUILayout.Space(10);

            _showAdvancedOptions = EditorGUILayout.Foldout(_showAdvancedOptions, "⚙️ ADVANCED OPTIONS", _foldoutStyle);

            if (_showAdvancedOptions)
            {
                using (new EditorGUILayout.VerticalScope(_resultBoxStyle))
                {
                    _findUnusedAssets = DrawToggleOption(
                        _findUnusedAssets,
                        "🗑️ Find Unused Assets (Experimental)",
                        "Identify assets that might not be referenced anywhere"
                    );

                    _optimizeTextures = DrawToggleOption(
                        _optimizeTextures,
                        "🖼️ Optimize Texture Settings",
                        "Check for textures that could be optimized"
                    );

                    _cleanupMetaFiles = DrawToggleOption(
                        _cleanupMetaFiles,
                        "📋 Cleanup Meta Files",
                        "Remove orphaned .meta files"
                    );
                }
            }
        }

        private void DrawActionButtons()
        {
            GUILayout.Space(15);

            EditorGUI.BeginDisabledGroup(_isProcessing);

            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button(_isProcessing ? "🔄 Processing..." : "▶️ ANALYZE PROJECT", _buttonStyle, GUILayout.Height(35)))
                {
                    AnalyzeProject();
                }

                if (GUILayout.Button("🧽 CLEAN PROJECT", _buttonStyle, GUILayout.Height(35), GUILayout.Width(150)))
                {
                    if (EditorUtility.DisplayDialog("Clean Project",
                        "This will perform the selected cleanup operations. This action cannot be undone. Continue?",
                        "Yes, Clean", "Cancel"))
                    {
                        RunCleanup();
                    }
                }
            }

            EditorGUI.EndDisabledGroup();
        }

        private void DrawResults()
        {
            if (!_showResults) return;

            GUILayout.Space(15);
            DrawSeparator();

            if (_totalIssuesFound > 0)
            {
                EditorGUILayout.LabelField($"⚠️ ANALYSIS RESULTS ({_totalIssuesFound} issues found)", _sectionStyle);

                DrawResultSection("Empty Folders", _emptyFolders, "📁");
                DrawResultSection("Missing References", _missingReferences, "❌");
                DrawResultSection("Duplicate Assets", _duplicateAssets, "👥");
                DrawResultSection("Unused Assets", _unusedAssets, "🗑️");
            }
            else
            {
                using (new EditorGUILayout.VerticalScope(_successBoxStyle))
                {
                    EditorGUILayout.LabelField("✅ Great! No issues found in your project.", EditorStyles.boldLabel);
                    EditorGUILayout.LabelField("Your project appears to be clean and well-organized.", EditorStyles.label);
                }
            }
        }

        private void DrawResultSection(string title, List<string> items, string icon)
        {
            if (items == null || items.Count == 0) return;

            GUILayout.Space(8);

            bool foldout = EditorPrefs.GetBool($"ProjectCleanup_{title}", false);
            foldout = EditorGUILayout.Foldout(foldout, $"{icon} {title} ({items.Count})", _foldoutStyle);
            EditorPrefs.SetBool($"ProjectCleanup_{title}", foldout);

            if (foldout)
            {
                using (new EditorGUILayout.VerticalScope(_warningBoxStyle))
                {
                    foreach (var item in items.Take(20)) // Limit display to first 20 items
                    {
                        EditorGUILayout.LabelField($"• {item}", EditorStyles.miniLabel);
                    }

                    if (items.Count > 20)
                    {
                        EditorGUILayout.LabelField($"... and {items.Count - 20} more items", EditorStyles.miniLabel);
                    }
                }
            }
        }

        private bool DrawToggleOption(bool value, string label, string tooltip)
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                bool newValue = EditorGUILayout.Toggle(value, GUILayout.Width(20));

                var content = new GUIContent(label, tooltip);
                EditorGUILayout.LabelField(content, EditorStyles.label);

                return newValue;
            }
        }

        private void DrawSeparator()
        {
            GUILayout.Space(5);
            var rect = GUILayoutUtility.GetRect(1, 2, GUILayout.ExpandWidth(true));
            EditorGUI.DrawRect(rect, EditorGUIUtility.isProSkin ? new Color(0.4f, 0.4f, 0.4f) : new Color(0.6f, 0.6f, 0.6f));
            GUILayout.Space(10);
        }

        #endregion

        #region Cleanup Logic

        private void AnalyzeProject()
        {
            _isProcessing = true;
            _showResults = false;

            // Clear previous results
            _emptyFolders.Clear();
            _missingReferences.Clear();
            _duplicateAssets.Clear();
            _unusedAssets.Clear();
            _totalIssuesFound = 0;

            try
            {
                if (_deleteEmptyFolders) AnalyzeEmptyFolders();
                if (_findMissingReferences) AnalyzeMissingReferences();
                if (_findDuplicateAssets) AnalyzeDuplicateAssets();
                if (_findUnusedAssets) AnalyzeUnusedAssets();

                _totalIssuesFound = _emptyFolders.Count + _missingReferences.Count +
                                  _duplicateAssets.Count + _unusedAssets.Count;

                _showResults = true;
                Debug.Log($"Project analysis completed. Found {_totalIssuesFound} potential issues.");
            }
            finally
            {
                _isProcessing = false;
            }
        }

        private void RunCleanup()
        {
            _isProcessing = true;

            try
            {
                if (_deleteEmptyFolders) DeleteEmptyFolders();
                if (_findMissingReferences) FindMissingReferencesInCurrentScene();
                if (_findDuplicateAssets) FindDuplicateAssets();

                AssetDatabase.Refresh();
                Debug.Log("Project cleanup completed successfully!");

                // Re-analyze after cleanup
                AnalyzeProject();
            }
            finally
            {
                _isProcessing = false;
            }
        }

        private void AnalyzeEmptyFolders()
        {
            string[] folders = Directory.GetDirectories(Application.dataPath, "*", SearchOption.AllDirectories);

            foreach (string folder in folders)
            {
                if (Directory.GetFiles(folder, "*", SearchOption.AllDirectories).Length == 0)
                {
                    _emptyFolders.Add(folder.Replace(Application.dataPath, "Assets"));
                }
            }
        }

        private void AnalyzeMissingReferences()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            if (!currentScene.isLoaded) return;

            GameObject[] rootObjects = currentScene.GetRootGameObjects();

            foreach (GameObject rootObj in rootObjects)
            {
                Component[] components = rootObj.GetComponentsInChildren<Component>(true);

                for (int i = 0; i < components.Length; i++)
                {
                    if (components[i] == null)
                    {
                        GameObject parent = rootObj;
                        if (i < components.Length - 1 && components[i + 1] != null)
                        {
                            parent = components[i + 1].gameObject;
                        }

                        _missingReferences.Add($"{parent.name} (Scene: {currentScene.name})");
                    }
                }
            }
        }

        private void AnalyzeDuplicateAssets()
        {
            var allAssetPaths = AssetDatabase.GetAllAssetPaths()
                .Where(path => path.StartsWith("Assets/") && !AssetDatabase.IsValidFolder(path))
                .ToList();

            var hashes = new Dictionary<string, List<string>>();

            // Use a progress bar for better user experience, as hashing can be slow
            EditorUtility.DisplayProgressBar("Analyzing Duplicates", "Calculating file hashes...", 0f);

            try
            {
                for (int i = 0; i < allAssetPaths.Count; i++)
                {
                    string path = allAssetPaths[i];
                    EditorUtility.DisplayProgressBar("Analyzing Duplicates", $"Processing {Path.GetFileName(path)}", (float)i / allAssetPaths.Count);

                    string fullPath = Path.GetFullPath(path);
                    if (File.Exists(fullPath))
                    {
                        string hash = GetFileHash(fullPath);
                        if (!hashes.ContainsKey(hash))
                        {
                            hashes[hash] = new List<string>();
                        }
                        hashes[hash].Add(path);
                    }
                }
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }

            // Filter for groups with more than one file (actual duplicates)
            var duplicateGroups = hashes.Where(kvp => kvp.Value.Count > 1);

            foreach (var group in duplicateGroups)
            {
                // Format the output to be more informative
                var sb = new StringBuilder();
                sb.AppendLine($"The following {group.Value.Count} files are identical:");
                foreach (var path in group.Value)
                {
                    sb.AppendLine($"  - {path}");
                }
                _duplicateAssets.Add(sb.ToString());
            }
        }

        private string GetFileHash(string filePath)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    var hashBytes = md5.ComputeHash(stream);
                    return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        private void AnalyzeUnusedAssets()
        {
            // This is a simplified implementation
            // A full implementation would require dependency analysis
            var allAssets = AssetDatabase.GetAllAssetPaths()
                .Where(path => path.StartsWith("Assets/") && !path.StartsWith("Packages/"))
                .Where(path => !AssetDatabase.IsValidFolder(path))
                .ToList();

            // For demonstration, we'll mark assets in "Unused" folders as potentially unused
            var potentiallyUnused = allAssets
                .Where(path => path.Contains("/Unused/") || path.Contains("/Old/") || path.Contains("/Deprecated/"))
                .ToList();

            _unusedAssets.AddRange(potentiallyUnused.Take(50)); // Limit results
        }

        #endregion

        #region Original Cleanup Methods

        private void DeleteEmptyFolders()
        {
            string[] folders = Directory.GetDirectories(Application.dataPath, "*", SearchOption.AllDirectories);
            int deletedCount = 0;

            foreach (string folder in folders)
            {
                if (Directory.GetFiles(folder, "*", SearchOption.AllDirectories).Length == 0)
                {
                    try
                    {
                        Directory.Delete(folder, true);
                        deletedCount++;
                        Debug.Log($"Deleted empty folder: {folder}");
                    }
                    catch (System.Exception e)
                    {
                        Debug.LogWarning($"Could not delete folder {folder}: {e.Message}");
                    }
                }
            }

            Debug.Log($"Deleted {deletedCount} empty folders.");
        }

        private void FindMissingReferencesInCurrentScene()
        {
            Scene currentScene = SceneManager.GetActiveScene();

            if (!currentScene.isLoaded)
            {
                Debug.LogWarning("No scene is currently loaded.");
                return;
            }

            GameObject[] rootObjects = currentScene.GetRootGameObjects();
            var missingReferences = new List<(GameObject gameObject, int componentIndex)>();

            foreach (GameObject rootObj in rootObjects)
            {
                Component[] components = rootObj.GetComponentsInChildren<Component>(true);

                for (int i = 0; i < components.Length; i++)
                {
                    if (components[i] == null)
                    {
                        missingReferences.Add((rootObj, i));
                    }
                }
            }

            if (missingReferences.Any())
            {
                Debug.Log($"Found {missingReferences.Count} missing references in scene: {currentScene.name}");
                foreach (var (gameObject, componentIndex) in missingReferences)
                {
                    Debug.Log($"- Missing reference in GameObject: {gameObject.name} (Scene: {currentScene.name})");
                }
            }
            else
            {
                Debug.Log($"No missing references found in scene: {currentScene.name}");
            }
        }

        private void FindDuplicateAssets()
        {
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

        #endregion

        #region Style Initialization

        private void InitializeStyles()
        {
            if (_headerStyle == null)
            {
                _headerStyle = new GUIStyle(EditorStyles.boldLabel)
                {
                    fontSize = 18,
                    alignment = TextAnchor.MiddleCenter,
                    normal = { textColor = EditorGUIUtility.isProSkin ? new Color(0.8f, 0.9f, 1f) : new Color(0.2f, 0.3f, 0.5f) }
                };
            }

            if (_sectionStyle == null)
            {
                _sectionStyle = new GUIStyle(EditorStyles.boldLabel)
                {
                    fontSize = 12,
                    normal = { textColor = EditorGUIUtility.isProSkin ? new Color(0.7f, 0.8f, 0.9f) : new Color(0.3f, 0.4f, 0.6f) }
                };
            }

            if (_buttonStyle == null)
            {
                _buttonStyle = new GUIStyle(GUI.skin.button)
                {
                    fontSize = 12,
                    fontStyle = FontStyle.Bold
                };
            }

            if (_warningBoxStyle == null)
            {
                _warningBoxStyle = new GUIStyle(EditorStyles.helpBox)
                {
                    normal = { textColor = EditorGUIUtility.isProSkin ? Color.white : Color.black },
                    padding = new RectOffset(10, 10, 8, 8)
                };
            }

            if (_successBoxStyle == null)
            {
                _successBoxStyle = new GUIStyle(EditorStyles.helpBox)
                {
                    normal = {
                        textColor = EditorGUIUtility.isProSkin ? new Color(0.8f, 1f, 0.8f) : new Color(0.2f, 0.6f, 0.2f)
                    },
                    padding = new RectOffset(15, 15, 12, 12)
                };
            }

            if (_resultBoxStyle == null)
            {
                _resultBoxStyle = new GUIStyle(EditorStyles.helpBox)
                {
                    padding = new RectOffset(12, 12, 8, 8),
                    margin = new RectOffset(0, 0, 5, 5)
                };
            }

            if (_foldoutStyle == null)
            {
                _foldoutStyle = new GUIStyle(EditorStyles.foldout)
                {
                    fontSize = 11,
                    fontStyle = FontStyle.Bold
                };
            }
        }

        #endregion
    }
}
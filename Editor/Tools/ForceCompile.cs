// This is the correct, modern, and efficient way.
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

namespace ZuyZuy.Workspace.Editor
{
    public static class ForceCompile
    {
        [MenuItem("Tools/1 - Force Compile &1")]
        public static void ShowWindow()
        {
            Debug.Log("User requested a script recompilation.");

            // This is the modern, official API to request a script compilation.
            // It will trigger the same process as if you had just saved a script.
            CompilationPipeline.RequestScriptCompilation();
        }
    }
}
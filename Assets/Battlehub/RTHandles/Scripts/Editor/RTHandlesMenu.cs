using UnityEditor;
using UnityEngine;
using System.Linq;

using Battlehub.RTCommon;
using Battlehub.Utils;

namespace Battlehub.RTHandles
{
    public static class RTHandlesMenu
    {
        [MenuItem("Tools/Runtime Handles/Create")]
        public static void CreateRuntimeEditor()
        {
            GameObject go = new GameObject();
            go.name = "RTHandles";
            RuntimeSelectionComponent window = go.AddComponent<RuntimeSelectionComponent>();
            window.WindowType = RuntimeWindowType.SceneView;
            go.AddComponent<RuntimeUndoComponent>();
            go.AddComponent<RuntimeToolsComponent>();

            Undo.RegisterCreatedObjectUndo(go, "Battlehub.RTHandles.Create");
        }

        [MenuItem("Tools/Runtime Handles/Enable Editing", validate = true)]
        private static bool CanEnableEditing()
        {
            return Selection.gameObjects != null 
                && Selection.gameObjects.Length > 0 
                && Selection.gameObjects.Any(g => !g.GetComponent<ExposeToEditor>() && !g.IsPrefab())
                && Object.FindObjectOfType<RuntimeSelectionComponent>();
        }

        [MenuItem("Tools/Runtime Handles/Enable Editing")]
        private static void EnableEditing()
        {
            foreach (GameObject go in Selection.gameObjects)
            {
                ExposeToEditor exposeToEditor = go.GetComponent<ExposeToEditor>();
                if (!exposeToEditor && !go.IsPrefab())
                {
                    Undo.RegisterCreatedObjectUndo(go.AddComponent<ExposeToEditor>(), "Battlehub.RTHandles.EnableEditing");
                }   
            }
        }

        [MenuItem("Tools/Runtime Handles/Disable Editing", validate = true)]
        private static bool CanDisableEditing()
        {
            return Selection.gameObjects != null
                && Selection.gameObjects.Length > 0
                && Selection.gameObjects.Any(g => g.GetComponent<ExposeToEditor>() && !g.IsPrefab());
        }

        [MenuItem("Tools/Runtime Handles/Disable Editing")]
        private static void DisableEditing()
        {
            foreach (GameObject go in Selection.gameObjects)
            {
                ExposeToEditor exposeToEditor = go.GetComponent<ExposeToEditor>();
                if (exposeToEditor && !go.IsPrefab())
                {
                    Undo.DestroyObjectImmediate(exposeToEditor);
                }
            }
        }
    }

}

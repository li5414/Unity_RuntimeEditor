using UnityEditor;

using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using Battlehub.Utils;

namespace Battlehub.RTHandles
{
    public static class RTHandlesMenuDemo
    {
        const string root = BHPath.Root + @"/RTHandles/Demo/";
        public static GameObject InstantiatePrefab(string name)
        {
            Object prefab = AssetDatabase.LoadAssetAtPath("Assets/" + root + "Prefabs/" + name, typeof(GameObject));
            return (GameObject)PrefabUtility.InstantiatePrefab(prefab);
        }

        [MenuItem("Tools/Runtime Handles/Demo/Create Editor")]
        public static void CreateDemoEditor()
        {
            if (Object.FindObjectOfType<EventSystem>() == null)
            {
                GameObject eventSystemGO = new GameObject();
                eventSystemGO.AddComponent<EventSystem>();
                eventSystemGO.AddComponent<StandaloneInputModule>();
                eventSystemGO.name = "EventSystem";
                Undo.RegisterCreatedObjectUndo(eventSystemGO, "Battlehub.RTHandles.Create");
            }
            
            GameObject editor = InstantiatePrefab("EditorDemo.prefab");
            Undo.RegisterCreatedObjectUndo(editor, "Battlehub.RTHandles.Create");
        }

        [MenuItem("Tools/Runtime Handles/Demo/Expose Prefab", validate = true)]
        private static bool CanExposePrefab()
        {
            EditorDemo editor = Object.FindObjectOfType<EditorDemo>();
            if(editor == null)
            {
                return false;
            }
            return Selection.gameObjects != null
                && Selection.gameObjects.Any(go => go.IsPrefab() && (editor.Prefabs == null || !editor.Prefabs.Contains(go)));
        }

        [MenuItem("Tools/Runtime Handles/Demo/Expose Prefab")]
        private static void ExposePrefab()
        {
            EditorDemo editor = Object.FindObjectOfType<EditorDemo>();
            Undo.RecordObject(editor, "Battlehub.RTEditor.ExposeToEditor");
            List<GameObject> prefabs = new List<GameObject>(editor.Prefabs);

            foreach (GameObject go in Selection.gameObjects)
            {
                if (go.IsPrefab())
                {
                    if(editor.Prefabs == null || !editor.Prefabs.Contains(go))
                    {
                        prefabs.Add(go);
                    }
                }
            }

            editor.Prefabs = prefabs.ToArray();
            EditorUtility.SetDirty(editor);
        }

        [MenuItem("Tools/Runtime Handles/Demo/Hide Prefab", validate = true)]
        private static bool CanHidePrefab()
        {
            EditorDemo editor = Object.FindObjectOfType<EditorDemo>();
            if (editor == null)
            {
                return false;
            }
            return Selection.gameObjects != null
                && Selection.gameObjects.Any(go => go.IsPrefab() && editor.Prefabs != null && editor.Prefabs.Contains(go));
        }

        [MenuItem("Tools/Runtime Handles/Demo/Hide Prefab")]
        private static void HidePrefab()
        {
            EditorDemo editor = Object.FindObjectOfType<EditorDemo>();
            Undo.RecordObject(editor, "Battlehub.RTEditor.ExposeToEditor");
            List<GameObject> prefabs = editor.Prefabs.ToList();

            foreach (GameObject go in Selection.gameObjects)
            {
                if (go.IsPrefab())
                {
                    if (editor.Prefabs != null && editor.Prefabs.Contains(go))
                    {
                        prefabs.Remove(go);
                    }
                }
            }

            editor.Prefabs = prefabs.ToArray();
            EditorUtility.SetDirty(editor);
        }
    }

}

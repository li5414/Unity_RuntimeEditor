using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;

using System.Linq;
using System.Collections.Generic;
using System.Reflection;

using Battlehub.Utils;
using Battlehub.RTCommon;
using Battlehub.RTSaveLoad;
using System;

using UnityObject = UnityEngine.Object;
namespace Battlehub.RTEditor
{
    public static class RTEditorMenu
    {
        const string root = BHPath.Root + @"/RTEditor/";

        [MenuItem("Tools/Runtime Editor/Create")]
        public static void CreateRuntimeEditor()
        {
            Undo.RegisterCreatedObjectUndo(InstantiateRuntimeEditor(), "Battlehub.RTEditor.Create");
            if (!UnityObject.FindObjectOfType<EventSystem>())
            {
                GameObject es = new GameObject();
                es.AddComponent<EventSystem>();
                es.AddComponent<StandaloneInputModule>();
                es.name = "EventSystem";
            }
        }

        public static GameObject InstantiateRuntimeEditor()
        {
            return InstantiatePrefab("RuntimeEditor.prefab");
        }

        [MenuItem("Tools/Runtime Editor/Expose To Editor", validate = true)]
        private static bool CanExposeToEditor()
        {
            if (Selection.objects == null || Selection.objects.Length == 0)
            {
                return false;
            }

            Assembly unityEditorAssembly = typeof(Editor).Assembly;
            FolderTemplate[] folderTemplates = Resources.FindObjectsOfTypeAll<FolderTemplate>().Where(template => !template.gameObject.IsPrefab()).ToArray();
            Func<UnityObject, bool> isObjectHidden = o => (!folderTemplates.Any(template => template.Objects != null && template.Objects.Contains(o)));

            
            return
                Selection.objects.Any(o => 
                    !(o is GameObject) && 
                    !(o.GetType().Assembly == unityEditorAssembly) && 
                    isObjectHidden(o)) ||    
                Selection.gameObjects.Any(g => 
                    !g.IsPrefab() && !g.GetComponent<ExposeToEditor>() || 
                    g.IsPrefab() && isObjectHidden(g));

        }

        [MenuItem("Tools/Runtime Editor/Expose To Editor")]
        private static void ExposeToEditor()
        {
            FolderTemplate[] folderTemplates = Resources.FindObjectsOfTypeAll<FolderTemplate>().Where(template => !template.gameObject.IsPrefab()).ToArray();

            bool resourceMapUpdateRequired = false;
            IProjectManager projectManager = Dependencies.ProjectManager;

            List<UnityObject> objectsList = new List<UnityObject>();
            foreach (GameObject go in Selection.gameObjects)
            {
                if (go.IsPrefab())
                {
                    if (!objectsList.Contains(go))
                    {
                        objectsList.Add(go);
                    }
                }
                else
                {
                    if (!go.GetComponent<ExposeToEditor>())
                    {
                        Undo.RegisterCreatedObjectUndo(go.AddComponent<ExposeToEditor>(), "Battlehub.RTEditor.ExposeToEditor");
                    }
                }
            }

            foreach (Material material in Selection.objects.OfType<Material>())
            {
                if (!objectsList.Contains(material))
                {
                    objectsList.Add(material);
                }
            }


            foreach (UnityObject obj in Selection.objects)
            {
                if (obj is GameObject || obj is Material)
                {
                    continue;
                }

                if (!objectsList.Contains(obj))
                {
                    objectsList.Add(obj);
                }
            }

            Dictionary<UnityObject, FolderTemplate[]> objectsWithMultipleFolders = new Dictionary<UnityObject, FolderTemplate[]>();
            foreach (UnityObject obj in objectsList)
            {
                string path = AssetDatabase.GetAssetPath(obj);
                if(!string.IsNullOrEmpty(path))
                {
                    AssetImporter importer = AssetImporter.GetAtPath(path);
                    if(!string.IsNullOrEmpty(importer.assetBundleName))
                    {
                        EditorUtility.DisplayDialog("Unable expose to Editor", string.Format("Unable expose to editor resource {0} from {1}. Asset belongs to asset bundle: {2}", obj.name, path, importer.assetBundleName) , "OK");
                        continue;
                    }
                }

                if (obj is Material)
                {
                    Material material = (Material)obj;
                    if (material.shader != null)
                    {
                        if (Dependencies.ShaderUtil.GetShaderInfo(material.shader) == null)
                        {
                            resourceMapUpdateRequired = true;
                        }
                    }
                }
                
                if(projectManager != null)
                {
                    if(!projectManager.IsResource(obj))
                    {
                        resourceMapUpdateRequired = true;
                    }
                }
              

                FolderTemplate[] folders = folderTemplates.Where(template => IsSuitableForObject(template, obj)).ToArray();
                if (folders.Length > 1)
                {
                    if (!objectsWithMultipleFolders.ContainsKey(obj))
                    {
                        objectsWithMultipleFolders.Add(obj, folders);
                    }
                }
                else
                {
                    Debug.LogWarning("Can't expose " + obj);
                }
            }

            

            if (objectsWithMultipleFolders.Count > 0)
            {
                SelectFolderDialog window = ScriptableObject.CreateInstance<SelectFolderDialog>();
                window.position = new Rect(Screen.width / 2, Screen.height / 2, 250, 100);
                window.titleContent = new GUIContent("Select Folder");
                window.ResourceMapUpdateRequired = resourceMapUpdateRequired;
                window.ObjectsWithMultipleFolders = objectsWithMultipleFolders;
                window.ShowUtility();
            }

            foreach(FolderTemplate folderTemplate in folderTemplates)
            {
                EditorUtility.SetDirty(folderTemplate.gameObject);
            }    
        }

        private static bool IsSuitableForObject(FolderTemplate template,  UnityObject obj)
        {
            if (template.TypeHint != AssetTypeHint.All)
            {
                if ((template.TypeHint & AssetTypeHint.ProceduralMaterial) != 0 && obj.GetType() == typeof(ProceduralMaterial))
                {
                    return true;
                }

                if ((template.TypeHint & AssetTypeHint.Material) != 0 && obj.GetType() == typeof(Material))
                {
                    return true;
                }

                if ((template.TypeHint & AssetTypeHint.Mesh) != 0 && obj.GetType() == typeof(Mesh))
                {
                    return true;
                }

                if ((template.TypeHint & AssetTypeHint.Prefab) != 0 && obj.GetType() == typeof(GameObject))
                {
                    return true;
                }

                if ((template.TypeHint & AssetTypeHint.Texture) != 0 && obj is Texture)
                {
                    return true;
                }

                return false;
            }

            return true;

        }


        [MenuItem("Tools/Runtime Editor/Hide From Editor", validate = true)]
        private static bool CanHideFromEditor()
        {

            if (Selection.objects == null || Selection.objects.Length == 0)
            {
                return false;
            }

            FolderTemplate[] projectTemplates = Resources.FindObjectsOfTypeAll<FolderTemplate>().Where(templates => !templates.gameObject.IsPrefab()).ToArray();

            Func<UnityObject, bool> isObjectExposed = o => (projectTemplates.Any(template => template.Objects != null && template.Objects.Contains(o)));

            return
                Selection.objects.OfType<UnityObject>().Any(o => 
                    !(o is GameObject) && isObjectExposed(o)) ||

                Selection.gameObjects.Any(g => 
                    !g.IsPrefab() && g.GetComponent<ExposeToEditor>() || 
                     g.IsPrefab() && isObjectExposed(g));
        }

        [MenuItem("Tools/Runtime Editor/Hide From Editor")]
        private static void HideFromEditor()
        {
            FolderTemplate[] templates = Resources.FindObjectsOfTypeAll<FolderTemplate>().Where(template => !template.gameObject.IsPrefab()).ToArray();
      
            List<UnityObject> objectsList = new List<UnityObject>();

            foreach (GameObject go in Selection.gameObjects)
            {
                if (go.IsPrefab())
                {
                    objectsList.Add(go);
                }
                else
                {
                    ExposeToEditor exposeToEditor = go.GetComponent<ExposeToEditor>();
                    if (exposeToEditor != null)
                    {
                        Undo.DestroyObjectImmediate(exposeToEditor);
                    }
                }
            }

            foreach (UnityObject obj in Selection.objects)
            {
                if (obj is GameObject)
                {
                    continue;
                }
                objectsList.Add(obj);
            }

            foreach (UnityObject obj in objectsList)
            {
                FolderTemplate template = templates.Where(t => t.Objects != null && t.Objects.Contains(obj)).FirstOrDefault();
                if(template != null)
                {
                    Undo.RecordObject(template, "BH.RTE.HideFromEditor");
                    List<UnityObject> templateObjects = template.Objects.ToList();
                    templateObjects.Remove(obj);
                    template.Objects = templateObjects.ToArray();
                    EditorUtility.SetDirty(template.gameObject);
                }
            }
        }

        public static GameObject InstantiatePrefab(string name)
        {
            UnityObject prefab = AssetDatabase.LoadAssetAtPath("Assets/" + root + "Prefabs/" + name, typeof(GameObject));
            return (GameObject)PrefabUtility.InstantiatePrefab(prefab);
        }
    }

}

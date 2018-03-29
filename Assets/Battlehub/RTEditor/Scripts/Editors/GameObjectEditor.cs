
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Battlehub.RTCommon;
using System;

namespace Battlehub.RTEditor
{
    public class GameObjectEditor : MonoBehaviour
    {
        [SerializeField]
        private Toggle TogEnableDisable;
        [SerializeField]
        private InputField InputName;
        [SerializeField]
        private Transform ComponentsPanel;

        private void Start()
        {
            GameObject go = RuntimeSelection.activeGameObject;
            ExposeToEditor exposeToEditor = go.GetComponent<ExposeToEditor>();
            HierarchyItem hierarchyItem = go.GetComponent<HierarchyItem>();
            HashSet<Component> ignoreComponents = new HashSet<Component>();
            if(exposeToEditor != null)
            {
                if (exposeToEditor.Colliders != null)
                {
                    for (int i = 0; i < exposeToEditor.Colliders.Length; ++i)
                    {
                        Collider collider = exposeToEditor.Colliders[i];
                        if (!ignoreComponents.Contains(collider))
                        {
                            ignoreComponents.Add(collider);
                        }
                    }
                }

                ignoreComponents.Add(exposeToEditor);
            }
          
            if(hierarchyItem != null)
            {
                ignoreComponents.Add(hierarchyItem);
            }
            
            InputName.text = go.name;
            TogEnableDisable.isOn = go.activeSelf;

            InputName.onEndEdit.AddListener(OnEndEditName);
            TogEnableDisable.onValueChanged.AddListener(OnEnableDisable);
            Component[] components = go.GetComponents<Component>();
            for (int i = 0; i < components.Length; ++i)
            {
                Component component = components[i];
                if(component == null)
                {
                    continue;
                }

                if(ignoreComponents.Contains(component))
                {
                    continue;
                }

                if((component.hideFlags & HideFlags.HideInInspector) != 0)
                {
                    continue;
                }

                if (EditorsMap.IsObjectEditorEnabled(component.GetType()))
                {
                    GameObject editorPrefab = EditorsMap.GetObjectEditor(component.GetType());
                    if (editorPrefab != null)
                    {
                        ComponentEditor componentEditorPrefab = editorPrefab.GetComponent<ComponentEditor>();
                        if (componentEditorPrefab != null)
                        {
                            ComponentEditor editor = Instantiate(componentEditorPrefab);
                            editor.EndEditCallback = () =>
                            {
                                RuntimeEditorApplication.SaveSelectedObjects();
                            };
                            editor.transform.SetParent(ComponentsPanel, false);
                            editor.Component = component;
                        }
                        else
                        {
                            Debug.LogErrorFormat("editor prefab {0} does not have ComponentEditor script", editorPrefab.name);
                        }
                    }
                }
            }
        }

        private void OnDestroy()
        {
            if (InputName != null)
            {
                InputName.onEndEdit.RemoveListener(OnEndEditName);
            }
            if(TogEnableDisable != null)
            {
                TogEnableDisable.onValueChanged.RemoveListener(OnEnableDisable);
            }
        }

        private void OnEnableDisable(bool enable)
        {
            GameObject go = RuntimeSelection.activeGameObject;
            go.SetActive(enable);
        }

        private void OnEndEditName(string name)
        {
            GameObject go = RuntimeSelection.activeGameObject;
            ExposeToEditor exposeToEditor = go.GetComponent<ExposeToEditor>();
            if(exposeToEditor != null)
            {
                exposeToEditor.SetName(name);
            }
            else
            {
                go.name = name;
            }
        }
    }
}


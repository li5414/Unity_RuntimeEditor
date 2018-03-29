using System;
using System.Linq;
using UnityEngine;

using Battlehub.RTCommon;
using UnityObject = UnityEngine.Object;


namespace Battlehub.RTEditor
{
    public class InspectorWindow : RuntimeEditorWindow
    {
        public GameObject GameObjectEditor;
        public GameObject MaterialEditor;
        public Transform Panel;

        private GameObject m_editor;

        

        protected override void AwakeOverride()
        {
            base.AwakeOverride();
            if(GameObjectEditor == null)
            {
                Debug.LogError("GameObjectEditor is not set");
            }
            if (MaterialEditor == null)
            {
                Debug.LogError("MaterialEditor is not set");
            }
            RuntimeSelection.SelectionChanged += OnRuntimeSelectionChanged;
        }

        protected override void UpdateOverride()
        {
            base.UpdateOverride();
            UnityObject obj = RuntimeSelection.activeObject;
            if(obj == null)
            {
                if (m_editor != null)
                {
                    Destroy(m_editor);
                }
            }
        }

        private void OnEnable()
        {
            CreateEditor();
        }

        private void OnDisable()
        {
            if (m_editor != null)
            {
                Destroy(m_editor);
            }
        }

        protected override void OnDestroyOverride()
        {
            base.OnDestroyOverride();
            RuntimeSelection.SelectionChanged -= OnRuntimeSelectionChanged;
        }

        private void OnRuntimeSelectionChanged(UnityObject[] unselectedObjects)
        {
            CreateEditor();
        }

        private void CreateEditor()
        {
            if (m_editor != null)
            {
                Destroy(m_editor);
            }

            if (RuntimeSelection.activeObject == null)
            {
                return;
            }

            UnityObject[] selectedObjects = RuntimeSelection.objects.Where(o => o != null).ToArray();
            if (selectedObjects.Length != 1)
            {
                return;
            }

            //if (RuntimeSelection.activeGameObject != null)
            //{
            //    if (RuntimeSelection.activeGameObject.IsPrefab())
            //    {
            //        return;
            //    }
            //}

            Type objType = selectedObjects[0].GetType();
            for (int i = 1; i < selectedObjects.Length; ++i)
            {
                if (objType != selectedObjects[i].GetType())
                {
                    return;
                }
            }

            GameObject editorPrefab;

#if !UNITY_WEBGL
            if (objType == typeof(Material) || objType == typeof(ProceduralMaterial))    
#else
            if (objType == typeof(Material))
#endif

            {
                Material mat = selectedObjects[0] as Material;
                if (mat.shader == null)
                {
                    return;
                }

                //if(!EditorsMap.IsMaterialEditorEnabled(mat.shader))
                //{
                //    return;
                //}
                editorPrefab = EditorsMap.GetMaterialEditor(mat.shader);
            }
            else
            {
                if (!EditorsMap.IsObjectEditorEnabled(objType))
                {
                    return;
                }
                editorPrefab = EditorsMap.GetObjectEditor(objType);
            }

            if (editorPrefab != null)
            {
                m_editor = Instantiate(editorPrefab);
                m_editor.transform.SetParent(Panel, false);
            }
        }
    }
}

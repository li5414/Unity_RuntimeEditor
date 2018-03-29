using UnityEngine;
using System;
using System.Collections.Generic;

namespace Battlehub.RTEditor
{
    public partial class EditorsMap
    {
        private class EditorDescriptor
        {
            public int Index;
            public bool Enabled;
            public bool IsPropertyEditor;

            public EditorDescriptor(int index, bool enabled, bool isPropertyEditor)
            {
                Index = index;
                Enabled = enabled;
                IsPropertyEditor = isPropertyEditor;
            }
        }

        static EditorsMap()
        {
            LoadMap();
        }

        private class MaterialEditorDescriptor
        {
            public GameObject Editor;
            public bool Enabled;

            public MaterialEditorDescriptor(GameObject editor, bool enabled)
            {
                Editor = editor;
                Enabled = enabled;
            }
        }

        private static GameObject m_defaultMaterialEditor;
        private static Dictionary<Shader, MaterialEditorDescriptor> m_materialMap = new Dictionary<Shader, MaterialEditorDescriptor>();
        private static Dictionary<Type, EditorDescriptor> m_map = new Dictionary<Type, EditorDescriptor>();
        private static GameObject[] m_editors = new GameObject[0];
        private static bool m_isLoaded = false;
        public static void Reset()
        {
            if(!m_isLoaded)
            {
                return;
            }
            m_materialMap = new Dictionary<Shader, MaterialEditorDescriptor>();
            m_map = new Dictionary<Type, EditorDescriptor>();
            m_editors = new GameObject[0];
            m_defaultMaterialEditor = null;
            m_isLoaded = false;
        }

        public static void LoadMap()
        {
            if(m_isLoaded)
            {
                return;
            }
            m_isLoaded = true;

            InitEditorsMap();

            EditorsMapStorage editorsMap = Resources.Load<EditorsMapStorage>(EditorsMapStorage.EditorsMapPrefabName);
            if (editorsMap != null)
            {
                m_editors = editorsMap.Editors;
                
                for(int i = 0; i < editorsMap.MaterialEditors.Length; ++i)
                {
                    GameObject materialEditor = editorsMap.MaterialEditors[i];
                    Shader shader = editorsMap.Shaders[i];
                    bool enabled = editorsMap.IsMaterialEditorEnabled[i];
                    if(!m_materialMap.ContainsKey(shader))
                    {
                        m_materialMap.Add(shader, new MaterialEditorDescriptor(materialEditor, enabled));
                    }
                    m_defaultMaterialEditor = editorsMap.DefaultMaterialEditor;
                }
            }
            else
            {
                Debug.LogError("Editors map is null");
            }
        }

        public static bool IsObjectEditorEnabled(Type type)
        {
            return IsEditorEnabled(type, false, true);
        }

        public static bool IsPropertyEditorEnabled(Type type)
        {
            return IsEditorEnabled(type, true, false);
        }

        private static bool IsEditorEnabled(Type type, bool isPropertyEditor, bool strict)
        {
            EditorDescriptor descriptor = GetEditorDescriptor(type, isPropertyEditor, strict);
            if (descriptor != null)
            {
                return descriptor.Enabled;
            }
            return false;
        }

        public static bool IsMaterialEditorEnabled(Shader shader)
        {
            MaterialEditorDescriptor descriptor = GetEditorDescriptor(shader);
            if (descriptor != null)
            {
                return descriptor.Enabled;
            }

            return false;
        }

        public static GameObject GetObjectEditor(Type type, bool strict = false)
        {
            return GetEditor(type, false, strict);
        }

        public static GameObject GetPropertyEditor(Type type, bool strict = false)
        {
            return GetEditor(type, true, strict);
        }

        private static GameObject GetEditor(Type type, bool isPropertyEditor, bool strict = false)
        {
            EditorDescriptor descriptor = GetEditorDescriptor(type, isPropertyEditor, strict);
            if (descriptor != null)
            {
                return m_editors[descriptor.Index];
            }
            return null;
        }

        public static GameObject GetMaterialEditor(Shader shader, bool strict = false)
        {
            MaterialEditorDescriptor descriptor = GetEditorDescriptor(shader);
            if(descriptor != null)
            {
                return descriptor.Editor;
            }

            if(strict)
            {
                return null;
            }

            return m_defaultMaterialEditor;
        }

        private static MaterialEditorDescriptor GetEditorDescriptor(Shader shader)
        {
            MaterialEditorDescriptor descriptor;
            if(m_materialMap.TryGetValue(shader, out descriptor))
            {
                return m_materialMap[shader];
            }

            return null;
        }

        private static EditorDescriptor GetEditorDescriptor(Type type, bool isPropertyEditor, bool strict)
        {
            do
            {
                EditorDescriptor descriptor;


                if (m_map.TryGetValue(type, out descriptor))
                {
                    if (descriptor.IsPropertyEditor == isPropertyEditor)
                    {
                        return descriptor;
                    }
                }
                else
                {
                    if (type.IsGenericType)
                    {
                        type = type.GetGenericTypeDefinition();
                        continue;
                    }
                }

                if (strict)
                {
                    break;
                }

                type = type.BaseType();
            }
            while (type != null);
            return null;
        }
    }
}

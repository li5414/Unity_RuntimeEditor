using UnityEngine;
using UnityEditor;

using System.Linq;
using Battlehub.Utils;
using System;
using System.Reflection;
using System.Collections.Generic;

namespace Battlehub.RTEditor
{
    public class EditorDescriptor
    {
        public Type Type;
        public bool Enabled;
        public GameObject Editor;
        public bool IsPropertyEditor;

        public EditorDescriptor(Type type, bool enabled, GameObject editor, bool isPropertyEditor)
        {
            Type = type;
            Enabled = enabled;
            Editor = editor;
            IsPropertyEditor = isPropertyEditor;
        }
    }

    public class MaterialEditorDescriptor
    {
        public Shader Shader;
        public bool Enabled;
        public GameObject Editor;

        public MaterialEditorDescriptor(Shader shader, bool enabled, GameObject editor)
        {
            Shader = shader;
            Enabled = enabled;
            Editor = editor;
        }
    }


    public class EditorsMapWindow : EditorWindow
    {
        [MenuItem("Tools/Runtime Editor/Configuration")]
        public static void ShowMenuItem()
        {
            ShowWindow();
        }

        public static void ShowWindow()
        {
            EditorsMapWindow prevWindow = GetWindow<EditorsMapWindow>();
            if (prevWindow != null)
            {
                prevWindow.Close();
            }

            EditorsMapWindow window = CreateInstance<EditorsMapWindow>();
            window.titleContent = new GUIContent("RTE Config");
            window.Show();
            window.position = new Rect(200, 200, 600, 600);
        }

        private EditorsMapStorage m_map;
        private EditorDescriptor[] m_objectEditorDescriptors;
        private EditorDescriptor[] m_propertyEditorDescriptors;
        private EditorDescriptor[] m_stdComponentEditorDescriptors;
        private EditorDescriptor[] m_scriptEditorDescriptors;
        private MaterialEditorDescriptor[] m_materialDescriptors;
        
        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            m_map = Resources.Load<EditorsMapStorage>(EditorsMapStorage.EditorsMapPrefabName);
            EditorsMap.LoadMap();

            Assembly unityAssembly = typeof(GameObject).Assembly;
            Assembly unityEditorAssembly = typeof(Editor).Assembly;
            Assembly unityUI = typeof(UnityEngine.UI.Button).Assembly;
            Assembly unityNet = typeof(UnityEngine.Networking.NetworkBehaviour).Assembly;
            Assembly yetAnotherAssembly = typeof(NetworkScenePostProcess).Assembly;
            Assembly[] otherAssemblies = AppDomain.CurrentDomain.GetAssemblies().Except(new[] { unityAssembly, unityEditorAssembly, unityUI, unityNet, yetAnotherAssembly }).ToArray();

            m_objectEditorDescriptors = new[] { typeof(GameObject) }
                .Where(t => t.IsPublic && !t.IsGenericType)
                .Select(t => new EditorDescriptor(t, m_map != null && EditorsMap.IsObjectEditorEnabled(t), m_map != null ? EditorsMap.GetObjectEditor(t, true) : null, false)).ToArray();
            m_propertyEditorDescriptors = new[] { typeof(object), typeof(UnityEngine.Object), typeof(bool), typeof(Enum), typeof(List<>), typeof(Array), typeof(string), typeof(int), typeof(float), typeof(Range), typeof(Vector2), typeof(Vector3), typeof(Vector4), typeof(Quaternion), typeof(Color), typeof(Bounds) }
                .Where(t =>  t.IsPublic)
                .Select(t => new EditorDescriptor(t, m_map != null && EditorsMap.IsPropertyEditorEnabled(t), m_map != null ? EditorsMap.GetPropertyEditor(t, true) : null, true)).ToArray();
            m_stdComponentEditorDescriptors = unityAssembly.GetTypes()
                .Where(t => typeof(Component).IsAssignableFrom(t) && t.IsPublic && !t.IsGenericType)
                .OrderBy(t => (t == typeof(Component)) ? string.Empty : t.Name)
                .Select(t => new EditorDescriptor(t, m_map != null && EditorsMap.IsObjectEditorEnabled(t), m_map != null ? EditorsMap.GetObjectEditor(t, true) : null, false)).ToArray();
            m_scriptEditorDescriptors = otherAssemblies.SelectMany(a => a.GetTypes())
                .Where(t => typeof(MonoBehaviour).IsAssignableFrom(t) && t.IsPublic && !t.IsGenericType)
                .OrderBy(t => t.FullName)
                .Select(t => new EditorDescriptor(t, m_map != null && EditorsMap.IsObjectEditorEnabled(t), m_map != null ? EditorsMap.GetObjectEditor(t, true) : null, false)).ToArray();

            List<Material> materials = new List<Material>();
            string[] assets = AssetDatabase.GetAllAssetPaths();
            foreach(string asset in assets)
            {
                if(!asset.EndsWith(".mat"))
                {
                    continue;
                }
                Material material = AssetDatabase.LoadAssetAtPath<Material>(asset);

                if (material == null ||
                   (material.hideFlags & HideFlags.DontSaveInBuild) != 0 ||
                    material.shader == null ||
                   (material.shader.hideFlags & HideFlags.DontSaveInBuild) != 0)
                {
                    continue;
                }
                materials.Add(material);

            }
            MaterialEditorDescriptor[] defaultDescriptors = new[] { new MaterialEditorDescriptor(null, m_map != null && m_map.IsDefaultMaterialEditorEnabled, m_map != null ? m_map.DefaultMaterialEditor : null) };
            MaterialEditorDescriptor[] materialDescriptors = materials.Where(m => m.shader != null && !m.shader.name.StartsWith("Hidden/"))
                .Select(m => m.shader).Distinct()
                .OrderBy(s => s.name.StartsWith("Standard") ? string.Empty : s.name)
                .Select(s => new MaterialEditorDescriptor(s, m_map != null && EditorsMap.IsMaterialEditorEnabled(s), m_map != null ? EditorsMap.GetMaterialEditor(s, true) : null)).ToArray();

            m_materialDescriptors = defaultDescriptors.Union(materialDescriptors).ToArray();
        }

        private bool m_objectsGroup = true;
        private bool m_propertiesGroup = true;
        private bool m_stdComponentsGroup = true;
        private bool m_scriptComponentsGroup = true;
        private bool m_materialsGroup = true;
        private Vector2 m_position;
        private void OnGUI()
        {
            if (m_objectEditorDescriptors == null || m_propertyEditorDescriptors == null || m_stdComponentEditorDescriptors == null || m_scriptEditorDescriptors == null)
            {
                Init();
            }

            EditorGUILayout.Separator();
            GUILayout.Label("This window allows you to manage mappings between object types and editors");
            EditorGUILayout.Separator();
            if (GUILayout.Button("Save Editors Map"))
            {
                if (Application.isPlaying)
                {
                    EditorUtility.DisplayDialog("Unable to create Editors Map", "Application.isPlaying == true", "OK");
                }
                else
                {
                    EditorsMapGen.Generate(m_objectEditorDescriptors
                        .Union(m_propertyEditorDescriptors)
                        .Union(m_stdComponentEditorDescriptors)
                        .Union(m_scriptEditorDescriptors).ToArray(), 
                        m_materialDescriptors);
                    Close();
                }
            }
            EditorGUILayout.Separator();
            m_position = EditorGUILayout.BeginScrollView(m_position);
            ShowGroup(ref m_objectsGroup, "Object Editors", m_objectEditorDescriptors, d => d.Type.Name);
            EditorGUILayout.Separator();
            ShowGroup(ref m_propertiesGroup, "Property Editors", m_propertyEditorDescriptors, d =>
            {
                if(d.Type == typeof(object))
                {
                    return "Custom Type";
                }
                return d.Type.Name;
            });
            EditorGUILayout.Separator();
            ShowGroup(ref m_materialsGroup, "Material Editors", m_materialDescriptors, d => d.Shader != null ? d.Shader.name : "Default");
            EditorGUILayout.Separator();
            ShowGroup(ref m_stdComponentsGroup, "Standard Component Editors", m_stdComponentEditorDescriptors, d => d.Type.Name);
            EditorGUILayout.Separator();
            ShowGroup(ref m_scriptComponentsGroup, "Script Editors", m_scriptEditorDescriptors, d => d.Type.FullName);
            EditorGUILayout.EndScrollView();
        }

        private void ShowGroup(ref bool isExpanded, string label, EditorDescriptor[] descriptors, Func<EditorDescriptor, string> getName)
        {
            EditorGUIUtility.labelWidth = 350;
            isExpanded = EditorGUILayout.Foldout(isExpanded, label);
            if (isExpanded)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical();
                for (int i = 0; i < descriptors.Length; ++i)
                {
                    EditorDescriptor descriptor = descriptors[i];
                    descriptor.Enabled = EditorGUILayout.Toggle(getName(descriptor), descriptor.Enabled);
                }
                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginVertical();
                for (int i = 0; i < descriptors.Length; ++i)
                {
                    EditorDescriptor descriptor = descriptors[i];
                    descriptor.Editor = (GameObject)EditorGUILayout.ObjectField(descriptor.Editor, typeof(GameObject), false);
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();
                EditorGUI.indentLevel--;
            }

            
            EditorGUIUtility.labelWidth = 0;
        }

        private void ShowGroup(ref bool isExpanded, string label, MaterialEditorDescriptor[] descriptors, Func<MaterialEditorDescriptor, string> getName)
        {
            EditorGUIUtility.labelWidth = 350;
            isExpanded = EditorGUILayout.Foldout(isExpanded, label);
            if (isExpanded)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical();
                for (int i = 0; i < descriptors.Length; ++i)
                {
                    MaterialEditorDescriptor descriptor = descriptors[i];
                    descriptor.Enabled = EditorGUILayout.Toggle(getName(descriptor), descriptor.Enabled);
                }
                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginVertical();
                for (int i = 0; i < descriptors.Length; ++i)
                {
                    MaterialEditorDescriptor descriptor = descriptors[i];
                    descriptor.Editor = (GameObject)EditorGUILayout.ObjectField(descriptor.Editor, typeof(GameObject), false);
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();
                EditorGUI.indentLevel--;
            }


            EditorGUIUtility.labelWidth = 0;
        }
    }

}

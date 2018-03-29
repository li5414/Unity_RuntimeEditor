using UnityEngine;
using UnityEditor;

using System.Linq;
using System;
using System.Reflection;
using System.Collections.Generic;

namespace Battlehub.RTSaveLoad
{
    public class SaveLoadConfigWindow : EditorWindow
    {
        //[MenuItem("Tools/Runtime SaveLoad/Configuration")]
        public static void ShowMenuItem()
        {
            ShowWindow();
        }

        public static void ShowWindow()
        {
            SaveLoadConfigWindow prevWindow = GetWindow<SaveLoadConfigWindow>();
            if (prevWindow != null)
            {
                prevWindow.Close();
            }

            SaveLoadConfigWindow window = CreateInstance<SaveLoadConfigWindow>();
            window.titleContent = new GUIContent("RT SaveLoad");
            window.Show();
            window.position = new Rect(200, 200, 600, 600);
        }

        
        private Type[] m_stdComponentTypes;
        private Type[] m_scriptComponentTypes;
        private HashSet<Type> m_disabledTypes;
        
        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            Assembly unityAssembly = typeof(GameObject).Assembly;
            Assembly unityEditorAssembly = typeof(Editor).Assembly;
            Assembly unityUI = typeof(UnityEngine.UI.Button).Assembly;
            Assembly unityNet = typeof(UnityEngine.Networking.NetworkBehaviour).Assembly;
            Assembly yetAnotherAssembly = typeof(NetworkScenePostProcess).Assembly;
            Assembly[] otherAssemblies = AppDomain.CurrentDomain.GetAssemblies().Except(new[] { unityAssembly, unityEditorAssembly, unityUI, unityNet, yetAnotherAssembly }).ToArray();


            m_stdComponentTypes = unityAssembly.GetTypes().Union(unityUI.GetTypes())
                .Where(t => typeof(Component).IsAssignableFrom(t) && t.IsPublic && !t.IsGenericType)
                .OrderBy(t => (t == typeof(Component)) ? string.Empty : t.Name).ToArray();

            m_scriptComponentTypes = otherAssemblies.SelectMany(a => a.GetTypes())
                .Where(t => typeof(MonoBehaviour).IsAssignableFrom(t) && t.IsPublic && !t.IsGenericType)
                .OrderBy(t => t.FullName).ToArray();

            m_disabledTypes = new HashSet<Type>(SaveLoadConfig.DisabledComponentTypes);
        }

        private bool m_stdComponentsGroup = true;
        private bool m_scriptComponentsGroup = true;
        private Vector2 m_position;
        private void OnGUI()
        {
            if (m_stdComponentTypes == null || m_scriptComponentTypes == null || m_disabledTypes == null)
            {
                Init();
            }

            EditorGUILayout.Separator();
            GUILayout.Label("This window allows you to disable save & load of selected components");
            EditorGUILayout.Separator();
            GUILayout.BeginHorizontal();
 
            if (GUILayout.Button("Save"))
            {
                if (Application.isPlaying)
                {
                    EditorUtility.DisplayDialog("Unable to create Editors Map", "Application.isPlaying == true", "OK");
                }
                else
                {
                    SaveLoadConfigGen.Generate(m_disabledTypes.ToArray());
                    Close();
                }
            }

            GUILayout.EndHorizontal();
            EditorGUILayout.Separator();
            m_position = EditorGUILayout.BeginScrollView(m_position);
            ShowGroup(ref m_stdComponentsGroup, "Standard Components", m_stdComponentTypes, t => t.Name);
            EditorGUILayout.Separator();
            ShowGroup(ref m_scriptComponentsGroup, "Scripts", m_scriptComponentTypes, t => t.FullName);
            EditorGUILayout.EndScrollView();
        }

        private void ShowGroup(ref bool isExpanded, string label, Type[] types, Func<Type, string> getName)
        {
            EditorGUIUtility.labelWidth = 350;
            isExpanded = EditorGUILayout.Foldout(isExpanded, label);
            if (isExpanded)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical();
                for (int i = 0; i < types.Length; ++i)
                {
                    EditorGUI.BeginChangeCheck();
                    bool check = EditorGUILayout.Toggle(getName(types[i]), m_disabledTypes.Contains(types[i]));
                    if(EditorGUI.EndChangeCheck())
                    {
                        if(check)
                        {
                            if(!m_disabledTypes.Contains(types[i]))
                            {
                                m_disabledTypes.Add(types[i]);
                            }
                        }
                        else
                        {
                            m_disabledTypes.Remove(types[i]);
                        }
                    }
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();
                EditorGUI.indentLevel--;
            }

            EditorGUIUtility.labelWidth = 0;
        }
    }

}

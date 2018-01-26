using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEditor;
using System.Reflection;
using System.Linq;
using System;
using UnityObject = UnityEngine.Object;
using System.Collections.Generic;

namespace Battlehub.RTSaveLoad2
{
    public class PersistentObjectMapperWindow : EditorWindow
    {
    
        [MenuItem("Tools/Runtime SaveLoad2/Persistent Objects Mapper")]
        public static void ShowMenuItem()
        {
            ShowWindow();
        }

        public static void ShowWindow()
        {
            PersistentObjectMapperWindow prevWindow = GetWindow<PersistentObjectMapperWindow>();
            if (prevWindow != null)
            {
                prevWindow.Close();
            }

            PersistentObjectMapperWindow window = CreateInstance<PersistentObjectMapperWindow>();
            window.titleContent = new GUIContent("Persistent Objects Mapper");
            window.Show();
            window.position = new Rect(200, 200, 600, 600);
        }

        public const string EditorPrefabsPath = @"/" + BHPath.Root + @"/RTSaveLoad2/Editor/Prefabs";
        public const string ObjectMappingsStoragePath = "Assets" + EditorPrefabsPath + @"/ObjectMappingsStorage.prefab";
        private Assembly[] m_assemblies;
        private string[] m_assemblyNames;
        private int m_selectedAssemblyIndex;
        private string m_filterText = string.Empty;

        private Vector2 m_scrollPosition;
        private Type[] m_uoTypes;
        private Type[] m_mostImportantTypes =
        {
            typeof(GameObject),
            typeof(MeshRenderer),
            typeof(MeshFilter),
            typeof(SkinnedMeshRenderer),
            typeof(Mesh),
            typeof(Material),
            typeof(Rigidbody),
            typeof(BoxCollider),
            typeof(SphereCollider),
            typeof(CapsuleCollider),
            typeof(MeshCollider),
            typeof(Camera),
            typeof(AudioClip),
            typeof(AudioSource),
            typeof(Light),
        };

        private Dictionary<Type, int> m_typeToIndex;

        private bool IsAllSelected
        {
            get { return m_selectedCount == m_isUOTypeSelected.Length; }
        }

        private bool IsNoneSelected
        {
            get { return m_selectedCount == 0; }
        }
   
        private int m_selectedCount;
        private bool[] m_isUOTypeSelected;
        private bool[] m_isUOTypeExpanded;
        private bool[][] m_isUOTypeParentExpanded;
        private int[] m_uoTypeExpandedCounter;
        private int[] m_filteredTypeIndices;

        private PersistentPropertyMapping[][] m_propertyMappings;
        private bool[][] m_isPropertyMappingsEnabled;
        private string[][] m_propertyMappingNames;
        private int[][] m_propertyMappingsSelection;

        private void Awake()
        {
            m_assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.Contains("UnityEngine")).OrderBy(a => a.FullName).ToArray();

            List<Type> allUOTypes = new List<Type>();
            List<string> assemblyNames = new List<string> { "All" };
            List<Assembly> assemblies = new List<Assembly>() { null };

            for(int i = 0; i < m_assemblies.Length; ++i)
            {
                Assembly assembly = m_assemblies[i];
                Type[] uoTypes = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(UnityObject))).ToArray();
                if(uoTypes.Length > 0)
                {
                    assemblies.Add(assembly);
                    assemblyNames.Add(assembly.GetName().Name);
                    allUOTypes.AddRange(uoTypes);
                }
            }

            for (int i = 0; i < m_mostImportantTypes.Length; ++i)
            {
                allUOTypes.Remove(m_mostImportantTypes[i]);
            }

            m_uoTypes = m_mostImportantTypes.Union(allUOTypes.OrderBy(t => t.Name)).ToArray();
            m_propertyMappings = new PersistentPropertyMapping[m_uoTypes.Length][];
            m_isPropertyMappingsEnabled = new bool[m_uoTypes.Length][];
            m_propertyMappingNames = new string[m_uoTypes.Length][];
            m_propertyMappingsSelection = new int[m_uoTypes.Length][];
            m_isUOTypeExpanded = new bool[m_uoTypes.Length];
            m_isUOTypeParentExpanded = new bool[m_uoTypes.Length][];
            m_uoTypeExpandedCounter = new int[m_uoTypes.Length];
            m_isUOTypeSelected = new bool[m_uoTypes.Length];
            m_typeToIndex = new Dictionary<Type, int>();
            m_filteredTypeIndices = new int[m_uoTypes.Length];
            for (int i = 0; i < m_filteredTypeIndices.Length; ++i)
            {
                m_filteredTypeIndices[i] = i;
                m_typeToIndex.Add(m_uoTypes[i], i);
                m_isUOTypeParentExpanded[i] = new bool[GetAncestorsCount(m_uoTypes[i])];
            }

            m_assemblies = assemblies.ToArray();
            m_assemblyNames = assemblyNames.ToArray();
            

            //m_map = Resources.Load<EditorsMapStorage>(EditorsMapStorage.EditorsMapPrefabName);
            //EditorsMap.LoadMap();
        }

        private void OnGUI()
        {
            EditorGUILayout.Separator();
            EditorGUI.BeginChangeCheck();

            //EditorStyles.foldout.normal.textColor = Color.red;

            m_selectedAssemblyIndex = EditorGUILayout.Popup("Assembly", m_selectedAssemblyIndex, m_assemblyNames);
            m_filterText = EditorGUILayout.TextField("Type Filter:", m_filterText);

            if (EditorGUI.EndChangeCheck())
            {
                List<int> filteredTypeIndices = new List<int>();
                for (int i = 0; i < m_uoTypes.Length; ++i)
                {
                    Type type = m_uoTypes[i];
                    if (type.Name.ToLower().Contains(m_filterText.ToLower()) && (type.Assembly == m_assemblies[m_selectedAssemblyIndex] || m_selectedAssemblyIndex == 0))
                    {
                        filteredTypeIndices.Add(i);
                    }
                }
                m_filteredTypeIndices = filteredTypeIndices.ToArray();

            }

            EditorGUI.BeginChangeCheck();

            if(IsAllSelected)
            {
                GUILayout.Toggle(true, "Select All");
            }
            else if(IsNoneSelected)
            {
                GUILayout.Toggle(false, "Select All");
            }
            else
            {
                GUILayout.Toggle(false, "Select All", "ToggleMixed");
            }
            
            if(EditorGUI.EndChangeCheck())
            {
                if (IsAllSelected)
                {
                    for(int i = 0; i < m_isUOTypeSelected.Length; ++i)
                    {
                        m_isUOTypeSelected[i] = false;
                        ExpandOrCollapseType(i);
                    }
                    m_selectedCount = 0;
                }
                else
                {
                    for (int i = 0; i < m_isUOTypeSelected.Length; ++i)
                    {
                        m_isUOTypeSelected[i] = true;
                        ExpandOrCollapseType(i);
                    }
                    m_selectedCount = m_isUOTypeSelected.Length;
                }
            }

            EditorGUILayout.Separator();
            m_scrollPosition = EditorGUILayout.BeginScrollView(m_scrollPosition);

            EditorGUILayout.BeginVertical();
            {
                for (int i = 0; i < m_filteredTypeIndices.Length; ++i)
                {
                    int typeIndex = m_filteredTypeIndices[i];
                    DrawTypeEditor(typeIndex, typeIndex);
                }
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndScrollView();

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();
            GUILayout.Button("Create Persistent Objects");
            if(EditorGUI.EndChangeCheck())
            {
                SaveMappings();
            }

            EditorGUILayout.EndHorizontal();
        }

        private void SaveMappings()
        {
            GameObject storageGO = new GameObject();
            for(int typeIndex = 0; typeIndex < m_propertyMappings.GetLength(0); ++typeIndex)
            {
                if(!m_isUOTypeSelected[typeIndex])
                {
                    continue;
                }

                PersistentPropertyMapping[] propertyMappings = m_propertyMappings[typeIndex];
                GameObject typeStorageGO = new GameObject();
                typeStorageGO.transform.SetParent(storageGO.transform, false);
                typeStorageGO.name = m_uoTypes[typeIndex].FullName;
                PersistentObjectMapping objectMapping = typeStorageGO.AddComponent<PersistentObjectMapping>();

                List<PersistentPropertyMapping> selectedPropertyMappings = new List<PersistentPropertyMapping>();
                for(int i = 0; i < propertyMappings.Length; ++i)
                {
                    if(!m_isPropertyMappingsEnabled[typeIndex][i])
                    {
                        continue;
                    }

                    selectedPropertyMappings.Add(propertyMappings[i]);
                }

                objectMapping.PropertyMappings = selectedPropertyMappings.ToArray();
                objectMapping.MappedNamespace = m_uoTypes[typeIndex].Namespace;
                objectMapping.MappedTypeName = m_uoTypes[typeIndex].Name;
                objectMapping.PersistentNamespace = PersistentObjectMapping.ToPersistentNamespace(objectMapping.MappedNamespace);
                objectMapping.PersistentTypeName = m_uoTypes[typeIndex].Name;
            }

            PrefabUtility.CreatePrefab(ObjectMappingsStoragePath, storageGO);
            DestroyImmediate(storageGO);
        }

        private int GetAncestorsCount(Type type)
        {
            int count = 0;
            while(type.BaseType != typeof(UnityObject))
            {
                count++;
                type = type.BaseType;
            }
            return count;
        }

        private void DrawTypeEditor(int rootTypeIndex, int typeIndex, int indent = 1)
        {
            Type type = m_uoTypes[typeIndex];
            string label = type.Name;

            bool isExpandedChanged;
            bool isExpanded;
            bool isSelectionChanged;
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUI.BeginChangeCheck();
                m_isUOTypeSelected[typeIndex] = EditorGUILayout.Toggle(m_isUOTypeSelected[typeIndex], GUILayout.Width(indent * 15));
                isSelectionChanged = EditorGUI.EndChangeCheck();
               
                EditorGUI.BeginChangeCheck();
                if (indent == 1)
                {
                    m_isUOTypeExpanded[typeIndex] = EditorGUILayout.Foldout(m_isUOTypeExpanded[typeIndex], label, true);
                    isExpanded = m_isUOTypeExpanded[typeIndex];
                }
                else
                {
                    m_isUOTypeParentExpanded[rootTypeIndex][indent - 2] = EditorGUILayout.Foldout(m_isUOTypeParentExpanded[rootTypeIndex][indent - 2], label, true);
                    isExpanded = m_isUOTypeParentExpanded[rootTypeIndex][indent - 2];
                }
                isExpandedChanged = EditorGUI.EndChangeCheck();
            }
            EditorGUILayout.EndHorizontal();

            if (isExpandedChanged || isSelectionChanged)
            {
                if (isExpandedChanged)
                {
                    m_uoTypeExpandedCounter[typeIndex] = isExpanded ?
                        m_uoTypeExpandedCounter[typeIndex] + 1 :
                        m_uoTypeExpandedCounter[typeIndex] - 1;
                }

                if (isSelectionChanged)
                {
                    if (m_isUOTypeSelected[typeIndex])
                    {
                        m_selectedCount++;
                    }
                    else
                    {
                        m_selectedCount--;
                    }
                }

                ExpandOrCollapseType(typeIndex);
            }

            if (isExpanded)
            {
                //Show parents expanders for each object
                EditorGUI.indentLevel++;
                //EditorGUILayout.Separator();
                EditorGUILayout.BeginVertical();
                {
                    for (int p = 0; p < m_propertyMappings[typeIndex].Length; ++p)
                    {
                        EditorGUILayout.BeginHorizontal();
                        {
                            PersistentPropertyMapping pMapping = m_propertyMappings[typeIndex][p];

                            m_isPropertyMappingsEnabled[typeIndex][p] = EditorGUILayout.Toggle(m_isPropertyMappingsEnabled[typeIndex][p], GUILayout.Width(18 + indent * 12));
                            //EditorGUIUtility.labelWidth = 20;
                            EditorGUILayout.LabelField(pMapping.PersistentName, GUILayout.Width(188 + indent * 15));
                            //EditorGUIUtility.labelWidth = 0;
                            m_propertyMappingsSelection[typeIndex][p] = EditorGUILayout.Popup(m_propertyMappingsSelection[typeIndex][p], m_propertyMappingNames[typeIndex]);
                        }
                        EditorGUILayout.EndHorizontal();
                    }

                    if (type.BaseType != typeof(UnityObject))
                    {
                        int parentIndex;
                        if (m_typeToIndex.TryGetValue(type.BaseType, out parentIndex))
                        {
                            DrawTypeEditor(rootTypeIndex, parentIndex, indent + 1);
                        }
                    }
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.Separator();
                EditorGUI.indentLevel--;
            }
        }

        private void ExpandOrCollapseType(int typeIndex)
        {
            if (m_uoTypeExpandedCounter[typeIndex] > 0 || m_isUOTypeSelected[typeIndex])
            {
                ExpandType(typeIndex);
            }
            else
            {
                CollapseType(typeIndex);
            }
        }

        private void CollapseType(int typeIndex)
        {
            m_propertyMappings[typeIndex] = null;
            m_isPropertyMappingsEnabled[typeIndex] = null;
            m_propertyMappingNames[typeIndex] = null;
            m_propertyMappingsSelection[typeIndex] = null;
        }

        private void ExpandType(int typeIndex)
        {
            Type type = m_uoTypes[typeIndex];
            List<PersistentPropertyMapping> pMappings = new List<PersistentPropertyMapping>();
            List<bool> pMappingsEnabled = new List<bool>();
            FieldInfo[] fields = type.GetFields();
            for (int f = 0; f < fields.Length; ++f)
            {
                FieldInfo fInfo = fields[f];
                PersistentPropertyMapping pMapping = new PersistentPropertyMapping();
                pMapping.PersistentName = fInfo.Name;
                pMapping.PersistentType = fInfo.FieldType.FullName;
                pMapping.MappedName = fInfo.Name;
                pMapping.MappedType = fInfo.FieldType.FullName;
                pMapping.IsProperty = false;
                pMapping.IsMappedProperty = false;

                pMapping.UseBuiltInCodeSnippet = false;
                pMapping.BuiltInCodeSnippet = null;

                pMappings.Add(pMapping);
                pMappingsEnabled.Add(false);
            }

            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).Where(p => p.GetGetMethod() != null && p.GetSetMethod() != null).ToArray();
            for (int p = 0; p < properties.Length; ++p)
            {
                PropertyInfo pInfo = properties[p];
                PersistentPropertyMapping pMapping = new PersistentPropertyMapping();
                pMapping.PersistentName = pInfo.Name;       //property name of mapping
                pMapping.PersistentType = pInfo.PropertyType.FullName;
                pMapping.MappedName = pInfo.Name; //property name of unity type
                pMapping.MappedType = pInfo.PropertyType.FullName;
                pMapping.IsProperty = true;
                pMapping.IsMappedProperty = true;

                pMapping.UseBuiltInCodeSnippet = false;
                pMapping.BuiltInCodeSnippet = null;

                pMappings.Add(pMapping);
                pMappingsEnabled.Add(true);
            }

            m_propertyMappings[typeIndex] = pMappings.ToArray();
            m_isPropertyMappingsEnabled[typeIndex] = pMappingsEnabled.ToArray();
            m_propertyMappingNames[typeIndex] = pMappings.Select(m => m.MappedName).ToArray();
            m_propertyMappingsSelection[typeIndex] = new int[pMappings.Count];
            for (int i = 0; i < m_propertyMappingsSelection[typeIndex].Length; ++i)
            {
                m_propertyMappingsSelection[typeIndex][i] = i;
            }
        }
    }
}

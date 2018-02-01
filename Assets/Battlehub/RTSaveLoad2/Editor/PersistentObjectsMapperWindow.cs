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
        public const string ObjectMappingsTemplatePath = "Assets" + EditorPrefabsPath + @"/ObjectMappingsTemplate.prefab";
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
            get { return m_selectedCount == m_mappings.Length; }
        }

        private bool IsNoneSelected
        {
            get { return m_selectedCount == 0; }
        }

        private int m_selectedCount;
        private int[] m_filteredTypeIndices;

        private class ObjectMappingInfo
        {
            public bool IsSelected;
            public bool IsExpanded;
            public bool[] IsParentExpanded;
            public int ExpandedCounter;
            public PersistentPropertyMapping[] PropertyMappings;
            public bool[] IsPropertyMappingEnabled;
            public string[] PropertyMappingNames;
            public string[] PropertyMappingTypes;
            public int[] PropertyMappingSelection;
        }

        private ObjectMappingInfo[] m_mappings;

        private void Awake()
        {
            m_assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.Contains("UnityEngine")).OrderBy(a => a.FullName).ToArray();

            List<Type> allUOTypes = new List<Type>();
            List<string> assemblyNames = new List<string> { "All" };
            List<Assembly> assemblies = new List<Assembly>() { null };

            for (int i = 0; i < m_assemblies.Length; ++i)
            {
                Assembly assembly = m_assemblies[i];
                Type[] uoTypes = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(UnityObject))).ToArray();
                if (uoTypes.Length > 0)
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

            m_mappings = new ObjectMappingInfo[m_uoTypes.Length];
            for(int i = 0; i < m_uoTypes.Length; ++i)
            {
                m_mappings[i] = new ObjectMappingInfo();
            }

            m_typeToIndex = new Dictionary<Type, int>();
            m_filteredTypeIndices = new int[m_uoTypes.Length];
            for (int i = 0; i < m_filteredTypeIndices.Length; ++i)
            {
                m_filteredTypeIndices[i] = i;
                m_typeToIndex.Add(m_uoTypes[i], i);
                m_mappings[i].IsParentExpanded = new bool[GetAncestorsCount(m_uoTypes[i])];
            }

            m_assemblies = assemblies.ToArray();
            m_assemblyNames = assemblyNames.ToArray();

            LoadMappings();

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

            if (IsAllSelected)
            {
                GUILayout.Toggle(true, "Select All");
            }
            else if (IsNoneSelected)
            {
                GUILayout.Toggle(false, "Select All");
            }
            else
            {
                GUILayout.Toggle(false, "Select All", "ToggleMixed");
            }

            if (EditorGUI.EndChangeCheck())
            {
                if (IsAllSelected)
                {
                    UnselectAll();
                }
                else
                {
                    SelectAll();
                }
            }

            EditorGUILayout.Separator();

            EditorGUI.BeginChangeCheck();
            GUILayout.Button("Reset");
            if (EditorGUI.EndChangeCheck())
            {
                UnselectAll();
                LoadMappings();
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
            if (EditorGUI.EndChangeCheck())
            {
                SaveMappings();
            }

            EditorGUILayout.EndHorizontal();
        }

        private void UnselectAll()
        {
            for (int i = 0; i < m_mappings.Length; ++i)
            {
                m_mappings[i].IsSelected = false;
                TryExpandType(i);

                if(m_mappings[i].IsPropertyMappingEnabled != null)
                {
                    for (int j = 0; j < m_mappings[i].IsPropertyMappingEnabled.Length; ++j)
                    {
                        m_mappings[i].IsPropertyMappingEnabled[j] = false;
                    }
                }
            }
            m_selectedCount = 0;
        }

        private void SelectAll()
        {
            for (int i = 0; i < m_mappings.Length; ++i)
            {
                m_mappings[i].IsSelected = true;
                TryExpandType(i);
                for (int j = 0; j < m_mappings[i].IsPropertyMappingEnabled.Length; ++j)
                {
                    m_mappings[i].IsPropertyMappingEnabled[j] = true;
                }
            }
            m_selectedCount = m_mappings.Length;
        }

        private void LoadMappings()
        {
            GameObject storageGO = (GameObject)AssetDatabase.LoadAssetAtPath(ObjectMappingsStoragePath, typeof(GameObject));
            if(storageGO == null)
            {
                storageGO = (GameObject)AssetDatabase.LoadAssetAtPath(ObjectMappingsTemplatePath, typeof(GameObject));
            }

            if (storageGO != null)
            {
                PersistentObjectMapping[] mappings = storageGO.GetComponentsInChildren<PersistentObjectMapping>(true);
                for (int i = 0; i < mappings.Length; ++i)
                {
                    PersistentObjectMapping objectMapping = mappings[i];
                    Type type = Type.GetType(objectMapping.MappedAssemblyQualifiedName);
                    int typeIndex;
                    if (type != null && m_typeToIndex.TryGetValue(type, out typeIndex))
                    {
                        PersistentPropertyMapping[] pMappings = objectMapping.PropertyMappings;
                        m_mappings[typeIndex].PropertyMappings = pMappings;
                        m_mappings[typeIndex].IsSelected = true;
                        m_selectedCount++;
                        ExpandType(typeIndex);
                    }
                }
            }
        }

        private void SaveMappings()
        {
            GameObject storageGO = new GameObject();
            for (int typeIndex = 0; typeIndex < m_mappings.Length; ++typeIndex)
            {
                if (!m_mappings[typeIndex].IsSelected)
                {
                    continue;
                }

                PersistentPropertyMapping[] propertyMappings = m_mappings[typeIndex].PropertyMappings;
                int[] propertyMappingsSelection = m_mappings[typeIndex].PropertyMappingSelection;

                GameObject typeStorageGO = new GameObject();
            
                typeStorageGO.transform.SetParent(storageGO.transform, false);
                typeStorageGO.name = m_uoTypes[typeIndex].FullName;
                PersistentObjectMapping objectMapping = typeStorageGO.AddComponent<PersistentObjectMapping>();

                List<PersistentPropertyMapping> selectedPropertyMappings = new List<PersistentPropertyMapping>();
                for (int i = 0; i < propertyMappings.Length; ++i)
                {
                    if (!m_mappings[typeIndex].IsPropertyMappingEnabled[i])
                    {
                        continue;
                    }

                    PersistentPropertyMapping propertyMapping = propertyMappings[i];
                    if (propertyMappingsSelection[i] >= 0)
                    {
                        propertyMapping.MappedName = m_mappings[typeIndex].PropertyMappingNames[propertyMappingsSelection[i]];
                        propertyMapping.MappedType = m_mappings[typeIndex].PropertyMappingTypes[propertyMappingsSelection[i]];
                    }
                    else
                    {
                        propertyMapping.MappedName = null;
                        propertyMapping.MappedType = null;
                    }
                 
                    selectedPropertyMappings.Add(propertyMapping);
                }

                objectMapping.PropertyMappings = selectedPropertyMappings.ToArray();
                objectMapping.MappedAssemblyName = m_uoTypes[typeIndex].Assembly.FullName.Split(',')[0];
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
            while (type.BaseType != typeof(UnityObject))
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
                m_mappings[typeIndex].IsSelected = EditorGUILayout.Toggle(m_mappings[typeIndex].IsSelected, GUILayout.Width(indent * 15));
                isSelectionChanged = EditorGUI.EndChangeCheck();

                EditorGUI.BeginChangeCheck();
                if (indent == 1)
                {
                    m_mappings[typeIndex].IsExpanded = EditorGUILayout.Foldout(m_mappings[typeIndex].IsExpanded, label, true);
                    isExpanded = m_mappings[typeIndex].IsExpanded;
                }
                else
                {
                    m_mappings[rootTypeIndex].IsParentExpanded[indent - 2] = EditorGUILayout.Foldout(m_mappings[rootTypeIndex].IsParentExpanded[indent - 2], label, true);
                    isExpanded = m_mappings[rootTypeIndex].IsParentExpanded[indent - 2];
                }
                isExpandedChanged = EditorGUI.EndChangeCheck();
            }
            EditorGUILayout.EndHorizontal();

            if (isExpandedChanged || isSelectionChanged)
            {
                if (isExpandedChanged)
                {
                    m_mappings[typeIndex].ExpandedCounter = isExpanded ?
                        m_mappings[typeIndex].ExpandedCounter + 1 :
                        m_mappings[typeIndex].ExpandedCounter - 1;
                }

                if (isSelectionChanged)
                {
                    if (m_mappings[typeIndex].IsSelected)
                    {
                        m_selectedCount++;
                    }
                    else
                    {
                        m_selectedCount--;
                    }
                }

                TryExpandType(typeIndex);
            }

            if (isExpanded)
            {
                //Show parents expanders for each object
                EditorGUI.indentLevel++;
                //EditorGUILayout.Separator();
                EditorGUILayout.BeginVertical();
                {
                    for (int p = 0; p < m_mappings[typeIndex].PropertyMappings.Length; ++p)
                    {
                        EditorGUILayout.BeginHorizontal();
                        {
                            PersistentPropertyMapping pMapping = m_mappings[typeIndex].PropertyMappings[p];

                            m_mappings[typeIndex].IsPropertyMappingEnabled[p] = EditorGUILayout.Toggle(m_mappings[typeIndex].IsPropertyMappingEnabled[p], GUILayout.Width(18 + indent * 12));
                            //EditorGUIUtility.labelWidth = 20;
                            EditorGUILayout.LabelField(pMapping.PersistentName, GUILayout.Width(188 + indent * 15));
                            //EditorGUIUtility.labelWidth = 0;

                            //int oldPropertyIndex = m_mappings[typeIndex].PropertyMappingSelection[p];
                            int newPropertyIndex = EditorGUILayout.Popup(m_mappings[typeIndex].PropertyMappingSelection[p], m_mappings[typeIndex].PropertyMappingNames);
                          
                            //if(oldPropertyIndex != newPropertyIndex)
                            //{
                            //    for(int i = 0; i < m_mappings[typeIndex].PropertyMappingSelection.Length; ++i)
                            //    {
                            //        if(m_mappings[typeIndex].PropertyMappingSelection[i] == newPropertyIndex)
                            //        {
                            //            m_mappings[typeIndex].PropertyMappingSelection[i] = -1;
                            //            break;
                            //        }
                            //    }
                            //}

                            m_mappings[typeIndex].PropertyMappingSelection[p] = newPropertyIndex;
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

        private void TryExpandType(int typeIndex)
        {
            if (m_mappings[typeIndex].PropertyMappings != null)
            {
                return;
            }
            if (m_mappings[typeIndex].ExpandedCounter > 0 || m_mappings[typeIndex].IsSelected)
            {
                ExpandType(typeIndex);
            }
        }

        private void ExpandType(int typeIndex)
        {
            Type type = m_uoTypes[typeIndex];


     
            List<PersistentPropertyMapping> pMappings = new List<PersistentPropertyMapping>();
            List<bool> pMappingsEnabled = new List<bool>();

            PersistentPropertyMapping[] fieldMappings = m_mappings[typeIndex].PropertyMappings != null ?
                m_mappings[typeIndex].PropertyMappings.Where(p => !p.IsProperty).ToArray() :
                new PersistentPropertyMapping[0];

            HashSet<string> fieldMappingsHs = new HashSet<string>();
            IEnumerable<string> fmapKeys = fieldMappings.Select(fMap => fMap.PersistentType + " " + fMap.PersistentName);
            foreach(string key in fmapKeys)
            {
                if(!fieldMappingsHs.Contains(key))
                {
                    fieldMappingsHs.Add(key);
                }
            }
         
            PersistentPropertyMapping[] propertyMappings = m_mappings[typeIndex].PropertyMappings != null ?
                m_mappings[typeIndex].PropertyMappings.Where(p => p.IsProperty).ToArray() :
                new PersistentPropertyMapping[0];

            HashSet<string> propertyMappingsHs = new HashSet<string>();
            IEnumerable<string> pmapKeys = propertyMappings.Select(pMap => pMap.PersistentType + " " + pMap.PersistentName);
            foreach (string key in pmapKeys)
            {
                if (!propertyMappingsHs.Contains(key))
                {
                    propertyMappingsHs.Add(key);
                }
            }


            FieldInfo[] fields = type.GetFields();
            HashSet<string> fieldHs = new HashSet<string>(fields.Select(fInfo => fInfo.FieldType.FullName + " " + fInfo.Name));

            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).Where(p => p.GetGetMethod() != null && p.GetSetMethod() != null).ToArray();
            HashSet<string> propertyHs = new HashSet<string>(properties.Select(pInfo => pInfo.PropertyType.FullName + " " + pInfo.Name));

            for(int i = 0; i < fieldMappings.Length; ++i)
            {
                PersistentPropertyMapping mapping = fieldMappings[i];
                string key = mapping.MappedType + " " + mapping.MappedName;
                if(!fieldHs.Contains(key))
                {
                    mapping.MappedName = null;
                    mapping.MappedType = null;

                    pMappingsEnabled.Add(false);
                }
                else
                {
                    pMappingsEnabled.Add(true);
                }

                pMappings.Add(mapping);
            }


            for (int f = 0; f < fields.Length; ++f)
            {
                FieldInfo fInfo = fields[f];

                string key = fInfo.FieldType.FullName + " " + fInfo.Name;

                if (fieldMappingsHs.Contains(key))
                {
                    continue;
                }

                PersistentPropertyMapping pMapping = new PersistentPropertyMapping();
                pMapping.PersistentName = fInfo.Name;
                pMapping.PersistentType = fInfo.FieldType.FullName;

                pMapping.MappedName = fInfo.Name;
                pMapping.MappedType = fInfo.FieldType.FullName;
                pMapping.IsProperty = false;

                pMapping.UseBuiltInCodeSnippet = false;
                pMapping.BuiltInCodeSnippet = null;

                pMappingsEnabled.Add(false);
                pMappings.Add(pMapping);
            }

            for (int i = 0; i < propertyMappings.Length; ++i)
            {
                PersistentPropertyMapping mapping = propertyMappings[i];
                string key = mapping.MappedType + " " + mapping.MappedName;
                if (!propertyHs.Contains(key))
                {
                    mapping.MappedName = null;
                    mapping.MappedType = null;

                    pMappingsEnabled.Add(false);
                }
                else
                {
                    pMappingsEnabled.Add(true);
                }

                pMappings.Add(mapping);
            }


            for (int p = 0; p < properties.Length; ++p)
            {
                PropertyInfo pInfo = properties[p];

                string key = pInfo.PropertyType.FullName + " " + pInfo.Name;
                if (propertyMappingsHs.Contains(key))
                {
                    continue;
                }

                PersistentPropertyMapping pMapping = new PersistentPropertyMapping();
                pMapping.PersistentName = pInfo.Name;       //property name of mapping
                pMapping.PersistentType = pInfo.PropertyType.FullName;
                pMapping.MappedName = pInfo.Name; //property name of unity type
                pMapping.MappedType = pInfo.PropertyType.FullName;
                pMapping.IsProperty = true;

                pMapping.UseBuiltInCodeSnippet = false;
                pMapping.BuiltInCodeSnippet = null;

                pMappingsEnabled.Add(false);
                pMappings.Add(pMapping);
            }

            m_mappings[typeIndex].PropertyMappings = pMappings.ToArray();
            m_mappings[typeIndex].IsPropertyMappingEnabled = pMappingsEnabled.ToArray();

            var propertyInfo = 
                fields.Select(f => new { Name = f.Name, Type = f.FieldType.FullName }).Union(properties.Select(p => new { Name = p.Name, Type = p.PropertyType.FullName })).OrderBy(p => p.Name).ToArray();

            m_mappings[typeIndex].PropertyMappingNames = propertyInfo.Select(p => p.Name).ToArray();
            m_mappings[typeIndex].PropertyMappingTypes = propertyInfo.Select(p => p.Type).ToArray();
            m_mappings[typeIndex].PropertyMappingSelection = new int[pMappings.Count];

            string[] mappedKeys = propertyInfo.Select(m => m.Type + " " + m.Name).ToArray();

            for (int i = 0; i < m_mappings[typeIndex].PropertyMappingSelection.Length; ++i)
            {
                PersistentPropertyMapping mapping = m_mappings[typeIndex].PropertyMappings[i];

                m_mappings[typeIndex].PropertyMappingSelection[i] = Array.IndexOf(mappedKeys, mapping.MappedType + " " + mapping.MappedName);
            }
        }
    }
}

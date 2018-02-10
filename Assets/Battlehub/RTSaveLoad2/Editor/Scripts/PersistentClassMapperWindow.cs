using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using UnityObject = UnityEngine.Object;

namespace Battlehub.RTSaveLoad2
{
    public class PersistentClassMapperWindow : EditorWindow
    {
        [MenuItem("Tools/Runtime SaveLoad2/Persistent Classes Mapper")]
        public static void ShowMenuItem()
        {
            ShowWindow();
        }

        public static void ShowWindow()
        {
            PersistentClassMapperWindow prevWindow = GetWindow<PersistentClassMapperWindow>();
            if (prevWindow != null)
            {
                prevWindow.Close();
            }

            PersistentClassMapperWindow window = CreateInstance<PersistentClassMapperWindow>();
            window.titleContent = new GUIContent("RTSL2 Config");
            window.Show();
            window.position = new Rect(200, 200, 600, 600);
        }

        public const string EditorPrefabsPath = @"/" + BHPath.Root + @"/RTSaveLoad2/Editor/Prefabs";
        public const string ClassMappingsStoragePath = "Assets" + EditorPrefabsPath + @"/ClassMappingsStorage.prefab";
        public const string ClassMappingsTemplatePath = "Assets" + EditorPrefabsPath + @"/ClassMappingsTemplate.prefab";
        private Assembly[] m_assemblies;
        private string[] m_assemblyNames;
        private int m_selectedAssemblyIndex;
        private string m_filterText = string.Empty;

        private Vector2 m_scrollPosition;
        private Type[] m_uoTypes;
        private Type[] m_mostImportantTypes =
        {
            typeof(UnityObject),
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

        private class ClassMappingInfo
        {
            public int PersistentPropertyTag;
            public int PersistentSubclassTag;
            public bool IsEnabled;
            public bool IsExpanded;
            public bool[] IsParentExpanded;
            public int ExpandedCounter;
            public PersistentPropertyMapping[] PropertyMappings;
            public PersistentSubclass[] Subclasses;
            public bool[] IsPropertyMappingEnabled;
            public string[][] PropertyMappingNames; //per property
            public string[][] PropertyMappingTypeNames; //per property
            public string[][] PropertyMappingNamespaces;
            public string[][] PropertyMappingAssemblyNames;
            public int[] PropertyMappingSelection;
        }

        private ClassMappingInfo[] m_mappings;

        private CodeGen m_codeGen;

        private void OnGUI()
        {
            if(m_mappings == null)
            {
                Initialize();
                LoadMappings();
            }

            EditorGUILayout.Separator();
            EditorGUI.BeginChangeCheck();

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

        private void Initialize()
        {
            m_codeGen = new CodeGen();
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

            m_mappings = new ClassMappingInfo[m_uoTypes.Length];
            for (int i = 0; i < m_uoTypes.Length; ++i)
            {
                m_mappings[i] = new ClassMappingInfo();
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
        }

        private void SelectAll()
        {
            for (int i = 0; i < m_mappings.Length; ++i)
            {
                m_mappings[i].IsEnabled = true;
                TryExpandType(i);
                for (int j = 0; j < m_mappings[i].IsPropertyMappingEnabled.Length; ++j)
                {
                    m_mappings[i].IsPropertyMappingEnabled[j] = true;
                }
            }
            m_selectedCount = m_mappings.Length;
        }

        private void UnselectAll()
        {
            for (int i = 0; i < m_mappings.Length; ++i)
            {
                m_mappings[i].IsEnabled = false;
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

        private void LoadMappings()
        {
            GameObject storageGO = (GameObject)AssetDatabase.LoadAssetAtPath(ClassMappingsStoragePath, typeof(GameObject));
            if(storageGO == null)
            {
                storageGO = (GameObject)AssetDatabase.LoadAssetAtPath(ClassMappingsTemplatePath, typeof(GameObject));
            }

            if (storageGO != null)
            {
                PersistentClassMapping[] mappings = storageGO.GetComponentsInChildren<PersistentClassMapping>(true);
                for (int i = 0; i < mappings.Length; ++i)
                {
                    PersistentClassMapping classMapping = mappings[i];
                    Type type = Type.GetType(classMapping.MappedAssemblyQualifiedName);
                    int typeIndex;
                    if (type != null && m_typeToIndex.TryGetValue(type, out typeIndex))
                    {
                        PersistentPropertyMapping[] pMappings = classMapping.PropertyMappings;
                        PersistentSubclass[] subclasses = classMapping.Subclasses;
                        m_mappings[typeIndex].PropertyMappings = pMappings;
                        m_mappings[typeIndex].Subclasses = subclasses;
                        m_mappings[typeIndex].IsEnabled = classMapping.IsEnabled;
                        m_mappings[typeIndex].PersistentPropertyTag = classMapping.PersistentPropertyTag;
                        m_mappings[typeIndex].PersistentSubclassTag = classMapping.PersistentSubclassTag;
                        m_selectedCount++;
                        ExpandType(typeIndex);
                    }
                }
            }
            ExpandType(0);
            m_mappings[0].IsEnabled = true;
        }

        private void SaveMappings()
        {
            GameObject storageGO = (GameObject)AssetDatabase.LoadAssetAtPath(ClassMappingsStoragePath, typeof(GameObject));
            if (storageGO == null)
            {
                storageGO = (GameObject)AssetDatabase.LoadAssetAtPath(ClassMappingsTemplatePath, typeof(GameObject));
            }

            Dictionary<string, PersistentClassMapping> existingMappings;
            if (storageGO != null)
            {
                storageGO = Instantiate(storageGO);
                existingMappings = storageGO.GetComponentsInChildren<PersistentClassMapping>(true).ToDictionary(m => m.name);
            }
            else
            {
                storageGO = new GameObject();
                existingMappings = new Dictionary<string, PersistentClassMapping>();
            }

            Dictionary<int, Dictionary<string, PersistentSubclass>> typeIndexToSubclasses = new Dictionary<int, Dictionary<string, PersistentSubclass>>();
            for (int typeIndex = 0; typeIndex < m_mappings.Length; ++typeIndex)
            {
                ClassMappingInfo mapping = m_mappings[typeIndex];
                if(!mapping.IsEnabled)
                {
                    continue;
                }
                Dictionary<string, PersistentSubclass> subclassDictionary;
                if(mapping.Subclasses == null)
                {
                    subclassDictionary = new Dictionary<string, PersistentSubclass>();
                }
                else
                {
                    for(int i = 0; i < mapping.Subclasses.Length; ++i)
                    {
                        PersistentSubclass subclass = mapping.Subclasses[i];
                        subclass.IsEnabled = false;
                    }

                    subclassDictionary = mapping.Subclasses.ToDictionary(s => s.FullTypeName);
                }

                typeIndexToSubclasses.Add(typeIndex, subclassDictionary);
            }

            for (int typeIndex = 0; typeIndex < m_mappings.Length; ++typeIndex)
            {
                ClassMappingInfo mapping = m_mappings[typeIndex];
                if (!mapping.IsEnabled)
                {
                    continue;
                }


                Type type = m_uoTypes[typeIndex];
                Type baseType = GetEnabledBaseType(typeIndex);
                if(baseType == null)
                {
                    continue;
                }

                int baseTypeIndex;
                if(m_typeToIndex.TryGetValue(baseType, out baseTypeIndex))
                {
                    ClassMappingInfo baseClassMapping = m_mappings[baseTypeIndex];
                    string ns = PersistentClassMapping.ToPersistentNamespace(m_uoTypes[typeIndex].Namespace);
                    string typeName = PersistentClassMapping.ToPersistentName(m_uoTypes[typeIndex].Name);
                    string fullTypeName = string.Format("{0}.{1}", ns, typeName);

                    Dictionary<string, PersistentSubclass> subclassDictionary = typeIndexToSubclasses[baseTypeIndex];
                    if(!subclassDictionary.ContainsKey(fullTypeName))
                    {
                        PersistentSubclass subclass = new PersistentSubclass();
                        subclass.IsEnabled = true;
                        subclass.Namespace = PersistentClassMapping.ToPersistentNamespace(type.Namespace);
                        subclass.TypeName = PersistentClassMapping.ToPersistentName(type.Name);
                        baseClassMapping.PersistentSubclassTag++;
                        subclass.PersistentTag = baseClassMapping.PersistentSubclassTag;

                        subclassDictionary.Add(fullTypeName, subclass);
                    }
                }
            }

            for (int typeIndex = 0; typeIndex < m_mappings.Length; ++typeIndex)
            {
                if (m_mappings[typeIndex].PropertyMappings == null)
                {
                    continue;
                }

                PersistentClassMapping classMapping;
                if (!existingMappings.TryGetValue(m_uoTypes[typeIndex].FullName, out classMapping))
                {
                    GameObject typeStorageGO = new GameObject();
                    typeStorageGO.transform.SetParent(storageGO.transform, false);
                    typeStorageGO.name = m_uoTypes[typeIndex].FullName;
                    classMapping = typeStorageGO.AddComponent<PersistentClassMapping>();
                }

                PersistentPropertyMapping[] propertyMappings = m_mappings[typeIndex].PropertyMappings;
                int[] propertyMappingsSelection = m_mappings[typeIndex].PropertyMappingSelection;
                List<PersistentPropertyMapping> selectedPropertyMappings = new List<PersistentPropertyMapping>();
                for (int propIndex = 0; propIndex < propertyMappings.Length; ++propIndex)
                {
                    PersistentPropertyMapping propertyMapping = propertyMappings[propIndex];
                    propertyMapping.IsEnabled = m_mappings[typeIndex].IsPropertyMappingEnabled[propIndex];

                    if (propertyMappingsSelection[propIndex] >= 0)
                    {
                        propertyMapping.MappedName = m_mappings[typeIndex].PropertyMappingNames[propIndex][propertyMappingsSelection[propIndex]];
                        propertyMapping.MappedTypeName = m_mappings[typeIndex].PropertyMappingTypeNames[propIndex][propertyMappingsSelection[propIndex]];
                        propertyMapping.MappedNamespace = m_mappings[typeIndex].PropertyMappingNamespaces[propIndex][propertyMappingsSelection[propIndex]];
                        propertyMapping.MappedAssemblyName = m_mappings[typeIndex].PropertyMappingAssemblyNames[propIndex][propertyMappingsSelection[propIndex]];
                        if (propertyMapping.PersistentTag == 0)
                        {
                            m_mappings[typeIndex].PersistentPropertyTag++;
                            propertyMapping.PersistentTag = m_mappings[typeIndex].PersistentPropertyTag;
                        }

                        selectedPropertyMappings.Add(propertyMapping);
                    }
                }


                m_mappings[typeIndex].PropertyMappings = selectedPropertyMappings.ToArray();
                ExpandType(typeIndex);

                classMapping.IsEnabled = m_mappings[typeIndex].IsEnabled;
                classMapping.PersistentPropertyTag = m_mappings[typeIndex].PersistentPropertyTag;
                classMapping.PersistentSubclassTag = m_mappings[typeIndex].PersistentSubclassTag;
                classMapping.PropertyMappings = selectedPropertyMappings.ToArray();
                if(typeIndexToSubclasses.ContainsKey(typeIndex))
                {
                    classMapping.Subclasses = typeIndexToSubclasses[typeIndex].Values.ToArray();
                }      
                classMapping.MappedAssemblyName = m_uoTypes[typeIndex].Assembly.FullName.Split(',')[0];
                classMapping.MappedNamespace = m_uoTypes[typeIndex].Namespace;
                classMapping.MappedTypeName = m_uoTypes[typeIndex].Name;
                classMapping.PersistentNamespace = PersistentClassMapping.ToPersistentNamespace(classMapping.MappedNamespace);
                classMapping.PersistentTypeName = PersistentClassMapping.ToPersistentName(m_uoTypes[typeIndex].Name);

                Type baseType = GetEnabledBaseType(typeIndex);
                if(baseType == null)
                {
                    classMapping.PersistentBaseNamespace = null;
                    classMapping.PersistentBaseTypeName = null;
                }
                else
                {
                    classMapping.PersistentBaseNamespace = PersistentClassMapping.ToPersistentNamespace(baseType.Namespace);
                    classMapping.PersistentBaseTypeName = PersistentClassMapping.ToPersistentName(baseType.Name);
                }
           
            }

            PrefabUtility.CreatePrefab(ClassMappingsStoragePath, storageGO);
            DestroyImmediate(storageGO);
        }

        private Type GetEnabledBaseType(int typeIndex)
        {
            Type baseType = null;
            Type type = m_uoTypes[typeIndex];
            while (true)
            {
                type = type.BaseType;
                if (type == typeof(UnityObject))
                {
                    baseType = type;
                    break;
                }

                if (type == null)
                {
                    break;
                }

                int baseIndex;
                if (m_typeToIndex.TryGetValue(type, out baseIndex))
                {
                    if (m_mappings[baseIndex].IsEnabled)
                    {
                        baseType = type;
                        break;
                    }
                }
            }

            return baseType;
        }

        private int GetAncestorsCount(Type type)
        {
            int count = 0;
            while (type != null && type.BaseType != typeof(UnityObject))
            {
                count++;
                type = type.BaseType;
            }
            return count;
        }

        private void DrawTypeEditor(int rootTypeIndex, int typeIndex, int indent = 1)
        {
            Type type = m_uoTypes[typeIndex];
            if(type == typeof(UnityObject))
            {
                return;
            }

            string label = type.Name;
            bool isExpandedChanged;
            bool isExpanded;
            bool isSelectionChanged;

            EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(false));
            {
                GUILayout.Space(5 + 18 * (indent - 1));
                EditorGUI.BeginChangeCheck();

                m_mappings[typeIndex].IsEnabled = EditorGUILayout.Toggle(m_mappings[typeIndex].IsEnabled, GUILayout.MaxWidth(15));

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
                    if (m_mappings[typeIndex].IsEnabled)
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
                EditorGUILayout.BeginVertical();
                {
                    for (int propIndex = 0; propIndex < m_mappings[typeIndex].PropertyMappings.Length; ++propIndex)
                    {
                        EditorGUILayout.BeginHorizontal();
                        {
                            GUILayout.Space(5 + 18 * indent);

                            PersistentPropertyMapping pMapping = m_mappings[typeIndex].PropertyMappings[propIndex];

                            m_mappings[typeIndex].IsPropertyMappingEnabled[propIndex] = EditorGUILayout.Toggle(m_mappings[typeIndex].IsPropertyMappingEnabled[propIndex], GUILayout.MaxWidth(20));

                            EditorGUILayout.LabelField(pMapping.PersistentName, GUILayout.MaxWidth(230));

                            int newPropertyIndex = EditorGUILayout.Popup(m_mappings[typeIndex].PropertyMappingSelection[propIndex], m_mappings[typeIndex].PropertyMappingNames[propIndex]);
                            m_mappings[typeIndex].PropertyMappingSelection[propIndex] = newPropertyIndex;
                           

                            EditorGUI.BeginChangeCheck();
                            GUILayout.Button("X", GUILayout.Width(20));
                            if(EditorGUI.EndChangeCheck())
                            {
                                m_mappings[typeIndex].PropertyMappingSelection[propIndex] = -1;
                            }

                            EditorGUILayout.LabelField("Slot: " + pMapping.PersistentTag, GUILayout.Width(60));
                        }
                        EditorGUILayout.EndHorizontal();
                    }

                    EditorGUILayout.BeginHorizontal();
                    {
                        GUILayout.Space(5 + 18 * indent);
                        GUILayout.Button("Edit", GUILayout.Width(100));
                    }
                    EditorGUILayout.EndHorizontal();

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
            }
        }

        private void TryExpandType(int typeIndex)
        {
            if (m_mappings[typeIndex].PropertyMappings != null)
            {
                return;
            }
            if (m_mappings[typeIndex].ExpandedCounter > 0 || m_mappings[typeIndex].IsEnabled)
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
            IEnumerable<string> fmapKeys = fieldMappings.Select(fMap => fMap.PersistentFullTypeName + " " + fMap.PersistentName);
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
            IEnumerable<string> pmapKeys = propertyMappings.Select(pMap => pMap.PersistentFullTypeName + " " + pMap.PersistentName);
            foreach (string key in pmapKeys)
            {
                if (!propertyMappingsHs.Contains(key))
                {
                    propertyMappingsHs.Add(key);
                }
            }

            FieldInfo[] fields = m_codeGen.GetFields(type);
            HashSet<string> fieldHs = new HashSet<string>(fields.Select(fInfo => fInfo.FieldType.FullName + " " + fInfo.Name));

            PropertyInfo[] properties = m_codeGen.GetProperties(type);
            HashSet<string> propertyHs = new HashSet<string>(properties.Select(pInfo => pInfo.PropertyType.FullName + " " + pInfo.Name));

            for(int i = 0; i < fieldMappings.Length; ++i)
            {
                PersistentPropertyMapping mapping = fieldMappings[i];
                string key = mapping.MappedFullTypeName + " " + mapping.MappedName;
                if(!fieldHs.Contains(key))
                {
                    mapping.MappedName = null;
                    mapping.MappedTypeName = null;
                    mapping.MappedNamespace = null;
                    mapping.MappedAssemblyName = null;

                    pMappingsEnabled.Add(false);
                }
                else
                {
                    pMappingsEnabled.Add(mapping.IsEnabled);
                }

                pMappings.Add(mapping);
            }


            for (int f = 0; f < fields.Length; ++f)
            {
                FieldInfo fInfo = fields[f];

                string key = string.Format("{0}.{1}",
                    PersistentClassMapping.ToPersistentNamespace(fInfo.FieldType.Namespace),
                    fInfo.FieldType.Name) + " " + fInfo.Name;

                if (fieldMappingsHs.Contains(key))
                {
                    continue;
                }

                PersistentPropertyMapping pMapping = new PersistentPropertyMapping();
                pMapping.PersistentName = fInfo.Name;
                pMapping.PersistentTypeName = fInfo.FieldType.Name;
                pMapping.PersistentNamespace = PersistentClassMapping.ToPersistentNamespace(fInfo.FieldType.Namespace);

                pMapping.MappedName = fInfo.Name;
                pMapping.MappedTypeName = fInfo.FieldType.Name;
                pMapping.MappedNamespace = fInfo.FieldType.Namespace;
                pMapping.MappedAssemblyName = fInfo.FieldType.Assembly.FullName.Split(',')[0];      
                pMapping.IsProperty = false;

                pMapping.UseCustomCode = false;
                pMapping.BuiltInCodeSnippet = null;

                pMappingsEnabled.Add(false);
                pMappings.Add(pMapping);
            }

            for (int i = 0; i < propertyMappings.Length; ++i)
            {
                PersistentPropertyMapping mapping = propertyMappings[i];
                string key = mapping.MappedFullTypeName + " " + mapping.MappedName;
                if (!propertyHs.Contains(key))
                {
                    mapping.MappedName = null;
                    mapping.MappedTypeName = null;
                    mapping.MappedNamespace = null;
                    mapping.MappedAssemblyName = null;

                    pMappingsEnabled.Add(false);
                }
                else
                {
                    pMappingsEnabled.Add(mapping.IsEnabled);
                }

                pMappings.Add(mapping);
            }


            for (int p = 0; p < properties.Length; ++p)
            {
                PropertyInfo pInfo = properties[p];

                string key = string.Format("{0}.{1}", 
                    PersistentClassMapping.ToPersistentNamespace(pInfo.PropertyType.Namespace),
                    pInfo.PropertyType.Name) + " " + pInfo.Name;

                if (propertyMappingsHs.Contains(key))
                {
                    continue;
                }

                PersistentPropertyMapping pMapping = new PersistentPropertyMapping();

                pMapping.PersistentName = pInfo.Name;       //property name of mapping
                pMapping.PersistentTypeName = pInfo.PropertyType.Name;
                pMapping.PersistentNamespace = PersistentClassMapping.ToPersistentNamespace(pInfo.PropertyType.Namespace);

                pMapping.MappedName = pInfo.Name;             //property name of unity type
                pMapping.MappedTypeName = pInfo.PropertyType.Name;
                pMapping.MappedNamespace = pInfo.PropertyType.Namespace;
                pMapping.MappedAssemblyName = pInfo.PropertyType.Assembly.FullName.Split(',')[0];
                pMapping.IsProperty = true;

                pMapping.UseCustomCode = false;
                pMapping.BuiltInCodeSnippet = null;

                pMappingsEnabled.Add(false);
                pMappings.Add(pMapping);
            }

            m_mappings[typeIndex].PropertyMappings = pMappings.ToArray();
            m_mappings[typeIndex].IsPropertyMappingEnabled = pMappingsEnabled.ToArray();

            m_mappings[typeIndex].PropertyMappingNames = new string[pMappings.Count][]; 
            m_mappings[typeIndex].PropertyMappingTypeNames = new string[pMappings.Count][];
            m_mappings[typeIndex].PropertyMappingNamespaces = new string[pMappings.Count][];
            m_mappings[typeIndex].PropertyMappingAssemblyNames = new string[pMappings.Count][];
            m_mappings[typeIndex].PropertyMappingSelection = new int[pMappings.Count];

            string[][] mappedKeys = new string[pMappings.Count][];

            for(int propIndex = 0; propIndex < pMappings.Count; ++propIndex)
            {
                PersistentPropertyMapping pMapping = pMappings[propIndex];

                var propertyInfo = GetSuitableFields(fields, PersistentClassMapping.ToMappedNamespace(pMapping.PersistentNamespace) + "." + pMapping.PersistentTypeName)
                    .Select(f => new { Name = f.Name, Type = f.FieldType.Name, Namespace = f.FieldType.Namespace, Assembly = f.FieldType.Assembly.FullName.Split(',')[0] })
                    .Union(GetSuitableProperties(properties, PersistentClassMapping.ToMappedNamespace(pMapping.PersistentNamespace) + "." + pMapping.PersistentTypeName)
                    .Select(p => new { Name = p.Name, Type = p.PropertyType.Name, Namespace = p.PropertyType.Namespace, Assembly = p.PropertyType.Assembly.FullName.Split(',')[0] }))
                    .OrderBy(p => p.Name)
                    .ToArray();

                m_mappings[typeIndex].PropertyMappingNames[propIndex] = propertyInfo.Select(p => p.Name).ToArray();
                m_mappings[typeIndex].PropertyMappingTypeNames[propIndex] = propertyInfo.Select(p => p.Type).ToArray();
                m_mappings[typeIndex].PropertyMappingNamespaces[propIndex] = propertyInfo.Select(p => p.Namespace).ToArray();
                m_mappings[typeIndex].PropertyMappingAssemblyNames[propIndex] = propertyInfo.Select(p => p.Assembly).ToArray();
                mappedKeys[propIndex] = propertyInfo.Select(m => m.Namespace + "." + m.Type + " " + m.Name).ToArray();
            }

            for (int propIndex = 0; propIndex < m_mappings[typeIndex].PropertyMappingSelection.Length; ++propIndex)
            {
                PersistentPropertyMapping mapping = m_mappings[typeIndex].PropertyMappings[propIndex];

                m_mappings[typeIndex].PropertyMappingSelection[propIndex] = Array.IndexOf(mappedKeys[propIndex], mapping.MappedFullTypeName + " " + mapping.MappedName);
            }
        }


        private IEnumerable<PropertyInfo> GetSuitableProperties(PropertyInfo[] properties, string persistentType)
        {
            return properties.Where(pInfo => pInfo.PropertyType.FullName == persistentType);
        }

        private IEnumerable<FieldInfo> GetSuitableFields(FieldInfo[] fields, string persistentType)
        {
            return fields.Where(fInfo => fInfo.FieldType.FullName == persistentType);
        }

        
    }
}

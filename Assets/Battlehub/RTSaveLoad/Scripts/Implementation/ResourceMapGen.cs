#if UNITY_EDITOR
using UnityEngine;
using System;
using System.Collections.Generic;

using UnityObject = UnityEngine.Object;
using UnityEditor;
using System.Linq;
using System.Reflection;
using Battlehub.Utils;
using UnityEngine.Audio;

namespace Battlehub.RTSaveLoad
{

    public static class ResourceMapGen
    {
        public static readonly string[] AllowedPath = new string[]
        {
            //"Assets/"
           // "Assets/EditableObjects/"
        };

        private const string bundleNameDelimiter = ">";

        private static bool m_automaticDefault = false;
        public static bool Automatic
        {
            get
            {
                if (EditorPrefs.HasKey("Battlehub.RTSaveLoad.ResourceMapGen.Automatic"))
                {
                    return EditorPrefs.GetBool("Battlehub.RTSaveLoad.ResourceMapGen.Automatic");
                }
                return m_automaticDefault;
            }
            set
            {
                EditorPrefs.SetBool("Battlehub.RTSaveLoad.ResourceMapGen.Automatic", value);
            }
        }


        public const string ResourceMapsFolder = "ResourceMaps";
        public const string RootFolder = BHPath.Root + @"/RTSaveLoad";
        public const string ResourceMapsPath = RootFolder + "/" + ResourceMapsFolder;


        private class GroupDescriptor
        {
            public string Name;
            public List<UnityObject> Objects;
            public GroupDescriptor(string name)
            {
                Name = name;
                Objects = new List<UnityObject>();
            }
        }

        public const int ObjectsPerResourceGroup = 100;

        private static readonly string[] m_builtInResources =
            {
                   "Assets/" + BHPath.Root + "/RTSaveLoad/Resources/Capsule.prefab",
                   "Assets/" + BHPath.Root + "/RTSaveLoad/Resources/Cube.prefab",
                   "Assets/" + BHPath.Root + "/RTSaveLoad/Resources/Cylinder.prefab",
                   "Assets/" + BHPath.Root + "/RTSaveLoad/Resources/Plane.prefab",
                   "Assets/" + BHPath.Root + "/RTSaveLoad/Resources/Quad.prefab",
                   "Assets/" + BHPath.Root + "/RTSaveLoad/Resources/Sphere.prefab"
               };

        public static bool IsBusy
        {
            get;
            private set;
        }

        public static void CreateResourceMap(bool verbose)
        {
            try
            {
                IsBusy = true;
                CreateResourceMapInternal(verbose);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private class BundleResourceMapData
        {
            public HashSet<UnityObject> Objects = new HashSet<UnityObject>();
            public Dictionary<Type, GroupDescriptor> GroupsDict = new Dictionary<Type, GroupDescriptor>
            {
               { typeof(GameObject), new GroupDescriptor("Prefabs") },
               { typeof(Material), new GroupDescriptor("Materials") },
               { typeof(Mesh), new GroupDescriptor("Meshes") },
               { typeof(Texture), new GroupDescriptor("Texutres") },
               { typeof(Sprite), new GroupDescriptor("Sprites") },
               { typeof(PhysicMaterial), new GroupDescriptor("PhysicMaterials") },
               { typeof(PhysicsMaterial2D), new GroupDescriptor("PhysicMaterials2D") },
               { typeof(Motion), new GroupDescriptor("Motions") },
               { typeof(AnimatorOverrideController), new GroupDescriptor("AnimatorOverrideControllers") },
               { typeof(GUISkin), new GroupDescriptor("GUISkins") },
               { typeof(Avatar), new GroupDescriptor("Avatars") },
               { typeof(AudioClip), new GroupDescriptor("AudioClips") },
               { typeof(AudioMixer), new GroupDescriptor("AudioMixers") },
               { typeof(Shader), new GroupDescriptor("Shaders") },
               { typeof(TextAsset), new GroupDescriptor("TextAssets") },
               { typeof(Flare), new GroupDescriptor("Flares") },
               { typeof(Font), new GroupDescriptor("Fonts") },
               { typeof(UnityEditor.Animations.AnimatorController), new GroupDescriptor("AnimatorControllers") },
               { typeof(UnityObject), new GroupDescriptor("Other") }
            };
        }

        private static void CreateResourceMapInternal(bool verbose)
        {
            DateTime start = DateTime.Now;
            if (verbose) Debug.Log("[ResourceMapGen] Creating Resource Map... ");

            Assembly unityEditor = typeof(Editor).Assembly;
            string[] assetPaths = AssetDatabase.GetAllAssetPaths();

            Dictionary<string, BundleResourceMapData> objectsPerBundle = new Dictionary<string, BundleResourceMapData>();
            BundleResourceMapData allResourceMapData = new BundleResourceMapData();
            BundleResourceMapData notBundledObjects = new BundleResourceMapData();
            objectsPerBundle.Add(bundleNameDelimiter, notBundledObjects);

            bool hasBundledAssets = false;

            //searching for objects
            foreach (string path in assetPaths)
            {
                if(AllowedPath.Length > 0)
                {
                    if (!AllowedPath.Any(p => path.Contains(p)))
                    {
                        continue;
                    }
                }
                
                if (PathHelper.IsPathRooted(path))
                {
                    Debug.Log("Path is rooted " + path + ". skip...");
                    continue;
                }
                UnityObject obj = AssetDatabase.LoadAssetAtPath(path, typeof(UnityObject));
                if (obj == null)
                {
                    continue;
                }

                AssetImporter importer = AssetImporter.GetAtPath(path);
                string bundleName = importer.assetBundleName;
                bool isBundledAsset = !string.IsNullOrEmpty(bundleName);
                BundleResourceMapData resourceMapData;
                if (isBundledAsset)
                {
                    hasBundledAssets = true;

                    string bundleNameAndVariant = importer.assetBundleName + bundleNameDelimiter + importer.assetBundleVariant;
                    if (!objectsPerBundle.TryGetValue(bundleNameAndVariant, out resourceMapData))
                    {
                        resourceMapData = new BundleResourceMapData();
                        objectsPerBundle.Add(bundleNameAndVariant, resourceMapData);
                    }
                }
                else
                {
                    resourceMapData = notBundledObjects;
                }

                if (obj is GameObject)
                {
                    GameObject go = (GameObject)obj;
                    if (go.GetComponent<BundleResourceMap>() != null)
                    {
                        continue;
                    }
                }

                if (obj.GetType().Assembly != unityEditor || obj.GetType() == typeof(UnityEditor.Animations.AnimatorController))
                {
                    if (!allResourceMapData.Objects.Contains(obj))
                    {
                        resourceMapData.Objects.Add(obj);
                        allResourceMapData.Objects.Add(obj);
                    }
                }

                foreach (UnityObject sub in AssetDatabase.LoadAllAssetRepresentationsAtPath(path))
                {
                    if (sub == null)
                    {
                        continue;
                    }
                    if (sub.GetType().Assembly == unityEditor)
                    {
                        continue;
                    }

                    if (!allResourceMapData.Objects.Contains(sub))
                    {
                        resourceMapData.Objects.Add(sub);
                        allResourceMapData.Objects.Add(sub);
                        TryAddMaterialShader(sub, resourceMapData);
                    }
                }

                TryAddMaterialShader(obj, resourceMapData);
            }

            foreach (BundleResourceMapData resourceMapData in objectsPerBundle.Values)
            {
                if (resourceMapData == notBundledObjects)
                {
                    continue;
                }

                for (int i = 0; i < m_builtInResources.Length; ++i)
                {
                    GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(m_builtInResources[i]);
                    if (obj == null)
                    {
                        continue;
                    }
                    Material[] materials = obj.GetComponent<Renderer>().sharedMaterials;
                    foreach (Material material in materials)
                    {
                        if (!resourceMapData.Objects.Contains(material))
                        {
                            resourceMapData.Objects.Add(material);

                            //do not add to allResourceMapData. Built-in material could be added to serveral resource maps.
                        }

                        if (material.shader != null)
                        {
                            if (!resourceMapData.Objects.Contains(material.shader))
                            {
                                resourceMapData.Objects.Add(material.shader);

                                //do not add to allResourceMapData. Built-in shader could be added to serveral resource maps.
                            }
                        }
                    }
                }
            }

            HashSet<UnityObject> builtInResources = new HashSet<UnityObject>();
            for (int i = 0; i < m_builtInResources.Length; ++i)
            {
                GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(m_builtInResources[i]);
                if (obj == null)
                {
                    continue;
                }
                Material[] materials = obj.GetComponent<Renderer>().sharedMaterials;
                foreach (Material material in materials)
                {
                    if (!allResourceMapData.Objects.Contains(material))
                    {
                        notBundledObjects.Objects.Add(material);
                        allResourceMapData.Objects.Add(material);
                    }

                    if (material.shader != null)
                    {
                        if (!allResourceMapData.Objects.Contains(material.shader))
                        {
                            notBundledObjects.Objects.Add(material.shader);
                            allResourceMapData.Objects.Add(material.shader);
                        }
                    }

                }
                Mesh mesh = obj.GetComponent<MeshFilter>().sharedMesh;
                if (!allResourceMapData.Objects.Contains(mesh))
                {
                    notBundledObjects.Objects.Add(mesh);
                    builtInResources.Add(mesh);
                    allResourceMapData.Objects.Add(mesh);
                }
            }

            GameObject[] rootSceneObjects = Resources.FindObjectsOfTypeAll<GameObject>()
                .Where(go => !go.IsPrefab() &&
                              go.transform.parent == null &&
                              go.hideFlags == HideFlags.None).ToArray();

            List<UnityObject> persistentIgnoreObjects = new List<UnityObject>();
            for (int i = 0; i < rootSceneObjects.Length; ++i)
            {
                FindPersistentIgnoreObjects(rootSceneObjects[i].transform, persistentIgnoreObjects);
            }

            //processing objects 
            foreach (BundleResourceMapData data in objectsPerBundle.Values)
            {
                foreach (UnityObject obj in data.Objects)
                {
                    if (obj == null)
                    {
                        continue;
                    }

                    if ((obj.hideFlags & HideFlags.DontSaveInBuild) != 0)
                    {
                        if (!builtInResources.Contains(obj))
                        {
                            continue;
                        }
                    }

                    GroupDescriptor descriptor = data.GroupsDict[typeof(UnityObject)];
                    Type type = obj.GetType();
                    while (type != typeof(object))
                    {
                        if (data.GroupsDict.TryGetValue(type, out descriptor))
                        {
                            descriptor = data.GroupsDict[type];
                            break;
                        }
                        type = type.BaseType;
                    }

                    if (obj is GameObject)
                    {
                        GameObject prefab = (GameObject)obj;
                        if (PrefabUtility.FindPrefabRoot(prefab) == obj)
                        {
                            descriptor.Objects.Add(prefab);
                        }
                    }
                    else if (obj is Component)
                    {
                        continue;
                    }
                    else
                    {
                        descriptor.Objects.Add(obj);
                    }
                }
            }

            BundleResourceMap[] bundleResourceMaps = Resources.FindObjectsOfTypeAll<BundleResourceMap>();
            HashSet<Guid> bundleResourceMapGuids = new HashSet<Guid>(bundleResourceMaps.Select(m => new Guid(m.Guid)));

            ResourceMap[] resourceMaps = bundleResourceMaps.OfType<ResourceMap>().ToArray();
            if (resourceMaps.Length > 1)
            {
                if (verbose) Debug.LogError("You have more than one ResourceMap");
            }

            ResourceMap mainResourceMap;
            Dictionary<string, BundleResourceMap> bundleResourceMapDict = new Dictionary<string, BundleResourceMap>();
            {
                mainResourceMap = resourceMaps.OrderByDescending(m => m.GetCounter()).FirstOrDefault();
                if (mainResourceMap == null)
                {
                    GameObject mapGO = new GameObject();
                    mapGO.name = "Resource Map";
                    mainResourceMap = mapGO.AddComponent<ResourceMap>();
                }
                else
                {
                    GameObject resourceMapGO = UnityObject.Instantiate(mainResourceMap.gameObject);
                    resourceMapGO.transform.SetParent(mainResourceMap.transform.parent, false);

                    if (!mainResourceMap.gameObject.IsPrefab())
                    {
                        Undo.DestroyObjectImmediate(mainResourceMap.gameObject);
                    }

                    resourceMapGO.name = "Resource Map";
                    mainResourceMap = resourceMapGO.GetComponent<ResourceMap>();
                }

                bundleResourceMapDict.Add(bundleNameDelimiter, mainResourceMap);
            }


            for (int i = 0; i < bundleResourceMaps.Length; ++i)
            {
                BundleResourceMap bundleResourceMap = bundleResourceMaps[i];
                string key = bundleResourceMap.BundleName + bundleNameDelimiter + bundleResourceMap.VariantName;
                if (!bundleResourceMapDict.ContainsKey(key))
                {
                    GameObject bundleResourceMapGO = UnityObject.Instantiate(bundleResourceMap.gameObject);
                    bundleResourceMapGO.transform.SetParent(bundleResourceMap.transform.parent, false);

                    if (!bundleResourceMap.gameObject.IsPrefab())
                    {
                        Undo.DestroyObjectImmediate(bundleResourceMap.gameObject);
                    }

                    bundleResourceMapGO.name = "Resource Map " + key.Replace(bundleNameDelimiter, "_");
                    bundleResourceMap = bundleResourceMapGO.GetComponent<BundleResourceMap>();
                    bundleResourceMapDict.Add(key, bundleResourceMap);
                }
            }

            ResourceGroup[] allResourceGroups = Resources.FindObjectsOfTypeAll<ResourceGroup>().Where(r => !r.gameObject.IsPrefab()).ToArray();
            ResourceGroup[] destroyGroups = allResourceGroups.Where(r => string.IsNullOrEmpty(r.Guid) || !bundleResourceMapGuids.Contains(new Guid(r.Guid))).ToArray();
            for (int i = 0; i < destroyGroups.Length; ++i)
            {
                UnityObject.DestroyImmediate(destroyGroups[i]);
            }

            if (!AssetDatabase.IsValidFolder("Assets/" + ResourceMapsPath))
            {
                AssetDatabase.CreateFolder("Assets/" + RootFolder, ResourceMapsFolder);
            }
            if (!AssetDatabase.IsValidFolder("Assets/" + ResourceMapsPath + "/Resources"))
            {
                AssetDatabase.CreateFolder("Assets/" + ResourceMapsPath, "Resources");
            }

            foreach (KeyValuePair<string, BundleResourceMapData> keyBundleResourceMapData in objectsPerBundle)
            {
                BundleResourceMap bundleResourceMap;
                if (!bundleResourceMapDict.TryGetValue(keyBundleResourceMapData.Key, out bundleResourceMap))
                {
                    GameObject bundleResourceMapGO = new GameObject();
                    bundleResourceMapGO.name = "Resource Map" + keyBundleResourceMapData.Key.Replace(bundleNameDelimiter, "_");
                    bundleResourceMap = bundleResourceMapGO.AddComponent<BundleResourceMap>();
                    string[] bundleNameAndVariant = keyBundleResourceMapData.Key.Split(new[] { bundleNameDelimiter }, StringSplitOptions.None);
                    bundleResourceMap.BundleName = bundleNameAndVariant[0];
                    bundleResourceMap.VariantName = bundleNameAndVariant[1];
                    bundleResourceMapDict.Add(keyBundleResourceMapData.Key, bundleResourceMap);
                }

                Guid bundleResourceMapGuid = new Guid(bundleResourceMap.Guid);
                ResourceGroup[] resourceGroups = allResourceGroups.Where(r => !string.IsNullOrEmpty(r.Guid) && new Guid(r.Guid) == bundleResourceMapGuid).ToArray();
                Dictionary<UnityObject, int> extistingMapping = ToDictionary(resourceGroups);
                if (verbose) Debug.Log("[ResourceMapGen] " + keyBundleResourceMapData.Key + " Existing Mappings = " + extistingMapping.Count);

                DestroyChildren(bundleResourceMap.gameObject.transform);
                Undo.RegisterCreatedObjectUndo(bundleResourceMap.gameObject, "Battlehub.RTSaveLoad.ResourceMapGen");

                BundleResourceMapData resourceMapData = keyBundleResourceMapData.Value;
                PopulateRootGroups(mainResourceMap, bundleResourceMap, resourceMapData.GroupsDict.Where(kvp => kvp.Key != typeof(GameObject)).Select(kvp => kvp.Value), extistingMapping);

                GroupDescriptor prefabs = resourceMapData.GroupsDict[typeof(GameObject)];
                PopulatePrefabsGroup(mainResourceMap, bundleResourceMap, prefabs, extistingMapping);

                if (bundleResourceMap == mainResourceMap)
                {
                    CreatePersistentIngoreResourceGroups(mainResourceMap, persistentIgnoreObjects, extistingMapping);
                    OrderTransformsByName(bundleResourceMap.transform);
                }
                else
                {
                    OrderTransformsByName(bundleResourceMap.transform);
                    CreateResourceMapPrefab(verbose, "_" + keyBundleResourceMapData.Key.Replace(bundleNameDelimiter, "_") + "_" + bundleResourceMap.Guid, bundleResourceMap);
                }
            }

            CreateResourceMapPrefab(verbose, string.Empty, mainResourceMap);

            if (verbose) Debug.Log("[ResourceMapGen] Max ID: " + mainResourceMap.GetCounter());
            if (verbose) Debug.Log("[ResourceMapGen] Resource Map Created... Elapsed Time " + (DateTime.Now - start).TotalMilliseconds + " ms ");

            foreach (KeyValuePair<string, BundleResourceMap> kvp in bundleResourceMapDict)
            {
                BundleResourceMap bundleResourceMap = kvp.Value;
                GameObject mapGO = bundleResourceMap.gameObject;

                UnityObject.DestroyImmediate(bundleResourceMap);
                UnityObject.DestroyImmediate(mapGO);
            }

            BundleResourceMap[] maps = Resources.FindObjectsOfTypeAll<BundleResourceMap>();
#if UNITY_EDITOR

            foreach (BundleResourceMap bundleResourceMap in maps)
            {
                IdentifiersMap.Internal_Initialize(bundleResourceMap);
                RuntimeShaderInfoGen.RemoveUnused(bundleResourceMap.BundleName, bundleResourceMap.VariantName);
            }

            Dictionary<BundleResourceMap, Shader[]> m_shaders = new Dictionary<BundleResourceMap, Shader[]>();
            foreach (BundleResourceMap bundleResourceMap in maps)
            {
                IdentifiersMap.Internal_Initialize(bundleResourceMap);
                Shader[] shaders = RuntimeShaderInfoGen.Create(bundleResourceMap.BundleName, bundleResourceMap.VariantName);
                m_shaders.Add(bundleResourceMap, shaders);
            }

            AssetDatabase.Refresh();

            foreach (BundleResourceMap bundleResourceMap in maps)
            {
                IdentifiersMap.Internal_Initialize(bundleResourceMap);
                RuntimeShaderInfoGen.SetAssetBundleNameAndVariant(m_shaders[bundleResourceMap], bundleResourceMap.BundleName, bundleResourceMap.VariantName);
            }

            if (hasBundledAssets)
            {
                Debug.Log("Project has bundled assets. Build All Asset Bundles. Done.");
                CreateAssetBundles.BuildAllAssetBundles();
            }

#endif
        }

        private static void TryAddMaterialShader(UnityObject obj, BundleResourceMapData resourceMapData)
        {
            if (obj is Material)
            {
                Material material = (Material)obj;
                if (material.shader != null)
                {
                    if (!resourceMapData.Objects.Contains(material.shader))
                    {
                        resourceMapData.Objects.Add(material.shader);
                    }

                    //do not add to allResourceMapData. Shader could be added to serveral resource maps.
                }
            }
        }

        private static void CreateResourceMapPrefab(bool verbose, string bundleName, BundleResourceMap bundleResourceMap)
        {
            string prefabName = IdentifiersMap.ResourceMapPrefabName + bundleName + ".prefab";
            string prefabPath = "Assets/" + ResourceMapsPath + (string.IsNullOrEmpty(bundleName) ? "/Resources/" : "/") + prefabName;

            PrefabUtility.CreatePrefab(prefabPath, bundleResourceMap.gameObject, ReplacePrefabOptions.ReplaceNameBased);
            AssetImporter importer = AssetImporter.GetAtPath(prefabPath);
            importer.SetAssetBundleNameAndVariant(bundleResourceMap.BundleName, bundleResourceMap.VariantName);
            if (verbose) Debug.Log("[ResourceMapGen] Assets/" + prefabPath);
        }

        private static void CreatePersistentIngoreResourceGroups(ResourceMap map, List<UnityObject> ignoreObjects, Dictionary<UnityObject, int> dict)
        {
            for (int i = 0; i < ignoreObjects.Count; ++i)
            {
                PersistentIgnore io = (PersistentIgnore)ignoreObjects[i];
                if (!io)
                {
                    continue;
                }
                GameObject go = io.gameObject;
                Transform tr = io.transform;

                ResourceGroup[] resourceGroups = go.GetComponents<ResourceGroup>();
                foreach (ResourceGroup rg in resourceGroups)
                {
                    UnityObject.DestroyImmediate(rg);
                }

                List<ObjectToID> mappingsInGroup = new List<ObjectToID>();
                ResourceGroup resourceGroup = go.AddComponent<ResourceGroup>();
                resourceGroup.Guid = map.Guid;
                int id;
                if (!dict.TryGetValue(io, out id))
                {
                    id = map.IncCounter();
                    dict.Add(io, id);
                }

                mappingsInGroup.Add(new ObjectToID(io, id));

                if (!dict.TryGetValue(go, out id))
                {
                    id = map.IncCounter();
                    dict.Add(go, id);
                }

                mappingsInGroup.Add(new ObjectToID(go, id));

                if (!dict.TryGetValue(tr, out id))
                {
                    id = map.IncCounter();
                    dict.Add(tr, id);
                }

                mappingsInGroup.Add(new ObjectToID(tr, id));

                resourceGroup.Mapping = mappingsInGroup.OrderBy(m => m.Name).ToArray();
            }
        }

        private static void PopulatePrefabsGroup(ResourceMap mainResourceMap, BundleResourceMap bundleResourceMap, GroupDescriptor descriptor, Dictionary<UnityObject, int> dict)
        {
            GameObject rootGo = new GameObject();
            rootGo.name = descriptor.Name;
            rootGo.transform.SetParent(bundleResourceMap.transform, false);
            Transform t = rootGo.transform;

            GameObject[] rootPrefabs = descriptor.Objects.OfType<GameObject>().ToArray();
            for (int i = 0; i < rootPrefabs.Length; ++i)
            {
                GameObject rootPrefab = rootPrefabs[i];

                GameObject groupGo = new GameObject();
                groupGo.name = rootPrefab.name;
                groupGo.transform.SetParent(t, false);

                PopulatePrefabsGroup(mainResourceMap, bundleResourceMap, groupGo.AddComponent<ResourceGroup>(), rootPrefab, dict);
            }
        }

        private static void PopulatePrefabsGroup(ResourceMap mainResourceMap, BundleResourceMap bundleResourceMap, ResourceGroup resourceGroup, GameObject prefab, Dictionary<UnityObject, int> dict)
        {
            resourceGroup.Guid = bundleResourceMap.Guid;

            int id;
            if (!dict.TryGetValue(prefab, out id))
            {
                id = mainResourceMap.IncCounter();
                if (mainResourceMap != bundleResourceMap)
                {
                    id += int.MaxValue / 2;
                }
                dict.Add(prefab, id);
            }

            List<ObjectToID> mappingsInGroup = new List<ObjectToID>();
            mappingsInGroup.Add(new ObjectToID(prefab, id));

            Component[] components = prefab.GetComponents<Component>();
            for (int i = 0; i < components.Length; ++i)
            {
                Component component = components[i];
                if (component == null)
                {
                    Debug.LogWarning("component is null gameObject " + prefab.name);
                    continue;
                }
                if (!dict.TryGetValue(component, out id))
                {
                    id = mainResourceMap.IncCounter();
                    if (mainResourceMap != bundleResourceMap)
                    {
                        id += int.MaxValue / 2;
                    }
                    dict.Add(component, id);
                }

                mappingsInGroup.Add(new ObjectToID(component, id));
            }

            resourceGroup.Mapping = mappingsInGroup.OrderBy(m => m.Name).ToArray();

            foreach (Transform child in prefab.transform)
            {
                GameObject childGo = new GameObject();
                childGo.name = child.name;
                childGo.transform.SetParent(resourceGroup.transform);

                PopulatePrefabsGroup(mainResourceMap, bundleResourceMap, childGo.AddComponent<ResourceGroup>(), child.gameObject, dict);
            }
        }

        private static void PopulateRootGroups(ResourceMap mainResourceMap, BundleResourceMap bundleResourceMap, IEnumerable<GroupDescriptor> descriptors, Dictionary<UnityObject, int> dict)
        {
            foreach (GroupDescriptor descriptor in descriptors)
            {
                GameObject rootGo = new GameObject();
                rootGo.name = descriptor.Name;
                rootGo.transform.SetParent(bundleResourceMap.transform, false);
                Transform t = rootGo.transform;

                List<UnityObject> objects = descriptor.Objects;
                List<ObjectToID> allMappings = new List<ObjectToID>();
                for (int i = 0; i < objects.Count; ++i)
                {
                    int id;
                    UnityObject obj = objects[i];
                    if (!dict.TryGetValue(obj, out id))
                    {
                        id = mainResourceMap.IncCounter();
                        if (mainResourceMap != bundleResourceMap)
                        {
                            id += int.MaxValue / 2;
                        }
                        dict.Add(obj, id);
                    }

                    allMappings.Add(new ObjectToID(obj, id));
                }

                ObjectToID[] allMappingsOrdered = allMappings.OrderBy(o => o.Name).ToArray();
                List<ObjectToID> mappingsInGroup = new List<ObjectToID>();
                for (int i = 0; i < allMappingsOrdered.Length; ++i)
                {
                    if (mappingsInGroup.Count >= ObjectsPerResourceGroup)
                    {
                        ResourceGroup resourceGroup = t.gameObject.AddComponent<ResourceGroup>();
                        resourceGroup.Guid = bundleResourceMap.Guid;
                        resourceGroup.Mapping = mappingsInGroup.ToArray();
                        mappingsInGroup = new List<ObjectToID>();
                    }

                    mappingsInGroup.Add(allMappingsOrdered[i]);
                }

                ResourceGroup lastGroup = t.gameObject.AddComponent<ResourceGroup>();
                lastGroup.Guid = bundleResourceMap.Guid;
                lastGroup.Mapping = mappingsInGroup.ToArray();
            }
        }

        private static Dictionary<UnityObject, int> ToDictionary(ResourceGroup[] resourceGroups)
        {
            Dictionary<UnityObject, int> dict = new Dictionary<UnityObject, int>();
            for (int i = 0; i < resourceGroups.Length; ++i)
            {
                ResourceGroup group = resourceGroups[i];
                for (int j = 0; j < group.Mapping.Length; ++j)
                {
                    ObjectToID objToID = group.Mapping[j];
                    if (objToID.Object == null)
                    {
                        continue;
                    }
                    dict.Add(objToID.Object, objToID.Id);
                }
            }

            return dict;
        }


        private static Transform GetChildByName(Transform t, string name)
        {
            foreach (Transform child in t)
            {
                if (child.name == name)
                {
                    return child;
                }
            }

            return null;
        }

        private static void DestroyChildren(Transform t)
        {
            List<Transform> children = new List<Transform>();
            foreach (Transform c in t)
            {
                children.Add(c);
            }

            for (int i = 0; i < children.Count; ++i)
            {
                UnityObject.DestroyImmediate(children[i].gameObject);
            }
        }

        private static void FindPersistentIgnoreObjects(Transform t, List<UnityObject> pIgnoreObjects)
        {
            PersistentIgnore pIgnore = t.GetComponent<PersistentIgnore>();
            if (pIgnore != null)
            {
                pIgnoreObjects.Add(pIgnore);
            }

            foreach (Transform c in t)
            {
                FindPersistentIgnoreObjects(c, pIgnoreObjects);
            }
        }

        private static void OrderTransformsByName(Transform parent)
        {
            List<Transform> transforms = new List<Transform>();
            foreach (Transform child in parent)
            {
                transforms.Add(child);
            }

            Transform[] ordered = transforms.OrderBy(t => t.name).ToArray();
            for (int i = 0; i < ordered.Length; ++i)
            {
                ordered[i].SetSiblingIndex(i);
            }

            for (int i = 0; i < ordered.Length; ++i)
            {
                OrderTransformsByName(ordered[i]);
            }
        }
    }
}


#endif

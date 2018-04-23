using Battlehub.RTCommon;
using Battlehub.RTSaveLoad.PersistentObjects;
using Battlehub.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using UnityObject = UnityEngine.Object;
namespace Battlehub.RTSaveLoad
{
    [ExecuteInEditMode]
    public class ProjectManager : RuntimeSceneManager, IProjectManager
    {
        public event EventHandler ProjectLoading;
        public event EventHandler<ProjectManagerEventArgs> ProjectLoaded;
        public event EventHandler<ProjectManagerEventArgs> BundledResourcesAdded;
        public event EventHandler<ProjectManagerEventArgs> DynamicResourcesAdded;

        private IAssetBundleLoader m_bundleLoader;

    
        [SerializeField]
        private FolderTemplate m_projectTemplate;
        [NonSerialized]
        private ProjectRoot m_root;
        private bool m_isProjectLoaded;

        /// <summary>
        /// All loaded resources (static and dynamic)
        /// </summary>
        private Dictionary<long, UnityObject> m_loadedResources;

        private class LoadedAssetBundle
        {
            public AssetBundle Bundle;
            public int Usages;
        }
        private Dictionary<string, LoadedAssetBundle> m_loadedBundles = new Dictionary<string, LoadedAssetBundle>();

        /// <summary>
        /// Only loaded dynamic resources
        /// </summary>
        private Dictionary<long, UnityObject> m_dynamicResources = new Dictionary<long, UnityObject> ();

        [SerializeField]
        private Transform m_dynamicResourcesRoot;

        public ProjectItem Project
        {
            get
            {
                if(m_root == null)
                {
                    return null;
                }
                return m_root.Item;
            }
        }
        

        protected override void AwakeOverride()
        {
            base.AwakeOverride();
            m_bundleLoader = Dependencies.BundleLoader;
            if (m_dynamicResourcesRoot == null)
            {
                m_dynamicResourcesRoot = transform;
            }
        }

        protected override void OnDestroyOverride()
        {
            base.OnDestroyOverride();

            if(m_dynamicResources != null)
            {
                DestroyDynamicResources();
            }
            
            if(m_loadedBundles != null)
            {
                UnloadAssetBundles();
            }
            
            m_loadedResources = null;
        }

        private void OnApplicationQuit()
        {
            m_loadedResources = null;
            m_dynamicResources = null;
            m_loadedBundles = null;
        }

        private void UnloadAssetBundles()
        {
            foreach(KeyValuePair<string, LoadedAssetBundle> kvp in m_loadedBundles)
            {
                if(kvp.Value.Bundle != null)
                {
                    RuntimeShaderUtil.RemoveExtra(kvp.Key);
                    IdentifiersMap.Instance.Unregister(kvp.Value.Bundle);
                    kvp.Value.Bundle.Unload(true);
                }
            }
        }

        private void DestroyDynamicResources()
        {
            foreach(KeyValuePair<long, UnityObject> kvp in m_dynamicResources)
            {
                long mappedInstanceID = kvp.Key;
                IdentifiersMap.Instance.Unregister(mappedInstanceID);
                m_loadedResources.Remove(mappedInstanceID);
                UnityObject obj = kvp.Value;

                if(obj)
                {
                    Destroy(obj);
                }
            }
            m_dynamicResources.Clear();
        }

        private void EnumerateBundles(ProjectItem item)
        {
            if(!string.IsNullOrEmpty(item.BundleName))
            {
                LoadedAssetBundle loadedAssetBundle;
                if(!m_loadedBundles.TryGetValue(item.BundleName, out loadedAssetBundle))
                {
                    loadedAssetBundle = new LoadedAssetBundle
                    {
                        Usages = 1
                    };
                    m_loadedBundles.Add(item.BundleName, loadedAssetBundle);
                }
                else
                {
                    loadedAssetBundle.Usages++;
                }
            }

            if(item.Children != null)
            {
                for(int i = 0; i < item.Children.Count; ++i)
                {
                    EnumerateBundles(item.Children[i]);
                }
            }
        }

        private void LoadBundles(Action callback)
        {
            int loading = m_loadedBundles.Count;
            if(loading == 0)
            {
                if(callback != null)
                {
                    callback();
                }
            }
            foreach (KeyValuePair<string, LoadedAssetBundle> kvp in m_loadedBundles)
            {
                m_bundleLoader.Load(kvp.Key, (bundleName, bundle) =>
                {
                    if(bundle != null)
                    {
                        TextAsset[] textAssets = LoadAllTextAssets(bundle);

                        RuntimeShaderUtil.AddExtra(name, textAssets);
                        IdentifiersMap.Instance.Register(bundle);

                        kvp.Value.Bundle = bundle;
                    }

                    loading--;
                    if(loading == 0)
                    {
                        callback();
                    }
                });
            }
        }

        private static TextAsset[] LoadAllTextAssets(AssetBundle bundle)
        {
            string[] assetNames = bundle.GetAllAssetNames();
            List<TextAsset> textAssetsList = new List<TextAsset>();
            for (int i = 0; i < assetNames.Length; ++i)
            {
                string assetName = assetNames[i];
                if (assetName.EndsWith(".txt"))
                {
                    textAssetsList.Add(bundle.LoadAsset<TextAsset>(assetName));
                }
            }
            TextAsset[] textAssets = textAssetsList.ToArray();
            return textAssets;
        }

        public bool IsResource(UnityObject obj)
        {
            return !IdentifiersMap.IsNotMapped(obj);
        }

        public ID GetID(UnityObject obj)
        {
            return new ID(obj.GetMappedInstanceID());
        }

        public void LoadProject(string projectName, ProjectManagerCallback<ProjectItem> callback)
        {
            m_isProjectLoaded = false;

            if(ProjectLoading != null)
            {
                ProjectLoading(this, EventArgs.Empty);
            }

            UnloadAssetBundles();
            DestroyDynamicResources();

            IJob job = Dependencies.Job;
            job.Submit(doneCallback =>
            {
                bool metaOnly = false;
                int[] exceptTypes = { ProjectItemTypes.Scene };
                m_project.LoadProject(projectName, loadProjectCompleted =>
                {
                    m_root = loadProjectCompleted.Data;
                    //System.Threading.Thread.Sleep(5000);
                    doneCallback();

                }, metaOnly, exceptTypes);
            },
            () =>
            {
                if (m_root == null)
                {
                    m_root = new ProjectRoot();
                    m_root.Meta = new ProjectMeta();
                    m_root.Data = new ProjectData();
                    m_root.Item = ProjectItem.CreateFolder(projectName);
                }
                else
                {
                    if (m_root.Item == null)
                    {
                        m_root.Item = ProjectItem.CreateFolder(projectName);
                    }
                    else
                    {
                        m_root.Item.Name = projectName;
                    }
                }
                if (m_projectTemplate != null)
                {
                    ProjectItem newTemplateFolder = ProjectTemplateToProjectItem(projectName, m_projectTemplate);
                    if (m_root.Item.Children == null)
                    {
                        m_root.Item.Children = new List<ProjectItem>();
                    }
                    ProjectItem existingTemplateFolder = m_root.Item;//.Children.Where(item => item.Name == ProjectMangerConstants.PROJECT_TEMPLATE_FOLDER).FirstOrDefault();
                    ContinueLoadingProject(() =>
                    {
                        if (callback != null)
                        {
                            callback(m_root.Item);
                        }
                        if (ProjectLoaded != null)
                        {
                            ProjectLoaded(this, new ProjectManagerEventArgs(m_root.Item));
                        }
                    },
                    newTemplateFolder,
                    existingTemplateFolder);
                }
            });
        }


        private void ContinueLoadingProject(ProjectManagerCallback callback, ProjectItem newTemplateFolder, ProjectItem existingTemplateFolder)
        {
            List<ProjectItem> itemsToDelete = new List<ProjectItem>();
            if (existingTemplateFolder != null)
            {
                MergeData(newTemplateFolder, existingTemplateFolder);
                Diff(newTemplateFolder, existingTemplateFolder, itemsToDelete);
                m_root.Item = newTemplateFolder;
            }
            else
            {
                m_root.Item = newTemplateFolder;
            }

            m_project.Delete(itemsToDelete.ToArray(), deleteCompleted =>
            {
                EnumerateBundles(m_root.Item);
                LoadBundles(() =>
                {
                    m_project.Save(m_root.Item, false, saveRootCompleted =>
                    {
                        CompleteProjectLoading(callback);
                    });
                });
            });
        }

        private void CompleteProjectLoading(ProjectManagerCallback callback)
        {
            bool includeDynamic = false;
          
            Dictionary<long, UnityObject> resources = IdentifiersMap.FindResources(includeDynamic);

            bool allowNulls = false;

            m_loadedResources = new Dictionary<long, UnityObject>();
            FindDependencies(m_root.Item, m_loadedResources, resources, allowNulls);
            FindReferencedObjects(m_root.Item, m_loadedResources, resources, allowNulls);
            m_project.UnloadData(m_root.Item);

            m_isProjectLoaded = true;

            callback();
        }

        public void IgnoreTypes(params Type[] types)
        {
            for(int i = 0; i < types.Length; ++i)
            {
                PersistentDescriptor.IgnoreTypes.Add(types[i]);
            }
        }

        public override void SaveScene(ProjectItem scene, ProjectManagerCallback callback)
        {
            if (!m_isProjectLoaded)
            {
                throw new System.InvalidOperationException("project is not loaded");
            }
            if (!scene.IsScene)
            {
                throw new System.ArgumentException("is not a scene", "scene");
            }

     
            if(scene.Parent == null)
            {
                throw new ArgumentException("Scene does not have parent", "scene");
            }

            if(scene.Parent.Children.Where(c => c.NameExt.ToLower() == scene.NameExt).Count() > 1)
            {
                throw new ArgumentException("Scene with same name exists", "scene");
            }

            base.SaveScene(scene, callback);
        }

        public override void LoadScene(ProjectItem scene, ProjectManagerCallback callback)
        {
            if(!m_isProjectLoaded)
            {
                throw new InvalidOperationException("project is not loaded");
            }

            if(!scene.IsScene)
            {
                throw new ArgumentException("is not a scene", "scene");
            }

            DestroyDynamicResources();

            RaiseSceneLoading(scene);

            bool isEnabled = RuntimeUndo.Enabled;
            RuntimeUndo.Enabled = false;
            RuntimeSelection.objects = null;
            RuntimeUndo.Enabled = isEnabled;

            bool includeDynamicResources = false;
            Dictionary<long, UnityObject> allObjects = IdentifiersMap.FindResources(includeDynamicResources);
            Dictionary<long, UnityObject> sceneDependencies = new Dictionary<long, UnityObject>();

            bool dynamicOnly = true;
            Dictionary<long, ProjectItem> idToProjectItem = GetIdToProjectItemMapping(m_root.Item, dynamicOnly);

            m_project.LoadData(new[] { scene }, loadDataCompleted =>
            {
                scene = loadDataCompleted.Data[0];

                PersistentScene persistentScene = m_serializer.Deserialize<PersistentScene>(scene.Internal_Data.RawData);
                
                if (persistentScene.Data != null)
                {
                    for (int i = 0; i < persistentScene.Data.Length; ++i)
                    {
                        PersistentData sceneData = persistentScene.Data[i];
                        bool allowNulls = true;
                        sceneData.FindDependencies(sceneDependencies, allObjects, allowNulls);
                    }
                }

                List<ProjectItem> dynamicProjectItems = new List<ProjectItem>();
                foreach(KeyValuePair<long, UnityObject> sceneDependency in sceneDependencies)
                {
                    long instanceId = sceneDependency.Key;
                    UnityObject obj = sceneDependency.Value;
                    if(IdentifiersMap.IsDynamicResourceID(instanceId))
                    {
                        Debug.Assert(obj == null);

                        ProjectItem projectItem;
                        if(idToProjectItem.TryGetValue(instanceId, out projectItem))
                        {
                            dynamicProjectItems.Add(projectItem);
                        }
                    }
                }

                GetOrCreateObjects(dynamicProjectItems.ToArray(), idToProjectItem, () =>
                {
                    CompleteSceneLoading(scene, callback, isEnabled, persistentScene);
                });
            });
        }

        public override void CreateScene()
        {
            DestroyDynamicResources();

            base.CreateScene();
        }

        public void AddBundledResources(ProjectItem folder, string bundleName, Func<UnityObject, string, bool> filter, ProjectManagerCallback<ProjectItem[]> callback)
        {
            AddBundledResources(folder, bundleName, null, null,  filter, callback);
        }

        public void AddBundledResource(ProjectItem folder, string bundleName, string assetName, ProjectManagerCallback<ProjectItem[]> callback)
        {
            AddBundledResources(folder, bundleName, new[] { assetName }, null, (o, n) => true, callback);
        }

        public void AddBundledResource<T>(ProjectItem folder, string bundleName, string assetName, ProjectManagerCallback<ProjectItem[]> callback)
        {
            AddBundledResources(folder, bundleName, new[] { assetName }, new[] { typeof(T) }, (o, n) => true, callback);
        }

        public void AddBundledResource(ProjectItem folder, string bundleName, string assetName, Type assetType, ProjectManagerCallback<ProjectItem[]> callback)
        {
            AddBundledResources(folder, bundleName, new[] { assetName }, new[] { assetType }, (o, n) => true, callback);
        }

        public void AddBundledResources(ProjectItem folder, string bundleName, string[] assetNames, ProjectManagerCallback<ProjectItem[]> callback)
        {
            AddBundledResources(folder, bundleName, assetNames, null, (o, n) => true, callback);
        }

        private void LoadAssetBundle(string bundleName, AssetBundleEventHandler callback)
        {
            if(m_loadedBundles.ContainsKey(bundleName))
            {
                callback(bundleName, m_loadedBundles[bundleName].Bundle);
            }
            else
            {
                m_bundleLoader.Load(bundleName, callback);
            }
        }

        public void AddBundledResources(ProjectItem folder, string bundleName, string[] assetNames, Type[] assetTypes, Func<UnityObject, string, bool> filter, ProjectManagerCallback<ProjectItem[]> callback)
        {
            if (!m_isProjectLoaded)
            {
                throw new InvalidOperationException("project is not loaded");
            }

            if (string.IsNullOrEmpty(bundleName))
            {
                throw new ArgumentException("bandle name is not specified", "bundleName");
            }

            if (assetNames != null && assetNames.Length > 0)
            {
                if (assetNames.Length != assetNames.Distinct().Count())
                {
                    throw new ArgumentException("assetNames array contains duplicates", "assetNames");
                }

                if (assetTypes == null)
                {
                    assetTypes = new Type[assetNames.Length];
                }

                if (assetNames.Length != assetTypes.Length)
                {
                    throw new ArgumentException("asset types array should be of same size as the asset names array", "assetTypes");
                }
            }
            
            LoadAssetBundle(bundleName, (name, bundle) =>
            {
                if (bundle == null)
                {
                    throw new ArgumentException("unable to load bundle" + name, "bundleName");
                }

                LoadedAssetBundle loadedBundle;
                if (!m_loadedBundles.TryGetValue(name, out loadedBundle))
                {
                    loadedBundle = new LoadedAssetBundle
                    {
                        Bundle = bundle,
                        Usages = 0
                    };
                    m_loadedBundles.Add(name, loadedBundle);

                    TextAsset[] textAssets = LoadAllTextAssets(bundle);
                    RuntimeShaderUtil.AddExtra(name, textAssets);

                    IdentifiersMap.Instance.Register(bundle);
                }

                bool includeAllAssets = assetNames == null;
                if(includeAllAssets)
                {
                    assetNames = bundle.GetAllAssetNames();
                    assetTypes = new Type[assetNames.Length];
                }

                List<UnityObject> objects = new List<UnityObject>();
                for (int i = 0; i < assetNames.Length; ++i)
                {
                    string assetName = assetNames[i];
                    Type assetType = assetTypes[i];

                    UnityObject obj = assetType != null ? bundle.LoadAsset(assetName, assetType) : bundle.LoadAsset(assetName);
                    if (obj == null)
                    {
                        throw new ArgumentException("unable to load asset " + assetName + " " + assetType);
                    }
                    else
                    {
                        if(filter(obj, assetName) && AddBundledResourceInternalFilter(obj, assetName))
                        {
                            if(obj is Material)
                            {
                                Material material = (Material)obj;
                                if(material.shader != null && IdentifiersMap.IsNotMapped(material.shader))
                                {
                                    //trying to replace built-in shaders with shaders included to main package
                                    //material.shader = Shader.Find(material.shader.name);
                                }
                            }

                            objects.Add(obj);
                        }
                    }
                }

#if !UNITY_WEBGL && PROC_MATERIAL
                if (includeAllAssets)
                {
                    ProceduralMaterial[] proceduralMaterials = bundle.LoadAllAssets<ProceduralMaterial>();
                    for (int i = 0; i < proceduralMaterials.Length; ++i)
                    {
                        ProceduralMaterial procMaterial = proceduralMaterials[i];
                        if (objects.Contains(procMaterial))
                        {
                            continue;
                        }

                        if (filter(procMaterial, procMaterial.name) && AddBundledResourceInternalFilter(procMaterial, procMaterial.name))
                        {
                            objects.Add(procMaterial);
                        }
                    }
                }
#endif

                ProjectItem[] projectItems = ConvertObjectsToProjectItems(objects.ToArray(), false, bundleName, assetNames, assetTypes);
                projectItems = projectItems.OrderBy(p => p.NameExt).ToArray();
                for (int i = 0; i < projectItems.Length; ++i)
                {
                    ProjectItem projectItem = projectItems[i];

                    if(folder.Children != null)
                    {
                        //overwrite item with same name (if exists)
                        ProjectItem itemWithSameName = folder.Children.Where(p => p.NameExt == projectItem.NameExt).FirstOrDefault();
                        if (itemWithSameName != null)
                        {
                            folder.RemoveChild(itemWithSameName);
                        }
                        else
                        {
                            loadedBundle.Usages++;
                        }
                    }
                    else
                    {
                        loadedBundle.Usages++;
                    }

                    folder.AddChild(projectItem);
                }

                m_project.Save(projectItems, false, saveItemsCompleted =>
                {
                    bool includeDynamic = false;
                    Dictionary<long, UnityObject> resources = IdentifiersMap.FindResources(includeDynamic);
                    bool allowNulls = false;

                    for (int i = 0; i < projectItems.Length; ++i)
                    {
                        ProjectItem projectItem = projectItems[i];
                        FindDependencies(projectItem, m_loadedResources, resources, allowNulls);
                        FindReferencedObjects(projectItem, m_loadedResources, resources, allowNulls);
                    }

                    for (int i = 0; i < projectItems.Length; ++i)
                    {
                        ProjectItem projectItem = projectItems[i];
                        m_project.UnloadData(projectItem);
                    }

                    if (callback != null)
                    {
                        callback(projectItems);
                    }
                    if (BundledResourcesAdded != null)
                    {
                        BundledResourcesAdded(this, new ProjectManagerEventArgs(projectItems));
                    }
                });
            });
        }

        private bool AddBundledResourceInternalFilter(UnityObject obj, string assetName)
        {
            if (obj == null)
            {
                return false;
            }

            if(assetName.Contains("resourcemap"))
            {
                return false;
            }

            if(assetName.StartsWith("rt_shader"))
            {
                return false;
            }

            if(obj is TextAsset)
            {
                return false;
            }

            if (obj is Shader)
            {
                if (!obj.HasMappedInstanceID())
                {
                    Debug.LogWarningFormat("Shader {0} can't be added as bundled resource. Please consider adding it to main ResourceMap or bundle ResourceMap", obj.name);
                }
                return false;
            }

            if(!(obj is GameObject) && !(obj is Mesh) && !(obj is Material) && !(obj is Texture))
            {
                return false;
            }

            return PersistentData.CanCreate(obj);
        }

        public void AddDynamicResource(ProjectItem folder, UnityObject obj, ProjectManagerCallback<ProjectItem[]> callback)
        {
            AddDynamicResources(folder, new[] { obj }, false, o => true, callback);
        }

        public void AddDynamicResources(ProjectItem folder, UnityObject[] objects, ProjectManagerCallback<ProjectItem[]> callback)
        {
            AddDynamicResources(folder, objects, false, o => true, callback);
        }

        public void AddDynamicResource(ProjectItem folder, UnityObject obj, bool includingDependencies, Func<UnityObject, bool> filter, ProjectManagerCallback<ProjectItem[]> callback)
        {
            AddDynamicResources(folder, new[] { obj }, includingDependencies, filter, callback);
        }
 
        public void AddDynamicResources(ProjectItem folder, UnityObject[] objects,  bool includingDependencies, Func<UnityObject, bool> filter, ProjectManagerCallback<ProjectItem[]> callback)
        {
            if (!m_isProjectLoaded)
            {
                throw new InvalidOperationException("project is not loaded");
            }

            if(objects == null || objects.Length == 0)
            {
                throw new ArgumentNullException("objects array is null or empty", "objects");
            }

            if(objects.Distinct().Count() != objects.Length)
            {
                throw new ArgumentException("same object included to objects array multiple times", "objects");
            }


            Dictionary<long, UnityObject> allDependencies = new Dictionary<long, UnityObject>();
            if(includingDependencies)
            {
                HashSet<UnityObject> objectsHS = new HashSet<UnityObject>();
                while(objects.Length > 0)
                {
                    for (int i = 0; i < objects.Length; ++i)
                    {
                        UnityObject obj = objects[i];
                        if(AddDynamicResourceInternalFilter(obj) && filter(obj))
                        {
                            if (!objectsHS.Contains(obj))
                            {
                                objectsHS.Add(obj);
                            }
                        }
                        else
                        {
                            objects[i] = null;
                        }
                        
                    }

                    Dictionary<long, UnityObject> dependencies = new Dictionary<long, UnityObject>();
                    for (int i = 0; i < objects.Length; ++i)
                    {
                        UnityObject obj = objects[i];
                        if(obj != null)
                        {
                            GetDependencies(obj, dependencies);
                        }
                    }

                    objects = dependencies.Values.ToArray();
                    foreach(KeyValuePair<long, UnityObject> kvp in dependencies)
                    {
                        if(!allDependencies.ContainsKey(kvp.Key))
                        {
                            allDependencies.Add(kvp.Key, kvp.Value);
                        }
                    }
                }

                objects = objectsHS.ToArray();
            }
            else
            {
                for (int i = 0; i < objects.Length; ++i)
                {
                    UnityObject obj = objects[i];
                    if(AddDynamicResourceInternalFilter(obj) && filter(obj))
                    {
                        GetDependencies(obj, allDependencies);
                    }
                    else
                    {
                        objects[i] = null;
                    }
                }
                objects = objects.Where(o => o != null).ToArray();
            }
           
            //Components can't be added as project items, we need to replace them with gameobjects
            for(int i = 0; i < objects.Length; ++i)
            {
                UnityObject obj = objects[i];
                if(obj is Component)
                {
                    Component component = (Component)obj;
                    long goID = component.gameObject.GetMappedInstanceID();
                    if(!allDependencies.ContainsKey(goID))
                    {
                        allDependencies.Add(goID, component.gameObject);
                    }
                }
            }

            Dictionary<long, UnityObject> objectIdToDuplicate = new Dictionary<long, UnityObject>();
            DuplicateAndRegister(objects, objectIdToDuplicate);

            for (int i = 0; i < objects.Length; ++i)
            {
                UnityObject obj = objects[i];
                if (obj is Component)
                {
                    Component component = (Component)obj;
                    objects[i] = component.gameObject;
                }
            }

            for (int i = 0; i < objects.Length; ++i)
            {
                UnityObject obj = objects[i];
                obj.name = ProjectItem.GetUniqueName(obj.name, obj, folder);
            }

            ProjectItem[] projectItems = ConvertObjectsToProjectItems(objects, false);
            for (int i = 0; i < objects.Length; ++i)
            {
                UnityObject obj = objects[i];
                GetReferredObjects(obj, allDependencies);
            }

            //overwrite dependencies with duplicates
            foreach(KeyValuePair<long, UnityObject> kvp in objectIdToDuplicate)
            {
                if(allDependencies.ContainsKey(kvp.Key))
                {
                    allDependencies[kvp.Key] = kvp.Value;
                }
            }

            for (int i = 0; i < projectItems.Length; ++i)
            {
                ProjectItem projectItem = projectItems[i];
                PersistentData.RestoreDataAndResolveDependencies(projectItem.Internal_Data.PersistentData, allDependencies);
            }

            ProjectItem[] projectItemsResolvedDependencies = ConvertObjectsToProjectItems(objects, false);
            for(int i = 0; i < projectItemsResolvedDependencies.Length; ++i)
            {
                //Fix required to add texture 2d properly...
                projectItemsResolvedDependencies[i].Internal_Data.RawData = projectItems[i].Internal_Data.RawData;
            }
            projectItems = projectItemsResolvedDependencies;
            projectItems = projectItems.OrderBy(p => p.NameExt).ToArray(); 
            for (int i = 0; i < projectItems.Length; ++i)
            {
                ProjectItem projectItem = projectItems[i];
                folder.AddChild(projectItem);
            }

            m_project.Save(projectItems, false, saveItemsCompleted =>
            {
                bool includeDynamic = false;
                Dictionary<long, UnityObject> resources = IdentifiersMap.FindResources(includeDynamic);
                bool allowNulls = false;

                for (int i = 0; i < projectItems.Length; ++i)
                {
                    ProjectItem projectItem = projectItems[i];
                    FindDependencies(projectItem, m_loadedResources, resources, allowNulls);
                    FindReferencedObjects(projectItem, m_loadedResources, resources, allowNulls);
                }

                for (int i = 0; i < projectItems.Length; ++i)
                {
                    ProjectItem projectItem = projectItems[i];
                    m_project.UnloadData(projectItem);
                }

                m_project.SaveProjectMeta(Project.Name, m_root.Meta, saveProjectMetaCompleted =>
                {
                    if (callback != null)
                    {
                        callback(projectItems);
                    }

                    if (DynamicResourcesAdded != null)
                    {
                        DynamicResourcesAdded(this, new ProjectManagerEventArgs(projectItems));
                    }
                });
            });
        }

        private bool AddDynamicResourceInternalFilter(UnityObject obj)
        {
            if(obj == null)
            {
                return false;
            }

#if !UNITY_WEBGL && PROC_MATERIAL
            if(obj is ProceduralMaterial)
            {
                Debug.LogWarningFormat("ProceduralMaterials can't be added as dynamic resource. Procedural Material {0}. Please consider adding it to main ResourceMap or bundle ResourceMap", obj.name);
                return false;
            }
#endif

            if (obj is Texture2D)
            {
                Texture2D texutre = (Texture2D)obj;
                bool readable = texutre.IsReadable();
                if(!readable)
                {
                    Debug.LogWarningFormat("Texture {0} can't be added as dynamic resource. Please consider adding it to main ResourceMap or bundle ResourceMap", texutre.name);
                }
                return readable;
            }

            if(obj is Shader)
            {
                if(!obj.HasMappedInstanceID())
                {
                    Debug.LogWarningFormat("Shader {0} can't be added as dynamic resource. Please consider adding it to main ResourceMap or bundle ResourceMap", obj.name);
                }
                return false;
            }

            return PersistentData.CanCreate(obj); 
        }

        private void GetDependencies(UnityObject obj, Dictionary<long, UnityObject> objects)
        {
            PersistentData data = PersistentData.Create(obj);
            if(data == null)
            {
                return;
            }
            data.GetDependencies(obj, objects);

            if (obj is GameObject)
            {
                GameObject go = (GameObject)obj;
                Component[] components = go.GetComponents<Component>();
                for (int i = 0; i < components.Length; ++i)
                {
                    Component component = components[i];
                    if(component != null)
                    {
                        data = PersistentData.Create(component);
                        data.GetDependencies(component, objects);
                    }
                }

                foreach (Transform child in go.transform)
                {
                    GetDependencies(child.gameObject, objects);
                }
            }
        }

        private void GetReferredObjects(UnityObject obj, Dictionary<long, UnityObject> objects)
        {
            long instanceId = obj.GetMappedInstanceID();
            if (!objects.ContainsKey(instanceId))
            {
                objects.Add(instanceId, obj);
            }

            if (obj is GameObject)
            {
                GameObject go = (GameObject)obj;
                Component[] components = go.GetComponents<Component>();
                for(int i = 0; i < components.Length; ++i)
                {
                    Component component = components[i];
                    instanceId = component.GetMappedInstanceID();
                    if (!objects.ContainsKey(instanceId))
                    {
                        objects.Add(instanceId, component);
                    }
                }

                foreach(Transform child in go.transform)
                {
                    GetReferredObjects(child.gameObject, objects);
                }
            }
        }

        private void DuplicateAndRegister(UnityObject[] objects, Dictionary<long, UnityObject> objIdToDuplicate)
        {
            for (int i = 0; i < objects.Length; ++i)
            {
                UnityObject obj = objects[i];
                bool isActive = false;
                bool isComponent = obj is Component;
                bool isGameObject = obj is GameObject;
                GameObject go = null;
                if(isComponent)
                {
                    Component component = (Component)obj;
                    go = component.gameObject;
                }
                else if(isGameObject)
                {
                    go = (GameObject)obj;
                }

                if(go != null)
                {
                    isActive = go.activeSelf;
                    go.SetActive(false);
                }

                UnityObject duplicate = Instantiate(objects[i]);
                duplicate.name = obj.name;
                objects[i] = duplicate;

                GameObject duplicateGo = null;
                if(isComponent)
                {
                    Component component = (Component)duplicate;
                    duplicateGo = component.gameObject;
                    if (isComponent)
                    {
                        objIdToDuplicate.Add(go.GetMappedInstanceID(), duplicateGo);
                    }
                }
                else if(isGameObject)
                {
                    duplicateGo = (GameObject)duplicate;
                }
                
                objIdToDuplicate.Add(obj.GetMappedInstanceID(), duplicate);
               
                if (go != null)
                {
                    go.SetActive(isActive);

                    RegisterDynamicResource(duplicateGo,
                        () =>
                        {
                            if (m_root.Meta.Counter < 0)
                            {
                                throw new InvalidOperationException("identifiers exhausted");
                            }
                            m_root.Meta.Counter++;
                            return m_root.Meta.Counter;
                        });

                    duplicateGo.transform.SetParent(m_dynamicResourcesRoot, true);
                    duplicateGo.hideFlags = HideFlags.HideAndDontSave;
                    duplicateGo.SetActive(isActive);

                    Component[] components = duplicateGo.GetComponentsInChildren<Component>();
                    for (int j = 0; j < components.Length; ++j)
                    {
                        if(components[j] != null)
                        {
                            components[j].gameObject.hideFlags = HideFlags.HideAndDontSave;
                            components[j].hideFlags = HideFlags.HideAndDontSave;
                        }
                    }
                }
                else
                {
                    if (IdentifiersMap.IsNotMapped(duplicate))
                    {
                        if (m_root.Meta.Counter < 0)
                        {
                            throw new InvalidOperationException("identifiers exhausted");
                        }
                        m_root.Meta.Counter++;
                        IdentifiersMap.Instance.Register(duplicate, m_root.Meta.Counter);

                        long mappedInstanceId = duplicate.GetMappedInstanceID();
                        if (!m_loadedResources.ContainsKey(mappedInstanceId))
                        {
                            m_loadedResources.Add(mappedInstanceId, duplicate);
                        }
                    }
                }
            }
        }

        public void CreateFolder(string name, ProjectItem parent, ProjectManagerCallback<ProjectItem> callback)
        {
            ProjectItem folder = ProjectItem.CreateFolder(name);
            parent.AddChild(folder);
            folder.Name = ProjectItem.GetUniqueName(name, folder, parent);

            m_project.Save(folder, true, saveCompleted =>
            {
                if (callback != null)
                {
                    callback(folder);
                }
            });
        }

        public void SaveObjects(ProjectItemObjectPair[] itemObjectPairs, ProjectManagerCallback callback)
        {
            if(itemObjectPairs == null || itemObjectPairs.Length == 0)
            {
                callback();
                return;
            }

            SaveObjectsToProjectItems(itemObjectPairs);

            ProjectItem[] projectItems = itemObjectPairs.Select(i => i.ProjectItem).ToArray();

            m_project.Save(projectItems, false, saveCompleted =>
            {
                for(int i = 0; i < projectItems.Length; ++i)
                {
                    ProjectItem projectItem = projectItems[i];
                    m_project.UnloadData(projectItem);
                }

                if(callback != null)
                {
                    callback();
                }
            });
        }

        public void GetOrCreateObjects(ProjectItem folder, ProjectManagerCallback<ProjectItemObjectPair[]> callback)
        {
            if (folder == null || folder.Children == null || folder.Children.Count == 0)
            {
                callback(new ProjectItemObjectPair[0]);
                return;
            }
            ProjectItem[] projectItems = folder.Children.ToArray();
            GetOrCreateObjectsFromProjectItems(projectItems, callback);
        }

        public void GetOrCreateObjects(ProjectItem[] projectItems, ProjectManagerCallback<ProjectItemObjectPair[]> callback)
        {
            if(projectItems == null || projectItems.Length == 0)
            {
                callback(new ProjectItemObjectPair[0]);
                return;
            }

            ProjectItem[] files = projectItems
                .Where(item => item.Children != null && item.Children.Count > 0)
                .SelectMany(item => item.Children)
                .Union(projectItems.Where(item => !item.IsFolder)).ToArray();

            GetOrCreateObjectsFromProjectItems(files, callback);
        }

        private void GetOrCreateObjectsFromProjectItems(ProjectItem[] projectItems, ProjectManagerCallback<ProjectItemObjectPair[]> callback)
        {
            ProjectItem[] scenes = projectItems.Where(p => p.IsScene).OrderBy(p => p.Name).ToArray();
            ProjectItem[] folders = projectItems.Where(p => p.IsFolder).OrderBy(p => p.Name).ToArray();
            List<ProjectItemObjectPair> result = new List<ProjectItemObjectPair>();
            for(int i = 0; i < folders.Length; ++i)
            {
                ProjectItemWrapper wrapper = ScriptableObject.CreateInstance<ProjectItemWrapper>();
                wrapper.ProjectItem = folders[i];
                result.Add(new ProjectItemObjectPair(wrapper.ProjectItem, wrapper));
            }
            for (int i = 0; i < scenes.Length; ++i)
            {
                ProjectItemWrapper wrapper = ScriptableObject.CreateInstance<ProjectItemWrapper>();
                wrapper.ProjectItem = scenes[i];
                result.Add(new ProjectItemObjectPair(wrapper.ProjectItem, wrapper));
            }

            projectItems = projectItems.Where(p => !p.IsFolder && !p.IsScene).ToArray();

            bool dynamicOnly = true;
            Dictionary<long, ProjectItem> idToProjectItem = GetIdToProjectItemMapping(m_root.Item, dynamicOnly);

            GetOrCreateObjects(projectItems, idToProjectItem, () =>
            {
                projectItems = projectItems.OrderBy(item => item.Name).ToArray();
                for (int i = 0; i < projectItems.Length; i++)
                {
                    ProjectItem projectItem = projectItems[i];
                    UnityObject obj;
                    if (m_loadedResources.TryGetValue(projectItem.Internal_Meta.Descriptor.InstanceId, out obj))
                    {
                        result.Add(new ProjectItemObjectPair(projectItem, obj));
                    }
                }

                if (callback != null)
                {
                    callback(result.ToArray());
                }
            });
        }

        private void GetOrCreateObjects(ProjectItem[] projectItems, Dictionary<long, ProjectItem> idToProjectItem, ProjectManagerCallback callback)
        {
            if(projectItems == null || projectItems.Length == 0)
            {
                if (callback != null)
                {
                    callback();
                }
            }
            else
            {
                Dictionary<long, ProjectItem> loadedProjectItemsDictionary = new Dictionary<long, ProjectItem>();
                LoadProjectItemsAndDependencies(projectItems, idToProjectItem, loadedProjectItemsDictionary, () =>
                {
                    ProjectItem[] loadedProjectItems = loadedProjectItemsDictionary.Values.ToArray();
                    List<GameObject> createdGameObjects = new List<GameObject>();
                    for (int i = 0; i < loadedProjectItems.Length; ++i)
                    {
                        ProjectItem projectItem = loadedProjectItems[i];
                        Dictionary<long, UnityObject> decomposition = null;
                        if (projectItem.IsGameObject)
                        {
                            decomposition = new Dictionary<long, UnityObject>();
                        }

                        UnityObject obj = GetOrCreateObject(projectItem, m_loadedResources, decomposition);
                        RegisterDynamicResource(InstanceID(projectItem), obj, decomposition);
                        
                        if (obj is GameObject && IsDynamicResource(projectItem))
                        {
                            createdGameObjects.Add((GameObject)obj);
                        }
                    }

                    for (int i = 0; i < loadedProjectItems.Length; ++i)
                    {
                        ProjectItem projectItem = loadedProjectItems[i];
                      
                        Dictionary<long, UnityObject> dependencies = new Dictionary<long, UnityObject>();
                        FindDependencies(projectItem, dependencies, m_loadedResources, false);
                        FindReferencedObjects(projectItem, dependencies, m_loadedResources, false);
                        RestoreDataAndResolveDependencies(projectItem, dependencies);
                    }  

                    for(int i = 0; i < createdGameObjects.Count; ++i)
                    {
                        GameObject go = createdGameObjects[i];
                        go.transform.SetParent(m_dynamicResourcesRoot, true);
                        go.hideFlags = HideFlags.HideAndDontSave; 
                    }

                    for (int i = 0; i < loadedProjectItems.Length; ++i)
                    {
                        ProjectItem projectItem = loadedProjectItems[i];

                        m_project.UnloadData(projectItem);
                    }

                    if (callback != null)
                    {
                        callback();
                    }
                });
            }
        }

        public void Duplicate(ProjectItem[] projectItems, ProjectManagerCallback<ProjectItem[]> callback)
        {
            if (!m_isProjectLoaded)
            {
                throw new InvalidOperationException("project is not loaded");
            }

            projectItems = ProjectItem.GetRootItems(projectItems);
            List<ProjectItem> allOriginalProjectItems = new List<ProjectItem>();
            for (int i = 0; i < projectItems.Length; ++i)
            {
                allOriginalProjectItems.AddRange(projectItems[i].FlattenHierarchy(true));
            }

            m_project.LoadData(allOriginalProjectItems.ToArray(), loadDataCompleted =>
            {
                ProjectItem[] parents = new ProjectItem[loadDataCompleted.Data.Length];
                for(int i = 0; i < loadDataCompleted.Data.Length; ++i)
                {
                    parents[i] = loadDataCompleted.Data[i].Parent;
                    loadDataCompleted.Data[i].Parent = null;
                }

                projectItems = m_serializer.DeepClone(loadDataCompleted.Data);
                for (int i = 0; i < projectItems.Length; ++i)
                {
                    loadDataCompleted.Data[i].Parent = parents[i];
                    projectItems[i].Parent = parents[i];
                }

                for (int i = 0; i < allOriginalProjectItems.Count; ++i)
                {
                    m_project.UnloadData(allOriginalProjectItems[i]);
                }

                ProjectItem[] rootProjectItemCopies = ProjectItem.GetRootItems(projectItems);
                for (int i = 0; i < rootProjectItemCopies.Length; ++i)
                {
                    ProjectItem projectItem = rootProjectItemCopies[i];
                    projectItem.IsExposedFromEditor = false;

                    string newName = ProjectItem.GetUniqueName(projectItem.Name, rootProjectItemCopies[i], rootProjectItemCopies[i].Parent, false);

                    if (newName != projectItem.NameExt)
                    {
                        projectItem.NameExt = newName;
                        if (!projectItem.IsFolder && !projectItem.IsScene)
                        {
                            projectItem.Rename(projectItem.Name);
                        }
                    }
                }
            
                for (int i = 0; i < projectItems.Length; ++i)
                {
                    ProjectItem projectItem = projectItems[i];
                    if (projectItem.IsFolder || projectItem.IsScene)
                    {
                        continue;
                    }

                    LoadedAssetBundle loadedAssetBundle;
                    if (!string.IsNullOrEmpty(projectItem.BundleName) && m_loadedBundles.TryGetValue(projectItem.BundleName, out loadedAssetBundle))
                    {
                        loadedAssetBundle.Usages++;
                    }                 

                    //  m_project.SetProjectItemHideFlags(projectItem, HideFlags.HideAndDontSave);

                    PersistentDescriptor[] descriptors = projectItem.Internal_Meta.Descriptor.FlattenHierarchy();
                    if (projectItem.Internal_Data.PersistentData != null)
                    {
                        Dictionary<long, PersistentData> idToData = projectItem.Internal_Data.PersistentData.ToDictionary(item => item.InstanceId);
                        HashSet<PersistentTransform> transformsHS = new HashSet<PersistentTransform>();
                        for(int j = 0; j < descriptors.Length; ++j)
                        {
                            m_root.Meta.Counter++;
                            PersistentDescriptor descriptor = descriptors[j];
                            PersistentData data;
                            if(idToData.TryGetValue(descriptor.InstanceId, out data))
                            {
                                data.InstanceId = IdentifiersMap.ToDynamicResourceID(m_root.Meta.Counter);
                                if(data is PersistentTransform)
                                {
                                    PersistentTransform persistentTransform = (PersistentTransform)data;
                                    if(!transformsHS.Contains(persistentTransform))
                                    {
                                        transformsHS.Add(persistentTransform);
                                    }
                                    
                                }
                            }
                            descriptor.InstanceId = IdentifiersMap.ToDynamicResourceID(m_root.Meta.Counter);
                        }
                        foreach(PersistentTransform persistentTransform in transformsHS)
                        {
                            PersistentData parentData;
                            if(idToData.TryGetValue(persistentTransform.parent, out parentData))
                            {
                                persistentTransform.parent = parentData.InstanceId;
                            }
                        }
                    }
                    else
                    {
                        Debug.Assert(descriptors.Length == 1);
                        m_root.Meta.Counter++;
                        descriptors[0].InstanceId = IdentifiersMap.ToDynamicResourceID(m_root.Meta.Counter);
                    }
                }

                m_project.Save(projectItems.ToArray(), false, saveItemsCompleted =>
                {
                    for (int i = 0; i < projectItems.Length; ++i)
                    {
                        ProjectItem projectItem = projectItems[i];
                        m_project.UnloadData(projectItem);
                    }

                    m_project.SaveProjectMeta(Project.Name, m_root.Meta, saveProjectMetaCompleted =>
                    {
                        if (callback != null)
                        {
                            callback(projectItems); 
                        }
                    });
                });
            });
        }

        public void Rename(ProjectItem projectItem, string newName, ProjectManagerCallback callback)
        {
            m_project.Rename(projectItem, newName, renameCompleted =>
            {
                if (callback != null)
                {
                    callback();
                }
            });
        }

        public void Move(ProjectItem[] projectItems, ProjectItem folder, ProjectManagerCallback callback)
        {
            if (!m_isProjectLoaded)
            {
                throw new InvalidOperationException("project is not loaded");
            }

            projectItems = ProjectItem.GetRootItems(projectItems);
            projectItems = projectItems.Where(
                item => folder.Children == null || folder.Children.Contains(item) || !folder.Children.Any(c => c.NameExt == item.NameExt)).ToArray();
            if(projectItems.Length == 0)
            {
                throw new InvalidOperationException("Can't move items");
            }

            m_project.Move(projectItems, folder, moveCompleted =>
            {
                if(callback != null)
                {
                    callback();
                }
            });
        }

        public void Delete(ProjectItem[] projectItems, ProjectManagerCallback callback)
        {
            if (!m_isProjectLoaded)
            {
                throw new InvalidOperationException("project is not loaded");
            }

            if(projectItems.Any(pi => pi.IsResource && !IsDynamicResource(pi) && string.IsNullOrEmpty(pi.BundleName)))
            {
                throw new ArgumentException("Unable to remove non-dynamic and non-bundled projectItems", "projectItems");
            }

            Dictionary<long, ProjectItem> idToProjectItemMapping = new Dictionary<long, ProjectItem>();
            bool bundleUnloaded = false;
            for(int i = 0; i < projectItems.Length; ++i)
            {
                ProjectItem projectItem = projectItems[i];
                bool dynamicOnly = false;
                GetIdToProjectItemMapping(projectItem, idToProjectItemMapping, dynamicOnly);

                if(!string.IsNullOrEmpty(projectItem.BundleName))
                {
                    LoadedAssetBundle loadedBundle = m_loadedBundles[projectItem.BundleName];
                    loadedBundle.Usages--;
                    if(loadedBundle.Usages <= 0)
                    {
                        m_loadedBundles.Remove(projectItem.BundleName);
                        if(loadedBundle.Bundle != null)
                        {
                            bool unloadAllLoadedObjects = true;
                            RuntimeShaderUtil.RemoveExtra(projectItem.BundleName);
                            IdentifiersMap.Instance.Unregister(loadedBundle.Bundle);
                            loadedBundle.Bundle.Unload(unloadAllLoadedObjects);                            
                            bundleUnloaded = true;
                        }
                    }
                }
            }

            if(bundleUnloaded)
            {
                List<long> nullKeys = new List<long>();
                foreach(KeyValuePair<long, UnityObject> kvp in m_loadedResources)
                {
                    if(kvp.Value == null)
                    {
                        nullKeys.Add(kvp.Key);
                    }
                }

                for(int i = 0; i < nullKeys.Count; ++i)
                {
                    m_loadedResources.Remove(nullKeys[i]);
                }
            }

            foreach(long instanceId in idToProjectItemMapping.Keys)
            {
                if (!IdentifiersMap.IsDynamicResourceID(instanceId))
                {
                    continue;
                }

                UnityObject obj;
                if (m_loadedResources.TryGetValue(instanceId, out obj))
                {    
                    if(!(obj is Component))
                    {
                        Destroy(obj);
                    }
                    
                    m_loadedResources.Remove(instanceId);
                }
                m_dynamicResources.Remove(instanceId);
            }

            m_project.Delete(projectItems, deleteCompleted =>
            {
                for(int i = 0; i < projectItems.Length; ++i)
                {
                    ProjectItem projectItem = projectItems[i];
                    m_project.UnloadData(projectItem);
                    if (projectItem.Parent != null)
                    {
                        projectItem.Parent.RemoveChild(projectItem);
                    }
                }
                if(callback != null)
                {
                    callback();
                }
            });
        }

        private void LoadProjectItemsAndDependencies(ProjectItem[] projectItems, Dictionary<long, ProjectItem> idToProjectItem, Dictionary<long, ProjectItem> processedProjectItems, ProjectManagerCallback callback)
        {
            int[] exceptTypes = { ProjectItemTypes.Scene };

            List<ProjectItem> loadProjectItems = new List<ProjectItem>();
            for(int i = 0; i < projectItems.Length; ++i)
            {
                ProjectItem projectItem = projectItems[i];
                if(!processedProjectItems.ContainsKey(InstanceID(projectItem)))
                {
                    loadProjectItems.Add(projectItem);
                }
            }

            if(loadProjectItems.Count == 0)
            {
                if (callback != null)
                {
                    callback();
                }
            }
            else
            {
                IJob job = Dependencies.Job;
                ProjectItem[] loadedProjectItems = new ProjectItem[0];
                job.Submit(doneCallback =>
                {
                    m_project.LoadData(loadProjectItems.ToArray(), loadProjectItemsCompleted =>
                    {
                        loadedProjectItems = loadProjectItemsCompleted.Data;
                        doneCallback();

                    }, exceptTypes);
                },
                () =>
                {
                    Dictionary<long, ProjectItem> dependencies = new Dictionary<long, ProjectItem>();

                    for (int i = 0; i < loadedProjectItems.Length; ++i)
                    {
                        ProjectItem projectItem = loadedProjectItems[i];
                        if (!processedProjectItems.ContainsKey(InstanceID(projectItem)))
                        {
                            processedProjectItems.Add(InstanceID(projectItem), projectItem);
                            FindDependencies(projectItem, dependencies, idToProjectItem);
                        }
                    }

                    if (dependencies.Count > 0)
                    {
                        LoadProjectItemsAndDependencies(dependencies.Values.ToArray(), idToProjectItem, processedProjectItems, callback);
                    }
                    else
                    {
                        if (callback != null)
                        {
                            callback();
                        }
                    }

                });
            } 
        }

        private void RegisterDynamicResource(long mappedInstanceId, UnityObject dynamicResource, Dictionary<long, UnityObject> decomposition)
        {
            if(!IdentifiersMap.IsDynamicResourceID(mappedInstanceId))
            {
                return;
            }
            
            IdentifiersMap.Instance.Register(dynamicResource, mappedInstanceId);
            if (!m_dynamicResources.ContainsKey(mappedInstanceId))
            {
                m_dynamicResources.Add(mappedInstanceId, dynamicResource);

                if (dynamicResource is GameObject)
                {
                    GameObject dynamicResourceGO = (GameObject)dynamicResource;
                    dynamicResourceGO.transform.SetParent(m_dynamicResourcesRoot, true);
                    dynamicResourceGO.hideFlags = HideFlags.HideAndDontSave;
                }
            }

            if (decomposition != null)
            {
                foreach (KeyValuePair<long, UnityObject> decompositionKVP in decomposition)
                {
                    long instanceId = decompositionKVP.Key;
                    UnityObject obj = decompositionKVP.Value;
                    IdentifiersMap.Instance.Register(obj, instanceId);
                    if (!m_dynamicResources.ContainsKey(instanceId))
                    {
                        m_dynamicResources.Add(instanceId, obj);
                    }
                }
            }
        }

        private void RegisterDynamicResource(GameObject obj, System.Func<int> id)
        {
            IdentifiersMap.Instance.Register(obj, id());
            long mappedInstanceId = obj.GetMappedInstanceID();
            if(!m_loadedResources.ContainsKey(mappedInstanceId))
            {
                m_loadedResources.Add(mappedInstanceId, obj);
            }

            Component[] components = obj.GetComponents<Component>();
            for (int i = 0; i < components.Length; ++i)
            {
                Component component = components[i];
                if(component != null)
                {
                    IdentifiersMap.Instance.Register(component, id());
                    mappedInstanceId = component.GetMappedInstanceID();
                    if (!m_loadedResources.ContainsKey(mappedInstanceId))
                    {
                        m_loadedResources.Add(mappedInstanceId, component);
                    }
                }
            }

            foreach (Transform child in obj.transform)
            {
                RegisterDynamicResource(child.gameObject, id);
            }
        }

        private ProjectItem ProjectTemplateToProjectItem(string projectName, FolderTemplate projectTemplate)
        {
            ProjectItem root = ProjectItem.CreateFolder(projectName);
            ProjectTemplateToProjectItem(projectTemplate, root);
            root.Name = projectName;
            return root;
        }

        private static void ProjectTemplateToProjectItem(FolderTemplate template, ProjectItem folder)
        {
            folder.IsExposedFromEditor = true;
            folder.Name = template.name;
            foreach (Transform child in template.transform)
            {
                FolderTemplate childTemplate = child.GetComponent<FolderTemplate>();
                if (childTemplate != null)
                {
                    ProjectItem childFolder = ProjectItem.CreateFolder(childTemplate.name);
                    folder.AddChild(childFolder);
                    ProjectTemplateToProjectItem(childTemplate, childFolder);
                }
            }


            UnityObject[] templateObjects = template.Objects.Where(obj => obj != null).ToArray();
            ProjectItem[] projectItems = ConvertObjectsToProjectItems(templateObjects, true);
            for (int i = 0; i < projectItems.Length; ++i)
            {
                folder.AddChild(projectItems[i]);
            }
        }

        private static ProjectItem[] ConvertObjectsToProjectItems(UnityObject[] objects, bool isExposedFromEditor, string bundleName = null, string[] assetNames = null, Type[] assetTypes = null)
        {
            if(objects == null)
            {
                return null;
            }

            if(objects.Length == 0)
            {
                return new ProjectItem[0];
            }
            List<ProjectItem> projectItemsList = new List<ProjectItem>();
            List<ProjectItem> goProjectItemsList = new List<ProjectItem>();
            List<ProjectItem> objProjectItemsList = new List<ProjectItem>();
            List<GameObject> gameObjectsList = new List<GameObject>();
            List<UnityObject> objectsList = new List<UnityObject>();
            for (int i = 0; i < objects.Length; ++i)
            {
                UnityObject obj = objects[i];
                ProjectItem projectItem = new ProjectItem();
                if (obj is GameObject)
                {
                    gameObjectsList.Add((GameObject)obj);
                    goProjectItemsList.Add(projectItem);
                }
                else
                {
                    objectsList.Add(obj);
                    objProjectItemsList.Add(projectItem);
                }

                projectItem.Internal_Meta = new ProjectItemMeta();
                if(!string.IsNullOrEmpty(bundleName))
                {
                    projectItem.Internal_Meta.BundleDescriptor = new AssetBundleDescriptor
                    {
                        AssetName = assetNames.Length > i ? assetNames[i] : null,
                        TypeName = assetNames.Length > i && assetTypes[i] != null ? assetTypes[i].AssemblyQualifiedName : null,
                        BundleName = bundleName
                    };
                }

                projectItemsList.Add(projectItem);
            }

            
            if (gameObjectsList.Count > 0)
            {
                PersistentDescriptor[] goDescriptors;
                PersistentData[][] goData;
                PersistentData.CreatePersistentDescriptorsAndData(gameObjectsList.ToArray(), out goDescriptors, out goData);
                for (int i = 0; i < goDescriptors.Length; ++i)
                {
                    ProjectItem projectItem = goProjectItemsList[i];

                    projectItem.Internal_Meta.Descriptor = goDescriptors[i];
                    projectItem.Internal_Meta.Name = gameObjectsList[i].name;
                    projectItem.Internal_Meta.TypeCode = ProjectItemTypes.GetProjectItemType(typeof(GameObject));
                    projectItem.Internal_Meta.IsExposedFromEditor = isExposedFromEditor;

                    ProjectItemData data = new ProjectItemData
                    {
                        PersistentData = goData[i]
                    };
                    projectItem.Internal_Data = data;
                }
            }
       
            if(objectsList.Count > 0)
            {
                UnityObject[] objectsArray = objectsList.ToArray();
                PersistentDescriptor[] oDescriptors = PersistentDescriptor.CreatePersistentDescriptors(objectsArray);
                PersistentData[] oData = PersistentData.CreatePersistentData(objectsArray);
                for (int i = 0; i < oDescriptors.Length; ++i)
                {
                    UnityObject obj = objectsArray[i];
                    ProjectItem projectItem = objProjectItemsList[i];

                    projectItem.Internal_Meta.Descriptor = oDescriptors[i];
                    projectItem.Internal_Meta.Name = obj.name;
                    projectItem.Internal_Meta.TypeCode = ProjectItemTypes.GetProjectItemType(obj.GetType());
                    projectItem.Internal_Meta.IsExposedFromEditor = isExposedFromEditor;
                    
                    ProjectItemData data = CreateProjectItemData(oData[i], obj);
                    projectItem.Internal_Data = data;

                    TryReadRawData(projectItem, obj); 
                }
            }

            return projectItemsList.ToArray();
        }

        private static ProjectItemData CreateProjectItemData(PersistentData oData, UnityObject obj)
        {
            ProjectItemData result =  new ProjectItemData
            {
                PersistentData = new[] { oData }
            };

            return result;
        }

        private static void SaveObjectsToProjectItems(ProjectItemObjectPair[] itemToObjectPairs)
        {
            if (itemToObjectPairs == null)
            {
                return;
            }

            if (itemToObjectPairs.Length == 0)
            {
                return;
            }

            List<ProjectItemObjectPair> gameObjectsList = new List<ProjectItemObjectPair>();
            List<ProjectItemObjectPair> objectsList = new List<ProjectItemObjectPair>();
            for (int i = 0; i < itemToObjectPairs.Length; ++i)
            {
                ProjectItemObjectPair itemToObject = itemToObjectPairs[i];
                if (itemToObject.Object is GameObject)
                {
                    gameObjectsList.Add(itemToObject);
                }
                else
                {
                    objectsList.Add(itemToObject);
                }
            }

            if (gameObjectsList.Count > 0)
            {
                PersistentDescriptor[] goDescriptors;
                PersistentData[][] goData;
                PersistentData.CreatePersistentDescriptorsAndData(gameObjectsList.Select(o => (GameObject)o.Object).ToArray(), out goDescriptors, out goData);
                for (int i = 0; i < goDescriptors.Length; ++i)
                {
                    ProjectItemMeta meta = new ProjectItemMeta
                    {
                        Descriptor = goDescriptors[i],
                        Name = gameObjectsList[i].Object.name,
                        TypeCode = ProjectItemTypes.GetProjectItemType(typeof(GameObject)),
                        IsExposedFromEditor = gameObjectsList[i].ProjectItem.IsExposedFromEditor,
                        BundleDescriptor = gameObjectsList[i].ProjectItem.Internal_Meta.BundleDescriptor
                    };

                    ProjectItemData data = new ProjectItemData
                    {
                        PersistentData = goData[i]
                    };

                    ProjectItem projectItem = gameObjectsList[i].ProjectItem;
                    projectItem.Internal_Meta = meta;
                    projectItem.Internal_Data = data;
                }
            }

            if (objectsList.Count > 0)
            {
                UnityObject[] objectsArray = objectsList.Select(o => o.Object).ToArray();
                PersistentDescriptor[] oDescriptors = PersistentDescriptor.CreatePersistentDescriptors(objectsArray);
                PersistentData[] oData = PersistentData.CreatePersistentData(objectsArray);
           
                for (int i = 0; i < oDescriptors.Length; ++i)
                {
                    ProjectItemMeta meta = new ProjectItemMeta
                    {
                        Descriptor = oDescriptors[i],
                        Name = objectsArray[i].name,
                        TypeCode = ProjectItemTypes.GetProjectItemType(objectsArray[i].GetType()),
                        IsExposedFromEditor = objectsList[i].ProjectItem.IsExposedFromEditor,
                        BundleDescriptor = objectsList[i].ProjectItem.Internal_Meta.BundleDescriptor
                    };

                    ProjectItemData data = new ProjectItemData
                    {
                        PersistentData = new[] { oData[i] }
                    };

                    ProjectItem projectItem = objectsList[i].ProjectItem;
                    projectItem.Internal_Meta = meta;
                    projectItem.Internal_Data = data;
                    TryReadRawData(projectItem, objectsArray[i]);
                }
            }
        }

        private static long InstanceID(ProjectItem projectItem)
        {
            return projectItem.Internal_Meta.Descriptor.InstanceId;
        }

        private static bool IsDynamicResource(ProjectItem projectItem)
        {
            if (projectItem.Internal_Meta.Descriptor == null)
            {
                return false;
            }
            return IdentifiersMap.IsDynamicResourceID(projectItem.Internal_Meta.Descriptor.InstanceId);
        }

        private static UnityObject GetOrCreateObject(ProjectItem projectItem, Dictionary<long, UnityObject> allResources, Dictionary<long, UnityObject> decomposition = null)
        {
            if (projectItem.IsFolder)
            {
                throw new InvalidOperationException("Operation is invalid for Folder");
            }

            UnityObject obj = PersistentDescriptor.GetOrCreateObject(projectItem.Internal_Meta.Descriptor, allResources, decomposition);
            return obj;
        }

        private static void RestoreDataAndResolveDependencies(ProjectItem projectItem, Dictionary<long, UnityObject> objects)
        {
            if (projectItem.Internal_Data.PersistentData != null)
            {
                PersistentData.RestoreDataAndResolveDependencies(projectItem.Internal_Data.PersistentData, objects);
            }

            TryLoadRawData(projectItem, objects);
        }

        private static void FindDependencies(ProjectItem item, Dictionary<long, ProjectItem> dependencies, Dictionary<long, ProjectItem> identifiersMapping)
        {
            bool allowNulls = false;
            if (item.Internal_Data != null)
            {
                PersistentData[] persistentData = item.Internal_Data.PersistentData;
                for (int i = 0; i < persistentData.Length; ++i)
                {
                    persistentData[i].FindDependencies(dependencies, identifiersMapping, allowNulls);
                }
            }

            if (item.Children != null)
            {
                for (int i = 0; i < item.Children.Count; ++i)
                {
                    ProjectItem child = item.Children[i];
                    FindDependencies(child, dependencies, identifiersMapping);
                }
            }
        }

        private static void FindDependencies(ProjectItem item, Dictionary<long, UnityObject> dependencies, Dictionary<long, UnityObject> allResources, bool allowNulls)
        {
            if (item.Internal_Data != null)
            {
                PersistentData[] persistentData = item.Internal_Data.PersistentData;
                for (int i = 0; i < persistentData.Length; ++i)
                {
                    persistentData[i].FindDependencies(dependencies, allResources, allowNulls);
                }
            }

            if (item.Children != null)
            {
                for (int i = 0; i < item.Children.Count; ++i)
                {
                    ProjectItem child = item.Children[i];
                    FindDependencies(child, dependencies, allResources, allowNulls);
                }
            }
        }

        private static void FindReferencedObjects(ProjectItem item, Dictionary<long, UnityObject> referencedObjects, Dictionary<long, UnityObject> allResources, bool allowNulls)
        {
            if (item.Internal_Meta.Descriptor != null)
            {
                Debug.Assert(!item.IsFolder && !item.IsScene);
                item.Internal_Meta.Descriptor.FindReferencedObjects(referencedObjects, allResources, allowNulls);
            }

            if (item.Children != null)
            {
                for (int i = 0; i < item.Children.Count; ++i)
                {
                    ProjectItem childItem = item.Children[i];
                    FindReferencedObjects(childItem, referencedObjects, allResources, allowNulls);
                }
            }
        }

        private static Dictionary<long, ProjectItem> GetIdToProjectItemMapping(ProjectItem projectItem, bool dynamicOnly)
        {
            Dictionary<long, ProjectItem> mapping = new Dictionary<long, ProjectItem>();
            GetIdToProjectItemMapping(projectItem, mapping, dynamicOnly);
            return mapping;
        }

        private static void GetIdToProjectItemMapping(ProjectItem item, Dictionary<long, ProjectItem> mapping, bool dynamicOnly)
        {
            if (item.Internal_Meta.Descriptor != null)
            {
                if ((item.Internal_Meta.Descriptor.Children == null || item.Internal_Meta.Descriptor.Children.Length == 0) &&
                    (item.Internal_Meta.Descriptor.Components == null || item.Internal_Meta.Descriptor.Components.Length == 0))
                {
                    if (!dynamicOnly || IsDynamicResource(item))
                    {
                        if (!mapping.ContainsKey(InstanceID(item)))
                        {
                            mapping.Add(InstanceID(item), item);
                        }
                    }
                }
                else
                {
                    long[] ids = item.Internal_Meta.Descriptor.GetInstanceIds();
                    for (int i = 0; i < ids.Length; ++i)
                    {
                        long instanceID = ids[i];
                        if (!dynamicOnly || IdentifiersMap.IsDynamicResourceID(instanceID))
                        {
                            if (!mapping.ContainsKey(instanceID))
                            {
                                mapping.Add(instanceID, item);
                            }
                        }
                    }
                }
            }

            if (item.Children != null)
            {
                for (int i = 0; i < item.Children.Count; ++i)
                {
                    ProjectItem childItem = item.Children[i];
                    GetIdToProjectItemMapping(childItem, mapping, dynamicOnly);
                }
            }
        }

        private static void Diff(ProjectItem dst, ProjectItem src, List<ProjectItem> diff)
        {
            if(!dst.IsFolder)
            {
                return;
            }

            Dictionary<string, ProjectItem> children = dst.Children != null ?
                dst.Children.ToDictionary(child => child.ToString()) :
                new Dictionary<string, ProjectItem>();

            if (src.Children != null)
            {
                for (int i = 0; i < src.Children.Count; ++i)
                {
                    ProjectItem otherChild = src.Children[i];
                    ProjectItem child;
                    if (children.TryGetValue(otherChild.ToString(), out child))
                    {
                        Diff(otherChild, child, diff);
                    }
                    else
                    {
                        if(otherChild.IsExposedFromEditor)
                        {
                            diff.Add(otherChild);
                        }
                    }
                }
            }

        }

        private static void MergeData(ProjectItem dst, ProjectItem src)
        {
            if (!dst.IsFolder)
            {
                if (src.IsExposedFromEditor)
                {
                    Dictionary<long, PersistentData> data;
                    if (dst.Internal_Data.PersistentData != null)
                    {
                        data = dst.Internal_Data.PersistentData.ToDictionary(k => k.InstanceId);
                    }
                    else
                    {
                        data = new Dictionary<long, PersistentData>();
                    }

                    Dictionary<long, PersistentData> otherData = null;
                    if (src.Internal_Data.PersistentData != null)
                    {
                        otherData = src.Internal_Data.PersistentData.ToDictionary(k => k.InstanceId);
                    }

                    PersistentDescriptor otherDescriptor = src.Internal_Meta.Descriptor;
                    PersistentDescriptor descriptor = dst.Internal_Meta.Descriptor;

                    if (descriptor.InstanceId == otherDescriptor.InstanceId)
                    {
                        if (otherData != null)
                        {
                            MergeRecursive(descriptor, otherDescriptor, data, otherData);
                            dst.Internal_Data.PersistentData = data.Values.ToArray();
                            if (src.Internal_Data.RawData != null)
                            {
                                dst.Internal_Data.RawData = src.Internal_Data.RawData;
                            }
                        }
                        else
                        {
                            dst.Internal_Data.PersistentData = null;
                            dst.Internal_Data.RawData = src.Internal_Data.RawData;
                        }
                    }
                }
            }
            else
            {
                Dictionary<string, ProjectItem> children = dst.Children != null ?
                    dst.Children.ToDictionary(child => child.ToString()) :
                    new Dictionary<string, ProjectItem>();

                if (src.Children != null)
                {
                    for (int i = 0; i < src.Children.Count; ++i)
                    {
                        ProjectItem otherChild = src.Children[i];
                        ProjectItem child;
                        if (children.TryGetValue(otherChild.ToString(), out child))
                        {
                            MergeData(child, otherChild);
                        }
                        else
                        {
                            if (!otherChild.IsExposedFromEditor)
                            {
                                children.Add(otherChild.ToString(), otherChild);
                                dst.AddChild(otherChild);
                                i--;
                            }
                        }
                    }
                }
            }
        }

        private static void MergeRecursive(
            PersistentDescriptor descriptor,
            PersistentDescriptor otherDescriptor,
            Dictionary<long, PersistentData> data,
            Dictionary<long, PersistentData> otherData)
        {
            if (descriptor.InstanceId == otherDescriptor.InstanceId)
            {
                data[descriptor.InstanceId] = otherData[descriptor.InstanceId];
                if (descriptor.Components != null && otherDescriptor.Components != null)
                {
                    Dictionary<long, PersistentDescriptor> otherDescriptorComponents = otherDescriptor.Components.ToDictionary(k => k.InstanceId);
                    for (int i = 0; i < descriptor.Components.Length; ++i)
                    {
                        PersistentDescriptor componentDescriptor = descriptor.Components[i];
                        PersistentDescriptor otherComponentDescriptor;
                        if (otherDescriptorComponents.TryGetValue(componentDescriptor.InstanceId, out otherComponentDescriptor))
                        {
                            data[componentDescriptor.InstanceId] = otherData[otherComponentDescriptor.InstanceId];
                        }
                    }
                }

                if (descriptor.Children != null && otherDescriptor.Children != null)
                {
                    Dictionary<long, PersistentDescriptor> otherDescriptorChildren = otherDescriptor.Children.ToDictionary(k => k.InstanceId);
                    for (int i = 0; i < descriptor.Children.Length; ++i)
                    {
                        PersistentDescriptor childDescriptor = descriptor.Children[i];
                        PersistentDescriptor otherChildDescriptor;
                        if (otherDescriptorChildren.TryGetValue(childDescriptor.InstanceId, out otherChildDescriptor))
                        {
                            MergeRecursive(childDescriptor, otherChildDescriptor, data, otherData);
                        }
                    }
                }
            }
        }

        private static void TryLoadRawData(ProjectItem projectItem, Dictionary<long, UnityObject> objects)
        {
            if (projectItem.IsExposedFromEditor)
            {
                return;
            }
            if (projectItem.Internal_Data.RawData != null)
            {
                PersistentData data = projectItem.Internal_Data.PersistentData[0];
                UnityObject obj = objects.Get(data.InstanceId);
                if (obj is Texture2D)
                {
                    if (!string.IsNullOrEmpty(projectItem.BundleName))
                    {
                        return;
                    }

                    Texture2D tex2D = (Texture2D)obj;
                    tex2D.LoadImage(projectItem.Internal_Data.RawData);
                }
            }
        }

        private static void TryReadRawData(ProjectItem projectItem, UnityObject obj)
        {
            if (projectItem.IsExposedFromEditor)
            {
                return;
            }
            if (obj is Texture2D)
            {
                if (!string.IsNullOrEmpty(projectItem.BundleName))
                {
                    return;
                }

     
                Texture2D tex2D = (Texture2D)obj;
                projectItem.Internal_Data.RawData = tex2D.EncodeToPNG();
            }
        }

    }
}


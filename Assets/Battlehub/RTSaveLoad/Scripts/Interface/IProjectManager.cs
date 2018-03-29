#define RT_USE_DEFAULT_IMPLEMENTATION

#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif
using System;
using System.Collections.Generic;
using System.Linq;

using UnityObject = UnityEngine.Object;

namespace Battlehub.RTSaveLoad
{
#if RT_USE_DEFAULT_IMPLEMENTATION
#if RT_USE_PROTOBUF
    [ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
    [Serializable]
    public class ProjectItemMeta
    {
        public string TypeName
        {
            get
            {
                if (Descriptor == null)
                {
                    return null;
                }
                return Descriptor.TypeName;
            }
        }

        public string BundleName
        {
            get
            {
                if(BundleDescriptor == null)
                {
                    return null;
                }
                return BundleDescriptor.BundleName;
            }
        }

        public int TypeCode;  
        public string Name;
        public bool IsExposedFromEditor;


        //Could be removed in your own IProjectManager implementation
        public PersistentDescriptor Descriptor;

        //Could be removed in your own IProjectManager implementation
        public AssetBundleDescriptor BundleDescriptor;
    }

#if RT_USE_PROTOBUF
    [ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
    [Serializable]
    public class ProjectItemData
    {

        //Could be removed in your own IProjectManager implementation
        public PersistentData[] PersistentData;

        //Could be removed in your own IProjectManager implementation\
        public byte[] RawData;

        public void Rename(ProjectItemMeta meta, string name)
        {
            //Implementation could be removed in your own IProjectManager implementation

            if (PersistentData != null && PersistentData.Length != 0)
            {
                if (PersistentData.Length > 1)
                {
                    Dictionary<long, PersistentData> dataDictionary = PersistentData.ToDictionary(d => d.InstanceId);

                    PersistentData persistentData;
                    if (dataDictionary.TryGetValue(meta.Descriptor.InstanceId, out persistentData))
                    {
                        PersistentObjects.PersistentObject persistentObject = persistentData.AsPersistentObject;
                        persistentObject.name = name;
                        if (meta.Descriptor.Components != null)
                        {
                            for (int i = 0; i < meta.Descriptor.Components.Length; ++i)
                            {
                                if (dataDictionary.TryGetValue(meta.Descriptor.Components[i].InstanceId, out persistentData))
                                {
                                    persistentObject = persistentData.AsPersistentObject;
                                    persistentObject.name = name;
                                }
                            }
                        }
                    }
                }
                else
                {
                    PersistentObjects.PersistentObject persistentObject = PersistentData[0].AsPersistentObject;
                    persistentObject.name = name;
                }
            }
        }
    }
#else
#if RT_USE_PROTOBUF
    [ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
    [Serializable]
    public class ProjectItemMeta
    {
        public string TypeName
        {
            get
            {
                return string.Empty;
            }
        }

        public string BundleName
        {
            get
            {
                if (BundleDescriptor == null)
                {
                    return null;
                }
                return BundleDescriptor.BundleName;
            }
        }

        public int TypeCode;
        public string Name;
        public bool IsExposedFromEditor;

        public AssetBundleDescriptor BundleDescriptor;
    }

#if RT_USE_PROTOBUF
    [ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
    [Serializable]
    public class ProjectItemData
    {
        //Could be removed in your own IProjectManager implementation\
        public byte[] RawData;

        public void Rename(ProjectItemMeta meta, string name)
        {
        }
    }
#endif

    public delegate void ProjectManagerCallback();
    public delegate void ProjectManagerCallback<T>(T data);

    public class ProjectManagerEventArgs : EventArgs
    {
        public ProjectItem[] ProjectItems
        {
            get;
            private set;
        }

        public ProjectItem ProjectItem
        {
            get
            {
                if (ProjectItems == null || ProjectItems.Length == 0)
                {
                    return null;
                }
                return ProjectItems[0];
            }
        }

        public ProjectManagerEventArgs(ProjectItem[] items)
        {
            ProjectItems = items;
        }

        public ProjectManagerEventArgs(ProjectItem item)
        {
            ProjectItems = new[] { item };
        }
    }


    //public static class ProjectMangerConstants
    //{
    //    public const string PROJECT_TEMPLATE_FOLDER = "Exposed";
    //}

    public struct ID
    {
        private long m_id;
        public ID(long id)
        {
            m_id = id;
        }

        public override bool Equals(object obj)
        {
            if(obj is ID)
            {
                ID other = (ID)obj;
                return m_id.Equals(other.m_id);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return m_id.GetHashCode();
        }

        public override string ToString()
        {
            return m_id.ToString();
        }
    }

    public interface ISceneManager
    {
        event EventHandler<ProjectManagerEventArgs> SceneCreated;
        event EventHandler<ProjectManagerEventArgs> SceneSaving;
        event EventHandler<ProjectManagerEventArgs> SceneSaved;
        event EventHandler<ProjectManagerEventArgs> SceneLoading;
        event EventHandler<ProjectManagerEventArgs> SceneLoaded;

        ProjectItem ActiveScene
        {
            get;
        }

        void Exists(ProjectItem scene, ProjectManagerCallback<bool> callback);

        void SaveScene(ProjectItem scene, ProjectManagerCallback callback);

        void LoadScene(ProjectItem scene, ProjectManagerCallback callback);

        void CreateScene();
    }

    public interface IProjectManager : ISceneManager
    {
        event EventHandler ProjectLoading;
        event EventHandler<ProjectManagerEventArgs> ProjectLoaded;
        event EventHandler<ProjectManagerEventArgs> BundledResourcesAdded;
        event EventHandler<ProjectManagerEventArgs> DynamicResourcesAdded;

        ProjectItem Project
        {
            get;
        }

        bool IsResource(UnityObject obj);

        ID GetID(UnityObject obj);

        void LoadProject(string name, ProjectManagerCallback<ProjectItem> callback);

        void AddBundledResources(ProjectItem folder, string bundleName, Func<UnityObject, string, bool> filter, ProjectManagerCallback<ProjectItem[]> callback);

        void AddBundledResource(ProjectItem folder, string bundleName, string assetName, ProjectManagerCallback<ProjectItem[]> callback);

        void AddBundledResource<T>(ProjectItem folder, string bundleName, string assetName, ProjectManagerCallback<ProjectItem[]> callback);

        void AddBundledResource(ProjectItem folder, string bundleName, string assetName, Type assetType, ProjectManagerCallback<ProjectItem[]> callback);

        void AddBundledResources(ProjectItem folder, string bundleName, string[] assetNames, ProjectManagerCallback<ProjectItem[]> callback);

        void AddBundledResources(ProjectItem folder, string bundleName, string[] assetNames, Type[] assetTypes, Func<UnityObject, string, bool> filter, ProjectManagerCallback<ProjectItem[]> callback);

        void AddDynamicResource(ProjectItem folder, UnityObject obj, ProjectManagerCallback<ProjectItem[]> callback);

        void AddDynamicResources(ProjectItem folder, UnityObject[] objects, ProjectManagerCallback<ProjectItem[]> callback);

        void AddDynamicResource(ProjectItem folder, UnityObject obj, bool includingDependencies, Func<UnityObject, bool> filter, ProjectManagerCallback<ProjectItem[]> callback);

        void AddDynamicResources(ProjectItem folder, UnityObject[] objects, bool includingDependencies, Func<UnityObject, bool> filter, ProjectManagerCallback<ProjectItem[]> callback);

        void CreateFolder(string name, ProjectItem parent, ProjectManagerCallback<ProjectItem> callback);

        void SaveObjects(ProjectItemObjectPair[] itemObjectPairs, ProjectManagerCallback callback);

        void GetOrCreateObjects(ProjectItem folder, ProjectManagerCallback<ProjectItemObjectPair[]> callback);

        void GetOrCreateObjects(ProjectItem[] projectItems, ProjectManagerCallback<ProjectItemObjectPair[]> callback);

        void Duplicate(ProjectItem[] projectItems, ProjectManagerCallback<ProjectItem[]> callback);

        void Rename(ProjectItem projectItem, string newName, ProjectManagerCallback callback);

        void Move(ProjectItem[] projectItems, ProjectItem folder, ProjectManagerCallback callback);

        void Delete(ProjectItem[] projectItems, ProjectManagerCallback callback);

        void IgnoreTypes(params Type[] type);

       
    }
}

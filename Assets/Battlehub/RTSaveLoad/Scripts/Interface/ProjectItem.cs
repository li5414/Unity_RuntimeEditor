#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
using System;
#endif

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

using UnityObject = UnityEngine.Object;

namespace Battlehub.RTSaveLoad
{
#if RT_USE_PROTOBUF
    [ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
    [System.Serializable]
    public class ProjectMeta
    {
        public int Counter;
    }


#if RT_USE_PROTOBUF
    [ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
    [System.Serializable]
    public class ProjectData
    {
        //public Dictionary<string, List<string>> Dependencies;
        //public Dictionary<string, string> GuidToPath;    
    }
    
    public static class ProjectItemTypes
    {
        public const int None = 0;
        public const int Folder = 1;
        public const int Scene = 2;
        public const int Obj = 0x40000000;
        public const int Material = Obj | 1;
        public const int Mesh = Obj | 2;
        public const int Prefab = Obj | 3;
        public const int Texture = Obj | 4;
        public const int ProceduralMaterial = Obj | 5;

        public static readonly Dictionary<int, string> Ext = new Dictionary<int, string>
        {
            { Folder, string.Empty },
            { Scene, "rtsc" },
            { Material, "rtmat" },
            { ProceduralMaterial, "rtpmat" },
            { Mesh, "rtmesh" },
            { Prefab, "rtprefab" },
            { Texture, "rtimg" },
            { Obj, "rtobj" }
        };

        public static readonly Dictionary<System.Type, int> Type = new Dictionary<System.Type, int>
        {
            { typeof(GameObject), Prefab },
            { typeof(Mesh), Mesh },
            { typeof(Material), Material },
#if !UNITY_WEBGL
            { typeof(ProceduralMaterial), ProceduralMaterial },
#endif
            { typeof(Texture), Texture },
            { typeof(UnityObject), Obj },
        };

        public static int GetProjectItemType(System.Type type)
        {
            while(type != null)
            {
                int projectItemType;
                if(Type.TryGetValue(type, out projectItemType))
                {
                    return projectItemType;
                }

                type = type.BaseType();
            }

            return None;
        }
    }

#if RT_USE_PROTOBUF
    [ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
#endif
    [System.Serializable]
    public class AssetBundleDescriptor
    {
        public string BundleName;

        public string AssetName; //is not used currently
        public string TypeName; //is not used currently
    }

    public class ProjectRoot
    {
        public ProjectMeta Meta;
        public ProjectData Data;
        public ProjectItem Item;    
    }


#if RT_USE_PROTOBUF
    [ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
#endif
    [System.Serializable]
    public class ProjectItem
    {
        /// <summary>
        /// Do not use
        /// </summary>
        public ProjectItemMeta Internal_Meta;

        /// <summary>
        /// Do not use 
        /// </summary>
        public ProjectItemData Internal_Data;

        public ProjectItem Parent;
        public List<ProjectItem> Children;

        public bool IsExposedFromEditor
        {
            get { return Internal_Meta.IsExposedFromEditor; }
            set { Internal_Meta.IsExposedFromEditor = value; }
        }

        public string BundleName
        {
            get
            {
                if (Internal_Meta == null)
                {
                    return null;
                }

                return Internal_Meta.BundleName;
            }
        }

        public string Name
        {
            get { return Internal_Meta.Name; }
            set { Internal_Meta.Name = value; }
        }

        public string NameExt
        {
            get
            {
                string ext = Ext;
                if (string.IsNullOrEmpty(ext))
                {
                    return Internal_Meta.Name;
                }
                return string.Format("{0}.{1}", Internal_Meta.Name, Ext);
            }
            set
            {
                if (value == null)
                {
                    Internal_Meta.Name = null;
                }
                else
                {
                    int extIndex = value.LastIndexOf("." + Ext);
                    if (extIndex >= 0)
                    {
                        Internal_Meta.Name = value.Remove(extIndex);
                    }
                    else
                    {
                        Internal_Meta.Name = value;
                    }
                }
            }
        }

        public int TypeCode
        {
            get
            {
                if (Internal_Meta == null)
                {
                    return ProjectItemTypes.None;
                }
                return Internal_Meta.TypeCode;
            }
        }

        public string TypeName
        {
            get
            {
                if(Internal_Meta == null)
                {
                    return null;
                }

                return Internal_Meta.TypeName;
            }
        }

        public bool IsFolder
        {
            get { return Internal_Meta.TypeCode == ProjectItemTypes.Folder; }
        }

        public bool IsScene
        {
            get { return Internal_Meta.TypeCode == ProjectItemTypes.Scene; }
        }

        public bool IsResource
        {
            get { return !IsFolder && !IsScene; }
        }

        public bool IsGameObject
        {
            get
            {
                System.Type type = System.Type.GetType(TypeName);
                return type == typeof(GameObject);
            }
        }

        public string Ext
        {
            get
            {
                return ProjectItemTypes.Ext[Internal_Meta.TypeCode];
            }
        }

        public ProjectItem()
        {

        }

        public ProjectItem(ProjectItemMeta meta, ProjectItemData data)
        {
            Internal_Meta = meta;
            Internal_Data = data;
        }

        public void AddChild(ProjectItem item)
        {
            if(Children == null)
            {
                Children = new List<ProjectItem>();
            }

            if(item.Parent != null)
            {
                item.Parent.RemoveChild(item);
            }
            Children.Add(item);
            item.Parent = this;
        }

        public void RemoveChild(ProjectItem item)
        {
            if(Children == null)
            {
                return;
            }
            Children.Remove(item);
            item.Parent = null;
        }

        public int GetSiblingIndex()
        {
            return Parent.Children.IndexOf(this);
        }

        public void SetSiblingIndex(int index)
        {
            Parent.Children.Remove(this);
            Parent.Children.Insert(index, this);
        }

        public ProjectItem Get(string path)
        {
            path = path.Trim('/');
            string[] pathParts = path.Split('/');

            ProjectItem item = this;
            for(int i = 1; i < pathParts.Length; ++i)
            {
                string pathPart = pathParts[i];
                if(item.Children == null)
                {
                    return item;
                }

                if(i == pathParts.Length - 1 )
                {
                    item = item.Children.Where(child => child.NameExt == pathPart).FirstOrDefault();
                }
                else
                {
                    item = item.Children.Where(child => child.Name == pathPart).FirstOrDefault();
                }

                if(item == null)
                {
                    break;
                }
            }
            return item;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            ProjectItem parent = this;
            while (parent != null)
            {
                sb.Insert(0, parent.Internal_Meta.Name);
                sb.Insert(0, "/");
                parent = parent.Parent;
            }

            string ext = Ext;
            if(string.IsNullOrEmpty(ext))
            {
                return sb.ToString();
            }
            return string.Format("{0}.{1}", sb.ToString(), Ext);
        }

        public static ProjectItem CreateFolder(string name)
        {
            ProjectItem projectItem = new ProjectItem();
            projectItem.Internal_Meta = new ProjectItemMeta
            {
                Name = name,
                TypeCode = ProjectItemTypes.Folder,
            };

            return projectItem;
        }

        public static ProjectItem CreateScene(string name)
        {
            ProjectItem projectItem = new ProjectItem();
            projectItem.Internal_Meta = new ProjectItemMeta
            {
                Name = name,
                TypeCode = ProjectItemTypes.Scene,
            };

            return projectItem;
        }

        public static string GetUniqueName(string desiredName, UnityObject obj, ProjectItem parent)
        {
            if (parent == null)
            {
                throw new ArgumentNullException("parent");
            }
            string ext = ProjectItemTypes.Ext[ProjectItemTypes.GetProjectItemType(obj.GetType())];
            string[] existingNames = parent.Children != null ? parent.Children.Select(child => child.NameExt).ToArray() : new string[0];

            string name = PathHelper.GetUniqueName(desiredName, ext, existingNames);
            if (name == null)
            {
                return null;
            }

            int extIndex = name.LastIndexOf("." + ext);
            if (extIndex >= 0)
            {
                return name.Remove(extIndex);
            }

            return name;
        }

        public static string GetUniqueName(string desiredName, ProjectItem item, ProjectItem parent, bool exceptItem = true)
        {
            string[] existingNames;
            if (exceptItem)
            {
                existingNames = parent.Children.Except(new[] { item }).Select(child => child.NameExt).ToArray();
            }
            else
            {
                existingNames = parent.Children.Select(child => child.NameExt).ToArray();
            }

            return PathHelper.GetUniqueName(desiredName, item.Ext, existingNames);
        }

        public static bool IsValidName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return true;
            }
            return Path.GetInvalidFileNameChars().All(c => !name.Contains(c));
        }

        public static ProjectItem[] GetRootItems(ProjectItem[] items)
        {
            HashSet<ProjectItem> itemsHS = new HashSet<ProjectItem>();
            for (int i = 0; i < items.Length; ++i)
            {
                if (!itemsHS.Contains(items[i]))
                {
                    itemsHS.Add(items[i]);
                }
            }

            for (int i = 0; i < items.Length; ++i)
            {
                ProjectItem item = items[i];
                ProjectItem p = item.Parent;
                while (p != null)
                {
                    if (itemsHS.Contains(p))
                    {
                        itemsHS.Remove(item);
                        break;
                    }

                    p = p.Parent;
                }
            }

            items = itemsHS.ToArray();
            return items;
        }

        public ProjectItem[] FlattenHierarchy(bool includeSelf = false)
        {
            List<ProjectItem> ancestorsList = new List<ProjectItem>();
            if(includeSelf)
            {
                ancestorsList.Add(this);
            }
            GetAncestors(this, ancestorsList);
            return ancestorsList.ToArray();
        }

        private void GetAncestors(ProjectItem item, List<ProjectItem> list)
        {
            if(item.Children == null)
            {
                return;
            }
            for(int i = 0; i < item.Children.Count; ++i)
            {
                ProjectItem childItem = item.Children[i];
                list.Add(childItem);
                GetAncestors(childItem, list);
            }
        }

        public void Rename(string name)
        {
            Internal_Data.Rename(Internal_Meta, name);
        }
    }


    public class NoneItem : ProjectItemWrapper
    {

    }

    public class ProjectItemWrapper : ScriptableObject
    {
        [NonSerialized]
        private ProjectItem m_projectItem;
        public ProjectItem ProjectItem
        {
            get { return m_projectItem; }
            set
            {
                m_projectItem = value;
                if (m_projectItem == null)
                {
                    name = "None";
                }
                else
                {
                    name = m_projectItem.Name;
                }
            }
        }

        public bool IsNone
        {
            get { return ProjectItem == null; }
        }

        public bool IsScene
        {
            get { return !IsNone && ProjectItem.IsScene; }
        }

        public bool IsFolder
        {
            get { return !IsNone && ProjectItem.IsFolder; }
        }
    }

    public class ProjectItemObjectPair
    {
        public bool IsNone
        {
            get { return ProjectItem == null && Object is NoneItem; }
        }

        public bool IsSceneObject
        {
            get { return ProjectItem == null && !(Object is NoneItem) && Object != null; }
        }

        public bool IsScene
        {
            get { return !IsNone && ProjectItem != null && ProjectItem.IsScene; }
        }

        public bool IsFolder
        {
            get { return !IsNone && ProjectItem != null && ProjectItem.IsFolder; }
        }

        public bool IsResource
        {
            get { return !IsNone && ProjectItem != null && !ProjectItem.IsFolder && !ProjectItem.IsScene; }
        }

        public ProjectItem ProjectItem
        {
            get;
            private set;
        }

        public UnityObject Object
        {
            get;
            private set;
        }

        public ProjectItemObjectPair(ProjectItem projectItem, UnityObject obj)
        {
            ProjectItem = projectItem;
            Object = obj;
        }
    }
}


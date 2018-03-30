using System;
using UnityEngine;

namespace Battlehub.RTSaveLoad2
{
    [Serializable]
    public class PersistentPropertyMapping
    {
        public bool IsEnabled;
        public bool IsProperty;
        public int PersistentTag;

        public string PersistentFullTypeName
        {
            get { return PersistentNamespace + "." + PersistentTypeName; }
        }

        public string MappedFullTypeName
        {
            get { return MappedNamespace + "." + MappedTypeName; }
        }

        public string MappedAssemblyQualifiedName
        {
            get { return MappedFullTypeName + "," + MappedAssemblyName; }
        }

        public Type MappedType
        {
            get { return Type.GetType(MappedAssemblyQualifiedName); }
        }

        public string PersistentNamespace;
        public string PersistentTypeName;
        public string PersistentName;

        public string MappedAssemblyName;
        public string MappedNamespace;
        public string MappedTypeName;
        public string MappedName;

        public bool UseSurrogate;
        public bool HasDependenciesOrIsDependencyItself;
    }

    [Serializable]
    public class PersistentSubclass
    {
        public int PersistentTag;
        public string FullTypeName
        {
            get { return Namespace + "." + TypeName; }
        }
        public string Namespace;
        public string TypeName;
        public bool IsEnabled;
    }

    public class PersistentClassMapping : MonoBehaviour
    {
        public string MappedFullTypeName
        {
            get { return MappedNamespace + "." + MappedTypeName; }
        }

        public string PersistentFullTypeName
        {
            get { return PersistentNamespace + "." + PersistentTypeName; }
        }

        public string MappedAssemblyQualifiedName
        {
            get { return MappedFullTypeName + "," + MappedAssemblyName; }
        }

        public bool IsEnabled;
        public int PersistentPropertyTag;
        public int PersistentSubclassTag;
        public string MappedAssemblyName;
        public string MappedNamespace;
        public string MappedTypeName;
        public string PersistentNamespace;
        public string PersistentTypeName;
        public string PersistentBaseNamespace;
        public string PersistentBaseTypeName;

        public PersistentSubclass[] Subclasses;
        public PersistentPropertyMapping[] PropertyMappings;

        public static string ToPersistentNamespace(string mappedNamespace)
        {
            return mappedNamespace + ".Battlehub.SL2";
        }

        public static string ToMappedNamespace(string persistentNamespace)
        {
            return persistentNamespace.Replace(".Battlehub.SL2", "");
        }

        public static string ToPersistentName(string typeName)
        {
            return "Persistent" + typeName;
        }
    }

    
}



using UnityEngine;

namespace Battlehub.RTSaveLoad2
{
    [System.Serializable]
    public struct PersistentPropertyMapping
    {
        public bool IsEnabled;
        public int PersistentTag;
        public string PersistentName;
        public string PersistentType;
        public bool IsProperty;
        public string MappedName;
        public bool IsMappedProperty;
        public string MappedType;

        public bool UseBuiltInCodeSnippet;
        public string BuiltInCodeSnippet;
    }

    public class PersistentObjectMapping : MonoBehaviour
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
        public int PersistentTag;
        public string MappedAssemblyName;
        public string MappedNamespace;
        public string MappedTypeName;
        public string PersistentNamespace;
        public string PersistentTypeName;
        public PersistentPropertyMapping[] PropertyMappings;

        public static string ToPersistentNamespace(string mappedNamespace)
        {
            return mappedNamespace + ".Battlehub.SL2";
        }

        public static string ToMappedNamespace(string persistentNamespace)
        {
            return persistentNamespace.Replace(".Battlehub.SL2", "");
        }
    }

    
}



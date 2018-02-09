using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityObject = UnityEngine.Object;

namespace Battlehub.RTSaveLoad2
{
    public class CodeGen
    {
        private const int AutoFieldTagOffset = 256;
        private const int SubclassOffset = 1024;

        private static readonly string BR = Environment.NewLine;
        private static readonly string END = BR + BR;

        private static string[] DefaultNamespaces =
        {
            "System.Collections.Generic",
            "ProtoBuf",
        };

        private static readonly string PersistentClassTemplate =
            "{0}"                                               + BR +
            "using UnityObject = UnityEngine.Object;"           + BR +
            "namespace Battlehub.RTSaveLoad2"                   + BR +
            "{"                                                 + BR +
            "   [ProtoContract(AsReferenceDefault = true)"      + BR +
            "   public class {1} : {2}"                         + BR +
            "   {"                                              + BR +
            "       {3}"                                        + BR +
            "   }"                                              + BR +
            "}"                                                 + END;

        private static readonly string FieldTemplate =
            "[ProtoMember({0})]"                                + BR +
            "public {1} {2};"                                   + END;

        private static readonly string ReadFromMethodTemplate =
            "public override void ReadFrom(object obj)"         + BR +
            "{"                                                 + BR +
            "   UnityObject uo = (UnityObject)obj;"             + BR +
            "   {0}"                                            + BR +
            "}"                                                 + END;

        private static readonly string WriteToMethodTemplate =
            "public override void WriteTo(object obj)"          + BR +
            "{"                                                 + BR +
            "   UnityObject uo = (UnityObject)obj;"             + BR +
            "   {0}"                                            + BR +
            "}"                                                 + END;

        private static readonly string GetDepsMethodTemplate =
            "public virtual void GetDeps(HashSet<int> dependencies)"                    + BR +
            "{"                                                                         + BR +
            "   {0}"                                                                    + BR +
            "}"                                                                         + END;

        private static readonly string GetDepsFromMethodTemplate =
            "public virtual void GetDepsFrom(object obj, HashSet<int> dependencies)"    + BR +
            "{"                                                                         + BR +
            "   UnityObject uo = (UnityObject)obj;"                                     + BR +
            "   {0}"                                                                    + BR +
            "}"                                                                         + END;

        public PropertyInfo[] GetProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).Where(p => p.GetGetMethod() != null && p.GetSetMethod() != null).ToArray();
        }

        public FieldInfo[] GetFields(Type type)
        {
            return type.GetFields();
        }

        public string Generate(PersistentClassMapping mapping)
        {
            string usings = CreateUsings(mapping);
            string className = mapping.PersistentTypeName;
            string baseClassName = mapping.PersistentBaseTypeName;
            string body = CreateBody(mapping);

            return string.Format(PersistentClassTemplate, usings, className, baseClassName, body);
        }

        private string CreateBody(PersistentClassMapping mapping)
        {
            StringBuilder sb = new StringBuilder();

            for(int i = 0; i < mapping.PropertyMappings.Length; ++i)
            {
                PersistentPropertyMapping prop = mapping.PropertyMappings[i];

                Type repacementType = GetReplacementType(prop.MappedType);
                sb.Append(string.Format(
                    FieldTemplate, i + AutoFieldTagOffset,
                    repacementType != null ? repacementType.Name : prop.PersistentTypeName,
                    prop.PersistentName));
                sb.Append(END).Append(END);
            }

            sb.Append(string.Format(ReadFromMethodTemplate, CreateReadMethodBody(mapping)));
            sb.Append(END).Append(END);
            sb.Append(string.Format(WriteToMethodTemplate, CreateWriteMethodBody(mapping)));
            sb.Append(END).Append(END);
            sb.Append(string.Format(GetDepsMethodTemplate, CreateDepsMethodBody(mapping)));
            sb.Append(END).Append(END);
            sb.Append(string.Format(GetDepsFromMethodTemplate, CreateDepsFromMethodBody(mapping)));

            return sb.ToString();
        }

        private string CreateReadMethodBody(PersistentClassMapping mapping)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < mapping.PropertyMappings.Length; ++i)
            {
                PersistentPropertyMapping prop = mapping.PropertyMappings[i];
                sb.Append(string.Format("{0} = uo.{1};", prop.PersistentName, prop.MappedName));
                sb.Append(END);
            }

            return sb.ToString();
        }

        private string CreateWriteMethodBody(PersistentClassMapping mapping)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < mapping.PropertyMappings.Length; ++i)
            {
                PersistentPropertyMapping prop = mapping.PropertyMappings[i];
                sb.Append(string.Format("uo.{0} = {1};", prop.PersistentName, prop.MappedName));
                sb.Append(END);
            }

            return sb.ToString();
        }

        private string CreateDepsMethodBody(PersistentClassMapping mapping)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < mapping.PropertyMappings.Length; ++i)
            {
                PersistentPropertyMapping prop = mapping.PropertyMappings[i];
                if(prop.MappedType.IsSubclassOf(typeof(UnityObject)))
                {
                    sb.Append(string.Format("AddDependency({0}, dependencies);", prop.PersistentName));
                    sb.Append(END);
                }
            }
            return sb.ToString();
        }

        private string CreateDepsFromMethodBody(PersistentClassMapping mapping)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < mapping.PropertyMappings.Length; ++i)
            {
                PersistentPropertyMapping prop = mapping.PropertyMappings[i];
                if (prop.MappedType.IsSubclassOf(typeof(UnityObject)))
                {
                    sb.Append(string.Format("AddDependency(uo.{0}, dependencies);", prop.MappedName));
                    sb.Append(END);
                }
            }
            return sb.ToString();
        }

        private string CreateUsings(PersistentClassMapping mapping)
        {
            StringBuilder sb = new StringBuilder();
            HashSet<string> namespaces = new HashSet<string>();
            for(int i = 0; i < DefaultNamespaces.Length; ++i)
            {
                namespaces.Add(DefaultNamespaces[i]);
            }

            if(!namespaces.Contains(mapping.MappedNamespace))
            {
                namespaces.Add(mapping.MappedNamespace);
            }

            if(!namespaces.Contains(mapping.PersistentNamespace))
            {
                namespaces.Add(mapping.PersistentNamespace);
            }

            if(!namespaces.Contains(mapping.PersistentBaseNamespace))
            {
                namespaces.Add(mapping.PersistentBaseNamespace);
            }

            for(int i = 0; i < mapping.PropertyMappings.Length; ++i)
            {
                PersistentPropertyMapping propertyMapping = mapping.PropertyMappings[i];
                if(!namespaces.Contains(propertyMapping.MappedNamespace))
                {
                    namespaces.Add(propertyMapping.MappedNamespace);
                }

                Type type = propertyMapping.MappedType;
                Type replacementType = GetReplacementType(type);
                if(replacementType != null)
                {
                    if (!namespaces.Contains(replacementType.Namespace))
                    {
                        namespaces.Add(replacementType.Namespace);
                    }
                }
                else
                {
                    if (!namespaces.Contains(propertyMapping.PersistentNamespace))
                    {
                        namespaces.Add(propertyMapping.PersistentNamespace);
                    }
                }
            }

            foreach(string ns in namespaces)
            {
                sb.Append("using " + ns + ";");
            }

            return sb.ToString();
        }

        private Type GetReplacementType(Type type)
        {
            if(type.IsSubclassOf(typeof(UnityObject)))
            {
                return typeof(int);
            }
            return null;
        }
    }
}

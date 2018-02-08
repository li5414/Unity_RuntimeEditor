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

        public string WritePersistentClassCode(PersistentClassMapping mapping)
        {
            StringBuilder sb = new StringBuilder();
            WritePersistentClassUsings(mapping, sb);
            string usings = sb.ToString();
            
            string className = mapping.PersistentTypeName;
            string baseClassName = "";
            string body = "";

            return string.Format(PersistentClassTemplate, usings, className, baseClassName, body);
        }

        private void WritePersistentClassUsings(PersistentClassMapping mapping, StringBuilder sb)
        {
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

            for(int i = 0; i < mapping.PropertyMappings.Length; ++i)
            {
                PersistentPropertyMapping propertyMapping = mapping.PropertyMappings[i];
                if(!namespaces.Contains(propertyMapping.MappedNamespace))
                {
                    namespaces.Add(propertyMapping.MappedNamespace);
                }

                Type type = Type.GetType(propertyMapping.MappedAssemblyQualifiedName);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace Battlehub.RTSaveLoad2
{
    public class CodeGen
    {
        private const int AutoFieldTagOffset = 256;
        private const int SubclassOffset = 1024;

        private static readonly string BR = Environment.NewLine;
        private static readonly string END = BR + BR;
        private static readonly string SEMICOLON = ";";

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
            "public virtual void GetDepsFrom(object obj, HashSet<object> dependencies)" + BR +
            "{"                                                                         + BR +
            "   UnityObject uo = (UnityObject)obj;"                                     + BR +
            "   {0}"                                                                    + BR +
            "}"                                                                         + END;
                                  
        private static readonly string TypeModelCreatorTemplate =
            "{0}"                                                                       + BR +
            "using UnityObject = UnityEngine.Object;"                                   + BR +
            "namespace Battlehub.RTSaveLoad2"                                           + BR +
            "{"                                                                         + BR +
            "   public static partial class TypeModelCreator"                           + BR +
            "   {"                                                                      + BR +
            "       static partial void RegisterAutoTypes(RuntimeTypeModel model)"      + BR +
            "       {"                                                                  + BR +
            "           {1}"                                                            + BR +
            "       }"                                                                  + BR +
            "   }"                                                                      + BR +
            "}"                                                                         + END;

        private static readonly string AddTypeTemplate =
            "model.Add(typeof({0}), true){1}"                                           + END;
        private static readonly string AddSubtypeTemplate =
            "   .AddSubType({1}, typeof({0}))"                                          ;

        public PropertyInfo[] GetProperties(Type type)
        {
            return GetAllProperties(type).Where(p => p.GetGetMethod() != null && p.GetSetMethod() != null).ToArray();
        }

        public PropertyInfo[] GetAllProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).Where(p => p.GetIndexParameters().Length == 0).ToArray();
        }

        public FieldInfo[] GetFields(Type type)
        {
            return type.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        }

#warning Generate empty Surrogates for all surrogate types, even if they was not selected
        public Type GetSurrogateType(Type type)
        {
            if (type.IsArray)
            {
                type = type.GetElementType();
            }

            if (!type.IsSubclassOf(typeof(UnityObject)) &&
                    type != typeof(UnityObject) &&
                   !type.IsEnum &&
                   !type.IsGenericType &&
                   !type.IsArray &&
                   !type.IsPrimitive &&
                    type != typeof(string))
            {
                return type;
            }
            return null;
        }

        public string TypeName(Type type)
        {
            if (type.DeclaringType == null)
                return type.Name;

            return TypeName(type.DeclaringType) + "+" + type.Name;
        }

        public string CreateTypeModelCreator(PersistentClassMapping[] mappings)
        {
            string usings = CreateUsings(mappings);
            string body = CreateTypeModelCreatorBody(mappings);

            return string.Format(TypeModelCreatorTemplate, usings, body);
        }

        private string CreateTypeModelCreatorBody(PersistentClassMapping[] mappings)
        {
            StringBuilder sb = new StringBuilder();
            for(int m = 0; m < mappings.Length; ++m)
            {
                PersistentClassMapping mapping = mappings[m];

                string endOfLine;
                if(mapping.Subclasses != null && mapping.Subclasses.Length > 0)
                {
                    endOfLine = CreateAddSubtypesBody(mapping);
                }
                else
                {
                    endOfLine = SEMICOLON;
                }

                sb.AppendFormat(AddTypeTemplate, mapping.PersistentTypeName, endOfLine);
            }
 
            return sb.ToString();
        }

        private string CreateAddSubtypesBody(PersistentClassMapping mapping)
        {
            StringBuilder sb = new StringBuilder();
            PersistentSubclass[] subclasses = mapping.Subclasses.Where(sc => sc.IsEnabled).ToArray();
            for (int i = 0; i < subclasses.Length - 1; ++i)
            {
                PersistentSubclass subclass = mapping.Subclasses[i];
                sb.AppendFormat(AddSubtypeTemplate, subclass.TypeName, subclass.PersistentTag + SubclassOffset);
                sb.Append(END);
            }

            if(subclasses.Length > 0)
            {
                PersistentSubclass subclass = mapping.Subclasses[subclasses.Length - 1];
                sb.AppendFormat(AddSubtypeTemplate, subclass.TypeName, subclass.PersistentTag + SubclassOffset);
                sb.Append(SEMICOLON).Append(END);
            }

            return sb.ToString();
        }

        public string CreatePersistentSurrogate(PersistentClassMapping mapping)
        {
            string usings = CreateUsings(mapping);
            string className = mapping.PersistentSurrogateTypeName;
            string baseClassName = mapping.PersistentSurrogateTypeName;
            string body = CreatePersistentSurrogateBody(mapping);

            return string.Format("", usings, className, baseClassName, body);
        }

        private string CreatePersistentSurrogateBody(PersistentClassMapping mapping)
        {
            return "";
        }

        public string CreatePersistentClass(PersistentClassMapping mapping)
        {
            string usings = CreateUsings(mapping);
            string className = mapping.PersistentTypeName;
            string baseClassName = mapping.PersistentBaseTypeName;
            string body = CreatePersistentClassBody(mapping);

            return string.Format(PersistentClassTemplate, usings, className, baseClassName, body);
        }

        private string CreatePersistentClassBody(PersistentClassMapping mapping)
        {
            StringBuilder sb = new StringBuilder();

            for(int i = 0; i < mapping.PropertyMappings.Length; ++i)
            {
                PersistentPropertyMapping prop = mapping.PropertyMappings[i];

                Type repacementType = GetReplacementType(prop.MappedType);
                sb.AppendFormat(
                    FieldTemplate, i + AutoFieldTagOffset,
                    repacementType != null ? repacementType.Name : prop.PersistentTypeName,
                    prop.PersistentName);
                sb.Append(END).Append(END);
            }

            sb.AppendFormat(ReadFromMethodTemplate, CreateReadMethodBody(mapping));
            sb.Append(END).Append(END);
            sb.AppendFormat(WriteToMethodTemplate, CreateWriteMethodBody(mapping));
            sb.Append(END).Append(END);
            sb.AppendFormat(GetDepsMethodTemplate, CreateDepsMethodBody(mapping));
            sb.Append(END).Append(END);
            sb.AppendFormat(GetDepsFromMethodTemplate, CreateDepsFromMethodBody(mapping));

            return sb.ToString();
        }

        private string CreateReadMethodBody(PersistentClassMapping mapping)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < mapping.PropertyMappings.Length; ++i)
            {
                PersistentPropertyMapping prop = mapping.PropertyMappings[i];
                sb.AppendFormat("{0} = uo.{1};", prop.PersistentName, prop.MappedName);
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
                sb.AppendFormat("uo.{0} = {1};", prop.PersistentName, prop.MappedName);
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
                if(prop.UseSurrogate)
                {
                    sb.AppendFormat("(({0}){1}).GetDeps(dependencies);", prop.PersistentSurrogateTypeName, prop.PersistentName);
                    sb.Append(END);
                }
                else if(prop.MappedType.IsSubclassOf(typeof(UnityObject)))
                {
                    sb.AppendFormat("AddDependency({0}, dependencies);", prop.PersistentName);
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
                if (prop.UseSurrogate)
                {
                    sb.AppendFormat("(({0})uo.{1}).GetDeps(dependencies);", prop.MappedSurrogateTypeName, prop.MappedName);
                    sb.Append(END);
                }
                if (prop.MappedType.IsSubclassOf(typeof(UnityObject)))
                {
                    sb.AppendFormat("AddDependency(uo.{0}, dependencies);", prop.MappedName);
                    sb.Append(END);
                }
            }
            return sb.ToString();
        }

        private string CreateUsings(params PersistentClassMapping[] mappings)
        {
            StringBuilder sb = new StringBuilder();
            HashSet<string> namespaces = new HashSet<string>();

            for (int m = 0; m < mappings.Length; ++m)
            {
                PersistentClassMapping mapping = mappings[m];

                for (int i = 0; i < DefaultNamespaces.Length; ++i)
                {
                    namespaces.Add(DefaultNamespaces[i]);
                }

                if (!namespaces.Contains(mapping.MappedNamespace))
                {
                    namespaces.Add(mapping.MappedNamespace);
                }

                if (!namespaces.Contains(mapping.PersistentNamespace))
                {
                    namespaces.Add(mapping.PersistentNamespace);
                }

                if (!namespaces.Contains(mapping.PersistentBaseNamespace))
                {
                    namespaces.Add(mapping.PersistentBaseNamespace);
                }

                for (int i = 0; i < mapping.PropertyMappings.Length; ++i)
                {
                    PersistentPropertyMapping propertyMapping = mapping.PropertyMappings[i];
                    if (!namespaces.Contains(propertyMapping.MappedNamespace))
                    {
                        namespaces.Add(propertyMapping.MappedNamespace);
                    }

                    Type type = propertyMapping.MappedType;
                    Type replacementType = GetReplacementType(type);
                    if (replacementType != null)
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
            foreach (string ns in namespaces)
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

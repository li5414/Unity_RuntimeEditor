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
            "namespace {1}"                                     + BR +
            "{{"                                                + BR +
            "   [ProtoContract(AsReferenceDefault = true)"      + BR +
            "   public class {2} : {3}"                         + BR +
            "   {{"                                             + BR +
            "       {4}"                                        + BR +
            "   }}"                                             + BR +
            "}}"                                                + END;

        private static readonly string FieldTemplate =
            "[ProtoMember({0})]"                                + BR +
            "public {1} {2};"                                   + END;

        private static readonly string ReadFromMethodTemplate =
            "public override void ReadFrom(object obj)"         + BR +
            "{{"                                                + BR +
            "   base.ReadFrom(obj);"                            + BR +
            "   UnityObject uo = (UnityObject)obj;"             + BR +
            "   {0}"                                            + BR +
            "}}"                                                + END;

        private static readonly string WriteToMethodTemplate =
            "public override object WriteTo(object obj)"        + BR +
            "{{"                                                + BR +
            "   obj = base.WriteTo(obj);"                       + BR +
            "   UnityObject uo = (UnityObject)obj;"             + BR +
            "   {0}"                                            + BR +
            "   return obj;"                                    + BR +
            "}}"                                                + END;

        private static readonly string GetDepsMethodTemplate =
            "protected override void GetDepsImp(GetDepsContext context)"                + BR +
            "{{"                                                                        + BR +
            "   {0}"                                                                    + BR +
            "}}"                                                                        + END;

        private static readonly string GetDepsFromMethodTemplate =
            "protected override void GetDepsFromImpl(object obj, GetDepsFromContext context)"   + BR +
            "{{"                                                                                + BR +
            "   UnityObject uo = (UnityObject)obj;"                                             + BR +
            "   {0}"                                                                            + BR +
            "}}"                                                                                + END;

        private static readonly string ImplicitOperatorsTemplate =
            "public static implicit operator {0}({1} surrogate)"                                + BR +
            "{{"                                                                                + BR +
            "   return ({0})surrogate.WriteTo(new {0}());"                                      + BR +
            "}}"                                                                                + BR +
                                                                                                  BR +
            "public static implicit operator {1}({0} obj)"                                      + BR +
            "{{"                                                                                + BR +
            "   {1} surrogate = new {1}();"                                                     + BR +
            "   surrogate.ReadFrom(obj);"                                                       + BR +
            "   return surrogate;"                                                              + BR +
            "}}"                                                                                + END;

        private static readonly string TypeModelCreatorTemplate =
            "{0}"                                                                       + BR +
            "using UnityObject = UnityEngine.Object;"                                   + BR +
            "namespace Battlehub.RTSaveLoad2"                                           + BR +
            "{{"                                                                        + BR +
            "   public static partial class TypeModelCreator"                           + BR +
            "   {{"                                                                     + BR +
            "       static partial void RegisterAutoTypes(RuntimeTypeModel model)"      + BR +
            "       {{"                                                                 + BR +
            "           {1}"                                                            + BR +
            "       }}"                                                                 + BR +
            "   }}"                                                                     + BR +
            "}}"                                                                        + END;

        private static readonly string AddTypeTemplate =
            "model.Add(typeof({0}), false){1}"                                          + END;

        private static readonly string AddSubtypeTemplate =
            "   .AddSubType({1}, typeof({0}))"                                          ;

        private static readonly string SetSerializationSurrogate = 
            ".SetSurrogate(typeof({0}));"                                               + END;


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

        public bool HasDependencies(Type type)
        {
            return HasDependenciesRecursive(type, new HashSet<Type>());
        }

        private bool HasDependencies(Type type, HashSet<Type> inspectedTypes)
        {
            if(type.IsArray)
            {
                type = type.GetElementType();
            }

            if(inspectedTypes.Contains(type))
            {
                return false;
            }

            inspectedTypes.Add(type);
           
            PropertyInfo[] properties = GetProperties(type);
            for(int i = 0; i < properties.Length; ++i)
            {
                PropertyInfo property = properties[i];
                if(HasDependenciesRecursive(property.PropertyType, inspectedTypes))
                {
                    return true;
                }
            }

            FieldInfo[] fields = GetFields(type);
            for (int i = 0; i < fields.Length; ++i)
            {
                FieldInfo field = fields[i];
                if (HasDependenciesRecursive(field.FieldType, inspectedTypes))
                {
                    return true;
                }
            }

            return false;
        }

        private bool HasDependenciesRecursive(Type type, HashSet<Type> inspectedTypes)
        {
            if (type.IsArray)
            {
                type = type.GetElementType();
            }

            if(type.IsSubclassOf(typeof(UnityObject)))
            {
                return true;
            }

            Type surrogateType = GetSurrogateType(type);
            if(surrogateType != null)
            {
                return HasDependencies(surrogateType, inspectedTypes);
            }
            return false;
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

                string endOfLine = string.Empty;
                if(mapping.Subclasses != null && mapping.Subclasses.Length > 0)
                {
                    endOfLine = CreateAddSubtypesBody(mapping);
                }

                if (GetSurrogateType(Type.GetType(mapping.MappedAssemblyQualifiedName)) != null)
                {
                    endOfLine += string.Format(SetSerializationSurrogate, mapping.PersistentTypeName);
                }
                
                endOfLine += SEMICOLON + END;
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
            }

            return sb.ToString();
        }

        public string CreatePersistentClass(PersistentClassMapping mapping)
        {
            string usings = CreateUsings(mapping);
            string ns = mapping.PersistentNamespace;
            string className = mapping.PersistentTypeName;
            string baseClassName = mapping.PersistentBaseTypeName;
            string body = mapping.IsEnabled ? CreatePersistentClassBody(mapping) : string.Empty;
            return string.Format(PersistentClassTemplate, usings, ns, className, baseClassName, body);
        }

        private string CreatePersistentClassBody(PersistentClassMapping mapping)
        {
            StringBuilder sb = new StringBuilder();

            for(int i = 0; i < mapping.PropertyMappings.Length; ++i)
            {
                PersistentPropertyMapping prop = mapping.PropertyMappings[i];

                string typeName;
                Type repacementType = GetReplacementType(prop.MappedType);
                if(repacementType != null)
                {
                    typeName = repacementType.Name;
                }
                else
                {
                    typeName = prop.PersistentTypeName;
                }

                sb.AppendFormat(
                    FieldTemplate, i + AutoFieldTagOffset,
                    typeName,
                    prop.PersistentName);

                sb.Append(END).Append(END);
            }

            sb.AppendFormat(ReadFromMethodTemplate, CreateReadMethodBody(mapping));
            sb.Append(END);
            sb.AppendFormat(WriteToMethodTemplate, CreateWriteMethodBody(mapping));
            sb.Append(END);
            sb.AppendFormat(GetDepsMethodTemplate, CreateDepsMethodBody(mapping));
            sb.Append(END);
            sb.AppendFormat(GetDepsFromMethodTemplate, CreateDepsFromMethodBody(mapping));
            sb.Append(END);
            sb.AppendFormat(ImplicitOperatorsTemplate, mapping.MappedTypeName, mapping.PersistentTypeName);

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
                if(prop.HasDependenciesOrIsDependencyItself)
                {
                    if (prop.UseSurrogate)
                    {
                        sb.AppendFormat("AddSurrogateDeps({0}, context);", prop.PersistentName);
                        sb.Append(END);
                    }
                    else if (prop.MappedType.IsSubclassOf(typeof(UnityObject)))
                    {
                        sb.AppendFormat("AddDep({0}, context);", prop.PersistentName);
                        sb.Append(END);
                    }
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
                if (prop.HasDependenciesOrIsDependencyItself)
                {
                    if (prop.UseSurrogate)
                    {
                        sb.AppendFormat("AddSurrogateDeps(uo.{0}, context);", prop.MappedName);
                        sb.Append(END);
                    }
                    if (prop.MappedType.IsSubclassOf(typeof(UnityObject)))
                    {
                        sb.AppendFormat("AddDep(uo.{0}, context);", prop.MappedName);
                        sb.Append(END);
                    }
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

                if(mapping.IsEnabled)
                {
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
            }
            foreach (string ns in namespaces)
            {
                sb.Append("using " + ns + ";");
            }

            return sb.ToString();
        }

        private Type GetReplacementType(Type type)
        {
            if(type.IsArray)
            {
                Type elementType = type.GetElementType();
                if(elementType.IsSubclassOf(typeof(UnityObject)))
                {
                    return typeof(int[]);
                }
            }

            if(type.IsSubclassOf(typeof(UnityObject)))
            {
                return typeof(int);
            }
            return null;
        }
    }
}

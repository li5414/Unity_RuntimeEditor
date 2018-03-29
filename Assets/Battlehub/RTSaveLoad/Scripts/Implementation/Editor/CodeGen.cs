using Battlehub.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityObject = UnityEngine.Object;

namespace Battlehub.RTSaveLoad
{
    public class CodeGen 
    {
        public class CodeGenSettings
        {
            public bool IncludeObsoleteTypes = false;
            public string ClassPrefix = "Persistent";
            public string SurrogatePostfix = "Surrogate";
            public string SerializationSurrogatesFileName = "SerializationSurrogates.cs";
            public string Header = "/*This is auto-generated file. Tools->Runtime SaveLoad->Create Persistent Objects " + Environment.NewLine + "If you want prevent overwriting, drag this file to another folder.*/";
        }

        private static Dictionary<Type, HashSet<string>> m_exceptProperties = new Dictionary<Type, HashSet<string>>
            {
                {   typeof(Rigidbody),
                        new HashSet<string> {                                       //properties throwing exception
                            //Strong.MemberInfo((Rigidbody x) => x.useConeFriction ).Name, 5.5
                            Strong.MemberInfo((Rigidbody x) => x.inertiaTensorRotation ).Name,
                            Strong.MemberInfo((Rigidbody x) => x.inertiaTensor ).Name,} },
                {   typeof(Animator),
                        new HashSet<string> {                                       //properties throwing exception
                            Strong.MemberInfo((Animator x) => x.bodyPosition ).Name,
                            Strong.MemberInfo((Animator x) => x.bodyRotation ).Name,
                            Strong.MemberInfo((Animator x) => x.playbackTime ).Name,} },
                {   typeof(AnimationClip),
                        new HashSet<string> {
                            Strong.MemberInfo((AnimationClip x) => x.events ).Name} },
                {   typeof(Camera),
                        new HashSet<string> {                                       //properties preventing other properties to alter camer behavior
                            Strong.MemberInfo((Camera x) => x.worldToCameraMatrix ).Name,
                            Strong.MemberInfo((Camera x) => x.projectionMatrix ).Name,
                            Strong.MemberInfo((Camera x) => x.nonJitteredProjectionMatrix ).Name,
                            Strong.MemberInfo((Camera x) => x.cullingMatrix ).Name,
                            Strong.MemberInfo((Camera x) => x.aspect ).Name,} },
                {   typeof(Transform),
                        new HashSet<string> {                                       //redundant properties
                            Strong.MemberInfo((Transform x) => x.eulerAngles ).Name,
                            Strong.MemberInfo((Transform x) => x.localEulerAngles ).Name,
                            Strong.MemberInfo((Transform x) => x.forward ).Name,
                            Strong.MemberInfo((Transform x) => x.up ).Name,
                            Strong.MemberInfo((Transform x) => x.right ).Name,
                            Strong.MemberInfo((Transform x) => x.localRotation ).Name,
                            Strong.MemberInfo((Transform x) => x.localPosition ).Name,
                            Strong.MemberInfo((Transform x) => x.hasChanged ).Name,
                            Strong.MemberInfo((Transform x) => x.hierarchyCapacity ).Name } },
                {   typeof(MeshFilter),
                        new HashSet<string> {
                            Strong.MemberInfo((MeshFilter x) => x.mesh ).Name } }, //prevent unintented copy

                {   typeof(Light),
                        new HashSet<string> {
                            Strong.MemberInfo((Light x) => x.areaSize ).Name } }, //due to Type `UnityEngine.Light' does not contain a definition for `areaSize' in Unity 5.4.1
                {   typeof(Terrain),
                        new HashSet<string> {
                            Strong.MemberInfo((Terrain x) => x.bakeLightProbesForTrees ).Name } }, //due to Type `UnityEngine.Terrain' does not contain a definition for `bakeLightProbesForTrees' in Unity 5.4.1
                {   typeof(Texture2D),
                        new HashSet<string> {
                            Strong.MemberInfo((Texture2D x) => x.alphaIsTransparency ).Name } }, //due to Type `UnityEngine.Texture2D' does not contain a definition for `alphaIsTransparency' in Unity 5.4.1
                {   typeof(SkinnedMeshRenderer),
                        new HashSet<string> {
                            Strong.MemberInfo((SkinnedMeshRenderer x) => x.updateWhenOffscreen ).Name }},
                {   typeof(Renderer),
                        new HashSet<string> {
                            Strong.MemberInfo((Renderer x) => x.material ).Name, //prevent unintented copy
                            Strong.MemberInfo((Renderer x) => x.materials ).Name } },
                {   typeof(Collider),
                        new HashSet<string> {
                            Strong.MemberInfo((Collider x) => x.material ).Name} }, //prevent unintented copy
                { typeof(LightProbes),
                        new HashSet<string> {
                            Strong.MemberInfo((LightProbes x) => x.bakedProbes).Name} },
                {   typeof(Rect),
                        new HashSet<string> {                                   //redundant properties
                            Strong.MemberInfo((Rect x) => x.center).Name,
                            Strong.MemberInfo((Rect x) => x.max).Name,
                            Strong.MemberInfo((Rect x) => x.min).Name,
                            Strong.MemberInfo((Rect x) => x.position).Name,
                            Strong.MemberInfo((Rect x) => x.size).Name,
                            Strong.MemberInfo((Rect x) => x.xMax).Name,
                            Strong.MemberInfo((Rect x) => x.yMax).Name,
                            Strong.MemberInfo((Rect x) => x.xMin).Name,
                            Strong.MemberInfo((Rect x) => x.yMin).Name} }

            };
        
        private static Dictionary<Type, string> m_primitiveNames = new Dictionary<Type, string>
        {
            { typeof(string), "string" },
            { typeof(int), "int" },
            { typeof(long), "long" },
            { typeof(short), "short" },
            { typeof(byte), "byte" },
            { typeof(ulong), "ulong" },
            { typeof(uint), "uint" },
            { typeof(ushort), "ushort" },
            { typeof(char), "char" },
            { typeof(object), "object" },
            { typeof(float), "float" },
            { typeof(double), "double" },
            { typeof(bool), "bool" },
            { typeof(string[]), "string[]" },
            { typeof(long[]), "long[]" },
            { typeof(int[]), "int[]" },
            { typeof(short[]), "short[]" },
            { typeof(byte[]), "byte[]" },
            { typeof(ulong[]), "ulong[]" },
            { typeof(uint[]), "uint[]" },
            { typeof(ushort[]), "ushort[]" },
            { typeof(char[]), "char[]" },
            { typeof(object[]), "object[]" },
            { typeof(float[]), "float[]" },
            { typeof(double[]), "double[]" },
            { typeof(bool[]), "bool[]" },
        };

        private static HashSet<Type> m_extraTypes = new HashSet<Type>
        {
            typeof(GUIStyle),
            typeof(GUIStyleState),
            typeof(DetailPrototype),
            typeof(TreePrototype),
            typeof(SplatPrototype)
        };

        private const string methodsPlaceholder = "$METHOD$";
        private CodeGenSettings m_settings;

        private int m_uniqueId = 1000;

        public CodeGen()
        {
            m_settings = new CodeGenSettings();
        }

        public CodeGen(CodeGenSettings settings)
        {
            if(settings == null)
            {
                settings = new CodeGenSettings();
            }
            m_settings = settings;
        }

      
        public void GenerateSerializationSurrogates(string path, Assembly[] assemblies)
        {
            string fullPath = Application.dataPath + path;
            Directory.CreateDirectory(fullPath);

            HashSet<Type> requireSurrogateTypesHs = new HashSet<Type>();
            requireSurrogateTypesHs.Add(typeof(GradientAlphaKey));
            requireSurrogateTypesHs.Add(typeof(GradientColorKey));
            requireSurrogateTypesHs.Add(typeof(LayerMask));
            requireSurrogateTypesHs.Add(typeof(RectOffset));
            requireSurrogateTypesHs.Add(typeof(AnimationTriggers));
            requireSurrogateTypesHs.Add(typeof(ColorBlock));
            requireSurrogateTypesHs.Add(typeof(NavMeshPath));
            requireSurrogateTypesHs.Add(typeof(ClothSkinningCoefficient));
            requireSurrogateTypesHs.Add(typeof(BoneWeight));
            requireSurrogateTypesHs.Add(typeof(TreeInstance));
            requireSurrogateTypesHs.Add(typeof(CharacterInfo));

            HashSet<Type> psNestedTypes = new HashSet<Type>(GetParticleSystemNestedTypes());

            Type[] unityTypes = assemblies.SelectMany(asm => asm.GetTypes().Where(t => t.IsSubclassOf(typeof(UnityObject)))).Union(new[] { typeof(UnityObject) }).ToArray();
            for (int i = 0; i < unityTypes.Length; ++i)
            {
                Type type = unityTypes[i];
                if (type.IsNested)
                {
                    continue;
                }
                if (!IsTypeSupported(type))
                {
                    continue;
                }

                FieldInfo[] fields = type.GetSerializableFields();
                for (int j = 0; j < fields.Length; ++j)
                {
                    FieldInfo field = fields[j];

                    Type fieldType = field.FieldType;
                    if (fieldType.IsValueType && !fieldType.IsSerializable && !requireSurrogateTypesHs.Contains(fieldType) && !psNestedTypes.Contains(fieldType))
                    {
                        requireSurrogateTypesHs.Add(fieldType);
                    }
                }

                if (type.IsScript())
                {
                    continue;   
                }

                bool setterRequired = false;
                PropertyInfo[] properties = GetSerializableProperties(type, setterRequired);
                for(int j = 0; j < properties.Length; ++j)
                {
                    PropertyInfo property = properties[j];
                    Type propertyType = property.PropertyType;
                    if (propertyType.IsValueType && !propertyType.IsSerializable && !requireSurrogateTypesHs.Contains(propertyType) && !psNestedTypes.Contains(propertyType))
                    {
                        requireSurrogateTypesHs.Add(propertyType);
                    }
                }
            }

            StringBuilder ssSB = new StringBuilder();
            ssSB.AppendLine(m_settings.Header);
            ssSB.AppendLine();
            BeginPartialClassWithStaticCtor(ssSB, typeof(SerializationSurrogates), "static");

            Type[] requireSurrogateTypes = requireSurrogateTypesHs.ToArray();
            List<string> surrogates = new List<string>();
            string ns = typeof(SerializationSurrogates).Namespace;
            for (int i = 0; i < requireSurrogateTypes.Length; ++i)
            {
                Type type = requireSurrogateTypes[i];
                string typeNs = ns;
                if (type.Namespace != null)
                {
                    typeNs += "." + type.Namespace.Replace(".", "NS.") + "NS";
                }

                string surrogateFullTypeName = CombineNamespaceWithTypeName(typeNs, type.Name + m_settings.SurrogatePostfix);
                if(typeof(SerializationSurrogates).Assembly.GetType(surrogateFullTypeName) != null)
                {
                    if(!File.Exists(Path.Combine(fullPath, m_settings.SerializationSurrogatesFileName)))
                    {
                        Debug.LogFormat("Serialization surrogates already defined elsewhere. Found Serialization Surrogate of type {0}", surrogateFullTypeName);
                        return;
                    }
                }

                ssSB.AppendFrmt(3, "m_surrogates.Add(typeof({0}), new {1}());", type.FullName.Replace('+', '.'), surrogateFullTypeName);

                string surrogate = GenerateSerializationSurrogate(type);
                surrogates.Add(surrogate);
            }
            EndPartialClassWithStaticCtor(ssSB);
            ssSB.AppendLine();
            for (int i = 0; i < surrogates.Count; ++i)
            {
                string surrogate = surrogates[i];
                ssSB.Append(surrogate);
                ssSB.AppendLine();
            }

            string usings = string.Format("using {0};", typeof(ISerializationSurrogate).Namespace);
            StringBuilder usingsSB = new StringBuilder();

            usingsSB.AppendLine("#define RT_USE_PROTOBUF");
            usingsSB.AppendLine("#if RT_USE_PROTOBUF");
            usingsSB.AppendLine("using ProtoBuf;");
            usingsSB.AppendLine("#endif");

            usingsSB.AppendLine(usings);
            usingsSB.Append(ssSB.ToString());
            
            File.WriteAllText(Path.Combine(fullPath, m_settings.SerializationSurrogatesFileName), usingsSB.ToString());
        }

        private string GenerateSerializationSurrogate(Type type)
        {
            string ns = typeof(SerializationSurrogates).Namespace;
            StringBuilder classSB = new StringBuilder();
            if(type.Namespace != null)
            {
                ns += "." + type.Namespace.Replace(".", "NS.") + "NS";
            }

            classSB.AppendFrmt(0, "namespace {0}", ns);
            classSB.AppendLine(0, "{");
            classSB.AppendLine(1, "#if RT_USE_PROTOBUF");
            classSB.AppendLine(1, "[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]");
            classSB.AppendLine(1, "#endif");
            classSB.AppendLine(1, "#if !UNITY_WINRT");
            classSB.AppendFrmt(1, "public class {0} : {1}", type.Name + m_settings.SurrogatePostfix, typeof(ISerializationSurrogate).Name);
            classSB.AppendLine(1, "#else");
            classSB.AppendFrmt(1, "public class {0}", type.Name + m_settings.SurrogatePostfix);
            classSB.AppendLine(1, "#endif");
            classSB.AppendLine(1, "{");

            MethodInfo getObjectDataMethod = Strong.MethodInfo<ISerializationSurrogate>(x => new Action<object, SerializationInfo, StreamingContext>(x.GetObjectData));
            MethodInfo setObjectDataMethod = Strong.MethodInfo<ISerializationSurrogate>(x => new Func<object, SerializationInfo, StreamingContext, ISurrogateSelector, object>(x.SetObjectData));

            HashSet<string> exceptProperties;
            m_exceptProperties.TryGetValue(type, out exceptProperties);
            if (exceptProperties == null)
            {
                exceptProperties = new HashSet<string>();
            }

            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(f => IsTypeSupported(f.FieldType) /*&& f.FieldType.IsSerializable*/ )
                .Where(f => m_settings.IncludeObsoleteTypes ||
                            !f.IsDefined(typeof(ObsoleteAttribute), true)).ToArray();

            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(p => IsTypeSupported(p.PropertyType) && 
                            /*p.PropertyType.IsSerializable &&*/
                            p.GetSetMethod() != null && 
                            p.GetGetMethod() != null &&
                            !exceptProperties.Contains(p.Name) &&
                            p.GetIndexParameters().Length == 0)
                .Where(p => m_settings.IncludeObsoleteTypes ||
                            !p.IsDefined(typeof(ObsoleteAttribute), true)).ToArray();

            if(fields.Length > 0 || properties.Length > 0)
            {
                for (int i = 0; i < fields.Length; ++i)
                {
                    FieldInfo field = fields[i];
                    if (field.FieldType.IsEnum())
                    {
                        classSB.AppendFrmt(2, "public uint {0};", field.Name);
                    }
                    else
                    {
                        classSB.AppendFrmt(2, "public {0} {1};", field.FieldType.FullName.Replace("+", "."), field.Name);
                    }
                }
                for (int i = 0; i < properties.Length; ++i)
                {
                    PropertyInfo property = properties[i];
                    if (property.PropertyType.IsEnum())
                    {
                        classSB.AppendFrmt(2, "public uint {0};", property.Name);
                    }
                    else
                    {
                        classSB.AppendFrmt(2, "public {0} {1};", property.PropertyType.FullName.Replace("+", "."), property.Name);
                    }
                }

             
                classSB.AppendFrmt(2, "public static implicit operator {0}({1} v)", type.FullName.Replace('+', '.'), type.Name + m_settings.SurrogatePostfix);
                classSB.AppendLine(2, "{");
                classSB.AppendFrmt(3, "{0} o =  new {0}();", type.FullName.Replace('+', '.'));
                for (int i = 0; i < fields.Length; ++i)
                {
                    FieldInfo field = fields[i];
                    if(field.FieldType.IsEnum())
                    {
                        classSB.AppendFrmt(3, "o.{0} = ({1})v.{0};", field.Name, field.FieldType.FullName.Replace('+', '.'));
                    }
                    else
                    {
                        classSB.AppendFrmt(3, "o.{0} = v.{0};", field.Name);
                    }
                    
                }
                for (int i = 0; i < properties.Length; ++i)
                {
                    PropertyInfo property = properties[i];
                    if (property.PropertyType.IsEnum())
                    {
                        classSB.AppendFrmt(3, "o.{0} = ({1})v.{0};", property.Name, property.PropertyType.FullName.Replace('+', '.'));
                    }
                    else
                    {
                        classSB.AppendFrmt(3, "o.{0} = v.{0};", property.Name);
                    }
                    
                }
                classSB.AppendLine(3, "return o;");
                classSB.AppendLine(2, "}");

                classSB.AppendFrmt(2, "public static implicit operator {0}({1} v)", type.Name + m_settings.SurrogatePostfix, type.FullName.Replace('+', '.'));
                classSB.AppendLine(2, "{");
                classSB.AppendFrmt(3, "{0} o =  new {0}();", type.Name + m_settings.SurrogatePostfix);
                for (int i = 0; i < fields.Length; ++i)
                {
                    FieldInfo field = fields[i];
                    if (field.FieldType.IsEnum())
                    {
                        classSB.AppendFrmt(3, "o.{0} = (uint)v.{0};", field.Name);
                    }
                    else
                    {
                        classSB.AppendFrmt(3, "o.{0} = v.{0};", field.Name);
                    }
                }
                for (int i = 0; i < properties.Length; ++i)
                {
                    PropertyInfo property = properties[i];
                    if (property.PropertyType.IsEnum())
                    {
                        classSB.AppendFrmt(3, "o.{0} = (uint)v.{0};", property.Name);
                    }
                    else
                    {
                        classSB.AppendFrmt(3, "o.{0} = v.{0};", property.Name);
                    }
                }
                classSB.AppendLine(3, "return o;");
                classSB.AppendLine(2, "}");

                classSB.AppendLine(2, "#if !UNITY_WINRT");
                classSB.AppendFrmt(2, "public void {0}(object obj, SerializationInfo info, StreamingContext context)", getObjectDataMethod.Name);
                classSB.AppendLine(2, "{");
                classSB.AppendFrmt(3, "{0} o = ({0})obj;", type.FullName.Replace('+', '.'));
                for (int i = 0; i < fields.Length; ++i)
                {
                    FieldInfo field = fields[i];
                    classSB.AppendFrmt(3, "info.AddValue(\"{0}\", o.{0});", field.Name);
                }
                for (int i = 0; i < properties.Length; ++i)
                {
                    PropertyInfo property = properties[i];
                    classSB.AppendFrmt(3, "info.AddValue(\"{0}\", o.{0});", property.Name);
                }
                classSB.AppendLine(2, "}");
                classSB.AppendFrmt(2, "public object {0}(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)", setObjectDataMethod.Name);
                classSB.AppendLine(2, "{");
                classSB.AppendFrmt(3, "{0} o = ({0})obj;", type.FullName.Replace('+', '.'));
                for (int i = 0; i < fields.Length; ++i)
                {
                    FieldInfo field = fields[i];
                    string fieldTypeName;
                    if (m_primitiveNames.ContainsKey(field.FieldType))
                    {
                        fieldTypeName = m_primitiveNames[field.FieldType];
                    }
                    else
                    {
                        fieldTypeName = field.FieldType.FullName.Replace('+', '.');
                    }
                    classSB.AppendFrmt(3, "o.{0} = ({1})info.GetValue(\"{0}\", typeof({1}));", field.Name, fieldTypeName);
                }
                for (int i = 0; i < properties.Length; ++i)
                {
                    PropertyInfo property = properties[i];
                    string propertyTypeName;
                    if (m_primitiveNames.ContainsKey(property.PropertyType))
                    {
                        propertyTypeName = m_primitiveNames[property.PropertyType];
                    }
                    else
                    {
                        propertyTypeName = property.PropertyType.FullName.Replace('+', '.');
                    }
                    classSB.AppendFrmt(3, "o.{0} = ({1})info.GetValue(\"{0}\", typeof({1}));", property.Name, propertyTypeName);
                }
                classSB.AppendLine(3, "return o;");
                classSB.AppendLine(2, "}");
                classSB.AppendLine(2, "#endif");
            }
            else
            {
                classSB.AppendFrmt(2, "public static implicit operator {0}({1} v)", type.FullName.Replace('+', '.'), type.Name + m_settings.SurrogatePostfix);
                classSB.AppendLine(2, "{");
                classSB.AppendFrmt(3, "{0} o =  new {0}();", type.FullName.Replace('+', '.'));
                classSB.AppendLine(3, "return o;");
                classSB.AppendLine(2, "}");

                classSB.AppendFrmt(2, "public static implicit operator {0}({1} v)", type.Name + m_settings.SurrogatePostfix, type.FullName.Replace('+', '.'));
                classSB.AppendLine(2, "{");
                classSB.AppendFrmt(3, "{0} o =  new {0}();", type.Name + m_settings.SurrogatePostfix);
                classSB.AppendLine(3, "return o;");
                classSB.AppendLine(2, "}");

                classSB.AppendLine(2, "#if !UNITY_WINRT");
                classSB.AppendFrmt(2, "public void {0}(object obj, SerializationInfo info, StreamingContext context) {{ }}", getObjectDataMethod.Name);
                classSB.AppendFrmt(2, "public object {0}(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector) {{ return obj; }}", setObjectDataMethod.Name);
                classSB.AppendLine(2, "#endif");
            }

            classSB.AppendLine(1, "}");
            classSB.AppendLine(0, "}");
            return classSB.ToString();
        }

        private Type[] GetParticleSystemNestedTypes()
        {
            Type[] types = typeof(ParticleSystem).GetNestedTypes(BindingFlags.Public | BindingFlags.Instance);
            return types.Union(new[] { typeof(AnimationCurve), typeof(Keyframe), typeof(Gradient) })
                .Distinct().ToArray();
        }

        //private Type[] GetUITypes()
        //{
        //    return new[] { typeof(Navigation), typeof(Dropdown.OptionData), typeof(SpriteState) };
        //}

        public void GenerateParticleSystemPersistentModules(string path)
        {
            string fullPath = Application.dataPath + path;
            Directory.CreateDirectory(fullPath);

            List<string> persistentFilesContent = new List<string>();
            List<string> persistentFileNames = new List<string>();

            Type[] psTypes = GetParticleSystemNestedTypes();
            HashSet<Type> persistentTypes = new HashSet<Type>(psTypes);
            
            for (int i = 0; i < psTypes.Length; ++i)
            {
                Type psType = psTypes[i];
                if (!IsTypeSupported(psType))
                {
                    continue;
                }

                bool overwrite = true;
                string persistentNamespace = GetNormalizedNS(psType, persistentTypes);
                string persistentTypeName = CombineNamespaceWithTypeName(persistentNamespace, GetPersistentTypeName(psType));
                string persistentFileName = GetPersistentTypeName(psType) + ".cs";
                Type persistentType = typeof(PersistentData).Assembly.GetType(persistentTypeName);
                if (persistentType != null)
                {
                    overwrite = File.Exists(Path.Combine(fullPath, persistentFileName));
                    if (!overwrite)
                    {
                        Debug.LogFormat("Type already defined elsewhere. Skip {0}", persistentType.FullName);
                    }
                }

                if(overwrite)
                {
                    string persistentFileContent = GeneratePersistentType(psType, GetDerivedTypes(psType, psTypes), persistentTypes);
                    persistentFileNames.Add(persistentFileName);
                    persistentFilesContent.Add(persistentFileContent);
                }
            }

            for (int i = 0; i < persistentFilesContent.Count; ++i)
            {
                File.WriteAllText(Path.Combine(fullPath, persistentFileNames[i]), persistentFilesContent[i]);
            }
        }

        public void GeneratePersistentClasses(string path, Assembly[] assemblies)
        {
            string fullPath = Application.dataPath + path;
            Directory.CreateDirectory(fullPath);

            Type[] psTypes = GetParticleSystemNestedTypes();
            HashSet<Type> persistentTypes = new HashSet<Type>(psTypes);
            foreach(Type t in m_extraTypes)
            {
                if(!persistentTypes.Contains(t))
                {
                    persistentTypes.Add(t);
                }    
            }
            

            Type[] unityTypes = assemblies.SelectMany(asm => asm.GetTypes().Where(t => t.IsSubclassOf(typeof(UnityObject))))
                .Union(new[] { typeof(UnityObject) })
                .Union(m_extraTypes)
                .ToArray();
            List<string> persistentFilesContent = new List<string>();
            List<string> persistentFileNames = new List<string>();

            StringBuilder persistentDataSB = new StringBuilder();
            //persistentDataSB.AppendLine("#define RT_PE_MAINTANANCE");
            BeginPartialClassWithStaticCtor(persistentDataSB, typeof(PersistentData), "abstract");

            for (int i = 0; i < unityTypes.Length; ++i)
            {
                Type type = unityTypes[i];
                if (type.IsNested)
                {
                    continue;
                }
                if (!IsTypeSupported(type))
                {
                    continue;
                }

                string persistentNamespace = GetNormalizedNS(type, persistentTypes);
                string persistentTypeName = CombineNamespaceWithTypeName(persistentNamespace, GetPersistentTypeName(type));

                bool overwrite = true;
                string persistentFileName = GetPersistentTypeName(type) + ".cs";
                Type persistentType = typeof(PersistentData).Assembly.GetType(persistentTypeName); 
                if (persistentType != null)
                {
                    overwrite = File.Exists(Path.Combine(fullPath, persistentFileName));
                    if(!overwrite)
                    {
                        Debug.LogFormat("Type already defined elsewhere. Skip {0}", persistentType.FullName);
                    }
                }

                persistentDataSB.AppendFrmt(3, "m_objToData.Add(typeof({0}), typeof({1}));", type.FullName, persistentTypeName);
                if (overwrite)
                {
                    string persistentTypeFileContent = GeneratePersistentType(type, GetDerivedTypes(type, assemblies), persistentTypes);
                    persistentFilesContent.Add(persistentTypeFileContent);
                    persistentFileNames.Add(persistentFileName);
                }
            }

            EndPartialClassWithStaticCtor(persistentDataSB);

            persistentFilesContent.Add(persistentDataSB.ToString());
            persistentFileNames.Add(typeof(PersistentData).Name + ".cs");

            for (int i = 0; i < persistentFilesContent.Count; ++i)
            {
                File.WriteAllText(Path.Combine(fullPath, persistentFileNames[i]), persistentFilesContent[i]);
            }
        }

        private Type[] GetDerivedTypes(Type type, Assembly[] assemblies)
        {
            return assemblies.SelectMany(asm => asm.GetTypes()).Where(t => t.BaseType == type && IsTypeSupported(t)).ToArray();
        }

        private Type[] GetDerivedTypes(Type type, Type[] types)
        {
            return types.Where(t => t.BaseType == type && IsTypeSupported(t)).ToArray();
        }

        private string GeneratePersistentType(Type type, Type[] derived, HashSet<Type> persistentTypes)
        {
            Type persistentDataType = typeof(PersistentData);
            string typeFullName = type.FullName.Replace('+', '.');
            string persistentTypeName = GetPersistentTypeName(type);
            string persistentTypeNs = GetNormalizedNS(type, persistentTypes);
            string baseTypeFullName;
            if (!type.IsSubclassOf(typeof(UnityObject)))
            {
                Type baseType = persistentDataType;
                baseTypeFullName = baseType.FullName.Replace('+', '.');
            }
            else
            {
                Type baseType = type.BaseType;
                baseTypeFullName = CombineNamespaceWithTypeName(GetNormalizedNS(baseType, persistentTypes), GetPersistentTypeName(baseType));
            }

            MethodInfo getInstanceIDSMethod = Strong.MethodInfo<UnityObject[]>(x => new Func<long[]>(x.GetMappedInstanceID));
            MethodInfo getInstanceIDMethod = Strong.MethodInfo<UnityObject>(x => new Func<long>(x.GetMappedInstanceID));
            MethodInfo loadFormMethod = Strong.MethodInfo<PersistentData>(x => new Action<UnityObject>(x.ReadFrom));
            StringBuilder loadFromSB = new StringBuilder();
            loadFromSB.AppendFrmt(2, "public override void {0}(object obj)", loadFormMethod.Name);
            loadFromSB.AppendLine(2, "{");
            loadFromSB.AppendFrmt(3, "base.{0}(obj);", loadFormMethod.Name);
            loadFromSB.AppendLine(3, "if(obj == null)");
            loadFromSB.AppendLine(3, "{");
            loadFromSB.AppendLine(4, "return;");
            loadFromSB.AppendLine(3, "}");
            loadFromSB.AppendFrmt(3, "{0} o = ({0})obj;", typeFullName);

            MethodInfo resolveMethod = Strong.MethodInfo<Dictionary<long, UnityObject>>(x => new Func<long, UnityObject>(x.Get));
            MethodInfo saveToMethod = Strong.MethodInfo<PersistentData>(x => new Func<UnityObject, Dictionary<long, UnityObject>, object>(x.WriteTo));
            StringBuilder saveToSB = new StringBuilder();
            saveToSB.AppendFrmt(2, "public override object {0}(object obj, System.Collections.Generic.Dictionary<long, {1}> objects)", saveToMethod.Name, typeof(UnityObject).FullName);
            saveToSB.AppendLine(2, "{");
            saveToSB.AppendFrmt(3, "obj = base.{0}(obj, objects);", saveToMethod.Name);
            saveToSB.AppendLine(3, "if(obj == null)");
            saveToSB.AppendLine(3, "{");
            saveToSB.AppendLine(4, "return null;");
            saveToSB.AppendLine(3, "}");
            saveToSB.AppendFrmt(3, "{0} o = ({0})obj;", typeFullName);

            StringBuilder getDepSB = new StringBuilder();
            getDepSB.AppendLine(2, "protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)");
            getDepSB.AppendLine(2, "{");
            getDepSB.AppendLine(3, "base.GetDependencies(dependencies, obj);");
            getDepSB.AppendLine(3, "if(obj == null)");
            getDepSB.AppendLine(3, "{");
            getDepSB.AppendLine(4, "return;");
            getDepSB.AppendLine(3, "}");
            getDepSB.AppendFrmt(3, "{0} o = ({0})obj;", typeFullName);

            MethodInfo findDepMethod = Strong.MethodInfo<PersistentData>(x => new Action<Dictionary<long, UnityObject>, Dictionary<long, UnityObject>, bool>(x.FindDependencies));
            StringBuilder findDepSB = new StringBuilder();
            findDepSB.AppendFrmt(2, "public override void {0}<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)", findDepMethod.Name);
            findDepSB.AppendLine(2, "{");
            findDepSB.AppendFrmt(3, "base.{0}(dependencies, objects, allowNulls);", findDepMethod.Name);

            StringBuilder classSB = new StringBuilder();
            classSB.AppendLine(m_settings.Header);
            classSB.AppendLine();
            classSB.AppendFrmt(0, "namespace {0}", persistentTypeNs);
            classSB.AppendLine(0, "{");
            classSB.AppendLine(1, "#if RT_USE_PROTOBUF");
            classSB.AppendLine(1, "[ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]");

            for (int i = 0; i < derived.Length; ++i)
            {
                Type derivedType = derived[i];
                string derivedTypeName = GetPersistentTypeName(derivedType);
                string derivedTypeNS = GetNormalizedNS(derivedType, persistentTypes);
                string derivedTypeFullName = CombineNamespaceWithTypeName(derivedTypeNS, derivedTypeName);
                classSB.AppendFrmt(1, "[ProtoInclude({0}, typeof({1}))]", m_uniqueId, derivedTypeFullName);
                m_uniqueId++;
            }

            classSB.AppendLine(1, "#endif");
            classSB.AppendLine(1, "[System.Serializable]");
            classSB.AppendFrmt(1, "public class {0} : {1}", persistentTypeName, baseTypeFullName);
            classSB.AppendLine(1, "{");
            classSB.Append(methodsPlaceholder);

            int addedPropertiesCount = 0;
            int addedUnityPropertiesCount = 0;
            int addedPersistentPropertiesCount = 0;

            bool requireSetter = true;// typeof(ParticleSystem) != type;

            PropertyInfo[] properties = GetSerializableProperties(type, requireSetter);
            for (int i = 0; i < properties.Length; ++i)
            {
                PropertyInfo property = properties[i];
                Type propertyType = property.PropertyType;

                bool isUnityObjectProperty = IsUnityObjectType(propertyType);
                bool isPersistentProperty = ContainsType(propertyType, persistentTypes);
                string propertyName = property.Name;
                string propertyTypeName;
                if (isUnityObjectProperty)
                {
                    if (propertyType.IsArray)
                    {
                        propertyTypeName = "long[]";
                    }
                    else
                    {
                        propertyTypeName = "long";
                    }
                }
                else if (isPersistentProperty)
                {
                    propertyTypeName = CombineNamespaceWithTypeName(GetNormalizedNS(propertyType, persistentTypes), GetPersistentTypeName(propertyType));

                    if (propertyType.IsArray)
                    {
                        persistentTypeName += "[]";
                    }
                }
                else
                {
                    if (propertyType.IsNested)
                    {
                        propertyTypeName = propertyType.FullName.Replace("+", ".");
                    }
                    else
                    {
                        propertyTypeName = propertyType.FullName;
                        if (m_primitiveNames.ContainsKey(propertyType))
                        {
                            propertyTypeName = m_primitiveNames[propertyType];
                        }
                    }
                }

                bool isOverrideMethod = false;
                MethodInfo getMethod = property.GetGetMethod(false);
                if (getMethod.IsVirtual)
                {
                    isOverrideMethod = getMethod.GetBaseDefinition() != getMethod;
                }

                if (!isOverrideMethod) //No need to generate properties for overriden properties
                {
                    if (propertyType.IsEnum())
                    {
                        classSB.AppendFrmt(2, "public uint {0};", propertyName);
                    }
                    else
                    {
                        classSB.AppendFrmt(2, "public {0} {1};", propertyTypeName, propertyName);
                    }
                    
                    classSB.AppendLine();

                    if (isUnityObjectProperty)
                    {
                        if (propertyType.IsArray)
                        {
                            loadFromSB.AppendFrmt(3, "{0} = o.{0}.{1}();", propertyName, getInstanceIDSMethod.Name);
                            saveToSB.AppendFrmt(3, "o.{0} = Resolve<{1}, UnityEngine.Object>({0}, objects);", propertyName, propertyType.GetElementType().FullName);
                            getDepSB.AppendFrmt(3, "AddDependencies(o.{0}, dependencies);", propertyName);
                            findDepSB.AppendFrmt(3, "AddDependencies({0}, dependencies, objects, allowNulls);", propertyName);
                        }
                        else
                        {
                            loadFromSB.AppendFrmt(3, "{0} = o.{0}.{1}();", propertyName, getInstanceIDMethod.Name);
                            saveToSB.AppendFrmt(3, "o.{0} = ({1})objects.{2}({0});", propertyName, propertyType.FullName, resolveMethod.Name);
                            getDepSB.AppendFrmt(3, "AddDependency(o.{0}, dependencies);", propertyName);
                            findDepSB.AppendFrmt(3, "AddDependency({0}, dependencies, objects, allowNulls);", propertyName);
                        }

                        addedUnityPropertiesCount++;
                    }
                    else if (isPersistentProperty)
                    {
                        loadFromSB.AppendFrmt(3, "{0} = Read({0}, o.{0});", propertyName);
                        saveToSB.AppendFrmt(3, "o.{0} = Write(o.{0}, {0}, objects);", propertyName);
                        getDepSB.AppendFrmt(3, "GetDependencies({0}, o.{0}, dependencies);", propertyName);
                        findDepSB.AppendFrmt(3, "FindDependencies({0}, dependencies, objects, allowNulls);", propertyName);

                        addedPersistentPropertiesCount++;
                    }
                    else
                    {
                        if(propertyType.IsEnum())
                        {
                            loadFromSB.AppendFrmt(3, "{0} = (uint)o.{0};", propertyName);
                        }
                        else
                        {
                            loadFromSB.AppendFrmt(3, "{0} = o.{0};", propertyName);
                        }

                        if (propertyName == "tag")
                        {
                            saveToSB.AppendFrmt(3, "try {{ o.{0} = {0}; }}", propertyName);
                            saveToSB.AppendLine(3, "catch (UnityEngine.UnityException e) { UnityEngine.Debug.LogWarning(e.Message); }");
                        }
                        else
                        {
                            if(propertyType.IsEnum())
                            {
                                saveToSB.AppendFrmt(3, "o.{0} = ({1}){0};", propertyName, propertyTypeName);
                            }
                            else
                            {
                                saveToSB.AppendFrmt(3, "o.{0} = {0};", propertyName);
                            }
                            
                        }
                    }
                    addedPropertiesCount++;
                }
            }

            if (addedPropertiesCount > 0)
            {
                loadFromSB.AppendLine(2, "}");
                getDepSB.AppendLine(2, "}");
                findDepSB.AppendLine(2, "}");

                saveToSB.AppendLine(3, "return o;");
                saveToSB.AppendLine(2, "}");
                saveToSB.AppendLine();

                saveToSB.Append(loadFromSB.ToString()).AppendLine();
                saveToSB.Append(findDepSB.ToString()).AppendLine();
                if (addedUnityPropertiesCount > 0 || addedPersistentPropertiesCount > 0)
                {
                    saveToSB.Append(getDepSB.ToString()).AppendLine();
                }

                classSB.AppendLine(1, "}");
                classSB.AppendLine("}");

                StringBuilder usingsSB = new StringBuilder();
                usingsSB.AppendLine("#define RT_USE_PROTOBUF");
                usingsSB.AppendLine("#if RT_USE_PROTOBUF");
                usingsSB.AppendLine("using ProtoBuf;");
                usingsSB.AppendLine("#endif");

                if (addedUnityPropertiesCount > 0)
                {
                    string dictionaryExtNs = typeof(DictionaryExt).Namespace;
                    string objectExtNs = typeof(ObjectExt).Namespace;

                    if (!string.IsNullOrEmpty(dictionaryExtNs))
                    {
                        usingsSB.AppendFrmt(0, "using {0};", dictionaryExtNs);
                    }

                    if (dictionaryExtNs != objectExtNs)
                    {
                        if (!string.IsNullOrEmpty(objectExtNs))
                        {
                            usingsSB.AppendFrmt(0, "using {0}", objectExtNs);
                        }
                    }
                }

                return usingsSB.Append(classSB.Replace(methodsPlaceholder, saveToSB.ToString()).ToString()).ToString();
            }

            StringBuilder useProtobuf = new StringBuilder();
            useProtobuf.AppendLine("#define RT_USE_PROTOBUF");
            useProtobuf.AppendLine("#if RT_USE_PROTOBUF");
            useProtobuf.AppendLine("using ProtoBuf;");
            useProtobuf.AppendLine("#endif");

            classSB.AppendLine(1, "}");
            classSB.AppendLine("}");
            return useProtobuf.Append(classSB.Replace(methodsPlaceholder, string.Empty)).ToString();

        }

  
        public bool IsTypeSupported(Type type)
        {
            if (type.IsGenericType)
            {
                return false;
            }
            if (type.IsNotPublic)
            {
                return false;
            }
            if(type.IsNestedFamORAssem)
            {
                return false;
            }

            if (!m_settings.IncludeObsoleteTypes)
            {
                if (type.IsArray)
                {
                    Type elementType = type.GetElementType();
                    if (elementType.IsDefined(typeof(ObsoleteAttribute), true))
                    {
                        return false;
                    }
                }
                else
                {
                    if (type.IsDefined(typeof(ObsoleteAttribute), true))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private string GetPersistentTypeName(Type type)
        {
            return m_settings.ClassPrefix + type.Name;
        }

        private static string CombineNamespaceWithTypeName(string ns, string name)
        {
            return string.Format("{0}.{1}", ns, name);
        }

        private static string GetNormalizedNS(Type type, HashSet<Type> persistentTypes)
        {
            if(type.IsArray)
            {
                type = type.GetElementType();
            }
            
            if(persistentTypes == null || !persistentTypes.Contains(type))
            {
                if (!IsUnityObjectType(type))
                {
                    return type.Namespace;
                }
                if (type.IsNested)
                {
                    return type.Namespace;
                }
            }

            string persistentObjectsNs = "Battlehub.RTSaveLoad.PersistentObjects";
            if (string.IsNullOrEmpty(type.Namespace))
            {
                return persistentObjectsNs;
            }

            int index = type.Namespace.IndexOf(".");
            if (index < 0)
            {
                return persistentObjectsNs;
            }
            else
            {
                return string.Format("{0}.{1}", persistentObjectsNs, type.Namespace.Remove(0, index + 1));
            }
        }

        private static bool ContainsType(Type type, HashSet<Type> types)
        {
            if(types == null)
            {
                return false;
            }

            if (type.IsArray)
            {
                Type elementType = type.GetElementType();
                return types.Contains(elementType);
            }

            return types.Contains(type);
        }

        private static bool IsUnityObjectType(Type type)
        {
            if(type.IsArray)
            {
                Type elementType = type.GetElementType();
                return elementType.IsSubclassOf(typeof(UnityObject)) || elementType == typeof(UnityObject);
            }

            return type.IsSubclassOf(typeof(UnityObject)) || type == typeof(UnityObject);
        }

        private PropertyInfo[] GetSerializableProperties(Type type, bool setterRequired = true)
        {
            HashSet<string> exceptProperties;
            m_exceptProperties.TryGetValue(type, out exceptProperties);
            if (exceptProperties == null)
            {
                exceptProperties = new HashSet<string>();
            }

            PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .Where(p => (!setterRequired || p.GetSetMethod() != null) &&
                            p.GetGetMethod() != null &&
                            IsTypeSupported(p.PropertyType) &&
                            !exceptProperties.Contains(p.Name) &&
                            p.GetIndexParameters().Length == 0)
                .Where(p => m_settings.IncludeObsoleteTypes ||
                            !p.IsDefined(typeof(ObsoleteAttribute), true))
                .ToArray();

            return properties;
        }

        private static void BeginPartialClassWithStaticCtor(StringBuilder sb, Type type, string modifier)
        {
            
            sb.AppendFrmt(0, "namespace {0}", type.Namespace);
            sb.AppendLine(0, "{");
            sb.AppendFrmt(1, "public {1} partial class {0}", type.Name, modifier);
            sb.AppendLine(1, "{");
            sb.AppendFrmt(2, "static {0}()", type.Name);
            sb.AppendLine(2, "{");
        }

        private static void EndPartialClassWithStaticCtor(StringBuilder sb)
        {
            sb.AppendLine(2, "}");
            sb.AppendLine(1, "}");
            sb.AppendLine(0, "}");
        }
    }

    public static class StringBuilderExt
    {
        private static string[] m_tabs;

        static StringBuilderExt()
        {
            m_tabs = new string[20];
            for (int i = 0; i < m_tabs.Length; ++i)
            {
                m_tabs[i] = string.Empty;
                for (int j = 0; j < i; ++j)
                {
                    m_tabs[i] += "\t";
                }
            }
        }

        public static StringBuilder AppendLine(this StringBuilder sb, int tabs, string val)
        {
            return sb.Append(m_tabs[tabs]).AppendLine(val);
        }

        public static StringBuilder AppendFrmt(this StringBuilder sb, int tabs, string format, params object[] val)
        {
            return sb.Append(m_tabs[tabs]).AppendFormat(format, val).AppendLine();
        }
    }
}

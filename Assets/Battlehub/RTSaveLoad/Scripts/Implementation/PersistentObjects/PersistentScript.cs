#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityObject = UnityEngine.Object;

namespace Battlehub.RTSaveLoad
{
    public interface IRTSerializable
    {
        void Serialize();

        void Deserialize(Dictionary<long, UnityObject> dependencies);

        void GetDependencies(Dictionary<long, UnityObject> dependencies);

        void FindDependencies<T>(Dictionary<long, T> dependencies, Dictionary<long, T> objects, bool allowNulls);

    }

#if RT_USE_PROTOBUF
    [ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
    #endif
    [Serializable]
    public class PersistentArgumentCache
    {
        public bool m_BoolArgument;
        public float m_FloatArgument;
        public int m_IntArgument;
        public string m_StringArgument;
        public long m_ObjectArgument; //instanceId
        public string m_ObjectArgumentAssemblyTypeName;

        private static bool m_isFieldInfoInitialized;
        private static FieldInfo m_boolArgumentFieldInfo;
        private static FieldInfo m_floatArgumentFieldInfo;
        private static FieldInfo m_intArgumentFieldInfo;
        private static FieldInfo m_stringArgumentFieldInfo;
        private static FieldInfo m_objectArgumentFieldInfo;
        private static FieldInfo m_objectArgumentAssemblyTypeNameFieldInfo;

        private static void Initialize(Type type)
        {
            if(m_isFieldInfoInitialized)
            {
                return;
            }

            m_boolArgumentFieldInfo = type.GetField("m_BoolArgument", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if(m_boolArgumentFieldInfo == null)
            {
                throw new NotSupportedException("m_BoolArgument FieldInfo not found.");
            }

            m_floatArgumentFieldInfo = type.GetField("m_FloatArgument", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (m_floatArgumentFieldInfo == null)
            {
                throw new NotSupportedException("m_FloatArgument FieldInfo not found.");
            }

            m_intArgumentFieldInfo = type.GetField("m_IntArgument", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (m_intArgumentFieldInfo == null)
            {
                throw new NotSupportedException("m_IntArgument FieldInfo not found.");
            }

            m_stringArgumentFieldInfo = type.GetField("m_StringArgument", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (m_stringArgumentFieldInfo == null)
            {
                throw new NotSupportedException("m_StringArgument FieldInfo not found.");
            }

            m_objectArgumentFieldInfo = type.GetField("m_ObjectArgument", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (m_objectArgumentFieldInfo == null)
            {
                throw new NotSupportedException("m_ObjectArgument FieldInfo not found.");
            }

            m_objectArgumentAssemblyTypeNameFieldInfo = type.GetField("m_ObjectArgumentAssemblyTypeName", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (m_objectArgumentAssemblyTypeNameFieldInfo == null)
            {
                throw new NotSupportedException("m_ObjectArgumentAssemblyTypeName FieldInfo not found.");
            }

            m_isFieldInfoInitialized = true;
        }

        public void ReadFrom(object obj)
        {
            if(obj == null)
            {
                m_BoolArgument = false;
                m_FloatArgument = 0;
                m_IntArgument = 0;
                m_StringArgument = null;
                m_ObjectArgument = 0;
                m_ObjectArgumentAssemblyTypeName = null;
                return;
            }
            Initialize(obj.GetType());
            m_BoolArgument = (bool)m_boolArgumentFieldInfo.GetValue(obj);
            m_FloatArgument = (float)m_floatArgumentFieldInfo.GetValue(obj);
            m_IntArgument = (int)m_intArgumentFieldInfo.GetValue(obj);
            m_StringArgument = (string)m_stringArgumentFieldInfo.GetValue(obj);
            UnityObject uobjArgument = (UnityObject)m_objectArgumentFieldInfo.GetValue(obj);
            m_ObjectArgument = uobjArgument.GetMappedInstanceID();
            m_ObjectArgumentAssemblyTypeName = (string)m_objectArgumentAssemblyTypeNameFieldInfo.GetValue(obj);
        }

        public void GetDependencies(object obj, Dictionary<long, UnityObject> dependencies)
        {
            if (obj == null)
            {
                return;
            }

            Initialize(obj.GetType());
            UnityObject uobjArgument = (UnityObject)m_objectArgumentFieldInfo.GetValue(obj);
            AddDependency(uobjArgument, dependencies);
        }

        protected void AddDependency(UnityObject obj, Dictionary<long, UnityObject> dependencies)
        {
            if (obj == null)
            {
                return;
            }

            long instanceId = obj.GetMappedInstanceID();
            if (!dependencies.ContainsKey(instanceId))
            {
                dependencies.Add(instanceId, obj);
            }
        }

        public void FindDependencies<T>(Dictionary<long, T> dependencies, Dictionary<long, T> objects, bool allowNulls)
        {
            AddDependency(m_ObjectArgument, dependencies, objects, allowNulls);
        }

        protected void AddDependency<T>(long id, Dictionary<long, T> dependencies, Dictionary<long, T> objects, bool allowNulls)
        {
            T obj = objects.Get(id);
            if (obj != null || allowNulls)
            {
                if (!dependencies.ContainsKey(id))
                {
                    dependencies.Add(id, obj);
                }
            }
        }

        public void WriteTo(object obj, Dictionary<long, UnityObject> objects)
        {
            if (obj == null)
            {
                return;
            }
            Initialize(obj.GetType());
            m_boolArgumentFieldInfo.SetValue(obj, m_BoolArgument);
            m_floatArgumentFieldInfo.SetValue(obj, m_FloatArgument);
            m_intArgumentFieldInfo.SetValue(obj, m_IntArgument);
            m_stringArgumentFieldInfo.SetValue(obj, m_StringArgument);
            m_objectArgumentFieldInfo.SetValue(obj, objects.Get(m_ObjectArgument));
            m_objectArgumentAssemblyTypeNameFieldInfo.SetValue(obj, m_ObjectArgumentAssemblyTypeName);
        }
    }

    #if RT_USE_PROTOBUF
    [ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
    #endif
    [Serializable]
    public class PersistentPersistentCall
    {
        public PersistentArgumentCache m_Arguments;
        public UnityEventCallState m_CallState;
        public string m_MethodName;
        public PersistentListenerMode m_Mode;
        public long m_Target; //instanceId
        public string TypeName;

        private static bool m_isFieldInfoInitialized;
        private static FieldInfo m_argumentsFieldInfo;
        private static FieldInfo m_callStatFieldInfo;
        private static FieldInfo m_methodNameFieldInfo;
        private static FieldInfo m_modeFieldInfo;
        private static FieldInfo m_targetFieldInfo;

        private static void Initialize(Type type)
        {
            if(m_isFieldInfoInitialized)
            {
                return;
            }

            m_argumentsFieldInfo = type.GetField("m_Arguments", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if(m_argumentsFieldInfo == null)
            {
                throw new NotSupportedException("m_Arguments FieldInfo not found.");
            }
            m_callStatFieldInfo = type.GetField("m_CallState", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (m_callStatFieldInfo == null)
            {
                throw new NotSupportedException("m_CallState FieldInfo not found.");
            }
            m_methodNameFieldInfo = type.GetField("m_MethodName", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (m_methodNameFieldInfo == null)
            {
                throw new NotSupportedException("m_MethodName FieldInfo not found.");
            }
            m_modeFieldInfo = type.GetField("m_Mode", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (m_modeFieldInfo == null)
            {
                throw new NotSupportedException("m_Mode FieldInfo not found.");
            }
            m_targetFieldInfo = type.GetField("m_Target", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (m_targetFieldInfo == null)
            {
                throw new NotSupportedException("m_Target FieldInfo not found.");
            }

            m_isFieldInfoInitialized = true;
        }

        public void ReadFrom(object obj)
        {
            if(obj == null)
            {
                m_Arguments = default(PersistentArgumentCache);
                m_CallState = default(UnityEventCallState);
                m_MethodName = null;
                m_Mode = default(PersistentListenerMode);
                m_Target = 0;
                return;
            }

            Initialize(obj.GetType());
            m_Arguments = new PersistentArgumentCache();
            m_Arguments.ReadFrom(m_argumentsFieldInfo.GetValue(obj));
            m_CallState = (UnityEventCallState)m_callStatFieldInfo.GetValue(obj);
            m_MethodName = (string)m_methodNameFieldInfo.GetValue(obj);
            m_Mode = (PersistentListenerMode)m_modeFieldInfo.GetValue(obj);
            UnityObject target = (UnityObject)m_targetFieldInfo.GetValue(obj);
            m_Target = target.GetMappedInstanceID();
        }


        public void GetDependencies(object obj, Dictionary<long, UnityObject> dependencies)
        {
            if (obj == null)
            {
                return;
            }

            Initialize(obj.GetType());

            PersistentArgumentCache args = new PersistentArgumentCache();
            args.GetDependencies(m_argumentsFieldInfo.GetValue(obj), dependencies);
            
            UnityObject target = (UnityObject)m_targetFieldInfo.GetValue(obj);
            AddDependency(target, dependencies);
        }

        protected void AddDependency(UnityObject obj, Dictionary<long, UnityObject> dependencies)
        {
            if (obj == null)
            {
                return;
            }

            long instanceId = obj.GetMappedInstanceID();
            if (!dependencies.ContainsKey(instanceId))
            {
                dependencies.Add(instanceId, obj);
            }
        }

        public void FindDependencies<T>(Dictionary<long, T> dependencies, Dictionary<long, T> objects, bool allowNulls)
        {           
            if (m_Arguments != null)
            {
                m_Arguments.FindDependencies(dependencies, objects, allowNulls);    
            }

            AddDependency(m_Target, dependencies, objects, allowNulls);
        }

        protected void AddDependency<T>(long id, Dictionary<long, T> dependencies, Dictionary<long, T> objects, bool allowNulls)
        {
            T obj = objects.Get(id);
            if (obj != null || allowNulls)
            {
                if (!dependencies.ContainsKey(id))
                {
                    dependencies.Add(id, obj);
                }
            }
        }

        public void WriteTo(object obj, Dictionary<long, UnityObject> objects)
        {
            if(obj == null)
            {
                return;
            }
            Initialize(obj.GetType());

            TypeName = obj.GetType().AssemblyQualifiedName;

            if (m_Arguments != null)
            {
                object arguments = Activator.CreateInstance(m_argumentsFieldInfo.FieldType);
                m_Arguments.WriteTo(arguments, objects);
                m_argumentsFieldInfo.SetValue(obj, arguments);
            }

            m_callStatFieldInfo.SetValue(obj, m_CallState);
            m_methodNameFieldInfo.SetValue(obj, m_MethodName);
            m_modeFieldInfo.SetValue(obj, m_Mode);
            m_targetFieldInfo.SetValue(obj, objects.Get(m_Target));
        }
    }



    #if RT_USE_PROTOBUF
    [ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
    #endif
    [Serializable]
    public class PersistentUnityEventBase
    {
        private static FieldInfo m_persistentCallGroupInfo;
        private static FieldInfo m_callsInfo;
        private static Type m_callType;

        public PersistentPersistentCall[] m_calls;

        static PersistentUnityEventBase()
        {
            m_persistentCallGroupInfo = typeof(UnityEventBase).GetField("m_PersistentCalls", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            if (m_persistentCallGroupInfo == null)
            {
                throw new NotSupportedException("m_PersistentCalls FieldInfo not found.");
            }

            Type persistentCallsType = m_persistentCallGroupInfo.FieldType;
            m_callsInfo = persistentCallsType.GetField("m_Calls", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (m_callsInfo == null)
            {
                throw new NotSupportedException("m_Calls FieldInfo not found. ");
            }

            Type callsType = m_callsInfo.FieldType;
            if (!callsType.IsGenericType() || callsType.GetGenericTypeDefinition() != typeof(List<>))
            {
                throw new NotSupportedException("m_callsInfo.FieldType is not a generic List<>");
            }

            m_callType = callsType.GetGenericArguments()[0];
        }

        public void ReadFrom(UnityEventBase obj)
        {
            if (obj == null)
            {
                return;
            }

            object persistentCalls = m_persistentCallGroupInfo.GetValue(obj);
            if (persistentCalls == null)
            {
                return;
            }

            object calls = m_callsInfo.GetValue(persistentCalls);
            if (calls == null)
            {
                return;
            }

            IList list = (IList)calls;
            m_calls = new PersistentPersistentCall[list.Count];
            for (int i = 0; i < list.Count; ++i)
            {
                object call = list[i];
                PersistentPersistentCall persistentCall = new PersistentPersistentCall();
                persistentCall.ReadFrom(call);
                m_calls[i] = persistentCall;
            }
        }

        public void GetDependencies(UnityEventBase obj, Dictionary<long, UnityObject> dependencies)
        {
            if (obj == null)
            {
                return;
            }

            object persistentCalls = m_persistentCallGroupInfo.GetValue(obj);
            if (persistentCalls == null)
            {
                return;
            }

            object calls = m_callsInfo.GetValue(persistentCalls);
            if (calls == null)
            {
                return;
            }

            IList list = (IList)calls;
            for (int i = 0; i < list.Count; ++i)
            {
                object call = list[i];
                PersistentPersistentCall persistentCall = new PersistentPersistentCall();
                persistentCall.GetDependencies(call, dependencies);
            }
        }

        public void FindDependencies<T>(Dictionary<long, T> dependencies, Dictionary<long, T> objects, bool allowNulls)
        {
            if (m_calls == null)
            {
                return;
            }

            for (int i = 0; i < m_calls.Length; ++i)
            {
                PersistentPersistentCall persistentCall = m_calls[i];
                if (persistentCall != null)
                {
                    persistentCall.FindDependencies(dependencies, objects, allowNulls);
                }
            } 
        }

        public void WriteTo(UnityEventBase obj, Dictionary<long, UnityObject> objects)
        {
            if (obj == null)
            {
                return;
            }

            if (m_calls == null)
            {
                return;
            }

            object persistentCalls = Activator.CreateInstance(m_persistentCallGroupInfo.FieldType);
            object calls = Activator.CreateInstance(m_callsInfo.FieldType);
         
            IList list = (IList)calls;
            for(int i = 0; i < m_calls.Length; ++i)
            {
                PersistentPersistentCall persistentCall = m_calls[i];
                if(persistentCall != null)
                {
                    object call = Activator.CreateInstance(m_callType);
                    persistentCall.WriteTo(call, objects);
                    list.Add(call);
                }
                else
                {
                    list.Add(null);
                }
            }
            m_callsInfo.SetValue(persistentCalls, calls);
            m_persistentCallGroupInfo.SetValue(obj, persistentCalls);
        }
    }

    #if RT_USE_PROTOBUF
    [ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
    #endif
    [System.Serializable]
    public class IntArray
    {
        public int[] Array;
    }

    [ProtoContract]
    [ProtoInclude(101, typeof(PrimitiveContract<bool>))]
    [ProtoInclude(102, typeof(PrimitiveContract<char>))]
    [ProtoInclude(103, typeof(PrimitiveContract<byte>))]
    [ProtoInclude(104, typeof(PrimitiveContract<short>))]
    [ProtoInclude(105, typeof(PrimitiveContract<int>))]
    [ProtoInclude(106, typeof(PrimitiveContract<long>))]
    [ProtoInclude(107, typeof(PrimitiveContract<ushort>))]
    [ProtoInclude(108, typeof(PrimitiveContract<uint>))]
    [ProtoInclude(110, typeof(PrimitiveContract<ulong>))]
    [ProtoInclude(111, typeof(PrimitiveContract<string>))]
    [ProtoInclude(112, typeof(PrimitiveContract<float>))]
    [ProtoInclude(113, typeof(PrimitiveContract<double>))]
    [ProtoInclude(114, typeof(PrimitiveContract<decimal>))]
    [ProtoInclude(115, typeof(PrimitiveContract<bool[]>))]
    [ProtoInclude(116, typeof(PrimitiveContract<char[]>))]
    [ProtoInclude(117, typeof(PrimitiveContract<byte[]>))]
    [ProtoInclude(118, typeof(PrimitiveContract<short[]>))]
    [ProtoInclude(119, typeof(PrimitiveContract<int[]>))]
    [ProtoInclude(120, typeof(PrimitiveContract<long[]>))]
    [ProtoInclude(121, typeof(PrimitiveContract<ushort[]>))]
    [ProtoInclude(122, typeof(PrimitiveContract<uint[]>))]
    [ProtoInclude(123, typeof(PrimitiveContract<ulong[]>))]
    [ProtoInclude(124, typeof(PrimitiveContract<string[]>))]
    [ProtoInclude(125, typeof(PrimitiveContract<float[]>))]
    [ProtoInclude(126, typeof(PrimitiveContract<double[]>))]
    [ProtoInclude(127, typeof(PrimitiveContract<decimal[]>))]
    [ProtoInclude(128, typeof(PrimitiveContract<Color>))]
    [ProtoInclude(129, typeof(PrimitiveContract<Color[]>))]
    [ProtoInclude(130, typeof(PrimitiveContract<Vector3>))]
    [ProtoInclude(131, typeof(PrimitiveContract<Vector3[]>))]
    [ProtoInclude(132, typeof(PrimitiveContract<Vector4>))]
    [ProtoInclude(133, typeof(PrimitiveContract<Vector4[]>))]
    public abstract class PrimitiveContract
    {
        public static PrimitiveContract<T> Create<T>(T value)
        {
            return new PrimitiveContract<T>(value);
        }

        public static PrimitiveContract Create(Type type)
        {
            Type d1 = typeof(PrimitiveContract<>);
            Type constructed = d1.MakeGenericType(type);
            return (PrimitiveContract)Activator.CreateInstance(constructed);
        }

        public object ValueBase
        {
            get { return ValueImpl; }
            set { ValueImpl = value; }
        }
        protected abstract object ValueImpl { get; set; }
        protected PrimitiveContract() { }
    }

    [ProtoContract]
    public class PrimitiveContract<T> : PrimitiveContract
    {
        public PrimitiveContract() { }
        public PrimitiveContract(T value) { Value = value; }
        [ProtoMember(1)]
        public T Value { get; set; }
        protected override object ValueImpl
        {
            get { return Value; }
            set { Value = (T)value; }
        }
    }

    [ProtoContract(AsReferenceDefault = true)]
    public class DataContract
    {   
        [ProtoMember(1, DynamicType = true)]
        public object Data
        {
            get;
            set;
        }

        public PrimitiveContract AsPrimitive
        {
            get { return Data as PrimitiveContract; }
        }

        public PersistentUnityEventBase AsUnityEvent
        {
            get { return Data as PersistentUnityEventBase; }
        }

        public PersistentData AsPersistentData
        {
            get { return Data as PersistentData; }
        }

        public DataContract() { }
        
        public DataContract(PersistentData data)
        {
            Data = data;
        }

        public DataContract(PrimitiveContract primitive)
        {
            Data = primitive;
        }

        public DataContract(PersistentUnityEventBase data)
        {
            Data = data;
        }
        
    }

    #if RT_USE_PROTOBUF
    [ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
    #endif
    [Serializable]
    public class PersistentScript : PersistentObjects.PersistentObject
    {
        public PersistentData baseObjectData;
        public Dictionary<string, DataContract> fields = new Dictionary<string, DataContract>();
        public string TypeName;

        public PersistentScript()
        {

        }

        public PersistentScript(PersistentData baseObjData)
        {
            baseObjectData = baseObjData;
        }

     

        public override void ReadFrom(object obj)
        {
            base.ReadFrom(obj);
        
            if (baseObjectData != null)
            {
                baseObjectData.ReadFrom(obj);
            }

            Type type = obj.GetType();
            if (!type.IsScript())
            {
                throw new ArgumentException(string.Format("obj of type {0} is not subclass of {1}", type, typeof(MonoBehaviour)), "obj");
            }

            TypeName = type.AssemblyQualifiedName;

            do
            {
                FieldInfo[] fields = type.GetSerializableFields();
                for (int i = 0; i < fields.Length; ++i)
                {
                    FieldInfo field = fields[i];

                    if (this.fields.ContainsKey(field.Name))
                    {
                        Debug.LogErrorFormat("Fields with same names are not supported. Field name {0}", field.Name);
                        continue;
                    }

                    bool isArray = field.FieldType.IsArray;
                    Type fieldType = field.FieldType;
                    if (isArray)
                    {
                        fieldType = fieldType.GetElementType();
                    }

                    if (fieldType.IsSubclassOf(typeof(UnityObject)) || fieldType == typeof(UnityObject))
                    {
                        if (isArray)
                        {
                            UnityObject[] unityObjects = (UnityObject[])field.GetValue(obj);
                            if(unityObjects != null)
                            {
                                long[] ids = new long[unityObjects.Length];
                                for (int j = 0; j < ids.Length; ++j)
                                {
                                    ids[j] = unityObjects[j].GetMappedInstanceID();
                                }
                                this.fields.Add(field.Name, new DataContract(PrimitiveContract.Create(ids)));
                            }
                            else
                            {
                                this.fields.Add(field.Name, new DataContract(PrimitiveContract.Create(new long[0])));
                            }
                           
                        }
                        else
                        {
                            UnityObject unityObject = (UnityObject)field.GetValue(obj);
                            this.fields.Add(field.Name, new DataContract(PrimitiveContract.Create(unityObject.GetMappedInstanceID())));
                        }
                    }
                    else
                    {
                        if (fieldType.IsSubclassOf(typeof(UnityEventBase)))
                        {
                            UnityEventBase unityEvent = (UnityEventBase)field.GetValue(obj);
                            if (unityEvent != null)
                            {
                                PersistentUnityEventBase persistentUnityEvent = new PersistentUnityEventBase();
                                persistentUnityEvent.ReadFrom(unityEvent);
                                this.fields.Add(field.Name, new DataContract(persistentUnityEvent));
                            }
                        }
                        else
                        {
                            if(!field.FieldType.IsEnum())
                            {
                                object fieldValue = field.GetValue(obj);
                                if (typeof(IEnumerable).IsAssignableFrom(field.FieldType))
                                {
                                    IEnumerable enumerable = (IEnumerable)fieldValue;
                                    foreach (object o in enumerable)
                                    {
                                        if (o is IRTSerializable)
                                        {
                                            IRTSerializable rtSerializable = (IRTSerializable)o;
                                            rtSerializable.Serialize();
                                        }
                                    }
                                }
                                else
                                {
                                    if (fieldValue is IRTSerializable)
                                    {
                                        IRTSerializable rtSerializable = (IRTSerializable)fieldValue;
                                        rtSerializable.Serialize();
                                    }
                                }

                                if (field.FieldType.IsPrimitive() || field.FieldType.IsArray())
                                {
                                    PrimitiveContract primitive = PrimitiveContract.Create(field.FieldType);
                                    primitive.ValueBase = fieldValue;
                                    this.fields.Add(field.Name, new DataContract(primitive));
                                }
                                else
                                {
                                    this.fields.Add(field.Name, new DataContract { Data = fieldValue });
                                }            
                            }
                            else
                            {
                                //Debug.Log("Want to create primitive contract for " + field.FieldType);
                                PrimitiveContract primitive = PrimitiveContract.Create(typeof(uint));
                                primitive.ValueBase = (uint)Convert.ChangeType(field.GetValue(obj), typeof(uint));
                                this.fields.Add(field.Name, new DataContract(primitive));
                            }
                        }
                    }
                }
                
                type = type.BaseType();
            }
            while (type.IsScript());
        }

        protected override void GetDependencies(Dictionary<long, UnityObject> dependencies, object obj)
        {
            base.GetDependencies(dependencies, obj);

            base.ReadFrom(obj); //? 


            if (baseObjectData != null)
            {
                baseObjectData.GetDependencies(obj, dependencies);
            }

            Type type = obj.GetType();
            if (!type.IsScript())
            {
                throw new ArgumentException(string.Format("obj of type {0} is not subclass of {1}", type, typeof(MonoBehaviour)), "obj");
            }

            do
            {
                FieldInfo[] fields = type.GetSerializableFields();
                for (int i = 0; i < fields.Length; ++i)
                {
                    FieldInfo field = fields[i];


                    bool isArray = field.FieldType.IsArray;
                    Type fieldType = field.FieldType;
                    if (isArray)
                    {
                        fieldType = fieldType.GetElementType();
                    }

                    if (fieldType.IsSubclassOf(typeof(UnityObject)) || fieldType == typeof(UnityObject))
                    {
                        if (isArray)
                        {
                            UnityObject[] unityObjects = (UnityObject[])field.GetValue(obj);
                            if (unityObjects != null)
                            {
                                AddDependencies(unityObjects, dependencies);
                            }
                        }
                        else
                        {
                            UnityObject unityObject = (UnityObject)field.GetValue(obj);
                            AddDependency(unityObject, dependencies);
                        }
                    }
                    else
                    {
                        if (fieldType.IsSubclassOf(typeof(UnityEventBase)))
                        {
                            UnityEventBase unityEvent = (UnityEventBase)field.GetValue(obj);
                            if (unityEvent != null)
                            {
                                PersistentUnityEventBase persistentUnityEvent = new PersistentUnityEventBase();
                                persistentUnityEvent.GetDependencies(unityEvent, dependencies);
                            }
                        }
                        else
                        {
                            if (!field.FieldType.IsEnum())
                            {
                                object fieldValue = field.GetValue(obj);
                                if (typeof(IEnumerable).IsAssignableFrom(field.FieldType))
                                {
                                    IEnumerable enumerable = (IEnumerable)fieldValue;
                                    foreach (object o in enumerable)
                                    {
                                        if (o is IRTSerializable)
                                        {
                                            IRTSerializable rtSerializable = (IRTSerializable)o;
                                            rtSerializable.GetDependencies(dependencies);
                                        }
                                    }
                                }
                                else
                                {
                                    if (fieldValue is IRTSerializable)
                                    {
                                        IRTSerializable rtSerializable = (IRTSerializable)fieldValue;
                                        rtSerializable.GetDependencies(dependencies);
                                    }
                                }
                            }
                        }
                    }
                }

                type = type.BaseType();
            }
            while (type.IsScript());
        }

        public override void FindDependencies<T>(Dictionary<long, T> dependencies, Dictionary<long, T> objects, bool allowNulls)
        {
            base.FindDependencies(dependencies, objects, allowNulls);

            if (baseObjectData != null)
            {
                baseObjectData.FindDependencies(dependencies, objects, allowNulls);
            }

            Type type = Type.GetType(TypeName);
            if(type == null)
            {
                Debug.LogWarning(TypeName + " is not found");
                return;
            }

            if (!type.IsScript())
            {
                throw new ArgumentException(string.Format("obj of type {0} is not subclass of {1}", type, typeof(MonoBehaviour)), "obj");
            }

            do
            {
                FieldInfo[] fields = type.GetSerializableFields();
                for (int i = 0; i < fields.Length; ++i)
                {
                    FieldInfo field = fields[i];
                    string name = field.Name;
                    if (!this.fields.ContainsKey(field.Name))
                    {
                        FormerlySerializedAsAttribute formelySerializedAs = field.GetCustomAttributes(typeof(FormerlySerializedAsAttribute), false).FirstOrDefault() as FormerlySerializedAsAttribute;
                        if (formelySerializedAs == null)
                        {
                            continue;
                        }
                        name = formelySerializedAs.oldName;
                        if (!this.fields.ContainsKey(name))
                        {
                            continue;
                        }
                    }

                    bool isArray = field.FieldType.IsArray;
                    Type fieldType = field.FieldType;
                    if (isArray)
                    {
                        fieldType = fieldType.GetElementType();
                    }

                    DataContract value = this.fields[name];
                    if (fieldType.IsSubclassOf(typeof(UnityObject)) || fieldType == typeof(UnityObject))
                    {
                        if (isArray)
                        {
                            if (!(value.AsPrimitive.ValueBase is long[]))
                            {
                                continue;
                            }

                            long[] instanceIds = (long[])value.AsPrimitive.ValueBase;
                            AddDependencies(instanceIds, dependencies, objects, allowNulls);
                        }
                        else
                        {
                            if (!(value.AsPrimitive.ValueBase is long))
                            {
                                continue;
                            }

                            long instanceId = (long)value.AsPrimitive.ValueBase;
                            AddDependency(instanceId, dependencies, objects, allowNulls);
                        }
                    }
                    else
                    {
                        if (value.Data != null)
                        {
                            if (field.FieldType.IsSubclassOf(typeof(UnityEventBase)))
                            {
                                PersistentUnityEventBase persistentUnityEvent = value.AsUnityEvent;
                                persistentUnityEvent.FindDependencies(dependencies, objects, allowNulls);
                            }
                        }
                        else
                        {
                            if (!field.FieldType.IsEnum())
                            {
                                object fieldValue;
                                if (field.FieldType.IsPrimitive() || field.FieldType.IsArray())
                                {
                                    PrimitiveContract primitive = value.AsPrimitive;
                                    if (primitive == null ||
                                        primitive.ValueBase == null && field.FieldType.IsValueType() ||
                                        primitive.ValueBase != null && !field.FieldType.IsAssignableFrom(primitive.ValueBase.GetType()))
                                    {
                                        continue;
                                    }
                                    fieldValue = primitive.ValueBase;
                                }
                                else
                                {
                                    fieldValue = value.Data;
                                }

                                if (fieldValue is IEnumerable)
                                {
                                    IEnumerable enumerable = (IEnumerable)fieldValue;
                                    foreach (object o in enumerable)
                                    {
                                        if (o is IRTSerializable)
                                        {
                                            IRTSerializable rtSerializable = (IRTSerializable)o;
                                            rtSerializable.FindDependencies(dependencies, objects, allowNulls);
                                        }
                                    }
                                }
                                else
                                {
                                    if (fieldValue is IRTSerializable)
                                    {
                                        IRTSerializable rtSerializable = (IRTSerializable)fieldValue;
                                        rtSerializable.FindDependencies(dependencies, objects, allowNulls);
                                    }
                                }
                            }
                        }
                    }
                }
                type = type.BaseType();
            }
            while (type.IsScript());
        }


        public override object WriteTo(object obj, Dictionary<long, UnityObject> objects)
        {
            obj = base.WriteTo(obj, objects);

            if (baseObjectData != null)
            {
                //prevent name to be overriden with name stored m_baseObjectData.name
                PersistentObjects.PersistentObject persistentObj = baseObjectData.AsPersistentObject;
                if(persistentObj != null)
                {
                    persistentObj.name = name; 
                }
                baseObjectData.WriteTo(obj, objects);
            }

            Type type = obj.GetType();
            if (!type.IsScript())
            {
                throw new ArgumentException(string.Format("obj of type {0} is not subclass of {1}", type, typeof(MonoBehaviour)), "obj");
            }

            do
            {
                FieldInfo[] fields = type.GetSerializableFields();
                for (int i = 0; i < fields.Length; ++i)
                {
                    FieldInfo field = fields[i];
                    string name = field.Name;
                    if (!this.fields.ContainsKey(field.Name))
                    {
                        FormerlySerializedAsAttribute formelySerializedAs = field.GetCustomAttributes(typeof(FormerlySerializedAsAttribute), false).FirstOrDefault() as FormerlySerializedAsAttribute;
                        if (formelySerializedAs == null)
                        {
                            continue;
                        }
                        name = formelySerializedAs.oldName;
                        if (!this.fields.ContainsKey(name))
                        {
                            continue;
                        }
                    }

                    bool isArray = field.FieldType.IsArray;
                    Type fieldType = field.FieldType;
                    if (isArray)
                    {
                        fieldType = fieldType.GetElementType();
                    }

                    DataContract value = this.fields[name];
                    if (fieldType.IsSubclassOf(typeof(UnityObject)) || fieldType == typeof(UnityObject))
                    {
                        if (isArray)
                        {
                            if (value.AsPrimitive == null)
                            {
                                continue;
                            }

                            if (!(value.AsPrimitive.ValueBase is long[]))
                            {
                                continue;
                            }

                            long[] instanceIds = (long[])value.AsPrimitive.ValueBase;
                            Array objectsFoundByInstanceId = Array.CreateInstance(fieldType, instanceIds.Length);
                            for (int j = 0; j < instanceIds.Length; ++j)
                            {
                                object o = objects.Get(instanceIds[j]);
                                objectsFoundByInstanceId.SetValue(o, j);
                            }
                            field.SetValue(obj, objectsFoundByInstanceId);
                        }
                        else
                        {
                            if(value.AsPrimitive == null)
                            {
                                continue;
                            }
                            if (!(value.AsPrimitive.ValueBase is long))
                            {
                                continue;
                            }

                            long instanceId = (long)value.AsPrimitive.ValueBase;
                            if (!objects.ContainsKey(instanceId))
                            {
                                continue;
                            }

                            object objectFoundByInstanceId = objects[instanceId];
                            try
                            {
                                field.SetValue(obj, objectFoundByInstanceId);
                            }
                            catch
                            {
                                Debug.LogError(instanceId);
                                throw;
                            }
                        }
                    }
                    else
                    {
                        if (value == null)
                        {
                            if (field.FieldType.IsValueType())
                            {
                                continue;
                            }
                            field.SetValue(obj, value);
                        }
                        else
                        {
                            if (field.FieldType.IsSubclassOf(typeof(UnityEventBase)))
                            {
                                if(value.AsUnityEvent == null)
                                {
                                    continue;
                                }
                                PersistentUnityEventBase persistentUnityEvent = value.AsUnityEvent;
                                UnityEventBase unityEvent = (UnityEventBase)Activator.CreateInstance(field.FieldType);
                                persistentUnityEvent.WriteTo(unityEvent, objects);
                                field.SetValue(obj, unityEvent);
                            }
                            else
                            {
                                if (!field.FieldType.IsEnum())
                                {
                                    object fieldValue;
                                    if(field.FieldType.IsPrimitive() || field.FieldType.IsArray())
                                    {
                                        PrimitiveContract primitive = value.AsPrimitive;
                                        if (primitive == null ||
                                            primitive.ValueBase == null && field.FieldType.IsValueType() ||
                                            primitive.ValueBase != null && !field.FieldType.IsAssignableFrom(primitive.ValueBase.GetType()))
                                        {
                                            continue;
                                        }
                                        fieldValue = primitive.ValueBase;
                                    }
                                    else
                                    {
                                        fieldValue = value.Data;
                                    }

                                    field.SetValue(obj, fieldValue);
                                    if (fieldValue is IEnumerable)
                                    {
                                        IEnumerable enumerable = (IEnumerable)fieldValue;
                                        foreach (object o in enumerable)
                                        {
                                            if (o is IRTSerializable)
                                            {
                                                IRTSerializable rtSerializable = (IRTSerializable)o;
                                                rtSerializable.Deserialize(objects);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (fieldValue is IRTSerializable)
                                        {
                                            IRTSerializable rtSerializable = (IRTSerializable)fieldValue;
                                            rtSerializable.Deserialize(objects);
                                        }
                                    }
                                }
                                else// if (field.FieldType.IsEnum)
                                {
                                    PrimitiveContract primitive = value.AsPrimitive;
                                    if (primitive == null ||
                                        primitive.ValueBase == null && field.FieldType.IsValueType() ||
                                        primitive.ValueBase != null && primitive.ValueBase.GetType() != typeof(uint))
                                    {
                                        continue;
                                    }

                                    var val = Enum.ToObject(field.FieldType, primitive.ValueBase);
                                    field.SetValue(obj, val);
                                }
                            }
                        }
                    }
                }
                type = type.BaseType();
            }
            while (type.IsScript());

            return obj;
        }
    }

    
}

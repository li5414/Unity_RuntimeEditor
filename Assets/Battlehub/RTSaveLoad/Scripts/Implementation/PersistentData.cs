#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif

using Battlehub.RTSaveLoad.PersistentObjects;
using Battlehub.RTCommon;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityObject = UnityEngine.Object;

namespace Battlehub.RTSaveLoad
{
    public static class DictionaryExt
    {
        public static U Get<T, U>(this Dictionary<T, U> dict, T key)
        {
            U val;
            if (dict.TryGetValue(key, out val))
            {
                return val;
            }
            return default(U);
        }
    }

    #if RT_USE_PROTOBUF && !RT_PE_MAINTANANCE
    [ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
    [ProtoInclude(500, typeof(PersistentAnimationCurve))]
    [ProtoInclude(501, typeof(PersistentBurst))]
    [ProtoInclude(502, typeof(PersistentColorBySpeedModule))]
    [ProtoInclude(503, typeof(PersistentColorOverLifetimeModule))]
    [ProtoInclude(504, typeof(PersistentCustomDataModule))]
    [ProtoInclude(505, typeof(PersistentEmitParams))]
    [ProtoInclude(506, typeof(PersistentExternalForcesModule))]
    [ProtoInclude(507, typeof(PersistentForceOverLifetimeModule))]
    [ProtoInclude(508, typeof(PersistentGradient))]
    [ProtoInclude(509, typeof(PersistentInheritVelocityModule))]
    [ProtoInclude(510, typeof(PersistentKeyframe))]
    [ProtoInclude(511, typeof(PersistentLightsModule))]
    [ProtoInclude(512, typeof(PersistentLimitVelocityOverLifetimeModule))]
    [ProtoInclude(513, typeof(PersistentMainModule))]
    [ProtoInclude(514, typeof(PersistentMinMaxCurve))]
    [ProtoInclude(515, typeof(PersistentMinMaxGradient))]
    [ProtoInclude(516, typeof(PersistentNoiseModule))]
    [ProtoInclude(517, typeof(PersistentParticle))]
    [ProtoInclude(518, typeof(PersistentRotationBySpeedModule))]
    [ProtoInclude(519, typeof(PersistentRotationOverLifetimeModule))]
    [ProtoInclude(520, typeof(PersistentShapeModule))]
    [ProtoInclude(521, typeof(PersistentSizeBySpeedModule))]
    [ProtoInclude(522, typeof(PersistentSizeOverLifetimeModule))]
    [ProtoInclude(523, typeof(PersistentSubEmittersModule))]
    [ProtoInclude(524, typeof(PersistentTextureSheetAnimationModule))]
    [ProtoInclude(525, typeof(PersistentTrailModule))]
    [ProtoInclude(526, typeof(PersistentVelocityOverLifetimeModule))]
    [ProtoInclude(527, typeof(PersistentGUIStyle))]
    [ProtoInclude(528, typeof(PersistentGUIStyleState))]
    [ProtoInclude(529, typeof(PersistentCollisionModule))]
    [ProtoInclude(530, typeof(PersistentEmissionModule))]
    [ProtoInclude(531, typeof(PersistentTriggerModule))]
    [ProtoInclude(532, typeof(PersistentObject))]
    [ProtoInclude(533, typeof(PersistentObjects.UI.PersistentNavigation))]
    [ProtoInclude(534, typeof(PersistentObjects.UI.PersistentOptionData))]
    [ProtoInclude(535, typeof(PersistentObjects.UI.PersistentSpriteState))]
    #endif
    [Serializable]
    public abstract partial class PersistentData
    {
        public const int USER_DEFINED_FIELD_TAG = 100000;

        //populated in static constructor of partial class
        protected static readonly Dictionary<Type, Type> m_objToData = new Dictionary<Type, Type>();

        public bool ActiveSelf;

        public long InstanceId;
       
        public PersistentObject AsPersistentObject
        {
            get { return this as PersistentObject; }
        }

        //comment for me: just get instance id for UnityObject field
        public virtual void ReadFrom(object obj)
        {
            UnityObject uObj = obj as UnityObject;
            if (uObj != null)
            {
                InstanceId = uObj.GetMappedInstanceID();
            }
        }

        public void GetDependencies(object obj, Dictionary<long, UnityObject> dependencies)
        {
            GetDependencies(dependencies, obj);
        }

        protected virtual void GetDependencies(Dictionary<long, UnityObject> dependencies, object obj)
        {
           
        }

        protected void AddDependencies(UnityObject[] objs, Dictionary<long, UnityObject> dependencies)
        {
            for (int i = 0; i < objs.Length; ++i)
            {
                UnityObject obj = objs[i];
                AddDependency(obj, dependencies);
            }
        }

        protected void AddDependency(UnityObject obj, Dictionary<long, UnityObject> dependencies)
        {
            if(obj == null)
            {
                return;
            }

            long instanceId = obj.GetMappedInstanceID();
            if (!dependencies.ContainsKey(instanceId))
            {
                dependencies.Add(instanceId, obj);
            }
        }

        protected void GetDependencies<T, V>(T[] dst, V[] src, Dictionary<long, UnityObject> dependencies) where T : PersistentData, new()
        {
            if (src == null)
            {
                return;
            }
            if (dst == null)
            {
                dst = new T[src.Length];
            }
            if (dst.Length != src.Length)
            {
                Array.Resize(ref dst, src.Length);
            }

            for (int i = 0; i < src.Length; ++i)
            {
                GetDependencies(dst[i], src[i], dependencies);
            }
        }

        protected void GetDependencies<T>(T dst, object obj, Dictionary<long, UnityObject> dependencies) where T : PersistentData, new()
        {
            if (obj == null)
            {
                return;
            }

            if(dst == null)
            {
                dst = new T();
            }

            dst.GetDependencies(dependencies, obj);
        }


        public virtual void FindDependencies<T>(Dictionary<long, T> dependencies, Dictionary<long, T> objects, bool allowNulls)
        {      
            
        }

        protected void AddDependencies<T>(long[] ids, Dictionary<long, T> dependencies, Dictionary<long, T> objects, bool allowNulls) 
        {
            T[] resolvedObjs = Resolve<T, T>(ids, objects);
            for(int i = 0; i < ids.Length; ++i)
            {
                T obj = resolvedObjs[i];
                if(obj != null || allowNulls)
                {
                    long id = ids[i];
                    if (!dependencies.ContainsKey(id))
                    {
                        dependencies.Add(id, obj);
                    }
                }
            }
        }

        protected void AddDependency<T>(long id,  Dictionary<long, T> dependencies, Dictionary<long, T> objects, bool allowNulls)
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

        protected void FindDependencies<T,V>(V data, Dictionary<long, T> dependencies, Dictionary<long, T> objects, bool allowNulls) where V : PersistentData
        {
            if(data == null)
            {
                return;
            }

            data.FindDependencies(dependencies, objects, allowNulls);
        }

        protected void FindDependencies<T, V>(V[] data, Dictionary<long, T> dependencies, Dictionary<long, T> objects, bool allowNulls) where V : PersistentData
        {
            if (data == null)
            {
                return;
            }

            for(int i = 0; i < data.Length; ++i)
            {
                FindDependencies(data[i], dependencies, objects, allowNulls);
            }
        }

        public virtual object WriteTo(object obj, Dictionary<long, UnityObject> objects /*needed to resolve references*/)
        {
            if(obj is UnityObject)
            {
                UnityObject uobj = (UnityObject)obj;
                if(uobj == null)
                {
                    return null;
                }
            }

            return obj;
        }

        protected T[] Read<T, V>(T[] dst, V[] src) where T : PersistentData, new()
        {
            if(src == null)
            {
                return null;
            }
            if(dst == null)
            {
                dst = new T[src.Length];
            }
            if(dst.Length != src.Length)
            {
                Array.Resize(ref dst, src.Length);
            }
            for(int i = 0; i < dst.Length; ++i)
            {
                dst[i] = Read(dst[i], src[i]);
            }
            return dst;
        }

        protected T Read<T>(T dst, object src)  where T : PersistentData, new()
        {
            if(src == null)
            {
                return null;
            }

            if(dst == null)
            {
                dst = new T();
            }

            dst.ReadFrom(src);
            return dst;
        }

        protected PersistentUnityEventBase Read(PersistentUnityEventBase dst, object src)
        {
            if (src == null)
            {
                return null;
            }

            if (dst == null)
            {
                dst = new PersistentUnityEventBase();
            }

            dst.ReadFrom((UnityEventBase)src);
            return dst;
        }

        protected T[] Write<T>(T[] dst, PersistentData[] src, Dictionary<long, UnityObject> objects)
        {
            if (src == null)
            {
                return null;
            }
            if (dst == null)
            {
                dst = new T[src.Length];
            }
            if (dst.Length != src.Length)
            {
                Array.Resize(ref dst, src.Length);
            }
            for (int i = 0; i < dst.Length; ++i)
            {
                dst[i] = Write(dst[i], src[i], objects);
            }
            return dst;
        }

        protected T Write<T>(T dst, PersistentUnityEventBase src, Dictionary<long, UnityObject> objects) where T : UnityEventBase
        {
            if (src == null)
            {
                return default(T);
            }

            if (dst == null)
            {
                try
                {
                    dst = Activator.CreateInstance<T>();
                }
                catch (MissingMethodException)
                {
                    Debug.LogWarningFormat("Unable to instantiate object. {0} default constructor missing", typeof(T).FullName);
                }
            }

            src.WriteTo(dst, objects);
            return dst;
        }

        protected T Write<T>(T dst, PersistentData src, Dictionary<long, UnityObject> objects)
        {
            if(src == null)
            {
                return default(T);
            }

            if(dst == null)
            {
                try
                {
                    dst = Activator.CreateInstance<T>();
                }
                catch(MissingMethodException)
                {
                    Debug.LogWarningFormat("Unable to instantiate object. {0} default constructor missing", typeof(T).FullName);
                }
            }

            return (T)src.WriteTo(dst, objects);
        }

        //Do not rename
        protected T2[] Resolve<T2, T1>(long[] ids, Dictionary<long, T1> objects) where T2 : T1
        {
            T2[] result = new T2[ids.Length];
            for(int i = 0; i < ids.Length; ++i)
            {
                result[i] = (T2)objects.Get(ids[i]);
            }
            return result;
        }

        public static bool CanCreate(object obj)
        {
            Type type = obj.GetType();
            if (!m_objToData.ContainsKey(type))
            {
                if (type.IsScript())
                {
                    do
                    {
                        //trying to find base type which has corresponding PersistenData type
                        //it will hold base class data of PersistentScript
                        type = type.BaseType();
                    }
                    while (type != null && !m_objToData.ContainsKey(type));

                    return true;
                }
                return false;
            }

            return m_objToData.ContainsKey(type);
        }

        public static PersistentData Create(object obj)
        {
            Type type = obj.GetType();
            if (!m_objToData.ContainsKey(type))
            {
                if (type.IsScript())
                {
                    do
                    {
                        //trying to find base type which has corresponding PersistenData type
                        //it will hold base class data of PersistentScript
                        type = type.BaseType();
                    }
                    while (type != null && !m_objToData.ContainsKey(type));

                    PersistentData baseObjectData = null;
                    if (type != null)
                    {
                        baseObjectData = (PersistentData)Activator.CreateInstance(m_objToData[type]);
                    }
                    return new PersistentScript(baseObjectData);
                }
                Debug.Log(string.Format("there is no persistent data object for {0}", type));
                return null;
            }

            return (PersistentData)Activator.CreateInstance(m_objToData[type]);
        }

        public static void RestoreDataAndResolveDependencies(PersistentData[] dataObjects, Dictionary<long, UnityObject> objects)
        {
            //3. Create persistent data dictionary (new object id -> Persistent Data)
            Dictionary<UnityObject, PersistentData> persistentData = new Dictionary<UnityObject, PersistentData>();
            for (int i = 0; i < dataObjects.Length; ++i)
            {
                PersistentData data = dataObjects[i];

                UnityObject obj;
                if(objects.TryGetValue(data.InstanceId, out obj))
                {
                    persistentData.Add(obj, data);
                }
                
            }

            //4. Create replacement prefabs and replace PersistentIgnoreObjects
            foreach (KeyValuePair<UnityObject, PersistentData> kvp in persistentData)
            {
                PersistentIgnore persistentIgnore = kvp.Key as PersistentIgnore;
                if (persistentIgnore == null)
                {
                    continue;
                }

                GameObject go = persistentIgnore.gameObject;

                PersistentData goData = persistentData[go];
                PersistentData scriptData = kvp.Value;
                PersistentData transformData = persistentData[go.transform];

                //Recover go data
                goData.WriteTo(go, objects);

                //Recover script data
                scriptData.WriteTo(persistentIgnore, objects);

                //Recover transform data
                transformData.WriteTo(go.transform, objects);

                /*
                if (persistentIgnore.ReplacementPrefab != null)
                {
                    //make sure that Replacement prefab is not active, 
                    //this will prevent Awake and other methods from running too early
                    persistentIgnore.ReplacementPrefab.gameObject.SetActive(false);

                    PersistentIgnore replacementScript = UnityObject.Instantiate(persistentIgnore.ReplacementPrefab);
                    List<GameObject> destroy = new List<GameObject>();
                    //Destroy prefab children according to PersistentIgnore component settings
                    foreach (Transform childTransform in replacementScript.transform)
                    {
                        if (!replacementScript.IsChildOfReplacementPrefab(childTransform))
                        {
                            destroy.Add(childTransform.gameObject);
                        }
                    }
                    for (int i = 0; i < destroy.Count; ++i)
                    {
                        UnityObject.DestroyImmediate(destroy[i]);
                    }

                    //replace gameObject with repacementPrefab in objects dictionary
                    //Thereby all other objects will reference replacementPrefab instad of gameObject
                    objects[goData.InstanceId] = replacementScript.gameObject;
                    objects[scriptData.InstanceId] = replacementScript;
                    objects[transformData.InstanceId] = replacementScript.transform;

                    //insert replacementPrefab to hierarchy
                    replacementScript.transform.SetParent(go.transform.parent);
                    foreach (Transform childTransform in go.transform)
                    {
                        childTransform.SetParent(replacementScript.transform);
                    }

                    UnityObject.Destroy(go); //Destroy gameObject which was replaced by replacement Prefab
                }
                 */
            }

            List<GameObject> goList = new List<GameObject>();
            List<bool> goActivationList = new List<bool>();
            //5. Recover data using scene.Data
            for (int i = 0; i < dataObjects.Length; ++i)
            {
                PersistentData data = dataObjects[i];
                if (!objects.ContainsKey(data.InstanceId))
                {
                    Debug.LogWarningFormat("objects does not have object with instance id {0} however PersistentData of type {1} is present", data.InstanceId, data.GetType());
                    continue;
                }

                UnityObject obj = objects[data.InstanceId];
                data.WriteTo(obj, objects);

                if (obj is GameObject)
                {
                    goList.Add((GameObject)obj);
                    goActivationList.Add(data.ActiveSelf);
                }
            }

            for (int i = 0; i < goList.Count; ++i)
            {
                bool activeSelf = goActivationList[i];
                GameObject go = goList[i];
                go.SetActive(activeSelf);
            }
        }

        public static void CreatePersistentDescriptorsAndData(GameObject[] gameObjects, 
            out PersistentDescriptor[] descriptors,
            out PersistentData[] data/*,
            out Dictionary<long, bool> activeSelf*/)
        {
            List<PersistentData> dataList = new List<PersistentData>();
            List<PersistentDescriptor> descriptorsList = new List<PersistentDescriptor>();
           // activeSelf = new Dictionary<long, bool>();
            for (int i = 0; i < gameObjects.Length; ++i)
            {
                GameObject go = gameObjects[i];
                PersistentDescriptor descriptor = PersistentDescriptor.CreateDescriptor(go, null);
                if (descriptor != null)
                {
                    descriptorsList.Add(descriptor);
                }

                CreatePersistentData(go, dataList);
                //ReadActiveSelfProperty(go, activeSelf);
            }

            descriptors = descriptorsList.ToArray();
            data = dataList.ToArray();
        }

        public static void CreatePersistentDescriptorsAndData(GameObject[] gameObjects,
            out PersistentDescriptor[] descriptors,
            out PersistentData[][] data/*,
            out Dictionary<long, bool> activeSelf*/)
        {
            List<PersistentData[]> dataPerDescriptorList = new List<PersistentData[]>();
            List<PersistentDescriptor> descriptorsList = new List<PersistentDescriptor>();
            //activeSelf = new Dictionary<long, bool>();
            for (int i = 0; i < gameObjects.Length; ++i)
            {
                GameObject go = gameObjects[i];
                PersistentDescriptor descriptor = PersistentDescriptor.CreateDescriptor(go, null);
                if (descriptor != null)
                {
                    descriptorsList.Add(descriptor);
                }

                List<PersistentData> dataList = new List<PersistentData>();
                CreatePersistentData(go, dataList);
                //ReadActiveSelfProperty(go, activeSelf);
                dataPerDescriptorList.Add(dataList.ToArray());
            }

            descriptors = descriptorsList.ToArray();
            data = dataPerDescriptorList.ToArray();
        }

        public static PersistentData[] CreatePersistentData(UnityObject[] objects)
        {
            List<PersistentData> data = new List<PersistentData>();
            for (int i = 0; i < objects.Length; ++i)
            {
                UnityObject obj = objects[i];
                if (obj == null)
                {
                    continue;
                }

                PersistentData persistentData = Create(obj);
                if(persistentData == null)
                {
                    continue;
                }

                persistentData.ReadFrom(obj);
                
                data.Add(persistentData);
            }

            return data.ToArray();
        }


        /// <summary>
        /// Create Persistent Data for all GameObjects and Components, write it to PersistentData list
        /// </summary>
        /// <param name="go">gameObject (initially root)</param>
        /// <param name="data">list which will be populated with PersistentData objects</param>
        private static void CreatePersistentData(GameObject go, List<PersistentData> data)
        {
            PersistentIgnore persistentIgnore = go.GetComponent<PersistentIgnore>();
            if (persistentIgnore != null /* && persistentIgnore.ReplacementPrefab == null*/)
            {
                //Do not save persistent ignore objects without replacement prefab
                return;
            }
            PersistentData goData = Create(go);
            if (goData != null)
            {
                goData.ActiveSelf = go.activeSelf;
                goData.ReadFrom(go);
                data.Add(goData);
            }

            Component[] components;
            if (persistentIgnore == null)
            {
                components = go.GetComponents<Component>().Where(c => c != null && !PersistentDescriptor.IgnoreTypes.Contains(c.GetType())).ToArray();
            }
            else
            {
                //if PersistentIgnore component exists save only Transform and PersistentIgnore components
                components = go.GetComponents<Transform>();
                Array.Resize(ref components, components.Length + 1);
                components[components.Length - 1] = persistentIgnore;
            }

            for (int i = 0; i < components.Length; ++i)
            {
                Component component = components[i];
                PersistentData componentData = PersistentData.Create(component);
                if (componentData != null)
                {
                    componentData.ReadFrom(component);
                    data.Add(componentData);
                }
            }

            Transform transform = go.transform;
            foreach (Transform childTransform in transform)
            {
                //Do not create childDescriptor for replacementPrefab child & for persistentIgnore without ReplacementPrefab
                if (persistentIgnore == null /* || persistentIgnore.ReplacementPrefab != null && !persistentIgnore.IsChildOfReplacementPrefab(childTransform)*/)
                {
                    //only for independent child
                    CreatePersistentData(childTransform.gameObject, data);
                }
            }
        }
        public static void RegisterPersistentType<T, TPersistent>() where TPersistent : PersistentData
        {
            m_objToData.Add(typeof(T), typeof(TPersistent));
        }

    }
}
#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityObject = UnityEngine.Object;
namespace Battlehub.RTSaveLoad
{
    #if RT_USE_PROTOBUF
    [ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
    #endif
    [Serializable]
    public class PersistentDescriptor
    {
        /// <summary>
        /// Add here component types which will be ignored by save and load procedures
        /// </summary>
        public static readonly HashSet<Type> IgnoreTypes = new HashSet<Type>(new Type[] {  /* typeof(Transform) /* for example */ });
        
        /// <summary>
        /// Add here dependencies to figure out which components automatically added
        /// for example ParticleSystemRenderer should be added automatically if ParticleSystem component exists 
        /// </summary>
        public readonly static Dictionary<Type, HashSet<Type>> m_dependencies = new Dictionary<Type, HashSet<Type>>
            {
                //type depends on <- { types }
                { typeof(ParticleSystemRenderer), new HashSet<Type> { typeof(ParticleSystem) } }
            };

        private static Shader m_standard;

        public long InstanceId;
        public string TypeName;
#if RT_USE_PROTOBUF
        [ProtoIgnore]
#endif
        public PersistentDescriptor Parent;
        public PersistentDescriptor[] Children;
        public PersistentDescriptor[] Components;

        public PersistentDescriptor()
        {
            Children = new PersistentDescriptor[0];
            Components = new PersistentDescriptor[0];
        }

        public PersistentDescriptor(UnityObject obj)
        {
            InstanceId = obj.GetMappedInstanceID();
            TypeName = obj.GetType().AssemblyQualifiedName;

            Children = new PersistentDescriptor[0];
            Components = new PersistentDescriptor[0];
        }

        public override string ToString()
        {
            string pathToDesriptor = string.Empty;
            PersistentDescriptor descriptor = this;
            if (descriptor.Parent == null)
            {
                pathToDesriptor += "/";
            }
            else
            {
                while (descriptor.Parent != null)
                {
                    pathToDesriptor += "/" + descriptor.Parent.InstanceId;
                    descriptor = descriptor.Parent;
                }
            }
            return string.Format("Descriptor InstanceId = {0}, Type = {1}, Path = {2}, Children = {3} Components = {4}", InstanceId, TypeName, pathToDesriptor, Children != null ? Children.Length : 0, Components != null ? Components.Length : 0);
        }

        public long[] GetInstanceIds()
        {
            if((Children == null || Children.Length == 0) && (Components == null || Components.Length == 0))
            {
                return new[] { InstanceId };
            }

            List<long> instanceIds = new List<long>();
            GetInstanceIds(this, instanceIds);
            return instanceIds.ToArray();
        }

        private void GetInstanceIds(PersistentDescriptor descriptor, List<long> instanceIds)
        {
            instanceIds.Add(descriptor.InstanceId);
            if (descriptor.Components != null)
            {
                for (int i = 0; i < descriptor.Components.Length; ++i)
                {
                    GetInstanceIds(descriptor.Components[i], instanceIds);
                }
            }

            if(descriptor.Children != null)
            {
                for (int i = 0; i < descriptor.Children.Length; ++i)
                {
                    GetInstanceIds(descriptor.Children[i], instanceIds);
                }
            }
        }

        public void FindReferencedObjects(Dictionary<long, UnityObject> referredObjects, Dictionary<long, UnityObject> allObjects, bool allowNulls)
        {
            FindReferencedObjects(this, referredObjects, allObjects, allowNulls);
        }

        private void FindReferencedObjects(PersistentDescriptor descriptor, Dictionary<long, UnityObject> referencedObjects, Dictionary<long, UnityObject> allObjects, bool allowNulls)
        {
            UnityObject referredObject;
            if(allObjects.TryGetValue(descriptor.InstanceId, out referredObject))
            {
                if(!referencedObjects.ContainsKey(descriptor.InstanceId))
                {
                    referencedObjects.Add(descriptor.InstanceId, referredObject);
                }
            }
            else if(allowNulls)
            {
                if (!referencedObjects.ContainsKey(descriptor.InstanceId))
                {
                    referencedObjects.Add(descriptor.InstanceId, null);
                }
            }

            if(descriptor.Components != null)
            {
                for (int i = 0; i < descriptor.Components.Length; ++i)
                {
                    PersistentDescriptor componentDescriptor = descriptor.Components[i];
                    FindReferencedObjects(componentDescriptor, referencedObjects, allObjects, allowNulls);
                }
            }

            if(descriptor.Children != null)
            {
                for(int i = 0; i < descriptor.Children.Length; ++i)
                {
                    PersistentDescriptor childDescriptor = descriptor.Children[i];
                    FindReferencedObjects(childDescriptor, referencedObjects, allObjects, allowNulls);
                }
            }
        }

        public PersistentDescriptor[] FlattenHierarchy()
        {
            List<PersistentDescriptor> descriptors = new List<PersistentDescriptor>();
            FlattenHierarchy(this, descriptors);
            return descriptors.ToArray();
        }

        private void FlattenHierarchy(PersistentDescriptor descriptor, List<PersistentDescriptor> descriptors)
        {
            descriptors.Add(descriptor);

            if (descriptor.Components != null)
            {
                for (int i = 0; i < descriptor.Components.Length; ++i)
                {
                    descriptors.Add(descriptor.Components[i]);
                }
            }

            if (descriptor.Children != null)
            {
                for (int i = 0; i < descriptor.Children.Length; ++i)
                {
                    FlattenHierarchy(descriptor.Children[i], descriptors);
                }
            }
        }

        public static UnityObject GetOrCreateObject(PersistentDescriptor descriptor, Dictionary<long, UnityObject> dependencies, Dictionary<long, UnityObject> decomposition = null)
        {
            Type type = Type.GetType(descriptor.TypeName);
            if (type == null)
            {
                Debug.LogError("Unable to find System.Type for " + descriptor.TypeName);
                return null;
            }

            if (type == typeof(GameObject))
            {
                GameObject[] createGameObjects = GetOrCreateGameObjects(new[] { descriptor }, dependencies, decomposition);
                return createGameObjects[0];
            }

            UnityObject obj;
            if (!dependencies.TryGetValue(descriptor.InstanceId, out obj))
            {
                obj = CreateInstance(type);
            }

            if (obj == null)
            {
                Debug.LogError("Unable to instantiate object of type " + type.FullName);
                return null;
            }

            if (!dependencies.ContainsKey(descriptor.InstanceId))
            {
                dependencies.Add(descriptor.InstanceId, obj);
            }

            if (decomposition != null)
            {
                decomposition.Add(descriptor.InstanceId, obj);
            }

            return obj;
        }

        private static UnityObject CreateInstance(Type type)
        {
            if (type == typeof(Material))
            {
                if (m_standard == null)
                {
                    m_standard = Shader.Find("Standard");
                }
                Debug.Assert(m_standard != null, "Standard shader is not found");
                Material material = new Material(m_standard);
                return material;
            }
#if !UNITY_WEBGL
            else if (type == typeof(ProceduralMaterial))
            {
                throw new NotSupportedException();
            }
#endif
            else if (type == typeof(Texture2D))
            {
                Texture2D texture = new Texture2D(1, 1, TextureFormat.ARGB32, true);
                return texture;
            }

            UnityObject obj = (UnityObject)Activator.CreateInstance(type);
            return obj;
        }

        public static GameObject[] GetOrCreateGameObjects(PersistentDescriptor[] descriptors, Dictionary<long, UnityObject> dependencies, Dictionary<long, UnityObject> decomposition = null)
        {
            List<GameObject> createGameObjects = new List<GameObject>();

            //2. Create object hierarchy using scene.Descriptors
            for (int i = 0; i < descriptors.Length; ++i)
            {
                PersistentDescriptor descriptor = descriptors[i];
                CreateGameObjectWithComponents(descriptor, createGameObjects, dependencies, decomposition);
            }

            return createGameObjects.ToArray();
        }

        public static PersistentDescriptor[] CreatePersistentDescriptors(UnityObject[] objects)
        {
            List<PersistentDescriptor> descriptors = new List<PersistentDescriptor>();
            for (int i = 0; i < objects.Length; ++i)
            {
                UnityObject obj = objects[i];
                if (obj == null)
                {
                    continue;
                }

                PersistentDescriptor persistentDescriptor = new PersistentDescriptor(obj);
                descriptors.Add(persistentDescriptor);
            }

            return descriptors.ToArray();
        }

        /// <summary>
        /// Create GameObjects hierarchy and Add Components recursively
        /// </summary>
        /// <param name="descriptor">PersistentObject descriptor (initially root descriptor)</param>
        /// <param name="objects">Dictionary instanceId->UnityObject which will be populated with GameObjects and Components</param>
        private static void CreateGameObjectWithComponents(PersistentDescriptor descriptor, List<GameObject> createdGameObjects, Dictionary<long, UnityObject> objects, Dictionary<long, UnityObject> decomposition = null)
        {

            GameObject go;
            if (objects.ContainsKey(descriptor.InstanceId))
            {
                // throw new ArgumentException(string.Format("duplicate object descriptor found in descriptors hierarchy. {0}", descriptor.ToString()), "descriptor");
                //This is the case when object already exists in scene and not destroyed due to PersistentIgnore component
                UnityObject obj = objects[descriptor.InstanceId];
                if (obj != null && !(obj is GameObject))
                {
                    Debug.LogError("Invalid Type " + obj.name + " " + obj.GetType() + " " + obj.GetInstanceID() + " " + descriptor.TypeName);
                }

                go = (GameObject)obj;
            }
            else
            {
                go = new GameObject();
                objects.Add(descriptor.InstanceId, go);
            }

            if (decomposition != null)
            {
                if (!decomposition.ContainsKey(descriptor.InstanceId))
                {
                    decomposition.Add(descriptor.InstanceId, go);
                }
            }

            createdGameObjects.Add(go);
            go.SetActive(false);

            if (descriptor.Parent != null)
            {
                if (!objects.ContainsKey(descriptor.Parent.InstanceId))
                {
                    throw new ArgumentException(string.Format("objects dictionary is supposed to have object with instance id {0} at this stage. Descriptor {1}", descriptor.Parent.InstanceId, descriptor, "descriptor"));
                }

                GameObject parentGo = objects[descriptor.Parent.InstanceId] as GameObject;
                if (parentGo == null)
                {
                    throw new ArgumentException(string.Format("object with instance id {0} should have GameObject type. Descriptor {1}", descriptor.Parent.InstanceId, descriptor, "descriptor"));
                }
                go.transform.SetParent(parentGo.transform, false);
            }

            if (descriptor.Components != null)
            {
                HashSet<Type> requirements = new HashSet<Type>();
                for (int i = 0; i < descriptor.Components.Length; ++i)
                {
                    PersistentDescriptor componentDescriptor = descriptor.Components[i];
                    Type componentType = Type.GetType(componentDescriptor.TypeName);
                    if (componentType == null)
                    {
                        Debug.LogWarningFormat("Unknown type {0} associated with component Descriptor {1}", componentDescriptor.TypeName, componentDescriptor.ToString());
                        continue;
                    }

                    if (!componentType.IsSubclassOf(typeof(Component)))
                    {
                        Debug.LogErrorFormat("{0} is not subclass of {1}", componentType.FullName, typeof(Component).FullName);
                        continue;
                    }

                    UnityObject obj;
                    if (objects.ContainsKey(componentDescriptor.InstanceId))
                    {
                        //throw new ArgumentException(string.Format("duplicate component descriptor found in descriptors hierarchy. {0}", componentDescriptor.ToString()), "descriptor");

                        obj = objects[componentDescriptor.InstanceId];
                        if (obj != null && !(obj is Component))
                        {
                            Debug.LogError("Invalid Type. Component " + obj.name + " " + obj.GetType() + " " + obj.GetInstanceID() + " " + descriptor.TypeName + " " + componentDescriptor.TypeName);
                        }
                    }
                    else
                    {
                        obj = AddComponent(objects, go, requirements, componentDescriptor, componentType);
                    }

                    if (decomposition != null)
                    {
                        if (!decomposition.ContainsKey(componentDescriptor.InstanceId))
                        {
                            decomposition.Add(componentDescriptor.InstanceId, obj);
                        }
                    }
                }
            }

            if (descriptor.Children != null)
            {
                for (int i = 0; i < descriptor.Children.Length; ++i)
                {
                    PersistentDescriptor childDescriptor = descriptor.Children[i];
                    CreateGameObjectWithComponents(childDescriptor, createdGameObjects, objects, decomposition);
                }
            }
        }

        private static UnityObject AddComponent(Dictionary<long, UnityObject> objects, GameObject go, HashSet<Type> requirements, PersistentDescriptor componentDescriptor, Type componentType)
        {
            Component component;
            bool maybeComponentAlreadyAdded =
                requirements.Contains(componentType) ||
                componentType.IsSubclassOf(typeof(Transform)) ||
                componentType == typeof(Transform) ||
                componentType.IsDefined(typeof(DisallowMultipleComponent), true) ||
                m_dependencies.ContainsKey(componentType) && m_dependencies[componentType].Any(d => go.GetComponent(d) != null);

            if (maybeComponentAlreadyAdded)
            {
                component = go.GetComponent(componentType);
                if (component == null)
                {
                    component = go.AddComponent(componentType);
                }
            }
            else
            {
                component = go.AddComponent(componentType);
                if (component == null)
                {
                    component = go.GetComponent(componentType);
                }
            }
            if (component == null)
            {
                Debug.LogErrorFormat("Unable to add or get component of type {0}", componentType);
            }
            else
            {
                object[] requireComponents = component.GetType().GetCustomAttributes(typeof(RequireComponent), true);
                for (int j = 0; j < requireComponents.Length; ++j)
                {
                    RequireComponent requireComponent = requireComponents[j] as RequireComponent;
                    if (requireComponent != null)
                    {
                        if (requireComponent.m_Type0 != null && !requirements.Contains(requireComponent.m_Type0))
                        {
                            requirements.Add(requireComponent.m_Type0);
                        }
                        if (requireComponent.m_Type1 != null && !requirements.Contains(requireComponent.m_Type1))
                        {
                            requirements.Add(requireComponent.m_Type1);
                        }
                        if (requireComponent.m_Type2 != null && !requirements.Contains(requireComponent.m_Type2))
                        {
                            requirements.Add(requireComponent.m_Type2);
                        }
                    }
                }
                objects.Add(componentDescriptor.InstanceId, component);
            }

            return component;
        }

        /// <summary>
        /// Create PersistentDescriptor for gameObject recursive. Set PersistentDescriptor.Parent.
        /// Used is process of creation of PersistentScene object for current scene
        /// </summary>
        /// <param name="go">gameObject (initally root gameObject)</param>
        /// <param name="parentDescriptor">parent descriptor (initially null)</param>
        /// <returns></returns>
        public static PersistentDescriptor CreateDescriptor(GameObject go, PersistentDescriptor parentDescriptor = null)
        {
            PersistentIgnore persistentIgnore = go.GetComponent<PersistentIgnore>();
            if (persistentIgnore != null /*&& persistentIgnore.ReplacementPrefab == null*/)
            {
                //Do not save persistent ignore objects without replacement prefab
                return null;
            }
            PersistentDescriptor descriptor = new PersistentDescriptor(go);
            descriptor.Parent = parentDescriptor;

            Component[] components;
            if (persistentIgnore == null)
            {
                components = go.GetComponents<Component>().Where(c => c != null && !IgnoreTypes.Contains(c.GetType())).ToArray();
            }
            else
            {
                //if PersistentIgnore component exists then save only Transform and PersistentIgnore components
                components = go.GetComponents<Transform>();
                Array.Resize(ref components, components.Length + 1);
                components[components.Length - 1] = persistentIgnore;
            }

            if (components.Length > 0)
            {
                descriptor.Components = new PersistentDescriptor[components.Length];
                for (int i = 0; i < components.Length; ++i)
                {
                    Component component = components[i];

                    PersistentDescriptor componentDescriptor = new PersistentDescriptor(component);
                    componentDescriptor.Parent = descriptor;

                    descriptor.Components[i] = componentDescriptor;
                }
            }


            Transform transform = go.transform;
            if (transform.childCount > 0)
            {
                List<PersistentDescriptor> children = new List<PersistentDescriptor>();

                foreach (Transform child in transform)
                {
                    //Do not create childDescriptor for replacementPrefab child & for persistentIgnore without ReplacementPrefab
                    if (persistentIgnore == null /* || persistentIgnore.ReplacementPrefab != null && !persistentIgnore.IsChildOfReplacementPrefab(child)*/)
                    {
                        //only for independent child
                        PersistentDescriptor childDescriptor = CreateDescriptor(child.gameObject, descriptor);
                        if (childDescriptor != null)
                        {
                            children.Add(childDescriptor);
                        }
                    }
                }

                descriptor.Children = children.ToArray();
            }

            return descriptor;
        }


#if RT_USE_PROTOBUF
        [ProtoAfterDeserialization]
#endif
        public void OnDeserialized()
        {
            if(Components != null)
            {
                for (int i = 0; i < Components.Length; ++i)
                {
                    Components[i].Parent = this;
                }
            }

            if(Children != null)
            {        
                for(int i = 0; i < Children.Length; ++i)
                {
                    Children[i].Parent = this;
                }
            }
        }

    }
}
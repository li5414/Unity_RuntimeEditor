#define RT_USE_DEFAULT_IMPLEMENTATION

using UnityEngine;

using Battlehub.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

using UnityObject = UnityEngine.Object;

namespace Battlehub.RTSaveLoad
{
    
    
    [DisallowMultipleComponent]
    [ExecuteInEditMode]
    public class PersistentIgnore : MonoBehaviour
    {
        #if RT_USE_DEFAULT_IMPLEMENTATION
        //private bool m_isStarted;

        [ReadOnly]
        [SerializeField]
        private string m_guid;

        private static Dictionary<Guid, PersistentIgnore> m_instances = new Dictionary<Guid, PersistentIgnore>();
        #if UNITY_EDITOR
        [UnityEditor.Callbacks.DidReloadScripts]
        public static void ReloadInstances()
        {
            m_instances = Resources.FindObjectsOfTypeAll<PersistentIgnore>().Where(p => !p.gameObject.IsPrefab()).Where(p => !string.IsNullOrEmpty(p.m_guid)).ToDictionary(p => new Guid(p.m_guid));
        }
        #endif

        [ReadOnly]
        [SerializeField]
        private bool m_isRuntime = true;

        /// <summary>
        /// Is persistent ignore created or cloned in runtime. 
        /// This kind of objects will not be saved and will be destroyed during Load operation
        /// </summary>
        public bool IsRuntime
        {
            get { return m_isRuntime; }
        }

        //unity version 5.5.2 replacement prefab is not needed anymore
        //[SerializeField] 
        //private PersistentIgnore m_replacementPrefab; 

        //[SerializeField, UnityEditorOnly]
        //private Transform[] m_replacementPrefabChildren;

        //private HashSet<Transform> m_replacementPrefabChildrenHS;

        //unity version 5.5.2 replacement prefab is not needed anymore
        //public PersistentIgnore ReplacementPrefab
        //{
        //    get { return m_replacementPrefab; }
        //}

        //public bool IsChildOfReplacementPrefab(Transform child)
        //{
        //    if (m_replacementPrefabChildrenHS == null)
        //    {
        //        if (!m_isStarted)
        //        {
        //            m_replacementPrefabChildrenHS = new HashSet<Transform>(m_replacementPrefabChildren);
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }

        //    bool contains = m_replacementPrefabChildrenHS.Contains(child);
        //    return contains;
        //}


        private void Awake()
        {
            if (!Application.isPlaying)
            {
                m_isRuntime = false;
                if (!string.IsNullOrEmpty(m_guid))
                {
                    Guid guid = new Guid(m_guid);
                    if (m_instances.ContainsKey(guid))
                    {
                        if (m_instances[guid] != this)
                        {
                            guid = Guid.NewGuid();
                            m_instances.Add(guid, this);
                            m_guid = guid.ToString();

                            #if UNITY_EDITOR
                            ResourceGroup rg = GetComponent<ResourceGroup>();
                            if (rg != null)
                            {
                                DestroyImmediate(rg);
                                ResourceMapGen.CreateResourceMap(true);
                            }
                            #endif
                        }
                    }
                    else
                    {
                        m_instances.Add(guid, this);
                    }
                }
                else
                {
                    Guid guid = Guid.NewGuid();
                    m_instances.Add(guid, this);
                    m_guid = guid.ToString();
                }
            }
            else
            {
                if (string.IsNullOrEmpty(m_guid))
                {
                    m_isRuntime = true;
                }
                else
                {
                    if(m_instances == null)
                    {
                        m_instances = new Dictionary<Guid, PersistentIgnore>();
                    }

                    Guid guid = new Guid(m_guid);
                    if (m_instances.ContainsKey(guid))
                    {
                        m_isRuntime = true;
                    }
                    else
                    {
                        m_isRuntime = false;
                        m_instances.Add(guid, null);
                    }
                }
            }
        }


        private void Reset()
        {
            if (!Application.isPlaying)
            {
                Guid key = m_instances.Where(kvp => kvp.Value == this).Select(kvp => kvp.Key).FirstOrDefault();
                if(m_instances.ContainsKey(key))
                {
                    m_guid = key.ToString();
                }
                m_isRuntime = false;
            }
        }

        private void Start()
        {
            #if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                if(IdentifiersMap.Instance != null)
                {
                    if (GetComponent<ResourceGroup>() == null)
                    {
                        ResourceMapGen.CreateResourceMap(true);
                    }
                }  
            }
            #endif

            //if (m_replacementPrefabChildren != null && m_replacementPrefabChildren.Length > 0)
            //{
            //    m_replacementPrefabChildrenHS = new HashSet<Transform>(m_replacementPrefabChildren);
            //}
            //m_isStarted = true;
        }

        private void OnDestroy()
        {
            if (!string.IsNullOrEmpty(m_guid))
            {
                m_instances.Remove(new Guid(m_guid));
            }

        }
        #endif
        public GameObject InstantiatePrefab(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            if (!prefab.IsPrefab())
            {
                throw new ArgumentException("is not a prefab", "prefab");
            }

            //PersistentIgnore prefabScript = prefab.GetComponent<PersistentIgnore>();
            GameObject instance = Instantiate(prefab, position, rotation);
            //PersistentIgnore persistentIgnore = instance.GetComponent<PersistentIgnore>();
            //if (persistentIgnore.m_replacementPrefab != null)
            //{
            //    persistentIgnore.m_replacementPrefab = prefabScript;
            //}
            return instance;
        }


    }

    public static class GameObjectExtension
    {
        public static GameObject InstantiatePrefab(this GameObject prefab, Vector3 position, Quaternion rotation)
        {
            if(prefab == null)
            {
                return null;
            }

            PersistentIgnore persistentIgnore = prefab.GetComponent<PersistentIgnore>();
            if (persistentIgnore != null)
            {
                return persistentIgnore.InstantiatePrefab(prefab, position, rotation);
            }
            return UnityObject.Instantiate(prefab, position, rotation);
        }
    }
}


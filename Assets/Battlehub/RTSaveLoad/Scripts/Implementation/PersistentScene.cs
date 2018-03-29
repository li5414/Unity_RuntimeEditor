#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif

using System;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityObject = UnityEngine.Object;

namespace Battlehub.RTSaveLoad
{
#if RT_USE_PROTOBUF
    [ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
#endif
    [Serializable]
    public class PersistentScene
    {
        /// <summary>
        /// hierarchy stored in this array
        /// </summary>
        public PersistentDescriptor[] Descriptors;

        /// <summary>
        /// data for each game object and component stored in this array
        /// </summary>
        public PersistentData[] Data;

        /// <summary>
        /// activeSelf flag of each game object 
        /// </summary>
        //public Dictionary<long, bool> ActiveSelf;
        
        public PersistentScene()
        {
            Descriptors = new PersistentDescriptor[0];
            Data = new PersistentData[0];
            //ActiveSelf = new Dictionary<long, bool>();
        }

        /// <summary>
        /// Create GameObjects with Components using data from PersistentScene object
        /// </summary>
        /// <param name="scene">PersistentScene</param>
        public static void InstantiateGameObjects(PersistentScene scene)
        {
            if (IdentifiersMap.Instance == null)
            {
                Debug.LogError("Create Runtime Resource Map");
                return;
            }

            DestroyGameObjects();
            if (scene.Data == null && scene.Descriptors == null)
            {
                return;
            }

            if (scene.Data == null && scene.Descriptors != null || scene.Data != null && scene.Descriptors == null)
            {
                throw new ArgumentException("data is corrupted", "scene");
            }

            if (scene.Descriptors.Length == 0)
            {
                return;
            }


            bool includeDynamicResources = true;
            //1. Find prefabs and other resources;
            Dictionary<long, UnityObject> resources = IdentifiersMap.FindResources(includeDynamicResources);
            PersistentDescriptor.GetOrCreateGameObjects(scene.Descriptors, resources/*, scene.ActiveSelf*/);
            PersistentData.RestoreDataAndResolveDependencies(scene.Data, resources);

        }

        /// <summary>
        /// Destroy all game objects in active scene (except PersistentIgnore according to following rules:
        /// 1) If PersistentIgnore was created in runtime it will be destroyed
        /// 2) If PersistentIgnore has ReplacementPrefab it will not be destroyed. But all it's children will be destroyed;
        /// 3) If PersistentIgnore has no ReplacementPrefab it will not be destroyed. It's children will not be destoryed;
        /// </summary>
        private static void DestroyGameObjects()
        {
            GameObject[] gameObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
            for(int i = 0; i < gameObjects.Length; ++i)
            {
                GameObject go = gameObjects[i];
                if(go != null)
                {
                    if (DestroyGameObjects(go.transform, false))
                    {
                        UnityObject.DestroyImmediate(go);
                    }
                }
                
            }
        }

        private static bool DestroyGameObjects(Transform t, bool forceDestroy)
        {
            //We going to destroy all gameObjects except PersistentIgnore
            bool destroy = true; //destroy by default
            if(!forceDestroy) //if not forced to destroy
            {
                PersistentIgnore ignore = t.GetComponent<PersistentIgnore>();
                if (ignore) //check if this is PersistentIgnore 
                {
                    if(ignore.IsRuntime)
                    {
                        //if PersistentIgnore created in runtime (not in UnityEditor) then destroy it
                        return true; 
                    }

                    return false;
                    /*
                    if (ignore.ReplacementPrefab == null)
                    {
                        //if there is no replacement prefab leave gameobject subtree as is and do not destroy nothing
                        return false;
                    }

                    //if ReplacementPrefab field set do not destroy GameObject
                    destroy = false;
                    //but force destory all it's children
                    forceDestroy = true; 
                    */
                }
            }

            List<Transform> children = new List<Transform>();
            foreach(Transform child in t)
            {
                children.Add(child);   
            }

            for (int i = 0; i < children.Count; ++i)
            {
                Transform child = children[i];
                if (DestroyGameObjects(child, forceDestroy))
                {
                    UnityObject.DestroyImmediate(child.gameObject); 
                }
                else
                {
                    //PersistentIgnore objects became roots to prevent destruction
                    child.SetParent(null, true);
                }
            }

            return destroy;
        }

        /// <summary>
        /// Create PersistentScene object for active scene
        /// </summary>
        /// <returns>PersistentScene object</returns>
        public static PersistentScene CreatePersistentScene(params Type[] ignoreTypes)
        {
            if (IdentifiersMap.Instance == null)
            {
                Debug.LogError("Create Runtime Resource Map");
                return null;
            }
        
            GameObject[] gameObjects = SceneManager.GetActiveScene().GetRootGameObjects().OrderBy(g => g.transform.GetSiblingIndex()).ToArray();
            PersistentScene persistentScene = new PersistentScene();
            PersistentData.CreatePersistentDescriptorsAndData(gameObjects, out persistentScene.Descriptors, out persistentScene.Data /*, out persistentScene.ActiveSelf*/);
            
            return persistentScene;
        }


       
    }
}

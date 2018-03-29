
using UnityEngine;
using Battlehub.RTCommon;
using System.Collections.Generic;
using UnityObject = UnityEngine.Object;
using System.IO;
using System;


namespace Battlehub.RTSaveLoad
{
    public class AddResourcesTest : MonoBehaviour
    {

        public KeyCode AddInstantiatedObjectKey = KeyCode.Keypad1;
        public KeyCode AddPrefabKey = KeyCode.Keypad2;
        public KeyCode AddTextureKey = KeyCode.Keypad3;
        public KeyCode AddWithDependenciesKey = KeyCode.Keypad4;
        public KeyCode AddAssetBundleKey = KeyCode.Keypad5;
        public KeyCode AddAssetBundleKey2 = KeyCode.Keypad6;

        public GameObject Prefab;
        public string ImagePath = "test.png";

        private IProjectManager m_projectManager;

        private void Start()
        {
            m_projectManager = Dependencies.ProjectManager;
        }

        private void Update()
        {

            if (InputController.GetKeyDown(AddAssetBundleKey2))
            {
                ProjectItem rootFolder = m_projectManager.Project;
                m_projectManager.AddBundledResources(rootFolder, "bundledemo",
                    (obj, assetName) =>
                    {
                        return true;
                    },
                    addedItems =>
                    {
                        for (int i = 0; i < addedItems.Length; ++i)
                        {
                            Debug.Log(addedItems[i].ToString() + " added");
                        }
                    });
            }


            if (InputController.GetKeyDown(AddAssetBundleKey))
            {
                ProjectItem rootFolder = m_projectManager.Project;
                m_projectManager.AddBundledResource(rootFolder, "bundledemo", "monkey", addedItems =>
                {
                    for (int i = 0; i < addedItems.Length; ++i)
                    {
                        Debug.Log(addedItems[i].ToString() + " added");
                    }
                });
            }

            if (InputController.GetKeyDown(AddWithDependenciesKey))
            {
                ProjectItem rootFolder = m_projectManager.Project;
                List<UnityObject> objects = new List<UnityObject>();

                Material material = new Material(Shader.Find("Standard"));
                material.color = Color.yellow;

                Mesh mesh = RuntimeGraphics.CreateCubeMesh(Color.white, Vector3.zero, 1);
                mesh.name = "TestMesh";

                GameObject go = new GameObject();
                MeshRenderer renderer = go.AddComponent<MeshRenderer>();
                MeshFilter meshFilter = go.AddComponent<MeshFilter>();

                go.name = "TestGO";
                renderer.sharedMaterial = material;
                meshFilter.sharedMesh = mesh;

                //objects.Add(material);
                //objects.Add(mesh);
                objects.Add(go);

                bool includingDependencies = true;
                Func<UnityObject, bool> filter = o =>
                {
                    if (o is Shader)
                    {
                        return false;
                    }
                    return true;
                };

                m_projectManager.AddDynamicResources(rootFolder, objects.ToArray(), includingDependencies, filter, addedItems =>
                {
                    for (int i = 0; i < addedItems.Length; ++i)
                    {
                        Debug.Log(addedItems[i].ToString() + " added");
                    }

                    for (int i = 0; i < objects.Count; ++i)
                    {
                        Destroy(objects[i]);
                    }
                });
            }


            if (InputController.GetKeyDown(AddInstantiatedObjectKey))
            {
                ProjectItem rootFolder = m_projectManager.Project;
                List<UnityObject> objects = new List<UnityObject>();

                Material material = new Material(Shader.Find("Standard"));
                material.color = Color.yellow;

                Mesh mesh = RuntimeGraphics.CreateCubeMesh(Color.white, Vector3.zero, 1);
                mesh.name = "TestMesh";

                GameObject go = new GameObject();
                MeshRenderer renderer = go.AddComponent<MeshRenderer>();
                MeshFilter filter = go.AddComponent<MeshFilter>();

                go.name = "TestGO";
                renderer.sharedMaterial = material;
                filter.sharedMesh = mesh;

                objects.Add(material);
                objects.Add(mesh);
                objects.Add(go);

                m_projectManager.AddDynamicResources(rootFolder, objects.ToArray(), addedItems =>
                {
                    for (int i = 0; i < addedItems.Length; ++i)
                    {
                        Debug.Log(addedItems[i].ToString() + " added");
                    }

                    for (int i = 0; i < objects.Count; ++i)
                    {
                        Destroy(objects[i]);
                    }
                });
            }

            if (InputController.GetKeyDown(AddPrefabKey))
            {
                ProjectItem rootFolder = m_projectManager.Project;
                List<UnityObject> objects = new List<UnityObject>();

                if (Prefab != null)
                {
                    objects.Add(Prefab);
                }

                bool includingDependencies = true;
                Func<UnityObject, bool> filter = o =>
                {
                    if (o is Shader)
                    {
                        return false;
                    }
                    return true;
                };

                m_projectManager.AddDynamicResources(rootFolder, objects.ToArray(), includingDependencies, filter, addedItems =>
                {
                    for (int i = 0; i < addedItems.Length; ++i)
                    {
                        Debug.Log(addedItems[i].ToString() + " added");
                    }
                });

            }

            if (InputController.GetKeyDown(AddTextureKey))
            {
                ProjectItem rootFolder = m_projectManager.Project;
                List<UnityObject> objects = new List<UnityObject>();

                string path = Application.streamingAssetsPath + "/" + ImagePath;
                Texture2D texture2D = LoadPNG(path);
                if (texture2D == null)
                {
                    Debug.LogErrorFormat("File {0} not found", path);
                    return;
                }

                texture2D.name = "TestTexture";
                objects.Add(texture2D);

                m_projectManager.AddDynamicResources(rootFolder, objects.ToArray(), addedItems =>
                {
                    for (int i = 0; i < addedItems.Length; ++i)
                    {
                        Debug.Log(addedItems[i].ToString() + " added");
                    }
                });
            }
        }

        public static Texture2D LoadPNG(string filePath)
        {
            Texture2D tex = null;
            byte[] fileData;

            if (File.Exists(filePath))
            {
                fileData = File.ReadAllBytes(filePath);
                tex = new Texture2D(1, 1, TextureFormat.ARGB32, true);
                tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
            }
            return tex;
        }
    }


}

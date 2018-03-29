using Battlehub.RTSaveLoad.PersistentObjects;
using ProtoBuf;
using ProtoBuf.Meta;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Battlehub.RTSaveLoad
{
    public static class RuntimeSaveLoadMenu
    {
        private const string TypeModelPath = @"/" + BHPath.Root + "/Deps/";
        private const string TempProjectName = "8877b50d-7d6a-4bf0-bed3-41d253065edd";

        [MenuItem("Tools/Runtime SaveLoad/Build All")]
        public static void CreateAll()
        {
            CreateResourceMap();
            CreateTypeModel();
        }

        [MenuItem("Tools/Runtime SaveLoad/Build Type Model")]
        public static void CreateTypeModel()
        {
            RuntimeTypeModel model = new TypeModelCreator().Create();
            string dllName = "RTTypeModel.dll";

            model.Compile(new RuntimeTypeModel.CompilerOptions() { OutputPath = dllName, TypeName = "RTTypeModel" });

            string srcPath = Application.dataPath.Remove(Application.dataPath.LastIndexOf("Assets")) + dllName;
            string dstPath = Application.dataPath + TypeModelPath + dllName;
            Debug.LogFormat("Done! Moved {0} to {1} ...", srcPath, dstPath);
            File.Delete(dstPath);
            File.Move(srcPath, dstPath);

            AssetDatabase.Refresh();
        }

        [MenuItem("Tools/Runtime SaveLoad/Build Resource Map")]
        public static void CreateResourceMap()
        {
            ResourceMapGen.CreateResourceMap(true);
        }

        [MenuItem("Tools/Runtime SaveLoad/Load Project", validate = true)]
        public static bool CanLoadProject()
        {
            IProjectManager pm = Dependencies.ProjectManager;
            return pm != null;
        }

        [MenuItem("Tools/Runtime SaveLoad/Load Project")]
        public static void LoadProject()
        {
            string path = EditorUtility.OpenFilePanel("Open Project", Application.persistentDataPath, "rtpmeta");
            if (path.Length != 0)
            {
                IProjectManager pm = Dependencies.ProjectManager;
                if(pm == null)
                {
                    Debug.LogError("Project Manager not found");
                    return;
                }

                string projectFolder = Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path));
                if (!Directory.Exists(projectFolder))
                {
                    Debug.LogError(projectFolder + " does not exists");
                    return;
                }


                string tempPath = Path.Combine(Application.persistentDataPath, TempProjectName);
                if (Directory.Exists(tempPath))
                {
                    Directory.Delete(tempPath, true);
                }
                if(File.Exists(tempPath + ".rtpmeta"))
                {
                    File.Delete(tempPath + ".rtpmeta");
                }
                if (File.Exists(tempPath + ".rtmeta"))
                {
                    File.Delete(tempPath + ".rtmeta");
                }
                Directory.CreateDirectory(tempPath);
                Dependencies.Storage.CopyFile(path, tempPath + ".rtpmeta", copyMetaCompleted =>
                {
                    Dependencies.Storage.CopyFolder(projectFolder, tempPath, copyCompleted =>
                    {
                        pm.LoadProject(tempPath, projectLoaded =>
                        {
                            Debug.Log("ProjectLoaded");
                            File.Delete(tempPath + ".rtpmeta");
                            File.Delete(tempPath + ".rtmeta");
                            Directory.Delete(tempPath, true);
                        });
                    });
                });
            }
        }

        [MenuItem("Tools/Runtime SaveLoad/Load Scene")]
        public static void LoadScene()
        {
            string path = EditorUtility.OpenFilePanel("Open Scene", Application.persistentDataPath, "rtsc");
            if (path.Length != 0)
            {
                IProject project = Dependencies.Project;
                ISerializer serializer = Dependencies.Serializer;

                project.Load(new[] { path }, loadDataCompleted =>
                {
                    ProjectItem scene = loadDataCompleted.Data[0];

                    PersistentScene persistentScene = serializer.Deserialize<PersistentScene>(scene.Internal_Data.RawData);
                    PersistentScene.InstantiateGameObjects(persistentScene);

                    project.UnloadData(scene);

                    ExtraSceneData extraData = Object.FindObjectOfType<ExtraSceneData>();
                    Object.DestroyImmediate(extraData.gameObject);
                });
            }
        }


        // [MenuItem("Tools/Runtime SaveLoad/PersistentData DerivedTypes")]
        public static void PrintPersistentDataDerivedTypes()
        {
            var types = typeof(PersistentData).Assembly.GetTypes();
            types = types.Where(t => t.BaseType == typeof(PersistentData)).ToArray();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < types.Length; ++i)
            {
                sb.AppendFrmt(0, "[ProtoInclude({0}, typeof({1}))]", i + 500, types[i].Name);
            }

            Debug.Log(sb.ToString());

        }


        // [MenuItem("Tools/Runtime SaveLoad/-- Create Persistent Objects")]
        public static void CreatePersistentObjects()
        {
            if (!EditorUtility.DisplayDialog("Create Persistent Objects", "Are you sure you want to create Persistent Objects?", "Yes", "No"))
            {
                return;
            }

            const string path = @"/" + BHPath.Root + @"/RTSaveLoad/PersistentObjects";
            Assembly assembly = typeof(GameObject).Assembly;
            Assembly uiAssembly = typeof(Selectable).Assembly;
            Assembly[] asms = { assembly, uiAssembly };

            CodeGen codeGen = new CodeGen();
            codeGen.GeneratePersistentClasses(path, asms);
            codeGen.GenerateParticleSystemPersistentModules(path + "/ParticleSystem");

            asms = new[] { typeof(GameObject).Assembly, typeof(SerializationSurrogates).Assembly };
            codeGen.GenerateSerializationSurrogates(path, asms);

            AssetDatabase.ImportAsset("Assets" + path, ImportAssetOptions.ImportRecursive);
        }


        // [MenuItem("Tools/Runtime SaveLoad/Resource Map Update Mode/Auto", validate =true)]
        public static bool CanAutoCreateResourceMap()
        {
            return !ResourceMapGen.Automatic;
        }

        // [MenuItem("Tools/Runtime SaveLoad/Resource Map Update Mode/Auto")]
        public static void AutoCreateResourceMap()
        {
            ResourceMapGen.Automatic = true;
            Debug.Log("Switched to Auto");
        }

        //[MenuItem("Tools/Runtime SaveLoad/Resource Map Update Mode/Manual", validate = true)]
        public static bool CanManualCreateResourceMap()
        {
            return ResourceMapGen.Automatic;
        }


        //[MenuItem("Tools/Runtime SaveLoad/Resource Map Update Mode/Manual")]
        public static void ManualCreateResourceMap()
        {
            ResourceMapGen.Automatic = false;
            Debug.Log("Switched to Manual");
        }
    }
  
}


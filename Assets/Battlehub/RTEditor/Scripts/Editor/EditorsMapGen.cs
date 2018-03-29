using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battlehub.RTEditor
{
    public static class EditorsMapGen
    {
        public const string ScriptsPath = @"/" + BHPath.Root + @"/RTEditor/Scripts/Editors";
        public const string PrefabsPath = @"/" + BHPath.Root + @"/RTEditor/Prefabs/Editors";
        public const string ScriptName = "EditorsMapAuto.cs";

        public static void Generate(EditorDescriptor[] descriptors, MaterialEditorDescriptor[] materialDescriptors)
        {
            Dictionary<GameObject, int> editors = CreateComponentEditorMap(descriptors);

            if (!Directory.Exists(Application.dataPath + PrefabsPath + "/Resources"))
            {
                AssetDatabase.CreateFolder("Assets" + PrefabsPath, "Resources");
            }

            GameObject go = new GameObject();
            EditorsMapStorage editorsMap = go.AddComponent<EditorsMapStorage>();
            editorsMap.Editors = editors.OrderBy(k => k.Value).Select(k => k.Key).ToArray();

            MaterialEditorDescriptor defaultDescriptor = materialDescriptors.Where(d => d.Shader == null).FirstOrDefault();
            if(defaultDescriptor != null)
            {
                editorsMap.DefaultMaterialEditor = defaultDescriptor.Editor;
                editorsMap.IsDefaultMaterialEditorEnabled = defaultDescriptor.Enabled;
            }
            else
            {
                editorsMap.DefaultMaterialEditor = null;
                editorsMap.IsDefaultMaterialEditorEnabled = false;
            }

            materialDescriptors = materialDescriptors.Where(d => d != defaultDescriptor).ToArray();
            List<Shader> shaders = new List<Shader>();
            List<GameObject> materialEditors = new List<GameObject>();
            List<bool> materialEditorsEnabled = new List<bool>();
            for(int i = 0; i < materialDescriptors.Length; ++i)
            {
                MaterialEditorDescriptor descriptor = materialDescriptors[i];
                if(descriptor.Editor != null)
                {
                    shaders.Add(descriptor.Shader);
                    materialEditors.Add(descriptor.Editor);
                    materialEditorsEnabled.Add(descriptor.Enabled);
                }
            }

            editorsMap.Shaders = shaders.ToArray();
            editorsMap.MaterialEditors = materialEditors.ToArray();
            editorsMap.IsMaterialEditorEnabled = materialEditorsEnabled.ToArray();

            string path = "Assets" + PrefabsPath + "/Resources/" + EditorsMapStorage.EditorsMapPrefabName + ".prefab";
            PrefabUtility.CreatePrefab(path, go);
            UnityEngine.Object.DestroyImmediate(go);

            AssetDatabase.ImportAsset("Assets" + ScriptsPath, ImportAssetOptions.ImportRecursive);


        }

        private static Dictionary<GameObject, int> CreateComponentEditorMap(EditorDescriptor[] descriptors)
        {
            Type type = typeof(EditorsMap);

            string fullPath = Application.dataPath + ScriptsPath;
            Directory.CreateDirectory(fullPath);

            StringBuilder builder = new StringBuilder();
            builder.AppendLine(string.Format("using {0}; ", typeof(Type).Namespace));
            builder.AppendLine(string.Format("using {0}; ", typeof(MonoBehaviour).Namespace));
            builder.AppendLine(string.Format("using {0}; ", typeof(Dictionary<,>).Namespace));
            builder.AppendLine("namespace " + type.Namespace);
            builder.AppendLine("{");
            builder.AppendLine("\tpublic partial class " + type.Name);
            builder.AppendLine("\t{");
            builder.AppendLine("\t\tprotected static void InitEditorsMap()");
            builder.AppendLine("\t\t{");
            builder.AppendLine("\t\t\tm_map = new Dictionary<Type, EditorDescriptor>");
            builder.AppendLine("\t\t\t{");
            Dictionary<GameObject, int> editors = new Dictionary<GameObject, int>();
            int editorIndex = -1;
            for (int i = 0; i < descriptors.Length; ++i)
            {
                EditorDescriptor descriptor = descriptors[i];
                if (descriptor.Editor == null)
                {
                    continue;
                }

                if (editors.ContainsKey(descriptor.Editor))
                {
                    builder.AppendLine(
                        string.Format(
                            "\t\t\t\t{{ typeof({0}), new EditorDescriptor({1}, {2}, {3}) }},",
                            descriptor.Type.FullName.Replace("`1", "<>"),
                            editors[descriptor.Editor],
                            descriptor.Enabled ? "true" : "false",
                            descriptor.IsPropertyEditor ? "true" : "false"));
                }
                else
                {
                    editorIndex++;
                    editors.Add(descriptor.Editor, editorIndex);

                    builder.AppendLine(
                        string.Format(
                            "\t\t\t\t{{ typeof({0}), new EditorDescriptor({1}, {2}, {3}) }},",
                            descriptor.Type.FullName.Replace("`1", "<>"),
                            editorIndex,
                            descriptor.Enabled ? "true" : "false",
                            descriptor.IsPropertyEditor ? "true" : "false"));
                }

               
            }

            builder.AppendLine("\t\t\t};");
            builder.AppendLine("\t\t}");
            builder.AppendLine("\t}");
            builder.AppendLine("}");

            string content = builder.ToString();
            File.WriteAllText(Path.Combine(fullPath, ScriptName), content);
            return editors;
        }
    }

}

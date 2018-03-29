using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battlehub.RTSaveLoad
{
    public static class SaveLoadConfigGen
    {
        public const string ScriptName = "SaveLoadConfigAuto.cs";
        public const string ScriptsPath = @"/" + BHPath.Root + "/RTSaveLoad/Scripts";

        public static void Generate(Type[] enabledTypes)
        {
            CreateComponentEditorMap(enabledTypes);
        }

        private static void CreateComponentEditorMap(Type[] enabledTypes)
        {
            Type type = typeof(SaveLoadConfig);

            string fullPath = Application.dataPath + ScriptsPath;
            Directory.CreateDirectory(fullPath);

            StringBuilder builder = new StringBuilder();
            builder.AppendLine(string.Format("using {0}; ", typeof(Type).Namespace));
            builder.AppendLine(string.Format("using {0}; ", typeof(MonoBehaviour).Namespace));
            builder.AppendLine(string.Format("using {0}; ", typeof(HashSet<>).Namespace));
            builder.AppendLine("namespace " + type.Namespace);
            builder.AppendLine("{");
            builder.AppendLine("\tpublic partial class " + type.Name);
            builder.AppendLine("\t{");
            builder.AppendLine(string.Format("\t\tstatic {0}()", type.Name));
            builder.AppendLine("\t\t{");
            builder.AppendLine("\t\t\tm_disabledTypes = new Type[]");
            builder.AppendLine("\t\t\t{");
            for (int i = 0; i < enabledTypes.Length; ++i)
            {
                builder.AppendLine(
                    string.Format(
                        "\t\t\t\t typeof({0}),",
                        enabledTypes[i].FullName));
            }

            builder.AppendLine("\t\t\t};");
            builder.AppendLine("\t\t}");
            builder.AppendLine("\t}");
            builder.AppendLine("}");

            string content = builder.ToString();
            File.WriteAllText(Path.Combine(fullPath, ScriptName), content);

            AssetDatabase.ImportAsset("Assets" + ScriptsPath, ImportAssetOptions.ImportRecursive);

        }
    }

}

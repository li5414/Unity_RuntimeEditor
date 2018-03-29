#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace Battlehub.RTSaveLoad
{
    public static class RuntimeShaderInfoGen
    {
        private const string bundleNameDelimiter = ">";

        private static readonly Dictionary<ShaderUtil.ShaderPropertyType, RTShaderPropertyType> m_typeToType = new Dictionary<ShaderUtil.ShaderPropertyType, RTShaderPropertyType>
            { { ShaderUtil.ShaderPropertyType.Color, RTShaderPropertyType.Color },
              { ShaderUtil.ShaderPropertyType.Float, RTShaderPropertyType.Float },
              { ShaderUtil.ShaderPropertyType.Range, RTShaderPropertyType.Range },
              { ShaderUtil.ShaderPropertyType.TexEnv, RTShaderPropertyType.TexEnv },
              { ShaderUtil.ShaderPropertyType.Vector, RTShaderPropertyType.Vector },};


        public static Shader[] Create(string bundleName, string variantName)
        {
            List<Material> materials = new List<Material>();

            string[] assets = AssetDatabase.GetAllAssetPaths();
            foreach (string asset in assets)
            {
                if (!asset.EndsWith(".mat"))
                {
                    continue;
                }
                Material material = AssetDatabase.LoadAssetAtPath<Material>(asset);
                if (material == null || material.shader == null)
                {
                    continue;
                }
                AssetImporter importer = AssetImporter.GetAtPath(asset);
                if (importer.assetBundleName != bundleName || importer.assetBundleVariant != variantName)
                {
                    continue;
                }

                if ((material.hideFlags & HideFlags.DontSaveInBuild) != 0 ||
                   (material.shader.hideFlags & HideFlags.DontSaveInBuild) != 0)
                {
                    continue;
                }
                materials.Add(material);
            }

            foreach (string path in assets)
            {
                AssetImporter importer = null;

                if (PathHelper.IsPathRooted(path))
                {
                    continue;
                }

                foreach (Object sub in AssetDatabase.LoadAllAssetRepresentationsAtPath(path))
                {
                    if (sub == null)
                    {
                        continue;
                    }

                    if (sub is Material)
                    {
                        if (importer == null)
                        {
                            importer = AssetImporter.GetAtPath(path);
                        }

                        if (importer.assetBundleName != bundleName || importer.assetBundleVariant != variantName)
                        {
                            break;
                        }

                        Material material = (Material)sub;
                        if (material.shader == null)
                        {
                            continue;
                        }
                        if ((material.hideFlags & HideFlags.DontSaveInBuild) != 0 ||
                            (material.shader.hideFlags & HideFlags.DontSaveInBuild) != 0)
                        {
                            continue;
                        }
                        materials.Add(material);
                    }
                }
            }

            HashSet<Shader> shaders = new HashSet<Shader>();
            for (int i = 0; i < materials.Count; ++i)
            {
                Material material = materials[i];
                if (material.shader == null)
                {
                    continue;
                }

                if (shaders.Contains(material.shader))
                {
                    continue;
                }

                shaders.Add(material.shader);
                _Create(material.shader, bundleName, variantName);
            }

            return shaders.ToArray();
        }

        public static void SetAssetBundleNameAndVariant(Shader[] shaders, string bundleName, string variantName)
        {
            foreach (Shader shader in shaders)
            {
                _SetAssetBundleNameAndVariant(shader, bundleName, variantName);
            }
        }

        private static void _Create(Shader shader, string bundleName, string variantName)
        {
            if (shader == null)
            {
                throw new System.ArgumentNullException("shader");
            }

            int propertyCount = ShaderUtil.GetPropertyCount(shader);

            RuntimeShaderInfo shaderInfo = new RuntimeShaderInfo();
            shaderInfo.Name = shader.name;
            if (!shader.HasMappedInstanceID())
            {
                //bool create = EditorUtility.DisplayDialog("RuntimeShaderInfo Generator", "Unable to create RuntimeShaderInfo. Please Create or Update ResourceMap", "Create", "Cancel");
                //if (create)
                //{
                //    ResourceMapGen.CreateResourceMap(true);
                //}

                return;
            }

            shaderInfo.InstanceId = shader.GetMappedInstanceID();
            shaderInfo.PropertyCount = propertyCount;
            shaderInfo.PropertyDescriptions = new string[propertyCount];
            shaderInfo.PropertyNames = new string[propertyCount];
            shaderInfo.PropertyRangeLimits = new RuntimeShaderInfo.RangeLimits[propertyCount];
            shaderInfo.PropertyTexDims = new TextureDimension[propertyCount];
            shaderInfo.PropertyTypes = new RTShaderPropertyType[propertyCount];
            shaderInfo.IsHidden = new bool[propertyCount];

            for (int i = 0; i < propertyCount; ++i)
            {
                shaderInfo.PropertyDescriptions[i] = ShaderUtil.GetPropertyDescription(shader, i);
                shaderInfo.PropertyNames[i] = ShaderUtil.GetPropertyName(shader, i);
                shaderInfo.PropertyRangeLimits[i] = new RuntimeShaderInfo.RangeLimits(
                    ShaderUtil.GetRangeLimits(shader, i, 0),
                    ShaderUtil.GetRangeLimits(shader, i, 1),
                    ShaderUtil.GetRangeLimits(shader, i, 2));
                shaderInfo.PropertyTexDims[i] = ShaderUtil.GetTexDim(shader, i);

                RTShaderPropertyType rtType = RTShaderPropertyType.Unknown;
                ShaderUtil.ShaderPropertyType type = ShaderUtil.GetPropertyType(shader, i);
                if (m_typeToType.ContainsKey(type))
                {
                    rtType = m_typeToType[type];
                }

                shaderInfo.PropertyTypes[i] = rtType;
                shaderInfo.IsHidden[i] = ShaderUtil.IsShaderPropertyHidden(shader, i);
            }

            string fullPath = Application.dataPath + RuntimeShaderUtil.GetPath(true);
            Directory.CreateDirectory(fullPath);

            string fileName = RuntimeShaderUtil.GetShaderInfoFileName(shader);

            string path = Path.Combine(fullPath, fileName);

            bool refresh = !File.Exists(path);

            File.WriteAllText(path, JsonUtility.ToJson(shaderInfo));

            if(refresh)
            {
                AssetDatabase.Refresh();
            }
        }

        private static void _SetAssetBundleNameAndVariant(Shader shader, string bundleName, string variantName)
        {
            bool isBundledShader = !string.IsNullOrEmpty(bundleName);
            string fileName = RuntimeShaderUtil.GetShaderInfoFileName(shader);
            if (isBundledShader)
            {
                AssetImporter importer = AssetImporter.GetAtPath("Assets" + RuntimeShaderUtil.GetPath(true) + "/" + fileName);
                importer.SetAssetBundleNameAndVariant(bundleName, variantName);
            }
        }

        public static void RemoveUnused(string bundleName, string variantName /*bool fromResourcesFolder*/)
        {
            string fullPath = Application.dataPath + RuntimeShaderUtil.GetPath(true);
            if (!Directory.Exists(fullPath))
            {
                return;
            }
            string[] files = Directory.GetFiles(fullPath);
            HashSet<long> shadersHs = new HashSet<long>(Resources.FindObjectsOfTypeAll<Shader>().Where(s => s.HasMappedInstanceID()).Select(s => s.GetMappedInstanceID()).ToArray());
            for (int i = 0; i < files.Length; ++i)
            {
                string file = files[i];
                string assetPath = "Assets" + RuntimeShaderUtil.GetPath(true) + "/" + Path.GetFileName(file);
                AssetImporter importer = AssetImporter.GetAtPath(assetPath);
                if(importer == null)
                {
                    continue;
                }
                if(importer.assetBundleName != bundleName || importer.assetBundleVariant != variantName)
                {
                    continue;
                }

                long instanceId = RuntimeShaderUtil.FileNameToInstanceID(file);
                if (!shadersHs.Contains(instanceId))
                {
                    File.Delete(file);
                }
            }
        }
    }
}
#endif
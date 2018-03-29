//#define SIMPLIFIED_MATERIAL

using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;

using Battlehub.RTSaveLoad;
using Battlehub.Utils;

using UnityObject = UnityEngine.Object;
#if PROC_MATERIAL
using ProcMaterial = UnityEngine.ProceduralMaterial;
using ProcPropertyDescription = UnityEngine.ProceduralPropertyDescription;
using ProcPropertyType = UnityEngine.ProceduralPropertyType;
#endif

namespace Battlehub.RTEditor
{
    #if !UNITY_WEBGL && PROC_MATERIAL
    public class ProceduralMaterialPropertyAccessor
    {
        private string m_inputName;
        private ProcMaterial m_material;

        public bool Bool
        {
            get { return m_material.GetProceduralBoolean(m_inputName); }
            set
            {
                m_material.SetProceduralBoolean(m_inputName, value);
                m_material.RebuildTextures();
            }
        }

        public float Float
        {
            get { return m_material.GetProceduralFloat(m_inputName); }
            set
            {
                m_material.SetProceduralFloat(m_inputName, value);
                m_material.RebuildTextures();
            }
        }

        public Vector2 Vector2
        {
            get { return m_material.GetProceduralVector(m_inputName); }
            set
            {
                m_material.SetProceduralVector(m_inputName, value);
                m_material.RebuildTextures();
            }
        }

        public Vector2 Vector3
        {
            get { return m_material.GetProceduralVector(m_inputName); }
            set
            {
                m_material.SetProceduralVector(m_inputName, value);
                m_material.RebuildTextures();
            }
        }

        public Vector2 Vector4
        {
            get { return m_material.GetProceduralVector(m_inputName); }
            set
            {
                m_material.SetProceduralVector(m_inputName, value);
                m_material.RebuildTextures();
            }
        }

        public Color Color
        {
            get { return m_material.GetProceduralColor(m_inputName); }
            set
            {
                m_material.SetProceduralColor(m_inputName, value);
                m_material.RebuildTextures();
            }
        }

        public int Enum
        {
            get { return m_material.GetProceduralEnum(m_inputName); }
            set
            {
                m_material.SetProceduralEnum(m_inputName, value);
                m_material.RebuildTextures();
            }
        }

        public Texture2D Texture
        {
            get { return m_material.GetProceduralTexture(m_inputName); }
            set
            {
                m_material.SetProceduralTexture(m_inputName, value);
                m_material.RebuildTextures();
            }
        }

        public ProceduralMaterialPropertyAccessor(ProcMaterial material, string inputName)
        {
            m_material = material;
            m_inputName = inputName;
        }
    }
#endif

    public class MaterialPropertyAccessor
    {
        private int m_propertyId;
        private string m_propertyName;
        private Material m_material;

        public Color Color
        {
            get { return m_material.GetColor(m_propertyId); }
            set { m_material.SetColor(m_propertyId, value); }
        }

        public float Float
        {
            get { return m_material.GetFloat(m_propertyId); }
            set { m_material.SetFloat(m_propertyId, value); }
        }

        public Vector4 Vector
        {
            get { return m_material.GetVector(m_propertyId); }
            set { m_material.SetVector(m_propertyId, value); }
        }

        public Texture Texture
        {
            get { return m_material.GetTexture(m_propertyId); }
            set { m_material.SetTexture(m_propertyId, value); }
        }

        public Texture2D Texture2D
        {
            get { return (Texture2D)m_material.GetTexture(m_propertyId); }
            set { m_material.SetTexture(m_propertyId, value); }
        }

        public Texture3D Texture3D
        {
            get { return (Texture3D)m_material.GetTexture(m_propertyId); }
            set { m_material.SetTexture(m_propertyId, value); }
        }

        public Cubemap Cubemap
        {
            get { return (Cubemap)m_material.GetTexture(m_propertyId); }
            set { m_material.SetTexture(m_propertyId, value); }
        }

        public Texture2DArray Texture2DArray
        {
            get { return (Texture2DArray)m_material.GetTexture(m_propertyId); }
            set { m_material.SetTexture(m_propertyId, value); }
        }

        public Vector2 TextureOffset
        {
            get { return m_material.GetTextureOffset(m_propertyName); }
            set { m_material.SetTextureOffset(m_propertyName, value); }
        }

        public Vector2 TextureScale
        {
            get { return m_material.GetTextureScale(m_propertyName); }
            set { m_material.SetTextureScale(m_propertyName, value); }
        }

        public MaterialPropertyAccessor(Material material, string propertyName)
        {
            m_material = material;
            m_propertyName = propertyName;
            m_propertyId = Shader.PropertyToID(m_propertyName);
        }
    }

#if SIMPLIFIED_MATERIAL
    public class MaterialDescriptor : IMaterialDescriptor
    {
        public string ShaderName
        {
            get { return "Battlehub.RTEditor.MaterialPropertySelector"; }
        }

        public void CreateConverters(MaterialEditor editor)
        {
        }

        public MaterialPropertyDescriptor[] GetProperties(MaterialEditor editor)
        {
            RuntimeShaderInfo shaderInfo = RuntimeShaderUtil.GetShaderInfo(editor.Material.shader);
            if (shaderInfo == null)
            {
                return null;
            }
            List<MaterialPropertyDescriptor> descriptors = new List<MaterialPropertyDescriptor>();
            for (int i = 0; i < shaderInfo.PropertyCount; ++i)
            {
                bool isHidden = shaderInfo.IsHidden[i];
                if (isHidden)
                {
                    continue;
                }

                string propertyDescr = shaderInfo.PropertyDescriptions[i];
                string propertyName = shaderInfo.PropertyNames[i];
                if(propertyName != "_Color" && propertyName != "_MainTex")
                {
                    continue;
                }

                RTShaderPropertyType propertyType = shaderInfo.PropertyTypes[i];
                RuntimeShaderInfo.RangeLimits limits = shaderInfo.PropertyRangeLimits[i];
                TextureDimension dim = shaderInfo.PropertyTexDims[i];
                PropertyInfo propertyInfo = null;
                switch (propertyType)
                {
                    case RTShaderPropertyType.Color:
                        propertyInfo = Strong.PropertyInfo((MaterialPropertyAccessor x) => x.Color);
                        break;
                    case RTShaderPropertyType.Float:
                        propertyInfo = Strong.PropertyInfo((MaterialPropertyAccessor x) => x.Float);
                        break;
                    case RTShaderPropertyType.Range:
                        propertyInfo = Strong.PropertyInfo((MaterialPropertyAccessor x) => x.Float);
                        break;
                    case RTShaderPropertyType.TexEnv:
                        switch (dim)
                        {
                            case TextureDimension.Any:
                                propertyInfo = Strong.PropertyInfo((MaterialPropertyAccessor x) => x.Texture);
                                break;
                            case TextureDimension.Cube:
                                propertyInfo = Strong.PropertyInfo((MaterialPropertyAccessor x) => x.Cubemap);
                                break;
                            case TextureDimension.None:
                                propertyInfo = null;
                                break;
                            case TextureDimension.Tex2D:
                                propertyInfo = Strong.PropertyInfo((MaterialPropertyAccessor x) => x.Texture2D);
                                break;
                            case TextureDimension.Tex2DArray:
                                propertyInfo = Strong.PropertyInfo((MaterialPropertyAccessor x) => x.Texture2DArray);
                                break;
                            case TextureDimension.Tex3D:
                                propertyInfo = Strong.PropertyInfo((MaterialPropertyAccessor x) => x.Texture3D);
                                break;
                            case TextureDimension.Unknown:
                                propertyInfo = null;
                                break;
                        }

                        break;
                    case RTShaderPropertyType.Vector:
                        propertyInfo = Strong.PropertyInfo((MaterialPropertyAccessor x) => x.Float);
                        break;
                }

                if (propertyInfo == null)
                {
                    continue;
                }

                MaterialPropertyAccessor accessor = new MaterialPropertyAccessor(editor.Material, propertyName);
                MaterialPropertyDescriptor propertyDescriptor = new MaterialPropertyDescriptor(accessor, propertyDescr, propertyType, propertyInfo, limits, dim, null);
                descriptors.Add(propertyDescriptor);
            }

            return descriptors.ToArray();
        }
    }
#else
    public class MaterialDescriptor : IMaterialDescriptor
    {
        public string ShaderName
        {
            get { return "Battlehub.RTEditor.MaterialPropertySelector"; }
        }

        public object CreateConverter(MaterialEditor editor)
        {
            return null;
        }

        public MaterialPropertyDescriptor[] GetProperties(MaterialEditor editor, object converter)
        {
            RuntimeShaderInfo shaderInfo = null;
            IRuntimeShaderUtil shaderUtil = Dependencies.ShaderUtil;
            if (shaderUtil != null)
            {
                shaderInfo = shaderUtil.GetShaderInfo(editor.Material.shader);
            }

#if !UNITY_WEBGL && PROC_MATERIAL
            ProcMaterial procMaterial = editor.Material as ProcMaterial;       
            if (shaderInfo == null && procMaterial == null)
            {
                return null;
            }
#else
            if (shaderInfo == null)
            {
                return null;
            }
#endif
            List<MaterialPropertyDescriptor> descriptors = new List<MaterialPropertyDescriptor>();
            if (shaderInfo != null)
            {
                for (int i = 0; i < shaderInfo.PropertyCount; ++i)
                {
                    bool isHidden = shaderInfo.IsHidden[i];
                    if (isHidden)
                    {
                        continue;
                    }

                    string propertyDescr = shaderInfo.PropertyDescriptions[i];
                    string propertyName = shaderInfo.PropertyNames[i];
                    RTShaderPropertyType propertyType = shaderInfo.PropertyTypes[i];
                    RuntimeShaderInfo.RangeLimits limits = shaderInfo.PropertyRangeLimits[i];
                    TextureDimension dim = shaderInfo.PropertyTexDims[i];
                    PropertyInfo propertyInfo = null;
                    switch (propertyType)
                    {
                        case RTShaderPropertyType.Color:
                            propertyInfo = Strong.PropertyInfo((MaterialPropertyAccessor x) => x.Color);
                            break;
                        case RTShaderPropertyType.Float:
                            propertyInfo = Strong.PropertyInfo((MaterialPropertyAccessor x) => x.Float);
                            break;
                        case RTShaderPropertyType.Range:
                            propertyInfo = Strong.PropertyInfo((MaterialPropertyAccessor x) => x.Float);
                            break;
                        case RTShaderPropertyType.TexEnv:
                            switch (dim)
                            {
                                case TextureDimension.Any:
                                    propertyInfo = Strong.PropertyInfo((MaterialPropertyAccessor x) => x.Texture);
                                    break;
                                case TextureDimension.Cube:
                                    propertyInfo = Strong.PropertyInfo((MaterialPropertyAccessor x) => x.Cubemap);
                                    break;
                                case TextureDimension.None:
                                    propertyInfo = null;
                                    break;
                                case TextureDimension.Tex2D:
                                    propertyInfo = Strong.PropertyInfo((MaterialPropertyAccessor x) => x.Texture2D);
                                    break;
                                case TextureDimension.Tex2DArray:
                                    propertyInfo = Strong.PropertyInfo((MaterialPropertyAccessor x) => x.Texture2DArray);
                                    break;
                                case TextureDimension.Tex3D:
                                    propertyInfo = Strong.PropertyInfo((MaterialPropertyAccessor x) => x.Texture3D);
                                    break;
                                case TextureDimension.Unknown:
                                    propertyInfo = null;
                                    break;
                            }

                            break;
                        case RTShaderPropertyType.Vector:
                            propertyInfo = Strong.PropertyInfo((MaterialPropertyAccessor x) => x.Vector);
                            break;
                    }

                    if (propertyInfo == null)
                    {
                        continue;
                    }

                    MaterialPropertyAccessor accessor = new MaterialPropertyAccessor(editor.Material, propertyName);
                    MaterialPropertyDescriptor propertyDescriptor = new MaterialPropertyDescriptor(accessor, propertyDescr, propertyType, propertyInfo, limits, dim, null);
                    descriptors.Add(propertyDescriptor);
                }
            }
#if !UNITY_WEBGL && PROC_MATERIAL
            GetProceduralMaterialDescriptors(procMaterial, descriptors);
#endif

            return descriptors.ToArray();
        }
#if !UNITY_WEBGL && PROC_MATERIAL
        public static void GetProceduralMaterialDescriptors(ProcMaterial procMaterial, List<MaterialPropertyDescriptor> descriptors)
        {
           
            if (procMaterial != null)
            {
                ProcPropertyDescription[] inputs = procMaterial.GetProceduralPropertyDescriptions();
                for (int i = 0; i < inputs.Length; ++i)
                {
                    ProcPropertyDescription input = inputs[i];
                    
                    PropertyInfo propertyInfo = null;

                    switch (input.type)
                    {
                        case ProcPropertyType.Boolean:
                            propertyInfo = Strong.PropertyInfo((ProceduralMaterialPropertyAccessor x) => x.Bool);
                            break;
                        case ProcPropertyType.Color3:
                        case ProcPropertyType.Color4:
                            propertyInfo = Strong.PropertyInfo((ProceduralMaterialPropertyAccessor x) => x.Color);
                            break;
                        case ProcPropertyType.Enum:
                            propertyInfo = Strong.PropertyInfo((ProceduralMaterialPropertyAccessor x) => x.Enum);
                            break;
                        case ProcPropertyType.Float:
                            propertyInfo = Strong.PropertyInfo((ProceduralMaterialPropertyAccessor x) => x.Float);
                            break;
                        case ProcPropertyType.Texture:
                            propertyInfo = Strong.PropertyInfo((ProceduralMaterialPropertyAccessor x) => x.Texture);
                            break;
                        case ProcPropertyType.Vector2:
                            propertyInfo = Strong.PropertyInfo((ProceduralMaterialPropertyAccessor x) => x.Vector2);
                            break;
                        case ProcPropertyType.Vector3:
                            propertyInfo = Strong.PropertyInfo((ProceduralMaterialPropertyAccessor x) => x.Vector3);
                            break;
                        case ProcPropertyType.Vector4:
                            propertyInfo = Strong.PropertyInfo((ProceduralMaterialPropertyAccessor x) => x.Vector4);
                            break;
                    }

                    if (propertyInfo == null)
                    {
                        continue;
                    }

                    ProceduralMaterialPropertyAccessor accessor = new ProceduralMaterialPropertyAccessor(procMaterial, input.name);
                    MaterialPropertyDescriptor propertyDescriptor = new MaterialPropertyDescriptor(accessor, input.label, input, propertyInfo, null);
                    descriptors.Add(propertyDescriptor);
                }
            }
        }
#endif
    }

#endif
}

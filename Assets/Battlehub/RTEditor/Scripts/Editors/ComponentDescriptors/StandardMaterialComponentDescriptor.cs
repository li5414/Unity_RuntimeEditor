//#define SIMPLIFIED_MATERIAL

using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;

using Battlehub.RTSaveLoad;
using Battlehub.Utils;

namespace Battlehub.RTEditor
{
    #if !SIMPLIFIED_MATERIAL
    public class StandardMaterialValueConverter 
    {
        private enum WorkflowMode
        {
            Specular,
            Metallic,
            Dielectric
        }

        public enum SmoothnessMapChannel
        {
            SpecularMetallicAlpha,
            AlbedoAlpha,
        }


        public enum BlendMode
        {
            Opaque,
            Cutout,
            Fade,       // Old school alpha-blending mode, fresnel does not affect amount of transparency
            Transparent // Physically plausible transparency mode, implemented as alpha pre-multiply
        }

        public enum UVSec
        {
            UV0 = 0,
            UV1 = 1
        }


        private WorkflowMode m_workflow = WorkflowMode.Metallic;


        public BlendMode Mode
        {
            get { return (BlendMode)Material.GetFloat("_Mode"); }
            set
            {
                if (Mode != value)
                {
                    Material.SetFloat("_Mode", (float)value);
                    SetupMaterialWithBlendMode(Material, Mode);
                    SetMaterialKeywords(Material, m_workflow);
                }
            }
        }

        public float Cutoff
        {
            get { return Material.GetFloat("_Cutoff"); }
            set
            {
                Material.SetFloat("_Cutoff", value);
                SetMaterialKeywords(Material, m_workflow);
                SetupMaterialWithBlendMode(Material, Mode);
            }
        }

        public Texture MetallicGlossMap
        {
            get { return Material.GetTexture("_MetallicGlossMap"); }
            set
            {
                Material.SetTexture("_MetallicGlossMap", value);
                SetMaterialKeywords(Material, m_workflow);
                SetupMaterialWithBlendMode(Material, Mode);
            }
        }



        public Texture BumpMap
        {
            get { return Material.GetTexture("_BumpMap"); }
            set
            {
                Material.SetTexture("_BumpMap", value);
                SetMaterialKeywords(Material, m_workflow);
                SetupMaterialWithBlendMode(Material, Mode);
            }
        }

        public Texture ParallaxMap
        {
            get { return Material.GetTexture("_ParallaxMap"); }
            set
            {
                Material.SetTexture("_ParallaxMap", value);
                SetMaterialKeywords(Material, m_workflow);
                SetupMaterialWithBlendMode(Material, Mode);
            }
        }

        public Texture OcclusionMap
        {
            get { return Material.GetTexture("_OcclusionMap"); }
            set
            {
                Material.SetTexture("_OcclusionMap", value);
                SetMaterialKeywords(Material, m_workflow);
                SetupMaterialWithBlendMode(Material, Mode);
            }
        }

        public Texture EmissionMap
        {
            get { return Material.GetTexture("_EmissionMap"); }
            set
            {
                Material.SetTexture("_EmissionMap", value);
                SetMaterialKeywords(Material, m_workflow);
                SetupMaterialWithBlendMode(Material, Mode);
            }
        }

        public Color EmissionColor
        {
            get { return Material.GetColor("_EmissionColor"); }
            set
            {
                Material.SetColor("_EmissionColor", value);
                SetMaterialKeywords(Material, m_workflow);
                SetupMaterialWithBlendMode(Material, Mode);
            }
        }

        public Texture DetailMask
        {
            get { return Material.GetTexture("_DetailMask"); }
            set
            {
                Material.SetTexture("_DetailMask", value);
                SetMaterialKeywords(Material, m_workflow);
            }
        }

        public Texture DetailAlbedoMap
        {
            get { return Material.GetTexture("_DetailAlbedoMap"); }
            set
            {
                Material.SetTexture("_DetailAlbedoMap", value);
                SetMaterialKeywords(Material, m_workflow);
            }
        }

        public Texture DetailNormalMap
        {
            get { return Material.GetTexture("_DetailNormalMap"); }
            set
            {
                Material.SetTexture("_DetailNormalMap", value);
                SetMaterialKeywords(Material, m_workflow);
            }
        }

        public UVSec UVSecondary
        {
            get { return (UVSec)Material.GetFloat("_UVSec"); }
            set
            {
                Material.SetFloat("_UVSec", (float)value);
                SetMaterialKeywords(Material, m_workflow);
            }
        }

        private static bool ShouldEmissionBeEnabled(Material mat, Color color)
        {
            var realtimeEmission = (mat.globalIlluminationFlags & MaterialGlobalIlluminationFlags.RealtimeEmissive) > 0;
            return color.maxColorComponent > 0.1f / 255.0f || realtimeEmission;
        }

        public static SmoothnessMapChannel GetSmoothnessMapChannel(Material material)
        {
            int ch = (int)material.GetFloat("_SmoothnessTextureChannel");
            if (ch == (int)SmoothnessMapChannel.AlbedoAlpha)
                return SmoothnessMapChannel.AlbedoAlpha;
            else
                return SmoothnessMapChannel.SpecularMetallicAlpha;
        }


        private static void SetMaterialKeywords(Material material, WorkflowMode workflowMode)
        {

             // Note: keywords must be based on Material value not on MaterialProperty due to multi-edit & material animation
            // (MaterialProperty value might come from renderer material property block)
            SetKeyword(material, "_NORMALMAP", material.GetTexture("_BumpMap") || material.GetTexture("_DetailNormalMap"));
            if (workflowMode == WorkflowMode.Specular)
                SetKeyword(material, "_SPECGLOSSMAP", material.GetTexture("_SpecGlossMap"));
            else if (workflowMode == WorkflowMode.Metallic)
                SetKeyword(material, "_METALLICGLOSSMAP", material.GetTexture("_MetallicGlossMap"));
            SetKeyword(material, "_PARALLAXMAP", material.GetTexture("_ParallaxMap"));
            SetKeyword(material, "_DETAIL_MULX2", material.GetTexture("_DetailAlbedoMap") || material.GetTexture("_DetailNormalMap"));

            // A material's GI flag internally keeps track of whether emission is enabled at all, it's enabled but has no effect
            // or is enabled and may be modified at runtime. This state depends on the values of the current flag and emissive color.
            // The fixup routine makes sure that the material is in the correct state if/when changes are made to the mode or color.
           // MaterialEditor.FixupEmissiveFlag(material);
            bool shouldEmissionBeEnabled = (material.globalIlluminationFlags & MaterialGlobalIlluminationFlags.EmissiveIsBlack) == 0;
            SetKeyword(material, "_EMISSION", shouldEmissionBeEnabled);

            if (material.HasProperty("_SmoothnessTextureChannel"))
            {
                SetKeyword(material, "_SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A", GetSmoothnessMapChannel(material) == SmoothnessMapChannel.AlbedoAlpha);
            }

            //// Note: keywords must be based on Material value not on MaterialProperty due to multi-edit & material animation
            //// (MaterialProperty value might come from renderer material property block)
            //SetKeyword(material, "_NORMALMAP", material.GetTexture("_BumpMap") || material.GetTexture("_DetailNormalMap"));
            //if (workflowMode == WorkflowMode.Specular)
            //    SetKeyword(material, "_SPECGLOSSMAP", material.GetTexture("_SpecGlossMap"));
            //else if (workflowMode == WorkflowMode.Metallic)
            //    SetKeyword(material, "_METALLICGLOSSMAP", material.GetTexture("_MetallicGlossMap"));
            //SetKeyword(material, "_PARALLAXMAP", material.GetTexture("_ParallaxMap"));
            //SetKeyword(material, "_DETAIL_MULX2", material.GetTexture("_DetailAlbedoMap") || material.GetTexture("_DetailNormalMap"));

            //bool shouldEmissionBeEnabled = ShouldEmissionBeEnabled(material, material.GetColor("_EmissionColor"));
            //SetKeyword(material, "_EMISSION", shouldEmissionBeEnabled);

            //if (material.HasProperty("_SmoothnessTextureChannel"))
            //{
            //    SetKeyword(material, "_SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A", GetSmoothnessMapChannel(material) == SmoothnessMapChannel.AlbedoAlpha);
            //}

            //// Setup lightmap emissive flags
            //MaterialGlobalIlluminationFlags flags = material.globalIlluminationFlags;
            //if ((flags & (MaterialGlobalIlluminationFlags.BakedEmissive | MaterialGlobalIlluminationFlags.RealtimeEmissive)) != 0)
            //{
            //    flags &= ~MaterialGlobalIlluminationFlags.EmissiveIsBlack;
            //    if (!shouldEmissionBeEnabled)
            //        flags |= MaterialGlobalIlluminationFlags.EmissiveIsBlack;

            //    material.globalIlluminationFlags = flags;
            //}
        }

        static void SetKeyword(Material m, string keyword, bool state)
        {
            if (state)
                m.EnableKeyword(keyword);
            else
                m.DisableKeyword(keyword);
        }

        public static void SetupMaterialWithBlendMode(Material material, BlendMode blendMode)
        {
            switch (blendMode)
            {
                case BlendMode.Opaque:
                    material.SetOverrideTag("RenderType", "");
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt("_ZWrite", 1);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = -1;
                    break;
                case BlendMode.Cutout:
                    material.SetOverrideTag("RenderType", "TransparentCutout");
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt("_ZWrite", 1);
                    material.EnableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = (int)RenderQueue.AlphaTest;
                    break;
                case BlendMode.Fade:
                    material.SetOverrideTag("RenderType", "Transparent");
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_ZWrite", 0);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.EnableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = (int)RenderQueue.Transparent;
                    break;
                case BlendMode.Transparent:
                    material.SetOverrideTag("RenderType", "Transparent");
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_ZWrite", 0);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = (int)RenderQueue.Transparent;
                    break;
            }
        }

        public Material Material
        {
            get;
            set;
        }
    }

    public class StandardMaterialDescriptor : IMaterialDescriptor
    {
        const string _Mode = "_Mode";
        const string _MainTex = "_MainTex";
        const string _Color = "_Color";
        const string _Cutoff = "_Cutoff";
        const string _MetallicGlossMap = "_MetallicGlossMap";
        const string _Metallic = "_Metallic";
        const string _Glossiness = "_Glossiness";
        const string _GlossMapScale = "_GlossMapScale";
        const string _BumpScale = "_BumpScale";
        const string _Parallax = "_Parallax";
        const string _OcclusionStrength = "_OcclusionStrength";
        const string _DetailNormalMapScale = "_DetailNormalMapScale";

        public enum BlendMode
        {
            Opaque,
            Cutout,
            Fade,       // Old school alpha-blending mode, fresnel does not affect amount of transparency
            Transparent // Physically plausible transparency mode, implemented as alpha pre-multiply
        }


        private BlendMode GetBlendMode(Material material)
        {
            return (BlendMode)material.GetFloat(_Mode);
        }

        private bool IsMetallicGlossMapSet(Material material)
        {
            Texture texture = material.GetTexture(_MetallicGlossMap);

            return texture != null;
        }


        public string ShaderName
        {
            get { return "Standard"; }
        }

        public object CreateConverter(MaterialEditor editor)
        {
            StandardMaterialValueConverter converter = new StandardMaterialValueConverter();
            converter.Material = editor.Material;
            return converter;
        }

        public MaterialPropertyDescriptor[] GetProperties(MaterialEditor editor, object converterObject)
        {
            PropertyEditorCallback valueChangedCallback = () => editor.BuildEditor();
            StandardMaterialValueConverter converter = (StandardMaterialValueConverter)converterObject;

            PropertyInfo modeInfo = Strong.PropertyInfo((StandardMaterialValueConverter x) => x.Mode);
            PropertyInfo cutoffInfo = Strong.PropertyInfo((StandardMaterialValueConverter x) => x.Cutoff);
            PropertyInfo metallicMapInfo = Strong.PropertyInfo((StandardMaterialValueConverter x) => x.MetallicGlossMap);
            PropertyInfo bumpMapInfo = Strong.PropertyInfo((StandardMaterialValueConverter x) => x.BumpMap);
            PropertyInfo parallaxMapInfo = Strong.PropertyInfo((StandardMaterialValueConverter x) => x.ParallaxMap);
            PropertyInfo occlusionMapInfo = Strong.PropertyInfo((StandardMaterialValueConverter x) => x.OcclusionMap);
            PropertyInfo emissionMapInfo = Strong.PropertyInfo((StandardMaterialValueConverter x) => x.EmissionMap);
            PropertyInfo emissionColorInfo = Strong.PropertyInfo((StandardMaterialValueConverter x) => x.EmissionColor);
            PropertyInfo detailMaskInfo = Strong.PropertyInfo((StandardMaterialValueConverter x) => x.DetailMask);
            PropertyInfo detailAlbedoMap = Strong.PropertyInfo((StandardMaterialValueConverter x) => x.DetailAlbedoMap);
            PropertyInfo detailNormalMap = Strong.PropertyInfo((StandardMaterialValueConverter x) => x.DetailNormalMap);
            // PropertyInfo uvSecondaryInfo = Strong.PropertyInfo((StandardMaterialValueConverter x) => x.UVSecondary);

            PropertyInfo texInfo = Strong.PropertyInfo((MaterialPropertyAccessor x) => x.Texture);
            PropertyInfo colorInfo = Strong.PropertyInfo((MaterialPropertyAccessor x) => x.Color);
            PropertyInfo floatInfo = Strong.PropertyInfo((MaterialPropertyAccessor x) => x.Float);

            BlendMode mode = GetBlendMode(editor.Material);
            List<MaterialPropertyDescriptor> properties = new List<MaterialPropertyDescriptor>();
            properties.Add(new MaterialPropertyDescriptor(converter, "Rendering Mode", RTShaderPropertyType.Float, modeInfo, new RuntimeShaderInfo.RangeLimits(), TextureDimension.None, valueChangedCallback));
            properties.Add(new MaterialPropertyDescriptor(new MaterialPropertyAccessor(editor.Material, _MainTex), "Albedo", RTShaderPropertyType.TexEnv, texInfo, new RuntimeShaderInfo.RangeLimits(), TextureDimension.Tex2D, null));
            properties.Add(new MaterialPropertyDescriptor(new MaterialPropertyAccessor(editor.Material, _Color), "Albedo Color", RTShaderPropertyType.Color, colorInfo, new RuntimeShaderInfo.RangeLimits(), TextureDimension.None, null));
            if (mode == BlendMode.Cutout)
            {
                properties.Add(new MaterialPropertyDescriptor(converter, "Alpha Cutoff", RTShaderPropertyType.Range, cutoffInfo, new RuntimeShaderInfo.RangeLimits(0.5f, 0.0f, 1.0f), TextureDimension.None, null));
            }

            properties.Add(new MaterialPropertyDescriptor(converter, "Metallic", RTShaderPropertyType.TexEnv, metallicMapInfo, new RuntimeShaderInfo.RangeLimits(0.0f, 0.0f, 0.0f), TextureDimension.Tex2D, valueChangedCallback));
            bool hasGlossMap = IsMetallicGlossMapSet(editor.Material);
            if (!hasGlossMap)
            {
                properties.Add(new MaterialPropertyDescriptor(new MaterialPropertyAccessor(editor.Material, _Metallic), "Metallic", RTShaderPropertyType.Range, floatInfo, new RuntimeShaderInfo.RangeLimits(0.0f, 0.0f, 1.0f), TextureDimension.None, null));
                if (StandardMaterialValueConverter.GetSmoothnessMapChannel(editor.Material) == StandardMaterialValueConverter.SmoothnessMapChannel.SpecularMetallicAlpha)
                {
                    properties.Add(new MaterialPropertyDescriptor(new MaterialPropertyAccessor(editor.Material, _Glossiness), "Smoothness", RTShaderPropertyType.Range, floatInfo, new RuntimeShaderInfo.RangeLimits(0.5f, 0.0f, 1.0f), TextureDimension.None, null));
                }
                else
                {
                    properties.Add(new MaterialPropertyDescriptor(new MaterialPropertyAccessor(editor.Material, _GlossMapScale), "Smoothness", RTShaderPropertyType.Range, floatInfo, new RuntimeShaderInfo.RangeLimits(1.0f, 0.0f, 1.0f), TextureDimension.None, null));
                }
            }
            else
            {
                properties.Add(new MaterialPropertyDescriptor(new MaterialPropertyAccessor(editor.Material, _GlossMapScale), "Smoothness", RTShaderPropertyType.Range, floatInfo, new RuntimeShaderInfo.RangeLimits(1.0f, 0.0f, 1.0f), TextureDimension.None, null));
            }


            properties.Add(new MaterialPropertyDescriptor(converter, "Normal Map", RTShaderPropertyType.TexEnv, bumpMapInfo, new RuntimeShaderInfo.RangeLimits(0.0f, 0.0f, 0.0f), TextureDimension.Tex2D, valueChangedCallback));
            if (converter.BumpMap != null)
            {
                properties.Add(new MaterialPropertyDescriptor(new MaterialPropertyAccessor(editor.Material, _BumpScale), "Normal Map Scale", RTShaderPropertyType.Float, floatInfo, new RuntimeShaderInfo.RangeLimits(0.0f, 0.0f, 0.0f), TextureDimension.None, null));
            }

            properties.Add(new MaterialPropertyDescriptor(converter, "Height Map", RTShaderPropertyType.TexEnv, parallaxMapInfo, new RuntimeShaderInfo.RangeLimits(0.0f, 0.0f, 0.0f), TextureDimension.Tex2D, valueChangedCallback));
            if (converter.ParallaxMap != null)
            {
                properties.Add(new MaterialPropertyDescriptor(new MaterialPropertyAccessor(editor.Material, _Parallax), "Height Map Scale", RTShaderPropertyType.Range, floatInfo, new RuntimeShaderInfo.RangeLimits(0.02f, 0.005f, 0.08f), TextureDimension.None, null));
            }

            properties.Add(new MaterialPropertyDescriptor(converter, "Occlusion Map", RTShaderPropertyType.TexEnv, occlusionMapInfo, new RuntimeShaderInfo.RangeLimits(0.0f, 0.0f, 0.0f), TextureDimension.Tex2D, valueChangedCallback));
            if (converter.OcclusionMap != null)
            {
                properties.Add(new MaterialPropertyDescriptor(new MaterialPropertyAccessor(editor.Material, _OcclusionStrength), "Occlusion Strength", RTShaderPropertyType.Range, floatInfo, new RuntimeShaderInfo.RangeLimits(1.0f, 0, 1.0f), TextureDimension.None, null));
            }

            properties.Add(new MaterialPropertyDescriptor(converter, "Emission Map", RTShaderPropertyType.TexEnv, emissionMapInfo, new RuntimeShaderInfo.RangeLimits(0.0f, 0.0f, 0.0f), TextureDimension.Tex2D, null));
            properties.Add(new MaterialPropertyDescriptor(converter, "Emission Color", RTShaderPropertyType.Color, emissionColorInfo, new RuntimeShaderInfo.RangeLimits(0.0f, 0.0f, 0.0f), TextureDimension.None, null));
            properties.Add(new MaterialPropertyDescriptor(converter, "Detail Mask", RTShaderPropertyType.TexEnv, detailMaskInfo, new RuntimeShaderInfo.RangeLimits(0.0f, 0.0f, 0.0f), TextureDimension.Tex2D, null));
            properties.Add(new MaterialPropertyDescriptor(converter, "Detail Albedo Map", RTShaderPropertyType.TexEnv, detailAlbedoMap, new RuntimeShaderInfo.RangeLimits(0.0f, 0.0f, 0.0f), TextureDimension.Tex2D, null));
            properties.Add(new MaterialPropertyDescriptor(converter, "Detail Normal Map", RTShaderPropertyType.TexEnv, detailNormalMap, new RuntimeShaderInfo.RangeLimits(0.0f, 0.0f, 0.0f), TextureDimension.Tex2D, null));
            properties.Add(new MaterialPropertyDescriptor(new MaterialPropertyAccessor(editor.Material, _DetailNormalMapScale), "Detail Scale", RTShaderPropertyType.Float, floatInfo, new RuntimeShaderInfo.RangeLimits(0, 0, 0), TextureDimension.None, null));
            //properties.Add(new MaterialPropertyDescriptor(converter, "UV Set", RTShaderPropertyType.Float, detailNormalMap, new RuntimeShaderInfo.RangeLimits(0.0f, 0.0f, 0.0f), TextureDimension.Tex2D, null));

#if !UNITY_WEBGL
            ProceduralMaterial procMaterial = editor.Material as ProceduralMaterial;
            if(procMaterial != null)
            {
                MaterialDescriptor.GetProceduralMaterialDescriptors(procMaterial, properties);
            }
#endif
            
            return properties.ToArray();
        }
    }
#endif


}

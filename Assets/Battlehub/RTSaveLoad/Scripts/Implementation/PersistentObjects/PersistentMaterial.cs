#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif

/*To autogenerate PersistentMaterial remove this file and run Tools->Runtime SaveLoad->Create Persistent Objects command*/

using UnityEngine;
using System.Collections.Generic;

namespace Battlehub.RTSaveLoad.PersistentObjects
{
#if RT_USE_PROTOBUF && !RT_PE_MAINTANANCE
    [ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
#if !UNITY_WEBGL
    [ProtoInclude(1073, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentProceduralMaterial))]
#endif
#endif
    [System.Serializable]
    public class PersistentMaterial
        #if RT_PE_MAINTANANCE
        : PersistentData
        #else
        : PersistentObject
        #endif
    {

        public RTShaderPropertyType[] m_propertyTypes;
        public string[] m_propertyNames;
        public DataContract[] m_propertyValues;
        public string[] m_keywords;

        public override object WriteTo(object obj, Dictionary<long, Object> objects)
        {
            obj = base.WriteTo(obj, objects);
            if (obj == null)
            {
                return null;
            }
            Material o = (Material)obj;
            //if (IdentifiersMap.IsNotMapped(o))
            {
                o.shader = (Shader)objects.Get(shader);
            }

            if(o.HasProperty("_Color"))
            {
                o.color = color;
            }
            
            if (o.HasProperty("_MainTex"))
            {
                o.mainTexture = (Texture)objects.Get(mainTexture);
                o.mainTextureOffset = mainTextureOffset;
                o.mainTextureScale = mainTextureScale;
            }
            o.renderQueue = renderQueue;
            o.shaderKeywords = shaderKeywords;
            o.globalIlluminationFlags = (MaterialGlobalIlluminationFlags)globalIlluminationFlags;
            o.enableInstancing = enableInstancing;

            if (m_keywords != null)
            {
                foreach (string keyword in m_keywords)
                {
                    o.EnableKeyword(keyword);
                }
            }

            if (m_propertyNames != null)
            {
                for (int i = 0; i < m_propertyNames.Length; ++i)
                {
                    string name = m_propertyNames[i];
                    RTShaderPropertyType type = m_propertyTypes[i];
                    switch (type)
                    {
                        case RTShaderPropertyType.Color:
                            if (m_propertyValues[i].AsPrimitive.ValueBase is Color)
                            {
                                o.SetColor(name, (Color)m_propertyValues[i].AsPrimitive.ValueBase);
                            }
                            break;
                        case RTShaderPropertyType.Float:
                            if (m_propertyValues[i].AsPrimitive.ValueBase is float)
                            {
                                o.SetFloat(name, (float)m_propertyValues[i].AsPrimitive.ValueBase);
                            }
                            break;
                        case RTShaderPropertyType.Range:
                            if (m_propertyValues[i].AsPrimitive.ValueBase is float)
                            {
                                o.SetFloat(name, (float)m_propertyValues[i].AsPrimitive.ValueBase);
                            }
                            break;
                        case RTShaderPropertyType.TexEnv:
                            if (m_propertyValues[i].AsPrimitive.ValueBase is long)
                            {
                                o.SetTexture(name, objects.Get((long)m_propertyValues[i].AsPrimitive.ValueBase) as Texture);
                            }
                            break;
                        case RTShaderPropertyType.Vector:
                            if (m_propertyValues[i].AsPrimitive.ValueBase is Vector4)
                            {
                                o.SetVector(name, (Vector4)m_propertyValues[i].AsPrimitive.ValueBase);
                            }
                            break;
                        case RTShaderPropertyType.Unknown:
                            break;
                    }
                }
            }
            return o;
        }

        public override void FindDependencies<T>(Dictionary<long, T> dependencies, Dictionary<long, T> objects, bool allowNulls)
        {
            base.FindDependencies(dependencies, objects, allowNulls);

            AddDependency(shader, dependencies, objects, allowNulls);
            AddDependency(mainTexture, dependencies, objects, allowNulls);

            if (m_propertyValues != null)
            {
                for (int i = 0; i < m_propertyValues.Length; ++i)
                {
                    RTShaderPropertyType type = m_propertyTypes[i];
                    switch (type)
                    {
                        case RTShaderPropertyType.TexEnv:
                            if (m_propertyValues[i].AsPrimitive.ValueBase is long)
                            {
                                AddDependency((long)m_propertyValues[i].AsPrimitive.ValueBase, dependencies, objects, allowNulls);
                            }
                            break;
                    }
                }
            }
        }

        protected override void GetDependencies(Dictionary<long, Object> dependencies, object obj)
        {
            base.GetDependencies(dependencies, obj);
            if (obj == null)
            {
                return;
            }

            Material o = (Material)obj;
            AddDependency(o.shader, dependencies);
            if (o.HasProperty("_MainTex"))
            {
                AddDependency(o.mainTexture, dependencies);
            }

            RuntimeShaderInfo shaderInfo = null;
            IRuntimeShaderUtil shaderUtil = Dependencies.ShaderUtil;
            if(shaderUtil != null)
            {
                shaderInfo = shaderUtil.GetShaderInfo(o.shader);
            }
             
            if (shaderInfo == null)
            {
                return;
            }

            for (int i = 0; i < shaderInfo.PropertyCount; ++i)
            {
                string name = shaderInfo.PropertyNames[i];
                RTShaderPropertyType type = shaderInfo.PropertyTypes[i];
                switch (type)
                {
                    case RTShaderPropertyType.TexEnv:
                        AddDependency(o.GetTexture(name), dependencies);
                        break;
                }
            }
        }

        public override void ReadFrom(object obj)
        {
            base.ReadFrom(obj);
            if (obj == null)
            {
                return;
            }
            Material o = (Material)obj;

            shader = o.shader.GetMappedInstanceID();

            if(o.HasProperty("_Color"))
            {
                color = o.color;
            }
            if(o.HasProperty("_MainTex"))
            {
                mainTexture = o.mainTexture.GetMappedInstanceID();
                mainTextureOffset = o.mainTextureOffset;
                mainTextureScale = o.mainTextureScale;
            }


            renderQueue = o.renderQueue;
            shaderKeywords = o.shaderKeywords;
            globalIlluminationFlags = (uint)o.globalIlluminationFlags;
            enableInstancing = o.enableInstancing;

            if (o.shader == null)
            {
                return;
            }

            RuntimeShaderInfo shaderInfo = null;
            IRuntimeShaderUtil shaderUtil = Dependencies.ShaderUtil;
            if (shaderUtil != null)
            {
                shaderInfo = shaderUtil.GetShaderInfo(o.shader);
            }
            if (shaderInfo == null)
            {
                return;
            }

            m_propertyNames = new string[shaderInfo.PropertyCount];
            m_propertyTypes = new RTShaderPropertyType[shaderInfo.PropertyCount];
            m_propertyValues = new DataContract[shaderInfo.PropertyCount];
       
            for (int i = 0; i < shaderInfo.PropertyCount; ++i)
            {
                string name = shaderInfo.PropertyNames[i];
                RTShaderPropertyType type = shaderInfo.PropertyTypes[i];
                m_propertyNames[i] = name;
                m_propertyTypes[i] = type;
                switch (type)
                {
                    case RTShaderPropertyType.Color:
                        m_propertyValues[i] = new DataContract(PrimitiveContract.Create(o.GetColor(name)));
                        break;
                    case RTShaderPropertyType.Float:
                        m_propertyValues[i] = new DataContract(PrimitiveContract.Create(o.GetFloat(name)));
                        break;
                    case RTShaderPropertyType.Range:
                        m_propertyValues[i] = new DataContract(PrimitiveContract.Create(o.GetFloat(name)));
                        break;
                    case RTShaderPropertyType.TexEnv:
                        m_propertyValues[i] = new DataContract(PrimitiveContract.Create(o.GetTexture(name).GetMappedInstanceID()));
                        break;
                    case RTShaderPropertyType.Vector:
                        m_propertyValues[i] = new DataContract(PrimitiveContract.Create(o.GetVector(name)));
                        break;
                    case RTShaderPropertyType.Unknown:
                        m_propertyValues[i] = null;
                        break;
                }
            }

         
            m_keywords = o.shaderKeywords;
        }

        public long shader;
  
        public Color color;
   
        public long mainTexture;
        
        public Vector2 mainTextureOffset;

        public Vector2 mainTextureScale;

        public int renderQueue;

        public string[] shaderKeywords;
       
        public uint globalIlluminationFlags;

        public bool enableInstancing;
    }   
}

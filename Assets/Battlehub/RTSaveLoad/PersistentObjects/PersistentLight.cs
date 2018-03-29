#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif
using Battlehub.RTSaveLoad;
/*This is auto-generated file. Tools->Runtime SaveLoad->Create Persistent Objects 
If you want prevent overwriting, drag this file to another folder.*/

namespace Battlehub.RTSaveLoad.PersistentObjects
{
	#if RT_USE_PROTOBUF
	[ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
	#endif
	[System.Serializable]
	public class PersistentLight : Battlehub.RTSaveLoad.PersistentObjects.PersistentBehaviour
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.Light o = (UnityEngine.Light)obj;
			o.type = (UnityEngine.LightType)type;
			o.color = color;
			o.colorTemperature = colorTemperature;
			o.intensity = intensity;
			o.bounceIntensity = bounceIntensity;
			o.shadows = (UnityEngine.LightShadows)shadows;
			o.shadowStrength = shadowStrength;
			o.shadowResolution = (UnityEngine.Rendering.LightShadowResolution)shadowResolution;
			o.shadowCustomResolution = shadowCustomResolution;
			o.shadowBias = shadowBias;
			o.shadowNormalBias = shadowNormalBias;
			o.shadowNearPlane = shadowNearPlane;
			o.range = range;
			o.spotAngle = spotAngle;
			o.cookieSize = cookieSize;
			o.cookie = (UnityEngine.Texture)objects.Get(cookie);
			o.flare = (UnityEngine.Flare)objects.Get(flare);
			o.renderMode = (UnityEngine.LightRenderMode)renderMode;
			o.alreadyLightmapped = alreadyLightmapped;
			o.cullingMask = cullingMask;

			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.Light o = (UnityEngine.Light)obj;
			type = (uint)o.type;
			color = o.color;
			colorTemperature = o.colorTemperature;
			intensity = o.intensity;
			bounceIntensity = o.bounceIntensity;
			shadows = (uint)o.shadows;
			shadowStrength = o.shadowStrength;
			shadowResolution = (uint)o.shadowResolution;
			shadowCustomResolution = o.shadowCustomResolution;
			shadowBias = o.shadowBias;
			shadowNormalBias = o.shadowNormalBias;
			shadowNearPlane = o.shadowNearPlane;
			range = o.range;
			spotAngle = o.spotAngle;
			cookieSize = o.cookieSize;
			cookie = o.cookie.GetMappedInstanceID();
			flare = o.flare.GetMappedInstanceID();
			renderMode = (uint)o.renderMode;
			alreadyLightmapped = o.alreadyLightmapped;
			cullingMask = o.cullingMask;

		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			AddDependency(cookie, dependencies, objects, allowNulls);
			AddDependency(flare, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.Light o = (UnityEngine.Light)obj;
			AddDependency(o.cookie, dependencies);
			AddDependency(o.flare, dependencies);
		}

		public uint type;

		public UnityEngine.Color color;

		public float colorTemperature;

		public float intensity;

		public float bounceIntensity;

		public uint shadows;

		public float shadowStrength;

		public uint shadowResolution;

		public int shadowCustomResolution;

		public float shadowBias;

		public float shadowNormalBias;

		public float shadowNearPlane;

		public float range;

		public float spotAngle;

		public float cookieSize;

		public long cookie;

		public long flare;

		public uint renderMode;

		public bool alreadyLightmapped;

		public int cullingMask;


	}
}

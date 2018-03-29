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
	public class PersistentReflectionProbe : Battlehub.RTSaveLoad.PersistentObjects.PersistentBehaviour
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.ReflectionProbe o = (UnityEngine.ReflectionProbe)obj;
			o.hdr = hdr;
			o.size = size;
			o.center = center;
			o.nearClipPlane = nearClipPlane;
			o.farClipPlane = farClipPlane;
			o.shadowDistance = shadowDistance;
			o.resolution = resolution;
			o.cullingMask = cullingMask;
			o.clearFlags = (UnityEngine.Rendering.ReflectionProbeClearFlags)clearFlags;
			o.backgroundColor = backgroundColor;
			o.intensity = intensity;
			o.blendDistance = blendDistance;
			o.boxProjection = boxProjection;
			o.mode = (UnityEngine.Rendering.ReflectionProbeMode)mode;
			o.importance = importance;
			o.refreshMode = (UnityEngine.Rendering.ReflectionProbeRefreshMode)refreshMode;
			o.timeSlicingMode = (UnityEngine.Rendering.ReflectionProbeTimeSlicingMode)timeSlicingMode;
			o.bakedTexture = (UnityEngine.Texture)objects.Get(bakedTexture);
			o.customBakedTexture = (UnityEngine.Texture)objects.Get(customBakedTexture);
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.ReflectionProbe o = (UnityEngine.ReflectionProbe)obj;
			hdr = o.hdr;
			size = o.size;
			center = o.center;
			nearClipPlane = o.nearClipPlane;
			farClipPlane = o.farClipPlane;
			shadowDistance = o.shadowDistance;
			resolution = o.resolution;
			cullingMask = o.cullingMask;
			clearFlags = (uint)o.clearFlags;
			backgroundColor = o.backgroundColor;
			intensity = o.intensity;
			blendDistance = o.blendDistance;
			boxProjection = o.boxProjection;
			mode = (uint)o.mode;
			importance = o.importance;
			refreshMode = (uint)o.refreshMode;
			timeSlicingMode = (uint)o.timeSlicingMode;
			bakedTexture = o.bakedTexture.GetMappedInstanceID();
			customBakedTexture = o.customBakedTexture.GetMappedInstanceID();
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			AddDependency(bakedTexture, dependencies, objects, allowNulls);
			AddDependency(customBakedTexture, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.ReflectionProbe o = (UnityEngine.ReflectionProbe)obj;
			AddDependency(o.bakedTexture, dependencies);
			AddDependency(o.customBakedTexture, dependencies);
		}

		public bool hdr;

		public UnityEngine.Vector3 size;

		public UnityEngine.Vector3 center;

		public float nearClipPlane;

		public float farClipPlane;

		public float shadowDistance;

		public int resolution;

		public int cullingMask;

		public uint clearFlags;

		public UnityEngine.Color backgroundColor;

		public float intensity;

		public float blendDistance;

		public bool boxProjection;

		public uint mode;

		public int importance;

		public uint refreshMode;

		public uint timeSlicingMode;

		public long bakedTexture;

		public long customBakedTexture;

	}
}

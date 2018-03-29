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
	[ProtoInclude(1060, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentBillboardRenderer))]
	[ProtoInclude(1061, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentSkinnedMeshRenderer))]
	[ProtoInclude(1062, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentTrailRenderer))]
	[ProtoInclude(1063, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentLineRenderer))]
	[ProtoInclude(1064, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentMeshRenderer))]
	[ProtoInclude(1065, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentSpriteRenderer))]
	[ProtoInclude(1066, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentParticleSystemRenderer))]
	#endif
	[System.Serializable]
	public class PersistentRenderer : Battlehub.RTSaveLoad.PersistentObjects.PersistentComponent
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.Renderer o = (UnityEngine.Renderer)obj;
			o.enabled = enabled;
			o.shadowCastingMode = (UnityEngine.Rendering.ShadowCastingMode)shadowCastingMode;
			o.receiveShadows = receiveShadows;
			o.sharedMaterial = (UnityEngine.Material)objects.Get(sharedMaterial);
			o.sharedMaterials = Resolve<UnityEngine.Material, UnityEngine.Object>(sharedMaterials, objects);
			o.lightmapIndex = lightmapIndex;
			o.realtimeLightmapIndex = realtimeLightmapIndex;
			o.lightmapScaleOffset = lightmapScaleOffset;
			o.motionVectorGenerationMode = (UnityEngine.MotionVectorGenerationMode)motionVectorGenerationMode;
			o.realtimeLightmapScaleOffset = realtimeLightmapScaleOffset;
			o.lightProbeUsage = (UnityEngine.Rendering.LightProbeUsage)lightProbeUsage;
			o.lightProbeProxyVolumeOverride = (UnityEngine.GameObject)objects.Get(lightProbeProxyVolumeOverride);
			o.probeAnchor = (UnityEngine.Transform)objects.Get(probeAnchor);
			o.reflectionProbeUsage = (UnityEngine.Rendering.ReflectionProbeUsage)reflectionProbeUsage;
			o.sortingLayerName = sortingLayerName;
			o.sortingLayerID = sortingLayerID;
			o.sortingOrder = sortingOrder;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.Renderer o = (UnityEngine.Renderer)obj;
			enabled = o.enabled;
			shadowCastingMode = (uint)o.shadowCastingMode;
			receiveShadows = o.receiveShadows;
			sharedMaterial = o.sharedMaterial.GetMappedInstanceID();
			sharedMaterials = o.sharedMaterials.GetMappedInstanceID();
			lightmapIndex = o.lightmapIndex;
			realtimeLightmapIndex = o.realtimeLightmapIndex;
			lightmapScaleOffset = o.lightmapScaleOffset;
			motionVectorGenerationMode = (uint)o.motionVectorGenerationMode;
			realtimeLightmapScaleOffset = o.realtimeLightmapScaleOffset;
			lightProbeUsage = (uint)o.lightProbeUsage;
			lightProbeProxyVolumeOverride = o.lightProbeProxyVolumeOverride.GetMappedInstanceID();
			probeAnchor = o.probeAnchor.GetMappedInstanceID();
			reflectionProbeUsage = (uint)o.reflectionProbeUsage;
			sortingLayerName = o.sortingLayerName;
			sortingLayerID = o.sortingLayerID;
			sortingOrder = o.sortingOrder;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			AddDependency(sharedMaterial, dependencies, objects, allowNulls);
			AddDependencies(sharedMaterials, dependencies, objects, allowNulls);
			AddDependency(lightProbeProxyVolumeOverride, dependencies, objects, allowNulls);
			AddDependency(probeAnchor, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.Renderer o = (UnityEngine.Renderer)obj;
			AddDependency(o.sharedMaterial, dependencies);
			AddDependencies(o.sharedMaterials, dependencies);
			AddDependency(o.lightProbeProxyVolumeOverride, dependencies);
			AddDependency(o.probeAnchor, dependencies);
		}

		public bool enabled;

		public uint shadowCastingMode;

		public bool receiveShadows;

		public long sharedMaterial;

		public long[] sharedMaterials;

		public int lightmapIndex;

		public int realtimeLightmapIndex;

		public UnityEngine.Vector4 lightmapScaleOffset;

		public uint motionVectorGenerationMode;

		public UnityEngine.Vector4 realtimeLightmapScaleOffset;

		public uint lightProbeUsage;

		public long lightProbeProxyVolumeOverride;

		public long probeAnchor;

		public uint reflectionProbeUsage;

		public string sortingLayerName;

		public int sortingLayerID;

		public int sortingOrder;

	}
}

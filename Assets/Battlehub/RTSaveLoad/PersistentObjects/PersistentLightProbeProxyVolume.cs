#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif
/*This is auto-generated file. Tools->Runtime SaveLoad->Create Persistent Objects 
If you want prevent overwriting, drag this file to another folder.*/

namespace Battlehub.RTSaveLoad.PersistentObjects
{
	#if RT_USE_PROTOBUF
	[ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
	#endif
	[System.Serializable]
	public class PersistentLightProbeProxyVolume : Battlehub.RTSaveLoad.PersistentObjects.PersistentBehaviour
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.LightProbeProxyVolume o = (UnityEngine.LightProbeProxyVolume)obj;
			o.sizeCustom = sizeCustom;
			o.originCustom = originCustom;
			o.boundingBoxMode = (UnityEngine.LightProbeProxyVolume.BoundingBoxMode)boundingBoxMode;
			o.resolutionMode = (UnityEngine.LightProbeProxyVolume.ResolutionMode)resolutionMode;
			o.probePositionMode = (UnityEngine.LightProbeProxyVolume.ProbePositionMode)probePositionMode;
			o.refreshMode = (UnityEngine.LightProbeProxyVolume.RefreshMode)refreshMode;
			o.probeDensity = probeDensity;
			o.gridResolutionX = gridResolutionX;
			o.gridResolutionY = gridResolutionY;
			o.gridResolutionZ = gridResolutionZ;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.LightProbeProxyVolume o = (UnityEngine.LightProbeProxyVolume)obj;
			sizeCustom = o.sizeCustom;
			originCustom = o.originCustom;
			boundingBoxMode = (uint)o.boundingBoxMode;
			resolutionMode = (uint)o.resolutionMode;
			probePositionMode = (uint)o.probePositionMode;
			refreshMode = (uint)o.refreshMode;
			probeDensity = o.probeDensity;
			gridResolutionX = o.gridResolutionX;
			gridResolutionY = o.gridResolutionY;
			gridResolutionZ = o.gridResolutionZ;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public UnityEngine.Vector3 sizeCustom;

		public UnityEngine.Vector3 originCustom;

		public uint boundingBoxMode;

		public uint resolutionMode;

		public uint probePositionMode;

		public uint refreshMode;

		public float probeDensity;

		public int gridResolutionX;

		public int gridResolutionY;

		public int gridResolutionZ;

	}
}

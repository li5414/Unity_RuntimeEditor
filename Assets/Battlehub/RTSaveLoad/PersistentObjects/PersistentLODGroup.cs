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
	public class PersistentLODGroup : Battlehub.RTSaveLoad.PersistentObjects.PersistentComponent
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.LODGroup o = (UnityEngine.LODGroup)obj;
			o.localReferencePoint = localReferencePoint;
			o.size = size;
			o.fadeMode = (UnityEngine.LODFadeMode)fadeMode;
			o.animateCrossFading = animateCrossFading;
			o.enabled = enabled;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.LODGroup o = (UnityEngine.LODGroup)obj;
			localReferencePoint = o.localReferencePoint;
			size = o.size;
			fadeMode = (uint)o.fadeMode;
			animateCrossFading = o.animateCrossFading;
			enabled = o.enabled;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public UnityEngine.Vector3 localReferencePoint;

		public float size;

		public uint fadeMode;

		public bool animateCrossFading;

		public bool enabled;

	}
}

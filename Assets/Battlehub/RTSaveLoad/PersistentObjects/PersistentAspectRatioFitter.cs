#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif
/*This is auto-generated file. Tools->Runtime SaveLoad->Create Persistent Objects 
If you want prevent overwriting, drag this file to another folder.*/

namespace Battlehub.RTSaveLoad.PersistentObjects.UI
{
	#if RT_USE_PROTOBUF
	[ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
	#endif
	[System.Serializable]
	public class PersistentAspectRatioFitter : Battlehub.RTSaveLoad.PersistentObjects.EventSystems.PersistentUIBehaviour
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.UI.AspectRatioFitter o = (UnityEngine.UI.AspectRatioFitter)obj;
			o.aspectMode = (UnityEngine.UI.AspectRatioFitter.AspectMode)aspectMode;
			o.aspectRatio = aspectRatio;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.UI.AspectRatioFitter o = (UnityEngine.UI.AspectRatioFitter)obj;
			aspectMode = (uint)o.aspectMode;
			aspectRatio = o.aspectRatio;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public uint aspectMode;

		public float aspectRatio;

	}
}

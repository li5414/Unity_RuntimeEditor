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
	public class PersistentContentSizeFitter : Battlehub.RTSaveLoad.PersistentObjects.EventSystems.PersistentUIBehaviour
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.UI.ContentSizeFitter o = (UnityEngine.UI.ContentSizeFitter)obj;
			o.horizontalFit = (UnityEngine.UI.ContentSizeFitter.FitMode)horizontalFit;
			o.verticalFit = (UnityEngine.UI.ContentSizeFitter.FitMode)verticalFit;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.UI.ContentSizeFitter o = (UnityEngine.UI.ContentSizeFitter)obj;
			horizontalFit = (uint)o.horizontalFit;
			verticalFit = (uint)o.verticalFit;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public uint horizontalFit;

		public uint verticalFit;

	}
}

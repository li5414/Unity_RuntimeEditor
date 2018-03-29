#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif
/*This is auto-generated file. Tools->Runtime SaveLoad->Create Persistent Objects 
If you want prevent overwriting, drag this file to another folder.*/

namespace Battlehub.RTSaveLoad.PersistentObjects.Rendering
{
	#if RT_USE_PROTOBUF
	[ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
	#endif
	[System.Serializable]
	public class PersistentSortingGroup : Battlehub.RTSaveLoad.PersistentObjects.PersistentBehaviour
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.Rendering.SortingGroup o = (UnityEngine.Rendering.SortingGroup)obj;
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
			UnityEngine.Rendering.SortingGroup o = (UnityEngine.Rendering.SortingGroup)obj;
			sortingLayerName = o.sortingLayerName;
			sortingLayerID = o.sortingLayerID;
			sortingOrder = o.sortingOrder;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public string sortingLayerName;

		public int sortingLayerID;

		public int sortingOrder;

	}
}

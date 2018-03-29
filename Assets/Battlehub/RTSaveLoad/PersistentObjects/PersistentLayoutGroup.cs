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
	[ProtoInclude(1132, typeof(Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentGridLayoutGroup))]
	[ProtoInclude(1133, typeof(Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentHorizontalOrVerticalLayoutGroup))]
	#endif
	[System.Serializable]
	public class PersistentLayoutGroup : Battlehub.RTSaveLoad.PersistentObjects.EventSystems.PersistentUIBehaviour
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.UI.LayoutGroup o = (UnityEngine.UI.LayoutGroup)obj;
			o.padding = padding;
			o.childAlignment = (UnityEngine.TextAnchor)childAlignment;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.UI.LayoutGroup o = (UnityEngine.UI.LayoutGroup)obj;
			padding = o.padding;
			childAlignment = (uint)o.childAlignment;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public UnityEngine.RectOffset padding;

		public uint childAlignment;

	}
}

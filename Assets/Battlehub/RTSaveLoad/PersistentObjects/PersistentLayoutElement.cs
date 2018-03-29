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
	public class PersistentLayoutElement : Battlehub.RTSaveLoad.PersistentObjects.EventSystems.PersistentUIBehaviour
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.UI.LayoutElement o = (UnityEngine.UI.LayoutElement)obj;
			o.ignoreLayout = ignoreLayout;
			o.minWidth = minWidth;
			o.minHeight = minHeight;
			o.preferredWidth = preferredWidth;
			o.preferredHeight = preferredHeight;
			o.flexibleWidth = flexibleWidth;
			o.flexibleHeight = flexibleHeight;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.UI.LayoutElement o = (UnityEngine.UI.LayoutElement)obj;
			ignoreLayout = o.ignoreLayout;
			minWidth = o.minWidth;
			minHeight = o.minHeight;
			preferredWidth = o.preferredWidth;
			preferredHeight = o.preferredHeight;
			flexibleWidth = o.flexibleWidth;
			flexibleHeight = o.flexibleHeight;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public bool ignoreLayout;

		public float minWidth;

		public float minHeight;

		public float preferredWidth;

		public float preferredHeight;

		public float flexibleWidth;

		public float flexibleHeight;

	}
}

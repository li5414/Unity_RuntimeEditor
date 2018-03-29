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
	[ProtoInclude(1130, typeof(Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentHorizontalLayoutGroup))]
	[ProtoInclude(1131, typeof(Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentVerticalLayoutGroup))]
	#endif
	[System.Serializable]
	public class PersistentHorizontalOrVerticalLayoutGroup : Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentLayoutGroup
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.UI.HorizontalOrVerticalLayoutGroup o = (UnityEngine.UI.HorizontalOrVerticalLayoutGroup)obj;
			o.spacing = spacing;
			o.childForceExpandWidth = childForceExpandWidth;
			o.childForceExpandHeight = childForceExpandHeight;
			o.childControlWidth = childControlWidth;
			o.childControlHeight = childControlHeight;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.UI.HorizontalOrVerticalLayoutGroup o = (UnityEngine.UI.HorizontalOrVerticalLayoutGroup)obj;
			spacing = o.spacing;
			childForceExpandWidth = o.childForceExpandWidth;
			childForceExpandHeight = o.childForceExpandHeight;
			childControlWidth = o.childControlWidth;
			childControlHeight = o.childControlHeight;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public float spacing;

		public bool childForceExpandWidth;

		public bool childForceExpandHeight;

		public bool childControlWidth;

		public bool childControlHeight;

	}
}

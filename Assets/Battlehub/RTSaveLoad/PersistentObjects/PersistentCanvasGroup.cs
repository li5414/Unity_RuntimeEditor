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
	public class PersistentCanvasGroup : Battlehub.RTSaveLoad.PersistentObjects.PersistentComponent
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.CanvasGroup o = (UnityEngine.CanvasGroup)obj;
			o.alpha = alpha;
			o.interactable = interactable;
			o.blocksRaycasts = blocksRaycasts;
			o.ignoreParentGroups = ignoreParentGroups;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.CanvasGroup o = (UnityEngine.CanvasGroup)obj;
			alpha = o.alpha;
			interactable = o.interactable;
			blocksRaycasts = o.blocksRaycasts;
			ignoreParentGroups = o.ignoreParentGroups;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public float alpha;

		public bool interactable;

		public bool blocksRaycasts;

		public bool ignoreParentGroups;

	}
}

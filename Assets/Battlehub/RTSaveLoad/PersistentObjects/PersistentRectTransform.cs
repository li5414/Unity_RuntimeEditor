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
	public class PersistentRectTransform : Battlehub.RTSaveLoad.PersistentObjects.PersistentTransform
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.RectTransform o = (UnityEngine.RectTransform)obj;
			o.anchorMin = anchorMin;
			o.anchorMax = anchorMax;
			o.anchoredPosition3D = anchoredPosition3D;
			o.anchoredPosition = anchoredPosition;
			o.sizeDelta = sizeDelta;
			o.pivot = pivot;
			o.offsetMin = offsetMin;
			o.offsetMax = offsetMax;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.RectTransform o = (UnityEngine.RectTransform)obj;
			anchorMin = o.anchorMin;
			anchorMax = o.anchorMax;
			anchoredPosition3D = o.anchoredPosition3D;
			anchoredPosition = o.anchoredPosition;
			sizeDelta = o.sizeDelta;
			pivot = o.pivot;
			offsetMin = o.offsetMin;
			offsetMax = o.offsetMax;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public UnityEngine.Vector2 anchorMin;

		public UnityEngine.Vector2 anchorMax;

		public UnityEngine.Vector3 anchoredPosition3D;

		public UnityEngine.Vector2 anchoredPosition;

		public UnityEngine.Vector2 sizeDelta;

		public UnityEngine.Vector2 pivot;

		public UnityEngine.Vector2 offsetMin;

		public UnityEngine.Vector2 offsetMax;

	}
}

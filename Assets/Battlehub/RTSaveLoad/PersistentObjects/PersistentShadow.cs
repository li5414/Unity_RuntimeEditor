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
	[ProtoInclude(1136, typeof(Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentOutline))]
	#endif
	[System.Serializable]
	public class PersistentShadow : Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentBaseMeshEffect
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.UI.Shadow o = (UnityEngine.UI.Shadow)obj;
			o.effectColor = effectColor;
			o.effectDistance = effectDistance;
			o.useGraphicAlpha = useGraphicAlpha;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.UI.Shadow o = (UnityEngine.UI.Shadow)obj;
			effectColor = o.effectColor;
			effectDistance = o.effectDistance;
			useGraphicAlpha = o.useGraphicAlpha;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public UnityEngine.Color effectColor;

		public UnityEngine.Vector2 effectDistance;

		public bool useGraphicAlpha;

	}
}

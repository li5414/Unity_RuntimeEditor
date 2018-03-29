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
	public class PersistentGameObject : Battlehub.RTSaveLoad.PersistentObjects.PersistentObject
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.GameObject o = (UnityEngine.GameObject)obj;
			o.layer = layer;
			o.isStatic = isStatic;
			try { o.tag = tag; }
			catch (UnityEngine.UnityException e) { UnityEngine.Debug.LogWarning(e.Message); }
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.GameObject o = (UnityEngine.GameObject)obj;
			layer = o.layer;
			isStatic = o.isStatic;
			tag = o.tag;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public int layer;

		public bool isStatic;

		public string tag;

	}
}

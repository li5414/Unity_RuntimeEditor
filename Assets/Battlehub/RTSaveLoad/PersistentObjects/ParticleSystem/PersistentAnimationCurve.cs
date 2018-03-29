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
	public class PersistentAnimationCurve : Battlehub.RTSaveLoad.PersistentData
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.AnimationCurve o = (UnityEngine.AnimationCurve)obj;
			o.keys = Write(o.keys, keys, objects);
			o.preWrapMode = (UnityEngine.WrapMode)preWrapMode;
			o.postWrapMode = (UnityEngine.WrapMode)postWrapMode;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.AnimationCurve o = (UnityEngine.AnimationCurve)obj;
			keys = Read(keys, o.keys);
			preWrapMode = (uint)o.preWrapMode;
			postWrapMode = (uint)o.postWrapMode;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			FindDependencies(keys, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.AnimationCurve o = (UnityEngine.AnimationCurve)obj;
			GetDependencies(keys, o.keys, dependencies);
		}

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentKeyframe[] keys;

		public uint preWrapMode;

		public uint postWrapMode;

	}
}

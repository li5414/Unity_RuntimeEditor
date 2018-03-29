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
	public class PersistentAnimationClip : Battlehub.RTSaveLoad.PersistentObjects.PersistentMotion
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.AnimationClip o = (UnityEngine.AnimationClip)obj;
			o.frameRate = frameRate;
			o.wrapMode = (UnityEngine.WrapMode)wrapMode;
			o.localBounds = localBounds;
			o.legacy = legacy;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.AnimationClip o = (UnityEngine.AnimationClip)obj;
			frameRate = o.frameRate;
			wrapMode = (uint)o.wrapMode;
			localBounds = o.localBounds;
			legacy = o.legacy;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public float frameRate;

		public uint wrapMode;

		public UnityEngine.Bounds localBounds;

		public bool legacy;

	}
}

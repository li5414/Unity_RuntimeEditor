#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif
using Battlehub.RTSaveLoad;
/*This is auto-generated file. Tools->Runtime SaveLoad->Create Persistent Objects 
If you want prevent overwriting, drag this file to another folder.*/

namespace Battlehub.RTSaveLoad.PersistentObjects
{
	#if RT_USE_PROTOBUF
	[ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
	#endif
	[System.Serializable]
	public class PersistentAnimation : Battlehub.RTSaveLoad.PersistentObjects.PersistentBehaviour
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.Animation o = (UnityEngine.Animation)obj;
			o.clip = (UnityEngine.AnimationClip)objects.Get(clip);
			o.playAutomatically = playAutomatically;
			o.wrapMode = (UnityEngine.WrapMode)wrapMode;
			o.animatePhysics = animatePhysics;
			o.cullingType = (UnityEngine.AnimationCullingType)cullingType;
			o.localBounds = localBounds;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.Animation o = (UnityEngine.Animation)obj;
			clip = o.clip.GetMappedInstanceID();
			playAutomatically = o.playAutomatically;
			wrapMode = (uint)o.wrapMode;
			animatePhysics = o.animatePhysics;
			cullingType = (uint)o.cullingType;
			localBounds = o.localBounds;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			AddDependency(clip, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.Animation o = (UnityEngine.Animation)obj;
			AddDependency(o.clip, dependencies);
		}

		public long clip;

		public bool playAutomatically;

		public uint wrapMode;

		public bool animatePhysics;

		public uint cullingType;

		public UnityEngine.Bounds localBounds;

	}
}

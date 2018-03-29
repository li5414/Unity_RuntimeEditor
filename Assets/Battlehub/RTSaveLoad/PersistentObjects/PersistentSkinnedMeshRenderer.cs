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
	public class PersistentSkinnedMeshRenderer : Battlehub.RTSaveLoad.PersistentObjects.PersistentRenderer
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.SkinnedMeshRenderer o = (UnityEngine.SkinnedMeshRenderer)obj;
			o.bones = Resolve<UnityEngine.Transform, UnityEngine.Object>(bones, objects);
			o.rootBone = (UnityEngine.Transform)objects.Get(rootBone);
			o.quality = (UnityEngine.SkinQuality)quality;
			o.sharedMesh = (UnityEngine.Mesh)objects.Get(sharedMesh);
			//o.updateWhenOffscreen = updateWhenOffscreen;
			o.skinnedMotionVectors = skinnedMotionVectors;
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
			UnityEngine.SkinnedMeshRenderer o = (UnityEngine.SkinnedMeshRenderer)obj;
			bones = o.bones.GetMappedInstanceID();
			rootBone = o.rootBone.GetMappedInstanceID();
			quality = (uint)o.quality;
			sharedMesh = o.sharedMesh.GetMappedInstanceID();
			updateWhenOffscreen = o.updateWhenOffscreen;
			skinnedMotionVectors = o.skinnedMotionVectors;
			localBounds = o.localBounds;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			AddDependencies(bones, dependencies, objects, allowNulls);
			AddDependency(rootBone, dependencies, objects, allowNulls);
			AddDependency(sharedMesh, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.SkinnedMeshRenderer o = (UnityEngine.SkinnedMeshRenderer)obj;
			AddDependencies(o.bones, dependencies);
			AddDependency(o.rootBone, dependencies);
			AddDependency(o.sharedMesh, dependencies);
		}

		public long[] bones;

		public long rootBone;

		public uint quality;

		public long sharedMesh;

		public bool updateWhenOffscreen;

		public bool skinnedMotionVectors;

		public UnityEngine.Bounds localBounds;

	}
}

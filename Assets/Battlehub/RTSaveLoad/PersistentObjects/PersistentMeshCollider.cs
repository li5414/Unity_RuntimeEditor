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
	public class PersistentMeshCollider : Battlehub.RTSaveLoad.PersistentObjects.PersistentCollider
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.MeshCollider o = (UnityEngine.MeshCollider)obj;
			o.sharedMesh = (UnityEngine.Mesh)objects.Get(sharedMesh);
			o.convex = convex;
			o.inflateMesh = inflateMesh;
			o.skinWidth = skinWidth;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.MeshCollider o = (UnityEngine.MeshCollider)obj;
			sharedMesh = o.sharedMesh.GetMappedInstanceID();
			convex = o.convex;
			inflateMesh = o.inflateMesh;
			skinWidth = o.skinWidth;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			AddDependency(sharedMesh, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.MeshCollider o = (UnityEngine.MeshCollider)obj;
			AddDependency(o.sharedMesh, dependencies);
		}

		public long sharedMesh;

		public bool convex;

		public bool inflateMesh;

		public float skinWidth;

	}
}

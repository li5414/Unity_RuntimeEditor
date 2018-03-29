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
	[ProtoInclude(1087, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentCircleCollider2D))]
	[ProtoInclude(1088, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentBoxCollider2D))]
	[ProtoInclude(1089, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentEdgeCollider2D))]
	[ProtoInclude(1090, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentCapsuleCollider2D))]
	[ProtoInclude(1091, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentCompositeCollider2D))]
	[ProtoInclude(1092, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentPolygonCollider2D))]
	#endif
	[System.Serializable]
	public class PersistentCollider2D : Battlehub.RTSaveLoad.PersistentObjects.PersistentBehaviour
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.Collider2D o = (UnityEngine.Collider2D)obj;
			o.density = density;
			o.isTrigger = isTrigger;
			o.usedByEffector = usedByEffector;
			o.usedByComposite = usedByComposite;
			o.offset = offset;
			o.sharedMaterial = (UnityEngine.PhysicsMaterial2D)objects.Get(sharedMaterial);
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.Collider2D o = (UnityEngine.Collider2D)obj;
			density = o.density;
			isTrigger = o.isTrigger;
			usedByEffector = o.usedByEffector;
			usedByComposite = o.usedByComposite;
			offset = o.offset;
			sharedMaterial = o.sharedMaterial.GetMappedInstanceID();
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			AddDependency(sharedMaterial, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.Collider2D o = (UnityEngine.Collider2D)obj;
			AddDependency(o.sharedMaterial, dependencies);
		}

		public float density;

		public bool isTrigger;

		public bool usedByEffector;

		public bool usedByComposite;

		public UnityEngine.Vector2 offset;

		public long sharedMaterial;

	}
}

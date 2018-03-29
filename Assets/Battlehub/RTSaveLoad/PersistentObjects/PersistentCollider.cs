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
	[ProtoInclude(1077, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentBoxCollider))]
	[ProtoInclude(1078, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentSphereCollider))]
	[ProtoInclude(1079, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentMeshCollider))]
	[ProtoInclude(1080, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentCapsuleCollider))]
	[ProtoInclude(1081, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentCharacterController))]
	[ProtoInclude(1082, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentWheelCollider))]
	[ProtoInclude(1083, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentTerrainCollider))]
	#endif
	[System.Serializable]
	public class PersistentCollider : Battlehub.RTSaveLoad.PersistentObjects.PersistentComponent
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.Collider o = (UnityEngine.Collider)obj;
			o.enabled = enabled;
			o.isTrigger = isTrigger;
			o.contactOffset = contactOffset;
			o.sharedMaterial = (UnityEngine.PhysicMaterial)objects.Get(sharedMaterial);
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.Collider o = (UnityEngine.Collider)obj;
			enabled = o.enabled;
			isTrigger = o.isTrigger;
			contactOffset = o.contactOffset;
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
			UnityEngine.Collider o = (UnityEngine.Collider)obj;
			AddDependency(o.sharedMaterial, dependencies);
		}

		public bool enabled;

		public bool isTrigger;

		public float contactOffset;

		public long sharedMaterial;

	}
}

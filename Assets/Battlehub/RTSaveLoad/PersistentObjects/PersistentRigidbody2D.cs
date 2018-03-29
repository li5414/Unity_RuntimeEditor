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
	public class PersistentRigidbody2D : Battlehub.RTSaveLoad.PersistentObjects.PersistentComponent
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.Rigidbody2D o = (UnityEngine.Rigidbody2D)obj;
			o.position = position;
			o.rotation = rotation;
			o.velocity = velocity;
			o.angularVelocity = angularVelocity;
			o.useAutoMass = useAutoMass;
			o.mass = mass;
			o.sharedMaterial = (UnityEngine.PhysicsMaterial2D)objects.Get(sharedMaterial);
			o.centerOfMass = centerOfMass;
			o.inertia = inertia;
			o.drag = drag;
			o.angularDrag = angularDrag;
			o.gravityScale = gravityScale;
			o.bodyType = (UnityEngine.RigidbodyType2D)bodyType;
			o.useFullKinematicContacts = useFullKinematicContacts;
			o.isKinematic = isKinematic;
			o.freezeRotation = freezeRotation;
			o.constraints = (UnityEngine.RigidbodyConstraints2D)constraints;
			o.simulated = simulated;
			o.interpolation = (UnityEngine.RigidbodyInterpolation2D)interpolation;
			o.sleepMode = (UnityEngine.RigidbodySleepMode2D)sleepMode;
			o.collisionDetectionMode = (UnityEngine.CollisionDetectionMode2D)collisionDetectionMode;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.Rigidbody2D o = (UnityEngine.Rigidbody2D)obj;
			position = o.position;
			rotation = o.rotation;
			velocity = o.velocity;
			angularVelocity = o.angularVelocity;
			useAutoMass = o.useAutoMass;
			mass = o.mass;
			sharedMaterial = o.sharedMaterial.GetMappedInstanceID();
			centerOfMass = o.centerOfMass;
			inertia = o.inertia;
			drag = o.drag;
			angularDrag = o.angularDrag;
			gravityScale = o.gravityScale;
			bodyType = (uint)o.bodyType;
			useFullKinematicContacts = o.useFullKinematicContacts;
			isKinematic = o.isKinematic;
			freezeRotation = o.freezeRotation;
			constraints = (uint)o.constraints;
			simulated = o.simulated;
			interpolation = (uint)o.interpolation;
			sleepMode = (uint)o.sleepMode;
			collisionDetectionMode = (uint)o.collisionDetectionMode;
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
			UnityEngine.Rigidbody2D o = (UnityEngine.Rigidbody2D)obj;
			AddDependency(o.sharedMaterial, dependencies);
		}

		public UnityEngine.Vector2 position;

		public float rotation;

		public UnityEngine.Vector2 velocity;

		public float angularVelocity;

		public bool useAutoMass;

		public float mass;

		public long sharedMaterial;

		public UnityEngine.Vector2 centerOfMass;

		public float inertia;

		public float drag;

		public float angularDrag;

		public float gravityScale;

		public uint bodyType;

		public bool useFullKinematicContacts;

		public bool isKinematic;

		public bool freezeRotation;

		public uint constraints;

		public bool simulated;

		public uint interpolation;

		public uint sleepMode;

		public uint collisionDetectionMode;

	}
}

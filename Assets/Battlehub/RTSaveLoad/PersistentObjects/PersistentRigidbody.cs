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
	public class PersistentRigidbody : Battlehub.RTSaveLoad.PersistentObjects.PersistentComponent
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.Rigidbody o = (UnityEngine.Rigidbody)obj;
			o.velocity = velocity;
			o.angularVelocity = angularVelocity;
			o.drag = drag;
			o.angularDrag = angularDrag;
			o.mass = mass;
			o.useGravity = useGravity;
			o.maxDepenetrationVelocity = maxDepenetrationVelocity;
			o.isKinematic = isKinematic;
			o.freezeRotation = freezeRotation;
			o.constraints = (UnityEngine.RigidbodyConstraints)constraints;
			o.collisionDetectionMode = (UnityEngine.CollisionDetectionMode)collisionDetectionMode;
			o.centerOfMass = centerOfMass;
			o.detectCollisions = detectCollisions;
			o.position = position;
			o.rotation = rotation;
			o.interpolation = (UnityEngine.RigidbodyInterpolation)interpolation;
			o.solverIterations = solverIterations;
			o.solverVelocityIterations = solverVelocityIterations;
			o.sleepThreshold = sleepThreshold;
			o.maxAngularVelocity = maxAngularVelocity;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.Rigidbody o = (UnityEngine.Rigidbody)obj;
			velocity = o.velocity;
			angularVelocity = o.angularVelocity;
			drag = o.drag;
			angularDrag = o.angularDrag;
			mass = o.mass;
			useGravity = o.useGravity;
			maxDepenetrationVelocity = o.maxDepenetrationVelocity;
			isKinematic = o.isKinematic;
			freezeRotation = o.freezeRotation;
			constraints = (uint)o.constraints;
			collisionDetectionMode = (uint)o.collisionDetectionMode;
			centerOfMass = o.centerOfMass;
			detectCollisions = o.detectCollisions;
			position = o.position;
			rotation = o.rotation;
			interpolation = (uint)o.interpolation;
			solverIterations = o.solverIterations;
			solverVelocityIterations = o.solverVelocityIterations;
			sleepThreshold = o.sleepThreshold;
			maxAngularVelocity = o.maxAngularVelocity;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public UnityEngine.Vector3 velocity;

		public UnityEngine.Vector3 angularVelocity;

		public float drag;

		public float angularDrag;

		public float mass;

		public bool useGravity;

		public float maxDepenetrationVelocity;

		public bool isKinematic;

		public bool freezeRotation;

		public uint constraints;

		public uint collisionDetectionMode;

		public UnityEngine.Vector3 centerOfMass;

		public bool detectCollisions;

		public UnityEngine.Vector3 position;

		public UnityEngine.Quaternion rotation;

		public uint interpolation;

		public int solverIterations;

		public int solverVelocityIterations;

		public float sleepThreshold;

		public float maxAngularVelocity;

	}
}

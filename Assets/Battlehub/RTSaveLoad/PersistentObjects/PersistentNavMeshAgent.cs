#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif
/*This is auto-generated file. Tools->Runtime SaveLoad->Create Persistent Objects 
If you want prevent overwriting, drag this file to another folder.*/

namespace Battlehub.RTSaveLoad.PersistentObjects.AI
{
	#if RT_USE_PROTOBUF
	[ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
	#endif
	[System.Serializable]
	public class PersistentNavMeshAgent : Battlehub.RTSaveLoad.PersistentObjects.PersistentBehaviour
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.AI.NavMeshAgent o = (UnityEngine.AI.NavMeshAgent)obj;
			o.destination = destination;
			o.stoppingDistance = stoppingDistance;
			o.velocity = velocity;
			o.nextPosition = nextPosition;
			o.baseOffset = baseOffset;
			o.autoTraverseOffMeshLink = autoTraverseOffMeshLink;
			o.autoBraking = autoBraking;
			o.autoRepath = autoRepath;
			o.isStopped = isStopped;
			o.path = path;
			o.areaMask = areaMask;
			o.speed = speed;
			o.angularSpeed = angularSpeed;
			o.acceleration = acceleration;
			o.updatePosition = updatePosition;
			o.updateRotation = updateRotation;
			o.updateUpAxis = updateUpAxis;
			o.radius = radius;
			o.height = height;
			o.obstacleAvoidanceType = (UnityEngine.AI.ObstacleAvoidanceType)obstacleAvoidanceType;
			o.avoidancePriority = avoidancePriority;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.AI.NavMeshAgent o = (UnityEngine.AI.NavMeshAgent)obj;
			destination = o.destination;
			stoppingDistance = o.stoppingDistance;
			velocity = o.velocity;
			nextPosition = o.nextPosition;
			baseOffset = o.baseOffset;
			autoTraverseOffMeshLink = o.autoTraverseOffMeshLink;
			autoBraking = o.autoBraking;
			autoRepath = o.autoRepath;
			isStopped = o.isStopped;
			path = o.path;
			areaMask = o.areaMask;
			speed = o.speed;
			angularSpeed = o.angularSpeed;
			acceleration = o.acceleration;
			updatePosition = o.updatePosition;
			updateRotation = o.updateRotation;
			updateUpAxis = o.updateUpAxis;
			radius = o.radius;
			height = o.height;
			obstacleAvoidanceType = (uint)o.obstacleAvoidanceType;
			avoidancePriority = o.avoidancePriority;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public UnityEngine.Vector3 destination;

		public float stoppingDistance;

		public UnityEngine.Vector3 velocity;

		public UnityEngine.Vector3 nextPosition;

		public float baseOffset;

		public bool autoTraverseOffMeshLink;

		public bool autoBraking;

		public bool autoRepath;

		public bool isStopped;

		public UnityEngine.AI.NavMeshPath path;

		public int areaMask;

		public float speed;

		public float angularSpeed;

		public float acceleration;

		public bool updatePosition;

		public bool updateRotation;

		public bool updateUpAxis;

		public float radius;

		public float height;

		public uint obstacleAvoidanceType;

		public int avoidancePriority;

	}
}

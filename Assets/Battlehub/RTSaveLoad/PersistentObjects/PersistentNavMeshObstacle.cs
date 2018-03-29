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
	public class PersistentNavMeshObstacle : Battlehub.RTSaveLoad.PersistentObjects.PersistentBehaviour
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.AI.NavMeshObstacle o = (UnityEngine.AI.NavMeshObstacle)obj;
			o.height = height;
			o.radius = radius;
			o.velocity = velocity;
			o.carving = carving;
			o.carveOnlyStationary = carveOnlyStationary;
			o.carvingMoveThreshold = carvingMoveThreshold;
			o.carvingTimeToStationary = carvingTimeToStationary;
			o.shape = (UnityEngine.AI.NavMeshObstacleShape)shape;
			o.center = center;
			o.size = size;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.AI.NavMeshObstacle o = (UnityEngine.AI.NavMeshObstacle)obj;
			height = o.height;
			radius = o.radius;
			velocity = o.velocity;
			carving = o.carving;
			carveOnlyStationary = o.carveOnlyStationary;
			carvingMoveThreshold = o.carvingMoveThreshold;
			carvingTimeToStationary = o.carvingTimeToStationary;
			shape = (uint)o.shape;
			center = o.center;
			size = o.size;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public float height;

		public float radius;

		public UnityEngine.Vector3 velocity;

		public bool carving;

		public bool carveOnlyStationary;

		public float carvingMoveThreshold;

		public float carvingTimeToStationary;

		public uint shape;

		public UnityEngine.Vector3 center;

		public UnityEngine.Vector3 size;

	}
}

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
	public class PersistentCharacterController : Battlehub.RTSaveLoad.PersistentObjects.PersistentCollider
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.CharacterController o = (UnityEngine.CharacterController)obj;
			o.radius = radius;
			o.height = height;
			o.center = center;
			o.slopeLimit = slopeLimit;
			o.stepOffset = stepOffset;
			o.skinWidth = skinWidth;
			o.minMoveDistance = minMoveDistance;
			o.detectCollisions = detectCollisions;
			o.enableOverlapRecovery = enableOverlapRecovery;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.CharacterController o = (UnityEngine.CharacterController)obj;
			radius = o.radius;
			height = o.height;
			center = o.center;
			slopeLimit = o.slopeLimit;
			stepOffset = o.stepOffset;
			skinWidth = o.skinWidth;
			minMoveDistance = o.minMoveDistance;
			detectCollisions = o.detectCollisions;
			enableOverlapRecovery = o.enableOverlapRecovery;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public float radius;

		public float height;

		public UnityEngine.Vector3 center;

		public float slopeLimit;

		public float stepOffset;

		public float skinWidth;

		public float minMoveDistance;

		public bool detectCollisions;

		public bool enableOverlapRecovery;

	}
}

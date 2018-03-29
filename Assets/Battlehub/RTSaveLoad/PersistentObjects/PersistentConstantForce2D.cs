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
	public class PersistentConstantForce2D : Battlehub.RTSaveLoad.PersistentObjects.PersistentPhysicsUpdateBehaviour2D
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.ConstantForce2D o = (UnityEngine.ConstantForce2D)obj;
			o.force = force;
			o.relativeForce = relativeForce;
			o.torque = torque;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.ConstantForce2D o = (UnityEngine.ConstantForce2D)obj;
			force = o.force;
			relativeForce = o.relativeForce;
			torque = o.torque;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public UnityEngine.Vector2 force;

		public UnityEngine.Vector2 relativeForce;

		public float torque;

	}
}

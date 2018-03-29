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
	public class PersistentWheelCollider : Battlehub.RTSaveLoad.PersistentObjects.PersistentCollider
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.WheelCollider o = (UnityEngine.WheelCollider)obj;
			o.center = center;
			o.radius = radius;
			o.suspensionDistance = suspensionDistance;
			o.suspensionSpring = suspensionSpring;
			o.forceAppPointDistance = forceAppPointDistance;
			o.mass = mass;
			o.wheelDampingRate = wheelDampingRate;
			o.forwardFriction = forwardFriction;
			o.sidewaysFriction = sidewaysFriction;
			o.motorTorque = motorTorque;
			o.brakeTorque = brakeTorque;
			o.steerAngle = steerAngle;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.WheelCollider o = (UnityEngine.WheelCollider)obj;
			center = o.center;
			radius = o.radius;
			suspensionDistance = o.suspensionDistance;
			suspensionSpring = o.suspensionSpring;
			forceAppPointDistance = o.forceAppPointDistance;
			mass = o.mass;
			wheelDampingRate = o.wheelDampingRate;
			forwardFriction = o.forwardFriction;
			sidewaysFriction = o.sidewaysFriction;
			motorTorque = o.motorTorque;
			brakeTorque = o.brakeTorque;
			steerAngle = o.steerAngle;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public UnityEngine.Vector3 center;

		public float radius;

		public float suspensionDistance;

		public UnityEngine.JointSpring suspensionSpring;

		public float forceAppPointDistance;

		public float mass;

		public float wheelDampingRate;

		public UnityEngine.WheelFrictionCurve forwardFriction;

		public UnityEngine.WheelFrictionCurve sidewaysFriction;

		public float motorTorque;

		public float brakeTorque;

		public float steerAngle;

	}
}

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
	public class PersistentConfigurableJoint : Battlehub.RTSaveLoad.PersistentObjects.PersistentJoint
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.ConfigurableJoint o = (UnityEngine.ConfigurableJoint)obj;
			o.secondaryAxis = secondaryAxis;
			o.xMotion = (UnityEngine.ConfigurableJointMotion)xMotion;
			o.yMotion = (UnityEngine.ConfigurableJointMotion)yMotion;
			o.zMotion = (UnityEngine.ConfigurableJointMotion)zMotion;
			o.angularXMotion = (UnityEngine.ConfigurableJointMotion)angularXMotion;
			o.angularYMotion = (UnityEngine.ConfigurableJointMotion)angularYMotion;
			o.angularZMotion = (UnityEngine.ConfigurableJointMotion)angularZMotion;
			o.linearLimitSpring = linearLimitSpring;
			o.angularXLimitSpring = angularXLimitSpring;
			o.angularYZLimitSpring = angularYZLimitSpring;
			o.linearLimit = linearLimit;
			o.lowAngularXLimit = lowAngularXLimit;
			o.highAngularXLimit = highAngularXLimit;
			o.angularYLimit = angularYLimit;
			o.angularZLimit = angularZLimit;
			o.targetPosition = targetPosition;
			o.targetVelocity = targetVelocity;
			o.xDrive = xDrive;
			o.yDrive = yDrive;
			o.zDrive = zDrive;
			o.targetRotation = targetRotation;
			o.targetAngularVelocity = targetAngularVelocity;
			o.rotationDriveMode = (UnityEngine.RotationDriveMode)rotationDriveMode;
			o.angularXDrive = angularXDrive;
			o.angularYZDrive = angularYZDrive;
			o.slerpDrive = slerpDrive;
			o.projectionMode = (UnityEngine.JointProjectionMode)projectionMode;
			o.projectionDistance = projectionDistance;
			o.projectionAngle = projectionAngle;
			o.configuredInWorldSpace = configuredInWorldSpace;
			o.swapBodies = swapBodies;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.ConfigurableJoint o = (UnityEngine.ConfigurableJoint)obj;
			secondaryAxis = o.secondaryAxis;
			xMotion = (uint)o.xMotion;
			yMotion = (uint)o.yMotion;
			zMotion = (uint)o.zMotion;
			angularXMotion = (uint)o.angularXMotion;
			angularYMotion = (uint)o.angularYMotion;
			angularZMotion = (uint)o.angularZMotion;
			linearLimitSpring = o.linearLimitSpring;
			angularXLimitSpring = o.angularXLimitSpring;
			angularYZLimitSpring = o.angularYZLimitSpring;
			linearLimit = o.linearLimit;
			lowAngularXLimit = o.lowAngularXLimit;
			highAngularXLimit = o.highAngularXLimit;
			angularYLimit = o.angularYLimit;
			angularZLimit = o.angularZLimit;
			targetPosition = o.targetPosition;
			targetVelocity = o.targetVelocity;
			xDrive = o.xDrive;
			yDrive = o.yDrive;
			zDrive = o.zDrive;
			targetRotation = o.targetRotation;
			targetAngularVelocity = o.targetAngularVelocity;
			rotationDriveMode = (uint)o.rotationDriveMode;
			angularXDrive = o.angularXDrive;
			angularYZDrive = o.angularYZDrive;
			slerpDrive = o.slerpDrive;
			projectionMode = (uint)o.projectionMode;
			projectionDistance = o.projectionDistance;
			projectionAngle = o.projectionAngle;
			configuredInWorldSpace = o.configuredInWorldSpace;
			swapBodies = o.swapBodies;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public UnityEngine.Vector3 secondaryAxis;

		public uint xMotion;

		public uint yMotion;

		public uint zMotion;

		public uint angularXMotion;

		public uint angularYMotion;

		public uint angularZMotion;

		public UnityEngine.SoftJointLimitSpring linearLimitSpring;

		public UnityEngine.SoftJointLimitSpring angularXLimitSpring;

		public UnityEngine.SoftJointLimitSpring angularYZLimitSpring;

		public UnityEngine.SoftJointLimit linearLimit;

		public UnityEngine.SoftJointLimit lowAngularXLimit;

		public UnityEngine.SoftJointLimit highAngularXLimit;

		public UnityEngine.SoftJointLimit angularYLimit;

		public UnityEngine.SoftJointLimit angularZLimit;

		public UnityEngine.Vector3 targetPosition;

		public UnityEngine.Vector3 targetVelocity;

		public UnityEngine.JointDrive xDrive;

		public UnityEngine.JointDrive yDrive;

		public UnityEngine.JointDrive zDrive;

		public UnityEngine.Quaternion targetRotation;

		public UnityEngine.Vector3 targetAngularVelocity;

		public uint rotationDriveMode;

		public UnityEngine.JointDrive angularXDrive;

		public UnityEngine.JointDrive angularYZDrive;

		public UnityEngine.JointDrive slerpDrive;

		public uint projectionMode;

		public float projectionDistance;

		public float projectionAngle;

		public bool configuredInWorldSpace;

		public bool swapBodies;

	}
}

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
	public class PersistentCharacterJoint : Battlehub.RTSaveLoad.PersistentObjects.PersistentJoint
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.CharacterJoint o = (UnityEngine.CharacterJoint)obj;
			o.swingAxis = swingAxis;
			o.twistLimitSpring = twistLimitSpring;
			o.swingLimitSpring = swingLimitSpring;
			o.lowTwistLimit = lowTwistLimit;
			o.highTwistLimit = highTwistLimit;
			o.swing1Limit = swing1Limit;
			o.swing2Limit = swing2Limit;
			o.enableProjection = enableProjection;
			o.projectionDistance = projectionDistance;
			o.projectionAngle = projectionAngle;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.CharacterJoint o = (UnityEngine.CharacterJoint)obj;
			swingAxis = o.swingAxis;
			twistLimitSpring = o.twistLimitSpring;
			swingLimitSpring = o.swingLimitSpring;
			lowTwistLimit = o.lowTwistLimit;
			highTwistLimit = o.highTwistLimit;
			swing1Limit = o.swing1Limit;
			swing2Limit = o.swing2Limit;
			enableProjection = o.enableProjection;
			projectionDistance = o.projectionDistance;
			projectionAngle = o.projectionAngle;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public UnityEngine.Vector3 swingAxis;

		public UnityEngine.SoftJointLimitSpring twistLimitSpring;

		public UnityEngine.SoftJointLimitSpring swingLimitSpring;

		public UnityEngine.SoftJointLimit lowTwistLimit;

		public UnityEngine.SoftJointLimit highTwistLimit;

		public UnityEngine.SoftJointLimit swing1Limit;

		public UnityEngine.SoftJointLimit swing2Limit;

		public bool enableProjection;

		public float projectionDistance;

		public float projectionAngle;

	}
}

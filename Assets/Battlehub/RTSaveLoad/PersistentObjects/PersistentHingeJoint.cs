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
	public class PersistentHingeJoint : Battlehub.RTSaveLoad.PersistentObjects.PersistentJoint
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.HingeJoint o = (UnityEngine.HingeJoint)obj;
			o.motor = motor;
			o.limits = limits;
			o.spring = spring;
			o.useMotor = useMotor;
			o.useLimits = useLimits;
			o.useSpring = useSpring;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.HingeJoint o = (UnityEngine.HingeJoint)obj;
			motor = o.motor;
			limits = o.limits;
			spring = o.spring;
			useMotor = o.useMotor;
			useLimits = o.useLimits;
			useSpring = o.useSpring;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public UnityEngine.JointMotor motor;

		public UnityEngine.JointLimits limits;

		public UnityEngine.JointSpring spring;

		public bool useMotor;

		public bool useLimits;

		public bool useSpring;

	}
}

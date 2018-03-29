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
	public class PersistentHingeJoint2D : Battlehub.RTSaveLoad.PersistentObjects.PersistentAnchoredJoint2D
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.HingeJoint2D o = (UnityEngine.HingeJoint2D)obj;
			o.useMotor = useMotor;
			o.useLimits = useLimits;
			o.motor = motor;
			o.limits = limits;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.HingeJoint2D o = (UnityEngine.HingeJoint2D)obj;
			useMotor = o.useMotor;
			useLimits = o.useLimits;
			motor = o.motor;
			limits = o.limits;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public bool useMotor;

		public bool useLimits;

		public UnityEngine.JointMotor2D motor;

		public UnityEngine.JointAngleLimits2D limits;

	}
}

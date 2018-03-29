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
	public class PersistentTargetJoint2D : Battlehub.RTSaveLoad.PersistentObjects.PersistentJoint2D
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.TargetJoint2D o = (UnityEngine.TargetJoint2D)obj;
			o.anchor = anchor;
			o.target = target;
			o.autoConfigureTarget = autoConfigureTarget;
			o.maxForce = maxForce;
			o.dampingRatio = dampingRatio;
			o.frequency = frequency;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.TargetJoint2D o = (UnityEngine.TargetJoint2D)obj;
			anchor = o.anchor;
			target = o.target;
			autoConfigureTarget = o.autoConfigureTarget;
			maxForce = o.maxForce;
			dampingRatio = o.dampingRatio;
			frequency = o.frequency;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public UnityEngine.Vector2 anchor;

		public UnityEngine.Vector2 target;

		public bool autoConfigureTarget;

		public float maxForce;

		public float dampingRatio;

		public float frequency;

	}
}

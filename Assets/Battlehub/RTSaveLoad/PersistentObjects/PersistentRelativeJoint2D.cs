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
	public class PersistentRelativeJoint2D : Battlehub.RTSaveLoad.PersistentObjects.PersistentJoint2D
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.RelativeJoint2D o = (UnityEngine.RelativeJoint2D)obj;
			o.maxForce = maxForce;
			o.maxTorque = maxTorque;
			o.correctionScale = correctionScale;
			o.autoConfigureOffset = autoConfigureOffset;
			o.linearOffset = linearOffset;
			o.angularOffset = angularOffset;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.RelativeJoint2D o = (UnityEngine.RelativeJoint2D)obj;
			maxForce = o.maxForce;
			maxTorque = o.maxTorque;
			correctionScale = o.correctionScale;
			autoConfigureOffset = o.autoConfigureOffset;
			linearOffset = o.linearOffset;
			angularOffset = o.angularOffset;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public float maxForce;

		public float maxTorque;

		public float correctionScale;

		public bool autoConfigureOffset;

		public UnityEngine.Vector2 linearOffset;

		public float angularOffset;

	}
}

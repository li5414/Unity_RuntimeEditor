#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif
using Battlehub.RTSaveLoad;
/*This is auto-generated file. Tools->Runtime SaveLoad->Create Persistent Objects 
If you want prevent overwriting, drag this file to another folder.*/

namespace Battlehub.RTSaveLoad.PersistentObjects
{
	#if RT_USE_PROTOBUF
	[ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
	[ProtoInclude(1084, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentAnchoredJoint2D))]
	[ProtoInclude(1085, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentRelativeJoint2D))]
	[ProtoInclude(1086, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentTargetJoint2D))]
	#endif
	[System.Serializable]
	public class PersistentJoint2D : Battlehub.RTSaveLoad.PersistentObjects.PersistentBehaviour
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.Joint2D o = (UnityEngine.Joint2D)obj;
			o.connectedBody = (UnityEngine.Rigidbody2D)objects.Get(connectedBody);
			o.enableCollision = enableCollision;
			o.breakForce = breakForce;
			o.breakTorque = breakTorque;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.Joint2D o = (UnityEngine.Joint2D)obj;
			connectedBody = o.connectedBody.GetMappedInstanceID();
			enableCollision = o.enableCollision;
			breakForce = o.breakForce;
			breakTorque = o.breakTorque;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			AddDependency(connectedBody, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.Joint2D o = (UnityEngine.Joint2D)obj;
			AddDependency(o.connectedBody, dependencies);
		}

		public long connectedBody;

		public bool enableCollision;

		public float breakForce;

		public float breakTorque;

	}
}

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
	[ProtoInclude(1072, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentHingeJoint))]
	[ProtoInclude(1073, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentSpringJoint))]
	[ProtoInclude(1074, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentFixedJoint))]
	[ProtoInclude(1075, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentCharacterJoint))]
	[ProtoInclude(1076, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentConfigurableJoint))]
	#endif
	[System.Serializable]
	public class PersistentJoint : Battlehub.RTSaveLoad.PersistentObjects.PersistentComponent
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.Joint o = (UnityEngine.Joint)obj;
			o.connectedBody = (UnityEngine.Rigidbody)objects.Get(connectedBody);
			o.axis = axis;
			o.anchor = anchor;
			o.connectedAnchor = connectedAnchor;
			o.autoConfigureConnectedAnchor = autoConfigureConnectedAnchor;
			o.breakForce = breakForce;
			o.breakTorque = breakTorque;
			o.enableCollision = enableCollision;
			o.enablePreprocessing = enablePreprocessing;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.Joint o = (UnityEngine.Joint)obj;
			connectedBody = o.connectedBody.GetMappedInstanceID();
			axis = o.axis;
			anchor = o.anchor;
			connectedAnchor = o.connectedAnchor;
			autoConfigureConnectedAnchor = o.autoConfigureConnectedAnchor;
			breakForce = o.breakForce;
			breakTorque = o.breakTorque;
			enableCollision = o.enableCollision;
			enablePreprocessing = o.enablePreprocessing;
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
			UnityEngine.Joint o = (UnityEngine.Joint)obj;
			AddDependency(o.connectedBody, dependencies);
		}

		public long connectedBody;

		public UnityEngine.Vector3 axis;

		public UnityEngine.Vector3 anchor;

		public UnityEngine.Vector3 connectedAnchor;

		public bool autoConfigureConnectedAnchor;

		public float breakForce;

		public float breakTorque;

		public bool enableCollision;

		public bool enablePreprocessing;

	}
}

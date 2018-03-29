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
	[ProtoInclude(1093, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentSpringJoint2D))]
	[ProtoInclude(1094, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentDistanceJoint2D))]
	[ProtoInclude(1095, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentFrictionJoint2D))]
	[ProtoInclude(1096, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentHingeJoint2D))]
	[ProtoInclude(1097, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentSliderJoint2D))]
	[ProtoInclude(1098, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentFixedJoint2D))]
	[ProtoInclude(1099, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentWheelJoint2D))]
	#endif
	[System.Serializable]
	public class PersistentAnchoredJoint2D : Battlehub.RTSaveLoad.PersistentObjects.PersistentJoint2D
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.AnchoredJoint2D o = (UnityEngine.AnchoredJoint2D)obj;
			o.anchor = anchor;
			o.connectedAnchor = connectedAnchor;
			o.autoConfigureConnectedAnchor = autoConfigureConnectedAnchor;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.AnchoredJoint2D o = (UnityEngine.AnchoredJoint2D)obj;
			anchor = o.anchor;
			connectedAnchor = o.connectedAnchor;
			autoConfigureConnectedAnchor = o.autoConfigureConnectedAnchor;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public UnityEngine.Vector2 anchor;

		public UnityEngine.Vector2 connectedAnchor;

		public bool autoConfigureConnectedAnchor;

	}
}

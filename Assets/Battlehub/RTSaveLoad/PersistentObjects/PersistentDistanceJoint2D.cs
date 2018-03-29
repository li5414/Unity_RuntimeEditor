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
	public class PersistentDistanceJoint2D : Battlehub.RTSaveLoad.PersistentObjects.PersistentAnchoredJoint2D
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.DistanceJoint2D o = (UnityEngine.DistanceJoint2D)obj;
			o.autoConfigureDistance = autoConfigureDistance;
			o.distance = distance;
			o.maxDistanceOnly = maxDistanceOnly;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.DistanceJoint2D o = (UnityEngine.DistanceJoint2D)obj;
			autoConfigureDistance = o.autoConfigureDistance;
			distance = o.distance;
			maxDistanceOnly = o.maxDistanceOnly;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public bool autoConfigureDistance;

		public float distance;

		public bool maxDistanceOnly;

	}
}

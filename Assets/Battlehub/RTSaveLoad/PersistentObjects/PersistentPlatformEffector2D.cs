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
	public class PersistentPlatformEffector2D : Battlehub.RTSaveLoad.PersistentObjects.PersistentEffector2D
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.PlatformEffector2D o = (UnityEngine.PlatformEffector2D)obj;
			o.useOneWay = useOneWay;
			o.useOneWayGrouping = useOneWayGrouping;
			o.useSideFriction = useSideFriction;
			o.useSideBounce = useSideBounce;
			o.surfaceArc = surfaceArc;
			o.sideArc = sideArc;
			o.rotationalOffset = rotationalOffset;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.PlatformEffector2D o = (UnityEngine.PlatformEffector2D)obj;
			useOneWay = o.useOneWay;
			useOneWayGrouping = o.useOneWayGrouping;
			useSideFriction = o.useSideFriction;
			useSideBounce = o.useSideBounce;
			surfaceArc = o.surfaceArc;
			sideArc = o.sideArc;
			rotationalOffset = o.rotationalOffset;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public bool useOneWay;

		public bool useOneWayGrouping;

		public bool useSideFriction;

		public bool useSideBounce;

		public float surfaceArc;

		public float sideArc;

		public float rotationalOffset;

	}
}

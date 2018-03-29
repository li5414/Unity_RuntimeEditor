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
	public class PersistentBuoyancyEffector2D : Battlehub.RTSaveLoad.PersistentObjects.PersistentEffector2D
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.BuoyancyEffector2D o = (UnityEngine.BuoyancyEffector2D)obj;
			o.surfaceLevel = surfaceLevel;
			o.density = density;
			o.linearDrag = linearDrag;
			o.angularDrag = angularDrag;
			o.flowAngle = flowAngle;
			o.flowMagnitude = flowMagnitude;
			o.flowVariation = flowVariation;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.BuoyancyEffector2D o = (UnityEngine.BuoyancyEffector2D)obj;
			surfaceLevel = o.surfaceLevel;
			density = o.density;
			linearDrag = o.linearDrag;
			angularDrag = o.angularDrag;
			flowAngle = o.flowAngle;
			flowMagnitude = o.flowMagnitude;
			flowVariation = o.flowVariation;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public float surfaceLevel;

		public float density;

		public float linearDrag;

		public float angularDrag;

		public float flowAngle;

		public float flowMagnitude;

		public float flowVariation;

	}
}

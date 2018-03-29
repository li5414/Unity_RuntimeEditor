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
	public class PersistentAreaEffector2D : Battlehub.RTSaveLoad.PersistentObjects.PersistentEffector2D
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.AreaEffector2D o = (UnityEngine.AreaEffector2D)obj;
			o.forceAngle = forceAngle;
			o.useGlobalAngle = useGlobalAngle;
			o.forceMagnitude = forceMagnitude;
			o.forceVariation = forceVariation;
			o.drag = drag;
			o.angularDrag = angularDrag;
			o.forceTarget = (UnityEngine.EffectorSelection2D)forceTarget;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.AreaEffector2D o = (UnityEngine.AreaEffector2D)obj;
			forceAngle = o.forceAngle;
			useGlobalAngle = o.useGlobalAngle;
			forceMagnitude = o.forceMagnitude;
			forceVariation = o.forceVariation;
			drag = o.drag;
			angularDrag = o.angularDrag;
			forceTarget = (uint)o.forceTarget;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public float forceAngle;

		public bool useGlobalAngle;

		public float forceMagnitude;

		public float forceVariation;

		public float drag;

		public float angularDrag;

		public uint forceTarget;

	}
}

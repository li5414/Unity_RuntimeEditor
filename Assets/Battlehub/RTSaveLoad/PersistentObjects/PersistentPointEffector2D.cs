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
	public class PersistentPointEffector2D : Battlehub.RTSaveLoad.PersistentObjects.PersistentEffector2D
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.PointEffector2D o = (UnityEngine.PointEffector2D)obj;
			o.forceMagnitude = forceMagnitude;
			o.forceVariation = forceVariation;
			o.distanceScale = distanceScale;
			o.drag = drag;
			o.angularDrag = angularDrag;
			o.forceSource = (UnityEngine.EffectorSelection2D)forceSource;
			o.forceTarget = (UnityEngine.EffectorSelection2D)forceTarget;
			o.forceMode = (UnityEngine.EffectorForceMode2D)forceMode;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.PointEffector2D o = (UnityEngine.PointEffector2D)obj;
			forceMagnitude = o.forceMagnitude;
			forceVariation = o.forceVariation;
			distanceScale = o.distanceScale;
			drag = o.drag;
			angularDrag = o.angularDrag;
			forceSource = (uint)o.forceSource;
			forceTarget = (uint)o.forceTarget;
			forceMode = (uint)o.forceMode;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public float forceMagnitude;

		public float forceVariation;

		public float distanceScale;

		public float drag;

		public float angularDrag;

		public uint forceSource;

		public uint forceTarget;

		public uint forceMode;

	}
}

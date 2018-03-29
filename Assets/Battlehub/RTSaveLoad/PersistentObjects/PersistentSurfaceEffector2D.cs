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
	public class PersistentSurfaceEffector2D : Battlehub.RTSaveLoad.PersistentObjects.PersistentEffector2D
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.SurfaceEffector2D o = (UnityEngine.SurfaceEffector2D)obj;
			o.speed = speed;
			o.speedVariation = speedVariation;
			o.forceScale = forceScale;
			o.useContactForce = useContactForce;
			o.useFriction = useFriction;
			o.useBounce = useBounce;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.SurfaceEffector2D o = (UnityEngine.SurfaceEffector2D)obj;
			speed = o.speed;
			speedVariation = o.speedVariation;
			forceScale = o.forceScale;
			useContactForce = o.useContactForce;
			useFriction = o.useFriction;
			useBounce = o.useBounce;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public float speed;

		public float speedVariation;

		public float forceScale;

		public bool useContactForce;

		public bool useFriction;

		public bool useBounce;

	}
}

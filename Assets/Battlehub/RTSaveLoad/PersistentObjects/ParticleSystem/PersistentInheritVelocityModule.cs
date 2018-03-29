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
	public class PersistentInheritVelocityModule : Battlehub.RTSaveLoad.PersistentData
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.ParticleSystem.InheritVelocityModule o = (UnityEngine.ParticleSystem.InheritVelocityModule)obj;
			o.enabled = enabled;
			o.mode = (UnityEngine.ParticleSystemInheritVelocityMode)mode;
			o.curve = Write(o.curve, curve, objects);
			o.curveMultiplier = curveMultiplier;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.ParticleSystem.InheritVelocityModule o = (UnityEngine.ParticleSystem.InheritVelocityModule)obj;
			enabled = o.enabled;
			mode = (uint)o.mode;
			curve = Read(curve, o.curve);
			curveMultiplier = o.curveMultiplier;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			FindDependencies(curve, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.ParticleSystem.InheritVelocityModule o = (UnityEngine.ParticleSystem.InheritVelocityModule)obj;
			GetDependencies(curve, o.curve, dependencies);
		}

		public bool enabled;

		public uint mode;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve curve;

		public float curveMultiplier;

	}
}

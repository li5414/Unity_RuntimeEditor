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
	public class PersistentVelocityOverLifetimeModule : Battlehub.RTSaveLoad.PersistentData
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.ParticleSystem.VelocityOverLifetimeModule o = (UnityEngine.ParticleSystem.VelocityOverLifetimeModule)obj;
			o.enabled = enabled;
			o.x = Write(o.x, x, objects);
			o.y = Write(o.y, y, objects);
			o.z = Write(o.z, z, objects);
			o.xMultiplier = xMultiplier;
			o.yMultiplier = yMultiplier;
			o.zMultiplier = zMultiplier;
			o.space = (UnityEngine.ParticleSystemSimulationSpace)space;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.ParticleSystem.VelocityOverLifetimeModule o = (UnityEngine.ParticleSystem.VelocityOverLifetimeModule)obj;
			enabled = o.enabled;
			x = Read(x, o.x);
			y = Read(y, o.y);
			z = Read(z, o.z);
			xMultiplier = o.xMultiplier;
			yMultiplier = o.yMultiplier;
			zMultiplier = o.zMultiplier;
			space = (uint)o.space;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			FindDependencies(x, dependencies, objects, allowNulls);
			FindDependencies(y, dependencies, objects, allowNulls);
			FindDependencies(z, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.ParticleSystem.VelocityOverLifetimeModule o = (UnityEngine.ParticleSystem.VelocityOverLifetimeModule)obj;
			GetDependencies(x, o.x, dependencies);
			GetDependencies(y, o.y, dependencies);
			GetDependencies(z, o.z, dependencies);
		}

		public bool enabled;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve x;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve y;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve z;

		public float xMultiplier;

		public float yMultiplier;

		public float zMultiplier;

		public uint space;

	}
}

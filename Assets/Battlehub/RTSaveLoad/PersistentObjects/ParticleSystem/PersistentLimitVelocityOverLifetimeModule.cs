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
	public class PersistentLimitVelocityOverLifetimeModule : Battlehub.RTSaveLoad.PersistentData
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.ParticleSystem.LimitVelocityOverLifetimeModule o = (UnityEngine.ParticleSystem.LimitVelocityOverLifetimeModule)obj;
			o.enabled = enabled;
			o.limitX = Write(o.limitX, limitX, objects);
			o.limitXMultiplier = limitXMultiplier;
			o.limitY = Write(o.limitY, limitY, objects);
			o.limitYMultiplier = limitYMultiplier;
			o.limitZ = Write(o.limitZ, limitZ, objects);
			o.limitZMultiplier = limitZMultiplier;
			o.limit = Write(o.limit, limit, objects);
			o.limitMultiplier = limitMultiplier;
			o.dampen = dampen;
			o.separateAxes = separateAxes;
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
			UnityEngine.ParticleSystem.LimitVelocityOverLifetimeModule o = (UnityEngine.ParticleSystem.LimitVelocityOverLifetimeModule)obj;
			enabled = o.enabled;
			limitX = Read(limitX, o.limitX);
			limitXMultiplier = o.limitXMultiplier;
			limitY = Read(limitY, o.limitY);
			limitYMultiplier = o.limitYMultiplier;
			limitZ = Read(limitZ, o.limitZ);
			limitZMultiplier = o.limitZMultiplier;
			limit = Read(limit, o.limit);
			limitMultiplier = o.limitMultiplier;
			dampen = o.dampen;
			separateAxes = o.separateAxes;
			space = (uint)o.space;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			FindDependencies(limitX, dependencies, objects, allowNulls);
			FindDependencies(limitY, dependencies, objects, allowNulls);
			FindDependencies(limitZ, dependencies, objects, allowNulls);
			FindDependencies(limit, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.ParticleSystem.LimitVelocityOverLifetimeModule o = (UnityEngine.ParticleSystem.LimitVelocityOverLifetimeModule)obj;
			GetDependencies(limitX, o.limitX, dependencies);
			GetDependencies(limitY, o.limitY, dependencies);
			GetDependencies(limitZ, o.limitZ, dependencies);
			GetDependencies(limit, o.limit, dependencies);
		}

		public bool enabled;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve limitX;

		public float limitXMultiplier;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve limitY;

		public float limitYMultiplier;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve limitZ;

		public float limitZMultiplier;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve limit;

		public float limitMultiplier;

		public float dampen;

		public bool separateAxes;

		public uint space;

	}
}

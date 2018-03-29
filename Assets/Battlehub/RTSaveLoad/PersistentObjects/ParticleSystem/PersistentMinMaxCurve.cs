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
	public class PersistentMinMaxCurve : Battlehub.RTSaveLoad.PersistentData
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.ParticleSystem.MinMaxCurve o = (UnityEngine.ParticleSystem.MinMaxCurve)obj;
			o.mode = (UnityEngine.ParticleSystemCurveMode)mode;
			o.curveMultiplier = curveMultiplier;
			o.curveMax = Write(o.curveMax, curveMax, objects);
			o.curveMin = Write(o.curveMin, curveMin, objects);
			o.constantMax = constantMax;
			o.constantMin = constantMin;
			o.constant = constant;
			o.curve = Write(o.curve, curve, objects);
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.ParticleSystem.MinMaxCurve o = (UnityEngine.ParticleSystem.MinMaxCurve)obj;
			mode = (uint)o.mode;
			curveMultiplier = o.curveMultiplier;
			curveMax = Read(curveMax, o.curveMax);
			curveMin = Read(curveMin, o.curveMin);
			constantMax = o.constantMax;
			constantMin = o.constantMin;
			constant = o.constant;
			curve = Read(curve, o.curve);
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			FindDependencies(curveMax, dependencies, objects, allowNulls);
			FindDependencies(curveMin, dependencies, objects, allowNulls);
			FindDependencies(curve, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.ParticleSystem.MinMaxCurve o = (UnityEngine.ParticleSystem.MinMaxCurve)obj;
			GetDependencies(curveMax, o.curveMax, dependencies);
			GetDependencies(curveMin, o.curveMin, dependencies);
			GetDependencies(curve, o.curve, dependencies);
		}

		public uint mode;

		public float curveMultiplier;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentAnimationCurve curveMax;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentAnimationCurve curveMin;

		public float constantMax;

		public float constantMin;

		public float constant;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentAnimationCurve curve;

	}
}

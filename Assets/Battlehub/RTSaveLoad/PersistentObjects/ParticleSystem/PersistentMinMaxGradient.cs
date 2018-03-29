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
	public class PersistentMinMaxGradient : Battlehub.RTSaveLoad.PersistentData
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.ParticleSystem.MinMaxGradient o = (UnityEngine.ParticleSystem.MinMaxGradient)obj;
			o.mode = (UnityEngine.ParticleSystemGradientMode)mode;
			o.gradientMax = Write(o.gradientMax, gradientMax, objects);
			o.gradientMin = Write(o.gradientMin, gradientMin, objects);
			o.colorMax = colorMax;
			o.colorMin = colorMin;
			o.color = color;
			o.gradient = Write(o.gradient, gradient, objects);
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.ParticleSystem.MinMaxGradient o = (UnityEngine.ParticleSystem.MinMaxGradient)obj;
			mode = (uint)o.mode;
			gradientMax = Read(gradientMax, o.gradientMax);
			gradientMin = Read(gradientMin, o.gradientMin);
			colorMax = o.colorMax;
			colorMin = o.colorMin;
			color = o.color;
			gradient = Read(gradient, o.gradient);
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			FindDependencies(gradientMax, dependencies, objects, allowNulls);
			FindDependencies(gradientMin, dependencies, objects, allowNulls);
			FindDependencies(gradient, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.ParticleSystem.MinMaxGradient o = (UnityEngine.ParticleSystem.MinMaxGradient)obj;
			GetDependencies(gradientMax, o.gradientMax, dependencies);
			GetDependencies(gradientMin, o.gradientMin, dependencies);
			GetDependencies(gradient, o.gradient, dependencies);
		}

		public uint mode;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentGradient gradientMax;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentGradient gradientMin;

		public UnityEngine.Color colorMax;

		public UnityEngine.Color colorMin;

		public UnityEngine.Color color;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentGradient gradient;

	}
}

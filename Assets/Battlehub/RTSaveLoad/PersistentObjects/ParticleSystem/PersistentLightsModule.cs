#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif
using Battlehub.RTSaveLoad;
/*This is auto-generated file. Tools->Runtime SaveLoad->Create Persistent Objects 
If you want prevent overwriting, drag this file to another folder.*/

namespace Battlehub.RTSaveLoad.PersistentObjects
{
	#if RT_USE_PROTOBUF
	[ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
	#endif
	[System.Serializable]
	public class PersistentLightsModule : Battlehub.RTSaveLoad.PersistentData
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.ParticleSystem.LightsModule o = (UnityEngine.ParticleSystem.LightsModule)obj;
			o.enabled = enabled;
			o.ratio = ratio;
			o.useRandomDistribution = useRandomDistribution;
			o.light = (UnityEngine.Light)objects.Get(light);
			o.useParticleColor = useParticleColor;
			o.sizeAffectsRange = sizeAffectsRange;
			o.alphaAffectsIntensity = alphaAffectsIntensity;
			o.range = Write(o.range, range, objects);
			o.rangeMultiplier = rangeMultiplier;
			o.intensity = Write(o.intensity, intensity, objects);
			o.intensityMultiplier = intensityMultiplier;
			o.maxLights = maxLights;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.ParticleSystem.LightsModule o = (UnityEngine.ParticleSystem.LightsModule)obj;
			enabled = o.enabled;
			ratio = o.ratio;
			useRandomDistribution = o.useRandomDistribution;
			light = o.light.GetMappedInstanceID();
			useParticleColor = o.useParticleColor;
			sizeAffectsRange = o.sizeAffectsRange;
			alphaAffectsIntensity = o.alphaAffectsIntensity;
			range = Read(range, o.range);
			rangeMultiplier = o.rangeMultiplier;
			intensity = Read(intensity, o.intensity);
			intensityMultiplier = o.intensityMultiplier;
			maxLights = o.maxLights;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			AddDependency(light, dependencies, objects, allowNulls);
			FindDependencies(range, dependencies, objects, allowNulls);
			FindDependencies(intensity, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.ParticleSystem.LightsModule o = (UnityEngine.ParticleSystem.LightsModule)obj;
			AddDependency(o.light, dependencies);
			GetDependencies(range, o.range, dependencies);
			GetDependencies(intensity, o.intensity, dependencies);
		}

		public bool enabled;

		public float ratio;

		public bool useRandomDistribution;

		public long light;

		public bool useParticleColor;

		public bool sizeAffectsRange;

		public bool alphaAffectsIntensity;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve range;

		public float rangeMultiplier;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve intensity;

		public float intensityMultiplier;

		public int maxLights;

	}
}

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
	public class PersistentNoiseModule : Battlehub.RTSaveLoad.PersistentData
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.ParticleSystem.NoiseModule o = (UnityEngine.ParticleSystem.NoiseModule)obj;
			o.enabled = enabled;
			o.separateAxes = separateAxes;
			o.strength = Write(o.strength, strength, objects);
			o.strengthMultiplier = strengthMultiplier;
			o.strengthX = Write(o.strengthX, strengthX, objects);
			o.strengthXMultiplier = strengthXMultiplier;
			o.strengthY = Write(o.strengthY, strengthY, objects);
			o.strengthYMultiplier = strengthYMultiplier;
			o.strengthZ = Write(o.strengthZ, strengthZ, objects);
			o.strengthZMultiplier = strengthZMultiplier;
			o.frequency = frequency;
			o.damping = damping;
			o.octaveCount = octaveCount;
			o.octaveMultiplier = octaveMultiplier;
			o.octaveScale = octaveScale;
			o.quality = (UnityEngine.ParticleSystemNoiseQuality)quality;
			o.scrollSpeed = Write(o.scrollSpeed, scrollSpeed, objects);
			o.scrollSpeedMultiplier = scrollSpeedMultiplier;
			o.remapEnabled = remapEnabled;
			o.remap = Write(o.remap, remap, objects);
			o.remapMultiplier = remapMultiplier;
			o.remapX = Write(o.remapX, remapX, objects);
			o.remapXMultiplier = remapXMultiplier;
			o.remapY = Write(o.remapY, remapY, objects);
			o.remapYMultiplier = remapYMultiplier;
			o.remapZ = Write(o.remapZ, remapZ, objects);
			o.remapZMultiplier = remapZMultiplier;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.ParticleSystem.NoiseModule o = (UnityEngine.ParticleSystem.NoiseModule)obj;
			enabled = o.enabled;
			separateAxes = o.separateAxes;
			strength = Read(strength, o.strength);
			strengthMultiplier = o.strengthMultiplier;
			strengthX = Read(strengthX, o.strengthX);
			strengthXMultiplier = o.strengthXMultiplier;
			strengthY = Read(strengthY, o.strengthY);
			strengthYMultiplier = o.strengthYMultiplier;
			strengthZ = Read(strengthZ, o.strengthZ);
			strengthZMultiplier = o.strengthZMultiplier;
			frequency = o.frequency;
			damping = o.damping;
			octaveCount = o.octaveCount;
			octaveMultiplier = o.octaveMultiplier;
			octaveScale = o.octaveScale;
			quality = (uint)o.quality;
			scrollSpeed = Read(scrollSpeed, o.scrollSpeed);
			scrollSpeedMultiplier = o.scrollSpeedMultiplier;
			remapEnabled = o.remapEnabled;
			remap = Read(remap, o.remap);
			remapMultiplier = o.remapMultiplier;
			remapX = Read(remapX, o.remapX);
			remapXMultiplier = o.remapXMultiplier;
			remapY = Read(remapY, o.remapY);
			remapYMultiplier = o.remapYMultiplier;
			remapZ = Read(remapZ, o.remapZ);
			remapZMultiplier = o.remapZMultiplier;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			FindDependencies(strength, dependencies, objects, allowNulls);
			FindDependencies(strengthX, dependencies, objects, allowNulls);
			FindDependencies(strengthY, dependencies, objects, allowNulls);
			FindDependencies(strengthZ, dependencies, objects, allowNulls);
			FindDependencies(scrollSpeed, dependencies, objects, allowNulls);
			FindDependencies(remap, dependencies, objects, allowNulls);
			FindDependencies(remapX, dependencies, objects, allowNulls);
			FindDependencies(remapY, dependencies, objects, allowNulls);
			FindDependencies(remapZ, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.ParticleSystem.NoiseModule o = (UnityEngine.ParticleSystem.NoiseModule)obj;
			GetDependencies(strength, o.strength, dependencies);
			GetDependencies(strengthX, o.strengthX, dependencies);
			GetDependencies(strengthY, o.strengthY, dependencies);
			GetDependencies(strengthZ, o.strengthZ, dependencies);
			GetDependencies(scrollSpeed, o.scrollSpeed, dependencies);
			GetDependencies(remap, o.remap, dependencies);
			GetDependencies(remapX, o.remapX, dependencies);
			GetDependencies(remapY, o.remapY, dependencies);
			GetDependencies(remapZ, o.remapZ, dependencies);
		}

		public bool enabled;

		public bool separateAxes;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve strength;

		public float strengthMultiplier;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve strengthX;

		public float strengthXMultiplier;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve strengthY;

		public float strengthYMultiplier;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve strengthZ;

		public float strengthZMultiplier;

		public float frequency;

		public bool damping;

		public int octaveCount;

		public float octaveMultiplier;

		public float octaveScale;

		public uint quality;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve scrollSpeed;

		public float scrollSpeedMultiplier;

		public bool remapEnabled;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve remap;

		public float remapMultiplier;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve remapX;

		public float remapXMultiplier;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve remapY;

		public float remapYMultiplier;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve remapZ;

		public float remapZMultiplier;

	}
}

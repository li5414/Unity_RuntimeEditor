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
	public class PersistentTrailModule : Battlehub.RTSaveLoad.PersistentData
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.ParticleSystem.TrailModule o = (UnityEngine.ParticleSystem.TrailModule)obj;
			o.enabled = enabled;
			o.ratio = ratio;
			o.lifetime = Write(o.lifetime, lifetime, objects);
			o.lifetimeMultiplier = lifetimeMultiplier;
			o.minVertexDistance = minVertexDistance;
			o.textureMode = (UnityEngine.ParticleSystemTrailTextureMode)textureMode;
			o.worldSpace = worldSpace;
			o.dieWithParticles = dieWithParticles;
			o.sizeAffectsWidth = sizeAffectsWidth;
			o.sizeAffectsLifetime = sizeAffectsLifetime;
			o.inheritParticleColor = inheritParticleColor;
			o.colorOverLifetime = Write(o.colorOverLifetime, colorOverLifetime, objects);
			o.widthOverTrail = Write(o.widthOverTrail, widthOverTrail, objects);
			o.widthOverTrailMultiplier = widthOverTrailMultiplier;
			o.colorOverTrail = Write(o.colorOverTrail, colorOverTrail, objects);
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.ParticleSystem.TrailModule o = (UnityEngine.ParticleSystem.TrailModule)obj;
			enabled = o.enabled;
			ratio = o.ratio;
			lifetime = Read(lifetime, o.lifetime);
			lifetimeMultiplier = o.lifetimeMultiplier;
			minVertexDistance = o.minVertexDistance;
			textureMode = (uint)o.textureMode;
			worldSpace = o.worldSpace;
			dieWithParticles = o.dieWithParticles;
			sizeAffectsWidth = o.sizeAffectsWidth;
			sizeAffectsLifetime = o.sizeAffectsLifetime;
			inheritParticleColor = o.inheritParticleColor;
			colorOverLifetime = Read(colorOverLifetime, o.colorOverLifetime);
			widthOverTrail = Read(widthOverTrail, o.widthOverTrail);
			widthOverTrailMultiplier = o.widthOverTrailMultiplier;
			colorOverTrail = Read(colorOverTrail, o.colorOverTrail);
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			FindDependencies(lifetime, dependencies, objects, allowNulls);
			FindDependencies(colorOverLifetime, dependencies, objects, allowNulls);
			FindDependencies(widthOverTrail, dependencies, objects, allowNulls);
			FindDependencies(colorOverTrail, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.ParticleSystem.TrailModule o = (UnityEngine.ParticleSystem.TrailModule)obj;
			GetDependencies(lifetime, o.lifetime, dependencies);
			GetDependencies(colorOverLifetime, o.colorOverLifetime, dependencies);
			GetDependencies(widthOverTrail, o.widthOverTrail, dependencies);
			GetDependencies(colorOverTrail, o.colorOverTrail, dependencies);
		}

		public bool enabled;

		public float ratio;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve lifetime;

		public float lifetimeMultiplier;

		public float minVertexDistance;

		public uint textureMode;

		public bool worldSpace;

		public bool dieWithParticles;

		public bool sizeAffectsWidth;

		public bool sizeAffectsLifetime;

		public bool inheritParticleColor;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxGradient colorOverLifetime;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve widthOverTrail;

		public float widthOverTrailMultiplier;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxGradient colorOverTrail;

	}
}

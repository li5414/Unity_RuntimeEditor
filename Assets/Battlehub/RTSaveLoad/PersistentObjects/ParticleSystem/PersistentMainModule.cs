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
	public class PersistentMainModule : Battlehub.RTSaveLoad.PersistentData
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.ParticleSystem.MainModule o = (UnityEngine.ParticleSystem.MainModule)obj;
			o.duration = duration;
			o.loop = loop;
			o.prewarm = prewarm;
			o.startDelay = Write(o.startDelay, startDelay, objects);
			o.startDelayMultiplier = startDelayMultiplier;
			o.startLifetime = Write(o.startLifetime, startLifetime, objects);
			o.startLifetimeMultiplier = startLifetimeMultiplier;
			o.startSpeed = Write(o.startSpeed, startSpeed, objects);
			o.startSpeedMultiplier = startSpeedMultiplier;
			o.startSize3D = startSize3D;
			o.startSize = Write(o.startSize, startSize, objects);
			o.startSizeMultiplier = startSizeMultiplier;
			o.startSizeX = Write(o.startSizeX, startSizeX, objects);
			o.startSizeXMultiplier = startSizeXMultiplier;
			o.startSizeY = Write(o.startSizeY, startSizeY, objects);
			o.startSizeYMultiplier = startSizeYMultiplier;
			o.startSizeZ = Write(o.startSizeZ, startSizeZ, objects);
			o.startSizeZMultiplier = startSizeZMultiplier;
			o.startRotation3D = startRotation3D;
			o.startRotation = Write(o.startRotation, startRotation, objects);
			o.startRotationMultiplier = startRotationMultiplier;
			o.startRotationX = Write(o.startRotationX, startRotationX, objects);
			o.startRotationXMultiplier = startRotationXMultiplier;
			o.startRotationY = Write(o.startRotationY, startRotationY, objects);
			o.startRotationYMultiplier = startRotationYMultiplier;
			o.startRotationZ = Write(o.startRotationZ, startRotationZ, objects);
			o.startRotationZMultiplier = startRotationZMultiplier;
			o.randomizeRotationDirection = randomizeRotationDirection;
			o.startColor = Write(o.startColor, startColor, objects);
			o.gravityModifier = Write(o.gravityModifier, gravityModifier, objects);
			o.gravityModifierMultiplier = gravityModifierMultiplier;
			o.simulationSpace = (UnityEngine.ParticleSystemSimulationSpace)simulationSpace;
			o.customSimulationSpace = (UnityEngine.Transform)objects.Get(customSimulationSpace);
			o.simulationSpeed = simulationSpeed;
			o.scalingMode = (UnityEngine.ParticleSystemScalingMode)scalingMode;
			o.playOnAwake = playOnAwake;
			o.maxParticles = maxParticles;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.ParticleSystem.MainModule o = (UnityEngine.ParticleSystem.MainModule)obj;
			duration = o.duration;
			loop = o.loop;
			prewarm = o.prewarm;
			startDelay = Read(startDelay, o.startDelay);
			startDelayMultiplier = o.startDelayMultiplier;
			startLifetime = Read(startLifetime, o.startLifetime);
			startLifetimeMultiplier = o.startLifetimeMultiplier;
			startSpeed = Read(startSpeed, o.startSpeed);
			startSpeedMultiplier = o.startSpeedMultiplier;
			startSize3D = o.startSize3D;
			startSize = Read(startSize, o.startSize);
			startSizeMultiplier = o.startSizeMultiplier;
			startSizeX = Read(startSizeX, o.startSizeX);
			startSizeXMultiplier = o.startSizeXMultiplier;
			startSizeY = Read(startSizeY, o.startSizeY);
			startSizeYMultiplier = o.startSizeYMultiplier;
			startSizeZ = Read(startSizeZ, o.startSizeZ);
			startSizeZMultiplier = o.startSizeZMultiplier;
			startRotation3D = o.startRotation3D;
			startRotation = Read(startRotation, o.startRotation);
			startRotationMultiplier = o.startRotationMultiplier;
			startRotationX = Read(startRotationX, o.startRotationX);
			startRotationXMultiplier = o.startRotationXMultiplier;
			startRotationY = Read(startRotationY, o.startRotationY);
			startRotationYMultiplier = o.startRotationYMultiplier;
			startRotationZ = Read(startRotationZ, o.startRotationZ);
			startRotationZMultiplier = o.startRotationZMultiplier;
			randomizeRotationDirection = o.randomizeRotationDirection;
			startColor = Read(startColor, o.startColor);
			gravityModifier = Read(gravityModifier, o.gravityModifier);
			gravityModifierMultiplier = o.gravityModifierMultiplier;
			simulationSpace = (uint)o.simulationSpace;
			customSimulationSpace = o.customSimulationSpace.GetMappedInstanceID();
			simulationSpeed = o.simulationSpeed;
			scalingMode = (uint)o.scalingMode;
			playOnAwake = o.playOnAwake;
			maxParticles = o.maxParticles;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			FindDependencies(startDelay, dependencies, objects, allowNulls);
			FindDependencies(startLifetime, dependencies, objects, allowNulls);
			FindDependencies(startSpeed, dependencies, objects, allowNulls);
			FindDependencies(startSize, dependencies, objects, allowNulls);
			FindDependencies(startSizeX, dependencies, objects, allowNulls);
			FindDependencies(startSizeY, dependencies, objects, allowNulls);
			FindDependencies(startSizeZ, dependencies, objects, allowNulls);
			FindDependencies(startRotation, dependencies, objects, allowNulls);
			FindDependencies(startRotationX, dependencies, objects, allowNulls);
			FindDependencies(startRotationY, dependencies, objects, allowNulls);
			FindDependencies(startRotationZ, dependencies, objects, allowNulls);
			FindDependencies(startColor, dependencies, objects, allowNulls);
			FindDependencies(gravityModifier, dependencies, objects, allowNulls);
			AddDependency(customSimulationSpace, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.ParticleSystem.MainModule o = (UnityEngine.ParticleSystem.MainModule)obj;
			GetDependencies(startDelay, o.startDelay, dependencies);
			GetDependencies(startLifetime, o.startLifetime, dependencies);
			GetDependencies(startSpeed, o.startSpeed, dependencies);
			GetDependencies(startSize, o.startSize, dependencies);
			GetDependencies(startSizeX, o.startSizeX, dependencies);
			GetDependencies(startSizeY, o.startSizeY, dependencies);
			GetDependencies(startSizeZ, o.startSizeZ, dependencies);
			GetDependencies(startRotation, o.startRotation, dependencies);
			GetDependencies(startRotationX, o.startRotationX, dependencies);
			GetDependencies(startRotationY, o.startRotationY, dependencies);
			GetDependencies(startRotationZ, o.startRotationZ, dependencies);
			GetDependencies(startColor, o.startColor, dependencies);
			GetDependencies(gravityModifier, o.gravityModifier, dependencies);
			AddDependency(o.customSimulationSpace, dependencies);
		}

		public float duration;

		public bool loop;

		public bool prewarm;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve startDelay;

		public float startDelayMultiplier;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve startLifetime;

		public float startLifetimeMultiplier;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve startSpeed;

		public float startSpeedMultiplier;

		public bool startSize3D;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve startSize;

		public float startSizeMultiplier;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve startSizeX;

		public float startSizeXMultiplier;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve startSizeY;

		public float startSizeYMultiplier;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve startSizeZ;

		public float startSizeZMultiplier;

		public bool startRotation3D;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve startRotation;

		public float startRotationMultiplier;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve startRotationX;

		public float startRotationXMultiplier;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve startRotationY;

		public float startRotationYMultiplier;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve startRotationZ;

		public float startRotationZMultiplier;

		public float randomizeRotationDirection;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxGradient startColor;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve gravityModifier;

		public float gravityModifierMultiplier;

		public uint simulationSpace;

		public long customSimulationSpace;

		public float simulationSpeed;

		public uint scalingMode;

		public bool playOnAwake;

		public int maxParticles;

	}
}

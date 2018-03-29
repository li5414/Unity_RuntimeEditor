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
	public class PersistentParticle : Battlehub.RTSaveLoad.PersistentData
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.ParticleSystem.Particle o = (UnityEngine.ParticleSystem.Particle)obj;
			o.position = position;
			o.velocity = velocity;
			o.remainingLifetime = remainingLifetime;
			o.startLifetime = startLifetime;
			o.startSize = startSize;
			o.startSize3D = startSize3D;
			o.axisOfRotation = axisOfRotation;
			o.rotation = rotation;
			o.rotation3D = rotation3D;
			o.angularVelocity = angularVelocity;
			o.angularVelocity3D = angularVelocity3D;
			o.startColor = startColor;
			o.randomSeed = randomSeed;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.ParticleSystem.Particle o = (UnityEngine.ParticleSystem.Particle)obj;
			position = o.position;
			velocity = o.velocity;
			remainingLifetime = o.remainingLifetime;
			startLifetime = o.startLifetime;
			startSize = o.startSize;
			startSize3D = o.startSize3D;
			axisOfRotation = o.axisOfRotation;
			rotation = o.rotation;
			rotation3D = o.rotation3D;
			angularVelocity = o.angularVelocity;
			angularVelocity3D = o.angularVelocity3D;
			startColor = o.startColor;
			randomSeed = o.randomSeed;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public UnityEngine.Vector3 position;

		public UnityEngine.Vector3 velocity;

		public float remainingLifetime;

		public float startLifetime;

		public float startSize;

		public UnityEngine.Vector3 startSize3D;

		public UnityEngine.Vector3 axisOfRotation;

		public float rotation;

		public UnityEngine.Vector3 rotation3D;

		public float angularVelocity;

		public UnityEngine.Vector3 angularVelocity3D;

		public UnityEngine.Color32 startColor;

		public uint randomSeed;

	}
}

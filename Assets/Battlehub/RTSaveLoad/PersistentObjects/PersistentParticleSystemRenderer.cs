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
	public class PersistentParticleSystemRenderer : Battlehub.RTSaveLoad.PersistentObjects.PersistentRenderer
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.ParticleSystemRenderer o = (UnityEngine.ParticleSystemRenderer)obj;
			o.renderMode = (UnityEngine.ParticleSystemRenderMode)renderMode;
			o.lengthScale = lengthScale;
			o.velocityScale = velocityScale;
			o.cameraVelocityScale = cameraVelocityScale;
			o.normalDirection = normalDirection;
			o.alignment = (UnityEngine.ParticleSystemRenderSpace)alignment;
			o.pivot = pivot;
			o.sortMode = (UnityEngine.ParticleSystemSortMode)sortMode;
			o.sortingFudge = sortingFudge;
			o.minParticleSize = minParticleSize;
			o.maxParticleSize = maxParticleSize;
			o.mesh = (UnityEngine.Mesh)objects.Get(mesh);
			o.trailMaterial = (UnityEngine.Material)objects.Get(trailMaterial);
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.ParticleSystemRenderer o = (UnityEngine.ParticleSystemRenderer)obj;
			renderMode = (uint)o.renderMode;
			lengthScale = o.lengthScale;
			velocityScale = o.velocityScale;
			cameraVelocityScale = o.cameraVelocityScale;
			normalDirection = o.normalDirection;
			alignment = (uint)o.alignment;
			pivot = o.pivot;
			sortMode = (uint)o.sortMode;
			sortingFudge = o.sortingFudge;
			minParticleSize = o.minParticleSize;
			maxParticleSize = o.maxParticleSize;
			mesh = o.mesh.GetMappedInstanceID();
			trailMaterial = o.trailMaterial.GetMappedInstanceID();
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			AddDependency(mesh, dependencies, objects, allowNulls);
			AddDependency(trailMaterial, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.ParticleSystemRenderer o = (UnityEngine.ParticleSystemRenderer)obj;
			AddDependency(o.mesh, dependencies);
			AddDependency(o.trailMaterial, dependencies);
		}

		public uint renderMode;

		public float lengthScale;

		public float velocityScale;

		public float cameraVelocityScale;

		public float normalDirection;

		public uint alignment;

		public UnityEngine.Vector3 pivot;

		public uint sortMode;

		public float sortingFudge;

		public float minParticleSize;

		public float maxParticleSize;

		public long mesh;

		public long trailMaterial;

	}
}

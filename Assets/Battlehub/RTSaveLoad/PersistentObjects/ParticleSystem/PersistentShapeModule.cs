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
	public class PersistentShapeModule : Battlehub.RTSaveLoad.PersistentData
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.ParticleSystem.ShapeModule o = (UnityEngine.ParticleSystem.ShapeModule)obj;
			o.enabled = enabled;
			o.shapeType = (UnityEngine.ParticleSystemShapeType)shapeType;
			o.randomDirectionAmount = randomDirectionAmount;
			o.sphericalDirectionAmount = sphericalDirectionAmount;
			o.alignToDirection = alignToDirection;
			o.radius = radius;
			o.radiusMode = (UnityEngine.ParticleSystemShapeMultiModeValue)radiusMode;
			o.radiusSpread = radiusSpread;
			o.radiusSpeed = Write(o.radiusSpeed, radiusSpeed, objects);
			o.radiusSpeedMultiplier = radiusSpeedMultiplier;
			o.angle = angle;
			o.length = length;
			o.scale = box;
			o.meshShapeType = (UnityEngine.ParticleSystemMeshShapeType)meshShapeType;
			o.mesh = (UnityEngine.Mesh)objects.Get(mesh);
			o.meshRenderer = (UnityEngine.MeshRenderer)objects.Get(meshRenderer);
			o.skinnedMeshRenderer = (UnityEngine.SkinnedMeshRenderer)objects.Get(skinnedMeshRenderer);
			o.useMeshMaterialIndex = useMeshMaterialIndex;
			o.meshMaterialIndex = meshMaterialIndex;
			o.useMeshColors = useMeshColors;
			o.normalOffset = normalOffset;
            //o.meshScale = meshScale;
            o.scale = scale;
			o.arc = arc;
			o.arcMode = (UnityEngine.ParticleSystemShapeMultiModeValue)arcMode;
			o.arcSpread = arcSpread;
			o.arcSpeed = Write(o.arcSpeed, arcSpeed, objects);
			o.arcSpeedMultiplier = arcSpeedMultiplier;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.ParticleSystem.ShapeModule o = (UnityEngine.ParticleSystem.ShapeModule)obj;
			enabled = o.enabled;
			shapeType = (uint)o.shapeType;
			randomDirectionAmount = o.randomDirectionAmount;
			sphericalDirectionAmount = o.sphericalDirectionAmount;
			alignToDirection = o.alignToDirection;
			radius = o.radius;
			radiusMode = (uint)o.radiusMode;
			radiusSpread = o.radiusSpread;
			radiusSpeed = Read(radiusSpeed, o.radiusSpeed);
			radiusSpeedMultiplier = o.radiusSpeedMultiplier;
			angle = o.angle;
			length = o.length;
			box = o.scale;
			meshShapeType = (uint)o.meshShapeType;
			mesh = o.mesh.GetMappedInstanceID();
			meshRenderer = o.meshRenderer.GetMappedInstanceID();
			skinnedMeshRenderer = o.skinnedMeshRenderer.GetMappedInstanceID();
			useMeshMaterialIndex = o.useMeshMaterialIndex;
			meshMaterialIndex = o.meshMaterialIndex;
			useMeshColors = o.useMeshColors;
			normalOffset = o.normalOffset;
            //meshScale = o.meshScale;
            scale = o.scale;
            arc = o.arc;
			arcMode = (uint)o.arcMode;
			arcSpread = o.arcSpread;
			arcSpeed = Read(arcSpeed, o.arcSpeed);
			arcSpeedMultiplier = o.arcSpeedMultiplier;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			FindDependencies(radiusSpeed, dependencies, objects, allowNulls);
			AddDependency(mesh, dependencies, objects, allowNulls);
			AddDependency(meshRenderer, dependencies, objects, allowNulls);
			AddDependency(skinnedMeshRenderer, dependencies, objects, allowNulls);
			FindDependencies(arcSpeed, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.ParticleSystem.ShapeModule o = (UnityEngine.ParticleSystem.ShapeModule)obj;
			GetDependencies(radiusSpeed, o.radiusSpeed, dependencies);
			AddDependency(o.mesh, dependencies);
			AddDependency(o.meshRenderer, dependencies);
			AddDependency(o.skinnedMeshRenderer, dependencies);
			GetDependencies(arcSpeed, o.arcSpeed, dependencies);
		}

		public bool enabled;

		public uint shapeType;

		public float randomDirectionAmount;

		public float sphericalDirectionAmount;

		public bool alignToDirection;

		public float radius;

		public uint radiusMode;

		public float radiusSpread;

		public PersistentMinMaxCurve radiusSpeed;

		public float radiusSpeedMultiplier;

		public float angle;

		public float length;

		public UnityEngine.Vector3 box;

		public uint meshShapeType;

		public long mesh;

		public long meshRenderer;

		public long skinnedMeshRenderer;

		public bool useMeshMaterialIndex;

		public int meshMaterialIndex;

		public bool useMeshColors;

		public float normalOffset;

		public float meshScale;

		public float arc;

		public uint arcMode;

		public float arcSpread;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve arcSpeed;

		public float arcSpeedMultiplier;

        public UnityEngine.Vector3 scale;

	}
}

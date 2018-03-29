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
	public class PersistentCloth
#if RT_PE_MAINTANANCE
        : PersistentData
#else
        : Battlehub.RTSaveLoad.PersistentObjects.PersistentComponent
#endif
    {
        public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.Cloth o = (UnityEngine.Cloth)obj;
			o.sleepThreshold = sleepThreshold;
			o.bendingStiffness = bendingStiffness;
			o.stretchingStiffness = stretchingStiffness;
			o.damping = damping;
			o.externalAcceleration = externalAcceleration;
			o.randomAcceleration = randomAcceleration;
			o.useGravity = useGravity;
			o.enabled = enabled;
			o.friction = friction;
			o.collisionMassScale = collisionMassScale;
			o.useContinuousCollision = useContinuousCollision;
			o.useVirtualParticles = useVirtualParticles;
			o.coefficients = coefficients;
			o.worldVelocityScale = worldVelocityScale;
			o.worldAccelerationScale = worldAccelerationScale;
			o.solverFrequency = solverFrequency;
			o.capsuleColliders = Resolve<UnityEngine.CapsuleCollider, UnityEngine.Object>(capsuleColliders, objects);
			//o.sphereColliders = sphereColliders;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.Cloth o = (UnityEngine.Cloth)obj;
			sleepThreshold = o.sleepThreshold;
			bendingStiffness = o.bendingStiffness;
			stretchingStiffness = o.stretchingStiffness;
			damping = o.damping;
			externalAcceleration = o.externalAcceleration;
			randomAcceleration = o.randomAcceleration;
			useGravity = o.useGravity;
			enabled = o.enabled;
			friction = o.friction;
			collisionMassScale = o.collisionMassScale;
			useContinuousCollision = o.useContinuousCollision;
			useVirtualParticles = o.useVirtualParticles;
			coefficients = o.coefficients;
			worldVelocityScale = o.worldVelocityScale;
			worldAccelerationScale = o.worldAccelerationScale;
			solverFrequency = o.solverFrequency;
			capsuleColliders = o.capsuleColliders.GetMappedInstanceID();
			//sphereColliders = o.sphereColliders;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			AddDependencies(capsuleColliders, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.Cloth o = (UnityEngine.Cloth)obj;
			AddDependencies(o.capsuleColliders, dependencies);
		}

        public float sleepThreshold;

        public float bendingStiffness;
	
        public float stretchingStiffness;

        public float damping;
		
        public UnityEngine.Vector3 externalAcceleration;

        public UnityEngine.Vector3 randomAcceleration;

        public bool useGravity;
	
        public bool enabled;
	
        public float friction;
	
        public float collisionMassScale;

        public float useContinuousCollision;

        public float useVirtualParticles;

        public UnityEngine.ClothSkinningCoefficient[] coefficients;
	
        public float worldVelocityScale;

        public float worldAccelerationScale;
	
        public bool solverFrequency;
		
        public long[] capsuleColliders;
		
        //public UnityEngine.ClothSphereColliderPair[] sphereColliders;
      

    }
}

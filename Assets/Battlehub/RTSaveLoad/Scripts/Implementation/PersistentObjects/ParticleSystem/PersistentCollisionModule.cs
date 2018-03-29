#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif

/*To autogenerate PersistentCollisionModule remove this file and run Tools->Runtime SaveLoad->Create Persistent Objects command*/

using System.Collections.Generic;
using UnityEngine;

namespace Battlehub.RTSaveLoad.PersistentObjects
{

#if RT_USE_PROTOBUF
	[ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
#endif
    [System.Serializable]
    public class PersistentCollisionModule : Battlehub.RTSaveLoad.PersistentData
    {
        public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
        {
            obj = base.WriteTo(obj, objects);
            if (obj == null)
            {
                return null;
            }
            ParticleSystem.CollisionModule o = (UnityEngine.ParticleSystem.CollisionModule)obj;
            o.enabled = enabled;
            o.type = type;
            o.mode = mode;
#if !RT_PE_MAINTANANCE
            o.dampen = Write(o.dampen, dampen, objects);
            o.bounce = Write(o.bounce, bounce, objects);
            o.lifetimeLoss = Write(o.lifetimeLoss, lifetimeLoss, objects);
#endif

            o.dampenMultiplier = dampenMultiplier;
            o.bounceMultiplier = bounceMultiplier;            
            o.lifetimeLossMultiplier = lifetimeLossMultiplier;
            o.minKillSpeed = minKillSpeed;
            o.maxKillSpeed = maxKillSpeed;
            o.collidesWith = collidesWith;
            o.enableDynamicColliders = enableDynamicColliders;
            //o.enableInteriorCollisions = enableInteriorCollisions;
            o.maxCollisionShapes = maxCollisionShapes;
            o.quality = quality;
            o.voxelSize = voxelSize;
            o.radiusScale = radiusScale;
            o.sendCollisionMessages = sendCollisionMessages;

            if (planes == null)
            {
                for (int i = 0; i < o.maxPlaneCount; ++i)
                {
                    o.SetPlane(i, null);
                }
            }
            else
            {
                for (int i = 0; i < UnityEngine.Mathf.Min(o.maxPlaneCount, planes.Length); ++i)
                {
                    o.SetPlane(i, (UnityEngine.Transform)objects.Get(planes[i]));
                }
            }

            return o;
        }

        public override void ReadFrom(object obj)
        {
            base.ReadFrom(obj);
            if (obj == null)
            {
                return;
            }
            UnityEngine.ParticleSystem.CollisionModule o = (UnityEngine.ParticleSystem.CollisionModule)obj;
            enabled = o.enabled;
            type = o.type;
            mode = o.mode;
#if !RT_PE_MAINTANANCE
            dampen = Read(dampen, o.dampen);
            bounce = Read(bounce, o.bounce);
            lifetimeLoss = Read(lifetimeLoss, o.lifetimeLoss);
#endif
            dampenMultiplier = o.dampenMultiplier;
            bounceMultiplier = o.bounceMultiplier;
            lifetimeLossMultiplier = o.lifetimeLossMultiplier;
            minKillSpeed = o.minKillSpeed;
            maxKillSpeed = o.maxKillSpeed;
            collidesWith = o.collidesWith;
            enableDynamicColliders = o.enableDynamicColliders;
            //enableInteriorCollisions = o.enableInteriorCollisions;
            maxCollisionShapes = o.maxCollisionShapes;
            quality = o.quality;
            voxelSize = o.voxelSize;
            radiusScale = o.radiusScale;
            sendCollisionMessages = o.sendCollisionMessages;

            if (o.maxPlaneCount > 20)
            {
                UnityEngine.Debug.LogWarning("maxPlaneCount is expected to be 6 or at least <= 20");
            }
            planes = new long[o.maxPlaneCount];
            for (int i = 0; i < o.maxPlaneCount; ++i)
            {
                planes[i] = o.GetPlane(i).GetMappedInstanceID();
            }

        }

        public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
        {
            base.FindDependencies(dependencies, objects, allowNulls);

            AddDependencies(planes, dependencies, objects, allowNulls);
#if !RT_PE_MAINTANANCE
            FindDependencies(dampen, dependencies, objects, allowNulls);
            FindDependencies(bounce, dependencies, objects, allowNulls);
            FindDependencies(lifetimeLoss, dependencies, objects, allowNulls);
#endif
        }

        protected override void GetDependencies(Dictionary<long, Object> dependencies, object obj)
        {
            base.GetDependencies(dependencies, obj);
            if (obj == null)
            {
                return;
            }
            UnityEngine.ParticleSystem.CollisionModule o = (UnityEngine.ParticleSystem.CollisionModule)obj;

            Object[] planeObjects = new Object[o.maxPlaneCount];
            for (int i = 0; i < o.maxPlaneCount; ++i)
            {
                planeObjects[i] = o.GetPlane(i);
            }

            AddDependencies(planeObjects, dependencies);
#if !RT_PE_MAINTANANCE
            GetDependencies(dampen, o.dampen, dependencies);
            GetDependencies(bounce, o.bounce, dependencies);
            GetDependencies(lifetimeLoss, o.lifetimeLoss, dependencies);
#endif
        }

        public bool enabled;
        public ParticleSystemCollisionType type;
        public ParticleSystemCollisionMode mode;
        public long[] planes;

#if !RT_PE_MAINTANANCE
        public PersistentMinMaxCurve dampen;
        public PersistentMinMaxCurve bounce;
        public PersistentMinMaxCurve lifetimeLoss;
#endif
        public float dampenMultiplier;
        public float bounceMultiplier;
        public float lifetimeLossMultiplier;
        public float minKillSpeed;
        public float maxKillSpeed;
        public LayerMask collidesWith;
        public bool enableDynamicColliders;
        public bool enableInteriorCollisions;
        public int maxCollisionShapes;
        public ParticleSystemCollisionQuality quality;
        public float voxelSize;
        public float radiusScale;
        public bool sendCollisionMessages;

    }
}

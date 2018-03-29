#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif

/*To autogenerate PersistentTriggerModule remove this file and run Tools->Runtime SaveLoad->Create Persistent Objects command*/
using System.Collections.Generic;
using UnityEngine;

namespace Battlehub.RTSaveLoad.PersistentObjects
{
#if RT_USE_PROTOBUF
	[ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
#endif
    [System.Serializable]
    public class PersistentTriggerModule : Battlehub.RTSaveLoad.PersistentData
    {
        public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
        {
            obj = base.WriteTo(obj, objects);
            if (obj == null)
            {
                return null;
            }
            UnityEngine.ParticleSystem.TriggerModule o = (UnityEngine.ParticleSystem.TriggerModule)obj;
            o.enabled = enabled;
            o.inside = inside;
            o.outside = outside;
            o.enter = enter;
            o.exit = exit;
            o.radiusScale = radiusScale;
            if (colliders == null)
            {
                for (int i = 0; i < o.maxColliderCount; ++i)
                {
                    o.SetCollider(i, null);
                }
            }
            else
            {
                for (int i = 0; i < UnityEngine.Mathf.Min(o.maxColliderCount, colliders.Length); ++i)
                {
                    object col = objects.Get(colliders[i]);
                    o.SetCollider(i, (UnityEngine.Component)col);
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
            UnityEngine.ParticleSystem.TriggerModule o = (UnityEngine.ParticleSystem.TriggerModule)obj;
            enabled = o.enabled;
            inside = o.inside;
            outside = o.outside;
            enter = o.enter;
            exit = o.exit;
            radiusScale = o.radiusScale;
            if (o.maxColliderCount > 20)
            {
                UnityEngine.Debug.LogWarning("maxPlaneCount is expected to be 6 or at least <= 20");
            }
            colliders = new long[o.maxColliderCount];
            for (int i = 0; i < o.maxColliderCount; ++i)
            {
                UnityEngine.Component collider = o.GetCollider(i);
                colliders[i] = collider.GetMappedInstanceID();
            }
        }

        public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
        {
            base.FindDependencies(dependencies, objects, allowNulls);
            AddDependencies(colliders, dependencies, objects, allowNulls);
        }

        protected override void GetDependencies(Dictionary<long, Object> dependencies, object obj)
        {
            base.GetDependencies(dependencies, obj);
            if (obj == null)
            {
                return;
            }

            ParticleSystem.TriggerModule o = (ParticleSystem.TriggerModule)obj;
            Object[] colliderObjects = new Object[o.maxColliderCount];
            for (int i = 0; i < o.maxColliderCount; ++i)
            {
                colliderObjects[i] = o.GetCollider(i);
            }

            AddDependencies(colliderObjects, dependencies);
        }

        public bool enabled;
        public UnityEngine.ParticleSystemOverlapAction inside;
        public UnityEngine.ParticleSystemOverlapAction outside;
        public UnityEngine.ParticleSystemOverlapAction enter;
        public UnityEngine.ParticleSystemOverlapAction exit;
        public float radiusScale;
        public long[] colliders;
    }
}

	
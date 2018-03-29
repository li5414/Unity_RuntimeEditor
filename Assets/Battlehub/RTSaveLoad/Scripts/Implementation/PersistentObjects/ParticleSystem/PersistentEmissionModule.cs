#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif


/*To autogenerate PersistentEmissionModule remove this file and run Tools->Runtime SaveLoad->Create Persistent Objects command*/

using System.Collections.Generic;
using UnityEngine;

namespace Battlehub.RTSaveLoad.PersistentObjects
{
#if RT_USE_PROTOBUF
    [ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
#endif
    [System.Serializable]
    public class PersistentEmissionModule : Battlehub.RTSaveLoad.PersistentData
    {
        public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
        {
            obj = base.WriteTo(obj, objects);
            if (obj == null)
            {
                return null;
            }
            UnityEngine.ParticleSystem.EmissionModule o = (UnityEngine.ParticleSystem.EmissionModule)obj;
            o.enabled = enabled;
            #if !RT_PE_MAINTANANCE
            o.rateOverTime = Write(o.rateOverTime, rateOverTime, objects);
            o.rateOverDistance = Write(o.rateOverDistance, rateOverDistance, objects);

            if (bursts == null)
            {
                o.SetBursts(new ParticleSystem.Burst[0]);
            }
            else
            {
                ParticleSystem.Burst[] psBursts = new ParticleSystem.Burst[bursts.Length];
                for (int i = 0; i < bursts.Length; ++i)
                {
                    ParticleSystem.Burst burst = new ParticleSystem.Burst();
                    psBursts[i] = Write(burst, bursts[i], objects);
                }
                o.SetBursts(psBursts);
            }
            #endif
            o.rateOverTimeMultiplier = rateOverTimeMultiplier;
            o.rateOverDistanceMultiplier = rateOverDistanceMultiplier;
            return o;
        }

        public override void ReadFrom(object obj)
        {
            base.ReadFrom(obj);
            if (obj == null)
            {
                return;
            }
            UnityEngine.ParticleSystem.EmissionModule o = (UnityEngine.ParticleSystem.EmissionModule)obj;
            enabled = o.enabled;
            #if !RT_PE_MAINTANANCE
            rateOverTime = Read(rateOverTime, o.rateOverTime);
            rateOverDistance = Read(rateOverDistance, o.rateOverDistance);

            UnityEngine.ParticleSystem.Burst[] psBursts = new UnityEngine.ParticleSystem.Burst[o.burstCount];
            bursts = new PersistentBurst[o.burstCount];
            o.GetBursts(psBursts);
            for (int i = 0; i < bursts.Length; ++i)
            {
                PersistentBurst burst = new PersistentBurst();
                burst.ReadFrom(bursts[i]);
                bursts[i] = burst;
            }
            #endif
            rateOverTimeMultiplier = o.rateOverTimeMultiplier;
            rateOverDistanceMultiplier = o.rateOverDistanceMultiplier;
        }

        public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
        {
            base.FindDependencies(dependencies, objects, allowNulls);
#if !RT_PE_MAINTANANCE
            FindDependencies(rateOverTime, dependencies, objects, allowNulls);
            FindDependencies(rateOverDistance, dependencies, objects, allowNulls);
#endif
        }

        protected override void GetDependencies(Dictionary<long, Object> dependencies, object obj)
        {
            base.GetDependencies(dependencies, obj);
            if (obj == null)
            {
                return;
            }

#if !RT_PE_MAINTANANCE
            ParticleSystem.EmissionModule o = (ParticleSystem.EmissionModule)obj;
            GetDependencies(rateOverTime, o.rateOverTime, dependencies);
            GetDependencies(rateOverDistance, o.rateOverDistance, dependencies);
#endif
        }

#if !RT_PE_MAINTANANCE
        public PersistentBurst[] bursts;
#endif

        public bool enabled;

#if !RT_PE_MAINTANANCE
        public PersistentMinMaxCurve rateOverTime;
        public PersistentMinMaxCurve rateOverDistance;
#endif
    
        public float rateOverTimeMultiplier;
        public float rateOverDistanceMultiplier;

    }
}
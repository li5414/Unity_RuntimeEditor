#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif

/*To autogenerate PersistentParticleSystem remove this file and run Tools->Runtime SaveLoad->Create Persistent Objects command*/

using System.Collections.Generic;
using UnityEngine;

namespace Battlehub.RTSaveLoad.PersistentObjects
{
    #if RT_USE_PROTOBUF
	[ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
    #endif
    [System.Serializable]
    public class PersistentParticleSystem
#if RT_PE_MAINTANANCE
        : PersistentData
#else
        : PersistentComponent
#endif
    { 
        public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
        {
            obj = base.WriteTo(obj, objects);
            if (obj == null)
            {
                return null;
            }
            UnityEngine.ParticleSystem o = (UnityEngine.ParticleSystem)obj;
            o.time = time;
            o.randomSeed = randomSeed;
            o.useAutoRandomSeed = useAutoRandomSeed;

            Write(o.emission, emission, objects);
            Write(o.collision, collision, objects);
            Write(o.trigger, trigger, objects);
#if !RT_PE_MAINTANANCE
            Write(o.shape, shape, objects);
            Write(o.velocityOverLifetime, velocityOverLifetime, objects);
            Write(o.limitVelocityOverLifetime, limitVelocityOverLifetime, objects);
            Write(o.inheritVelocity, inheritVelocity, objects);
            Write(o.forceOverLifetime, forceOverLifetime, objects);
            Write(o.colorOverLifetime, colorOverLifetime, objects);
            Write(o.colorBySpeed, colorBySpeed, objects);
            Write(o.sizeOverLifetime, sizeOverLifetime, objects);
            Write(o.sizeBySpeed, sizeBySpeed, objects);
            Write(o.rotationOverLifetime, rotationOverLifetime, objects);
            Write(o.rotationBySpeed, rotationBySpeed, objects);
            Write(o.externalForces, externalForces, objects);
            Write(o.subEmitters, subEmitters, objects);
            Write(o.textureSheetAnimation, textureSheetAnimation, objects);
            Write(o.lights, lights, objects);
            Write(o.main, main, objects);
            Write(o.noise, noise, objects);
            Write(o.trails, trails, objects);
#endif
            return o;
        }

        public override void ReadFrom(object obj)
        {
            base.ReadFrom(obj);
            if (obj == null)
            {
                return;
            }
            UnityEngine.ParticleSystem o = (UnityEngine.ParticleSystem)obj;
            time = o.time;
            randomSeed = o.randomSeed;
            useAutoRandomSeed = o.useAutoRandomSeed;

            emission = Read(emission, o.emission);
            collision = Read(collision, o.collision);
            trigger = Read(trigger, o.trigger);
#if !RT_PE_MAINTANANCE
            shape = Read(shape, o.shape);
            velocityOverLifetime = Read(velocityOverLifetime, o.velocityOverLifetime);
            limitVelocityOverLifetime = Read(limitVelocityOverLifetime, o.limitVelocityOverLifetime);
            inheritVelocity = Read(inheritVelocity, o.inheritVelocity);
            forceOverLifetime = Read(forceOverLifetime, o.forceOverLifetime);
            colorOverLifetime = Read(colorOverLifetime, o.colorOverLifetime);
            colorBySpeed = Read(colorBySpeed, o.colorBySpeed);
            sizeOverLifetime = Read(sizeOverLifetime, o.sizeOverLifetime);
            sizeBySpeed = Read(sizeBySpeed, o.sizeBySpeed);
            rotationOverLifetime = Read(rotationOverLifetime, o.rotationOverLifetime);
            rotationBySpeed = Read(rotationBySpeed, o.rotationBySpeed);
            externalForces = Read(externalForces, o.externalForces);
            subEmitters = Read(subEmitters, o.subEmitters);
            textureSheetAnimation = Read(textureSheetAnimation, o.textureSheetAnimation);
            lights = Read(lights, o.lights);
            main = Read(main, o.main);
            noise = Read(noise, o.noise);
            trails = Read(trails, o.trails);
#endif
        }

        public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
        {
            base.FindDependencies(dependencies, objects, allowNulls);
            FindDependencies(emission, dependencies, objects, allowNulls);
            FindDependencies(collision, dependencies, objects, allowNulls);
            FindDependencies(trigger, dependencies, objects, allowNulls);
#if !RT_PE_MAINTANANCE
            FindDependencies(shape, dependencies, objects, allowNulls);
            FindDependencies(velocityOverLifetime, dependencies, objects, allowNulls);
            FindDependencies(limitVelocityOverLifetime, dependencies, objects, allowNulls);
            FindDependencies(inheritVelocity, dependencies, objects, allowNulls);
            FindDependencies(forceOverLifetime, dependencies, objects, allowNulls);
            FindDependencies(colorOverLifetime, dependencies, objects, allowNulls);
            FindDependencies(colorBySpeed, dependencies, objects, allowNulls);
            FindDependencies(sizeOverLifetime, dependencies, objects, allowNulls);
            FindDependencies(sizeBySpeed, dependencies, objects, allowNulls);
            FindDependencies(rotationOverLifetime, dependencies, objects, allowNulls);
            FindDependencies(rotationBySpeed, dependencies, objects, allowNulls);
            FindDependencies(externalForces, dependencies, objects, allowNulls);
            FindDependencies(subEmitters, dependencies, objects, allowNulls);
            FindDependencies(textureSheetAnimation, dependencies, objects, allowNulls);
            FindDependencies(lights, dependencies, objects, allowNulls);
            FindDependencies(main, dependencies, objects, allowNulls);
            FindDependencies(noise, dependencies, objects, allowNulls);
            FindDependencies(trails, dependencies, objects, allowNulls);
#endif
        }


        protected override void GetDependencies(Dictionary<long, Object> dependencies, object obj)
        {
            base.GetDependencies(dependencies, obj);
            if (obj == null)
            {
                return;
            }

            ParticleSystem o = (ParticleSystem)obj;
            GetDependencies(emission, o.emission, dependencies);
            GetDependencies(collision, o.collision, dependencies);
            GetDependencies(trigger, o.trigger, dependencies);
#if !RT_PE_MAINTANANCE
            GetDependencies(shape, o.shape, dependencies);
            GetDependencies(velocityOverLifetime, o.velocityOverLifetime, dependencies);
            GetDependencies(limitVelocityOverLifetime, o.limitVelocityOverLifetime, dependencies);
            GetDependencies(inheritVelocity, o.inheritVelocity, dependencies);
            GetDependencies(forceOverLifetime, o.forceOverLifetime, dependencies);
            GetDependencies(colorOverLifetime, o.colorOverLifetime, dependencies);
            GetDependencies(colorBySpeed, o.colorBySpeed, dependencies);
            GetDependencies(sizeOverLifetime, o.sizeOverLifetime, dependencies);
            GetDependencies(sizeBySpeed, o.sizeBySpeed, dependencies);
            GetDependencies(rotationOverLifetime, o.rotationOverLifetime, dependencies);
            GetDependencies(rotationBySpeed, o.rotationBySpeed, dependencies);
            GetDependencies(externalForces, o.externalForces, dependencies);
            GetDependencies(subEmitters, o.subEmitters, dependencies);
            GetDependencies(textureSheetAnimation, o.textureSheetAnimation, dependencies);
            GetDependencies(lights, o.lights, dependencies);
            GetDependencies(main, o.main, dependencies);
            GetDependencies(noise, o.noise, dependencies);
            GetDependencies(trails, o.trails, dependencies);
#endif
        }

        public float time;
        public uint randomSeed;
        public bool useAutoRandomSeed;
        public PersistentEmissionModule emission;
        public PersistentCollisionModule collision;
        public PersistentTriggerModule trigger;


#if !RT_PE_MAINTANANCE
        
        public PersistentShapeModule shape;    
        public PersistentVelocityOverLifetimeModule velocityOverLifetime;
        public PersistentLimitVelocityOverLifetimeModule limitVelocityOverLifetime;
        public PersistentInheritVelocityModule inheritVelocity;
        public PersistentForceOverLifetimeModule forceOverLifetime;
        public PersistentColorOverLifetimeModule colorOverLifetime;
        public PersistentColorBySpeedModule colorBySpeed;
        public PersistentSizeOverLifetimeModule sizeOverLifetime;
        public PersistentSizeBySpeedModule sizeBySpeed;
        public PersistentRotationOverLifetimeModule rotationOverLifetime;
        public PersistentRotationBySpeedModule rotationBySpeed;
        public PersistentExternalForcesModule externalForces;
        public PersistentSubEmittersModule subEmitters;
        public PersistentTextureSheetAnimationModule textureSheetAnimation;
        public PersistentLightsModule lights;
        public PersistentMainModule main;
        public PersistentNoiseModule noise;
        public PersistentTrailModule trails;
#endif

    }
}

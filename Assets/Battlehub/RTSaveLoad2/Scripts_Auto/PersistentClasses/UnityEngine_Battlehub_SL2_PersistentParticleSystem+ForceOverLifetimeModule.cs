using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentParticleSystemNestedForceOverLifetimeModule : PersistentSurrogate
    {
        public static implicit operator ParticleSystem.ForceOverLifetimeModule(PersistentParticleSystemNestedForceOverLifetimeModule surrogate)
        {
            return (ParticleSystem.ForceOverLifetimeModule)surrogate.WriteTo(new ParticleSystem.ForceOverLifetimeModule());
        }
        
        public static implicit operator PersistentParticleSystemNestedForceOverLifetimeModule(ParticleSystem.ForceOverLifetimeModule obj)
        {
            PersistentParticleSystemNestedForceOverLifetimeModule surrogate = new PersistentParticleSystemNestedForceOverLifetimeModule();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


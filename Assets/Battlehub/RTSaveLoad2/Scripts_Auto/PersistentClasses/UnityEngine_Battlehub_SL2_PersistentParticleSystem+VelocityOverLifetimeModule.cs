using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentParticleSystemNestedVelocityOverLifetimeModule : PersistentSurrogate
    {
        public static implicit operator ParticleSystem.VelocityOverLifetimeModule(PersistentParticleSystemNestedVelocityOverLifetimeModule surrogate)
        {
            return (ParticleSystem.VelocityOverLifetimeModule)surrogate.WriteTo(new ParticleSystem.VelocityOverLifetimeModule());
        }
        
        public static implicit operator PersistentParticleSystemNestedVelocityOverLifetimeModule(ParticleSystem.VelocityOverLifetimeModule obj)
        {
            PersistentParticleSystemNestedVelocityOverLifetimeModule surrogate = new PersistentParticleSystemNestedVelocityOverLifetimeModule();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


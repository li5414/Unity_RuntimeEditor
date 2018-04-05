using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentParticleSystemNestedLimitVelocityOverLifetimeModule : PersistentSurrogate
    {
        public static implicit operator ParticleSystem.LimitVelocityOverLifetimeModule(PersistentParticleSystemNestedLimitVelocityOverLifetimeModule surrogate)
        {
            return (ParticleSystem.LimitVelocityOverLifetimeModule)surrogate.WriteTo(new ParticleSystem.LimitVelocityOverLifetimeModule());
        }
        
        public static implicit operator PersistentParticleSystemNestedLimitVelocityOverLifetimeModule(ParticleSystem.LimitVelocityOverLifetimeModule obj)
        {
            PersistentParticleSystemNestedLimitVelocityOverLifetimeModule surrogate = new PersistentParticleSystemNestedLimitVelocityOverLifetimeModule();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


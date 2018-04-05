using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentParticleSystemNestedRotationOverLifetimeModule : PersistentSurrogate
    {
        public static implicit operator ParticleSystem.RotationOverLifetimeModule(PersistentParticleSystemNestedRotationOverLifetimeModule surrogate)
        {
            return (ParticleSystem.RotationOverLifetimeModule)surrogate.WriteTo(new ParticleSystem.RotationOverLifetimeModule());
        }
        
        public static implicit operator PersistentParticleSystemNestedRotationOverLifetimeModule(ParticleSystem.RotationOverLifetimeModule obj)
        {
            PersistentParticleSystemNestedRotationOverLifetimeModule surrogate = new PersistentParticleSystemNestedRotationOverLifetimeModule();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentParticleSystemNestedColorOverLifetimeModule : PersistentSurrogate
    {
        public static implicit operator ParticleSystem.ColorOverLifetimeModule(PersistentParticleSystemNestedColorOverLifetimeModule surrogate)
        {
            return (ParticleSystem.ColorOverLifetimeModule)surrogate.WriteTo(new ParticleSystem.ColorOverLifetimeModule());
        }
        
        public static implicit operator PersistentParticleSystemNestedColorOverLifetimeModule(ParticleSystem.ColorOverLifetimeModule obj)
        {
            PersistentParticleSystemNestedColorOverLifetimeModule surrogate = new PersistentParticleSystemNestedColorOverLifetimeModule();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentParticleSystemNestedNoiseModule : PersistentSurrogate
    {
        public static implicit operator ParticleSystem.NoiseModule(PersistentParticleSystemNestedNoiseModule surrogate)
        {
            return (ParticleSystem.NoiseModule)surrogate.WriteTo(new ParticleSystem.NoiseModule());
        }
        
        public static implicit operator PersistentParticleSystemNestedNoiseModule(ParticleSystem.NoiseModule obj)
        {
            PersistentParticleSystemNestedNoiseModule surrogate = new PersistentParticleSystemNestedNoiseModule();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


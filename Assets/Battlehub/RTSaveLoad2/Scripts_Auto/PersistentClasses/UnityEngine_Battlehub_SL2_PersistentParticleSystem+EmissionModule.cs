using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentParticleSystemNestedEmissionModule : PersistentSurrogate
    {
        public static implicit operator ParticleSystem.EmissionModule(PersistentParticleSystemNestedEmissionModule surrogate)
        {
            return (ParticleSystem.EmissionModule)surrogate.WriteTo(new ParticleSystem.EmissionModule());
        }
        
        public static implicit operator PersistentParticleSystemNestedEmissionModule(ParticleSystem.EmissionModule obj)
        {
            PersistentParticleSystemNestedEmissionModule surrogate = new PersistentParticleSystemNestedEmissionModule();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


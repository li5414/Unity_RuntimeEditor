using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentParticleSystemNestedLightsModule : PersistentSurrogate
    {
        public static implicit operator ParticleSystem.LightsModule(PersistentParticleSystemNestedLightsModule surrogate)
        {
            return (ParticleSystem.LightsModule)surrogate.WriteTo(new ParticleSystem.LightsModule());
        }
        
        public static implicit operator PersistentParticleSystemNestedLightsModule(ParticleSystem.LightsModule obj)
        {
            PersistentParticleSystemNestedLightsModule surrogate = new PersistentParticleSystemNestedLightsModule();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


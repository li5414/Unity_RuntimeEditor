using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentParticleSystemNestedExternalForcesModule : PersistentSurrogate
    {
        public static implicit operator ParticleSystem.ExternalForcesModule(PersistentParticleSystemNestedExternalForcesModule surrogate)
        {
            return (ParticleSystem.ExternalForcesModule)surrogate.WriteTo(new ParticleSystem.ExternalForcesModule());
        }
        
        public static implicit operator PersistentParticleSystemNestedExternalForcesModule(ParticleSystem.ExternalForcesModule obj)
        {
            PersistentParticleSystemNestedExternalForcesModule surrogate = new PersistentParticleSystemNestedExternalForcesModule();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


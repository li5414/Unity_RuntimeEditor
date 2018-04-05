using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentParticleSystemNestedSubEmittersModule : PersistentSurrogate
    {
        public static implicit operator ParticleSystem.SubEmittersModule(PersistentParticleSystemNestedSubEmittersModule surrogate)
        {
            return (ParticleSystem.SubEmittersModule)surrogate.WriteTo(new ParticleSystem.SubEmittersModule());
        }
        
        public static implicit operator PersistentParticleSystemNestedSubEmittersModule(ParticleSystem.SubEmittersModule obj)
        {
            PersistentParticleSystemNestedSubEmittersModule surrogate = new PersistentParticleSystemNestedSubEmittersModule();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


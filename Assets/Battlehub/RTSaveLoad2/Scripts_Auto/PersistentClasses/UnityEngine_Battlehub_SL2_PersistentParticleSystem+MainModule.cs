using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentParticleSystemNestedMainModule : PersistentSurrogate
    {
        public static implicit operator ParticleSystem.MainModule(PersistentParticleSystemNestedMainModule surrogate)
        {
            return (ParticleSystem.MainModule)surrogate.WriteTo(new ParticleSystem.MainModule());
        }
        
        public static implicit operator PersistentParticleSystemNestedMainModule(ParticleSystem.MainModule obj)
        {
            PersistentParticleSystemNestedMainModule surrogate = new PersistentParticleSystemNestedMainModule();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


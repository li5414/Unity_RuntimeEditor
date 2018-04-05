using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentParticleSystemNestedSizeBySpeedModule : PersistentSurrogate
    {
        public static implicit operator ParticleSystem.SizeBySpeedModule(PersistentParticleSystemNestedSizeBySpeedModule surrogate)
        {
            return (ParticleSystem.SizeBySpeedModule)surrogate.WriteTo(new ParticleSystem.SizeBySpeedModule());
        }
        
        public static implicit operator PersistentParticleSystemNestedSizeBySpeedModule(ParticleSystem.SizeBySpeedModule obj)
        {
            PersistentParticleSystemNestedSizeBySpeedModule surrogate = new PersistentParticleSystemNestedSizeBySpeedModule();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


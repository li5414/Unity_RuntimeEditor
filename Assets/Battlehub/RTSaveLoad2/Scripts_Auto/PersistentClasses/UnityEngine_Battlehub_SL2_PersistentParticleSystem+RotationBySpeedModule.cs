using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentParticleSystemNestedRotationBySpeedModule : PersistentSurrogate
    {
        public static implicit operator ParticleSystem.RotationBySpeedModule(PersistentParticleSystemNestedRotationBySpeedModule surrogate)
        {
            return (ParticleSystem.RotationBySpeedModule)surrogate.WriteTo(new ParticleSystem.RotationBySpeedModule());
        }
        
        public static implicit operator PersistentParticleSystemNestedRotationBySpeedModule(ParticleSystem.RotationBySpeedModule obj)
        {
            PersistentParticleSystemNestedRotationBySpeedModule surrogate = new PersistentParticleSystemNestedRotationBySpeedModule();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


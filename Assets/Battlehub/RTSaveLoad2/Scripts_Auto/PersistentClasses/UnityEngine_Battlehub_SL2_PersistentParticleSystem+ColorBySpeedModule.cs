using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentParticleSystemNestedColorBySpeedModule : PersistentSurrogate
    {
        public static implicit operator ParticleSystem.ColorBySpeedModule(PersistentParticleSystemNestedColorBySpeedModule surrogate)
        {
            return (ParticleSystem.ColorBySpeedModule)surrogate.WriteTo(new ParticleSystem.ColorBySpeedModule());
        }
        
        public static implicit operator PersistentParticleSystemNestedColorBySpeedModule(ParticleSystem.ColorBySpeedModule obj)
        {
            PersistentParticleSystemNestedColorBySpeedModule surrogate = new PersistentParticleSystemNestedColorBySpeedModule();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


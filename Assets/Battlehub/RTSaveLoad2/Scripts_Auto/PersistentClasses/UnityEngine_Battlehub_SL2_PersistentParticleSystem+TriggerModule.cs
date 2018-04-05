using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentParticleSystemNestedTriggerModule : PersistentSurrogate
    {
        public static implicit operator ParticleSystem.TriggerModule(PersistentParticleSystemNestedTriggerModule surrogate)
        {
            return (ParticleSystem.TriggerModule)surrogate.WriteTo(new ParticleSystem.TriggerModule());
        }
        
        public static implicit operator PersistentParticleSystemNestedTriggerModule(ParticleSystem.TriggerModule obj)
        {
            PersistentParticleSystemNestedTriggerModule surrogate = new PersistentParticleSystemNestedTriggerModule();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


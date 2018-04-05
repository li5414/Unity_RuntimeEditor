using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentParticleSystemNestedMinMaxGradient : PersistentSurrogate
    {
        public static implicit operator ParticleSystem.MinMaxGradient(PersistentParticleSystemNestedMinMaxGradient surrogate)
        {
            return (ParticleSystem.MinMaxGradient)surrogate.WriteTo(new ParticleSystem.MinMaxGradient());
        }
        
        public static implicit operator PersistentParticleSystemNestedMinMaxGradient(ParticleSystem.MinMaxGradient obj)
        {
            PersistentParticleSystemNestedMinMaxGradient surrogate = new PersistentParticleSystemNestedMinMaxGradient();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


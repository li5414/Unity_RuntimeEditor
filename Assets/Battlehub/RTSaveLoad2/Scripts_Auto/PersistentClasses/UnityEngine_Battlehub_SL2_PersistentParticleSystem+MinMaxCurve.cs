using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentParticleSystemNestedMinMaxCurve : PersistentSurrogate
    {
        public static implicit operator ParticleSystem.MinMaxCurve(PersistentParticleSystemNestedMinMaxCurve surrogate)
        {
            return (ParticleSystem.MinMaxCurve)surrogate.WriteTo(new ParticleSystem.MinMaxCurve());
        }
        
        public static implicit operator PersistentParticleSystemNestedMinMaxCurve(ParticleSystem.MinMaxCurve obj)
        {
            PersistentParticleSystemNestedMinMaxCurve surrogate = new PersistentParticleSystemNestedMinMaxCurve();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


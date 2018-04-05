using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentParticleSystemNestedTrailModule : PersistentSurrogate
    {
        public static implicit operator ParticleSystem.TrailModule(PersistentParticleSystemNestedTrailModule surrogate)
        {
            return (ParticleSystem.TrailModule)surrogate.WriteTo(new ParticleSystem.TrailModule());
        }
        
        public static implicit operator PersistentParticleSystemNestedTrailModule(ParticleSystem.TrailModule obj)
        {
            PersistentParticleSystemNestedTrailModule surrogate = new PersistentParticleSystemNestedTrailModule();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentParticleSystemNestedShapeModule : PersistentSurrogate
    {
        public static implicit operator ParticleSystem.ShapeModule(PersistentParticleSystemNestedShapeModule surrogate)
        {
            return (ParticleSystem.ShapeModule)surrogate.WriteTo(new ParticleSystem.ShapeModule());
        }
        
        public static implicit operator PersistentParticleSystemNestedShapeModule(ParticleSystem.ShapeModule obj)
        {
            PersistentParticleSystemNestedShapeModule surrogate = new PersistentParticleSystemNestedShapeModule();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


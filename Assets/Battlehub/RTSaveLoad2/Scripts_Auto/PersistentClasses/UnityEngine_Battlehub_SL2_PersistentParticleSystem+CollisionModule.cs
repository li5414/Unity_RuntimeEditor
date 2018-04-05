using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentParticleSystemNestedCollisionModule : PersistentSurrogate
    {
        public static implicit operator ParticleSystem.CollisionModule(PersistentParticleSystemNestedCollisionModule surrogate)
        {
            return (ParticleSystem.CollisionModule)surrogate.WriteTo(new ParticleSystem.CollisionModule());
        }
        
        public static implicit operator PersistentParticleSystemNestedCollisionModule(ParticleSystem.CollisionModule obj)
        {
            PersistentParticleSystemNestedCollisionModule surrogate = new PersistentParticleSystemNestedCollisionModule();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


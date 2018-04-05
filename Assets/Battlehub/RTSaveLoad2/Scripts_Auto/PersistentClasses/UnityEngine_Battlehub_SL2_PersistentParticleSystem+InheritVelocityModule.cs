using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentParticleSystemNestedInheritVelocityModule : PersistentSurrogate
    {
        public static implicit operator ParticleSystem.InheritVelocityModule(PersistentParticleSystemNestedInheritVelocityModule surrogate)
        {
            return (ParticleSystem.InheritVelocityModule)surrogate.WriteTo(new ParticleSystem.InheritVelocityModule());
        }
        
        public static implicit operator PersistentParticleSystemNestedInheritVelocityModule(ParticleSystem.InheritVelocityModule obj)
        {
            PersistentParticleSystemNestedInheritVelocityModule surrogate = new PersistentParticleSystemNestedInheritVelocityModule();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


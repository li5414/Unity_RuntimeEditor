using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentParticle : PersistentSurrogate
    {
        public static implicit operator Particle(PersistentParticle surrogate)
        {
            return (Particle)surrogate.WriteTo(new Particle());
        }
        
        public static implicit operator PersistentParticle(Particle obj)
        {
            PersistentParticle surrogate = new PersistentParticle();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


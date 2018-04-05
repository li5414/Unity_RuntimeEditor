using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentParticleSystemNestedSizeOverLifetimeModule : PersistentSurrogate
    {
        public static implicit operator ParticleSystem.SizeOverLifetimeModule(PersistentParticleSystemNestedSizeOverLifetimeModule surrogate)
        {
            return (ParticleSystem.SizeOverLifetimeModule)surrogate.WriteTo(new ParticleSystem.SizeOverLifetimeModule());
        }
        
        public static implicit operator PersistentParticleSystemNestedSizeOverLifetimeModule(ParticleSystem.SizeOverLifetimeModule obj)
        {
            PersistentParticleSystemNestedSizeOverLifetimeModule surrogate = new PersistentParticleSystemNestedSizeOverLifetimeModule();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


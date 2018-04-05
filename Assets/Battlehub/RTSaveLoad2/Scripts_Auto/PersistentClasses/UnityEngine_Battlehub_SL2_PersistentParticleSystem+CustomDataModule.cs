using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentParticleSystemNestedCustomDataModule : PersistentSurrogate
    {
        public static implicit operator ParticleSystem.CustomDataModule(PersistentParticleSystemNestedCustomDataModule surrogate)
        {
            return (ParticleSystem.CustomDataModule)surrogate.WriteTo(new ParticleSystem.CustomDataModule());
        }
        
        public static implicit operator PersistentParticleSystemNestedCustomDataModule(ParticleSystem.CustomDataModule obj)
        {
            PersistentParticleSystemNestedCustomDataModule surrogate = new PersistentParticleSystemNestedCustomDataModule();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentParticleSystemNestedTextureSheetAnimationModule : PersistentSurrogate
    {
        public static implicit operator ParticleSystem.TextureSheetAnimationModule(PersistentParticleSystemNestedTextureSheetAnimationModule surrogate)
        {
            return (ParticleSystem.TextureSheetAnimationModule)surrogate.WriteTo(new ParticleSystem.TextureSheetAnimationModule());
        }
        
        public static implicit operator PersistentParticleSystemNestedTextureSheetAnimationModule(ParticleSystem.TextureSheetAnimationModule obj)
        {
            PersistentParticleSystemNestedTextureSheetAnimationModule surrogate = new PersistentParticleSystemNestedTextureSheetAnimationModule();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


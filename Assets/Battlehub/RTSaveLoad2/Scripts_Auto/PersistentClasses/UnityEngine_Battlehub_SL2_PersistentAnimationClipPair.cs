using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentAnimationClipPair : PersistentSurrogate
    {
        public static implicit operator AnimationClipPair(PersistentAnimationClipPair surrogate)
        {
            return (AnimationClipPair)surrogate.WriteTo(new AnimationClipPair());
        }
        
        public static implicit operator PersistentAnimationClipPair(AnimationClipPair obj)
        {
            PersistentAnimationClipPair surrogate = new PersistentAnimationClipPair();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentAnimationState : PersistentSurrogate
    {
        public static implicit operator AnimationState(PersistentAnimationState surrogate)
        {
            return (AnimationState)surrogate.WriteTo(new AnimationState());
        }
        
        public static implicit operator PersistentAnimationState(AnimationState obj)
        {
            PersistentAnimationState surrogate = new PersistentAnimationState();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


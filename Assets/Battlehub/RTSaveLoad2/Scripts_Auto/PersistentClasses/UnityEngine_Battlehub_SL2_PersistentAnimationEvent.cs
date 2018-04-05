using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentAnimationEvent : PersistentSurrogate
    {
        public static implicit operator AnimationEvent(PersistentAnimationEvent surrogate)
        {
            return (AnimationEvent)surrogate.WriteTo(new AnimationEvent());
        }
        
        public static implicit operator PersistentAnimationEvent(AnimationEvent obj)
        {
            PersistentAnimationEvent surrogate = new PersistentAnimationEvent();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


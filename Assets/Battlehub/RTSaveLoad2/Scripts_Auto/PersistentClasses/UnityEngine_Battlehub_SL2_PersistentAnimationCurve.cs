using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentAnimationCurve : PersistentSurrogate
    {
        public static implicit operator AnimationCurve(PersistentAnimationCurve surrogate)
        {
            return (AnimationCurve)surrogate.WriteTo(new AnimationCurve());
        }
        
        public static implicit operator PersistentAnimationCurve(AnimationCurve obj)
        {
            PersistentAnimationCurve surrogate = new PersistentAnimationCurve();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


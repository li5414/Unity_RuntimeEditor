using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentAnimatorClipInfo : PersistentSurrogate
    {
        public static implicit operator AnimatorClipInfo(PersistentAnimatorClipInfo surrogate)
        {
            return (AnimatorClipInfo)surrogate.WriteTo(new AnimatorClipInfo());
        }
        
        public static implicit operator PersistentAnimatorClipInfo(AnimatorClipInfo obj)
        {
            PersistentAnimatorClipInfo surrogate = new PersistentAnimatorClipInfo();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


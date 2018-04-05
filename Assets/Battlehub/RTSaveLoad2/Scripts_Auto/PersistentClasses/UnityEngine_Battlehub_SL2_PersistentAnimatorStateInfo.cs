using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentAnimatorStateInfo : PersistentSurrogate
    {
        public static implicit operator AnimatorStateInfo(PersistentAnimatorStateInfo surrogate)
        {
            return (AnimatorStateInfo)surrogate.WriteTo(new AnimatorStateInfo());
        }
        
        public static implicit operator PersistentAnimatorStateInfo(AnimatorStateInfo obj)
        {
            PersistentAnimatorStateInfo surrogate = new PersistentAnimatorStateInfo();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


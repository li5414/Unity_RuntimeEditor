using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentAnimatorControllerParameter : PersistentSurrogate
    {
        public static implicit operator AnimatorControllerParameter(PersistentAnimatorControllerParameter surrogate)
        {
            return (AnimatorControllerParameter)surrogate.WriteTo(new AnimatorControllerParameter());
        }
        
        public static implicit operator PersistentAnimatorControllerParameter(AnimatorControllerParameter obj)
        {
            PersistentAnimatorControllerParameter surrogate = new PersistentAnimatorControllerParameter();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


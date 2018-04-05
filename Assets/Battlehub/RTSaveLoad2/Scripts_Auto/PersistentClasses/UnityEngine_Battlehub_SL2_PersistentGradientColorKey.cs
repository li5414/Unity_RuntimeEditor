using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentGradientColorKey : PersistentSurrogate
    {
        public static implicit operator GradientColorKey(PersistentGradientColorKey surrogate)
        {
            return (GradientColorKey)surrogate.WriteTo(new GradientColorKey());
        }
        
        public static implicit operator PersistentGradientColorKey(GradientColorKey obj)
        {
            PersistentGradientColorKey surrogate = new PersistentGradientColorKey();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


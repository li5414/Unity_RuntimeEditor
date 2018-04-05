using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentGradient : PersistentSurrogate
    {
        public static implicit operator Gradient(PersistentGradient surrogate)
        {
            return (Gradient)surrogate.WriteTo(new Gradient());
        }
        
        public static implicit operator PersistentGradient(Gradient obj)
        {
            PersistentGradient surrogate = new PersistentGradient();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


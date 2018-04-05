using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentBounds : PersistentSurrogate
    {
        public static implicit operator Bounds(PersistentBounds surrogate)
        {
            return (Bounds)surrogate.WriteTo(new Bounds());
        }
        
        public static implicit operator PersistentBounds(Bounds obj)
        {
            PersistentBounds surrogate = new PersistentBounds();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


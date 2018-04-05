using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentBoundsInt : PersistentSurrogate
    {
        public static implicit operator BoundsInt(PersistentBoundsInt surrogate)
        {
            return (BoundsInt)surrogate.WriteTo(new BoundsInt());
        }
        
        public static implicit operator PersistentBoundsInt(BoundsInt obj)
        {
            PersistentBoundsInt surrogate = new PersistentBoundsInt();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


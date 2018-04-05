using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentBoundsIntNestedPositionEnumerator : PersistentSurrogate
    {
        public static implicit operator BoundsInt.PositionEnumerator(PersistentBoundsIntNestedPositionEnumerator surrogate)
        {
            return (BoundsInt.PositionEnumerator)surrogate.WriteTo(new BoundsInt.PositionEnumerator());
        }
        
        public static implicit operator PersistentBoundsIntNestedPositionEnumerator(BoundsInt.PositionEnumerator obj)
        {
            PersistentBoundsIntNestedPositionEnumerator surrogate = new PersistentBoundsIntNestedPositionEnumerator();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


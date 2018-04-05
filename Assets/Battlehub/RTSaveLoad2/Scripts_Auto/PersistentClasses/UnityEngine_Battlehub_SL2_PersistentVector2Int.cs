using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentVector2Int : PersistentSurrogate
    {
        public static implicit operator Vector2Int(PersistentVector2Int surrogate)
        {
            return (Vector2Int)surrogate.WriteTo(new Vector2Int());
        }
        
        public static implicit operator PersistentVector2Int(Vector2Int obj)
        {
            PersistentVector2Int surrogate = new PersistentVector2Int();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


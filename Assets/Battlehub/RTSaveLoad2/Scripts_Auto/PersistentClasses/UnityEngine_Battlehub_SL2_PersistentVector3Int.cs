using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentVector3Int : PersistentSurrogate
    {
        public static implicit operator Vector3Int(PersistentVector3Int surrogate)
        {
            return (Vector3Int)surrogate.WriteTo(new Vector3Int());
        }
        
        public static implicit operator PersistentVector3Int(Vector3Int obj)
        {
            PersistentVector3Int surrogate = new PersistentVector3Int();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


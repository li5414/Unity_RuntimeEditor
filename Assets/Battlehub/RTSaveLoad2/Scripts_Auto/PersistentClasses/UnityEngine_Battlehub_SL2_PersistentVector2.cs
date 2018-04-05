using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentVector2 : PersistentSurrogate
    {
        public static implicit operator Vector2(PersistentVector2 surrogate)
        {
            return (Vector2)surrogate.WriteTo(new Vector2());
        }
        
        public static implicit operator PersistentVector2(Vector2 obj)
        {
            PersistentVector2 surrogate = new PersistentVector2();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


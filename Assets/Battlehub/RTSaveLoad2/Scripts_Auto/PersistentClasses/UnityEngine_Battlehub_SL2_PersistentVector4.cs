using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentVector4 : PersistentSurrogate
    {
        public static implicit operator Vector4(PersistentVector4 surrogate)
        {
            return (Vector4)surrogate.WriteTo(new Vector4());
        }
        
        public static implicit operator PersistentVector4(Vector4 obj)
        {
            PersistentVector4 surrogate = new PersistentVector4();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


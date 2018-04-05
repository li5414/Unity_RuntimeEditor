using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentVector3 : PersistentSurrogate
    {
        public static implicit operator Vector3(PersistentVector3 surrogate)
        {
            return (Vector3)surrogate.WriteTo(new Vector3());
        }
        
        public static implicit operator PersistentVector3(Vector3 obj)
        {
            PersistentVector3 surrogate = new PersistentVector3();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


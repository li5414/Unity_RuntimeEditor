using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentHash128 : PersistentSurrogate
    {
        public static implicit operator Hash128(PersistentHash128 surrogate)
        {
            return (Hash128)surrogate.WriteTo(new Hash128());
        }
        
        public static implicit operator PersistentHash128(Hash128 obj)
        {
            PersistentHash128 surrogate = new PersistentHash128();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


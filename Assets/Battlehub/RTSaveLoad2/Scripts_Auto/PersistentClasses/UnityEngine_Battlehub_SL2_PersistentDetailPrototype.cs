using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentDetailPrototype : PersistentSurrogate
    {
        public static implicit operator DetailPrototype(PersistentDetailPrototype surrogate)
        {
            return (DetailPrototype)surrogate.WriteTo(new DetailPrototype());
        }
        
        public static implicit operator PersistentDetailPrototype(DetailPrototype obj)
        {
            PersistentDetailPrototype surrogate = new PersistentDetailPrototype();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


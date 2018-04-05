using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentSplatPrototype : PersistentSurrogate
    {
        public static implicit operator SplatPrototype(PersistentSplatPrototype surrogate)
        {
            return (SplatPrototype)surrogate.WriteTo(new SplatPrototype());
        }
        
        public static implicit operator PersistentSplatPrototype(SplatPrototype obj)
        {
            PersistentSplatPrototype surrogate = new PersistentSplatPrototype();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentTreePrototype : PersistentSurrogate
    {
        public static implicit operator TreePrototype(PersistentTreePrototype surrogate)
        {
            return (TreePrototype)surrogate.WriteTo(new TreePrototype());
        }
        
        public static implicit operator PersistentTreePrototype(TreePrototype obj)
        {
            PersistentTreePrototype surrogate = new PersistentTreePrototype();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


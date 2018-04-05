using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentTreeInstance : PersistentSurrogate
    {
        public static implicit operator TreeInstance(PersistentTreeInstance surrogate)
        {
            return (TreeInstance)surrogate.WriteTo(new TreeInstance());
        }
        
        public static implicit operator PersistentTreeInstance(TreeInstance obj)
        {
            PersistentTreeInstance surrogate = new PersistentTreeInstance();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


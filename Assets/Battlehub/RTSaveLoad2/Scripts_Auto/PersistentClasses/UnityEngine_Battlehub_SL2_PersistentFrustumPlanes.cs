using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentFrustumPlanes : PersistentSurrogate
    {
        public static implicit operator FrustumPlanes(PersistentFrustumPlanes surrogate)
        {
            return (FrustumPlanes)surrogate.WriteTo(new FrustumPlanes());
        }
        
        public static implicit operator PersistentFrustumPlanes(FrustumPlanes obj)
        {
            PersistentFrustumPlanes surrogate = new PersistentFrustumPlanes();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentNetworkViewID : PersistentSurrogate
    {
        public static implicit operator NetworkViewID(PersistentNetworkViewID surrogate)
        {
            return (NetworkViewID)surrogate.WriteTo(new NetworkViewID());
        }
        
        public static implicit operator PersistentNetworkViewID(NetworkViewID obj)
        {
            PersistentNetworkViewID surrogate = new PersistentNetworkViewID();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


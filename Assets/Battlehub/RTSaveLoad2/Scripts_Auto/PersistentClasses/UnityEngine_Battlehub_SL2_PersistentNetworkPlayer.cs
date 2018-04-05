using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentNetworkPlayer : PersistentSurrogate
    {
        public static implicit operator NetworkPlayer(PersistentNetworkPlayer surrogate)
        {
            return (NetworkPlayer)surrogate.WriteTo(new NetworkPlayer());
        }
        
        public static implicit operator PersistentNetworkPlayer(NetworkPlayer obj)
        {
            PersistentNetworkPlayer surrogate = new PersistentNetworkPlayer();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine.Networking;
using UnityEngine.Networking.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Networking.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentNetworkInstanceId : PersistentSurrogate
    {
        public static implicit operator NetworkInstanceId(PersistentNetworkInstanceId surrogate)
        {
            return (NetworkInstanceId)surrogate.WriteTo(new NetworkInstanceId());
        }
        
        public static implicit operator PersistentNetworkInstanceId(NetworkInstanceId obj)
        {
            PersistentNetworkInstanceId surrogate = new PersistentNetworkInstanceId();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


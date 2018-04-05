using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine.Networking;
using UnityEngine.Networking.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Networking.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentNetworkConnection : PersistentSurrogate
    {
        public static implicit operator NetworkConnection(PersistentNetworkConnection surrogate)
        {
            return (NetworkConnection)surrogate.WriteTo(new NetworkConnection());
        }
        
        public static implicit operator PersistentNetworkConnection(NetworkConnection obj)
        {
            PersistentNetworkConnection surrogate = new PersistentNetworkConnection();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


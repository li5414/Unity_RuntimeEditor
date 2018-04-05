using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine.Networking;
using UnityEngine.Networking.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Networking.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentNetworkClient : PersistentSurrogate
    {
        public static implicit operator NetworkClient(PersistentNetworkClient surrogate)
        {
            return (NetworkClient)surrogate.WriteTo(new NetworkClient());
        }
        
        public static implicit operator PersistentNetworkClient(NetworkClient obj)
        {
            PersistentNetworkClient surrogate = new PersistentNetworkClient();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


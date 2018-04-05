using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine.Networking;
using UnityEngine.Networking.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Networking.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentNetworkHash128 : PersistentSurrogate
    {
        public static implicit operator NetworkHash128(PersistentNetworkHash128 surrogate)
        {
            return (NetworkHash128)surrogate.WriteTo(new NetworkHash128());
        }
        
        public static implicit operator PersistentNetworkHash128(NetworkHash128 obj)
        {
            PersistentNetworkHash128 surrogate = new PersistentNetworkHash128();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


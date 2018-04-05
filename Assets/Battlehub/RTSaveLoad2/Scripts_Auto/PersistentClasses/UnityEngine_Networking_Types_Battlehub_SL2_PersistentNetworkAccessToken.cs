using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Types.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Networking.Types.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentNetworkAccessToken : PersistentSurrogate
    {
        public static implicit operator NetworkAccessToken(PersistentNetworkAccessToken surrogate)
        {
            return (NetworkAccessToken)surrogate.WriteTo(new NetworkAccessToken());
        }
        
        public static implicit operator PersistentNetworkAccessToken(NetworkAccessToken obj)
        {
            PersistentNetworkAccessToken surrogate = new PersistentNetworkAccessToken();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


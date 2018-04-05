using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine.Networking;
using UnityEngine.Networking.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Networking.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentNetworkSceneId : PersistentSurrogate
    {
        public static implicit operator NetworkSceneId(PersistentNetworkSceneId surrogate)
        {
            return (NetworkSceneId)surrogate.WriteTo(new NetworkSceneId());
        }
        
        public static implicit operator PersistentNetworkSceneId(NetworkSceneId obj)
        {
            PersistentNetworkSceneId surrogate = new PersistentNetworkSceneId();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.Networking.NetworkSystem.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Networking.NetworkSystem.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentPeerInfoMessage : PersistentSurrogate
    {
        public static implicit operator PeerInfoMessage(PersistentPeerInfoMessage surrogate)
        {
            return (PeerInfoMessage)surrogate.WriteTo(new PeerInfoMessage());
        }
        
        public static implicit operator PersistentPeerInfoMessage(PeerInfoMessage obj)
        {
            PersistentPeerInfoMessage surrogate = new PersistentPeerInfoMessage();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


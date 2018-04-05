using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.Networking.NetworkSystem.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Networking.NetworkSystem.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentPeerInfoPlayer : PersistentSurrogate
    {
        public static implicit operator PeerInfoPlayer(PersistentPeerInfoPlayer surrogate)
        {
            return (PeerInfoPlayer)surrogate.WriteTo(new PeerInfoPlayer());
        }
        
        public static implicit operator PersistentPeerInfoPlayer(PeerInfoPlayer obj)
        {
            PersistentPeerInfoPlayer surrogate = new PersistentPeerInfoPlayer();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


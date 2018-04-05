using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine.Networking;
using UnityEngine.Networking.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Networking.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentConnectionConfig : PersistentSurrogate
    {
        public static implicit operator ConnectionConfig(PersistentConnectionConfig surrogate)
        {
            return (ConnectionConfig)surrogate.WriteTo(new ConnectionConfig());
        }
        
        public static implicit operator PersistentConnectionConfig(ConnectionConfig obj)
        {
            PersistentConnectionConfig surrogate = new PersistentConnectionConfig();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


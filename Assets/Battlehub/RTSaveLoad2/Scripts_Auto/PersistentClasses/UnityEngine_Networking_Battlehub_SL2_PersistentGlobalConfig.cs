using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine.Networking;
using UnityEngine.Networking.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Networking.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentGlobalConfig : PersistentSurrogate
    {
        public static implicit operator GlobalConfig(PersistentGlobalConfig surrogate)
        {
            return (GlobalConfig)surrogate.WriteTo(new GlobalConfig());
        }
        
        public static implicit operator PersistentGlobalConfig(GlobalConfig obj)
        {
            PersistentGlobalConfig surrogate = new PersistentGlobalConfig();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


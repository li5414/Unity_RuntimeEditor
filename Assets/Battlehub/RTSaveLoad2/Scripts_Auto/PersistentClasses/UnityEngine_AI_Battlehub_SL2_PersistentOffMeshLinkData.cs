using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine.AI;
using UnityEngine.AI.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.AI.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentOffMeshLinkData : PersistentSurrogate
    {
        public static implicit operator OffMeshLinkData(PersistentOffMeshLinkData surrogate)
        {
            return (OffMeshLinkData)surrogate.WriteTo(new OffMeshLinkData());
        }
        
        public static implicit operator PersistentOffMeshLinkData(OffMeshLinkData obj)
        {
            PersistentOffMeshLinkData surrogate = new PersistentOffMeshLinkData();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


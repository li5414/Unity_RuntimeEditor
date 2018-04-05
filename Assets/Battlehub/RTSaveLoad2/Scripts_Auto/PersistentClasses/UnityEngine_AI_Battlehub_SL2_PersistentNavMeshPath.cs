using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine.AI;
using UnityEngine.AI.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.AI.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentNavMeshPath : PersistentSurrogate
    {
        public static implicit operator NavMeshPath(PersistentNavMeshPath surrogate)
        {
            return (NavMeshPath)surrogate.WriteTo(new NavMeshPath());
        }
        
        public static implicit operator PersistentNavMeshPath(NavMeshPath obj)
        {
            PersistentNavMeshPath surrogate = new PersistentNavMeshPath();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


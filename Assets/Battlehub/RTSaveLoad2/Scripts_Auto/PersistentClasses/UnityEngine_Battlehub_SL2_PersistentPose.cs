using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentPose : PersistentSurrogate
    {
        public static implicit operator Pose(PersistentPose surrogate)
        {
            return (Pose)surrogate.WriteTo(new Pose());
        }
        
        public static implicit operator PersistentPose(Pose obj)
        {
            PersistentPose surrogate = new PersistentPose();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


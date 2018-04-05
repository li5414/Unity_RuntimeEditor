using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentBoneWeight : PersistentSurrogate
    {
        public static implicit operator BoneWeight(PersistentBoneWeight surrogate)
        {
            return (BoneWeight)surrogate.WriteTo(new BoneWeight());
        }
        
        public static implicit operator PersistentBoneWeight(BoneWeight obj)
        {
            PersistentBoneWeight surrogate = new PersistentBoneWeight();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


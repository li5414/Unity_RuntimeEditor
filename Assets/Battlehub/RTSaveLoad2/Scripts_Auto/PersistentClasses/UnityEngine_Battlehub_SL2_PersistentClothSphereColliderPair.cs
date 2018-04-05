using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentClothSphereColliderPair : PersistentSurrogate
    {
        public static implicit operator ClothSphereColliderPair(PersistentClothSphereColliderPair surrogate)
        {
            return (ClothSphereColliderPair)surrogate.WriteTo(new ClothSphereColliderPair());
        }
        
        public static implicit operator PersistentClothSphereColliderPair(ClothSphereColliderPair obj)
        {
            PersistentClothSphereColliderPair surrogate = new PersistentClothSphereColliderPair();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


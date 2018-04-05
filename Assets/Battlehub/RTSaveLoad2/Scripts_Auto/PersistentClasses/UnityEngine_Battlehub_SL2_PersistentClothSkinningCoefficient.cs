using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentClothSkinningCoefficient : PersistentSurrogate
    {
        public static implicit operator ClothSkinningCoefficient(PersistentClothSkinningCoefficient surrogate)
        {
            return (ClothSkinningCoefficient)surrogate.WriteTo(new ClothSkinningCoefficient());
        }
        
        public static implicit operator PersistentClothSkinningCoefficient(ClothSkinningCoefficient obj)
        {
            PersistentClothSkinningCoefficient surrogate = new PersistentClothSkinningCoefficient();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


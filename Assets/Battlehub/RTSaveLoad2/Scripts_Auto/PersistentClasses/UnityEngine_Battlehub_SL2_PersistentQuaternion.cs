using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentQuaternion : PersistentSurrogate
    {
        public static implicit operator Quaternion(PersistentQuaternion surrogate)
        {
            return (Quaternion)surrogate.WriteTo(new Quaternion());
        }
        
        public static implicit operator PersistentQuaternion(Quaternion obj)
        {
            PersistentQuaternion surrogate = new PersistentQuaternion();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


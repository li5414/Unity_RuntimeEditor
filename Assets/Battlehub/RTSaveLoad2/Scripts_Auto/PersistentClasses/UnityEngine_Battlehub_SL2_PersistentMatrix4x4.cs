using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentMatrix4x4 : PersistentSurrogate
    {
        public static implicit operator Matrix4x4(PersistentMatrix4x4 surrogate)
        {
            return (Matrix4x4)surrogate.WriteTo(new Matrix4x4());
        }
        
        public static implicit operator PersistentMatrix4x4(Matrix4x4 obj)
        {
            PersistentMatrix4x4 surrogate = new PersistentMatrix4x4();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


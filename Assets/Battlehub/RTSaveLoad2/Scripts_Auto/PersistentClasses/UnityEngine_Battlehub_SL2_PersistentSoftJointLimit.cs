using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentSoftJointLimit : PersistentSurrogate
    {
        public static implicit operator SoftJointLimit(PersistentSoftJointLimit surrogate)
        {
            return (SoftJointLimit)surrogate.WriteTo(new SoftJointLimit());
        }
        
        public static implicit operator PersistentSoftJointLimit(SoftJointLimit obj)
        {
            PersistentSoftJointLimit surrogate = new PersistentSoftJointLimit();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


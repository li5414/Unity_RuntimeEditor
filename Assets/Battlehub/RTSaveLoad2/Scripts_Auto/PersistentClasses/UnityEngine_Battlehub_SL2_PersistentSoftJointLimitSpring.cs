using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentSoftJointLimitSpring : PersistentSurrogate
    {
        public static implicit operator SoftJointLimitSpring(PersistentSoftJointLimitSpring surrogate)
        {
            return (SoftJointLimitSpring)surrogate.WriteTo(new SoftJointLimitSpring());
        }
        
        public static implicit operator PersistentSoftJointLimitSpring(SoftJointLimitSpring obj)
        {
            PersistentSoftJointLimitSpring surrogate = new PersistentSoftJointLimitSpring();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


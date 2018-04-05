using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentJointLimits : PersistentSurrogate
    {
        public static implicit operator JointLimits(PersistentJointLimits surrogate)
        {
            return (JointLimits)surrogate.WriteTo(new JointLimits());
        }
        
        public static implicit operator PersistentJointLimits(JointLimits obj)
        {
            PersistentJointLimits surrogate = new PersistentJointLimits();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


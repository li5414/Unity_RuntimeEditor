using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentJointAngleLimits2D : PersistentSurrogate
    {
        public static implicit operator JointAngleLimits2D(PersistentJointAngleLimits2D surrogate)
        {
            return (JointAngleLimits2D)surrogate.WriteTo(new JointAngleLimits2D());
        }
        
        public static implicit operator PersistentJointAngleLimits2D(JointAngleLimits2D obj)
        {
            PersistentJointAngleLimits2D surrogate = new PersistentJointAngleLimits2D();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


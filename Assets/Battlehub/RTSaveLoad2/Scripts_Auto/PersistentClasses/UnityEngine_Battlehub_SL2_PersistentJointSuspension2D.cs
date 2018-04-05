using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentJointSuspension2D : PersistentSurrogate
    {
        public static implicit operator JointSuspension2D(PersistentJointSuspension2D surrogate)
        {
            return (JointSuspension2D)surrogate.WriteTo(new JointSuspension2D());
        }
        
        public static implicit operator PersistentJointSuspension2D(JointSuspension2D obj)
        {
            PersistentJointSuspension2D surrogate = new PersistentJointSuspension2D();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentJointSpring : PersistentSurrogate
    {
        public static implicit operator JointSpring(PersistentJointSpring surrogate)
        {
            return (JointSpring)surrogate.WriteTo(new JointSpring());
        }
        
        public static implicit operator PersistentJointSpring(JointSpring obj)
        {
            PersistentJointSpring surrogate = new PersistentJointSpring();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


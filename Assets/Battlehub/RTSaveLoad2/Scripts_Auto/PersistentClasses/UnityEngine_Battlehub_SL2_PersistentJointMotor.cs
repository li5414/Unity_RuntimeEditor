using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentJointMotor : PersistentSurrogate
    {
        public static implicit operator JointMotor(PersistentJointMotor surrogate)
        {
            return (JointMotor)surrogate.WriteTo(new JointMotor());
        }
        
        public static implicit operator PersistentJointMotor(JointMotor obj)
        {
            PersistentJointMotor surrogate = new PersistentJointMotor();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


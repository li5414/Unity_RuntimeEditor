using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentJointMotor2D : PersistentSurrogate
    {
        public static implicit operator JointMotor2D(PersistentJointMotor2D surrogate)
        {
            return (JointMotor2D)surrogate.WriteTo(new JointMotor2D());
        }
        
        public static implicit operator PersistentJointMotor2D(JointMotor2D obj)
        {
            PersistentJointMotor2D surrogate = new PersistentJointMotor2D();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


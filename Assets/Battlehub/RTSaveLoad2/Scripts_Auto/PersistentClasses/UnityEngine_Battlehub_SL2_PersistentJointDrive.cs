using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentJointDrive : PersistentSurrogate
    {
        public static implicit operator JointDrive(PersistentJointDrive surrogate)
        {
            return (JointDrive)surrogate.WriteTo(new JointDrive());
        }
        
        public static implicit operator PersistentJointDrive(JointDrive obj)
        {
            PersistentJointDrive surrogate = new PersistentJointDrive();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentJointTranslationLimits2D : PersistentSurrogate
    {
        public static implicit operator JointTranslationLimits2D(PersistentJointTranslationLimits2D surrogate)
        {
            return (JointTranslationLimits2D)surrogate.WriteTo(new JointTranslationLimits2D());
        }
        
        public static implicit operator PersistentJointTranslationLimits2D(JointTranslationLimits2D obj)
        {
            PersistentJointTranslationLimits2D surrogate = new PersistentJointTranslationLimits2D();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


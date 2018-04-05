using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentWheelFrictionCurve : PersistentSurrogate
    {
        public static implicit operator WheelFrictionCurve(PersistentWheelFrictionCurve surrogate)
        {
            return (WheelFrictionCurve)surrogate.WriteTo(new WheelFrictionCurve());
        }
        
        public static implicit operator PersistentWheelFrictionCurve(WheelFrictionCurve obj)
        {
            PersistentWheelFrictionCurve surrogate = new PersistentWheelFrictionCurve();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


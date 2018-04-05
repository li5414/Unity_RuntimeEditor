using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentColor : PersistentSurrogate
    {
        public static implicit operator Color(PersistentColor surrogate)
        {
            return (Color)surrogate.WriteTo(new Color());
        }
        
        public static implicit operator PersistentColor(Color obj)
        {
            PersistentColor surrogate = new PersistentColor();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


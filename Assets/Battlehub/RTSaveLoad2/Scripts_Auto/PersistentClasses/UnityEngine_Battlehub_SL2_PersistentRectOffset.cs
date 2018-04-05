using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentRectOffset : PersistentSurrogate
    {
        public static implicit operator RectOffset(PersistentRectOffset surrogate)
        {
            return (RectOffset)surrogate.WriteTo(new RectOffset());
        }
        
        public static implicit operator PersistentRectOffset(RectOffset obj)
        {
            PersistentRectOffset surrogate = new PersistentRectOffset();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


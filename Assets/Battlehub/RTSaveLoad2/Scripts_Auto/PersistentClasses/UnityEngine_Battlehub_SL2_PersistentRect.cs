using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentRect : PersistentSurrogate
    {
        public static implicit operator Rect(PersistentRect surrogate)
        {
            return (Rect)surrogate.WriteTo(new Rect());
        }
        
        public static implicit operator PersistentRect(Rect obj)
        {
            PersistentRect surrogate = new PersistentRect();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


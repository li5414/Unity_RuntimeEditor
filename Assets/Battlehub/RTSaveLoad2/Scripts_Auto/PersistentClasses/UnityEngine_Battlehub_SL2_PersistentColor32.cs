using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentColor32 : PersistentSurrogate
    {
        public static implicit operator Color32(PersistentColor32 surrogate)
        {
            return (Color32)surrogate.WriteTo(new Color32());
        }
        
        public static implicit operator PersistentColor32(Color32 obj)
        {
            PersistentColor32 surrogate = new PersistentColor32();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


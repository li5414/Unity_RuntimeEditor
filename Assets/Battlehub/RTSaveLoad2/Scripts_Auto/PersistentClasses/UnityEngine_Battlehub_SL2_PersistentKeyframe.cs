using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentKeyframe : PersistentSurrogate
    {
        public static implicit operator Keyframe(PersistentKeyframe surrogate)
        {
            return (Keyframe)surrogate.WriteTo(new Keyframe());
        }
        
        public static implicit operator PersistentKeyframe(Keyframe obj)
        {
            PersistentKeyframe surrogate = new PersistentKeyframe();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


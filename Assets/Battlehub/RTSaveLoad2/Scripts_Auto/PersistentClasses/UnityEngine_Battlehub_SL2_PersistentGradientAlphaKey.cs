using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentGradientAlphaKey : PersistentSurrogate
    {
        public static implicit operator GradientAlphaKey(PersistentGradientAlphaKey surrogate)
        {
            return (GradientAlphaKey)surrogate.WriteTo(new GradientAlphaKey());
        }
        
        public static implicit operator PersistentGradientAlphaKey(GradientAlphaKey obj)
        {
            PersistentGradientAlphaKey surrogate = new PersistentGradientAlphaKey();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


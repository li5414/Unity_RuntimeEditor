using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine.UI;
using UnityEngine.UI.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.UI.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentAnimationTriggers : PersistentSurrogate
    {
        public static implicit operator AnimationTriggers(PersistentAnimationTriggers surrogate)
        {
            return (AnimationTriggers)surrogate.WriteTo(new AnimationTriggers());
        }
        
        public static implicit operator PersistentAnimationTriggers(AnimationTriggers obj)
        {
            PersistentAnimationTriggers surrogate = new PersistentAnimationTriggers();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


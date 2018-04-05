using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine.UI;
using UnityEngine.UI.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.UI.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentSpriteState : PersistentSurrogate
    {
        public static implicit operator SpriteState(PersistentSpriteState surrogate)
        {
            return (SpriteState)surrogate.WriteTo(new SpriteState());
        }
        
        public static implicit operator PersistentSpriteState(SpriteState obj)
        {
            PersistentSpriteState surrogate = new PersistentSpriteState();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


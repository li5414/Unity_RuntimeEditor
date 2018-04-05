using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine.UI;
using UnityEngine.UI.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.UI.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentToggleNestedToggleEvent : PersistentSurrogate
    {
        public static implicit operator Toggle.ToggleEvent(PersistentToggleNestedToggleEvent surrogate)
        {
            return (Toggle.ToggleEvent)surrogate.WriteTo(new Toggle.ToggleEvent());
        }
        
        public static implicit operator PersistentToggleNestedToggleEvent(Toggle.ToggleEvent obj)
        {
            PersistentToggleNestedToggleEvent surrogate = new PersistentToggleNestedToggleEvent();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


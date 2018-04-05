using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine.UI;
using UnityEngine.UI.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.UI.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentButtonNestedButtonClickedEvent : PersistentSurrogate
    {
        public static implicit operator Button.ButtonClickedEvent(PersistentButtonNestedButtonClickedEvent surrogate)
        {
            return (Button.ButtonClickedEvent)surrogate.WriteTo(new Button.ButtonClickedEvent());
        }
        
        public static implicit operator PersistentButtonNestedButtonClickedEvent(Button.ButtonClickedEvent obj)
        {
            PersistentButtonNestedButtonClickedEvent surrogate = new PersistentButtonNestedButtonClickedEvent();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


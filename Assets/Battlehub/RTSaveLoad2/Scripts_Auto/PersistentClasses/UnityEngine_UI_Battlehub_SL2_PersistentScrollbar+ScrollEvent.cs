using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine.UI;
using UnityEngine.UI.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.UI.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentScrollbarNestedScrollEvent : PersistentSurrogate
    {
        public static implicit operator Scrollbar.ScrollEvent(PersistentScrollbarNestedScrollEvent surrogate)
        {
            return (Scrollbar.ScrollEvent)surrogate.WriteTo(new Scrollbar.ScrollEvent());
        }
        
        public static implicit operator PersistentScrollbarNestedScrollEvent(Scrollbar.ScrollEvent obj)
        {
            PersistentScrollbarNestedScrollEvent surrogate = new PersistentScrollbarNestedScrollEvent();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


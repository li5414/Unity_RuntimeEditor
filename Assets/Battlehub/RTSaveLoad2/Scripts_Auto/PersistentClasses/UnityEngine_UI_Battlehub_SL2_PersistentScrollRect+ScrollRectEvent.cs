using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine.UI;
using UnityEngine.UI.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.UI.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentScrollRectNestedScrollRectEvent : PersistentSurrogate
    {
        public static implicit operator ScrollRect.ScrollRectEvent(PersistentScrollRectNestedScrollRectEvent surrogate)
        {
            return (ScrollRect.ScrollRectEvent)surrogate.WriteTo(new ScrollRect.ScrollRectEvent());
        }
        
        public static implicit operator PersistentScrollRectNestedScrollRectEvent(ScrollRect.ScrollRectEvent obj)
        {
            PersistentScrollRectNestedScrollRectEvent surrogate = new PersistentScrollRectNestedScrollRectEvent();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


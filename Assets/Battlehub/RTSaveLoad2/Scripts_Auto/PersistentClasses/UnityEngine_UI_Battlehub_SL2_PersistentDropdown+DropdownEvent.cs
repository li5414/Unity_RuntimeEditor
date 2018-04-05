using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine.UI;
using UnityEngine.UI.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.UI.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentDropdownNestedDropdownEvent : PersistentSurrogate
    {
        public static implicit operator Dropdown.DropdownEvent(PersistentDropdownNestedDropdownEvent surrogate)
        {
            return (Dropdown.DropdownEvent)surrogate.WriteTo(new Dropdown.DropdownEvent());
        }
        
        public static implicit operator PersistentDropdownNestedDropdownEvent(Dropdown.DropdownEvent obj)
        {
            PersistentDropdownNestedDropdownEvent surrogate = new PersistentDropdownNestedDropdownEvent();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


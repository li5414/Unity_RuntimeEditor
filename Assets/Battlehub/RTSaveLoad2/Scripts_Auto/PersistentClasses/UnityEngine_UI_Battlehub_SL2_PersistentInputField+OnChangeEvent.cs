using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine.UI;
using UnityEngine.UI.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.UI.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentInputFieldNestedOnChangeEvent : PersistentSurrogate
    {
        public static implicit operator InputField.OnChangeEvent(PersistentInputFieldNestedOnChangeEvent surrogate)
        {
            return (InputField.OnChangeEvent)surrogate.WriteTo(new InputField.OnChangeEvent());
        }
        
        public static implicit operator PersistentInputFieldNestedOnChangeEvent(InputField.OnChangeEvent obj)
        {
            PersistentInputFieldNestedOnChangeEvent surrogate = new PersistentInputFieldNestedOnChangeEvent();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


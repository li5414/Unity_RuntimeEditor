using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine.UI;
using UnityEngine.UI.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.UI.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentInputFieldNestedSubmitEvent : PersistentSurrogate
    {
        public static implicit operator InputField.SubmitEvent(PersistentInputFieldNestedSubmitEvent surrogate)
        {
            return (InputField.SubmitEvent)surrogate.WriteTo(new InputField.SubmitEvent());
        }
        
        public static implicit operator PersistentInputFieldNestedSubmitEvent(InputField.SubmitEvent obj)
        {
            PersistentInputFieldNestedSubmitEvent surrogate = new PersistentInputFieldNestedSubmitEvent();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


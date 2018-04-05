using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine.UI;
using UnityEngine.UI.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.UI.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentMaskableGraphicNestedCullStateChangedEvent : PersistentSurrogate
    {
        public static implicit operator MaskableGraphic.CullStateChangedEvent(PersistentMaskableGraphicNestedCullStateChangedEvent surrogate)
        {
            return (MaskableGraphic.CullStateChangedEvent)surrogate.WriteTo(new MaskableGraphic.CullStateChangedEvent());
        }
        
        public static implicit operator PersistentMaskableGraphicNestedCullStateChangedEvent(MaskableGraphic.CullStateChangedEvent obj)
        {
            PersistentMaskableGraphicNestedCullStateChangedEvent surrogate = new PersistentMaskableGraphicNestedCullStateChangedEvent();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


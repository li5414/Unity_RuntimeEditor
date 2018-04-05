using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine.UI;
using UnityEngine.UI.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.UI.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentSliderNestedSliderEvent : PersistentSurrogate
    {
        public static implicit operator Slider.SliderEvent(PersistentSliderNestedSliderEvent surrogate)
        {
            return (Slider.SliderEvent)surrogate.WriteTo(new Slider.SliderEvent());
        }
        
        public static implicit operator PersistentSliderNestedSliderEvent(Slider.SliderEvent obj)
        {
            PersistentSliderNestedSliderEvent surrogate = new PersistentSliderNestedSliderEvent();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


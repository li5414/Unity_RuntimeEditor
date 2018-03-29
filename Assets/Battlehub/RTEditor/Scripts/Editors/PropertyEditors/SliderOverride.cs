using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battlehub.RTEditor
{
    public class SliderOverride : Slider
    {
        public UnityEvent onEndEdit;

        private bool m_drag;
        
        public override void OnDrag(PointerEventData eventData)
        {
            m_drag = true;
            base.OnDrag(eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            if(m_drag)
            {
                onEndEdit.Invoke();
                m_drag = false;
            }
        }
    }

}

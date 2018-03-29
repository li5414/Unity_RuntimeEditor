using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
using UnityEngine.Events;
using Battlehub.Utils;

namespace Battlehub.RTEditor
{
    public class DragField : MonoBehaviour, IDragHandler, IBeginDragHandler, IDropHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public InputField Field;
        public float IncrementFactor = 0.1f;
        public Texture2D DragCursor;
        public UnityEvent BeginDrag;
        public UnityEvent EndDrag;

        private void Start()
        {
            if(Field == null)
            {
                Debug.LogWarning("Set Field " + gameObject.name);
                return;
            }
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            BeginDrag.Invoke();  
        }

        void IDropHandler.OnDrop(PointerEventData eventData)
        {
            EndDrag.Invoke();
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            EndDrag.Invoke();
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            if (Field == null)
            {
                return;
            }

            float d;
            if (float.TryParse(Field.text, out d))
            {
                d += IncrementFactor * eventData.delta.x;
                Field.text = d.ToString();
            }
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            CursorHelper.SetCursor(this, DragCursor, new Vector2(24, 24), CursorMode.Auto);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            CursorHelper.ResetCursor(this);
        }
    }

}

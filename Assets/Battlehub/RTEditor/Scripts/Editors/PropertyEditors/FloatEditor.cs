using UnityEngine;
using UnityEngine.UI;

namespace Battlehub.RTEditor
{
    public class FloatEditor : PropertyEditor<float>
    {
        [SerializeField]
        protected InputField m_input;
        [SerializeField]
        protected DragField m_dragField;

        protected override void AwakeOverride()
        {
            base.AwakeOverride();
            m_input.onValueChanged.AddListener(OnValueChanged);
            m_input.onEndEdit.AddListener(OnEndEdit);
            m_dragField.EndDrag.AddListener(OnEndDrag); 
        }

        protected override void OnDestroyOverride()
        {
            base.OnDestroyOverride();
            if(m_input != null)
            {
                m_input.onValueChanged.RemoveListener(OnValueChanged);
                m_input.onEndEdit.RemoveListener(OnEndEdit);
            }

            if(m_dragField != null)
            {
                m_dragField.EndDrag.RemoveListener(OnEndDrag);
            }
        }

        protected override void SetInputField(float value)
        {
            m_input.text = value.ToString();
        }

        protected virtual void OnValueChanged(string value)
        {
            float val;
            if(float.TryParse(value, out val))
            {
                SetValue(val);
            }
        }

        protected virtual void OnEndEdit(string value)
        {
            m_input.text = GetValue().ToString();
            EndEdit();
        }

        protected virtual void OnEndDrag()
        {
            EndEdit();
        }
    }
}

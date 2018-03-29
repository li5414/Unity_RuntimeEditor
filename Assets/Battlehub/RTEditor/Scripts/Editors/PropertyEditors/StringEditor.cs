using UnityEngine;
using UnityEngine.UI;

namespace Battlehub.RTEditor
{
    public class StringEditor : PropertyEditor<string>
    {
        [SerializeField]
        protected InputField m_input;

        protected override void AwakeOverride()
        {
            base.AwakeOverride();
            m_input.onValueChanged.AddListener(OnValueChanged);
            m_input.onEndEdit.AddListener(OnEndEdit);
        }

        protected override void OnDestroyOverride()
        {
            base.OnDestroyOverride();
            if (m_input != null)
            {
                m_input.onValueChanged.RemoveListener(OnValueChanged);
                m_input.onEndEdit.RemoveListener(OnEndEdit);
            }
        }

        protected override void SetInputField(string value)
        {
            m_input.text = value;
        }

        protected virtual void OnValueChanged(string value)
        {
            SetValue(value);
        }

        protected virtual void OnEndEdit(string value)
        {
            m_input.text = GetValue();
            
            EndEdit();
        }

        protected virtual void OnEndDrag()
        {
            EndEdit();
        }
    }


}

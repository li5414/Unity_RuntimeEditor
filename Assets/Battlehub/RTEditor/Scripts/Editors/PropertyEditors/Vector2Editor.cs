using System;
using UnityEngine;
using UnityEngine.UI;

namespace Battlehub.RTEditor
{
    public class Vector2Editor : PropertyEditor<Vector2>
    {
        [SerializeField]
        private InputField m_xInput;
        [SerializeField]
        private InputField m_yInput;
        [SerializeField]
        protected DragField[] m_dragFields;

        protected override void AwakeOverride()
        {
            base.AwakeOverride();
            m_xInput.onValueChanged.AddListener(OnXValueChanged);
            m_yInput.onValueChanged.AddListener(OnYValueChanged);
            m_xInput.onEndEdit.AddListener(OnEndEdit);
            m_yInput.onEndEdit.AddListener(OnEndEdit);

            for (int i = 0; i < m_dragFields.Length; ++i)
            {
                if (m_dragFields[i])
                {
                    m_dragFields[i].EndDrag.AddListener(OnEndDrag);
                }
            }

        }

        protected override void OnDestroyOverride()
        {
            base.OnDestroyOverride();
            if (m_xInput != null)
            {
                m_xInput.onValueChanged.RemoveListener(OnXValueChanged);
                m_xInput.onEndEdit.RemoveListener(OnEndEdit);   
            }

            if (m_yInput != null)
            {
                m_yInput.onValueChanged.RemoveListener(OnYValueChanged);
                m_yInput.onEndEdit.RemoveListener(OnEndEdit);
            }

            for (int i = 0; i < m_dragFields.Length; ++i)
            {
                if (m_dragFields[i])
                {
                    m_dragFields[i].EndDrag.RemoveListener(OnEndDrag);
                }
            }
        }

        protected override void SetInputField(Vector2 value)
        {
            m_xInput.text = value.x.ToString();
            m_yInput.text = value.y.ToString();
        }

        private void OnXValueChanged(string value)
        {
            float val;
            if (float.TryParse(value, out val))
            {
                Vector2 vector = GetValue();
                vector.x = val;
                SetValue(vector);
            }
        }

        private void OnYValueChanged(string value)
        {
            float val;
            if (float.TryParse(value, out val))
            {
                Vector2 vector = GetValue();
                vector.y = val;
                SetValue(vector);
            }
        }

        private void OnEndEdit(string value)
        {
            Vector2 vector = GetValue();
            m_xInput.text = vector.x.ToString();
            m_yInput.text = vector.y.ToString();

            EndEdit();
        }

        protected void OnEndDrag()
        {
            EndEdit();
        }
    }
}

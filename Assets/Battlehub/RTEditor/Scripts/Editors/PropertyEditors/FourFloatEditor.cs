using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace Battlehub.RTEditor
{
    public abstract class FourFloatEditor<T> : PropertyEditor<T>
    {
        [SerializeField]
        private InputField m_xInput;
        [SerializeField]
        private InputField m_yInput;
        [SerializeField]
        private InputField m_zInput;
        [SerializeField]
        private InputField m_wInput;
        [SerializeField]
        private RectTransform m_expander;
        [SerializeField]
        private RectTransform m_xLabel;
        [SerializeField]
        private RectTransform m_yLabel;
        [SerializeField]
        private RectTransform m_zLabel;
        [SerializeField]
        private RectTransform m_wLabel;

        [SerializeField]
        protected DragField[] m_dragFields;

        protected override void AwakeOverride()
        {
            base.AwakeOverride();

            m_xInput.onValueChanged.AddListener(OnXValueChanged);
            m_yInput.onValueChanged.AddListener(OnYValueChanged);
            m_zInput.onValueChanged.AddListener(OnZValueChanged);
            m_wInput.onValueChanged.AddListener(OnWValueChanged);

            m_xLabel.offsetMin = new Vector2(Indent, m_xLabel.offsetMin.y);
            m_yLabel.offsetMin = new Vector2(Indent, m_yLabel.offsetMin.y);
            m_zLabel.offsetMin = new Vector2(Indent, m_zLabel.offsetMin.y);
            m_wLabel.offsetMin = new Vector2(Indent, m_wLabel.offsetMin.y);

            m_xInput.onEndEdit.AddListener(OnEndEdit);
            m_yInput.onEndEdit.AddListener(OnEndEdit);
            m_zInput.onEndEdit.AddListener(OnEndEdit);
            m_wInput.onEndEdit.AddListener(OnEndEdit);


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

            if (m_zInput != null)
            {
                m_zInput.onValueChanged.RemoveListener(OnZValueChanged);
                m_zInput.onEndEdit.RemoveListener(OnEndEdit);
            }

            if (m_wInput != null)
            {
                m_wInput.onValueChanged.RemoveListener(OnWValueChanged);
                m_wInput.onEndEdit.RemoveListener(OnEndEdit);
            }

            for (int i = 0; i < m_dragFields.Length; ++i)
            {
                if (m_dragFields[i])
                {
                    m_dragFields[i].EndDrag.RemoveListener(OnEndDrag);
                }
            }
        }

        protected override void SetIndent(float indent)
        {
            m_expander.offsetMin = new Vector2(indent, m_expander.offsetMin.y);
            m_xLabel.offsetMin = new Vector2(indent + Indent, m_xLabel.offsetMin.y);
            m_yLabel.offsetMin = new Vector2(indent + Indent, m_yLabel.offsetMin.y);
            m_zLabel.offsetMin = new Vector2(indent + Indent, m_zLabel.offsetMin.y);
            m_wLabel.offsetMin = new Vector2(indent + Indent, m_wLabel.offsetMin.y);
        }

        protected override void SetInputField(T v)
        {
            m_xInput.text = GetX(v).ToString();
            m_yInput.text = GetY(v).ToString();
            m_zInput.text = GetZ(v).ToString();
            m_wInput.text = GetW(v).ToString();
        }

        private void OnXValueChanged(string value)
        {
            float val;
            if (float.TryParse(value, out val))
            {
                T v = SetX(GetValue(), val);
                SetValue(v);
            }
        }

        private void OnYValueChanged(string value)
        {
            float val;
            if (float.TryParse(value, out val))
            {
                T v = SetY(GetValue(), val);
                SetValue(v);
            }
        }

        private void OnZValueChanged(string value)
        {
            float val;
            if (float.TryParse(value, out val))
            {
                T v = SetZ(GetValue(), val);
                SetValue(v);
            }
        }

        private void OnWValueChanged(string value)
        {
            float val;
            if (float.TryParse(value, out val))
            {
                T v = SetW(GetValue(), val);
                SetValue(v);
            }
        }

        protected virtual void OnEndEdit(string value)
        {
            T v = GetValue();
            m_xInput.text = GetX(v).ToString();
            m_yInput.text = GetY(v).ToString();
            m_zInput.text = GetZ(v).ToString();
            m_wInput.text = GetW(v).ToString();

            EndEdit();
        }


        protected void OnEndDrag()
        {
            EndEdit();
        }

        protected abstract T SetX(T v, float x);
        protected abstract T SetY(T v, float y);
        protected abstract T SetZ(T v, float z);
        protected abstract T SetW(T v, float w);
        protected abstract float GetX(T v);
        protected abstract float GetY(T v);
        protected abstract float GetZ(T v);
        protected abstract float GetW(T v);


    }
}


using UnityEngine;
using System;
using UnityEngine.UI;

namespace Battlehub.RTEditor
{
    public class Vector3Editor : PropertyEditor<Vector3>
    {
        [SerializeField]
        private InputField m_xInput;
        [SerializeField]
        private InputField m_yInput;
        [SerializeField]
        private InputField m_zInput;
        [SerializeField]
        protected DragField[] m_dragFields;

        public bool IsXInteractable
        {
            get { return m_xInput.interactable; }
            set
            {
                m_xInput.interactable = value;
                m_dragFields[0].enabled = value;
            }
        }
        public bool IsYInteractable
        {
            get { return m_yInput.interactable; }
            set
            {
                m_yInput.interactable = value;
                m_dragFields[1].enabled = value; ;
            }
        }
        public bool IsZInteractable
        {
            get { return m_zInput.interactable; }
            set
            {
                m_zInput.interactable = value;
                m_dragFields[2].enabled = value;
            }
        }
        protected override void AwakeOverride()
        {
            base.AwakeOverride();
            m_xInput.onValueChanged.AddListener(OnXValueChanged);
            m_yInput.onValueChanged.AddListener(OnYValueChanged);
            m_zInput.onValueChanged.AddListener(OnZValueChanged);

            m_xInput.onEndEdit.AddListener(OnEndEdit);
            m_yInput.onEndEdit.AddListener(OnEndEdit);
            m_zInput.onEndEdit.AddListener(OnEndEdit);


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

            for (int i = 0; i < m_dragFields.Length; ++i)
            {
                if (m_dragFields[i])
                {
                    m_dragFields[i].EndDrag.RemoveListener(OnEndDrag);
                }
            }
        }

        protected override void SetInputField(Vector3 value)
        {
            m_xInput.text = value.x.ToString();
            m_yInput.text = value.y.ToString();
            m_zInput.text = value.z.ToString();
        }

        private void OnXValueChanged(string value)
        {
            float val;
            if (float.TryParse(value, out val))
            {
                Vector3 vector = GetValue();
                vector.x = val;
                SetValue(vector);
            }
        }

        private void OnYValueChanged(string value)
        {
            float val;
            if (float.TryParse(value, out val))
            {
                Vector3 vector = GetValue();
                vector.y = val;
                SetValue(vector);
            }
        }

        private void OnZValueChanged(string value)
        {
            float val;
            if (float.TryParse(value, out val))
            {
                Vector3 vector = GetValue();
                vector.z = val;
                SetValue(vector);
            }
        }

        private void OnEndEdit(string value)
        {
            Vector3 vector = GetValue();
            m_xInput.text = vector.x.ToString();
            m_yInput.text = vector.y.ToString();
            m_zInput.text = vector.z.ToString();

            EndEdit();
        }

        protected void OnEndDrag()
        {
            EndEdit();
        }
    }
}


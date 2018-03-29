using System;
using UnityEngine;
using UnityEngine.UI;

namespace Battlehub.RTEditor
{
    public class BoolEditor : PropertyEditor<bool>
    {
        [SerializeField]
        private Toggle m_input;

        protected override void AwakeOverride()
        {
            base.AwakeOverride();
            m_input.onValueChanged.AddListener(OnValueChanged);
        }

        protected override void OnDestroyOverride()
        {
            base.OnDestroyOverride();
            if (m_input != null)
            {
                m_input.onValueChanged.RemoveListener(OnValueChanged);
            }
        }

        protected override void SetInputField(bool value)
        {
            m_input.isOn = value;
        }

        private void OnValueChanged(bool value)
        {
            SetValue(value);
            EndEdit();
        }
    }
}


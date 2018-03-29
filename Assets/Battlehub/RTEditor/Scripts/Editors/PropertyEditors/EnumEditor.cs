using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Battlehub.RTEditor
{
    public class EnumEditor : PropertyEditor<Enum>
    {
        [SerializeField]
        private Dropdown m_input;

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

        protected override void InitOverride(object target, MemberInfo memberInfo, string label = null)
        {
            base.InitOverride(target, memberInfo, label);
            List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
            string[] names = Enum.GetNames(MemberInfoType);
            for (int i = 0; i < names.Length; ++i)
            {
                options.Add(new Dropdown.OptionData(names[i]));
            }

            m_input.options = options;
        }

        protected override void SetInputField(Enum value)
        {
            int index = Array.IndexOf(Enum.GetValues(MemberInfoType), value);
            m_input.value = index;
        }

        private void OnValueChanged(int index)
        {
            Enum value = (Enum)Enum.GetValues(MemberInfoType).GetValue(index);
            SetValue(value);
            EndEdit();
        }
    }
}

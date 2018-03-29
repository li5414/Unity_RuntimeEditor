using System;
using Battlehub.UIControls;
using UnityEngine;
using UnityEngine.UI;

namespace Battlehub.RTEditor
{
    public class ColorEditor : PropertyEditor<Color>
    {
        [SerializeField]
        private Image MainColor;

        [SerializeField]
        private RectTransform Alpha;

        [SerializeField]
        private Button BtnSelect;

        [SerializeField]
        private SelectColorDialog ColorSelectorPrefab;

        private SelectColorDialog m_colorSelector;

        protected override void SetInputField(Color value)
        {
            Color color = value;
            color.a = 1.0f;
            MainColor.color = color;
            Alpha.transform.localScale = new Vector3(value.a, 1, 1);   
        }


        protected override void AwakeOverride()
        {
            base.AwakeOverride();
            BtnSelect.onClick.AddListener(OnSelect);
        }

        protected override void OnDestroyOverride()
        {
            base.OnDestroyOverride();
            if (BtnSelect != null)
            {
                BtnSelect.onClick.RemoveListener(OnSelect);
            }
        }

        private void OnSelect()
        {
            m_colorSelector = Instantiate(ColorSelectorPrefab);
            m_colorSelector.transform.position = Vector3.zero;
            m_colorSelector.SelectedColor = GetValue();

            PopupWindow.Show("Select " + MemberInfoType.Name, m_colorSelector.transform, "Select",
                args =>
                {
                    SetValue(m_colorSelector.SelectedColor);
                    EndEdit();
                    SetInputField(m_colorSelector.SelectedColor);
                },
                "Cancel", no => { }, 260);
        }

    }

}

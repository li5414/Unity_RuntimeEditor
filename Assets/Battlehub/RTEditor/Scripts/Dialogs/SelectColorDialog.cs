using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Battlehub.UIControls;

namespace Battlehub.RTEditor
{
    public class SelectColorDialog : MonoBehaviour
    {
        [HideInInspector]
        public Color SelectedColor;

        [SerializeField]
        private Slider RSlider;
        [SerializeField]
        private Text RValue;

        [SerializeField]
        private Slider GSlider;
        [SerializeField]
        private Text GValue;

        [SerializeField]
        private Slider BSlider;
        [SerializeField]
        private Text BValue;

        [SerializeField]
        private Slider ASlider;
        [SerializeField]
        private Text AValue;

        [SerializeField]
        private InputField InputHex;

        [SerializeField]
        private Image ColorPreview;

        [SerializeField]
        private Image AlphaPreview;

        private PopupWindow m_parentPopup;

        private void Start()
        {
            m_parentPopup = GetComponentInParent<PopupWindow>();
            m_parentPopup.OK.AddListener(OnOK);

            RSlider.onValueChanged.AddListener(OnRChanged);
            RSlider.minValue = 0;
            RSlider.maxValue = 255;
            RSlider.wholeNumbers = true;
            GSlider.onValueChanged.AddListener(OnGChanged);
            GSlider.minValue = 0;
            GSlider.maxValue = 255;
            GSlider.wholeNumbers = true;
            BSlider.onValueChanged.AddListener(OnBChanged);
            BSlider.minValue = 0;
            BSlider.maxValue = 255;
            BSlider.wholeNumbers = true;
            ASlider.onValueChanged.AddListener(OnAChanged);
            ASlider.minValue = 0;
            ASlider.maxValue = 255;
            ASlider.wholeNumbers = true;
            InputHex.onValueChanged.AddListener(OnHexValueChanged);
            InputHex.onEndEdit.AddListener(OnHexEndEdit);

            InputHex.text = ColorToHex(SelectedColor);
        }

        private void OnDestroy()
        {
            if (m_parentPopup != null) m_parentPopup.OK.RemoveListener(OnOK);

            if (RSlider != null) RSlider.onValueChanged.RemoveListener(OnRChanged);
            if (GSlider != null) GSlider.onValueChanged.RemoveListener(OnGChanged);
            if (BSlider != null) BSlider.onValueChanged.RemoveListener(OnBChanged);
            if (ASlider != null) ASlider.onValueChanged.RemoveListener(OnAChanged);
            if (InputHex != null) InputHex.onValueChanged.RemoveListener(OnHexValueChanged);
            if (InputHex != null) InputHex.onEndEdit.RemoveListener(OnHexEndEdit);
        }

        private void OnRChanged(float value)
        {
            Color32 color32 = ColorPreview.color;
            color32.a = (byte)(AlphaPreview.transform.localScale.x * 255);
            color32.r = (byte)value;

            InputHex.text = ColorToHex(color32);
        }

        private void OnGChanged(float value)
        {
            Color32 color32 = ColorPreview.color;
            color32.a = (byte)(AlphaPreview.transform.localScale.x * 255);
            color32.g = (byte)value;

            InputHex.text = ColorToHex(color32);
        }

        private void OnBChanged(float value)
        {
            Color32 color32 = ColorPreview.color;
            color32.a = (byte)(AlphaPreview.transform.localScale.x * 255);
            color32.b = (byte)value;

            InputHex.text = ColorToHex(color32);
        }

        private void OnAChanged(float value)
        {
            Color32 color32 = ColorPreview.color;
            color32.a = (byte)value;

            InputHex.text = ColorToHex(color32);
        }

        private void OnHexValueChanged(string value)
        {
            if(IsValidHex())
            {
                Color color = HexToColor(value);
                float alpha = color.a;
                color.a = 1.0f;
                ColorPreview.color = color;
                AlphaPreview.transform.localScale = new Vector3(alpha, 1, 1);

                Color32 color32 = ColorPreview.color;
                RSlider.value = color32.r;
                GSlider.value = color32.g;
                BSlider.value = color32.b;
                ASlider.value = (byte)(AlphaPreview.transform.localScale.x * 255);

                RValue.text = color32.r.ToString();
                GValue.text = color32.g.ToString();
                BValue.text = color32.b.ToString();
                AValue.text = ASlider.value.ToString();
            }
         
        }

        private void OnHexEndEdit(string value)
        {
            if(!IsValidHex())
            {
                InputHex.text = ColorToHex(ColorPreview.color);
            }
        }
        

        private bool IsValidHex()
        {
            if(string.IsNullOrEmpty(InputHex.text))
            {
                return false;
            }

            if(InputHex.text.Length < 8)
            {
                return false;
            }

            int hex;
            return int.TryParse(InputHex.text,
                System.Globalization.NumberStyles.HexNumber,
                System.Globalization.CultureInfo.InvariantCulture, out hex);
        }

        string ColorToHex(Color32 color)
        {
            string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2") + color.a.ToString("X2");
            return hex;
        }

        Color HexToColor(string hex)
        {
            byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            byte a = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);

            return new Color32(r, g, b, a);
        }

        private void OnOK(PopupWindowArgs args)
        {
            if (!IsValidHex())
            {
                args.Cancel = true;
            }
            else
            {
                SelectedColor = HexToColor(InputHex.text);
            }
        }
    }

}

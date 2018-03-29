using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

namespace Battlehub.RTEditor
{
    public class Range
    {
       
        public float Min;
        public float Max;

        public Range(float min, float max)
        {
            Min = min;
            Max = max;
        }
    }
   
    public class RangeEditor : FloatEditor
    {
        [SerializeField]
        private SliderOverride m_slider;

        public float Min = 0.0f;
        public float Max = 1.0f;

        protected override void AwakeOverride()
        {
            base.AwakeOverride();
            m_slider.onValueChanged.AddListener(OnSliderValueChanged);
            m_slider.onEndEdit.AddListener(OnSliderEndEdit);
        }

        protected override void StartOverride()
        {
            base.StartOverride();
            m_slider.minValue = Min;
            m_slider.maxValue = Max;
        }

        protected override void SetInputField(float value)
        {
            base.SetInputField(value);
            m_slider.minValue = Min;
            m_slider.maxValue = Max;
            m_slider.value = value;
        }

        protected override void OnDestroyOverride()
        {
            base.OnDestroyOverride();
            if(m_slider != null)
            {
                m_slider.onValueChanged.RemoveListener(OnSliderValueChanged);
                m_slider.onEndEdit.RemoveListener(OnSliderEndEdit);
            }
        }

        private void OnSliderValueChanged(float value)
        {
            m_input.text = value.ToString();
        }

        protected override void OnValueChanged(string value)
        {
            float val;
            if (float.TryParse(value, out val))
            {
                if(Min <= val && val <= Max)
                {
                    SetValue(val);
                    m_slider.value = val;
                }
            }
        }

        private void OnSliderEndEdit()
        {
            EndEdit();
        }


    }
}


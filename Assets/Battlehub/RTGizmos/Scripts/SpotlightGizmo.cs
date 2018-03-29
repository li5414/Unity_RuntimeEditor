using System;
using Battlehub.RTCommon;
using Battlehub.Utils;
using UnityEngine;


namespace Battlehub.RTGizmos
{
    public class SpotlightGizmo : ConeGizmo
    {
        [SerializeField]
        private Light m_light;

        protected override float Height
        {
            get { return m_light.range; }
            set
            {
                if (m_light != null)
                {
                    m_light.range = value;
                }
            }
        }

        protected override float Radius
        {
            get
            {
                if (m_light == null)
                {
                    return 0;
                }

                return m_light.range * Mathf.Tan(Mathf.Deg2Rad * m_light.spotAngle / 2);
            }
            set
            {
                if (m_light != null)
                {
                    m_light.spotAngle = Mathf.Atan2(value, m_light.range) * Mathf.Rad2Deg * 2;
                }
            }
        }

        protected override void AwakeOverride()
        {
            if (m_light == null)
            {
                m_light = GetComponent<Light>();
            }

            if (m_light == null)
            {
                Debug.LogError("Set Light");
            }

            if (m_light.type != LightType.Spot)
            {
                Debug.LogWarning("m_light.Type != LightType.Spot");
            }

            base.AwakeOverride();
        }

        protected override void RecordOverride()
        {
            base.RecordOverride();
            RuntimeUndo.RecordValue(m_light, Strong.PropertyInfo((Light x) => x.range));
            RuntimeUndo.RecordValue(m_light, Strong.PropertyInfo((Light x) => x.spotAngle));
        }

        private void Reset()
        {
            LineColor = new Color(1, 1, 0.5f, 0.5f);
            HandlesColor = new Color(1, 1, 0.35f, 0.95f);
            SelectionColor = new Color(1.0f, 1.0f, 0, 1.0f);
        }
    }

}


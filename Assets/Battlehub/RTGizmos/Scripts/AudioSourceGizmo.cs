using UnityEngine;
using Battlehub.RTCommon;
using Battlehub.Utils;
using System.Linq;

namespace Battlehub.RTGizmos
{
    public class AudioSourceGizmo : SphereGizmo
    {
        [SerializeField]
        private AudioSource m_source;

        [SerializeField]
        [HideInInspector]
        private bool m_max = true;

        protected override Vector3 Center
        {
            get { return Vector3.zero; }
            set { }
        }

        protected override float Radius
        {
            get
            {
                if (m_source == null)
                {
                    return 0;
                }

                if(m_max)
                {
                    return m_source.maxDistance;
                }
                else
                {
                    return m_source.minDistance;
                }
            }
            set
            {
                if (m_source != null)
                {
                    if (m_max)
                    {
                        m_source.maxDistance = value;
                    }
                    else
                    {
                        m_source.minDistance = value;
                    }
                }
            }
        }

        protected override void AwakeOverride()
        {
            if (m_source == null)
            {
                m_source = GetComponent<AudioSource>();
            }

            if (m_source == null)
            {
                Debug.LogError("Set AudioSource");
            }


            if(gameObject.GetComponents<AudioSourceGizmo>().Count( a => a.m_source == m_source) == 1)
            {
                AudioSourceGizmo gizmo = gameObject.AddComponent<AudioSourceGizmo>();
                gizmo.LineColor = LineColor;
                gizmo.HandlesColor = HandlesColor;
                gizmo.SelectionColor = SelectionColor;
                gizmo.SelectionMargin = SelectionMargin;
                gizmo.EnableUndo = EnableUndo;
                gizmo.m_max = !m_max;
            }

            base.AwakeOverride();
        }

        protected override void RecordOverride()
        {
            base.RecordOverride();
            RuntimeUndo.RecordValue(m_source, Strong.PropertyInfo((AudioSource x) => x.minDistance));
            RuntimeUndo.RecordValue(m_source, Strong.PropertyInfo((AudioSource x) => x.maxDistance));
        }

        private void Reset()
        {
            LineColor = new Color(0.375f, 0.75f, 1, 0.5f);
            HandlesColor = new Color(0.375f, 0.75f, 1, 0.5f);
            SelectionColor = new Color(1.0f, 1.0f, 0, 1.0f);
        }
    }
}


using UnityEngine;
using System.Collections;
using Battlehub.RTCommon;
using Battlehub.Utils;

namespace Battlehub.RTGizmos
{
    public class SphereColliderGizmo : SphereGizmo
    {
        [SerializeField]
        private SphereCollider m_collider;

        protected override Vector3 Center
        {
            get
            {
                if (m_collider == null)
                {
                    return Vector3.zero;
                }
                return m_collider.center;
            }
            set
            {
                if (m_collider != null)
                {
                    m_collider.center = value;
                }
            }
        }

        protected override float Radius
        {
            get
            {
                if (m_collider == null)
                {
                    return 0;
                }

                return m_collider.radius;
            }
            set
            {
                if (m_collider != null)
                {
                    m_collider.radius = value;
                }
            }
        }

     
        protected override void AwakeOverride()
        {
            if (m_collider == null)
            {
                m_collider = GetComponent<SphereCollider>();
            }

            if (m_collider == null)
            {
                Debug.LogError("Set Collider");
            }

            base.AwakeOverride();
        }

        protected override void RecordOverride()
        {
            base.RecordOverride();
            RuntimeUndo.RecordValue(m_collider, Strong.PropertyInfo((SphereCollider x) => x.center));
            RuntimeUndo.RecordValue(m_collider, Strong.PropertyInfo((SphereCollider x) => x.radius));
        }
    }
}


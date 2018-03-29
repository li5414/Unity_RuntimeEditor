using Battlehub.RTCommon;
using Battlehub.Utils;
using UnityEngine;

namespace Battlehub.RTGizmos
{
    public class BoxColliderGizmo : BoxGizmo
    {
        [SerializeField]
        private BoxCollider m_collider;
        private static readonly Bounds m_zeroBounds = new Bounds();
        protected override Bounds Bounds
        {
            get
            {
                if(m_collider == null)
                {
                    return m_zeroBounds;
                }
                return new Bounds(m_collider.center, m_collider.size);
            }
            set
            {
                if(m_collider != null)
                {
                    m_collider.center = value.center;
                    m_collider.size = value.extents * 2;
                }
            }
        }


        protected override void AwakeOverride()
        {
            if(m_collider == null)
            {
                m_collider = GetComponent<BoxCollider>();
            }

            if(m_collider == null)
            {
                Debug.LogError("Set Collider");
            }
            
            base.AwakeOverride();
            
        }

        protected override void RecordOverride()
        {
            base.RecordOverride();
            RuntimeUndo.RecordValue(m_collider, Strong.PropertyInfo((BoxCollider x) => x.center));
            RuntimeUndo.RecordValue(m_collider, Strong.PropertyInfo((BoxCollider x) => x.size));
        }
    }

}

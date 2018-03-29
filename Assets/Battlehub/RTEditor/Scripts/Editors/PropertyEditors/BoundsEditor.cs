using Battlehub.Utils;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace Battlehub.RTEditor
{
    public class BoundsAccessor
    {
        private PropertyEditor<Bounds> m_editor;

        public Vector3 Center
        {
            get { return GetBounds().center; }
            set
            {
                Bounds bounds = GetBounds();
                bounds.center = value;
                m_editor.SetValue(bounds);
            }
        }

        public Vector3 Extents
        {
            get { return GetBounds().extents; }
            set
            {
                Bounds bounds = GetBounds();
                bounds.extents = value;
                m_editor.SetValue(bounds);
            }
        }

        private Bounds GetBounds()
        {
            return m_editor.GetValue();
        }

        public BoundsAccessor(PropertyEditor<Bounds> editor)
        {
            m_editor = editor;
        }
    }

    public class BoundsEditor : PropertyEditor<Bounds>
    {
        [SerializeField]
        private Vector3Editor m_center;
        [SerializeField]
        private Vector3Editor m_extents;
        
        protected override void AwakeOverride()
        {
            base.AwakeOverride();
        }

        protected override void OnDestroyOverride()
        {
            base.OnDestroyOverride();
        }

        protected override void InitOverride(object target, MemberInfo memberInfo, string label = null)
        {
            base.InitOverride(target, memberInfo, label);

            BoundsAccessor accessor = new BoundsAccessor(this);
            m_center.Init(accessor, Strong.MemberInfo((BoundsAccessor x) => x.Center), "Center", OnValueChanging, null, OnEndEdit, false);
            m_extents.Init(accessor, Strong.MemberInfo((BoundsAccessor x) => x.Extents), "Extents", OnValueChanging, null, OnEndEdit, false);
        }

        protected override void ReloadOverride()
        {
            base.ReloadOverride();
            m_center.Reload();
            m_extents.Reload();
        }

        private void OnValueChanging()
        {
            BeginEdit();
        }

        private void OnEndEdit()
        {
            EndEdit();
        }
    }
}


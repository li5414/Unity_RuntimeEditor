using Battlehub.RTCommon;
using UnityEngine;
using UnityEngine.UI;

namespace Battlehub.RTEditor
{
    public class ColliderEditor : ComponentEditor
    {
        [SerializeField]
        private GameObject ToggleButton;

        private Toggle m_editColliderButton;

        private bool m_isEditing;

        private RuntimeTool m_lastTool;

        protected override void AwakeOverride()
        {
            base.AwakeOverride();
            m_lastTool = RuntimeTools.Current;
            RuntimeTools.ToolChanged += OnToolChanged;
        }

        protected override void OnDestroyOverride()
        {
            RuntimeTools.ToolChanged -= OnToolChanged;
            base.OnDestroyOverride();
            if (m_editColliderButton != null)
            {
                m_editColliderButton.onValueChanged.RemoveListener(OnEditCollider);
            }
            RuntimeTools.Current = m_lastTool;
        }

        private void OnToolChanged()
        {
            if(RuntimeTools.Current != RuntimeTool.None)
            {
                m_lastTool = RuntimeTools.Current;
                m_isEditing = false;
                if (m_editColliderButton != null)
                {
                    m_editColliderButton.isOn = false;
                }
                
            }
        }

        protected override void BuildEditor(IComponentDescriptor componentDescriptor, PropertyDescriptor[] descriptors)
        {
            base.BuildEditor(componentDescriptor, descriptors);
            m_editColliderButton = Instantiate(ToggleButton).GetComponent<Toggle>();
            m_editColliderButton.transform.SetParent(EditorsPanel, false);
            m_editColliderButton.onValueChanged.RemoveListener(OnEditCollider);
            m_editColliderButton.isOn = m_isEditing;
            m_editColliderButton.onValueChanged.AddListener(OnEditCollider);
        }

        protected override void DestroyEditor()
        {
            base.DestroyEditor();
            if(m_editColliderButton != null)
            {
                m_editColliderButton.onValueChanged.RemoveListener(OnEditCollider);
                Destroy(m_editColliderButton.gameObject);
            }
        }

      
        private void OnEditCollider(bool edit)
        {
            m_isEditing = edit;
            if(m_isEditing)
            {
                m_lastTool = RuntimeTools.Current;
                RuntimeTools.Current = RuntimeTool.None;
                TryCreateGizmo(GetComponentDescriptor());
            }
            else
            {
                RuntimeTools.Current = m_lastTool;
                DestroyGizmo();
            }
        }

        protected override void TryCreateGizmo(IComponentDescriptor componentDescriptor)
        {
            if(m_isEditing)
            {
                base.TryCreateGizmo(componentDescriptor);
            }
            
        }

        protected override void DestroyGizmo()
        {
            base.DestroyGizmo();
        }
    }
}


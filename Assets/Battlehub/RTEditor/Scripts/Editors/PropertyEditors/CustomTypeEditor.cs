using Battlehub.RTCommon;
using System;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battlehub.RTEditor
{
    public class CustomTypeEditor : PropertyEditor<object>, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private Transform Panel;
        [SerializeField]
        private Toggle Expander;

        public bool StartExpanded;

        protected override void AwakeOverride()
        {
            base.AwakeOverride();
            Expander.onValueChanged.AddListener(OnExpanded);
        }

        protected override void StartOverride()
        {
            base.StartOverride();
        }

        protected override void OnDestroyOverride()
        {
            base.OnDestroyOverride();
            if (Expander != null)
            {
                Expander.onValueChanged.RemoveListener(OnExpanded);
            }

            if (m_coExpand != null)
            {
                StopCoroutine(m_coExpand);
                m_coExpand = null;
            }
        }

        protected override void SetIndent(float indent)
        {
            RectTransform rt = Expander.GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.offsetMin = new Vector2(indent, rt.offsetMin.y);
            }
        }

        protected override void InitOverride(object target, MemberInfo memberInfo, string label = null)
        {
            base.InitOverride(target, memberInfo, label);
           
            FieldInfo[] serializableFields = Reflection.GetSerializableFields(memberInfo.GetType());

            if (StartExpanded)
            {
                Expander.isOn = serializableFields.Length < 8;
            }
        }

        private void OnExpanded(bool isExpanded)
        {
            Panel.gameObject.SetActive(isExpanded);

            m_currentValue = GetValue();
            if (isExpanded)
            {
                CreateElementEditors(m_currentValue);
            }
            else
            {
                foreach (Transform c in Panel)
                {
                    Destroy(c.gameObject);
                }
            }
        }


        private void BuildEditor()
        {
            foreach (Transform c in Panel)
            {
                Destroy(c.gameObject);
            }

            CreateElementEditors(m_currentValue);
        }

        private void CreateElementEditors(object value)
        {
            FieldInfo[] fields = Reflection.GetSerializableFields(MemberInfoType);
            for (int i = 0; i < fields.Length; ++i)
            {
                MemberInfo memberInfo = fields[i];
                Type type = fields[i].FieldType;
                CreateElementEditor(memberInfo, type);
            }

            PropertyInfo[] properties = Reflection.GetSerializableProperties(MemberInfoType);
            for(int i = 0; i < properties.Length; ++i)
            {
                PropertyInfo propertyInfo = properties[i];
                Type type = properties[i].PropertyType;
                CreateElementEditor(propertyInfo, type);
            }
        }

        private void CreateElementEditor(MemberInfo memberInfo, Type type)
        {
            if (!EditorsMap.IsPropertyEditorEnabled(type))
            {
                return;
            }
            GameObject editorPrefab = EditorsMap.GetPropertyEditor(type);
            if (editorPrefab == null)
            {
                return;
            }
            PropertyEditor editor = Instantiate(editorPrefab).GetComponent<PropertyEditor>();
            if (editor == null)
            {
                return;
            }

            CustomTypeFieldAccessor accessor = null;
            if(ChildDescriptors == null)
            {
                accessor = new CustomTypeFieldAccessor(this, memberInfo, memberInfo.Name);
            }
            else
            {
                PropertyDescriptor childPropertyDescriptor;
                if(ChildDescriptors.TryGetValue(memberInfo, out childPropertyDescriptor))
                {
                    accessor = new CustomTypeFieldAccessor(
                        this, 
                        childPropertyDescriptor.MemberInfo, childPropertyDescriptor.Label);
                }
            }
            if(accessor != null)
            {
                editor.transform.SetParent(Panel, false);
                editor.Init(accessor, accessor.GetType().GetProperty("Value"), accessor.Name, OnValueChanging, OnValueChanged, null, false);
            }
        }

        private void OnValueChanging()
        {
            BeginEdit();
        }

        private void OnValueChanged()
        {
            EndEdit();
        }

        protected override void SetInputField(object value)
        {
            if (value == null)
            {
                if(MemberInfoType.IsArray)
                {
                    Array newArray = (Array)Activator.CreateInstance(MemberInfoType, 0);
                    SetValue(newArray);
                    return;
                }
            }
        }

        //protected override void ReloadOverride()
        //{
        //    if (!Expander.isOn)
        //    {
        //        return;
        //    }

        //    object value = GetValue();
        //    if (m_currentValue == null && value == null)
        //    {
        //        return;
        //    }

        //    if (m_currentValue == null || value == null)
        //    {
        //        m_currentValue = value;
        //        SetInputField(value);
        //        BuildEditor();
        //    }
        //    else
        //    {
        //        FieldInfo[] fields = Reflection.GetSerializableFields(value.GetType());
        //        for (int i = 0; i < fields.Length; ++i)
        //        {
        //            FieldInfo fieldInfo = fields[i];
        //            if(!EditorsMap.IsPropertyEditorEnabled(fieldInfo.FieldType))
        //            {
        //                continue;
        //            }
        //            object c = fieldInfo.GetValue(m_currentValue);
        //            object v = fieldInfo.GetValue(value);
        //            if (c == null && v == null)
        //            {
        //                continue;
        //            }
        //            if (c == null || v == null || !c.Equals(v))
        //            {
        //                m_currentValue = value;
        //                BuildEditor();
        //            }
        //        }
             
        //    }
        //}

        private IEnumerator m_coExpand;
        private IEnumerator CoExpand()
        {
            yield return new WaitForSeconds(0.5f);
            Expander.isOn = true;
            m_coExpand = null;
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (DragDrop.DragItem != null)
            {
                if (Expander != null)
                {
                    m_coExpand = CoExpand();
                    StartCoroutine(m_coExpand);
                }
            }
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            if (m_coExpand != null)
            {
                StopCoroutine(m_coExpand);
                m_coExpand = null;
            }
        }
    }
}


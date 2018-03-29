
using System;
using System.Collections;

using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using Battlehub.RTCommon;

namespace Battlehub.RTEditor
{
    public abstract class IListEditor : PropertyEditor<IList>, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private GameObject SizeEditor;
        [SerializeField]
        private InputField SizeInput;
        [SerializeField]
        private Text SizeLabel;

        [SerializeField]
        private Transform Panel;
        [SerializeField]
        private Toggle Expander;

        private PropertyEditor m_editorPrefab;

        public bool StartExpanded;

        protected override void AwakeOverride()
        {
            base.AwakeOverride();
            Expander.onValueChanged.AddListener(OnExpanded);
            SizeInput.onValueChanged.AddListener(OnSizeValueChanged);
            SizeInput.onEndEdit.AddListener(OnSizeEndEdit);

            RectTransform rt = SizeLabel.GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.offsetMin = new Vector2(Indent, rt.offsetMin.y);
            }
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

            if (SizeInput != null)
            {
                SizeInput.onValueChanged.RemoveListener(OnSizeValueChanged);
                SizeInput.onEndEdit.RemoveListener(OnSizeEndEdit);
            }

            if (m_coExpand != null)
            {
                StopCoroutine(m_coExpand);
                m_coExpand = null;
            }
        }

        protected override void SetIndent(float indent)
        {
            RectTransform rt = SizeLabel.GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.offsetMin = new Vector2(indent + Indent, rt.offsetMin.y);
            }

            rt = Expander.GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.offsetMin = new Vector2(indent, rt.offsetMin.y);
            }
        }

        protected override void InitOverride(object target, MemberInfo memberInfo, string label = null)
        {
            Type elementType = null;
            if (memberInfo is PropertyInfo)
            {
                elementType = ((PropertyInfo)memberInfo).PropertyType.GetElementType();
                if (elementType == null)
                {
                    if (((PropertyInfo)memberInfo).PropertyType.IsGenericType)
                    {
                        elementType = ((PropertyInfo)memberInfo).PropertyType.GetGenericArguments()[0];
                    }
                }
            }
            else if (memberInfo is FieldInfo)
            {
                elementType = ((FieldInfo)memberInfo).FieldType.GetElementType();
                if (elementType == null)
                {
                    if (((FieldInfo)memberInfo).FieldType.IsGenericType)
                    {
                        elementType = ((FieldInfo)memberInfo).FieldType.GetGenericArguments()[0];
                    }
                }
            }
           
           

            if (elementType != null)
            {
                GameObject editor = EditorsMap.GetPropertyEditor(elementType);
                if (EditorsMap.IsPropertyEditorEnabled(elementType))
                {
                    m_editorPrefab = editor.GetComponent<PropertyEditor>();
                }

                if (m_editorPrefab == null)
                {
                    Debug.LogWarning("Editor for " + memberInfo.Name + " not found");
                    Destroy(gameObject);
                }

                base.InitOverride(target, memberInfo, label);
            }
            else
            {
                if (elementType == null)
                {
                    Debug.LogWarning("Editor for " + memberInfo.Name + " not found");
                    Destroy(gameObject);
                }
            }

            if (StartExpanded)
            {
                Expander.isOn = GetValue().Count < 8;
            }
        }

        private void OnExpanded(bool isExpanded)
        {
            SizeEditor.SetActive(isExpanded);
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

        private void CreateElementEditors(IList value)
        {
            for (int i = 0; i < value.Count; ++i)
            {
                PropertyEditor editor = Instantiate(m_editorPrefab);
                editor.transform.SetParent(Panel, false);
                IListElementAccessor accessor = new IListElementAccessor(this, i, "Element " + i);
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

        private void OnSizeValueChanged(string value)
        {
            BeginEdit();
        }

        protected abstract IList Resize(IList list, int size);
        
        private void OnSizeEndEdit(string value)
        {
            IList list = GetValue();

            int size;
            if (int.TryParse(value, out size) && size >= 0)
            {
                foreach (Transform c in Panel)
                {
                    Destroy(c.gameObject);
                }

                IList newList = Resize(list, size);
                SetValue(newList);
                EndEdit();
                if (Expander.isOn)
                {
                    CreateElementEditors(newList);
                }
            }
            else
            {
                SizeInput.text = list.Count.ToString();
            }
        }

        protected override void SetInputField(IList value)
        {
            if (value == null)
            {
                Array newArray = (Array)Activator.CreateInstance(MemberInfoType, 0);
                SetValue(newArray);
                return;
            }

            SizeInput.text = value.Count.ToString();
        }

        protected override void ReloadOverride()
        {
            if (!Expander.isOn)
            {
                return;
            }

            IList value = GetValue();
            if (m_currentValue == null && value == null)
            {
                return;
            }

            if (m_currentValue == null || value == null || m_currentValue.Count != value.Count)
            {
                m_currentValue = value;
                SetInputField(value);
                BuildEditor();
            }
            else
            {
                for (int i = 0; i < m_currentValue.Count; ++i)
                {
                    object c = m_currentValue[i];
                    object v = value[i];
                    if (c == null && v == null)
                    {
                        continue;
                    }
                    if (c == null || v == null || !c.Equals(v))
                    {
                        m_currentValue = value;
                        BuildEditor();
                    }
                }
            }
        }

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

using UnityEngine;
using System.Collections;

using System.Reflection;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Battlehub.Utils;
using Battlehub.RTSaveLoad;
using System;
using System.Collections.Generic;
using System.Linq;

using Battlehub.RTCommon;
namespace Battlehub.RTEditor
{
 

    public class MaterialPropertyDescriptor
    {
        public object Target;
        public string Label;
        public RTShaderPropertyType Type;
#if !UNITY_WEBGL
        public ProceduralPropertyDescription ProceduralDescription;
#endif
        public PropertyInfo PropertyInfo;
        public RuntimeShaderInfo.RangeLimits Limits;
        public TextureDimension TexDims;
        
        public PropertyEditorCallback ValueChangedCallback;

        public MaterialPropertyDescriptor(object target, string label, RTShaderPropertyType type, PropertyInfo propertyInfo, RuntimeShaderInfo.RangeLimits limits, TextureDimension dims, PropertyEditorCallback callback)
        {
            Target = target;
            Label = label;
            Type = type;
            PropertyInfo = propertyInfo;
            Limits = limits;
            TexDims = dims;
            ValueChangedCallback = callback;
        }
#if !UNITY_WEBGL
        public MaterialPropertyDescriptor(object target, string label, ProceduralPropertyDescription procDescription, PropertyInfo propertyInfo, PropertyEditorCallback callback)
        {
            Target = target;
            Label = label;
            Type = RTShaderPropertyType.Procedural;
            ProceduralDescription = procDescription;
            PropertyInfo = propertyInfo;
            ValueChangedCallback = callback;
        }
#endif
    }


    public interface IMaterialDescriptor
    {
        string ShaderName
        {
            get;
        }

        object CreateConverter(MaterialEditor editor);

        MaterialPropertyDescriptor[] GetProperties(MaterialEditor editor, object converter);
    }

    public class MaterialEditor : MonoBehaviour
    {
        private static Dictionary<string, IMaterialDescriptor> m_propertySelectors;
        static MaterialEditor()
        {
            var type = typeof(IMaterialDescriptor);

            var types = Reflection.GetAssignableFromTypes(type);

            m_propertySelectors = new Dictionary<string, IMaterialDescriptor>();
            foreach (Type t in types)
            {
                IMaterialDescriptor selector = (IMaterialDescriptor)Activator.CreateInstance(t);
                if (selector == null)
                {
                    Debug.LogWarningFormat("Unable to instantiate selector of type " + t.FullName);
                    continue;
                }
                if (selector.ShaderName == null)
                {
                    Debug.LogWarningFormat("ComponentType is null. Selector ShaderName is null {0}", t.FullName);
                    continue;
                }
                if (m_propertySelectors.ContainsKey(selector.ShaderName))
                {
                    Debug.LogWarningFormat("Duplicate component selector for {0} found. Type name {1}. Using {2} instead", selector.ShaderName, selector.GetType().FullName, m_propertySelectors[selector.ShaderName].GetType().FullName);
                }
                else
                {
                    m_propertySelectors.Add(selector.ShaderName, selector);
                }
            }
        }

        // [SerializeField]
        // private ShaderVariantCollection ShaderVariants;

        [SerializeField]
        private RangeEditor RangeEditor;
        [SerializeField]
        private TakeSnapshot Preview;
        private Image m_image;
        [SerializeField]
        private Text TxtMaterialName;
        [SerializeField]
        private Text TxtShaderName;

        [SerializeField]
        private Transform EditorsPanel;

        [HideInInspector]
        public Material Material;

        private void Start()
        {
            //if(!ShaderVariants.isWarmedUp)
            //{
            //    ShaderVariants.WarmUp();
            //}
            if (Material == null)
            {
                Material = RuntimeSelection.activeObject as Material;
            }

            if (Material == null)
            {
                Debug.LogError("Select material");
                return;
            }

            TxtMaterialName.text = Material.name;
            if (Material.shader != null)
            {
                TxtShaderName.text = Material.shader.name;
            }
            else
            {
                TxtShaderName.text = "Shader missing";
            }

            if (Preview != null)
            {
                m_image = Preview.GetComponent<Image>();
            }

            UpdatePreview(Material);

            BuildEditor();
        }

        public void BuildEditor()
        {
          
            foreach(Transform t in EditorsPanel)
            {
                Destroy(t.gameObject);
            }

            IMaterialDescriptor selector;
            if(!m_propertySelectors.TryGetValue(Material.shader.name, out selector))
            {
                selector = new MaterialDescriptor();
            }


            object converter = selector.CreateConverter(this);
            MaterialPropertyDescriptor[] descriptors = selector.GetProperties(this, converter);
            if(descriptors == null)
            {
                Destroy(gameObject);
                return;
            }

            for(int i = 0; i < descriptors.Length; ++i)
            {
                MaterialPropertyDescriptor descriptor = descriptors[i];
                PropertyEditor editor = null;
                object target = descriptor.Target;
                PropertyInfo propertyInfo = descriptor.PropertyInfo;
#if !UNITY_WEBGL
                if(descriptor.ProceduralDescription == null)
#endif
                {
                    RTShaderPropertyType propertyType = descriptor.Type;

                    switch (propertyType)
                    {
                        case RTShaderPropertyType.Range:
                            if(RangeEditor != null)
                            {
                                RangeEditor range = Instantiate(RangeEditor);
                                range.transform.SetParent(EditorsPanel, false);

                                var rangeLimits = descriptor.Limits;
                                range.Min = rangeLimits.Min;
                                range.Max = rangeLimits.Max;
                                editor = range;
                            }
                            break;
                        default:
                            if (EditorsMap.IsPropertyEditorEnabled(propertyInfo.PropertyType))
                            {
                                GameObject editorPrefab = EditorsMap.GetPropertyEditor(propertyInfo.PropertyType);
                                GameObject instance = Instantiate(editorPrefab);
                                instance.transform.SetParent(EditorsPanel, false);

                                if (instance != null)
                                {
                                    editor = instance.GetComponent<PropertyEditor>();
                                }
                            }
                            break;
                    }
                }
#if !UNITY_WEBGL
                else
                {
                    ProceduralPropertyDescription input = descriptor.ProceduralDescription;
                    if(input.hasRange)
                    {
                        if(input.type == ProceduralPropertyType.Float)
                        {
                            if(RangeEditor != null)
                            {
                                RangeEditor range = Instantiate(RangeEditor);
                                range.transform.SetParent(EditorsPanel, false);
                                range.Min = input.minimum;
                                range.Max = input.maximum;
                                //TODO implement step on range editor // = input.step
                                editor = range;
                            }
                        }
                        else
                        {
                            //TODO: Implement range on vector editors

                            if (EditorsMap.IsPropertyEditorEnabled(propertyInfo.PropertyType))
                            {
                                GameObject editorPrefab = EditorsMap.GetPropertyEditor(propertyInfo.PropertyType);
                                GameObject instance = Instantiate(editorPrefab);
                                instance.transform.SetParent(EditorsPanel, false);

                                if (instance != null)
                                {
                                    editor = instance.GetComponent<PropertyEditor>();
                                }
                            }
                        }
                    }
                    else
                    {
                        //if(input.type == ProceduralPropertyType.Enum)
                        //TODO: Implement enum from string array editor. //input.enumOptions

                        if (EditorsMap.IsPropertyEditorEnabled(propertyInfo.PropertyType))
                        {
                            GameObject editorPrefab = EditorsMap.GetPropertyEditor(propertyInfo.PropertyType);
                            GameObject instance = Instantiate(editorPrefab);
                            instance.transform.SetParent(EditorsPanel, false);

                            if (instance != null)
                            {
                                editor = instance.GetComponent<PropertyEditor>();
                            }
                        }
                    }
                }
#endif

                if (editor == null)
                {
                    continue;
                }

                editor.Init(target, propertyInfo, descriptor.Label, null, descriptor.ValueChangedCallback, () => 
                {
                    RuntimeEditorApplication.SaveSelectedObjects();
                });
            }
        }



        private PropertyEditor InstantiateEditor( PropertyInfo propertyInfo)
        {
            PropertyEditor editor = null;
            if (EditorsMap.IsPropertyEditorEnabled(propertyInfo.PropertyType))
            {
                GameObject prefab = EditorsMap.GetPropertyEditor(propertyInfo.PropertyType);
                if (prefab != null)
                {
                    editor = Instantiate(prefab).GetComponent<PropertyEditor>();
                    editor.transform.SetParent(EditorsPanel, false);
                }
            }

            return editor;
        }

        private void UpdatePreview(Material material)
        {
            if (Preview && m_image != null)
            {
                m_image.sprite = ResourcePreview.CreatePreview(material, Preview);
            }
        }

        private int m_updateCounter = 0;
        private void Update()
        {
            m_updateCounter++;
            m_updateCounter %= ResourcePreview.UpdatePreviewInterval;
            if (m_updateCounter == 0)
            {
                Material material = RuntimeSelection.activeObject as Material;
                UpdatePreview(material);
            }
           
        }
    }
}


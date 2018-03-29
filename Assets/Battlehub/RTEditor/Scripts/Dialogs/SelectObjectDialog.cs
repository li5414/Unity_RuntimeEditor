using System;
using System.Linq;

using UnityEngine;

using Battlehub.UIControls;
using Battlehub.RTSaveLoad;
#if UNITY_WINRT
using Battlehub.RTCommon;
#endif

using UnityObject = UnityEngine.Object;

namespace Battlehub.RTEditor
{
    public class SelectObjectDialog : MonoBehaviour
    {
        [HideInInspector]
        public UnityObject SelectedObject;
        [HideInInspector]
        public Type ObjectType;
        [SerializeField]
        private Transform ObjectsPanel;

        public bool IsNoneSelected
        {
            get;
            private set;
        }

        private PopupWindow m_parentPopup;
        private ProjectResourcesWindow m_resources;
        private void Start()
        {
            m_parentPopup = GetComponentInParent<PopupWindow>();
            m_parentPopup.OK.AddListener(OnOK);

            RuntimeEditor runtimeEditor = FindObjectOfType<RuntimeEditor>();
            ProjectResourcesWindow resources = runtimeEditor.GetComponentInChildren<ProjectResourcesWindow>(true);
            IProjectManager manager = Dependencies.ProjectManager;
            if(manager != null)
            {
                ProjectItem[] itemsOfType = manager.Project.FlattenHierarchy();
                itemsOfType = itemsOfType.Where(item => item.TypeName != null && Type.GetType(item.TypeName) != null && ObjectType.IsAssignableFrom(Type.GetType(item.TypeName))).ToArray();

                m_resources = Instantiate(resources);
                m_resources.TypeFilter = ObjectType;
                m_resources.transform.position = Vector3.zero;
                m_resources.transform.SetParent(ObjectsPanel, false);

                manager.GetOrCreateObjects(itemsOfType, objects =>
                {
                    m_resources.SetObjects(objects, true);
                });


                m_resources.DoubleClick += OnResourcesDoubleClick;
                m_resources.SelectionChanged += OnResourcesSelectionChanged;
                m_resources.TypeFilter = ObjectType;
            }
        }

        private void OnDestroy()
        {
            if (m_parentPopup != null)
            {
                m_parentPopup.OK.RemoveListener(OnOK);
            }

            if (m_resources != null)
            {
                m_resources.DoubleClick -= OnResourcesDoubleClick;
                m_resources.SelectionChanged -= OnResourcesSelectionChanged;
            }
        }

        private void OnResourcesSelectionChanged(object sender, SelectionChangedArgs<ProjectItemObjectPair> e)
        {
            if (e.NewItem != null && e.NewItem.IsNone)
            {
                IsNoneSelected = true;
            }
            else
            {
                IsNoneSelected = false;
                if(e.NewItem != null)
                {
                    SelectedObject = e.NewItem.Object;
                }
                else
                {
                    SelectedObject = null;
                }
            }
        }

        private void OnResourcesDoubleClick(object sender, ProjectResourcesEventArgs e)
        {
            if (e.ItemObjectPair != null && e.ItemObjectPair.IsNone)
            {
                IsNoneSelected = true;
            }
            else
            {
                IsNoneSelected = false;
                if (e.ItemObjectPair != null)
                {
                    SelectedObject = e.ItemObjectPair.Object;
                }
                else
                {
                    SelectedObject = null;
                }
            }

            m_parentPopup.Close(true);
        }

        private void OnOK(PopupWindowArgs args)
        {
            if (SelectedObject == null && !IsNoneSelected)
            {
                args.Cancel = true;
            }
        }
    }
}


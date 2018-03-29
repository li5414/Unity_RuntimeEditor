using UnityEngine;
using UnityEngine.UI;
using System.Linq;

using Battlehub.UIControls;
using Battlehub.RTCommon;
using Battlehub.RTSaveLoad;

namespace Battlehub.RTEditor
{
    public class SaveSceneDialog : MonoBehaviour
    {
        [SerializeField]
        private TreeView TreeViewPrefab;
        [SerializeField]
        private InputField Input;
        [SerializeField]
        private Sprite FolderIcon;
        [SerializeField]
        private Sprite SceneIcon;

        private PopupWindow m_parentPopup;
        private TreeView m_treeView;

        [HideInInspector]
        public bool ShowRoot = true;

        private bool ShowProgress
        {
            get;
            set; //show progress if required;
        }

        private IProjectManager m_projectManager;

        private void Start()
        {
            m_parentPopup = GetComponentInParent<PopupWindow>();
            if (m_parentPopup != null)
            {
                m_parentPopup.OK.AddListener(OnOK);
            }

            m_treeView = GetComponentInChildren<TreeView>();
            if (m_treeView == null)
            {
                m_treeView = Instantiate(TreeViewPrefab);
                m_treeView.transform.SetParent(transform, false);
            }

            m_treeView.ItemDataBinding += OnItemDataBinding;
            m_treeView.ItemExpanding += OnItemExpanding;
            m_treeView.SelectionChanged += OnSelectionChanged;
            m_treeView.ItemDoubleClick += OnItemDoubleClick;
            m_treeView.CanDrag = false;
            m_treeView.CanEdit = false;
            m_treeView.CanUnselectAll = false;
            m_treeView.RemoveKey = KeyCode.None;
            m_treeView.SelectAllKey = KeyCode.None;
            m_treeView.RangeselectKey = KeyCode.None;
            m_treeView.MultiselectKey = KeyCode.None;

            m_projectManager = Dependencies.ProjectManager;

            if (m_projectManager == null)
            {
                Debug.LogError("ProjectManager.Instance is null");
                return;
            }

            if (ShowRoot)
            {
                m_treeView.Items = new[] { m_projectManager.Project };
                m_treeView.SelectedItem = m_projectManager.Project;
            }
            else
            {
                if (m_projectManager.Project.Children != null)
                {
                    m_treeView.Items = m_projectManager.Project.Children.Where(c => c.IsFolder);
                    m_treeView.SelectedItem = m_projectManager.Project.Children.Where(c => c.IsFolder).FirstOrDefault();
                }

            }

            TreeViewItem root = m_treeView.GetTreeViewItem(0);
            root.IsExpanded = true;

            Input.ActivateInputField();
        }


        private void OnEnable()
        {
            StartCoroutine(CoActivateInputField());
        }

        private System.Collections.IEnumerator CoActivateInputField()
        {
            yield return new WaitForEndOfFrame();
            if (Input != null)
            {
                Input.ActivateInputField();
            }
        }

        private void OnDestroy()
        {
            if (m_parentPopup != null)
            {
                m_parentPopup.OK.RemoveListener(OnOK);
            }

            if (m_treeView != null)
            {
                m_treeView.ItemDataBinding -= OnItemDataBinding;
                m_treeView.ItemExpanding -= OnItemExpanding;
                m_treeView.SelectionChanged -= OnSelectionChanged;
                m_treeView.ItemDoubleClick -= OnItemDoubleClick;
            }
        }

        private void OnItemDataBinding(object sender, TreeViewItemDataBindingArgs e)
        {
            ProjectItem item = e.Item as ProjectItem;
            if (item != null)
            {
                Text text = e.ItemPresenter.GetComponentInChildren<Text>(true);
                text.text = item.Name;

                Image image = e.ItemPresenter.GetComponentInChildren<Image>(true);
                if (item.IsScene)
                {
                    image.sprite = SceneIcon;
                }
                else
                {
                    image.sprite = FolderIcon;
                }
                image.gameObject.SetActive(true);

                e.HasChildren = item.Children != null && item.Children.Count(projectItem => projectItem.IsFolder || projectItem.IsScene) > 0;
            }
        }

        private void OnItemExpanding(object sender, ItemExpandingArgs e)
        {
            ProjectItem item = e.Item as ProjectItem;
            if (item != null)
            {
                e.Children = item.Children.Where(projectItem => projectItem.IsFolder).OrderBy(projectItem => projectItem.Name)
                    .Union(item.Children.Where(projectItem => projectItem.IsScene).OrderBy(projectItem => projectItem.Name));
            }
        }

        private void OnSelectionChanged(object sender, SelectionChangedArgs e)
        {
            ProjectItem selectedItem = (ProjectItem)e.NewItem;
            if (selectedItem == null)
            {
                return;
            }
            if (selectedItem.IsScene)
            {
                Input.text = selectedItem.Name;
            }

            Input.ActivateInputField();
        }

        private void OnItemDoubleClick(object sender, ItemArgs e)
        {
            TreeViewItem treeViewItem = m_treeView.GetTreeViewItem(e.Items[0]);
            if (treeViewItem != null)
            {
                treeViewItem.IsExpanded = !treeViewItem.IsExpanded;
            }

            Input.ActivateInputField();
        }

        private void OnOK(PopupWindowArgs args)
        {
            if (m_treeView.SelectedItem == null)
            {
                args.Cancel = true;
                return;
            }

            if (string.IsNullOrEmpty(Input.text))
            {
                args.Cancel = true;
                Input.ActivateInputField();
                return;
            }

            if (Input.text != null && Input.text.Length > 0 && (!char.IsLetter(Input.text[0]) || Input.text[0] == '-'))
            {
                PopupWindow.Show("Scene name is invalid", "Scene name should start with letter", "OK");
                args.Cancel = true;
                return;
            }

            if (!ProjectItem.IsValidName(Input.text))
            {
                PopupWindow.Show("Scene name is invalid", "Scene name contains invalid characters", "OK");
                args.Cancel = true;
                return;
            }

            ProjectItem selectedItem = (ProjectItem)m_treeView.SelectedItem;
            if (selectedItem.IsScene)
            {
                if (Input.text.ToLower() == selectedItem.Name.ToLower())
                {
                    PopupWindow.Show("Scene with same name already exits", "Do you want to override it?", "Yes", yes =>
                    {
                        RuntimeUndo.Purge();
                        ShowProgress = true;
                        m_projectManager.SaveScene(selectedItem, () =>
                        {
                            ShowProgress = false;
                            m_parentPopup.Close(false);
                        });
                    },
                    "No", no => Input.ActivateInputField());
                    args.Cancel = true;
                }
                else
                {
                    ProjectItem folder = selectedItem.Parent;
                    SaveSceneToFolder(args, folder);
                }
            }
            else
            {
                ProjectItem folder = selectedItem;
                SaveSceneToFolder(args, folder);
            }
        }

        private void SaveSceneToFolder(PopupWindowArgs args, ProjectItem folder)
        {
            if (folder.Children != null && folder.Children.Any(p => p.Name.ToLower() == Input.text.ToLower() && p.IsScene))
            {
                PopupWindow.Show("Scene with same name already exits", "Do you want to override it?", "Yes", yes =>
                {
                    RuntimeUndo.Purge();
                    ShowProgress = true;
                    m_projectManager.SaveScene(folder.Children.Where(p => p.Name.ToLower() == Input.text.ToLower() && p.IsScene).First(), () =>
                    {
                        ShowProgress = false;
                        m_parentPopup.Close(false);
                    });
                },
                "No", no => Input.ActivateInputField());
                args.Cancel = true;
            }
            else
            {
                ProjectItem newScene = ProjectItem.CreateScene(Input.text);
                folder.AddChild(newScene);

                RuntimeUndo.Purge();
                ShowProgress = true;
                m_projectManager.SaveScene(newScene, () =>
                {
                    ShowProgress = false;
                    m_parentPopup.Close(false);
                });
            }
        }
    }
}


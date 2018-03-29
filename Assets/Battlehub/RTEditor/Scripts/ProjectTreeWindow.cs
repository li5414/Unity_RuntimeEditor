using System;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using Battlehub.RTCommon;
using Battlehub.UIControls;
using Battlehub.RTSaveLoad;

namespace Battlehub.RTEditor
{
    public class SelectionChangedArgs<T> : EventArgs
    {
        /// <summary>
        /// Unselected Items
        /// </summary>
        public T[] OldItems
        {
            get;
            private set;
        }

        /// <summary>
        /// Selected Items
        /// </summary>
        public T[] NewItems
        {
            get;
            private set;
        }

        /// <summary>
        /// First Unselected Item
        /// </summary>
        public T OldItem
        {
            get
            {
                if (OldItems == null)
                {
                    return default(T);
                }
                if (OldItems.Length == 0)
                {
                    return default(T);
                }
                return OldItems[0];
            }
        }

        /// <summary>
        /// First Selected Item
        /// </summary>
        public T NewItem
        {
            get
            {
                if (NewItems == null)
                {
                    return default(T);
                }
                if (NewItems.Length == 0)
                {
                    return default(T);
                }
                return NewItems[0];
            }
        }

        public bool IsUserAction
        {
            get;
            private set;
        }

        //public T[] Diff()
        //{
        //    if(NewItems == null || NewItems.Length == 0)
        //    {
        //        return NewItems;
        //    }

        //    if(OldItems == null || OldItems.Length == 0)
        //    {
        //        return NewItems;
        //    }

        //    HashSet<T> oldItemsHs = new HashSet<T>(OldItems);
        //    List<T> newItems = new List<T>();
        //    for(int i = 0; i < NewItems.Length; ++i)
        //    {
        //        if (!oldItemsHs.Contains(NewItems[i]))
        //        {
        //            newItems.Add(NewItems[i]);
        //        }
        //    }

        //    return newItems.ToArray();
        //}

        public SelectionChangedArgs(T[] oldItems, T[] newItems, bool isUserAction)
        {
            OldItems = oldItems;
            NewItems = newItems;
            IsUserAction = isUserAction;
        }

        public SelectionChangedArgs(T oldItem, T newItem, bool isUserAction)
        {
            OldItems = new[] { oldItem };
            NewItems = new[] { newItem };
            IsUserAction = isUserAction;
        }

        public SelectionChangedArgs(SelectionChangedArgs args, bool isUserAction)
        {
            if(args.OldItems != null)
            {
                OldItems = args.OldItems.OfType<T>().ToArray();
            }

            if(args.NewItems != null)
            {
                NewItems = args.NewItems.OfType<T>().ToArray();
            }
            IsUserAction = isUserAction;
        }
    }

    public class ProjectTreeEventArgs : EventArgs
    {
        public ProjectItem[] ProjectItems
        {
            get;
            private set;
        }

        public ProjectItem ProjectItem
        {
            get
            {
                if (ProjectItems == null || ProjectItems.Length == 0)
                {
                    return null;
                }
                return ProjectItems[0];
            }
        }

        public ProjectTreeEventArgs(ProjectItem[] projectItems)
        {
            ProjectItems = projectItems;
        }
    }

    public class ProjectTreeRenamedEventArgs : ProjectTreeEventArgs
    {
        public string[] OldNames
        {
            get;
            private set;
        }

        public string OldName
        {
            get
            {
                if (OldNames == null || OldNames.Length == 0)
                {
                    return null;
                }
                return OldNames[0];
            }
        }

        public ProjectTreeRenamedEventArgs(ProjectItem[] projectItems, string[] oldNames)
            : base(projectItems)
        {
            OldNames = oldNames;
        }
    }

    public class ProjectTreeWindow : RuntimeEditorWindow
    {
        public event EventHandler<SelectionChangedArgs<ProjectItem>> SelectionChanged;
        public event EventHandler<ProjectTreeRenamedEventArgs> Renamed;
        public event EventHandler<ProjectTreeEventArgs> Deleted;
        public event EventHandler<ItemDropArgs> Drop;

        [SerializeField]
        private GameObject TreeViewPrefab;
        [SerializeField]
        private Sprite FolderIcon;

        [SerializeField]
        private Sprite ExposedFolderIcon;

        private TreeView m_treeView;

        [NonSerialized]
        private ProjectItem m_dragProjectItem;
        public KeyCode RemoveKey = KeyCode.Delete;
        [HideInInspector]
        public bool ShowRootFolder = true;

        [NonSerialized]
        private ProjectItem m_root;

        public ProjectItem SelectedFolder
        {
            get
            {
                return (ProjectItem)m_treeView.SelectedItem;
            }
            set
            {
                ProjectItem folder = value;
                string path = folder.ToString();
                folder = m_root.Get(path);
                TreeViewItem treeViewItem = Expand(folder);
                if (treeViewItem != null)
                {
                    SnapTo(treeViewItem.GetComponent<RectTransform>());
                    m_treeView.SelectedItem = folder;
                }
            }
        }

        private TreeViewItem Expand(ProjectItem projectItem)
        {
            TreeViewItem treeViewItem = m_treeView.GetTreeViewItem(projectItem);
            if (treeViewItem == null)
            {
                Expand(projectItem.Parent);
                treeViewItem = m_treeView.GetTreeViewItem(projectItem);
            }
            else
            {
                treeViewItem.IsExpanded = true;
            }
            return treeViewItem;
        }

        private void Toggle(ProjectItem projectItem)
        {
            TreeViewItem treeViewItem = m_treeView.GetTreeViewItem(projectItem);
            if (treeViewItem == null)
            {
                Toggle(projectItem.Parent);
                treeViewItem = m_treeView.GetTreeViewItem(projectItem);
            }
            else
            {
                treeViewItem.IsExpanded = !treeViewItem.IsExpanded;
            }
        }

        private void SnapTo(RectTransform target)
        {
            m_treeView.FixScrollRect();
            ScrollRect scrollRect = m_treeView.GetComponentInChildren<ScrollRect>();
            RectTransform contentPanel = scrollRect.content;
            contentPanel.anchoredPosition =
                (Vector2)scrollRect.transform.InverseTransformPoint(contentPanel.position)
                - (Vector2)scrollRect.transform.InverseTransformPoint(target.position);
        }

        protected override void AwakeOverride()
        {
            base.AwakeOverride();
            m_treeView = Instantiate(TreeViewPrefab).GetComponent<TreeView>();
            m_treeView.CanReorder = false;
            m_treeView.CanReparent = ShowRootFolder;
            m_treeView.CanUnselectAll = false;
            m_treeView.CanDrag = ShowRootFolder;
            m_treeView.RemoveKey = KeyCode.None;

            m_treeView.transform.SetParent(transform, false);
            m_treeView.SelectionChanged += OnSelectionChanged;
            m_treeView.ItemDataBinding += OnItemDataBinding;
            m_treeView.ItemExpanding += OnItemExpanding;
            m_treeView.ItemsRemoving += OnItemsRemoving;
            m_treeView.ItemsRemoved += OnItemsRemoved;
            m_treeView.ItemBeginEdit += OnItemBeginEdit;
            m_treeView.ItemEndEdit += OnItemEndEdit;
            m_treeView.ItemBeginDrop += OnItemBeginDrop;
            m_treeView.ItemDrop += OnItemDrop;
            m_treeView.ItemDoubleClick += OnItemDoubleClick;
        }

        protected override void UpdateOverride()
        {
            base.UpdateOverride();
            if (RuntimeEditorApplication.IsActiveWindow(this))
            {
                if (InputController.GetKeyDown(RemoveKey))
                {
                    if (m_treeView.SelectedItem != null)
                    {
                        ProjectItem projectItem = (ProjectItem)m_treeView.SelectedItem;
                        if(projectItem.Parent == null)
                        {
                            PopupWindow.Show("Unable to Remove", "Unable to remove root folder", "OK");
                        }
                        else
                        {
                            PopupWindow.Show("Remove Selected assets", "You can not undo this action", "Delete", args =>
                            {
                                m_treeView.RemoveSelectedItems();
                            },"Cancel");
                        }
                    }
                }
            }


            if (RuntimeEditorApplication.IsPointerOverWindow(this))
            {
                if (m_dragProjectItem != null)
                {
                    m_treeView.ExternalItemDrag(Input.mousePosition);
                }
            }
        }

        protected override void OnDestroyOverride()
        {
            base.OnDestroyOverride();
            Unsubscribe();
        }

        private void Unsubscribe()
        {
            m_treeView.SelectionChanged -= OnSelectionChanged;
            m_treeView.ItemDataBinding -= OnItemDataBinding;
            m_treeView.ItemExpanding -= OnItemExpanding;
            m_treeView.ItemsRemoving -= OnItemsRemoving;
            m_treeView.ItemsRemoved -= OnItemsRemoved;
            m_treeView.ItemBeginEdit -= OnItemBeginEdit;
            m_treeView.ItemEndEdit -= OnItemEndEdit;
            m_treeView.ItemBeginDrop -= OnItemBeginDrop;
            m_treeView.ItemDrop -= OnItemDrop;
            m_treeView.ItemDoubleClick -= OnItemDoubleClick;

        }

        public void LoadProject(ProjectItem root)
        {
            if (ShowRootFolder)
            {
                m_treeView.Items = new[] { root };
                m_treeView.SelectedItem = root;
            }
            else
            {
                if (root.Children != null)
                {
                    m_root.Children = root.Children.OrderBy(projectItem => projectItem.NameExt).ToList();
                    m_treeView.Items = m_root.Children.Where(projectItem => CanDisplayFolder(projectItem)).ToArray();
                    m_treeView.SelectedItem = m_treeView.Items.OfType<object>().FirstOrDefault();
                }
            }

            TreeViewItem rootTreeViewItem = m_treeView.GetTreeViewItem(0);
            if (rootTreeViewItem != null)
            {
                rootTreeViewItem.IsExpanded = true;
            }

            m_root = root;
        }

        public void BeginDragProjectItem(ProjectItem projectItem)
        {
            m_dragProjectItem = projectItem;
        }

        public ProjectItem BeginDropProjectItem()
        {
            ProjectItem dropTarget = null;
            if (m_dragProjectItem != null)
            {
                if (m_treeView.DropAction == ItemDropAction.SetLastChild)
                {
                    dropTarget = (ProjectItem)m_treeView.DropTarget;
                }

                if (RuntimeEditorApplication.IsPointerOverWindow(RuntimeWindowType.ProjectTree))
                {
                    RuntimeTools.SpawnPrefab = null;
                }
            }
            m_treeView.ExternalItemDrop();
            m_dragProjectItem = null;

            return dropTarget;
        }
        

        public void AddProjectItem(ProjectItem item, ProjectItem parent)
        {
            m_treeView.AddChild(parent, item);
        }


        public void DropProjectItem(ProjectItem dragProjectItem, ProjectItem dropTarget)
        {
            InsertOrdered(dropTarget, dragProjectItem);
        }

        private void InsertOrdered(ProjectItem parent, ProjectItem item)
        {
            parent.AddChild(item);

            m_treeView.RemoveChild(parent, item, parent.Children.Count == 1);

            TreeViewItem treeViewItem = m_treeView.GetTreeViewItem(item);
            if(treeViewItem == null)
            {
                m_treeView.AddChild(parent, item);
            }

            ProjectItem[] orderedChildren = parent.Children.OrderBy(projectItem => projectItem.NameExt).ToArray();
            int index = Array.IndexOf(orderedChildren, item);
            item.SetSiblingIndex(index);

            if (item.IsFolder)
            {
                if (index > 0)
                {
                    object prevSibling = parent.Children[index - 1];
                    m_treeView.SetNextSibling(prevSibling, item);
                }
                else
                {
                    if (parent.Children.Count > 1)
                    {
                        object nextSibling = parent.Children[1];
                        m_treeView.SetPrevSibling(nextSibling, item);
                    }
                }
            }
        }

        public void UpdateProjectItem(ProjectItem projectItem)
        {
            TreeViewItem treeViewItem = m_treeView.GetTreeViewItem(projectItem);
            if(treeViewItem != null)
            {
                m_treeView.DataBindItem(projectItem, treeViewItem);
            }
        }

        public void RemoveProjectItemsFromTree(ProjectItem[] projectItems)
        {
            for(int i = 0; i < projectItems.Length; ++i)
            {
                ProjectItem projectItem = projectItems[i];
                if(projectItem.IsFolder)
                {
                    bool isLastChild = projectItem.Parent == null || projectItem.Parent.Children.Where(p => p.IsFolder).Count() == 1;
                    m_treeView.RemoveChild(projectItem.Parent, projectItem, isLastChild);
                    if(projectItem.Parent != null)
                    {
                        projectItem.Parent.RemoveChild(projectItem);
                    }
                }
            }
        }

        private void OnItemDoubleClick(object sender, ItemArgs e)
        {
            ProjectItem projectItem = (ProjectItem)e.Items[0];
            Toggle(projectItem);
        }

      
        private void OnItemBeginDrop(object sender, ItemDropCancelArgs e)
        {
            if (!e.IsExternal)
            {
                ProjectItem dropFolder = (ProjectItem)e.DropTarget;
                Expand(dropFolder);

                if (dropFolder.Children == null)
                {
                    return;
                }

                ProjectItem[] childFolders = dropFolder.Children.Where(pi => pi.IsFolder).ToArray();
                if (childFolders.Length == 0)
                {
                    return;
                }

                foreach (ProjectItem dragFolder in e.DragItems)
                {
                    if (childFolders.Where(pi => pi.NameExt == dragFolder.NameExt).Any())
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        private void OnItemDrop(object sender, ItemDropArgs e)
        {
            ProjectItem drop = (ProjectItem)e.DropTarget;
            if (e.Action == ItemDropAction.SetLastChild)
            {
                if (Drop != null)
                {
                    Drop(this, e);
                }
            }

        }

        private void OnItemBeginEdit(object sender, TreeViewItemDataBindingArgs e)
        {
            ProjectItem item = e.Item as ProjectItem;
            if (item != null)
            {
                InputField inputField = e.EditorPresenter.GetComponentInChildren<InputField>(true);
                inputField.text = item.Name;
                inputField.ActivateInputField();
                inputField.Select();

                Image image = e.EditorPresenter.GetComponentInChildren<Image>(true);
                if(item.IsExposedFromEditor)
                {
                    image.sprite = ExposedFolderIcon;
                }
                else
                {
                    image.sprite = FolderIcon;
                }
                image.gameObject.SetActive(true);

                LayoutElement layout = inputField.GetComponent<LayoutElement>();

                Text text = e.ItemPresenter.GetComponentInChildren<Text>(true);
                text.text = item.Name;

                RectTransform rt = text.GetComponent<RectTransform>();
                layout.preferredWidth = rt.rect.width;
            }
        }

        private void OnItemEndEdit(object sender, TreeViewItemDataBindingArgs e)
        {
            InputField inputField = e.EditorPresenter.GetComponentInChildren<InputField>(true);
            Text text = e.ItemPresenter.GetComponentInChildren<Text>(true);

            ProjectItem projectItem = (ProjectItem)e.Item;
            string oldName = projectItem.Name;
            if (projectItem.Parent != null)
            {
                ProjectItem parentItem = projectItem.Parent;
                string newNameExt = inputField.text.Trim() + "." + projectItem.Ext;
                if (!string.IsNullOrEmpty(inputField.text.Trim()) && ProjectItem.IsValidName(inputField.text.Trim()) && !parentItem.Children.Any(p => p.NameExt == newNameExt))
                {
                    projectItem.Name = inputField.text.Trim();
                }
            }

            if (Renamed != null)
            {
                Renamed(this, new ProjectTreeRenamedEventArgs(new[] { projectItem }, new[] { oldName }));
            }

            text.text = projectItem.Name;

            //Following code is required to unfocus inputfield if focused and release InputManager
            if (EventSystem.current != null && !EventSystem.current.alreadySelecting)
            {
                EventSystem.current.SetSelectedGameObject(null);
            }
        }

        private void OnItemsRemoving(object sender, ItemsCancelArgs e)
        {
            if (e.Items == null)
            {
                return;
            }

            if (!RuntimeEditorApplication.IsActiveWindow(this))
            {
                e.Items.Clear();
                return;
            }

            for (int i = e.Items.Count - 1; i >= 0; i--)
            {
                ProjectItem item = (ProjectItem)e.Items[i];
                if (item.IsExposedFromEditor)
                {
                    e.Items.Remove(item);
                }
            }

            if (e.Items.Count == 0)
            {
                PopupWindow.Show("Can't remove folder", "Unable to remove folders exposed from editor", "OK");
            }
        }

        private void OnItemsRemoved(object sender, ItemsRemovedArgs e)
        {
            if(Deleted != null)
            {
                Deleted(this, new ProjectTreeEventArgs(e.Items.OfType<ProjectItem>().ToArray()));
            }
        }

        private void OnItemExpanding(object sender, ItemExpandingArgs e)
        {
            ProjectItem item = e.Item as ProjectItem;
            if (item != null)
            {
                item.Children = item.Children
                    .OrderBy(projectItem => projectItem.NameExt).ToList();
                e.Children = item.Children
                    .Where(projectItem => CanDisplayFolder(projectItem))
                    .OrderBy(projectItem => projectItem.NameExt);
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
                if (item.IsExposedFromEditor)
                {
                    image.sprite = ExposedFolderIcon;
                }
                else
                {
                    image.sprite = FolderIcon;
                }
                image.gameObject.SetActive(true);
                e.CanEdit = !item.IsExposedFromEditor && item.Parent != null;
                e.CanDrag = !item.IsExposedFromEditor && item.Parent != null;
                e.HasChildren = item.Children != null && item.Children.Count(projectItem => CanDisplayFolder(projectItem)) > 0;
            }
        }

        private void OnSelectionChanged(object sender, SelectionChangedArgs e)
        {
            if(SelectionChanged != null)
            {
                SelectionChanged(this, new SelectionChangedArgs<ProjectItem>(e, true));
            }
        }


        private bool CanDisplayFolder(ProjectItem projectItem)
        {
            return projectItem.IsFolder;// && (projectItem.ResourceTypes == null || projectItem.ResourceTypes.Any(type => m_displayResourcesHS.Contains(type)));
        }

        protected override void OnPointerEnterOverride(PointerEventData eventData)
        {
            base.OnPointerEnterOverride(eventData);
            if (m_dragProjectItem != null)
            {
                m_treeView.ExternalBeginDrag(eventData.position);
            }
        }

        protected override void OnPointerExitOverride(PointerEventData eventData)
        {
            base.OnPointerExitOverride(eventData);
            if (m_dragProjectItem != null)
            {
                m_treeView.ExternalItemDrop();
            }
        }
    }
}
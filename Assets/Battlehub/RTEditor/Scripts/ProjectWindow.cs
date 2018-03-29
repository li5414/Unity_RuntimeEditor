using UnityEngine;
using UnityEngine.UI;
using Battlehub.RTCommon;
using System.Linq;
using Battlehub.UIControls;
using Battlehub.RTSaveLoad;
using Battlehub.Utils;


namespace Battlehub.RTEditor
{
    public class ProjectWindow : RuntimeEditorWindow
    {
        private IProjectManager m_projectManager;

        [SerializeField]
        private ProjectTreeWindow m_projectTree;
        [SerializeField]
        private ProjectResourcesWindow m_projectResources;
        [SerializeField]
        private Button m_btnDuplicate;
        [SerializeField]
        private Button m_btnAddFolder;

        [SerializeField]
        private string ProjectName = "Assets";

        public KeyCode DuplicateKey = KeyCode.D;
        public KeyCode RuntimeModifierKey = KeyCode.LeftControl;
        public KeyCode EditorModifierKey = KeyCode.LeftShift;
        public KeyCode ModifierKey
        {
            get
            {
                #if UNITY_EDITOR
                return EditorModifierKey;
                #else
                return RuntimeModifierKey;
                #endif
            }
        }

        private bool ShowProgress
        {
            get; 
            set; //Show progress bar if ui here needed
        }

        private void Start()
        {
            m_projectManager = Dependencies.ProjectManager;
            if(m_projectManager == null)
            {
                Destroy(gameObject);
                return;
            }

            RuntimeEditorApplication.SaveSelectedObjectsRequired += OnSaveSelectedObjectsRequest;
                 
            m_projectResources.SelectionChanged += OnProjectResourcesSelectionChanged;
            m_projectResources.DoubleClick += OnProjectResourcesDoubleClick;
            m_projectResources.Renamed += OnProjectResourcesRenamed;
            m_projectResources.Deleted += OnProjectResourcesDeleted;
            m_projectResources.BeginDrag += OnProjectResourcesBeginDrag;
            m_projectResources.Drop += OnProjectResourcesDrop;

            m_projectTree.SelectionChanged += OnProjectTreeSelectionChanged;
            m_projectTree.Renamed += OnProjectTreeItemRenamed;
            m_projectTree.Deleted += OnProjectTreeItemDeleted;
            m_projectTree.Drop += OnProjectTreeItemDrop;

            ShowProgress = true;

            m_projectManager.LoadProject(ProjectName, root =>
            {
                ShowProgress = false;
                m_projectTree.LoadProject(root);

                if(root.Children != null)
                {
                    ProjectItem projTemplateFolder = root;//.Children.Where(c => c.NameExt == ProjectMangerConstants.PROJECT_TEMPLATE_FOLDER).FirstOrDefault();
                    if (projTemplateFolder != null)
                    {
                        m_projectTree.SelectedFolder = projTemplateFolder;
                    }
                }
            });

            m_projectManager.DynamicResourcesAdded += OnDynamicResourcesAdded;
            m_projectManager.BundledResourcesAdded += OnBundledResourcesAdded;
            m_projectManager.SceneCreated += OnSceneCreated;
            m_projectManager.SceneLoaded += OnSceneLoaded;
            m_projectManager.SceneSaved += OnSceneSaved;

            if (m_btnDuplicate != null)
            {
                m_btnDuplicate.onClick.AddListener(DuplicateProjectResources);
            }

            if(m_btnAddFolder != null)
            {
                m_btnAddFolder.onClick.AddListener(AddFolder);
            }
        }

        protected override void OnDestroyOverride()
        {
            RuntimeEditorApplication.SaveSelectedObjectsRequired -= OnSaveSelectedObjectsRequest;

            if (m_projectResources != null)
            {
                m_projectResources.DoubleClick -= OnProjectResourcesDoubleClick;
                m_projectResources.SelectionChanged -= OnProjectResourcesSelectionChanged;
                m_projectResources.Renamed -= OnProjectResourcesRenamed;
                m_projectResources.Deleted -= OnProjectResourcesDeleted;
                m_projectResources.BeginDrag -= OnProjectResourcesBeginDrag;
                m_projectResources.Drop -= OnProjectResourcesDrop;
            }

            if(m_projectTree != null)
            {
                m_projectTree.SelectionChanged -= OnProjectTreeSelectionChanged;
                m_projectTree.Renamed -= OnProjectTreeItemRenamed;
                m_projectTree.Deleted -= OnProjectTreeItemDeleted;
                m_projectTree.Drop -= OnProjectTreeItemDrop;
            }
            
            if(m_projectManager != null)
            {
                m_projectManager.DynamicResourcesAdded -= OnDynamicResourcesAdded;
                m_projectManager.BundledResourcesAdded -= OnBundledResourcesAdded;
                m_projectManager.SceneCreated -= OnSceneCreated;
                m_projectManager.SceneSaved -= OnSceneSaved;
                m_projectManager.SceneLoaded -= OnSceneLoaded;
            }
           

            if (m_btnDuplicate != null)
            {
                m_btnDuplicate.onClick.RemoveListener(DuplicateProjectResources);
            }

            if (m_btnAddFolder != null)
            {
                m_btnAddFolder.onClick.RemoveListener(AddFolder);
            }
        }

        protected override void UpdateOverride()
        {
            base.UpdateOverride();
            if (InputController.GetKeyDown(DuplicateKey) && InputController.GetKey(ModifierKey))
            {
                if (RuntimeEditorApplication.IsActiveWindow(m_projectResources))
                {
                    DuplicateProjectResources();
                }
            }
        }

        private void AddFolder()
        {
            ProjectItem parent = m_projectTree.SelectedFolder;
            
            ShowProgress = true;

            ProjectItemObjectPair[] selection = GetSelection(false);
            m_projectManager.SaveObjects(selection.Where(iop => iop.IsResource).ToArray(), () =>
            {
                m_projectManager.CreateFolder("Folder", parent, folder =>
                {
                    m_projectTree.AddProjectItem(folder, parent);
                    m_projectManager.GetOrCreateObjects(parent, objects =>
                    {
                        ShowProgress = false;
                        m_projectResources.SetSelectedItems(objects, new[] { folder });
                        m_projectResources.SetObjects(objects, false);
                    });
                });
            });
        }

        private bool CanDuplicate(ProjectItemObjectPair itemObjectPair)
        {
            ProjectItem projectItem = itemObjectPair.ProjectItem;

            if (projectItem.TypeCode == ProjectItemTypes.Texture)
            {
                return (itemObjectPair.Object as Texture2D).IsReadable();
            }
            else if (projectItem.TypeCode == ProjectItemTypes.ProceduralMaterial)
            {
                return false; //unable to duplicate procedural materials
            }

            return projectItem != null && (!projectItem.IsExposedFromEditor || !projectItem.IsFolder);
        }


        private ProjectItemObjectPair[] GetSelection(bool checkIfCanDuplicate)
        {
            
            ProjectItemObjectPair[] selection = m_projectResources.SelectionToProjectItemObjectPair(m_projectResources.SelectedItems);
            if (selection == null)
            {
                return new ProjectItemObjectPair[0]; 
            }
            selection = selection.Where(iop => iop.ProjectItem != null && (!checkIfCanDuplicate || CanDuplicate(iop))).ToArray();
            if (selection.Length == 0)
            {
                return new ProjectItemObjectPair[0]; 
            }

            return selection;
        }

        private void DuplicateProjectResources()
        {
            ProjectItemObjectPair[] selection = GetSelection(true);

            if (selection.Length == 0)
            {
                return;
            }

            ShowProgress = true;
            m_projectManager.SaveObjects(selection.Where(iop => iop.IsResource).ToArray(), () =>
            {
                m_projectManager.Duplicate(selection.Select(p => p.ProjectItem).ToArray(), duplicatedItems =>
                {
                    ProjectItem parent = null;
                    for (int i = 0; i < selection.Length; ++i)
                    {
                        parent = selection[i].ProjectItem.Parent;
                        if (parent != null)
                        {
                            ProjectItem duplicatedItem = duplicatedItems[i];
                            if (duplicatedItem.IsFolder)
                            {
                                m_projectTree.AddProjectItem(duplicatedItem, parent);
                                m_projectTree.DropProjectItem(duplicatedItem, parent);
                            }
                            else
                            {
                                parent.AddChild(duplicatedItem);
                            }
                        }
                    }

                    if (parent != null)
                    {
                        m_projectTree.SelectedFolder = parent;
                    }

                    m_projectManager.GetOrCreateObjects(m_projectTree.SelectedFolder, objects =>
                    {
                        ShowProgress = false;

                        m_projectResources.SetSelectedItems(objects, duplicatedItems);
                        m_projectResources.SetObjects(objects, false);

                        if(m_projectResources.SelectedItems != null)
                        {
                            RuntimeSelection.objects = m_projectResources.SelectedItems.Where(o => o != null).ToArray();
                        }
                    });
                });
            }); 
        }

        private void OnBundledResourcesAdded(object sender, ProjectManagerEventArgs e)
        {
            ShowProgress = true;
            OnResourcesAdded(e);
        }

        private void OnDynamicResourcesAdded(object sender, ProjectManagerEventArgs e)
        {
            ShowProgress = true;
            OnResourcesAdded(e);
        }

        private void OnResourcesAdded(ProjectManagerEventArgs e)
        {
            ProjectItemObjectPair[] selection = GetSelection(false);
            m_projectManager.SaveObjects(selection.Where(iop => iop.IsResource).ToArray(), () =>
            {
                m_projectManager.GetOrCreateObjects(m_projectTree.SelectedFolder, objects =>
                {
                    ShowProgress = false;

                    m_projectResources.SetSelectedItems(objects, e.ProjectItems.Take(1).ToArray());
                    m_projectResources.SetObjects(objects, false);

                    if (m_projectResources.SelectedItems != null)
                    {
                        RuntimeSelection.activeObject = m_projectResources.SelectedItems.Where(o => o != null).FirstOrDefault();
                    }
                });
            });
        }

        private void OnSceneCreated(object sender, ProjectManagerEventArgs e)
        {
            ShowProgress = true;
            m_projectManager.GetOrCreateObjects(m_projectTree.SelectedFolder, objects =>
            {
                ShowProgress = false;
                m_projectResources.SetObjects(objects, false);
            });
        }

        private void OnSceneSaved(object sender, ProjectManagerEventArgs e)
        {
            ShowProgress = true;
            m_projectManager.GetOrCreateObjects(m_projectTree.SelectedFolder, objects =>
            {
                ShowProgress = false;
                m_projectResources.SetObjects(objects, false);
            });
        }


        private void OnSceneLoaded(object sender, ProjectManagerEventArgs e)
        {
            ShowProgress = true;
            m_projectManager.GetOrCreateObjects(m_projectTree.SelectedFolder, objects =>
            {
                ShowProgress = false;
                m_projectResources.SetObjects(objects, false);
            });
        }

        private void OnProjectResourcesSelectionChanged(object sender, SelectionChangedArgs<ProjectItemObjectPair> e)
        {
            if (e.IsUserAction)
            {
                RuntimeSelection.objects = e.NewItems.Where(p => p.Object != null).Select(p => p.Object).ToArray();
            }

            ProjectItemObjectPair[] unselected = e.OldItems;
            if (unselected != null)
            {
                unselected = unselected.Where(
                    p => p.IsResource &&
                    // do not save mesh each time it unselected
                    p.ProjectItem.TypeCode != ProjectItemTypes.Mesh
                    ).ToArray();
                if (unselected.Length != 0)
                {
                    ShowProgress = true;
                    m_projectManager.SaveObjects(unselected, () =>
                    {
                        ShowProgress = false;
                    });
                }
            }

            UpdateCanDuplicateButtonState(e.NewItem);
        }

        private void UpdateCanDuplicateButtonState(ProjectItemObjectPair itemObjectPair)
        {
            if (m_btnDuplicate != null)
            {
                if (itemObjectPair == null)
                {
                    m_btnDuplicate.gameObject.SetActive(false);
                }
                else
                {
                    m_btnDuplicate.gameObject.SetActive(CanDuplicate(itemObjectPair));
                }
            }
        }

        private void OnProjectTreeSelectionChanged(object sender, SelectionChangedArgs<ProjectItem> e)
        {
            if (m_btnAddFolder != null)
            {
                m_btnAddFolder.gameObject.SetActive(e.NewItem != null);
            }

            ShowProgress = true;
            m_projectManager.SaveObjects(GetSelection(false).Where(
                iop => iop.IsResource &&
                // do not save mesh each time it unselected
                iop.ProjectItem.TypeCode != ProjectItemTypes.Mesh
                ).ToArray(), () =>
            {
                m_projectManager.GetOrCreateObjects(e.NewItems, objects =>
                {
                    ShowProgress = false;
                    m_projectResources.SetObjects(objects, true);

                    UpdateCanDuplicateButtonState(GetSelection(true).FirstOrDefault());

                });
            });
        }


        private void OnSaveSelectedObjectsRequest()
        {
            
            ShowProgress = true;
            m_projectManager.SaveObjects(GetSelection(false).Where(
                 iop => iop.IsResource &&
                 // do not save mesh each time it unselected
                 iop.ProjectItem.TypeCode != ProjectItemTypes.Mesh).ToArray(), () =>
                 {
                     ShowProgress = false;
                 });
        }
                 
        private void OnProjectResourcesDoubleClick(object sender, ProjectResourcesEventArgs e)
        {
            if(e.ItemObjectPair != null)
            {
                ProjectItem projectItem = e.ItemObjectPair.ProjectItem;
                if(projectItem.IsFolder)
                {
                    m_projectTree.SelectedFolder = projectItem;
                }
                else if(projectItem.IsScene)
                {
                    if (RuntimeEditorApplication.IsPlaying)
                    {
                        PopupWindow.Show("Unable to load scene", "Unable to load scene in play mode", "OK");
                        return;
                    }

                    RuntimeUndo.Purge();

                    ExposeToEditor[] editorObjects = ExposeToEditor.FindAll(ExposeToEditorObjectType.EditorMode, false).Select(go => go.GetComponent<ExposeToEditor>()).ToArray();
                    for (int i = 0; i < editorObjects.Length; ++i)
                    {
                        ExposeToEditor exposeToEditor = editorObjects[i];
                        if (exposeToEditor != null)
                        {
                            DestroyImmediate(exposeToEditor.gameObject);
                        }
                    }

                    ShowProgress = true;
                    m_projectManager.LoadScene(projectItem, () =>
                    {
                        ShowProgress = false;
                    });

                }
            }
        }

        private void OnProjectResourcesRenamed(object sender, ProjectResourcesRenamedEventArgs e)
        {
            if (e.ItemObjectPair != null)
            {
                ProjectItem projectItem = e.ItemObjectPair.ProjectItem;
                string name = projectItem.Name;
                projectItem.Name = e.OldName;
                ShowProgress = true;
                m_projectManager.Rename(projectItem, name, () =>
                {
                    m_projectTree.UpdateProjectItem(projectItem);
                    ShowProgress = false;
                });
            }
        }

        private void OnProjectTreeItemRenamed(object sender, ProjectTreeRenamedEventArgs e)
        {
            if (e.ProjectItem != null)
            {
                ProjectItem projectItem = e.ProjectItem;
                string name = projectItem.Name;
                projectItem.Name = e.OldName;
                ShowProgress = true;
                m_projectManager.Rename(projectItem, name, () =>
                {
                    m_projectResources.UpdateProjectItem(projectItem);
                    ShowProgress = false;
                });
            }
        }

        private void OnProjectResourcesDeleted(object sender, ProjectResourcesEventArgs e)
        {
            if (e.ItemObjectPair != null)
            {
                ProjectItem[] projectItems = e.ItemObjectPairs.Select(p => p.ProjectItem).ToArray();
                projectItems = ProjectItem.GetRootItems(projectItems);
                
                ShowProgress = true;
                m_projectManager.Delete(projectItems, () =>
                {
                    m_projectTree.RemoveProjectItemsFromTree(projectItems);
                    ShowProgress = false;
                });
            }
        }

        private void OnProjectTreeItemDeleted(object sender, ProjectTreeEventArgs e)
        {
            if (e.ProjectItem != null)
            {
                ProjectItem[] projectItems = e.ProjectItems;
                projectItems = ProjectItem.GetRootItems(projectItems);
                ProjectItem firstParent = projectItems[0].Parent;

                ShowProgress = true;
                m_projectManager.Delete(projectItems, () =>
                {
                    for(int i = 0; i < projectItems.Length; ++i)
                    {
                        ProjectItem parent = projectItems[i].Parent;
                        if(parent != null)
                        {
                            parent.RemoveChild(projectItems[i]);
                        }
                    }
                    ShowProgress = false;
                    m_projectTree.SelectedFolder = firstParent;
                });  
            }
        }


        private void OnProjectResourcesBeginDrag(object sender, ProjectResourcesEventArgs e)
        {
            if(!e.ItemObjectPair.ProjectItem.IsExposedFromEditor)
            {
                m_projectTree.BeginDragProjectItem(e.ItemObjectPair.ProjectItem);
            }
            
        }

        private void OnProjectResourcesDrop(object sender, ProjectResourcesEventArgs e)
        {
            if (e.ItemObjectPair.ProjectItem.IsExposedFromEditor)
            {
                return;
            }
            ProjectItem dragItem = e.ItemObjectPair.ProjectItem;
            ProjectItem dropTarget = m_projectTree.BeginDropProjectItem();
            if(dragItem == dropTarget)
            {
                return;
            }

            if(dropTarget != null)
            {
                if(dropTarget.Children != null && dropTarget.Children.Any(c => c.NameExt == dragItem.NameExt))
                {
                    return;
                }

                m_projectManager.Move(new[] { dragItem }, dropTarget, () =>
                {
                    if(dragItem.IsFolder)
                    {
                        m_projectTree.DropProjectItem(dragItem, dropTarget);
                    }
                    if (m_projectTree.SelectedFolder.Children == null ||
                        !m_projectTree.SelectedFolder.Children.Contains(e.ItemObjectPair.ProjectItem))
                    {
                        m_projectResources.RemoveProjectItem(e.ItemObjectPair);
                    }
                });
            }
        }

        private void OnProjectTreeItemDrop(object sender, ItemDropArgs e)
        {

            ProjectItem[] dragItems = e.DragItems.OfType<ProjectItem>().ToArray();
            ProjectItem dropTarget = (ProjectItem)e.DropTarget;
            m_projectManager.Move(dragItems, dropTarget, () =>
            {
                for (int i = 0; i < e.DragItems.Length; ++i)
                {
                    ProjectItem dragItem = (ProjectItem)e.DragItems[i];

                    m_projectTree.DropProjectItem(dragItem, dropTarget);

                    if (m_projectTree.SelectedFolder != null &&
                        (m_projectTree.SelectedFolder.Children == null ||
                        !m_projectTree.SelectedFolder.Children.Contains(dragItem)))
                    {
                        m_projectResources.RemoveProjectItem(dragItem);
                    }
                }
            });
        }

    }
}

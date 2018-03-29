using UnityEngine;
using UnityEngine.UI;

using Battlehub.UIControls;
using Battlehub.RTCommon;
using Battlehub.RTSaveLoad;

namespace Battlehub.RTEditor
{
    public class ToolsPanel : MonoBehaviour
    {
        private bool m_handleValueChange = true;

        public Toggle ViewToggle;
        public Toggle MoveToggle;
        public Toggle RotateToggle;
        public Toggle ScaleToggle;

        public Toggle PivotRotationToggle;
        public Toggle PivotModeToggle;
        public Toggle WireframeToggle;

        public Toggle AutoFocusToggle;
        public Toggle VertexSnappingToggle;
        public Toggle UnitSnappingToggle;

        public Toggle PlayToggle;

        public GameObject SaveSceneDialog;

        public Button BtnNew;
        public Button BtnSave;
        public Button BtnSaveAs;
        public Button BtnUndo;
        public Button BtnRedo;

        private IProjectManager m_projectManager;

        private void OnEnable()
        {
            m_projectManager = Dependencies.ProjectManager;

            OnRuntimeToolChanged();
            OnPivotRotationChanged();
            OnPivotModeChanged();
            OnBoundingBoxSnappingChanged();
            OnUnitSnappingChanged();
            OnAutoFocusChanged();
            OnPlaymodeStateChanged();

            UpdateUndoRedoButtonsState();
            

            RuntimeTools.ToolChanged += OnRuntimeToolChanged;
            RuntimeTools.PivotRotationChanged += OnPivotRotationChanged;
            RuntimeTools.PivotModeChanged += OnPivotModeChanged;
            RuntimeTools.AutoFocusChanged += OnAutoFocusChanged;
            RuntimeTools.IsSnappingChanged += OnBoundingBoxSnappingChanged;
            RuntimeTools.UnitSnappingChanged += OnUnitSnappingChanged;
            RuntimeEditorApplication.PlaymodeStateChanged += OnPlaymodeStateChanged;

            if (m_projectManager != null)
            {
                m_projectManager.SceneLoading += OnSceneLoading;
                m_projectManager.SceneLoaded += OnSceneLoaded;
                m_projectManager.SceneSaving += OnSceneSaving;
                m_projectManager.SceneSaved += OnSceneSaved;
            }

            UpdateLoadSaveButtonsState();

            RuntimeUndo.UndoCompleted += OnUndoCompleted;
            RuntimeUndo.RedoCompleted += OnRedoCompleted;
            RuntimeUndo.StateChanged += OnStateChanged;
            
            if (ViewToggle != null)
            {
                ViewToggle.onValueChanged.AddListener(OnViewToggleValueChanged);
            }
            if (MoveToggle != null)
            {
                MoveToggle.onValueChanged.AddListener(OnMoveToggleValueChanged);
            }
            if (RotateToggle != null)
            {
                RotateToggle.onValueChanged.AddListener(OnRotateToggleValueChanged);
            }
            if (ScaleToggle != null)
            {
                ScaleToggle.onValueChanged.AddListener(OnScaleToggleValueChanged);
            }
            if(PivotRotationToggle != null)
            {
                PivotRotationToggle.onValueChanged.AddListener(OnPivotRotationToggleValueChanged);
            }
            if(PivotModeToggle != null)
            {
                PivotModeToggle.onValueChanged.AddListener(OnPivotModeToggleValueChanged);
            }
            if(WireframeToggle != null)
            {
                WireframeToggle.onValueChanged.AddListener(OnWireframeToggleValueChanged);
            }
            if(UnitSnappingToggle != null)
            {
                UnitSnappingToggle.onValueChanged.AddListener(OnUnitSnappingToggleValueChanged);
            }
            if(VertexSnappingToggle != null)
            {
                VertexSnappingToggle.onValueChanged.AddListener(OnBoundingBoxSnappingToggleValueChanged);
            }
            if(AutoFocusToggle != null)
            {
                AutoFocusToggle.onValueChanged.AddListener(OnAutoFocusToggleValueChanged);
            }
            if(PlayToggle != null)
            {
                PlayToggle.onValueChanged.AddListener(OnPlayToggleValueChanged);
            }

            if(BtnSave != null)
            {
                BtnSave.onClick.AddListener(OnSaveClick);
            }

            if(BtnSaveAs != null)
            {
                BtnSaveAs.onClick.AddListener(OnSaveAsClick);
            }

            if(BtnNew != null)
            {
                BtnNew.onClick.AddListener(OnNewClick);
            }
            if(BtnUndo != null)
            {
                BtnUndo.onClick.AddListener(OnUndoClick);
            }
            if(BtnRedo != null)
            {
                BtnRedo.onClick.AddListener(OnRedoClick);
            }
        }


        private void OnDisable()
        {
            RuntimeTools.ToolChanged -= OnRuntimeToolChanged;
            RuntimeTools.PivotRotationChanged -= OnPivotRotationChanged;
            RuntimeTools.PivotModeChanged -= OnPivotModeChanged;
            RuntimeTools.AutoFocusChanged -= OnAutoFocusChanged;
            RuntimeTools.UnitSnappingChanged -= OnUnitSnappingChanged;
            RuntimeTools.IsSnappingChanged -= OnBoundingBoxSnappingChanged;
            RuntimeEditorApplication.PlaymodeStateChanged -= OnPlaymodeStateChanged;
            if (m_projectManager != null)
            {
                m_projectManager.SceneLoading -= OnSceneLoading;
                m_projectManager.SceneLoaded -= OnSceneLoaded;
                m_projectManager.SceneSaving -= OnSceneSaving;
                m_projectManager.SceneSaved -= OnSceneSaved;
            }
            RuntimeUndo.UndoCompleted -= OnUndoCompleted;
            RuntimeUndo.RedoCompleted -= OnRedoCompleted;
            RuntimeUndo.StateChanged -= OnStateChanged;

            if (ViewToggle != null)
            {
                ViewToggle.onValueChanged.RemoveListener(OnViewToggleValueChanged);
            }
            if (MoveToggle != null)
            {
                MoveToggle.onValueChanged.RemoveListener(OnMoveToggleValueChanged);
            }
            if (RotateToggle != null)
            {
                RotateToggle.onValueChanged.RemoveListener(OnRotateToggleValueChanged);
            }
            if (ScaleToggle != null)
            {
                ScaleToggle.onValueChanged.RemoveListener(OnScaleToggleValueChanged);
            }
            if (PivotRotationToggle != null)
            {
                PivotRotationToggle.onValueChanged.RemoveListener(OnPivotRotationToggleValueChanged);
            }
            if (PivotModeToggle != null)
            {
                PivotModeToggle.onValueChanged.RemoveListener(OnPivotModeToggleValueChanged);
            }
            if (WireframeToggle != null)
            {
                WireframeToggle.onValueChanged.RemoveListener(OnWireframeToggleValueChanged);
            }
            if (UnitSnappingToggle != null)
            {
                UnitSnappingToggle.onValueChanged.RemoveListener(OnUnitSnappingToggleValueChanged);
            }
            if (VertexSnappingToggle != null)
            {
                VertexSnappingToggle.onValueChanged.RemoveListener(OnBoundingBoxSnappingToggleValueChanged);
            }
            if (AutoFocusToggle != null)
            {
                AutoFocusToggle.onValueChanged.RemoveListener(OnAutoFocusToggleValueChanged);
            }
            if (PlayToggle != null)
            {
                PlayToggle.onValueChanged.RemoveListener(OnPlayToggleValueChanged);
            }

            if (BtnSave != null)
            {
                BtnSave.onClick.RemoveListener(OnSaveClick);
            }

            if (BtnSaveAs != null)
            {
                BtnSaveAs.onClick.RemoveListener(OnSaveAsClick);
            }

            if (BtnNew != null)
            {
                BtnNew.onClick.RemoveListener(OnNewClick);
            }
            if (BtnUndo != null)
            {
                BtnUndo.onClick.RemoveListener(OnUndoClick);
            }
            if (BtnRedo != null)
            {
                BtnRedo.onClick.RemoveListener(OnRedoClick);
            }
        }

        private void OnViewToggleValueChanged(bool value)
        {
            if(!m_handleValueChange)
            {
                return;
            }
            if (value)
            {
                RuntimeTools.Current = RuntimeTool.View;
                m_handleValueChange = false;
                RotateToggle.isOn = false;
                ScaleToggle.isOn = false;
                MoveToggle.isOn = false;
                m_handleValueChange = true;
            }
            else
            {
                if (RuntimeTools.Current == RuntimeTool.View)
                {
                    ViewToggle.isOn = true;
                }
            }
        }
        private void OnMoveToggleValueChanged(bool value)
        {
            if (!m_handleValueChange)
            {
                return;
            }
            if (value)
            {
                RuntimeTools.Current = RuntimeTool.Move;
                m_handleValueChange = false;
                RotateToggle.isOn = false;
                ScaleToggle.isOn = false;
                ViewToggle.isOn = false;
                m_handleValueChange = true;

            }
            else
            {
                if (RuntimeTools.Current == RuntimeTool.Move)
                {
                    MoveToggle.isOn = true;
                }
            }
        }

        private void OnRotateToggleValueChanged(bool value)
        {
            if (!m_handleValueChange)
            {
                return;
            }
            if (value)
            {
                RuntimeTools.Current = RuntimeTool.Rotate;
                m_handleValueChange = false;
                ViewToggle.isOn = false;
                ScaleToggle.isOn = false;
                MoveToggle.isOn = false;
                m_handleValueChange = true;
            }
            else
            {
                if (RuntimeTools.Current == RuntimeTool.Rotate)
                {
                    RotateToggle.isOn = true;
                }
            }

        }

        private void OnScaleToggleValueChanged(bool value)
        {
            if (!m_handleValueChange)
            {
                return;
            }
            if (value)
            {
                RuntimeTools.Current = RuntimeTool.Scale;
                m_handleValueChange = false;
                ViewToggle.isOn = false;
                RotateToggle.isOn = false;
                MoveToggle.isOn = false;
                m_handleValueChange = true;
            }
            else
            {
                if(RuntimeTools.Current == RuntimeTool.Scale)
                {
                    ScaleToggle.isOn = true;
                }
            }
        }

        private void OnPivotRotationToggleValueChanged(bool value)
        {
            if(value)
            {
                RuntimeTools.PivotRotation = RuntimePivotRotation.Global;
            }
            else
            {
                RuntimeTools.PivotRotation = RuntimePivotRotation.Local;
            }
        }


        private void OnPivotModeToggleValueChanged(bool value)
        {
            if (value)
            {
                RuntimeTools.PivotMode = RuntimePivotMode.Center;
            }
            else
            {
                RuntimeTools.PivotMode = RuntimePivotMode.Pivot;
            }
        }

        private void OnWireframeToggleValueChanged(bool value)
        {
            //NOT IMPLEMENTED
        }

        private void OnAutoFocusToggleValueChanged(bool value)
        {
            RuntimeTools.AutoFocus = value;
        }

        private void OnUnitSnappingToggleValueChanged(bool value)
        {
            RuntimeTools.UnitSnapping = value;
        }

        private void OnBoundingBoxSnappingToggleValueChanged(bool value)
        {
            RuntimeTools.IsSnapping = value;
        }

        private void OnPlayToggleValueChanged(bool value)
        {
            RuntimeEditorApplication.IsPlaying = value;
        }

        private void OnPlaymodeStateChanged()
        {
            if(RuntimeEditorApplication.IsPlaying)
            {
                RuntimeEditorApplication.ActivateWindow(RuntimeWindowType.GameView);
            }
            else
            {
                RuntimeEditorApplication.ActivateWindow(RuntimeWindowType.SceneView);
            }
            
            if(PlayToggle != null)
            {
                PlayToggle.isOn = RuntimeEditorApplication.IsPlaying;
            }

            UpdateLoadSaveButtonsState();
        }

        private void OnPivotRotationChanged()
        {
            if(PivotRotationToggle != null)
            {
                if (RuntimeTools.PivotRotation == RuntimePivotRotation.Global)
                {
                    PivotRotationToggle.isOn = true;
                }
                else
                {
                    PivotRotationToggle.isOn = false;
                }
            }
        }

        private void OnPivotModeChanged()
        {
            if (PivotModeToggle != null)
            {
                if (RuntimeTools.PivotMode == RuntimePivotMode.Center)
                {
                    PivotModeToggle.isOn = true;
                }
                else
                {
                    PivotModeToggle.isOn = false;
                }
            }
        }

        private void OnRuntimeToolChanged()
        {
            if(!m_handleValueChange)
            {
                return;
            }
            if (ViewToggle != null)
            {
                ViewToggle.isOn = RuntimeTools.Current == RuntimeTool.View;
            }
            if (MoveToggle != null)
            {
                MoveToggle.isOn = RuntimeTools.Current == RuntimeTool.Move;
            }
            if (RotateToggle != null)
            {
                RotateToggle.isOn = RuntimeTools.Current == RuntimeTool.Rotate;
            }
            if (ScaleToggle != null)
            {
                ScaleToggle.isOn = RuntimeTools.Current == RuntimeTool.Scale;
            }
        }

        private void OnAutoFocusChanged()
        {
            if(AutoFocusToggle != null)
            {
                AutoFocusToggle.isOn = RuntimeTools.AutoFocus;
            }
        }

        private void OnUnitSnappingChanged()
        {
            if(UnitSnappingToggle != null)
            {
                UnitSnappingToggle.isOn = RuntimeTools.UnitSnapping;
            }
        }

        private void OnBoundingBoxSnappingChanged()
        {
            if(VertexSnappingToggle != null)
            {
                VertexSnappingToggle.isOn = RuntimeTools.IsSnapping;
            }
        }

        private void UpdateLoadSaveButtonsState()
        {
            if(BtnSave != null)
            {
                BtnSave.interactable = m_projectManager != null && (RuntimeUndo.CanRedo || RuntimeUndo.CanUndo) && !RuntimeEditorApplication.IsPlaying;
            }

            if (BtnSaveAs != null)
            {
                BtnSaveAs.interactable = m_projectManager != null && (RuntimeUndo.CanRedo || RuntimeUndo.CanUndo) && !RuntimeEditorApplication.IsPlaying;
            }

            if (BtnNew != null)
            {
                BtnNew.interactable = m_projectManager != null && !RuntimeEditorApplication.IsPlaying; 
            }
        }

        private void UpdateUndoRedoButtonsState()
        {
            if (BtnUndo != null)
            {
                BtnUndo.interactable = RuntimeUndo.CanUndo;
            }

            if (BtnRedo != null)
            {
                BtnRedo.interactable = RuntimeUndo.CanRedo;
            }
        }

        private void OnSaveClick()
        {
            if (RuntimeEditorApplication.IsPlaying)
            {
                PopupWindow.Show("Save Scene", "Scene can not be saved in playmode", "OK");
                return;
            }
            if (m_projectManager.ActiveScene == null)
            {
                PopupWindow.Show("Save Scene", "Unable to save. ActiveScene is null", "OK");
                return;
            }

            if(m_projectManager.ActiveScene.Parent == null)
            {
                GameObject saveSceneDialog = Instantiate(SaveSceneDialog);
                saveSceneDialog.transform.position = Vector3.zero;

                PopupWindow.Show("Save Scene As", saveSceneDialog.transform, "Save",
                    args =>
                    {
                        if(!args.Cancel)
                        {
                            BtnSave.interactable = false;
                            BtnSaveAs.interactable = false;
                        }
                    },
                    "Cancel");
            }
            else
            {
                RuntimeUndo.Purge();
                m_projectManager.SaveScene(m_projectManager.ActiveScene, () =>
                {
                });
            }
        }

        private void OnSaveAsClick()
        {
            if (RuntimeEditorApplication.IsPlaying)
            {
                PopupWindow.Show("Save Scene", "Scene can not be saved in playmode", "OK");
                return;
            }

            if (m_projectManager == null)
            {
                Debug.LogError("Project Manager is null");
            }

            if (m_projectManager.ActiveScene == null)
            {
                PopupWindow.Show("Save Scene", "Unable to save. ActiveScene is null", "OK");
                return;
            }


            GameObject saveSceneDialog = Instantiate(SaveSceneDialog);
            saveSceneDialog.transform.position = Vector3.zero;

            PopupWindow.Show("Save Scene As", saveSceneDialog.transform, "Save",
                args =>
                {
                    if (!args.Cancel)
                    {
                        BtnSave.interactable = false;
                        BtnSaveAs.interactable = false;
                    }
                },
                "Cancel");

        }

        private void OnNewClick()
        {
            if (RuntimeEditorApplication.IsPlaying)
            {
                PopupWindow.Show("Create Scene", "Scene can not be create in playmode", "OK");
                return;
            }

            if (m_projectManager == null)
            {
                Debug.LogError("Project Manager is null");
            }

            PopupWindow.Show("Create Scene", "Are you sure you want to create new scene?", "Yes",
                args =>
                {
                    m_projectManager.CreateScene();

                    if (BtnSave != null)
                    {
                        BtnSave.interactable = true;
                        BtnSaveAs.interactable = true;
                    }
                    
                }, "No");
        }

        private void OnUndoClick()
        {
            RuntimeUndo.Undo();
        }

        private void OnRedoClick()
        {
            RuntimeUndo.Redo();
        }

        private void OnStateChanged()
        {
            UpdateUndoRedoButtonsState();
            UpdateLoadSaveButtonsState();
        }

        private void OnRedoCompleted()
        {
            UpdateUndoRedoButtonsState();
            UpdateLoadSaveButtonsState();
        }

        private void OnUndoCompleted()
        {
            UpdateUndoRedoButtonsState();
            UpdateLoadSaveButtonsState();
        }

        private void OnSceneSaving(object sender, ProjectManagerEventArgs args)
        {
            
        }

        private void OnSceneSaved(object sender, ProjectManagerEventArgs args)
        {
            UpdateUndoRedoButtonsState();
            UpdateLoadSaveButtonsState();
        }

        private void OnSceneLoading(object sender, ProjectManagerEventArgs args)
        {

        }

        private void OnSceneLoaded(object sender, ProjectManagerEventArgs args)
        {
            UpdateUndoRedoButtonsState();
            UpdateLoadSaveButtonsState();
        }
    }
}

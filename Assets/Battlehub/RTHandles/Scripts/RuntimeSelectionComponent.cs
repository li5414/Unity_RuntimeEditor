using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Battlehub.Utils;
using Battlehub.RTCommon;
namespace Battlehub.RTHandles
{
    public delegate void UnityEditorToolChanged();
    public class UnityEditorToolsListener
    {
        public static event UnityEditorToolChanged ToolChanged;

        #if UNITY_EDITOR
        private static UnityEditor.Tool m_tool;
        static UnityEditorToolsListener()
        {
            m_tool = UnityEditor.Tools.current;
        }
        #endif

        public static void Update()
        {
            #if UNITY_EDITOR
            if (m_tool != UnityEditor.Tools.current)
            {
                if (ToolChanged != null)
                {
                    ToolChanged();
                }
                m_tool = UnityEditor.Tools.current;
            }
            #endif
        }
    }

    /// <summary>
    /// Basic selection component (handles user input and allow RuntimeSelection to be changed_
    /// </summary>
    public class RuntimeSelectionComponent : RuntimeEditorWindow
    {
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

        public KeyCode SelectAllKey = KeyCode.A;
        public KeyCode MultiselectKey = KeyCode.LeftControl;
        public KeyCode MultiselectKey2 = KeyCode.RightControl;
        public KeyCode RangeSelectKey = KeyCode.LeftShift;
        public Camera SceneCamera;

        protected virtual LayerMask LayerMask
        {
            get { return -1; }
        }

        protected virtual bool IPointerOverEditorArea
        {
            get
            {
                return !RuntimeTools.IsPointerOverGameObject();
            }
        }

        protected PositionHandle PositionHandle
        {
            get { return m_positionHandle; }
        }

        protected RotationHandle RotationHandle
        {
            get { return m_rotationHandle; }
        }

        protected ScaleHandle ScaleHandle
        {
            get { return m_scaleHandle; }
        }

        private PositionHandle m_positionHandle;
        private RotationHandle m_rotationHandle;
        private ScaleHandle m_scaleHandle;
        public Transform HandlesParent;

        private void Start()
        {
            if (BoxSelection.Current == null)
            {
                GameObject boxSelection = new GameObject();
                boxSelection.name = "BoxSelection";
                boxSelection.transform.SetParent(transform, false);
                boxSelection.AddComponent<BoxSelection>();
            }

            StartOverride();
        }

        private void OnEnable()
        {
            OnEnableOverride();
        }

        private void OnDisable()
        {
            OnDisableOverride();
        }

        private void LateUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(RuntimeTools.ActiveTool != null && RuntimeTools.ActiveTool != BoxSelection.Current)
                {
                    return;
                }
                
                if (!IPointerOverEditorArea)
                {
                    return;
                }

                if (RuntimeTools.IsViewing)
                {
                    return;
                }
          
                bool rangeSelect = InputController.GetKey(RangeSelectKey);
                bool multiselect = InputController.GetKey(MultiselectKey) || InputController.GetKey(MultiselectKey2) || rangeSelect;
                Ray ray = SceneCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                //if (Physics.Raycast(ray, out hitInfo, float.MaxValue, LayerMask.value))
                if (Physics.Raycast(ray, out hitInfo, float.MaxValue))
                {
                    GameObject hitGO = hitInfo.collider.gameObject;
                    bool canSelect = CanSelect(hitGO);
                    if (canSelect)
                    {
                        if (multiselect)
                        {
                            List<Object> selection;
                            if (RuntimeSelection.objects != null)
                            {
                                selection = RuntimeSelection.objects.ToList();
                            }
                            else
                            {
                                selection = new List<Object>();
                            }

                            if (selection.Contains(hitGO))
                            {
                                selection.Remove(hitGO);
                                if (rangeSelect)
                                {
                                    selection.Insert(0, hitGO);
                                }
                            }
                            else
                            {
                                selection.Insert(0, hitGO);
                            }
                            RuntimeSelection.Select(hitGO, selection.ToArray());
                        }
                        else
                        {
                            RuntimeSelection.activeObject = hitGO;
                        }
                    }
                    else
                    {
                        if (!multiselect)
                        {
                            RuntimeSelection.activeObject = null;
                        }
                    }
                }
                else
                {
                    if (!multiselect)
                    {
                        RuntimeSelection.activeObject = null;
                    }
                }
            }

            if (RuntimeEditorApplication.IsActiveWindow(this))
            {
                if (InputController.GetKeyDown(SelectAllKey) && InputController.GetKey(ModifierKey))
                {
                    IEnumerable<GameObject> filtered = RuntimeEditorApplication.IsPlaying ?
                          ExposeToEditor.FindAll(ExposeToEditorObjectType.PlayMode) :
                          ExposeToEditor.FindAll(ExposeToEditorObjectType.EditorMode);
                    RuntimeSelection.objects = filtered.ToArray();
                }
            }
        }

        private void OnApplicationQuit()
        {
            BoxSelection.Filtering -= OnBoxSelectionFiltering;
            OnApplicationQuitOverride();
        }

        protected override void AwakeOverride()
        {
            base.AwakeOverride();

            if (SceneCamera == null)
            {
                SceneCamera = Camera.main;
            }

            if (HandlesParent == null)
            {
                HandlesParent = transform;
            }

            GameObject positionHandle = new GameObject();
            positionHandle.name = "PositionHandle";
            m_positionHandle = positionHandle.AddComponent<PositionHandle>();
            m_positionHandle.SceneCamera = SceneCamera;
            positionHandle.SetActive(false);
            positionHandle.transform.SetParent(HandlesParent);

            GameObject rotationHandle = new GameObject();
            rotationHandle.name = "RotationHandle";
            m_rotationHandle = rotationHandle.AddComponent<RotationHandle>();
            m_rotationHandle.SceneCamera = SceneCamera;
            rotationHandle.SetActive(false);
            rotationHandle.transform.SetParent(HandlesParent);

            GameObject scaleHandle = new GameObject();
            scaleHandle.name = "ScaleHandle";
            m_scaleHandle = scaleHandle.AddComponent<ScaleHandle>();
            m_scaleHandle.SceneCamera = SceneCamera;
            scaleHandle.SetActive(false);
            scaleHandle.transform.SetParent(HandlesParent);

            BoxSelection.Filtering += OnBoxSelectionFiltering;
            RuntimeSelection.SelectionChanged += OnRuntimeSelectionChanged;
            RuntimeTools.ToolChanged += OnRuntimeToolChanged;

            if (InputController.Instance == null)
            {
                gameObject.AddComponent<InputController>();
            }
        }


        protected virtual void StartOverride()
        {

        }


        protected virtual void OnEnableOverride()
        {

        }


        protected virtual void OnDisableOverride()
        {

        }

        private void OnApplicationQuitOverride()
        {

        }

        protected override void OnDestroyOverride()
        {
            base.OnDestroyOverride();
            BoxSelection.Filtering -= OnBoxSelectionFiltering;
            RuntimeTools.Current = RuntimeTool.None;
            RuntimeSelection.SelectionChanged -= OnRuntimeSelectionChanged;
            RuntimeTools.ToolChanged -= OnRuntimeToolChanged;
        }

        private void OnRuntimeToolChanged()
        {
            SetCursor();

            if (RuntimeSelection.activeTransform == null)
            {
                return;
            }

            if (m_positionHandle != null)
            {
                m_positionHandle.gameObject.SetActive(false);
                if (RuntimeTools.Current == RuntimeTool.Move)
                {
                    m_positionHandle.transform.position = RuntimeSelection.activeTransform.position;
                    m_positionHandle.Targets = GetTargets();
                    m_positionHandle.gameObject.SetActive(m_positionHandle.Targets.Length > 0);
                }
            }
            if (m_rotationHandle != null)
            {
                m_rotationHandle.gameObject.SetActive(false);
                if (RuntimeTools.Current == RuntimeTool.Rotate)
                {
                    m_rotationHandle.transform.position = RuntimeSelection.activeTransform.position;
                    m_rotationHandle.Targets = GetTargets();
                    m_rotationHandle.gameObject.SetActive(m_rotationHandle.Targets.Length > 0);
                }
            }
            if (m_scaleHandle != null)
            {
                m_scaleHandle.gameObject.SetActive(false);
                if (RuntimeTools.Current == RuntimeTool.Scale)
                {
                    m_scaleHandle.transform.position = RuntimeSelection.activeTransform.position;
                    m_scaleHandle.Targets = GetTargets();
                    m_scaleHandle.gameObject.SetActive(m_scaleHandle.Targets.Length > 0);
                }
            }

            #if UNITY_EDITOR
            switch (RuntimeTools.Current)
            {
                case RuntimeTool.None:
                    UnityEditor.Tools.current = UnityEditor.Tool.None;
                    break;
                case RuntimeTool.Move:
                    UnityEditor.Tools.current = UnityEditor.Tool.Move;
                    break;
                case RuntimeTool.Rotate:
                    UnityEditor.Tools.current = UnityEditor.Tool.Rotate;
                    break;
                case RuntimeTool.Scale:
                    UnityEditor.Tools.current = UnityEditor.Tool.Scale;
                    break;
                case RuntimeTool.View:
                    UnityEditor.Tools.current = UnityEditor.Tool.View;
                    break;
            }
            #endif
        }

      

        private void OnBoxSelectionFiltering(object sender, FilteringArgs e)
        {
            if (e.Object == null)
            {
                e.Cancel = true;
            }

            ExposeToEditor exposeToEditor = e.Object.GetComponent<ExposeToEditor>();
            if (!exposeToEditor || !exposeToEditor.CanSelect)
            {
                e.Cancel = true;
            }
        }

        private void OnRuntimeSelectionChanged(Object[] unselected)
        {
            if(unselected != null)
            {
                for(int i = 0; i < unselected.Length; ++i)
                {
                    GameObject unselectedObj = unselected[i] as GameObject;
                    if(unselectedObj != null)
                    {
                        SelectionGizmo selectionGizmo = unselectedObj.GetComponent<SelectionGizmo>();
                        if(selectionGizmo != null)
                        {
                            DestroyImmediate(selectionGizmo);
                        }

                        ExposeToEditor exposeToEditor = unselectedObj.GetComponent<ExposeToEditor>();
                        if (exposeToEditor)
                        {
                            if (exposeToEditor.Unselected != null)
                            {
                                exposeToEditor.Unselected.Invoke(exposeToEditor);
                            }
                        }
                    }
                }
            }

            GameObject[] selected = RuntimeSelection.gameObjects;
            if(selected != null)
            {
                
                for(int i = 0; i < selected.Length; ++i)
                {
                    GameObject selectedObj = selected[i];
                    ExposeToEditor exposeToEditor = selectedObj.GetComponent<ExposeToEditor>();
                    if (exposeToEditor && exposeToEditor.CanSelect && !selectedObj.IsPrefab() && !selectedObj.isStatic)
                    {
                        SelectionGizmo selectionGizmo = selectedObj.GetComponent<SelectionGizmo>();
                        if (selectionGizmo == null)
                        {
                            selectionGizmo = selectedObj.AddComponent<SelectionGizmo>();
                        }
                        selectionGizmo.SceneCamera = SceneCamera;

                        if(exposeToEditor.Selected != null)
                        {
                            exposeToEditor.Selected.Invoke(exposeToEditor);
                        }
                    }
                }
            }

            if (RuntimeSelection.activeGameObject == null || RuntimeSelection.activeGameObject.IsPrefab())
            {
                if (m_positionHandle != null)
                {
                    m_positionHandle.gameObject.SetActive(false);
                }
                if (m_rotationHandle != null)
                {
                    m_rotationHandle.gameObject.SetActive(false);
                }
                if (m_scaleHandle != null)
                {
                    m_scaleHandle.gameObject.SetActive(false);
                }
            }
            else
            {
                OnRuntimeToolChanged();
            }
        }


        protected virtual void SetCursor()
        {

        }

        protected virtual bool CanSelect(GameObject go)
        {
            return go.GetComponent<ExposeToEditor>();
        }

        protected virtual Transform[] GetTargets()
        {
            return RuntimeSelection.gameObjects.Select(g => g.transform).OrderByDescending(g => RuntimeSelection.activeTransform == g).ToArray();
        }


        public virtual void SetSceneCamera(Camera camera)
        {
            SceneCamera = camera;
            if(m_positionHandle != null)
            {
                m_positionHandle.SceneCamera = camera;
            }
            if(m_rotationHandle != null)
            {
                m_rotationHandle.SceneCamera = camera;
            }
            if(m_scaleHandle != null)
            {
                m_scaleHandle.SceneCamera = camera;
            }

            GameObject[] selected = RuntimeSelection.gameObjects;
            if (selected != null)
            {
                for (int i = 0; i < selected.Length; ++i)
                {
                    GameObject selectedObj = selected[i];
                    ExposeToEditor exposeToEditor = selectedObj.GetComponent<ExposeToEditor>();
                    if (exposeToEditor && exposeToEditor.CanSelect && !selectedObj.IsPrefab() && !selectedObj.isStatic)
                    {
                        SelectionGizmo selectionGizmo = selectedObj.GetComponent<SelectionGizmo>();
                        if(selectionGizmo != null)
                        {
                            Destroy(selectionGizmo);
                            selectionGizmo = selectedObj.AddComponent<SelectionGizmo>();
                        }
                        if(selectionGizmo != null)
                        {
                            selectionGizmo.SceneCamera = SceneCamera;
                        }
                        
                    }
                }
            }
        }
    }



}

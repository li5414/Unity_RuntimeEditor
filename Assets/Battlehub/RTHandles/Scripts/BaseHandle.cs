using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

using Battlehub.RTCommon;
using Battlehub.Utils;

namespace Battlehub.RTHandles
{
    /// <summary>
    /// Base class for all handles (Position, Rotation and Scale)
    /// </summary>
    public abstract class BaseHandle : MonoBehaviour, IGL
    {
        /// <summary>
        /// current size of grid 
        /// </summary>
        protected float EffectiveGridUnitSize
        {
            get;
            private set;
        }


        /// <summary>
        /// Use RuntimeUndo subsystem
        /// </summary>
        public bool EnableUndo = true;

        /// <summary>
        /// HighlightOnHover
        /// </summary>
        public bool HightlightOnHover = true;

        /// <summary>
        /// Key which activates Unit Snapping
        /// </summary>
        public KeyCode UnitSnapKey = KeyCode.LeftControl;

        /// <summary>
        /// Raycasting camera
        /// </summary>
        public Camera SceneCamera;

        /// <summary>
        /// Screen space selection margin in pixesl
        /// </summary>
        public float SelectionMargin = 10;

        protected LockObject LockObject
        {
            get { return RuntimeTools.LockAxes; }
            set { RuntimeTools.LockAxes = value; }
        }
        /// <summary>
        /// Target objects which will be affected by handle (for example if m_targets array containes O1 and O2 objects, and O1 is parent of O2 then m_activeTargets array will contain only O1 object)
        /// </summary>
        private Transform[] m_activeTargets;
        protected Transform[] ActiveTargets
        {
            get { return m_activeTargets; }
        }


        private Transform[] m_activeRealTargets;
        private Transform[] m_realTargets;
        protected Transform[] RealTargets
        {
            get
            {
                if(m_realTargets == null)
                {
                    return Targets;
                }
                return m_realTargets;
            }
        }

        private Transform[] m_commonCenter;
        private Transform[] m_commonCenterTarget;



        private void GetActiveRealTargets()
        {
            if(m_realTargets == null)
            {
                m_activeRealTargets = null;
                return;
            }

            m_realTargets = m_realTargets.Where(t => t != null && t.hideFlags == HideFlags.None).ToArray();
            HashSet<Transform> targetsHS = new HashSet<Transform>();
            for (int i = 0; i < m_realTargets.Length; ++i)
            {
                if (m_realTargets[i] != null && !targetsHS.Contains(m_realTargets[i]))
                {
                    targetsHS.Add(m_realTargets[i]);
                }
            }
            m_realTargets = targetsHS.ToArray();
            if (m_realTargets.Length == 0)
            {
                m_activeRealTargets = new Transform[0];
                return;
            }
            else if (m_realTargets.Length == 1)
            {
                m_activeRealTargets = new[] { m_realTargets[0] };
            }

            for (int i = 0; i < m_realTargets.Length; ++i)
            {
                Transform target = m_realTargets[i];
                Transform p = target.parent;
                while (p != null)
                {
                    if (targetsHS.Contains(p))
                    {
                        targetsHS.Remove(target);
                        break;
                    }

                    p = p.parent;
                }
            }

            m_activeRealTargets = targetsHS.ToArray();
        }
        /// <summary>
        /// All Target objects
        /// </summary>
        [SerializeField]
        private Transform[] m_targets;
        public Transform[] Targets
        {
            get
            {
                return Targets_Internal;
            }
            set
            {
                DestroyCommonCenter();
                m_realTargets = value;
                GetActiveRealTargets();
                Targets_Internal = value;
                if (Targets_Internal == null || Targets_Internal.Length == 0)
                {
                    return;
                }

                if (RuntimeTools.PivotMode == RuntimePivotMode.Center && ActiveTargets.Length > 1)
                {
                    Vector3 centerPosition = Targets_Internal[0].position;
                    for (int i = 1; i < Targets_Internal.Length; ++i)
                    {
                        centerPosition += Targets_Internal[i].position;
                    }

                    centerPosition = centerPosition / Targets_Internal.Length;
                    m_commonCenter = new Transform[1];
                    m_commonCenter[0] = new GameObject { name = "CommonCenter" }.transform;
                    m_commonCenter[0].SetParent(transform.parent, true);
                    m_commonCenter[0].position = centerPosition;
                    m_commonCenterTarget = new Transform[m_realTargets.Length];
                    for (int i = 0; i < m_commonCenterTarget.Length; ++i)
                    {
                        GameObject target = new GameObject { name = "ActiveTarget " + m_realTargets[i].name };
                        target.transform.SetParent(m_commonCenter[0]);

                        target.transform.position = m_realTargets[i].position;
                        target.transform.rotation = m_realTargets[i].rotation;
                        target.transform.localScale = m_realTargets[i].localScale;

                        m_commonCenterTarget[i] = target.transform;
                    }
                    LockObject lockObject = LockObject;
                    Targets_Internal = m_commonCenter;
                    LockObject = lockObject;
                }
            }
        }

        private Transform[] Targets_Internal
        {
            get { return m_targets; }
            set
            {
             
                m_targets = value;
                if(m_targets == null)
                {
                    LockObject = LockAxes.Eval(null);
                    m_activeTargets = null;
                    return;
                }

                m_targets = m_targets.Where(t => t != null && t.hideFlags == HideFlags.None).ToArray();
                HashSet<Transform> targetsHS = new HashSet<Transform>();
                for (int i = 0; i < m_targets.Length; ++i)
                {
                    if (m_targets[i] != null && !targetsHS.Contains(m_targets[i]))
                    {
                        targetsHS.Add(m_targets[i]);
                    }
                }
                m_targets = targetsHS.ToArray();
                if (m_targets.Length == 0)
                {
                    LockObject = LockAxes.Eval(new LockAxes[0]);
                    m_activeTargets = new Transform[0];
                    return;
                }
                else if(m_targets.Length == 1)
                {
                    m_activeTargets = new [] { m_targets[0] };
                }

                for(int i = 0; i < m_targets.Length; ++i)
                {
                    Transform target = m_targets[i];
                    Transform p = target.parent;
                    while(p != null)
                    {
                        if(targetsHS.Contains(p))
                        {
                            targetsHS.Remove(target);
                            break;
                        }

                        p = p.parent;
                    }
                }

                m_activeTargets = targetsHS.ToArray();
                LockObject = LockAxes.Eval(m_activeTargets.Where(t => t.GetComponent<LockAxes>() != null).Select(t => t.GetComponent<LockAxes>()).ToArray());
                if(m_activeTargets.Any(target => target.gameObject.isStatic))
                {
                    LockObject = new LockObject();
                    LockObject.PositionX = LockObject.PositionY = LockObject.PositionZ = true;
                    LockObject.RotationX = LockObject.RotationY = LockObject.RotationZ = true;
                    LockObject.ScaleX = LockObject.ScaleY = LockObject.ScaleZ = true;
                    LockObject.RotationScreen = true;
                }

                if(m_activeTargets != null && m_activeTargets.Length > 0)
                {
                    transform.position = m_activeTargets[0].position;
                }
            }
        }

        public Transform Target
        {
            get
            {
                if(Targets == null)
                {
                    return null;
                }
                return Targets[0];
            }
        }

        /// <summary>
        /// Selected axis
        /// </summary>
        private RuntimeHandleAxis m_selectedAxis;

        /// <summary>
        /// Whether drag operation in progress
        /// </summary>
        private bool m_isDragging;
        
        /// <summary>
        /// Drag plane
        /// </summary>
        private Plane m_dragPlane;

        public bool IsDragging
        {
            get { return m_isDragging; }
        }

        /// <summary>
        /// Tool type
        /// </summary>
        protected abstract RuntimeTool Tool
        {
            get;
        }

        /// <summary>
        /// Quaternion Rotation based on selected coordinate system (local or global)
        /// </summary>
        protected Quaternion Rotation
        {
            get
            {
                if(Targets == null || Targets.Length <= 0 || Target == null)
                {
                    return Quaternion.identity;
                }

                return RuntimeTools.PivotRotation == RuntimePivotRotation.Local ? Target.rotation : Quaternion.identity;
            }
        }

        protected RuntimeHandleAxis SelectedAxis
        {
            get { return m_selectedAxis; }
            set { m_selectedAxis = value; }
        }

        protected Plane DragPlane
        {
            get { return m_dragPlane; }
            set { m_dragPlane = value; }
        }

        protected abstract float CurrentGridUnitSize
        {
            get;
        }

        /// Lifecycle methods
        private void Awake()
        {
            if(m_targets != null && m_targets.Length > 0)
            {
                Targets = m_targets;
            }

            RuntimeTools.PivotModeChanged += OnPivotModeChanged;

            AwakeOverride();
        }

        private void Start()
        {
            if (SceneCamera == null)
            {
                SceneCamera = Camera.main;
            }

            if (EnableUndo)
            {
                if (!RuntimeUndoComponent.IsInitialized)
                {
                    GameObject runtimeUndo = new GameObject();
                    runtimeUndo.name = "RuntimeUndo";
                    runtimeUndo.AddComponent<RuntimeUndoComponent>();
                }
            }

            if (GLRenderer.Instance == null)
            {
                GameObject glRenderer = new GameObject();
                glRenderer.name = "GLRenderer";
                glRenderer.AddComponent<GLRenderer>();
            }

            if (SceneCamera != null)
            {
                if (!SceneCamera.GetComponent<GLCamera>())
                {
                    SceneCamera.gameObject.AddComponent<GLCamera>();
                }
            }

            if (Targets == null || Targets.Length == 0)
            {
                Targets = new[] { transform };
            }

            if (GLRenderer.Instance != null)
            {
                GLRenderer.Instance.Add(this);
            }

            if (Targets[0].position != transform.position)
            {
                transform.position = Targets[0].position;
            }

            StartOverride();
        }

        private void OnEnable()
        {
            if (GLRenderer.Instance != null)
            {
                GLRenderer.Instance.Add(this);
            }

            OnEnableOverride();

            RuntimeUndo.UndoCompleted += OnUndoCompleted;
            RuntimeUndo.RedoCompleted += OnRedoCompleted;
        }

        private void OnDisable()
        {
            if (GLRenderer.Instance != null)
            {
                GLRenderer.Instance.Remove(this);
            }

            DestroyCommonCenter();

            OnDisableOverride();

            RuntimeUndo.UndoCompleted -= OnUndoCompleted;
            RuntimeUndo.RedoCompleted -= OnRedoCompleted;
        }

        private void OnDestroy()
        {
            if (GLRenderer.Instance != null)
            {
                GLRenderer.Instance.Remove(this);
            }

            if (RuntimeTools.ActiveTool == this)
            {
                RuntimeTools.ActiveTool = null;
            }

            RuntimeTools.PivotModeChanged -= OnPivotModeChanged;

            DestroyCommonCenter();

            OnDestroyOverride();
        }

        private void DestroyCommonCenter()
        {
            if (m_commonCenter != null)
            {
                for (int i = 0; i < m_commonCenter.Length; ++i)
                {
                    Destroy(m_commonCenter[i].gameObject);
                }
            }

            if (m_commonCenterTarget != null)
            {
                for (int i = 0; i < m_commonCenterTarget.Length; ++i)
                {
                    Destroy(m_commonCenterTarget[i].gameObject);
                }
            }

            m_commonCenter = null;
            m_commonCenterTarget = null;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (RuntimeTools.Current != Tool && RuntimeTools.Current != RuntimeTool.None || RuntimeTools.IsViewing)
                {
                    return;
                }

                if (RuntimeTools.IsPointerOverGameObject())
                {
                    return;
                }

                if (SceneCamera == null)
                {
                    Debug.LogError("Camera is null");
                    return;
                }

                if (RuntimeTools.ActiveTool != null)
                {
                    return;
                }

                if (RuntimeEditorApplication.ActiveSceneCamera != null && !RuntimeEditorApplication.IsPointerOverWindow(RuntimeWindowType.SceneView))
                {
                    return;
                }

                m_isDragging = OnBeginDrag();
                if (m_isDragging)
                {
                    RuntimeTools.ActiveTool = this;
                    RecordTransform();
                }
                else
                {
                    RuntimeTools.ActiveTool = null;
                }

            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (m_isDragging)
                {
                    OnDrop();
                    RecordTransform();
                    m_isDragging = false;
                    RuntimeTools.ActiveTool = null;
                }
            }
            else
            {
                if (m_isDragging)
                {
                    if (InputController.GetKey(UnitSnapKey) || RuntimeTools.UnitSnapping)
                    {
                        EffectiveGridUnitSize = CurrentGridUnitSize;
                    }
                    else
                    {
                        EffectiveGridUnitSize = 0;
                    }

                    OnDrag();


                }
            }

            UpdateOverride();

            if (m_isDragging)
            {
                if (RuntimeTools.PivotMode == RuntimePivotMode.Center && m_commonCenterTarget != null && m_realTargets != null && m_realTargets.Length > 1)
                {
                    for (int i = 0; i < m_commonCenterTarget.Length; ++i)
                    {
                        Transform commonCenterTarget = m_commonCenterTarget[i];
                        Transform target = m_realTargets[i];

                        target.transform.position = commonCenterTarget.position;
                        target.transform.rotation = commonCenterTarget.rotation;
                        target.transform.localScale = commonCenterTarget.localScale;
                    }
                }
            }
        }

        /// Lifecycle method overrides
        protected virtual void AwakeOverride()
        {

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

        protected virtual void OnDestroyOverride()
        {

        }


        protected virtual void UpdateOverride()
        {
            if (Targets != null && Targets.Length > 0 && Targets[0] != null && Targets[0].position != transform.position)
            {
                if (IsDragging)
                {
                    Vector3 offset = transform.position - Targets[0].position;
                    for (int i = 0; i < ActiveTargets.Length; ++i)
                    {
                        if (ActiveTargets[i] != null)
                        {
                            ActiveTargets[i].position += offset;
                        }
                    }

                }
                else
                {

                    transform.position = Targets[0].position;
                    transform.rotation = Targets[0].rotation;
                }
            }
        }

        /// Drag And Drop virtual methods
        protected virtual bool OnBeginDrag()
        {
            return false;
        }

        protected virtual void OnDrag()
        {

        }

        protected virtual void OnDrop()
        {

        }


        protected virtual void OnPivotModeChanged()
        {
            if (RealTargets != null)
            {
                Targets = RealTargets;
            }

            if (RuntimeTools.PivotMode != RuntimePivotMode.Center)
            {
                m_realTargets = null;   
            }
            
            if(Target != null)
            {
                transform.position = Target.position;
            }            
        }

        protected virtual void RecordTransform()
        {
            if (EnableUndo)
            {
                RuntimeUndo.BeginRecord();
                for (int i = 0; i < m_activeRealTargets.Length; ++i)
                {
                    RuntimeUndo.RecordTransform(m_activeRealTargets[i]);
                }
                RuntimeUndo.EndRecord();
            }
        }

        private void OnRedoCompleted()
        {
            if (RuntimeTools.PivotMode == RuntimePivotMode.Center)
            {
                if(m_realTargets != null)
                {
                    Targets = m_realTargets;
                }
             
            }
        }

        private void OnUndoCompleted()
        {
            if (RuntimeTools.PivotMode == RuntimePivotMode.Center)
            {
                if (m_realTargets != null)
                {
                    Targets = m_realTargets;
                }
              
            }
        }

        /// Hit testing methods      
        protected virtual bool HitCenter()
        {
            Vector2 screenCenter = SceneCamera.WorldToScreenPoint(transform.position);
            Vector2 mouse = Input.mousePosition;

            return (mouse - screenCenter).magnitude <= SelectionMargin;
        }

        protected virtual bool HitAxis(Vector3 axis, Matrix4x4 matrix, out float distanceToAxis)
        {
            axis = matrix.MultiplyVector(axis);
            Vector2 screenVectorBegin = SceneCamera.WorldToScreenPoint(transform.position);
            Vector2 screenVectorEnd = SceneCamera.WorldToScreenPoint(axis + transform.position);

            Vector3 screenVector = screenVectorEnd - screenVectorBegin;
            float screenVectorMag = screenVector.magnitude;
            screenVector.Normalize();
            if (screenVector != Vector3.zero)
            {
                return HitScreenAxis(out distanceToAxis, screenVectorBegin, screenVector, screenVectorMag);
            }
            else
            {
                Vector2 mousePosition = Input.mousePosition;

                distanceToAxis = (screenVectorBegin - mousePosition).magnitude;
                bool result = distanceToAxis <= SelectionMargin;
                if (!result)
                {
                    distanceToAxis = float.PositiveInfinity;
                }
                else
                {
                    distanceToAxis = 0.0f;
                }
                return result;
            }
        }

        protected virtual bool HitScreenAxis(out float distanceToAxis, Vector2 screenVectorBegin, Vector3 screenVector, float screenVectorMag)
        {
            Vector2 perp = PerpendicularClockwise(screenVector).normalized;
            Vector2 mousePosition = Input.mousePosition;
            Vector2 relMousePositon = mousePosition - screenVectorBegin;

            distanceToAxis = Mathf.Abs(Vector2.Dot(perp, relMousePositon));
            Vector2 hitPoint = (relMousePositon - perp * distanceToAxis);
            float vectorSpaceCoord = Vector2.Dot(screenVector, hitPoint);

            bool result = vectorSpaceCoord <= screenVectorMag + SelectionMargin && vectorSpaceCoord >= -SelectionMargin && distanceToAxis <= SelectionMargin;
            if (!result)
            {
                distanceToAxis = float.PositiveInfinity;
            }
            else
            {
                if (screenVectorMag < SelectionMargin)
                {
                    distanceToAxis = 0.0f;
                }
            }
            return result;
        }

        protected virtual Plane GetDragPlane(Matrix4x4 matrix, Vector3 axis)
        {
            Plane plane = new Plane(matrix.MultiplyVector(axis).normalized, matrix.MultiplyPoint(Vector3.zero));
            return plane;
        }

        protected virtual Plane GetDragPlane()
        {
            Vector3 toCam = SceneCamera.cameraToWorldMatrix.MultiplyVector(Vector3.forward); //Camera.transform.position - transform.position;
            Plane dragPlane = new Plane(toCam.normalized, transform.position);
            return dragPlane;
        }

        protected virtual bool GetPointOnDragPlane(Vector3 screenPos, out Vector3 point)
        {
            return GetPointOnDragPlane(m_dragPlane, screenPos, out point);
        }

        protected virtual bool GetPointOnDragPlane(Plane dragPlane, Vector3 screenPos, out Vector3 point)
        {
            Ray ray = SceneCamera.ScreenPointToRay(screenPos);
            float distance;
            if (dragPlane.Raycast(ray, out distance))
            {
                point = ray.GetPoint(distance);
                return true;
            }

            point = Vector3.zero;
            return false;
        }

        private static Vector2 PerpendicularClockwise(Vector2 vector2)
        {
            return new Vector2(-vector2.y, vector2.x);
        }

        void IGL.Draw(int cullingMask)
        {
            RTLayer layer = RTLayer.SceneView;
            if((cullingMask & (int)layer) == 0)
            {
                return;
            }

            DrawOverride();
        }

        protected virtual void DrawOverride()
        {

        }
    }
}

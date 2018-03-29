using UnityEngine;
using UnityEngine.EventSystems;
using Battlehub.Utils;
using Battlehub.RTCommon;
namespace Battlehub.RTHandles
{
    public class RuntimeSceneView : RuntimeSelectionComponent
    {
        protected override bool IPointerOverEditorArea
        {
            get { return RuntimeEditorApplication.IsPointerOverWindow(this) || !RuntimeEditorApplication.IsOpened; }
        }

        //[SerializeField]
        //private LayerMask m_raycastLayerMask = 1 << 31;
        //private int m_raycastLayer = 31;

        //protected override LayerMask LayerMask
        //{
        //    get { return m_raycastLayerMask; }
        //}

        //public int RaycastLayer
        //{
        //    get { return m_raycastLayer; }
        //    set
        //    {
        //        m_raycastLayer = value;
        //        m_raycastLayerMask = 1 << value;
        //    }
        //}

        public KeyCode FocusKey = KeyCode.F;
        public KeyCode SnapToGridKey = KeyCode.S;
        public KeyCode RotateKey = KeyCode.LeftAlt;
        public KeyCode RotateKey2 = KeyCode.RightAlt;
        public KeyCode RotateKey3 = KeyCode.AltGr;

        public Texture2D ViewTexture;
        public Texture2D MoveTexture;
        public Transform Pivot; //pivot primarily used by MouseOrbit
        public Transform SecondaryPivot; //pivot used to display grid and instantiate prefabs

        private bool m_pan;
        private Plane m_dragPlane;
        private bool m_rotate;
        private bool m_handleInput;
        private bool m_lockInput;
        private Vector3 m_lastMousePosition;

        private MouseOrbit m_mouseOrbit;
        public float ZoomSensitivity = 8f;
        public float PanSensitivity = 100f;

        private IAnimationInfo m_focusAnimation;
        private Transform m_autoFocusTransform;
        public float GridSize = 1;

        protected override void AwakeOverride()
        {
            base.AwakeOverride();
            if (Run.Instance == null)
            {
                GameObject runGO = new GameObject();
                runGO.name = "Run";
                runGO.AddComponent<Run>();
            }        

            if(Pivot == null)
            {
                GameObject pivot = new GameObject();
                pivot.transform.SetParent(transform, false);
                pivot.name = "Pivot";

                Pivot = pivot.transform;
            }

            if(SecondaryPivot == null)
            {
                GameObject secondaryPivot = new GameObject();
                secondaryPivot.transform.SetParent(transform, false);
                secondaryPivot.name = "SecondaryPivot";

                SecondaryPivot = secondaryPivot.transform;
            }
        }

        protected override void OnEnableOverride()
        {
            if (SceneCamera == null)
            {
                return;
            }
            base.OnEnableOverride();
            if(SceneCamera != null)
            {
                SetSceneCamera(SceneCamera);
            }
        }


        protected override void UpdateOverride()
        {
            base.UpdateOverride();

            if (RuntimeTools.ActiveTool != null)
            {
                return;
            }
           
            HandleInput();

            if (RuntimeEditorApplication.IsPointerOverWindow(this))
            {
                SetCursor();
            }
            else
            {
                CursorHelper.ResetCursor(this);
            }
        }

        protected override void SetCursor()
        {
            if (!IPointerOverEditorArea)
            {
                CursorHelper.ResetCursor(this);
                return;
            }

            if (m_pan)
            {
                if (m_rotate && RuntimeTools.Current == RuntimeTool.View)
                {
                    CursorHelper.SetCursor(this, ViewTexture, ViewTexture != null ? new Vector2(ViewTexture.width / 2, ViewTexture.height / 2) : Vector2.zero, CursorMode.Auto);
                }
                else
                {
                    CursorHelper.SetCursor(this, MoveTexture, MoveTexture != null ? new Vector2(MoveTexture.width / 2, MoveTexture.height / 2) : Vector2.zero, CursorMode.Auto);
                }
            }
            else if (m_rotate)
            {
                CursorHelper.SetCursor(this, ViewTexture, ViewTexture != null ? new Vector2(ViewTexture.width / 2, ViewTexture.height / 2) : Vector2.zero, CursorMode.Auto);
            }
            else
            {
                if (RuntimeTools.Current == RuntimeTool.View)
                {
                    CursorHelper.SetCursor(this, MoveTexture, MoveTexture != null ? new Vector2(MoveTexture.width / 2, MoveTexture.height / 2) : Vector2.zero, CursorMode.Auto);
                }
                else
                {
                    bool rotate = InputController.GetKey(RotateKey) || InputController.GetKey(RotateKey2) || InputController.GetKey(RotateKey3);
                    if(!rotate)
                    {
                        CursorHelper.ResetCursor(this);
                    }
                }
            }
        }


        public void LockInput()
        {
            m_lockInput = true;
        }

        public void UnlockInput()
        {
            m_lockInput = false;
            if (m_mouseOrbit != null)
            {
                Pivot.position = SceneCamera.transform.position + SceneCamera.transform.forward * m_mouseOrbit.Distance;
                SecondaryPivot.position = Pivot.position;
                m_mouseOrbit.Target = Pivot;
                m_mouseOrbit.SyncAngles();
            }
        }

        public void OnProjectionChanged()
        {
            float fov = SceneCamera.fieldOfView * Mathf.Deg2Rad;
            float distance = (SceneCamera.transform.position - Pivot.position).magnitude;
            float objSize = distance * Mathf.Sin(fov / 2);
            SceneCamera.orthographicSize = objSize;
        }

        public void SnapToGrid()
        {
            GameObject[] selection = RuntimeSelection.gameObjects;
            if (selection == null || selection.Length == 0)
            {
                return;
            }

            Transform activeTransform = selection[0].transform;
            Vector3 position = activeTransform.position;
            if (GridSize < 0.01)
            {
                GridSize = 0.01f;
            }
            position.x = Mathf.Round(position.x / GridSize) * GridSize;
            position.y = Mathf.Round(position.y / GridSize) * GridSize;
            position.z = Mathf.Round(position.z / GridSize) * GridSize;
            Vector3 offset = position - activeTransform.position;

            for (int i = 0; i < selection.Length; ++i)
            {
                selection[i].transform.position += offset;
            }
        }

        public void Focus()
        {
            if (RuntimeSelection.activeTransform == null)
            {
                return;
            }

            m_autoFocusTransform = RuntimeSelection.activeTransform;
            if(RuntimeSelection.activeTransform.gameObject.hideFlags != HideFlags.None)
            {
                return;
            }

            Bounds bounds = CalculateBounds(RuntimeSelection.activeTransform);
            float fov = SceneCamera.fieldOfView * Mathf.Deg2Rad;
            float objSize = Mathf.Max(bounds.extents.y, bounds.extents.x, bounds.extents.z) * 2.0f;
            float distance = Mathf.Abs(objSize / Mathf.Sin(fov / 2.0f));

            Pivot.position = bounds.center;
            SecondaryPivot.position = RuntimeSelection.activeTransform.position;
            const float duration = 0.1f;

            m_focusAnimation = new Vector3AnimationInfo(SceneCamera.transform.position, Pivot.position - distance * SceneCamera.transform.forward, duration, Vector3AnimationInfo.EaseOutCubic,
                (target, value, t, completed) =>
                {
                    if (SceneCamera)
                    {
                        SceneCamera.transform.position = value;
                    }
                });
            Run.Instance.Animation(m_focusAnimation);
            Run.Instance.Animation(new FloatAnimationInfo(m_mouseOrbit.Distance, distance, duration, Vector3AnimationInfo.EaseOutCubic,
                (target, value, t, completed) =>
                {
                    if (m_mouseOrbit)
                    {
                        m_mouseOrbit.Distance = value;
                    }
                }));

            Run.Instance.Animation(new FloatAnimationInfo(SceneCamera.orthographicSize, objSize, duration, Vector3AnimationInfo.EaseOutCubic,
                (target, value, t, completed) =>
                {
                    if (SceneCamera)
                    {
                        SceneCamera.orthographicSize = value;
                    }
                }));
        }

        private Bounds CalculateBounds(Transform t)
        {
            Renderer renderer = t.GetComponentInChildren<Renderer>();
            if (renderer)
            {
                Bounds bounds = renderer.bounds;
                if (bounds.size == Vector3.zero && bounds.center != renderer.transform.position)
                {
                    bounds = TransformBounds(renderer.transform.localToWorldMatrix, bounds);
                }
                CalculateBounds(t, ref bounds);
                if (bounds.extents == Vector3.zero)
                {
                    bounds.extents = new Vector3(0.5f, 0.5f, 0.5f);
                }
                return bounds;
            }

            return new Bounds(t.position, new Vector3(0.5f, 0.5f, 0.5f));
        }

        private void CalculateBounds(Transform t, ref Bounds totalBounds)
        {
            foreach (Transform child in t)
            {
                Renderer renderer = child.GetComponent<Renderer>();
                if (renderer)
                {
                    Bounds bounds = renderer.bounds;
                    if (bounds.size == Vector3.zero && bounds.center != renderer.transform.position)
                    {
                        bounds = TransformBounds(renderer.transform.localToWorldMatrix, bounds);
                    }
                    totalBounds.Encapsulate(bounds.min);
                    totalBounds.Encapsulate(bounds.max);
                }

                CalculateBounds(child, ref totalBounds);
            }
        }
        public static Bounds TransformBounds(Matrix4x4 matrix, Bounds bounds)
        {
            var center = matrix.MultiplyPoint(bounds.center);

            // transform the local extents' axes
            var extents = bounds.extents;
            var axisX = matrix.MultiplyVector(new Vector3(extents.x, 0, 0));
            var axisY = matrix.MultiplyVector(new Vector3(0, extents.y, 0));
            var axisZ = matrix.MultiplyVector(new Vector3(0, 0, extents.z));

            // sum their absolute value to get the world extents
            extents.x = Mathf.Abs(axisX.x) + Mathf.Abs(axisY.x) + Mathf.Abs(axisZ.x);
            extents.y = Mathf.Abs(axisX.y) + Mathf.Abs(axisY.y) + Mathf.Abs(axisZ.y);
            extents.z = Mathf.Abs(axisX.z) + Mathf.Abs(axisY.z) + Mathf.Abs(axisZ.z);

            return new Bounds { center = center, extents = extents };
        }

        private void Pan()
        {
            Vector3 pointOnDragPlane;
            Vector3 prevPointOnDragPlane;
            if (GetPointOnDragPlane(Input.mousePosition, out pointOnDragPlane) &&
                GetPointOnDragPlane(m_lastMousePosition, out prevPointOnDragPlane))
            {
                Vector3 delta = (pointOnDragPlane - prevPointOnDragPlane);
                m_lastMousePosition = Input.mousePosition;
                SceneCamera.transform.position -= delta;
                Pivot.position -= delta;
                SecondaryPivot.position -= delta;
            }
        }

        private bool GetPointOnDragPlane(Vector3 mouse, out Vector3 point)
        {
            Ray ray = SceneCamera.ScreenPointToRay(mouse);
            float distance;
            if (m_dragPlane.Raycast(ray, out distance))
            {
                point = ray.GetPoint(distance);
                return true;
            }

            point = Vector3.zero;
            return false;
        }

        protected override bool CanSelect(GameObject go)
        {
            ExposeToEditor exposeToEditor = go.GetComponent<ExposeToEditor>();
            return exposeToEditor != null && exposeToEditor.CanSelect;
        }


        private void HandleInput()
        {
            if (RuntimeTools.AutoFocus)
            {
                do
                {
                    if (RuntimeTools.ActiveTool != null)
                    {
                        break;
                    }

                    if (m_autoFocusTransform == null)
                    {
                        break;
                    }

                    if (m_autoFocusTransform.position == SecondaryPivot.position)
                    {
                        break;
                    }

                    if (m_focusAnimation != null && m_focusAnimation.InProgress)
                    {
                        break;
                    }

                    Vector3 offset = (m_autoFocusTransform.position - SecondaryPivot.position);
                    SceneCamera.transform.position += offset;
                    Pivot.transform.position += offset;
                    SecondaryPivot.transform.position += offset;
                }
                while (false);
            }

            if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2))
            {
                m_handleInput = false;
                m_mouseOrbit.enabled = false;
                m_rotate = false;
                SetCursor();
                return;
            }

            bool isGameViewActive = RuntimeEditorApplication.IsActiveWindow(RuntimeWindowType.GameView);

            if(!isGameViewActive)
            {
                float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
                if (mouseWheel != 0)
                {
                    if (!RuntimeTools.IsPointerOverGameObject())
                    {
                        m_mouseOrbit.Zoom();
                    }
                }

            }

            if (m_lockInput)
            {
                return;
            }

          
            if(!isGameViewActive)
            {
                if (InputController.GetKeyDown(SnapToGridKey) && InputController.GetKey(ModifierKey))
                {
                    SnapToGrid();
                }

                if (InputController.GetKeyDown(FocusKey))
                {
                    Focus();
                }

                bool rotate = InputController.GetKey(RotateKey) || InputController.GetKey(RotateKey2) || InputController.GetKey(RotateKey3);
                bool pan = Input.GetMouseButton(2) || Input.GetMouseButton(1) || Input.GetMouseButton(0) && RuntimeTools.Current == RuntimeTool.View;
                if (pan != m_pan)
                {
                    m_pan = pan;
                    if (m_pan)
                    {
                        if (RuntimeTools.Current != RuntimeTool.View)
                        {
                            m_rotate = false;
                        }
                        m_dragPlane = new Plane(-SceneCamera.transform.forward, Pivot.position);
                    }
                    SetCursor();
                }
                else
                {
                    if (rotate != m_rotate)
                    {
                        m_rotate = rotate;
                        SetCursor();
                    }
                }
            }
            
            RuntimeTools.IsViewing = m_rotate || m_pan;
            bool isLocked = RuntimeTools.IsViewing || isGameViewActive;
            if (!IPointerOverEditorArea)
            {
                return;
            }


            bool isMouse0ButtonDown = Input.GetMouseButtonDown(0);
            bool isMouse1ButtonDown = Input.GetMouseButtonDown(1);
            bool isMouse2ButtonDown = Input.GetMouseButtonDown(2);

            if (isMouse0ButtonDown || isMouse1ButtonDown || isMouse2ButtonDown)
            {
                m_handleInput = !PositionHandle.IsDragging;
                m_lastMousePosition = Input.mousePosition;
                if (m_rotate)
                {
                    m_mouseOrbit.enabled = true;
                }
            }

        

            if (m_handleInput)
            {
                if (isLocked)
                {
                    if (m_pan && (!m_rotate || RuntimeTools.Current != RuntimeTool.View))
                    {
                        Pan();
                    }
                }
            }
        }

        public override void SetSceneCamera(Camera camera)
        {
            base.SetSceneCamera(camera);
            SceneCamera.fieldOfView = 60;
            OnProjectionChanged();
            m_mouseOrbit = SceneCamera.gameObject.GetComponent<MouseOrbit>();
            if (m_mouseOrbit == null)
            {
                m_mouseOrbit = SceneCamera.gameObject.AddComponent<MouseOrbit>();
            }
            UnlockInput();
            m_mouseOrbit.enabled = false;
        }
    }
}

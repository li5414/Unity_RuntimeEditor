using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using Battlehub.Utils;
using Battlehub.RTCommon;
using Battlehub.RTSaveLoad;
namespace Battlehub.RTHandles
{
    public enum BoxSelectionMethod
    {
        LooseFitting,
        BoundsCenter,
        TansformCenter
    }

    public class FilteringArgs : EventArgs
    {
        private bool m_cancel;

        public bool Cancel
        {
            get { return m_cancel; }
            set
            {
                if (value) //can't reset cancel flag
                {
                    m_cancel = true;
                }
            }
        }

        public GameObject Object
        {
            get;
            set;
        }

        public void Reset()
        {
            m_cancel = false;
        }
    }

    
    /// <summary>
    /// Box Selection
    /// </summary>
    public class BoxSelection : MonoBehaviour
    {
        public Sprite Graphics;
        protected Image m_image;
        protected RectTransform m_rectTransform;
        protected Canvas m_canvas;
        protected bool m_isDragging;
        protected Vector3 m_startMousePosition;
        protected Vector2 m_startPt;
        protected Vector2 m_endPt;

        public bool UseCameraSpace;
        public Camera SceneCamera;
        public BoxSelectionMethod Method;
        public int MouseButton = 0;
        public KeyCode KeyCode = KeyCode.None;
        protected bool m_active;

        public static event EventHandler<FilteringArgs> Filtering;

        public bool IsDragging
        {
            get { return m_isDragging; }
        }

        private static BoxSelection m_current;
        public static BoxSelection Current
        {
            get { return m_current; }
        }
        
        private void Awake()
        {
            if(m_current != null)
            {
                Debug.LogWarning("Another instance of BoxSelection exists");
            }

            if(!GetComponent<PersistentIgnore>())
            {
                gameObject.AddComponent<PersistentIgnore>();
            }

            m_current = this;

            m_image = gameObject.AddComponent<Image>();
            m_image.type = Image.Type.Sliced;
            if (Graphics == null)
            {
                Graphics = Resources.Load<Sprite>("BoxSelection");
            }
            m_image.sprite = Graphics;
            m_image.raycastTarget = false;
            m_rectTransform = GetComponent<RectTransform>();
            m_rectTransform.sizeDelta = new Vector2(0, 0);
            m_rectTransform.pivot = new Vector2(0, 0);
            m_rectTransform.anchoredPosition = new Vector3(0, 0);

            if (SceneCamera == null)
            {
                SceneCamera = Camera.main;
            }
        }

        private void OnEnable()
        {
            m_canvas = GetComponentInParent<Canvas>();
            if(SceneCamera == null)
            {
                return;
            }
            if(m_canvas == null)
            {
                GameObject go = new GameObject();
                go.name = "BoxSelectionCanvas";
                m_canvas = go.AddComponent<Canvas>();
            }

            CanvasScaler scaler = m_canvas.GetComponent<CanvasScaler>();
            if (scaler == null)
            {
                scaler = m_canvas.gameObject.AddComponent<CanvasScaler>();
            }

            
            if (UseCameraSpace)
            {
                m_canvas.worldCamera = SceneCamera;
                m_canvas.renderMode = RenderMode.ScreenSpaceCamera;
                m_canvas.planeDistance = SceneCamera.nearClipPlane + 0.05f;
            }
            else
            {
                m_canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            }

            scaler.referencePixelsPerUnit = 1;
            m_canvas.sortingOrder = -1;
            transform.SetParent(m_canvas.gameObject.transform);
        }

        public void SetSceneCamera(Camera camera)
        {
            SceneCamera = camera;

            if (m_canvas != null)
            {
                if (UseCameraSpace)
                {
                    m_canvas.worldCamera = camera;
                }
            }
        }

        private void OnDestroy()
        {
            if (RuntimeTools.ActiveTool == this)
            {
                RuntimeTools.ActiveTool = null;
            }

            if(m_current == this)
            {
                m_current = null;
            }
        }

        private void LateUpdate()
        {
            if (RuntimeTools.ActiveTool  != null && RuntimeTools.ActiveTool != this)
            {
                return;
            }

            if (RuntimeTools.IsViewing)
            {
                return;
            }

            if (KeyCode == KeyCode.None || InputController.GetKeyDown(KeyCode))
            {
               m_active = true; 
            }

            if (m_active)
            {
                if (Input.GetMouseButtonDown(MouseButton))
                {
                    m_startMousePosition = Input.mousePosition;
                    m_isDragging = GetPoint(out m_startPt) && 
                        (!RuntimeEditorApplication.IsOpened || RuntimeEditorApplication.IsPointerOverWindow(RuntimeWindowType.SceneView) && !RuntimeTools.IsPointerOverGameObject());
                    if (m_isDragging)
                    {
                        m_rectTransform.anchoredPosition = m_startPt;
                        m_rectTransform.sizeDelta = new Vector2(0, 0);
                        CursorHelper.SetCursor(this, null, Vector3.zero, CursorMode.Auto);
                        
                    }
                    else
                    {
                        RuntimeTools.ActiveTool = null;
                    }
                }
                else if (Input.GetMouseButtonUp(MouseButton))
                {
                    if (m_isDragging)
                    {
                        m_isDragging = false;
                        
                        HitTest();
                        m_rectTransform.sizeDelta = new Vector2(0, 0);
                        CursorHelper.ResetCursor(this);
                    }

                    RuntimeTools.ActiveTool = null;
                    m_active = false;

                }
                else if (m_isDragging)
                {
                    GetPoint(out m_endPt);

                    Vector2 size = m_endPt - m_startPt;
                    if(size != Vector2.zero)
                    {
                        RuntimeTools.ActiveTool = this;
                    }
                    m_rectTransform.sizeDelta = new Vector2(Mathf.Abs(size.x), Mathf.Abs(size.y));
                    m_rectTransform.localScale = new Vector3(Mathf.Sign(size.x), Mathf.Sign(size.y), 1);
                }
            }
        }

        private void HitTest()
        {
            if (m_rectTransform.sizeDelta.magnitude < 5f)
            {
                return;
            }

            Vector3 center = (m_startMousePosition + Input.mousePosition) / 2;
            center.z = 0.0f;
            Bounds selectionBounds = new Bounds(center, m_rectTransform.sizeDelta);

            //Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(SceneCamera);
            
            HashSet<GameObject> selection = new HashSet<GameObject>();
            Renderer[] renderers = FindObjectsOfType<Renderer>();
            Collider[] colliders = FindObjectsOfType<Collider>();
            FilteringArgs args = new FilteringArgs();
            for (int i = 0; i < renderers.Length; ++i)
            {
                Renderer r = renderers[i];
                Bounds bounds = r.bounds;
                GameObject go = r.gameObject;
                TrySelect(ref selectionBounds, selection, args, ref bounds, go /*, frustumPlanes*/);

           
            }
            for (int i = 0; i < colliders.Length; ++i)
            {
                Collider c = colliders[i];
                Bounds bounds = c.bounds;
                GameObject go = c.gameObject;
                TrySelect(ref selectionBounds, selection, args, ref bounds, go /*, frustumPlanes*/);
            }

            RuntimeSelection.objects = selection.ToArray();
        }

        private void TrySelect(ref Bounds selectionBounds, HashSet<GameObject> selection, FilteringArgs args, ref Bounds bounds, GameObject go /*, Plane[] frustumPlanes*/)
        {

            bool select;
            if (Method == BoxSelectionMethod.LooseFitting)
            {
                select = LooseFitting(ref selectionBounds, ref bounds);
            }
            else if (Method == BoxSelectionMethod.BoundsCenter)
            {
                select = BoundsCenter(ref selectionBounds, ref bounds);
            }
            else
            {
                select = TransformCenter(ref selectionBounds, go.transform);
            }

            //if (!GeometryUtility.TestPlanesAABB(frustumPlanes, bounds))
            //{
            //    select = false;
            //}

            if (select)
            {
                if (!selection.Contains(go))
                {
                    if (Filtering != null)
                    {
                        args.Object = go;
                        Filtering(this, args);
                        if (!args.Cancel)
                        {

                            selection.Add(go);
                        }
                        args.Reset();
                    }
                    else
                    {
                        selection.Add(go);
                    }
                }
            }
        }

        private bool TransformCenter(ref Bounds selectionBounds, Transform tr)
        {
            Vector3 screenPoint = SceneCamera.WorldToScreenPoint(tr.position);
            screenPoint.z = 0;
            return selectionBounds.Contains(screenPoint);
        }

        private bool BoundsCenter(ref Bounds selectionBounds, ref Bounds bounds)
        {
            Vector3 screenPoint = SceneCamera.WorldToScreenPoint(bounds.center);
            screenPoint.z = 0;
            return selectionBounds.Contains(screenPoint);
        }

        private bool LooseFitting(ref Bounds selectionBounds, ref Bounds bounds)
        {
            Vector3 p0 = bounds.center + new Vector3(-bounds.extents.x, -bounds.extents.y, -bounds.extents.z);
            Vector3 p1 = bounds.center + new Vector3(-bounds.extents.x, -bounds.extents.y, bounds.extents.z);
            Vector3 p2 = bounds.center + new Vector3(-bounds.extents.x, bounds.extents.y, -bounds.extents.z);
            Vector3 p3 = bounds.center + new Vector3(-bounds.extents.x, bounds.extents.y, bounds.extents.z);
            Vector3 p4 = bounds.center + new Vector3(bounds.extents.x, -bounds.extents.y, -bounds.extents.z);
            Vector3 p5 = bounds.center + new Vector3(bounds.extents.x, -bounds.extents.y, bounds.extents.z);
            Vector3 p6 = bounds.center + new Vector3(bounds.extents.x, bounds.extents.y, -bounds.extents.z);
            Vector3 p7 = bounds.center + new Vector3(bounds.extents.x, bounds.extents.y, bounds.extents.z);

            p0 = SceneCamera.WorldToScreenPoint(p0);
            p1 = SceneCamera.WorldToScreenPoint(p1);
            p2 = SceneCamera.WorldToScreenPoint(p2);
            p3 = SceneCamera.WorldToScreenPoint(p3);
            p4 = SceneCamera.WorldToScreenPoint(p4);
            p5 = SceneCamera.WorldToScreenPoint(p5);
            p6 = SceneCamera.WorldToScreenPoint(p6);
            p7 = SceneCamera.WorldToScreenPoint(p7);

            float minX = Mathf.Min(p0.x, p1.x, p2.x, p3.x, p4.x, p5.x, p6.x, p7.x);
            float maxX = Mathf.Max(p0.x, p1.x, p2.x, p3.x, p4.x, p5.x, p6.x, p7.x);
            float minY = Mathf.Min(p0.y, p1.y, p2.y, p3.y, p4.y, p5.y, p6.y, p7.y);
            float maxY = Mathf.Max(p0.y, p1.y, p2.y, p3.y, p4.y, p5.y, p6.y, p7.y);
            Vector3 min = new Vector2(minX, minY);
            Vector3 max = new Vector2(maxX, maxY);

            Bounds b = new Bounds((min + max) / 2, (max - min));
            return selectionBounds.Intersects(b);
        }

        private bool GetPoint(out Vector2 localPoint)
        {
            Camera cam = null;
            if(m_canvas.renderMode != RenderMode.ScreenSpaceOverlay)
            {
                cam = m_canvas.worldCamera;
            }

            return RectTransformUtility.ScreenPointToLocalPointInRectangle(m_canvas.GetComponent<RectTransform>(), Input.mousePosition, cam, out localPoint);
        }
    }

}

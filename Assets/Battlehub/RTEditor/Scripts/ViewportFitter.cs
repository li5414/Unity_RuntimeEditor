using UnityEngine;
using UnityEngine.Events;
using System;

namespace Battlehub.RTEditor
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(RectTransform))]
    public class ViewportFitter : MonoBehaviour
    {
        public UnityEvent ViewportRectChanged;

        private RectTransform m_viewport;
        private Vector3 m_viewportPosition;
        private float m_viewportWidth;
        private float m_viewportHeight;
        public Camera[] Cameras;

        private void Awake()
        {
            m_viewport = GetComponent<RectTransform>();
            
            Canvas canvas = m_viewport.GetComponentInParent<Canvas>();
            if(canvas == null)
            {
                gameObject.SetActive(false);
                return;
            }

            if(canvas.renderMode != RenderMode.ScreenSpaceCamera && canvas.renderMode != RenderMode.ScreenSpaceOverlay)
            {
                gameObject.SetActive(false);
                Debug.LogWarning("ViewportFitter requires canvas.renderMode -> RenderMode.ScreenSpaceOverlay or RenderMode.ScreenSpaceCamera");
                return;
            }

            if (Cameras == null)
            {
                return;
            }
            for (int i = 0; i < Cameras.Length; ++i)
            {
                if (Cameras[i] == null)
                {
                    continue;
                }
                Cameras[i].pixelRect = new Rect(new Vector2(0, 0), new Vector2(Screen.width, Screen.height));
            }
        }

        private void OnEnable()
        {
            Rect rect = m_viewport.rect;
            UpdateViewport();
            m_viewportHeight = rect.height;
            m_viewportWidth = rect.width;
            m_viewportPosition = m_viewport.position;   
        }

        private void Start()
        {
            Rect rect = m_viewport.rect;
            UpdateViewport();
            m_viewportHeight = rect.height;
            m_viewportWidth = rect.width;
            m_viewportPosition = m_viewport.position;
        }

        private void OnDisable()
        {
            if(Cameras == null)
            {
                return;
            }
            for (int i = 0; i < Cameras.Length; ++i)
            {
                if (Cameras[i] == null)
                {
                    continue;
                }
                Cameras[i].pixelRect = new Rect(new Vector2(0, 0), new Vector2(Screen.width, Screen.height));
            }

            ViewportRectChanged.Invoke();
        }

        private void OnGUI()
        {
            if(m_viewport != null)
            {
                Rect rect = m_viewport.rect;
                if (m_viewportHeight != rect.height || m_viewportWidth != rect.width || m_viewportPosition != m_viewport.position)
                {
                    UpdateViewport();
                    m_viewportHeight = rect.height;
                    m_viewportWidth = rect.width;
                    m_viewportPosition = m_viewport.position;
           
                }
            }
        }

        private void UpdateViewport()
        {

            if (Cameras == null)
            {
                return;
            }
            if (Cameras.Length == 0)
            {
                return;
            }
            
            Vector3[] corners = new Vector3[4];
            m_viewport.GetWorldCorners(corners);

            for (int i = 0; i < Cameras.Length; ++i)
            {
                if (Cameras[i] == null)
                {
                    continue;
                }
                Cameras[i].pixelRect = new Rect(corners[0], new Vector2(corners[2].x - corners[0].x, corners[1].y - corners[0].y));
            }

            ViewportRectChanged.Invoke();
        }
    }
}


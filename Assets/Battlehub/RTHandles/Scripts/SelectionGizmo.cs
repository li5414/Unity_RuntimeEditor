using UnityEngine;

using Battlehub.RTCommon;

namespace Battlehub.RTHandles
{
    /// <summary>
    /// Draws bounding box of selected object
    /// </summary>
    public class SelectionGizmo : MonoBehaviour, IGL
    {
        public bool DrawRay = true;
        public Camera SceneCamera;
        private ExposeToEditor m_exposeToEditor;
        private void Awake()
        {
            if (SceneCamera == null)
            {
                SceneCamera = Camera.main;
            }

            m_exposeToEditor = GetComponent<ExposeToEditor>();

            
        }

        private void Start()
        {
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

            if (m_exposeToEditor != null)
            {
                GLRenderer.Instance.Add(this);
            }

            if (!RuntimeSelection.IsSelected(gameObject))
            {
                Destroy(this);
            }
        }

        private void OnEnable()
        {
            if (m_exposeToEditor != null)
            {
                if (GLRenderer.Instance != null)
                {
                    GLRenderer.Instance.Add(this);
                }
            }
        }

        private void OnDisable()
        {
            if (GLRenderer.Instance != null)
            {
                GLRenderer.Instance.Remove(this);
            }
        }

        public void Draw(int cullingMask)
        {
            if (RuntimeTools.ShowSelectionGizmos)
            {
                RTLayer layer = RTLayer.SceneView;
                if ((cullingMask & (int)layer) == 0)
                {
                    return;
                }

                Bounds bounds = m_exposeToEditor.Bounds;
                Transform trform = m_exposeToEditor.BoundsObject.transform;
                RuntimeHandles.DrawBounds(ref bounds, trform.position, trform.rotation, trform.lossyScale);
             
                if(RuntimeTools.DrawSelectionGizmoRay)
                {
                    RuntimeHandles.DrawBoundRay(ref bounds, trform.TransformPoint(bounds.center), Quaternion.identity, trform.lossyScale);
                }
            }
        }
    }
}

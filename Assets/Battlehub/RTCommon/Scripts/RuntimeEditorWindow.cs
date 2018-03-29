using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battlehub.RTCommon
{
    public enum RuntimeWindowType
    {
        None,
        GameView,
        SceneView,
        Hierarchy,
        ProjectTree,
        Resources,
        Inspector,
        Other
    }

    public class RuntimeEditorWindow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        public RuntimeWindowType WindowType;

        private bool m_isPointerOver;

        private void Awake()
        {
            RuntimeEditorApplication.AddWindow(this);
            AwakeOverride();
        }


    

        private void OnDestroy()
        {
            RuntimeEditorApplication.ActivateWindow(null);
            RuntimeEditorApplication.PointerExit(this);
            RuntimeEditorApplication.RemoveWindow(this);
            OnDestroyOverride();
        }

        private void Update()
        {
            if (WindowType == RuntimeWindowType.GameView)
            {
                if (RuntimeEditorApplication.GameCameras == null || RuntimeEditorApplication.GameCameras.Length == 0)
                {
                    return;
                }

                Rect cameraRect = RuntimeEditorApplication.GameCameras[0].pixelRect;
                UpdateState(cameraRect, true);
            }
            else if (WindowType == RuntimeWindowType.SceneView)
            {
                if (RuntimeEditorApplication.ActiveSceneCamera == null)
                {
                    if (Camera.main != null)
                    {
                        RuntimeEditorApplication.SceneCameras = new[] { Camera.main };
                    }
                    else
                    {
                        return;
                    }
                }

                Rect cameraRect = RuntimeEditorApplication.ActiveSceneCamera.pixelRect;
                UpdateState(cameraRect, false);
            }
            else if (WindowType == RuntimeWindowType.None)
            {
                if(Camera.main == null)
                {
                    return;
                }

                Rect cameraRect = Camera.main.pixelRect;
                UpdateState(cameraRect, false);
            }
            else if(WindowType == RuntimeWindowType.Other)
            {
                return;
            }
            else
            {
                if (m_isPointerOver)
                {
                    if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2) ||
                        Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
                    {
                        RuntimeEditorApplication.ActivateWindow(this);
                    }
                }
            }

            UpdateOverride();
        }


        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (WindowType == RuntimeWindowType.SceneView || WindowType == RuntimeWindowType.GameView)
            {
                return;
            }

            RuntimeEditorApplication.ActivateWindow(this);
            OnPointerDownOverride(eventData);
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (WindowType == RuntimeWindowType.SceneView || WindowType == RuntimeWindowType.GameView)
            {
                return;
            }

            m_isPointerOver = true;
            RuntimeEditorApplication.PointerEnter(this);
            OnPointerEnterOverride(eventData);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            if (WindowType == RuntimeWindowType.SceneView || WindowType == RuntimeWindowType.GameView)
            {
                return;
            }
            m_isPointerOver = false;
            RuntimeEditorApplication.PointerExit(this);
            OnPointerExitOverride(eventData);
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            OnPointerUpOverride(eventData);
        }

        protected virtual void AwakeOverride()
        {

        }

   
        protected virtual void OnDestroyOverride()
        {

        }

        protected virtual void UpdateOverride()
        {

        }

        protected virtual void OnPointerDownOverride(PointerEventData eventData)
        {

        }

        protected virtual void OnPointerUpOverride(PointerEventData eventData)
        {

        }

        protected virtual void OnPointerEnterOverride(PointerEventData eventData)
        {

        }

        protected virtual void OnPointerExitOverride(PointerEventData eventData)
        {

        }

        private void UpdateState(Rect cameraRect, bool isGameView)
        {
            bool isPointerOver = cameraRect.Contains(Input.mousePosition) && !RuntimeTools.IsPointerOverGameObject();
            if (RuntimeEditorApplication.IsPointerOverWindow(this))
            {
                if (!isPointerOver)
                {
                    RuntimeEditorApplication.PointerExit(this);
                }
            }
            else
            {
                if (isPointerOver)
                {
                    RuntimeEditorApplication.PointerEnter(this);
                }
            }

            if (isPointerOver)
            {
                if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2) ||
                    Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
                {
                    if (!isGameView || isGameView && RuntimeEditorApplication.IsPlaying)
                    {
                        RuntimeEditorApplication.ActivateWindow(this);
                    }
                }
            }
        }

        
    }

}

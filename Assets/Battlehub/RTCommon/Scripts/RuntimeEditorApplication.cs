using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battlehub.RTCommon
{
    public delegate void RuntimeEditorEvent();
    public static class RuntimeEditorApplication
    {
        public static event RuntimeEditorEvent PlaymodeStateChanging;
        public static event RuntimeEditorEvent PlaymodeStateChanged;
        public static event RuntimeEditorEvent ActiveWindowChanged;
        public static event RuntimeEditorEvent PointerOverWindowChanged;
        public static event RuntimeEditorEvent IsOpenedChanged;
        public static event RuntimeEditorEvent ActiveSceneCameraChanged;

        public static event RuntimeEditorEvent SaveSelectedObjectsRequired;
        public static void SaveSelectedObjects()
        {
            if(SaveSelectedObjectsRequired != null)
            {
                SaveSelectedObjectsRequired();
            }
        }

        private static List<RuntimeEditorWindow> m_windows = new List<RuntimeEditorWindow>();
        private static RuntimeEditorWindow m_pointerOverWindow;
        private static RuntimeEditorWindow m_activeWindow;

        static RuntimeEditorApplication()
        {
            Reset();
        }

        public static void Reset()
        {
            m_windows = new List<RuntimeEditorWindow>(); 
            m_pointerOverWindow = null;
            m_activeWindow = null;
            m_activeCameraIndex = 0;
            GameCameras = null;
            SceneCameras = null;
            m_isOpened = false;
            m_isPlaying = false;

            RuntimeSelection.objects = null;
            RuntimeUndo.Reset();
            RuntimeTools.Reset();
        }

        public static RuntimeEditorWindow PointerOverWindow
        {
            get { return m_pointerOverWindow; }
        }

        public static RuntimeWindowType PointerOverWindowType
        {
            get
            {
                if (m_pointerOverWindow == null)
                {
                    return RuntimeWindowType.None;
                }

                return m_pointerOverWindow.WindowType;
            }
        }

        public static RuntimeEditorWindow ActiveWindow
        {
            get { return m_activeWindow; }
        }

        public static RuntimeWindowType ActiveWindowType
        {
            get
            {
                if (m_activeWindow == null)
                {
                    return RuntimeWindowType.None;
                }

                return m_activeWindow.WindowType;
            }
        }

        public static RuntimeEditorWindow GetWindow(RuntimeWindowType type)
        {
            return m_windows.Where(wnd => wnd != null && wnd.WindowType == type).FirstOrDefault();
        }

        public static void ActivateWindow(RuntimeEditorWindow window)
        {
            if (m_activeWindow != window)
            {
                m_activeWindow = window;
                if (ActiveWindowChanged != null)
                {
                    ActiveWindowChanged();
                }
            }
        }

        public static void ActivateWindow(RuntimeWindowType type)
        {
            ActivateWindow(GetWindow(type));
        }

        public static void PointerEnter(RuntimeEditorWindow window)
        {
            if (m_pointerOverWindow != window)
            {
                m_pointerOverWindow = window;
                if (PointerOverWindowChanged != null)
                {
                    PointerOverWindowChanged();
                }
            }

        }

        public static void PointerExit(RuntimeEditorWindow window)
        {
            if (m_pointerOverWindow == window && m_pointerOverWindow != null)
            {
                m_pointerOverWindow = null;
                if (PointerOverWindowChanged != null)
                {
                    PointerOverWindowChanged();
                }
            }
        }

        public static bool IsPointerOverWindow(RuntimeWindowType type)
        {
            return PointerOverWindowType == type;
        }

        public static bool IsPointerOverWindow(RuntimeEditorWindow window)
        {
            return m_pointerOverWindow == window;
        }

        public static bool IsActiveWindow(RuntimeWindowType type)
        {
            return ActiveWindowType == type;
        }

        public static bool IsActiveWindow(RuntimeEditorWindow window)
        {
            return m_activeWindow == window;
        }

        public static void AddWindow(RuntimeEditorWindow window)
        {
            m_windows.Add(window);
        }

        public static void RemoveWindow(RuntimeEditorWindow window)
        {
            if(m_windows != null)
            {
                m_windows.Remove(window);
            }
            
        }

        public static Camera[] GameCameras
        {
            get;
            set;
        }

        public static Camera[] SceneCameras
        {
            get;
            set;
        }

        private static int m_activeCameraIndex;
        public static int ActiveSceneCameraIndex
        {
            get { return m_activeCameraIndex; }
            set
            {
                if(m_activeCameraIndex != value)
                {
                    m_activeCameraIndex = value;
                    if(ActiveSceneCameraChanged != null)
                    {
                        ActiveSceneCameraChanged();
                    }
                }
            }
        }

        public static Camera ActiveSceneCamera
        {
            get
            {
                if (SceneCameras == null || SceneCameras.Length == 0)
                {
                    return null;
                }
                return SceneCameras[ActiveSceneCameraIndex];
            }
        }
        private static bool m_isOpened;
        public static bool IsOpened
        {
            get { return m_isOpened; }
            set
            {
                if (m_isOpened != value)
                {
                    m_isOpened = value;
                    if (!m_isOpened)
                    {
                        ActivateWindow(GetWindow(RuntimeWindowType.GameView));
                    }
                    if (IsOpenedChanged != null)
                    {
                        IsOpenedChanged();
                    }

                }
            }
        }

        private static bool m_isPlayModeStateChanging;
        public static bool IsPlaymodeStateChanging
        {
            get { return m_isPlayModeStateChanging; }
        }
        
        private static bool m_isPlaying;
        public static bool IsPlaying
        {
            get
            {
                return m_isPlaying;
            }
            set
            {
                if (m_isPlaying != value)
                {
                    m_isPlaying = value;

                    m_isPlayModeStateChanging = true;
                    if (PlaymodeStateChanging != null)
                    {
                        PlaymodeStateChanging();
                    }
                    if (PlaymodeStateChanged != null)
                    {
                        PlaymodeStateChanged();
                    }
                    m_isPlayModeStateChanging = false;

                }
            }
        }
    }

}

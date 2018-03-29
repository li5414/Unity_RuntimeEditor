using UnityEngine;
using UnityEngine.EventSystems;

namespace Battlehub.RTCommon
{
    public enum RuntimeTool
    {
        None,
        Move,
        Rotate,
        Scale,
        View,
    }

    public enum RuntimePivotRotation
    {
        Local,
        Global
    }

    public enum RuntimePivotMode
    {
        Center = 0,
        Pivot = 1
    }

    public enum SnappingMode
    {
        BoundingBox,
        Vertex,
    }

    public delegate void RuntimeToolsEvent();
    public delegate void SpawnPrefabChanged(GameObject oldPrefab);


    /// <summary>
    /// Runtime tools and flags
    /// </summary>
    public static class RuntimeTools
    {
        public static event RuntimeToolsEvent ToolChanged;
        public static event RuntimeToolsEvent PivotRotationChanged;
        public static event RuntimeToolsEvent PivotModeChanged;
        public static event SpawnPrefabChanged SpawnPrefabChanged;
        
        public static event RuntimeToolsEvent IsViewingChanged;
        public static event RuntimeToolsEvent ShowSelectionGizmosChanged;
        public static event RuntimeToolsEvent ShowGizmosChanged;
        public static event RuntimeToolsEvent AutoFocusChanged;
        public static event RuntimeToolsEvent UnitSnappingChanged;
        public static event RuntimeToolsEvent IsSnappingChanged;
        public static event RuntimeToolsEvent SnappingModeChanged;

        private static RuntimeTool m_current;
        private static RuntimePivotMode m_pivotMode;
        private static RuntimePivotRotation m_pivotRotation;

        private static bool m_isViewing;
        public static bool IsViewing
        {
            get { return m_isViewing; }
            set
            {
                if(m_isViewing != value)
                {
                    m_isViewing = value;
                    if(m_isViewing)
                    {
                        ActiveTool = null;
                    }
                    if(IsViewingChanged != null)
                    {
                        IsViewingChanged();
                    }
                }
            }
        }

        private static bool m_showSelectionGizmos;
        public static bool ShowSelectionGizmos
        {
            get { return m_showSelectionGizmos; }
            set
            {
                if(m_showSelectionGizmos != value)
                {
                    m_showSelectionGizmos = value;
                    if(ShowSelectionGizmosChanged != null)
                    {
                        ShowSelectionGizmosChanged();
                    }
                }
            }
        }

        private static bool m_showGizmos;
        public static bool ShowGizmos
        {
            get { return m_showGizmos; }
            set
            {
                if(m_showGizmos != value)
                {
                    m_showGizmos = value;
                    if(ShowGizmosChanged != null)
                    {
                        ShowGizmosChanged();
                    }
                }
            }
        }

        private static bool m_autoFocus;
        public static bool AutoFocus
        {
            get { return m_autoFocus; }
            set
            {
                if(m_autoFocus != value)
                {
                    m_autoFocus = value;
                    if(AutoFocusChanged != null)
                    {
                        AutoFocusChanged();
                    }
                }
            }
        }

        private static bool m_unitSnapping;
        public static bool UnitSnapping
        {
            get { return m_unitSnapping; }
            set
            {
                if(m_unitSnapping != value)
                {
                    m_unitSnapping = value;
                    if(UnitSnappingChanged != null)
                    {
                        UnitSnappingChanged();
                    }
                }
            }
        }

        private static bool m_isSnapping;
        public static bool IsSnapping
        {
            get { return m_isSnapping; }
            set
            {
                if(m_isSnapping != value)
                {
                    m_isSnapping = value;
                    if(IsSnappingChanged != null)
                    {
                        IsSnappingChanged();
                    }
                }
            }
        }

        private static SnappingMode m_snappingMode = SnappingMode.Vertex;
        public static SnappingMode SnappingMode
        {
            get { return m_snappingMode; }
            set
            {
                if(m_snappingMode != value)
                {
                    m_snappingMode = value;
                    if(SnappingModeChanged != null)
                    {
                        SnappingModeChanged();
                    }
                }
            }
        }

        private static GameObject m_spawnPrefab;
        public static GameObject SpawnPrefab
        {
            get { return m_spawnPrefab; }
            set
            {
                if (m_spawnPrefab != value)
                {
                    GameObject oldPrefab = m_spawnPrefab;
                    m_spawnPrefab = value;
                    if (SpawnPrefabChanged != null)
                    {
                        SpawnPrefabChanged(oldPrefab);
                    }
                }
            }
        }

        public static Object ActiveTool
        {
            get;
            set;
        }
        
        public static LockObject LockAxes
        {
            get;
            set;
        }

        public static RuntimeTool Current
        {
            get { return m_current; }
            set
            {
                if (m_current != value)
                {
                    m_current = value;
                    if (ToolChanged != null)
                    {
                        ToolChanged();
                    }
                }
            }
        }

    
        public static RuntimePivotRotation PivotRotation
        {
            get { return m_pivotRotation; }
            set
            {
                if (m_pivotRotation != value)
                {
                    m_pivotRotation = value;
                    if (PivotRotationChanged != null)
                    {
                        PivotRotationChanged();
                    }
                }
            }
        }

        public static RuntimePivotMode PivotMode
        {
            get { return m_pivotMode; }
            set
            {
                if(m_pivotMode != value)
                {
                    m_pivotMode = value;
                    if(PivotModeChanged != null)
                    {
                        PivotModeChanged();
                    }
                }
            }
        }

        public static bool DrawSelectionGizmoRay
        {
            get;
            set;
        }

        static RuntimeTools()
        {
            Reset();
        }

        public static void Reset()
        {
            ActiveTool = null;
            LockAxes = null;
            m_isViewing = false;
            m_isSnapping = false;
            m_showSelectionGizmos = true;
            m_showGizmos = true;
            m_unitSnapping = false;
            m_pivotMode = RuntimePivotMode.Center;
            SpawnPrefab = null;
        }

        public static bool IsPointerOverGameObject()
        {
            return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
        }
    }
}

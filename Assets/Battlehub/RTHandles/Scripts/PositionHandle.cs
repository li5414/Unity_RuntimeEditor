using System.Collections.Generic;
using UnityEngine;
using Battlehub.RTSaveLoad;
using System.Linq;
using UnityEngine.EventSystems;

using Battlehub.RTCommon;
namespace Battlehub.RTHandles
{
  
    /// <summary>
    /// Position Handle
    /// </summary>
    public class PositionHandle : BaseHandle
    {
        public float GridSize = 1.0f;

        private Vector3 m_cursorPosition;
        private Vector3 m_currentPosition;

        private Vector3 m_prevPoint;
        private Matrix4x4 m_matrix;
        private Matrix4x4 m_inverse;

        private Vector2 m_prevMousePosition;
        private int[] m_targetLayers;
        private Transform[] m_snapTargets;
        private Bounds[] m_snapTargetsBounds;
        private ExposeToEditor[] m_allExposedToEditor;

        public bool SnapToGround;
        public KeyCode SnapToGroundKey = KeyCode.G;


        public KeyCode SnappingKey = KeyCode.V;
        public KeyCode SnappingToggle = KeyCode.LeftShift;

        private bool m_isInSnappingMode = false;
        private bool IsInSnappingMode
        {
            get { return m_isInSnappingMode || RuntimeTools.IsSnapping; }
        }
 
        private Vector3[] m_boundingBoxCorners = new Vector3[8];
        private Vector3 m_handleOffset;
        private Vector3 HandlePosition
        {
            get { return transform.position + m_handleOffset; }
            set
            {
                transform.position = value - m_handleOffset;
            }
        }

        protected override RuntimeTool Tool
        {
            get { return RuntimeTool.Move; }
        }

        protected override float CurrentGridUnitSize
        {
            get { return GridSize; }
        }

        protected override void AwakeOverride()
        {
           
        }

        protected override void OnDestroyOverride()
        {
            base.OnDestroyOverride();
        }

        protected override void OnEnableOverride()
        {
            base.OnEnableOverride();
            m_isInSnappingMode = false;
            RuntimeTools.IsSnapping = false;
            m_handleOffset = Vector3.zero;
            m_targetLayers = null;
            m_snapTargets = null;
            m_snapTargetsBounds = null;
            m_allExposedToEditor = null;

            RuntimeTools.IsSnappingChanged += OnSnappingChanged;
            OnSnappingChanged();
        }

        protected override void OnDisableOverride()
        {
            base.OnDisableOverride();
            RuntimeTools.IsSnapping = false;
            m_targetLayers = null;
            m_snapTargets = null;
            m_snapTargetsBounds = null;
            m_allExposedToEditor = null;

            RuntimeTools.IsSnappingChanged -= OnSnappingChanged;
        }

        protected override void UpdateOverride()
        {
            base.UpdateOverride();

            if (RuntimeTools.IsPointerOverGameObject())
            {
                return;
            }

            if (IsDragging)
            {
                if ((SnapToGround || InputController.GetKey(SnapToGroundKey)) && SelectedAxis != RuntimeHandleAxis.Y)
                {
                    SnapActiveTargetsToGround(ActiveTargets, SceneCamera, true);
                    transform.position = Targets[0].position;
                }
            }

            if (HightlightOnHover && !IsDragging)
            {
                SelectedAxis = Hit();
            }
            
            if (InputController.GetKeyDown(SnappingKey))
            {

                if(!LockObject.IsPositionLocked)
                {
                    m_isInSnappingMode = true;
                    if (InputController.GetKey(SnappingToggle))
                    {
                        RuntimeTools.IsSnapping = !RuntimeTools.IsSnapping;
                    }

                    BeginSnap();
                    m_prevMousePosition = Input.mousePosition;
                }

                
            }
            else if (InputController.GetKeyUp(SnappingKey))
            {
                SelectedAxis = RuntimeHandleAxis.None;
                m_isInSnappingMode = false;
                if (!IsInSnappingMode)
                {
                    m_handleOffset = Vector3.zero;
                }
            }

            if (IsInSnappingMode)
            {
                Vector2 mousePosition = Input.mousePosition;
                if (RuntimeTools.SnappingMode == SnappingMode.BoundingBox)
                {
                    if (IsDragging)
                    {
                        SelectedAxis = RuntimeHandleAxis.Snap;
                        if (m_prevMousePosition != mousePosition)
                        {
                            m_prevMousePosition = mousePosition;
                            float minDistance = float.MaxValue;
                            Vector3 minPoint = Vector3.zero;
                            bool minPointFound = false;
                            for (int i = 0; i < m_allExposedToEditor.Length; ++i)
                            {
                                ExposeToEditor exposeToEditor = m_allExposedToEditor[i];
                                Bounds bounds = exposeToEditor.Bounds;
                                m_boundingBoxCorners[0] = bounds.center + new Vector3(bounds.extents.x, bounds.extents.y, bounds.extents.z);
                                m_boundingBoxCorners[1] = bounds.center + new Vector3(bounds.extents.x, bounds.extents.y, -bounds.extents.z);
                                m_boundingBoxCorners[2] = bounds.center + new Vector3(bounds.extents.x, -bounds.extents.y, bounds.extents.z);
                                m_boundingBoxCorners[3] = bounds.center + new Vector3(bounds.extents.x, -bounds.extents.y, -bounds.extents.z);
                                m_boundingBoxCorners[4] = bounds.center + new Vector3(-bounds.extents.x, bounds.extents.y, bounds.extents.z);
                                m_boundingBoxCorners[5] = bounds.center + new Vector3(-bounds.extents.x, bounds.extents.y, -bounds.extents.z);
                                m_boundingBoxCorners[6] = bounds.center + new Vector3(-bounds.extents.x, -bounds.extents.y, bounds.extents.z);
                                m_boundingBoxCorners[7] = bounds.center + new Vector3(-bounds.extents.x, -bounds.extents.y, -bounds.extents.z);
                                GetMinPoint(ref minDistance, ref minPoint, ref minPointFound, exposeToEditor.BoundsObject.transform);
                            }

                            if (minPointFound)
                            {
                                HandlePosition = minPoint;
                            }
                        }
                    }
                    else
                    {
                        SelectedAxis = RuntimeHandleAxis.None;
                        if (m_prevMousePosition != mousePosition)
                        {
                            m_prevMousePosition = mousePosition;

                            float minDistance = float.MaxValue;
                            Vector3 minPoint = Vector3.zero;
                            bool minPointFound = false;
                            for (int i = 0; i < m_snapTargets.Length; ++i)
                            {
                                Transform snapTarget = m_snapTargets[i];
                                Bounds bounds = m_snapTargetsBounds[i];

                                m_boundingBoxCorners[0] = bounds.center + new Vector3(bounds.extents.x, bounds.extents.y, bounds.extents.z);
                                m_boundingBoxCorners[1] = bounds.center + new Vector3(bounds.extents.x, bounds.extents.y, -bounds.extents.z);
                                m_boundingBoxCorners[2] = bounds.center + new Vector3(bounds.extents.x, -bounds.extents.y, bounds.extents.z);
                                m_boundingBoxCorners[3] = bounds.center + new Vector3(bounds.extents.x, -bounds.extents.y, -bounds.extents.z);
                                m_boundingBoxCorners[4] = bounds.center + new Vector3(-bounds.extents.x, bounds.extents.y, bounds.extents.z);
                                m_boundingBoxCorners[5] = bounds.center + new Vector3(-bounds.extents.x, bounds.extents.y, -bounds.extents.z);
                                m_boundingBoxCorners[6] = bounds.center + new Vector3(-bounds.extents.x, -bounds.extents.y, bounds.extents.z);
                                m_boundingBoxCorners[7] = bounds.center + new Vector3(-bounds.extents.x, -bounds.extents.y, -bounds.extents.z);
                                if (Targets[i] != null)
                                {
                                    GetMinPoint(ref minDistance, ref minPoint, ref minPointFound, snapTarget);
                                }
                            }

                            if (minPointFound)
                            {
                                m_handleOffset = minPoint - transform.position;
                            }
                        }
                    }
                }
                else
                {
                    if(IsDragging)
                    {
                        SelectedAxis = RuntimeHandleAxis.Snap;
                        if (m_prevMousePosition != mousePosition)
                        {
                            m_prevMousePosition = mousePosition;

                            Ray ray = SceneCamera.ScreenPointToRay(mousePosition);
                            RaycastHit hitInfo;

                            LayerMask layerMask = (1 << Physics.IgnoreRaycastLayer);
                            layerMask = ~layerMask;

                            for (int i = 0; i < m_snapTargets.Length; ++i)
                            {
                                m_targetLayers[i] = m_snapTargets[i].gameObject.layer;
                                m_snapTargets[i].gameObject.layer = Physics.IgnoreRaycastLayer;
                            }

                            GameObject closestObject = null;
                            if (Physics.Raycast(ray, out hitInfo, float.PositiveInfinity, layerMask))
                            {
                                closestObject = hitInfo.collider.gameObject;
                            }
                            else
                            {
                                float minDistance = float.MaxValue;
                                for (int i = 0; i < m_allExposedToEditor.Length; ++i)
                                {
                                    ExposeToEditor exposedToEditor = m_allExposedToEditor[i];
                                    Bounds bounds = exposedToEditor.Bounds;

                                    m_boundingBoxCorners[0] = bounds.center + new Vector3(bounds.extents.x, bounds.extents.y, bounds.extents.z);
                                    m_boundingBoxCorners[1] = bounds.center + new Vector3(bounds.extents.x, bounds.extents.y, -bounds.extents.z);
                                    m_boundingBoxCorners[2] = bounds.center + new Vector3(bounds.extents.x, -bounds.extents.y, bounds.extents.z);
                                    m_boundingBoxCorners[3] = bounds.center + new Vector3(bounds.extents.x, -bounds.extents.y, -bounds.extents.z);
                                    m_boundingBoxCorners[4] = bounds.center + new Vector3(-bounds.extents.x, bounds.extents.y, bounds.extents.z);
                                    m_boundingBoxCorners[5] = bounds.center + new Vector3(-bounds.extents.x, bounds.extents.y, -bounds.extents.z);
                                    m_boundingBoxCorners[6] = bounds.center + new Vector3(-bounds.extents.x, -bounds.extents.y, bounds.extents.z);
                                    m_boundingBoxCorners[7] = bounds.center + new Vector3(-bounds.extents.x, -bounds.extents.y, -bounds.extents.z);

                                    for(int j = 0; j < m_boundingBoxCorners.Length; ++j)
                                    {
                                        Vector2 screenPoint = SceneCamera.WorldToScreenPoint(exposedToEditor.BoundsObject.transform.TransformPoint(m_boundingBoxCorners[j]));
                                        float distance = (screenPoint - mousePosition).magnitude;
                                        
                                        if (distance < minDistance)
                                        {
                                            closestObject = exposedToEditor.gameObject;
                                            minDistance = distance;
                                        }
                                    }
                                }
                            }

                            if(closestObject != null)
                            {
                                float minDistance = float.MaxValue;
                                Vector3 minPoint = Vector3.zero;
                                bool minPointFound = false;
                                Transform meshTransform;
                                Mesh mesh = GetMesh(closestObject, out meshTransform);
                                GetMinPoint(meshTransform, ref minDistance, ref minPoint, ref minPointFound, mesh);

                                if (minPointFound)
                                {
                                    HandlePosition = minPoint;
                                }

                            }

                            for (int i = 0; i < m_snapTargets.Length; ++i)
                            {
                                m_snapTargets[i].gameObject.layer = m_targetLayers[i];
                            }
                        }
                    }
                    else
                    {
                        SelectedAxis = RuntimeHandleAxis.None;
                        if (m_prevMousePosition != mousePosition)
                        {
                            m_prevMousePosition = mousePosition;

                            float minDistance = float.MaxValue;
                            Vector3 minPoint = Vector3.zero;
                            bool minPointFound = false;
                            for (int i = 0; i < RealTargets.Length; ++i)
                            {
                                Transform snapTarget = RealTargets[i];
                                Transform meshTranform;
                                Mesh mesh = GetMesh(snapTarget.gameObject, out meshTranform);
                                GetMinPoint(meshTranform, ref minDistance, ref minPoint, ref minPointFound, mesh);   
                            }
                            if (minPointFound)
                            {
                                m_handleOffset = minPoint - transform.position;
                            }
                        }
                    }
                }
            }     
        }

        private void GetMinPoint(Transform meshTransform, ref float minDistance, ref Vector3 minPoint, ref bool minPointFound, Mesh mesh)
        {
            if (mesh != null && mesh.isReadable)
            {
                Vector3[] vertices = mesh.vertices;
                for (int i = 0; i < vertices.Length; ++i)
                {
                    Vector3 vert = vertices[i];
                    vert = meshTransform.TransformPoint(vert);

                    Vector3 screenPoint = SceneCamera.WorldToScreenPoint(vert);
                    screenPoint.z = 0;
                    Vector3 mousePoint = Input.mousePosition;
                    mousePoint.z = 0;
                    float distance = (screenPoint - mousePoint).magnitude;
                    if (distance < minDistance)
                    {
                        minPointFound = true;
                        minDistance = distance;
                        minPoint = vert;
                    }
                }
            }
        }

        private static Mesh GetMesh(GameObject go, out Transform meshTransform)
        {
            Mesh mesh = null;
            meshTransform = null;
            MeshFilter filter = go.GetComponentInChildren<MeshFilter>();
            if (filter != null)
            {
                mesh = filter.sharedMesh;
                meshTransform = filter.transform;
            }
            else
            {
                SkinnedMeshRenderer skinnedMeshRender = go.GetComponentInChildren<SkinnedMeshRenderer>();
                if (skinnedMeshRender != null)
                {
                    mesh = skinnedMeshRender.sharedMesh;
                    meshTransform = skinnedMeshRender.transform;
                }
                else
                {
                    MeshCollider collider = go.GetComponentInChildren<MeshCollider>();
                    if (collider != null)
                    {
                        mesh = collider.sharedMesh;
                        meshTransform = collider.transform;
                    }
                }
            }

            return mesh;
        }

        protected override void OnDrop()
        {
            base.OnDrop();
            if(SnapToGround || InputController.GetKey(SnapToGroundKey))
            {
                SnapActiveTargetsToGround(ActiveTargets, SceneCamera, true);
                transform.position = Targets[0].position;
            }
        }


        private static void SnapActiveTargetsToGround(Transform[] targets, Camera camera,  bool rotate)
        {
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
            for (int i = 0; i < targets.Length; ++i)
            {
                Transform activeTarget = targets[i];
                Ray ray = new Ray(activeTarget.position, Vector3.up);
                bool hitFrustum = false;
                Vector3 topPoint = activeTarget.position;
                for (int j = 0; j < planes.Length; ++j)
                {
                    float distance;
                    if (planes[j].Raycast(ray, out distance))
                    {
                        hitFrustum = true;
                        topPoint = ray.GetPoint(distance);
                    }
                }

                if (!hitFrustum)
                {
                    continue;
                }

                ray = new Ray(topPoint, Vector3.down);

                RaycastHit[] hits = Physics.RaycastAll(ray).Where(hit => !hit.transform.IsChildOf(activeTarget)).ToArray();
                if (hits.Length == 0)
                {
                    continue;
                }

                float minDistance = float.PositiveInfinity;
                RaycastHit bestHit = hits[0];
                for (int j = 0; j < hits.Length; ++j)
                {
                    float mag = (activeTarget.position - hits[j].point).magnitude;
                    if (mag < minDistance)
                    {
                        minDistance = mag;
                        bestHit = hits[j];
                    }
                }

                activeTarget.position += (bestHit.point - activeTarget.position);
                if (rotate)
                {
                    activeTarget.rotation = Quaternion.FromToRotation(activeTarget.up, bestHit.normal) * activeTarget.rotation;
                }
            }
        }

        private void OnSnappingChanged()
        {
            if (RuntimeTools.IsSnapping)
            {
                BeginSnap();
            }
            else
            {
                m_handleOffset = Vector3.zero;
            }
        }

        private void BeginSnap()
        {
            if(SceneCamera == null)
            {
                return;
            }
            HashSet<Transform> snapTargetsHS = new HashSet<Transform>();
            List<Transform> snapTargets = new List<Transform>();
            List<Bounds> snapTargetBounds = new List<Bounds>();
            
            if (Target != null)
            {
                for (int i = 0; i < RealTargets.Length; ++i)
                {
                    Transform target = RealTargets[i];
                    if (target != null)
                    {
                        snapTargets.Add(target);
                        snapTargetsHS.Add(target);

                        ExposeToEditor exposeToEditor = target.GetComponent<ExposeToEditor>();
                        if (exposeToEditor != null)
                        {
                            snapTargetBounds.Add(exposeToEditor.Bounds);
                        }
                        else
                        {
                            MeshFilter filter = target.GetComponent<MeshFilter>();
                            if(filter != null && filter.sharedMesh != null)
                            {
                                snapTargetBounds.Add(filter.sharedMesh.bounds);
                            }
                            else
                            {
                                SkinnedMeshRenderer smr = target.GetComponent<SkinnedMeshRenderer>();
                                if(smr != null && smr.sharedMesh != null)
                                {
                                    snapTargetBounds.Add(smr.sharedMesh.bounds);
                                }
                                else
                                {
                                    Bounds b = new Bounds(Vector3.zero, Vector3.zero);
                                    snapTargetBounds.Add(b);
                                }
                            }
                        }
                    }
                }
            }

            
            m_snapTargets = snapTargets.ToArray();
            m_targetLayers = new int[m_snapTargets.Length];
            m_snapTargetsBounds = snapTargetBounds.ToArray();


            Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(SceneCamera);
            ExposeToEditor[] exposeToEditorObjects = FindObjectsOfType<ExposeToEditor>();
            List<ExposeToEditor> insideOfFrustum = new List<ExposeToEditor>();
            for (int i = 0; i < exposeToEditorObjects.Length; ++i)
            {
                ExposeToEditor exposeToEditor = exposeToEditorObjects[i];
                if (exposeToEditor.CanSnap)
                {
                    if (GeometryUtility.TestPlanesAABB(frustumPlanes, new Bounds(exposeToEditor.transform.TransformPoint(exposeToEditor.Bounds.center), Vector3.zero)))
                    {
                        if (!snapTargetsHS.Contains(exposeToEditor.transform))
                        {
                            insideOfFrustum.Add(exposeToEditor);
                        }
                    }
                }

            }
            m_allExposedToEditor = insideOfFrustum.ToArray();
        }

        private void GetMinPoint(ref float minDistance, ref Vector3 minPoint, ref bool minPointFound, Transform tr)
        {
            for (int j = 0; j < m_boundingBoxCorners.Length; ++j)
            {
                Vector3 worldPoint = tr.TransformPoint(m_boundingBoxCorners[j]);
                Vector3 screenPoint = SceneCamera.WorldToScreenPoint(worldPoint);
                screenPoint.z = 0;
                Vector3 mousePoint = Input.mousePosition;
                mousePoint.z = 0;
                float distance = (screenPoint - mousePoint).magnitude;
                if (distance < minDistance)
                {
                    minPointFound = true;
                    minDistance = distance;
                    minPoint = worldPoint;
                }
            }
        }

        private bool HitSnapHandle()
        {
            Vector3 sp = SceneCamera.WorldToScreenPoint(HandlePosition);
            Vector3 mp = Input.mousePosition;

            const float pixelSize = 10;

            return sp.x - pixelSize <= mp.x && mp.x <= sp.x + pixelSize  &&
                   sp.y - pixelSize  <= mp.y && mp.y <= sp.y + pixelSize;
        }

        private bool HitQuad(Vector3 axis, Matrix4x4 matrix, float size)
        {
            Ray ray = SceneCamera.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(matrix.MultiplyVector(axis).normalized, matrix.MultiplyPoint(Vector3.zero));

            float distance;
            if(!plane.Raycast(ray, out distance))
            {
                return false;
            }

            Vector3 point = ray.GetPoint(distance);
            point = matrix.inverse.MultiplyPoint(point);

            Vector3 toCam = matrix.inverse.MultiplyVector(SceneCamera.transform.position - HandlePosition);

            float fx = Mathf.Sign(Vector3.Dot(toCam, Vector3.right));
            float fy = Mathf.Sign(Vector3.Dot(toCam, Vector3.up));
            float fz = Mathf.Sign(Vector3.Dot(toCam, Vector3.forward));

            point.x *= fx;
            point.y *= fy;
            point.z *= fz;

            float lowBound = -0.01f;

            bool result = point.x >= lowBound && point.x <= size && point.y >= lowBound && point.y <= size && point.z >= lowBound && point.z <= size;

            if(result)
            {
                DragPlane = GetDragPlane(matrix, axis);
            }
           
            return result;
        }

        private RuntimeHandleAxis Hit()
        {
            float scale = RuntimeHandles.GetScreenScale(HandlePosition, SceneCamera);
            m_matrix = Matrix4x4.TRS(HandlePosition, Rotation, Vector3.one);// transform.localScale);
            m_inverse = m_matrix.inverse;

            Matrix4x4 matrix = Matrix4x4.TRS(HandlePosition, Rotation, new Vector3(scale, scale, scale));
            float s = 0.3f * scale;

            if (HitQuad(Vector3.up * RuntimeHandles.HandleScale, m_matrix, s))
            {
                return RuntimeHandleAxis.XZ;
            }

            if (HitQuad(Vector3.right * RuntimeHandles.HandleScale, m_matrix, s))
            {
                return RuntimeHandleAxis.YZ;
            }

            if (HitQuad(Vector3.forward * RuntimeHandles.HandleScale, m_matrix, s))
            {
                return RuntimeHandleAxis.XY;
            }

            float distToYAxis;
            float distToZAxis;
            float distToXAxis;
            bool hit = HitAxis(Vector3.up * RuntimeHandles.HandleScale, matrix, out distToYAxis);
            hit |= HitAxis(Vector3.forward * RuntimeHandles.HandleScale, matrix, out distToZAxis);
            hit |= HitAxis(Vector3.right * RuntimeHandles.HandleScale, matrix, out distToXAxis);

            if (hit)
            {
                if (distToYAxis <= distToZAxis && distToYAxis <= distToXAxis)
                {
                    return RuntimeHandleAxis.Y;
                }
                else if (distToXAxis <= distToYAxis && distToXAxis <= distToZAxis)
                {
                    return RuntimeHandleAxis.X;
                }
                else
                {
                    return RuntimeHandleAxis.Z;
                }
            }

            return  RuntimeHandleAxis.None;
        }

        protected override bool OnBeginDrag()
        {
            SelectedAxis = Hit();
            
            m_currentPosition = HandlePosition;
            m_cursorPosition = HandlePosition;

            if (IsInSnappingMode)
            {
                return HitSnapHandle();
            }

            if (SelectedAxis == RuntimeHandleAxis.XZ)
            {
                return GetPointOnDragPlane(Input.mousePosition, out m_prevPoint);
            }

            if (SelectedAxis == RuntimeHandleAxis.YZ)
            {
                return GetPointOnDragPlane(Input.mousePosition, out m_prevPoint);
            }

            if (SelectedAxis == RuntimeHandleAxis.XY)
            {
                return GetPointOnDragPlane(Input.mousePosition, out m_prevPoint);
            }

            if (SelectedAxis != RuntimeHandleAxis.None)
            {
                DragPlane = GetDragPlane();
                bool result = GetPointOnDragPlane(Input.mousePosition, out m_prevPoint);
                if(!result)
                {
                    SelectedAxis = RuntimeHandleAxis.None;
                }
                return result;
            }

            return false;
        }

        protected override void OnDrag()
        {
            if (IsInSnappingMode)
            {
                return;
            }

            Vector3 point;
            if (GetPointOnDragPlane(Input.mousePosition, out point))
            {
                Vector3 offset = m_inverse.MultiplyVector(point - m_prevPoint);
                float mag = offset.magnitude;
                if (SelectedAxis == RuntimeHandleAxis.X)
                {
                    offset.y = offset.z = 0.0f;
                }
                else if (SelectedAxis == RuntimeHandleAxis.Y)
                {
                    offset.x = offset.z = 0.0f;
                }
                else if (SelectedAxis == RuntimeHandleAxis.Z)
                {
                    offset.x = offset.y = 0.0f;
                }
                if (LockObject.PositionX)
                {
                    offset.x = 0.0f;
                }
                if(LockObject.PositionY)
                {
                    offset.y = 0.0f;
                }
                if(LockObject.PositionZ)
                {
                    offset.z = 0.0f;
                }

                if (EffectiveGridUnitSize == 0.0)
                {
                    offset = m_matrix.MultiplyVector(offset).normalized * mag;
                    transform.position += offset;
                    m_prevPoint = point;
                }
                else
                {
                    offset = m_matrix.MultiplyVector(offset).normalized * mag;
                    m_cursorPosition += offset;
                    Vector3 toCurrentPosition = m_cursorPosition - m_currentPosition;
                    Vector3 gridOffset = Vector3.zero;
                    if (Mathf.Abs(toCurrentPosition.x * 1.5f) >= EffectiveGridUnitSize)
                    {
                        gridOffset.x = EffectiveGridUnitSize * Mathf.Sign(toCurrentPosition.x); 
                    }

                    if (Mathf.Abs(toCurrentPosition.y * 1.5f) >= EffectiveGridUnitSize)
                    {
                        gridOffset.y = EffectiveGridUnitSize * Mathf.Sign(toCurrentPosition.y);
                    }


                    if (Mathf.Abs(toCurrentPosition.z * 1.5f) >= EffectiveGridUnitSize)
                    {
                        gridOffset.z = EffectiveGridUnitSize * Mathf.Sign(toCurrentPosition.z);
                    }
                  
                    m_currentPosition += gridOffset;
                    HandlePosition = m_currentPosition;
                    m_prevPoint = point;
                }
            }
        }

        protected override void DrawOverride()
        {
            RuntimeHandles.DoPositionHandle(HandlePosition, Rotation, SelectedAxis, IsInSnappingMode, LockObject);
        }
    }

}

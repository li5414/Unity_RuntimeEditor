using UnityEngine;
using UnityEngine.EventSystems;

using Battlehub.RTCommon;
namespace Battlehub.RTHandles
{
    public class ScaleHandle : BaseHandle
    {
        public float GridSize = 0.1f;
        private Vector3 m_prevPoint;
        private Matrix4x4 m_matrix;
        private Matrix4x4 m_inverse;

        private Vector3 m_roundedScale;
        private Vector3 m_scale;
        private Vector3[] m_refScales;
        private float m_screenScale;

        protected override RuntimeTool Tool
        {
            get { return RuntimeTool.Scale; }
        }

        protected override float CurrentGridUnitSize
        {
            get { return GridSize; }
        }

        protected override void AwakeOverride()
        {
            m_scale = Vector3.one;
            m_roundedScale = m_scale;
        }

        protected override void OnDestroyOverride()
        {
            base.OnDestroyOverride();
        }

        protected override void UpdateOverride()
        {
            base.UpdateOverride();

            if(HightlightOnHover && !IsDragging)
            {
                if (RuntimeTools.IsPointerOverGameObject())
                {
                    return;
                }
                SelectedAxis = Hit();
            }
        }


        private RuntimeHandleAxis Hit()
        {
            m_screenScale = RuntimeHandles.GetScreenScale(transform.position, SceneCamera) * RuntimeHandles.HandleScale;
            m_matrix = Matrix4x4.TRS(transform.position, Rotation, Vector3.one);
            m_inverse = m_matrix.inverse;

            Matrix4x4 matrix = Matrix4x4.TRS(transform.position, Rotation, new Vector3(m_screenScale, m_screenScale, m_screenScale));

            if (HitCenter())
            {
                return RuntimeHandleAxis.Free;
            }
            float distToYAxis;
            float distToZAxis;
            float distToXAxis;
            bool hit = HitAxis(Vector3.up, matrix, out distToYAxis);
            hit |= HitAxis(Vector3.forward, matrix, out distToZAxis);
            hit |= HitAxis(Vector3.right, matrix, out distToXAxis);

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
            
            return RuntimeHandleAxis.None;
        }

        protected override bool OnBeginDrag()
        {
            SelectedAxis = Hit();

            if(SelectedAxis == RuntimeHandleAxis.Free)
            {
                DragPlane = GetDragPlane();
            }
            else if(SelectedAxis == RuntimeHandleAxis.None)
            {
                return false;
            }

            m_refScales = new Vector3[ActiveTargets.Length];
            for (int i = 0; i < m_refScales.Length; ++i)
            {
                Quaternion rotation = RuntimeTools.PivotRotation == RuntimePivotRotation.Global ? ActiveTargets[i].rotation : Quaternion.identity;
                m_refScales[i] = rotation * ActiveTargets[i].localScale;
            }

            DragPlane = GetDragPlane();
            bool result = GetPointOnDragPlane(Input.mousePosition, out m_prevPoint);
            if(!result)
            {
                SelectedAxis = RuntimeHandleAxis.None;
            }
            return result;
        }

        protected override void OnDrag()
        {
            Vector3 point;
            if (GetPointOnDragPlane(Input.mousePosition, out point))
            {
                Vector3 offset = m_inverse.MultiplyVector((point - m_prevPoint) / m_screenScale);
                float mag = offset.magnitude;
                if (SelectedAxis == RuntimeHandleAxis.X)
                {
                    offset.y = offset.z = 0.0f;

                    if (!LockObject.ScaleX)
                    {
                        m_scale.x += Mathf.Sign(offset.x) * mag;
                    }
                }
                else if (SelectedAxis == RuntimeHandleAxis.Y)
                {
                    offset.x = offset.z = 0.0f;
                    if(!LockObject.ScaleY)
                    {
                        m_scale.y += Mathf.Sign(offset.y) * mag;
                    }
                }
                else if(SelectedAxis == RuntimeHandleAxis.Z)
                {
                    offset.x = offset.y = 0.0f;
                    if(!LockObject.ScaleZ)
                    {
                        m_scale.z += Mathf.Sign(offset.z) * mag;
                    }
                }
                if(SelectedAxis == RuntimeHandleAxis.Free)
                {
                    float sign = Mathf.Sign(offset.x + offset.y);

                    if(!LockObject.ScaleX)
                    {
                        m_scale.x += sign * mag;
                    }
                    
                    if(!LockObject.ScaleY)
                    {
                        m_scale.y += sign * mag;
                    }
                    
                    if(!LockObject.ScaleZ)
                    {
                        m_scale.z += sign * mag;
                    }
                }

                m_roundedScale = m_scale;

                if(EffectiveGridUnitSize > 0.01)
                {
                    m_roundedScale.x = Mathf.RoundToInt(m_roundedScale.x / EffectiveGridUnitSize) * EffectiveGridUnitSize;
                    m_roundedScale.y = Mathf.RoundToInt(m_roundedScale.y / EffectiveGridUnitSize) * EffectiveGridUnitSize;
                    m_roundedScale.z = Mathf.RoundToInt(m_roundedScale.z / EffectiveGridUnitSize) * EffectiveGridUnitSize;
                }

                for (int i = 0; i < m_refScales.Length; ++i)
                {
                    Quaternion rotation =  RuntimeTools.PivotRotation == RuntimePivotRotation.Global ? Targets[i].rotation : Quaternion.identity;
                    
                    ActiveTargets[i].localScale = Quaternion.Inverse(rotation) * Vector3.Scale(m_refScales[i], m_roundedScale);
                }
                
                m_prevPoint = point;
            }
        }

        protected override void OnDrop()
        {
            m_scale = Vector3.one;
            m_roundedScale = m_scale;
        }

        protected override void DrawOverride()
        {
            RuntimeHandles.DoScaleHandle(m_roundedScale, Target.position, Rotation,  SelectedAxis, LockObject);
        }
    }
}
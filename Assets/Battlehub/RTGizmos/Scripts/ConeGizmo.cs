using UnityEngine;
using System;
using Battlehub.RTCommon;

namespace Battlehub.RTGizmos
{
    public abstract class ConeGizmo : BaseGizmo
    {
        protected abstract float Radius
        {
            get;
            set;
        }
           
        protected abstract float Height
        {
            get;
            set;
        }

        private Vector3 Scale
        {
            get { return new Vector3(Mathf.Max(Mathf.Abs(Radius), 0.001f), Mathf.Max(Mathf.Abs(Radius), 0.001f), 1); }
        }

        protected override Matrix4x4 HandlesTransform
        {
            get { return Matrix4x4.TRS(Target.TransformPoint(Vector3.forward * Height), Target.rotation, Scale); }
        }

        private Vector3[] m_coneHandlesPositions;
        protected override Vector3[] HandlesPositions
        {
            get { return m_coneHandlesPositions; }
        }

        private Vector3[] m_coneHandesNormals;
        protected override Vector3[] HandlesNormals
        {
            get { return m_coneHandesNormals; }
        }

        protected override void AwakeOverride()
        {
            base.AwakeOverride();
            m_coneHandlesPositions = RuntimeGizmos.GetConeHandlesPositions();
            m_coneHandesNormals = RuntimeGizmos.GetConeHandlesNormals();
        }

        protected override bool OnDrag(int index, Vector3 offset)
        {
            float sign = Math.Sign(Vector3.Dot(offset.normalized, HandlesNormals[index]));
            if (index == 0)
            {
                Height += offset.magnitude * sign;
            }
            else
            {
                Radius += offset.magnitude * sign;
            }
            
            return true;
        }

        protected override bool HitOverride(int index, Vector3 vertex, Vector3 normal)
        {
            if(index == 0 && Mathf.Abs(Radius) < 0.0001f)
            {
                return false;
            }
            return true;
        }


        protected override void DrawOverride()
        {
            base.DrawOverride();

            
            RuntimeGizmos.DrawConeHandles(Target.TransformPoint(Vector3.forward * Height), Target.rotation, Scale, HandlesColor);
            RuntimeGizmos.DrawWireConeGL(Height, Radius, Target.position, Target.rotation, Vector3.one, LineColor);

            if (IsDragging)
            {
                RuntimeGizmos.DrawSelection(Target.TransformPoint(Vector3.forward * Height + Vector3.Scale(HandlesPositions[DragIndex], Scale)), Target.rotation, Scale, SelectionColor);
            }
        }
    }

}

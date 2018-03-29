using System;
using Battlehub.RTCommon;
using UnityEngine;
namespace Battlehub.RTGizmos
{
    public abstract class SphereGizmo : BaseGizmo
    {
        protected abstract Vector3 Center
        {
            get;
            set;
        }

        protected abstract float Radius
        {
            get;
            set;
        }

        protected override Matrix4x4 HandlesTransform
        {
            get
            {
                return Matrix4x4.TRS(Target.TransformPoint(Center), Target.rotation, Target.localScale * Radius);
            }
        }

        protected override bool OnDrag(int index, Vector3 offset)
        {
            Radius += offset.magnitude * Math.Sign(Vector3.Dot(offset, HandlesNormals[index]));
            if(Radius < 0)
            {
                Radius = 0;
                return false;
            }
            return true;
        }

        protected override void DrawOverride()
        {
            base.DrawOverride();

            if(Target == null)
            {
                return;
            }

            Vector3 scale = Target.localScale * Radius;
            RuntimeGizmos.DrawCubeHandles(Target.TransformPoint(Center) , Target.rotation, scale , HandlesColor);
            RuntimeGizmos.DrawWireSphereGL(Target.TransformPoint(Center), Target.rotation, scale, LineColor);

            if (IsDragging)
            {
                RuntimeGizmos.DrawSelection(Target.TransformPoint(Center + Vector3.Scale(HandlesPositions[DragIndex], scale)), Target.rotation, scale, SelectionColor);
            }
        }
    }

}

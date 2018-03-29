using System;
using Battlehub.RTCommon;
using UnityEngine;
namespace Battlehub.RTGizmos
{
   
    public abstract class CapsuleGizmo : BaseGizmo
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
        protected abstract float Height
        {
            get;
            set;
        }
        protected abstract int Direction
        {
            get;
            set;
        }
        
        protected override Matrix4x4 HandlesTransform
        {
            get
            {
                return Matrix4x4.TRS(Target.TransformPoint(Center), Target.rotation, GetHandlesScale());
            }
        }

        protected override bool OnDrag(int index, Vector3 offset)
        {
            Vector3 axis;
            if(Direction == 0)
            {
                axis = Vector3.right;
            }
            else if(Direction == 1)
            {
                axis = Vector3.up;
            }
            else
            {
                axis = Vector3.forward;
            }

            if (Mathf.Abs(Vector3.Dot(offset.normalized, axis)) > 0.99f)
            {
                float sign = Math.Sign(Vector3.Dot(offset.normalized, HandlesNormals[index]));

                Height += 2 * offset.magnitude * sign;
                if(Height < 0)
                {
                    Height = 0;
                    return false;
                }
            }
            else
            {
                float maxHs = GetMaxHorizontalScale();
                Radius += (Vector3.Scale(offset, Target.localScale).magnitude / maxHs) * Mathf.Sign(Vector3.Dot(offset, HandlesNormals[index]));
                
                if(Radius < 0)
                {
                    Radius = 0;
                    return false;
                }
            }
            return true;
        }

        protected override void DrawOverride()
        {
            base.DrawOverride();

           
            float hs = GetMaxHorizontalScale();
            Vector3 scale = GetHandlesScale();
            RuntimeGizmos.DrawCubeHandles(Target.TransformPoint(Center), Target.rotation,

                GetHandlesScale(),
                
                HandlesColor);
           
            RuntimeGizmos.DrawWireCapsuleGL(Direction, 
                
                GetHeight(), 

                Radius, Target.TransformPoint(Center), Target.rotation,

                new Vector3(hs, hs, hs), LineColor);

            if (IsDragging)
            {
                RuntimeGizmos.DrawSelection(Target.TransformPoint(Center + Vector3.Scale(HandlesPositions[DragIndex], scale)), Target.rotation, scale, SelectionColor);
            }
        }

        private float GetHeight()
        {
            float s;
            float hs = GetMaxHorizontalScale();
            if (Direction == 0)
            {
                s = Target.localScale.x;
            }
            else if (Direction == 1)
            {
                s = Target.localScale.y;
            }
            else
            {
                s = Target.localScale.z;
            }

            return Height * s / hs;
        }

        private Vector3 GetHandlesScale()
        {
            float x;
            float y;
            float z;
            float hs = GetMaxHorizontalScale();
            if (Direction == 0)
            {
                x = GetHandlesHeight();
                y = hs * Radius; 
                z = hs * Radius;
            }
            else if (Direction == 1)
            {
                x = hs * Radius;
                y = GetHandlesHeight();
                z = hs * Radius;
            }
            else
            {
                x = hs * Radius;
                y = hs * Radius;
                z = GetHandlesHeight();
            }

            const float min = 0.001f;
            if(x < min && x > -min)
            {
                x = 0.001f;
            }
            if (y < min && y > -min)
            {
                y = 0.001f;
            }
            if (z < min && z > -min)
            {
                z = 0.001f;
            }
            return new Vector3(x, y, z);
        }

        private float GetHandlesHeight()
        {
            if (Direction == 0)
            {
                return MaxAbs(Target.localScale.x * Height / 2, Radius * GetMaxHorizontalScale());
            }
            else if (Direction == 1)
            {
                return MaxAbs(Target.localScale.y * Height / 2, Radius * GetMaxHorizontalScale());
            }

            return MaxAbs(Target.localScale.z * Height / 2, Radius * GetMaxHorizontalScale());

        }

        private float GetMaxHorizontalScale()
        {
            if(Direction == 0)
            {
                return MaxAbs(Target.localScale.y, Target.localScale.z);
            }
            else if(Direction == 1)
            {
                return MaxAbs(Target.localScale.x, Target.localScale.z);
            }

            return MaxAbs(Target.localScale.x, Target.localScale.y);
        }

        private float MaxAbs(float a, float b)
        {
            if (Math.Abs(a) > Math.Abs(b))
            {
                return a;
            }
            return b;
        }

  

    }

}

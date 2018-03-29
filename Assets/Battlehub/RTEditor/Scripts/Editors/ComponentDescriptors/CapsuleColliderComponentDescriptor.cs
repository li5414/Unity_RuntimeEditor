using UnityEngine;
using System.Reflection;
using System;
using Battlehub.Utils;
using Battlehub.RTGizmos;

namespace Battlehub.RTEditor
{
    public class CapsuleColliderPropertyConverter 
    {
        public enum CapsuleColliderDirection
        {
            X,
            Y,
            Z
        }

        public CapsuleColliderDirection Direction
        {
            get
            {
                if(Component == null)
                {
                    return CapsuleColliderDirection.X;
                }

                return (CapsuleColliderDirection)Component.direction;
            }
            set
            {
                if (Component == null)
                {
                    return;
                }
                Component.direction = (int)value;
            }
        }

        public CapsuleCollider Component
        {
            get;
            set;
        }
    }

    public class CapsuleColliderComponentDescriptor : IComponentDescriptor
    {
        public string DisplayName
        {
            get { return ComponentType.Name; }
        }

        public Type ComponentType
        {
            get { return typeof(CapsuleCollider); }
        }

        public Type GizmoType
        {
            get { return typeof(CapsuleColliderGizmo); }
        }

        public object CreateConverter(ComponentEditor editor)
        {
            CapsuleColliderPropertyConverter converter = new CapsuleColliderPropertyConverter();
            converter.Component = (CapsuleCollider)editor.Component;
            return converter;
        }

        public PropertyDescriptor[] GetProperties(ComponentEditor editor, object converterObj)
        {
            CapsuleColliderPropertyConverter converter = (CapsuleColliderPropertyConverter)converterObj;

            MemberInfo isTriggerInfo = Strong.MemberInfo((CapsuleCollider x) => x.isTrigger);
            MemberInfo materialInfo = Strong.MemberInfo((CapsuleCollider x) => x.sharedMaterial);
            MemberInfo centerInfo = Strong.MemberInfo((CapsuleCollider x) => x.center);
            MemberInfo radiusInfo = Strong.MemberInfo((CapsuleCollider x) => x.radius);
            MemberInfo heightInfo = Strong.MemberInfo((CapsuleCollider x) => x.height);
            MemberInfo directionInfo = Strong.MemberInfo((CapsuleCollider x) => x.direction);
            MemberInfo directionConvertedInfo = Strong.MemberInfo((CapsuleColliderPropertyConverter x) => x.Direction);

            return new[]
            {
                new PropertyDescriptor("Is Trigger", editor.Component, isTriggerInfo, isTriggerInfo),
                new PropertyDescriptor("Material", editor.Component, materialInfo, materialInfo),
                new PropertyDescriptor("Center", editor.Component, centerInfo, centerInfo),
                new PropertyDescriptor("Radius", editor.Component, radiusInfo, radiusInfo),
                new PropertyDescriptor("Height", editor.Component, heightInfo, heightInfo),
                new PropertyDescriptor("Direction", converter, directionConvertedInfo, directionInfo),
            };
        }
    }
}


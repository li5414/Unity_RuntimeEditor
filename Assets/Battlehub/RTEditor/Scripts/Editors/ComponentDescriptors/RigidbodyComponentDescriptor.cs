using UnityEngine;
using System.Reflection;
using System;
using Battlehub.Utils;

namespace Battlehub.RTEditor
{
    public class RigidbodyComponentDescriptor : IComponentDescriptor
    {
        public string DisplayName
        {
            get { return ComponentType.Name; }
        }

        public Type ComponentType
        {
            get { return typeof(Rigidbody); }
        }

        public Type GizmoType
        {
            get { return null; }
        }

        public object CreateConverter(ComponentEditor editor)
        {
            return null;
        }

        public PropertyDescriptor[] GetProperties(ComponentEditor editor, object converter)
        {
            MemberInfo massInfo = Strong.MemberInfo((Rigidbody x) => x.mass);
            MemberInfo dragInfo = Strong.MemberInfo((Rigidbody x) => x.drag);
            MemberInfo angularDragInfo = Strong.MemberInfo((Rigidbody x) => x.angularDrag);
            MemberInfo useGravityInfo = Strong.MemberInfo((Rigidbody x) => x.useGravity);
            MemberInfo isKinematicInfo = Strong.MemberInfo((Rigidbody x) => x.isKinematic);
            MemberInfo interpolationInfo = Strong.MemberInfo((Rigidbody x) => x.interpolation);
            MemberInfo collisionDetectionInfo = Strong.MemberInfo((Rigidbody x) => x.collisionDetectionMode);

            return new[]
            {
                new PropertyDescriptor("Mass", editor.Component, massInfo, massInfo),
                new PropertyDescriptor("Drag", editor.Component, dragInfo, dragInfo),
                new PropertyDescriptor("Angular Drag", editor.Component, angularDragInfo, angularDragInfo),
                new PropertyDescriptor("Use Gravity", editor.Component, useGravityInfo, useGravityInfo),
                new PropertyDescriptor("Is Kinematic", editor.Component, isKinematicInfo, isKinematicInfo),
                new PropertyDescriptor("Interpolation", editor.Component, interpolationInfo, interpolationInfo),
                new PropertyDescriptor("Collision Detection", editor.Component, collisionDetectionInfo, collisionDetectionInfo),
            };
        }
    }

}


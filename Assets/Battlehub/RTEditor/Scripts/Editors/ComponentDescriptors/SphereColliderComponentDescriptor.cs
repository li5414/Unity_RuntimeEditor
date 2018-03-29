using UnityEngine;
using System.Reflection;
using System;
using Battlehub.Utils;
using Battlehub.RTGizmos;

namespace Battlehub.RTEditor
{
    public class SphereColliderComponentDescriptor : IComponentDescriptor
    {
        public string DisplayName
        {
            get { return ComponentType.Name; }
        }

        public Type ComponentType
        {
            get { return typeof(SphereCollider); }
        }

        public Type GizmoType
        {
            get { return typeof(SphereColliderGizmo); }
        }

        public object CreateConverter(ComponentEditor editor)
        {
            return null;
        }

        public PropertyDescriptor[] GetProperties(ComponentEditor editor, object converter)
        {
            MemberInfo isTriggerInfo = Strong.MemberInfo((SphereCollider x) => x.isTrigger);
            MemberInfo materialInfo = Strong.MemberInfo((SphereCollider x) => x.sharedMaterial);
            MemberInfo centerInfo = Strong.MemberInfo((SphereCollider x) => x.center);
            MemberInfo radiusInfo = Strong.MemberInfo((SphereCollider x) => x.radius);


            return new[]
            {
                new PropertyDescriptor("Is Trigger", editor.Component, isTriggerInfo, isTriggerInfo),
                new PropertyDescriptor("Material", editor.Component, materialInfo, materialInfo),
                new PropertyDescriptor("Center", editor.Component, centerInfo, centerInfo),
                new PropertyDescriptor("Radius", editor.Component, radiusInfo, radiusInfo),
            };
        }
    }
}


using UnityEngine;
using System.Reflection;
using System;
using Battlehub.Utils;
using Battlehub.RTGizmos;

namespace Battlehub.RTEditor
{
    public class BoxColliderComponentDescriptor : IComponentDescriptor
    {
        public string DisplayName
        {
            get { return ComponentType.Name; }
        }

        public Type ComponentType
        {
            get { return typeof(BoxCollider); }
        }

        public Type GizmoType
        {
            get { return typeof(BoxColliderGizmo); }
        }

        public object CreateConverter(ComponentEditor editor)
        {
            return null;
        }

        public PropertyDescriptor[] GetProperties(ComponentEditor editor, object converter)
        {
            MemberInfo isTriggerInfo = Strong.MemberInfo((BoxCollider x) => x.isTrigger);
            MemberInfo materialInfo = Strong.MemberInfo((BoxCollider x) => x.sharedMaterial);
            MemberInfo centerInfo = Strong.MemberInfo((BoxCollider x) => x.center);
            MemberInfo sizeInfo = Strong.MemberInfo((BoxCollider x) => x.size);

            return new[]
            {
                new PropertyDescriptor("Is Trigger", editor.Component, isTriggerInfo, isTriggerInfo),
                new PropertyDescriptor("Material", editor.Component, materialInfo, materialInfo),
                new PropertyDescriptor("Center", editor.Component, centerInfo, centerInfo),
                new PropertyDescriptor("Size", editor.Component, sizeInfo, sizeInfo),
            };
        }
    }
}


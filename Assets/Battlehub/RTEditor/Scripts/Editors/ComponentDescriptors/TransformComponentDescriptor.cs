using UnityEngine;
using System.Reflection;
using System;
using Battlehub.Utils;
using System.Collections.Generic;

namespace Battlehub.RTEditor
{
    public class TransformComponentDescriptor : IComponentDescriptor
    {
        public string DisplayName
        {
            get { return ComponentType.Name; }
        }

        public Type ComponentType
        {
            get { return typeof(Transform); }
        }

        public Type GizmoType
        {
            get { return null; }
        }

        public object CreateConverter(ComponentEditor editor)
        {
            TransformPropertyConverter converter = new TransformPropertyConverter();
            converter.Component = (Transform)editor.Component;
            return converter;
        }

        public PropertyDescriptor[] GetProperties(ComponentEditor editor, object converterObj)
        {
            TransformPropertyConverter converter = (TransformPropertyConverter)converterObj;

            MemberInfo position = Strong.MemberInfo((Transform x) => x.position);
            MemberInfo rotation = Strong.MemberInfo((Transform x) => x.rotation);
            MemberInfo rotationConverted = Strong.MemberInfo((TransformPropertyConverter x) => x.Rotation);
            MemberInfo scale = Strong.MemberInfo((Transform x) => x.localScale);

            return new[]
                {
                    new PropertyDescriptor( "Position", editor.Component, position, position) ,
                    new PropertyDescriptor( "Rotation", converter, rotationConverted, rotation),
                    new PropertyDescriptor( "Scale", editor.Component, scale, scale)
                };
        }
    }

    public class TransformPropertyConverter 
    {
        public Vector3 Rotation
        {
            get
            {
                if(Component == null)
                {
                    return Vector3.zero;
                }
                return Component.rotation.eulerAngles;
            }
            set
            {
                if (Component == null)
                {
                    return;
                }
                Component.rotation = Quaternion.Euler(value);
            }
        }

        public Transform Component
        {
            get;
            set;
        }
    }
}


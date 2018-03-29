using UnityEngine;
using System.Reflection;
using System;
using Battlehub.Utils;

namespace Battlehub.RTEditor
{
    public class MeshFilterComponentDescriptor : IComponentDescriptor
    {
        public string DisplayName
        {
            get { return ComponentType.Name; }
        }

        public Type ComponentType
        {
            get { return typeof(MeshFilter); }
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
            MemberInfo sharedMeshInfo = Strong.MemberInfo((MeshFilter x) => x.sharedMesh);
            return new[]
            {
                new PropertyDescriptor("Mesh", editor.Component, sharedMeshInfo, sharedMeshInfo)
            };
        }
    }
}


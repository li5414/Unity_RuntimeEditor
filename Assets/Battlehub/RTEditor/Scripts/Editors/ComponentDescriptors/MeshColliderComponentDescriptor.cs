using UnityEngine;
using System.Reflection;
using System;
using Battlehub.Utils;

namespace Battlehub.RTEditor
{
    public class MeshColliderComponentDescriptor : IComponentDescriptor
    {
        public string DisplayName
        {
            get { return ComponentType.Name; }
        }

        public Type ComponentType
        {
            get { return typeof(MeshCollider); }
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
            PropertyEditorCallback valueChanged = () => editor.BuildEditor();

            MemberInfo convexInfo = Strong.MemberInfo((MeshCollider x) => x.convex);
            MemberInfo isTriggerInfo = Strong.MemberInfo((MeshCollider x) => x.isTrigger);
            MemberInfo materialInfo = Strong.MemberInfo((MeshCollider x) => x.sharedMaterial);
            MemberInfo meshInfo = Strong.MemberInfo((MeshCollider x) => x.sharedMesh);


            MeshCollider collider = (MeshCollider)editor.Component;
            if (collider.convex)
            {
                return new[]
                {
                    new PropertyDescriptor("Convex", editor.Component, convexInfo, convexInfo, valueChanged),
                    new PropertyDescriptor("Is Trigger", editor.Component, isTriggerInfo, isTriggerInfo),
                    new PropertyDescriptor("Material", editor.Component, materialInfo, materialInfo),
                    new PropertyDescriptor("Mesh", editor.Component, meshInfo, meshInfo),
                };
            }
            else
            {
                return new[]
                {
                    new PropertyDescriptor("Convex", editor.Component, convexInfo, convexInfo, valueChanged),
                    new PropertyDescriptor("Material", editor.Component, materialInfo, materialInfo),
                    new PropertyDescriptor("Mesh", editor.Component, meshInfo, meshInfo),
                };
            }
        }
    }

}


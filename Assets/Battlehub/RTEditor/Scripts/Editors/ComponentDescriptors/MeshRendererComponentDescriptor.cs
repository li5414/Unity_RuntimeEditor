//#define SIMPLIFIED_MESHRENDERER

using UnityEngine;
using System.Reflection;
using System;

using Battlehub.Utils;
using System.Linq;

namespace Battlehub.RTEditor
{
#if SIMPLIFIED_MESHRENDERER

    public class MeshRendererComponentDescriptor : IComponentDescriptor
    {
        public Type ComponentType
        {
            get { return typeof(MeshRenderer); }
        }

        public object CreateConverter(ComponentEditor editor)
        {
            return null;
        }

        public PropertyDescriptor[] GetProperties(ComponentEditor editor, object converter)
        {
            MemberInfo materials = Strong.MemberInfo((MeshRenderer x) => x.sharedMaterials);
            return new[]
                {
                    new PropertyDescriptor( "Materials", editor.Component, materials, materials),
                };
        }
    }
#else
    public class MeshRendererComponentDescriptor : IComponentDescriptor
    {
        public string DisplayName
        {
            get { return ComponentType.Name; }
        }

        public Type ComponentType
        {
            get { return typeof(MeshRenderer); }
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
            MemberInfo shadowCastingMode = Strong.MemberInfo((MeshRenderer x) => x.shadowCastingMode);
            MemberInfo receiveShadows = Strong.MemberInfo((MeshRenderer x) => x.receiveShadows);
            MemberInfo materials = Strong.MemberInfo((MeshRenderer x) => x.sharedMaterials);
            MemberInfo lightProbes = Strong.MemberInfo((MeshRenderer x) => x.lightProbeUsage);
            MemberInfo reflectionProbes = Strong.MemberInfo((MeshRenderer x) => x.reflectionProbeUsage);
            MemberInfo anchorOverride = Strong.MemberInfo((MeshRenderer x) => x.probeAnchor);

            return new[]
                {
                    new PropertyDescriptor( "Cast Shadows", editor.Component, shadowCastingMode, shadowCastingMode),
                    new PropertyDescriptor( "Receive Shadows", editor.Component, receiveShadows, receiveShadows),
                    new PropertyDescriptor( "Materials", editor.Component, materials, materials),
                    new PropertyDescriptor( "Light Probes", editor.Component, lightProbes, lightProbes),
                    new PropertyDescriptor( "Reflection Probes", editor.Component, reflectionProbes, reflectionProbes),
                    new PropertyDescriptor( "Anchor Override", editor.Component, anchorOverride, anchorOverride),
                };
        }
    }
#endif
}
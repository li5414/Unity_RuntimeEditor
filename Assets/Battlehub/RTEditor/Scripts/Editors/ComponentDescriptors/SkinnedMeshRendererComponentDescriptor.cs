//#define SIMPLIFIED_MESHRENDERER
using UnityEngine;
using System.Reflection;
using System;
using Battlehub.Utils;
using System.Collections.Generic;
using Battlehub.RTGizmos;

namespace Battlehub.RTEditor
{
#if SIMPLIFIED_MESHRENDERER
    public class SkinnedMeshRendererComponentDescriptor : IComponentDescriptor
    {
        public Type ComponentType
        {
            get { return typeof(SkinnedMeshRenderer); }
        }

         public Type GizmoType
        {
            get { return typeof(SkinnedMeshRendererGizmo); }
        }
        public object CreateConverter(ComponentEditor editor)
        {
            return null;
        }

        public PropertyDescriptor[] GetProperties(ComponentEditor editor, object converter)
        {
            MemberInfo materialsInfo = Strong.MemberInfo((SkinnedMeshRenderer x) => x.sharedMaterials);
            List<PropertyDescriptor> descriptors = new List<PropertyDescriptor>();
            descriptors.Add(new PropertyDescriptor("Materials", editor.Component, materialsInfo, materialsInfo));
            return descriptors.ToArray();
        }
    }
#else
    public class SkinnedMeshRendererComponentDescriptor : IComponentDescriptor
    {
        public string DisplayName
        {
            get { return ComponentType.Name; }
        }

        public Type ComponentType
        {
            get { return typeof(SkinnedMeshRenderer); }
        }


        public Type GizmoType
        {
            get { return typeof(SkinnedMeshRendererGizmo); }
        }

        public object CreateConverter(ComponentEditor editor)
        {
            return null;
        }

        public PropertyDescriptor[] GetProperties(ComponentEditor editor, object converter)
        {
            MemberInfo castShadowsInfo = Strong.MemberInfo((SkinnedMeshRenderer x) => x.shadowCastingMode);
            MemberInfo receiveShadowsInfo = Strong.MemberInfo((SkinnedMeshRenderer x) => x.receiveShadows);
            MemberInfo materialsInfo = Strong.MemberInfo((SkinnedMeshRenderer x) => x.sharedMaterials);
            MemberInfo lightProbesInfo = Strong.MemberInfo((SkinnedMeshRenderer x) => x.lightProbeUsage);
            MemberInfo reflectionProbesInfo = Strong.MemberInfo((SkinnedMeshRenderer x) => x.reflectionProbeUsage);
            MemberInfo anchorOverrideInfo = Strong.MemberInfo((SkinnedMeshRenderer x) => x.probeAnchor);
            MemberInfo qualityInfo = Strong.MemberInfo((SkinnedMeshRenderer x) => x.quality);
            MemberInfo updateWhenOffscreenInfo = Strong.MemberInfo((SkinnedMeshRenderer x) => x.updateWhenOffscreen);
            MemberInfo skinnedMotionVectorsInfo = Strong.MemberInfo((SkinnedMeshRenderer x) => x.skinnedMotionVectors);
            MemberInfo meshInfo = Strong.MemberInfo((SkinnedMeshRenderer x) => x.sharedMesh);
            MemberInfo rootBoneInfo = Strong.MemberInfo((SkinnedMeshRenderer x) => x.rootBone);
            MemberInfo boundsInfo = Strong.MemberInfo((SkinnedMeshRenderer x) => x.localBounds);

            List<PropertyDescriptor> descriptors = new List<PropertyDescriptor>();

            descriptors.Add(new PropertyDescriptor("Cast Shadows", editor.Component, castShadowsInfo, castShadowsInfo));
            descriptors.Add(new PropertyDescriptor("Receive Shadows", editor.Component, receiveShadowsInfo, receiveShadowsInfo));
            descriptors.Add(new PropertyDescriptor("Materials", editor.Component, materialsInfo, materialsInfo));
            descriptors.Add(new PropertyDescriptor("Light Probes", editor.Component, lightProbesInfo, lightProbesInfo));
            descriptors.Add(new PropertyDescriptor("Reflection Probes", editor.Component, reflectionProbesInfo, reflectionProbesInfo));
            descriptors.Add(new PropertyDescriptor("Anchor Override", editor.Component, anchorOverrideInfo, anchorOverrideInfo));
            descriptors.Add(new PropertyDescriptor("Quality", editor.Component, qualityInfo, qualityInfo));
            descriptors.Add(new PropertyDescriptor("Update When Offscreen", editor.Component, updateWhenOffscreenInfo, updateWhenOffscreenInfo));
            descriptors.Add(new PropertyDescriptor("Skinned Motion Vectors", editor.Component, skinnedMotionVectorsInfo, skinnedMotionVectorsInfo));
            descriptors.Add(new PropertyDescriptor("Mesh", editor.Component, meshInfo, meshInfo));
            descriptors.Add(new PropertyDescriptor("Root Bone", editor.Component, rootBoneInfo, rootBoneInfo));
            descriptors.Add(new PropertyDescriptor("Bounds", editor.Component, boundsInfo, boundsInfo));

            return descriptors.ToArray();
        }
    }
#endif

}


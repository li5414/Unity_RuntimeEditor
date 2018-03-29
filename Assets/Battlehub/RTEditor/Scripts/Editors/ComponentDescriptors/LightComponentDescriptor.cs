using UnityEngine;
using System.Reflection;
using System;
using Battlehub.Utils;
using System.Collections.Generic;
using Battlehub.RTGizmos;

namespace Battlehub.RTEditor
{
    public class LightComponentDescriptor : IComponentDescriptor
    {
        public string DisplayName
        {
            get { return ComponentType.Name; }
        }

        public Type ComponentType
        {
            get { return typeof(Light); }
        }

        public Type GizmoType
        {
            get { return typeof(LightGizmo); }
        }


        public object CreateConverter(ComponentEditor editor)
        {
            return null;
        }

        public PropertyDescriptor[] GetProperties(ComponentEditor editor, object converter)
        {
            Light light = (Light)editor.Component;

            PropertyEditorCallback valueChanged = () => editor.BuildEditor();

            MemberInfo enabledInfo = Strong.MemberInfo((Light x) => x.enabled);
            MemberInfo lightTypeInfo = Strong.MemberInfo((Light x) => x.type);
            MemberInfo colorInfo = Strong.MemberInfo((Light x) => x.color);
            MemberInfo intensityInfo = Strong.MemberInfo((Light x) => x.intensity);
            MemberInfo bounceIntensityInfo = Strong.MemberInfo((Light x) => x.bounceIntensity);
            MemberInfo shadowTypeInfo = Strong.MemberInfo((Light x) => x.shadows);
            MemberInfo cookieInfo = Strong.MemberInfo((Light x) => x.cookie);
            MemberInfo cookieSizeInfo = Strong.MemberInfo((Light x) => x.cookieSize);
            MemberInfo flareInfo = Strong.MemberInfo((Light x) => x.flare);
            MemberInfo renderModeInfo = Strong.MemberInfo((Light x) => x.renderMode);

            List<PropertyDescriptor> descriptors = new List<PropertyDescriptor>();
            descriptors.Add(new PropertyDescriptor("Enabled", editor.Component, enabledInfo, enabledInfo));
            descriptors.Add(new PropertyDescriptor("Type", editor.Component, lightTypeInfo, lightTypeInfo, valueChanged));
            if (light.type == LightType.Point)
            {
                MemberInfo rangeInfo = Strong.MemberInfo((Light x) => x.range);
                descriptors.Add(new PropertyDescriptor("Range", editor.Component, rangeInfo, rangeInfo));
            }
            else if (light.type == LightType.Spot)
            {
                MemberInfo rangeInfo = Strong.MemberInfo((Light x) => x.range);
                MemberInfo spotAngleInfo = Strong.MemberInfo((Light x) => x.spotAngle);
                descriptors.Add(new PropertyDescriptor("Range", editor.Component, rangeInfo, rangeInfo));
                descriptors.Add(new PropertyDescriptor("Spot Angle", editor.Component, spotAngleInfo, spotAngleInfo, null, new Range(1, 179)));
            }

            descriptors.Add(new PropertyDescriptor("Color", editor.Component, colorInfo, colorInfo));
            descriptors.Add(new PropertyDescriptor("Intensity", editor.Component, intensityInfo, intensityInfo, null, new Range(0, 8)));
            descriptors.Add(new PropertyDescriptor("Bounce Intensity", editor.Component, bounceIntensityInfo, bounceIntensityInfo, null, new Range(0, 8)));

            if (light.type != LightType.Area)
            {
                descriptors.Add(new PropertyDescriptor("Shadow Type", editor.Component, shadowTypeInfo, shadowTypeInfo, valueChanged));
                if (light.shadows == LightShadows.Soft || light.shadows == LightShadows.Hard)
                {
                    MemberInfo shadowStrengthInfo = Strong.MemberInfo((Light x) => x.shadowStrength);
                    MemberInfo shadowResolutionInfo = Strong.MemberInfo((Light x) => x.shadowResolution);
                    MemberInfo shadowBiasInfo = Strong.MemberInfo((Light x) => x.shadowBias);
                    MemberInfo shadowNormalBiasInfo = Strong.MemberInfo((Light x) => x.shadowNormalBias);
                    MemberInfo shadowNearPlaneInfo = Strong.MemberInfo((Light x) => x.shadowNearPlane);

                    descriptors.Add(new PropertyDescriptor("Strength", editor.Component, shadowStrengthInfo, shadowStrengthInfo, null, new Range(0, 1)));
                    descriptors.Add(new PropertyDescriptor("Resoultion", editor.Component, shadowResolutionInfo, shadowResolutionInfo));
                    descriptors.Add(new PropertyDescriptor("Bias", editor.Component, shadowBiasInfo, shadowBiasInfo, null, new Range(0, 2)));
                    descriptors.Add(new PropertyDescriptor("Normal Bias", editor.Component, shadowNormalBiasInfo, shadowNormalBiasInfo, null, new Range(0, 3)));
                    descriptors.Add(new PropertyDescriptor("Shadow Near Plane", editor.Component, shadowNearPlaneInfo, shadowNearPlaneInfo, null, new Range(0, 10)));
                }

                descriptors.Add(new PropertyDescriptor("Cookie", editor.Component, cookieInfo, cookieInfo));
                descriptors.Add(new PropertyDescriptor("Cookie Size", editor.Component, cookieSizeInfo, cookieSizeInfo));
            }

            descriptors.Add(new PropertyDescriptor("Flare", editor.Component, flareInfo, flareInfo));
            descriptors.Add(new PropertyDescriptor("Render Mode", editor.Component, renderModeInfo, renderModeInfo));

            return descriptors.ToArray();
        }
    }
}


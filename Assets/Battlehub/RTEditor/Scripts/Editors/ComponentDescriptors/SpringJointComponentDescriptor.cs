using Battlehub.Utils;
using System;
using System.Reflection;
using UnityEngine;

namespace Battlehub.RTEditor
{
    public class SpringJointComponentDescriptor : IComponentDescriptor
    {
        public string DisplayName
        {
            get { return ComponentType.Name; }
        }

        public Type ComponentType
        {
            get { return typeof(SpringJoint); }
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
           
            MemberInfo connectedBodyInfo = Strong.MemberInfo((SpringJoint x) => x.connectedBody);
            MemberInfo anchorInfo = Strong.MemberInfo((SpringJoint x) => x.anchor);
            MemberInfo autoConfigAnchorInfo = Strong.MemberInfo((SpringJoint x) => x.autoConfigureConnectedAnchor);
            MemberInfo connectedAnchorInfo = Strong.MemberInfo((SpringJoint x) => x.connectedAnchor);
            MemberInfo springInfo = Strong.MemberInfo((SpringJoint x) => x.spring);
            MemberInfo damperInfo = Strong.MemberInfo((SpringJoint x) => x.damper);
            MemberInfo minDistanceInfo = Strong.MemberInfo((SpringJoint x) => x.minDistance);
            MemberInfo maxDistanceInfo = Strong.MemberInfo((SpringJoint x) => x.maxDistance);
            MemberInfo toleranceInfo = Strong.MemberInfo((SpringJoint x) => x.tolerance);
            MemberInfo breakForceInfo = Strong.MemberInfo((SpringJoint x) => x.breakForce);
            MemberInfo breakTorqueInfo = Strong.MemberInfo((SpringJoint x) => x.breakTorque);
            MemberInfo enableCollisionInfo = Strong.MemberInfo((SpringJoint x) => x.enableCollision);
            MemberInfo enablePreporcessingInfo = Strong.MemberInfo((SpringJoint x) => x.enablePreprocessing);

            return new[]
            {
                new PropertyDescriptor("ConnectedBody", editor.Component, connectedBodyInfo),
                new PropertyDescriptor("Anchor", editor.Component, anchorInfo),
                new PropertyDescriptor("Auto Configure Connected Anchor", editor.Component, autoConfigAnchorInfo),
                new PropertyDescriptor("Connected Anchor", editor.Component, connectedAnchorInfo),
                new PropertyDescriptor("Spring", editor.Component, springInfo),
                new PropertyDescriptor("Damper", editor.Component, damperInfo),
                new PropertyDescriptor("MinDistance", editor.Component, minDistanceInfo),
                new PropertyDescriptor("MaxDistance", editor.Component, maxDistanceInfo),
                new PropertyDescriptor("Tolerance", editor.Component, toleranceInfo),
                new PropertyDescriptor("Break Force", editor.Component, breakForceInfo),
                new PropertyDescriptor("Break Torque", editor.Component, breakTorqueInfo),
                new PropertyDescriptor("Enable Collision", editor.Component, enableCollisionInfo),
                new PropertyDescriptor("Enable Preprocessing", editor.Component, enablePreporcessingInfo),
            };            
        }
    }
}

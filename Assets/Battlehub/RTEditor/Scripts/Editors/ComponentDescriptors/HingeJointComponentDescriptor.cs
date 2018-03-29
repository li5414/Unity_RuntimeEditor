using Battlehub.Utils;
using System;
using System.Reflection;
using UnityEngine;

namespace Battlehub.RTEditor
{
    public class HingeJointComponentDescriptor : IComponentDescriptor
    {
        public string DisplayName
        {
            get { return ComponentType.Name; }
        }

        public Type ComponentType
        {
            get { return typeof(HingeJoint); }
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
            MemberInfo connectedBodyInfo = Strong.MemberInfo((HingeJoint x) => x.connectedBody);
            MemberInfo anchorInfo = Strong.MemberInfo((HingeJoint x) => x.anchor);
            MemberInfo axisInfo = Strong.MemberInfo((HingeJoint x) => x.axis);
            MemberInfo autoConfigAnchorInfo = Strong.MemberInfo((HingeJoint x) => x.autoConfigureConnectedAnchor);
            MemberInfo connectedAnchorInfo = Strong.MemberInfo((HingeJoint x) => x.connectedAnchor);
            MemberInfo useSpringInfo = Strong.MemberInfo((HingeJoint x) => x.useSpring);
            MemberInfo springInfo = Strong.MemberInfo((HingeJoint x) => x.spring);
            MemberInfo useMotorInfo = Strong.MemberInfo((HingeJoint x) => x.useMotor);
            MemberInfo motorInfo = Strong.MemberInfo((HingeJoint x) => x.motor);
            MemberInfo useLimitsInfo = Strong.MemberInfo((HingeJoint x) => x.useLimits);
            MemberInfo limitsInfo = Strong.MemberInfo((HingeJoint x) => x.limits);
            MemberInfo breakForceInfo = Strong.MemberInfo((HingeJoint x) => x.breakForce);
            MemberInfo breakTorqueInfo = Strong.MemberInfo((HingeJoint x) => x.breakTorque);
            MemberInfo enableCollisionInfo = Strong.MemberInfo((HingeJoint x) => x.enableCollision);
            MemberInfo enablePreporcessingInfo = Strong.MemberInfo((HingeJoint x) => x.enablePreprocessing);

            return new[]
            {
                new PropertyDescriptor("ConnectedBody", editor.Component, connectedBodyInfo),
                new PropertyDescriptor("Anchor", editor.Component, anchorInfo),
                new PropertyDescriptor("Axis", editor.Component, axisInfo),
                new PropertyDescriptor("Auto Configure Connected Anchor", editor.Component, autoConfigAnchorInfo),
                new PropertyDescriptor("Connected Anchor", editor.Component, connectedAnchorInfo),
                new PropertyDescriptor("Use Spring", editor.Component, useSpringInfo),
                new PropertyDescriptor("Spring", editor.Component, springInfo),
                new PropertyDescriptor("Use Motor", editor.Component, useMotorInfo),
                new PropertyDescriptor("Motor", editor.Component, motorInfo),
                new PropertyDescriptor("Use Limits", editor.Component, useLimitsInfo),
                new PropertyDescriptor("Limits", editor.Component, limitsInfo)
                {
                    ChildDesciptors = new[]
                    {
                        new PropertyDescriptor("Min", null, Strong.MemberInfo((JointLimits x) => x.min)),
                        new PropertyDescriptor("Max", null, Strong.MemberInfo((JointLimits x) => x.max)),
                        new PropertyDescriptor("Bounciness", null, Strong.MemberInfo((JointLimits x) => x.bounciness)),
                        new PropertyDescriptor("Bounce Min Velocity", null, Strong.MemberInfo((JointLimits x) => x.bounceMinVelocity)),
                        new PropertyDescriptor("Contact Distance", null, Strong.MemberInfo((JointLimits x) => x.contactDistance)),
                    }
                },
                new PropertyDescriptor("Break Force", editor.Component, breakForceInfo),
                new PropertyDescriptor("Break Torque", editor.Component, breakTorqueInfo),
                new PropertyDescriptor("Enable Collision", editor.Component, enableCollisionInfo),
                new PropertyDescriptor("Enable Preprocessing", editor.Component, enablePreporcessingInfo),
            };            
        }
    }
}

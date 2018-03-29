using Battlehub.Utils;
using System;
using System.Reflection;
using UnityEngine;

namespace Battlehub.RTEditor
{
    public class FixedJointComponentDescriptor : IComponentDescriptor
    {
        public string DisplayName
        {
            get { return ComponentType.Name; }
        }

        public Type ComponentType
        {
            get { return typeof(FixedJoint); }
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
            MemberInfo breakForceInfo = Strong.MemberInfo((SpringJoint x) => x.breakForce);
            MemberInfo breakTorqueInfo = Strong.MemberInfo((SpringJoint x) => x.breakTorque);
            MemberInfo enableCollisionInfo = Strong.MemberInfo((SpringJoint x) => x.enableCollision);
            MemberInfo enablePreporcessingInfo = Strong.MemberInfo((SpringJoint x) => x.enablePreprocessing);

            return new[]
            {
                new PropertyDescriptor("ConnectedBody", editor.Component, connectedBodyInfo),
                new PropertyDescriptor("Break Force", editor.Component, breakForceInfo),
                new PropertyDescriptor("Break Torque", editor.Component, breakTorqueInfo),
                new PropertyDescriptor("Enable Collision", editor.Component, enableCollisionInfo),
                new PropertyDescriptor("Enable Preprocessing", editor.Component, enablePreporcessingInfo),
            };            
        }
    }
}

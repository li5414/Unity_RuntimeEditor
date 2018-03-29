using Battlehub.RTCommon;
using Battlehub.Utils;
using UnityEngine;

namespace Battlehub.RTEditor
{
    public class TransformEditor : ComponentEditor
    {
        protected override void InitEditor(PropertyEditor editor, PropertyDescriptor descriptor)
        {
            base.InitEditor(editor, descriptor);

            if(RuntimeTools.LockAxes == null)
            {
                return;
            }

            if(descriptor.ComponentMemberInfo == Strong.MemberInfo((Transform x) => x.position))
            {
                Vector3Editor vector3Editor = (Vector3Editor)editor;
                vector3Editor.IsXInteractable = !RuntimeTools.LockAxes.PositionX;
                vector3Editor.IsYInteractable = !RuntimeTools.LockAxes.PositionY;
                vector3Editor.IsZInteractable = !RuntimeTools.LockAxes.PositionZ;
            }

            if (descriptor.ComponentMemberInfo == Strong.MemberInfo((Transform x) => x.rotation))
            {
                Vector3Editor vector3Editor = (Vector3Editor)editor;
                vector3Editor.IsXInteractable = !RuntimeTools.LockAxes.RotationX;
                vector3Editor.IsYInteractable = !RuntimeTools.LockAxes.RotationY;
                vector3Editor.IsZInteractable = !RuntimeTools.LockAxes.RotationZ;
            }

            if (descriptor.ComponentMemberInfo == Strong.MemberInfo((Transform x) => x.localScale))
            {
                Vector3Editor vector3Editor = (Vector3Editor)editor;
                vector3Editor.IsXInteractable = !RuntimeTools.LockAxes.ScaleX;
                vector3Editor.IsYInteractable = !RuntimeTools.LockAxes.ScaleY;
                vector3Editor.IsZInteractable = !RuntimeTools.LockAxes.ScaleZ;
            }
        }
    }
}


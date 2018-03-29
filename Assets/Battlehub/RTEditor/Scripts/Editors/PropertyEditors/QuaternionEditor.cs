using UnityEngine;

namespace Battlehub.RTEditor
{
    public class QuaternionEditor : FourFloatEditor<Quaternion>
    {
        protected override float GetW(Quaternion v)
        {
            return v.w;
        }

        protected override float GetX(Quaternion v)
        {
            return v.x;
        }

        protected override float GetY(Quaternion v)
        {
            return v.y;
        }

        protected override float GetZ(Quaternion v)
        {
            return v.z;
        }

        protected override Quaternion SetW(Quaternion v, float w)
        {
            v.w = w;
            return v;
        }

        protected override Quaternion SetX(Quaternion v, float x)
        {
            v.x = x;
            return v;
        }

        protected override Quaternion SetY(Quaternion v, float y)
        {
            v.y = y;
            return v;
        }

        protected override Quaternion SetZ(Quaternion v, float z)
        {
            v.z = z;
            return v;
        }
    }
}

using UnityEngine;

namespace Battlehub.RTEditor
{
    public class Vector4Editor : FourFloatEditor<Vector4>
    {
        protected override float GetW(Vector4 v)
        {
            return v.w;
        }

        protected override float GetX(Vector4 v)
        {
            return v.x;
        }

        protected override float GetY(Vector4 v)
        {
            return v.y;
        }

        protected override float GetZ(Vector4 v)
        {
            return v.z;
        }

        protected override Vector4 SetW(Vector4 v, float w)
        {
            v.w = w;
            return v;
        }

        protected override Vector4 SetX(Vector4 v, float x)
        {
            v.x = x;
            return v;
        }

        protected override Vector4 SetY(Vector4 v, float y)
        {
            v.y = y;
            return v;
        }

        protected override Vector4 SetZ(Vector4 v, float z)
        {
            v.z = z;
            return v;
        }
    }
}


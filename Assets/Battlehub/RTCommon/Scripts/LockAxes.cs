using UnityEngine;
using System.Linq;

namespace Battlehub.RTCommon
{
    public class LockObject
    {
        public bool PositionX;
        public bool PositionY;
        public bool PositionZ;
        public bool RotationX;
        public bool RotationY;
        public bool RotationZ;
        public bool RotationScreen;
        public bool ScaleX;
        public bool ScaleY;
        public bool ScaleZ;

        public bool IsPositionLocked
        {
            get { return PositionX && PositionY && PositionZ; }
        }
    }

    public class LockAxes : MonoBehaviour
    {
        public bool PositionX;
        public bool PositionY;
        public bool PositionZ;
        public bool RotationX;
        public bool RotationY;
        public bool RotationZ;
        public bool RotationScreen;
        public bool ScaleX;
        public bool ScaleY;
        public bool ScaleZ;

        public static LockObject Eval(LockAxes[] lockAxes)
        {
            LockObject lockObject = new LockObject();
            if(lockAxes != null)
            {
                lockObject.PositionX = lockAxes.Any(la => la.PositionX);
                lockObject.PositionY = lockAxes.Any(la => la.PositionY);
                lockObject.PositionZ = lockAxes.Any(la => la.PositionZ);

                lockObject.RotationX = lockAxes.Any(la => la.RotationX);
                lockObject.RotationY = lockAxes.Any(la => la.RotationY);
                lockObject.RotationZ = lockAxes.Any(la => la.RotationZ);
                lockObject.RotationScreen = lockAxes.Any(la => la.RotationScreen);

                lockObject.ScaleX = lockAxes.Any(la => la.ScaleX);
                lockObject.ScaleY = lockAxes.Any(la => la.ScaleY);
                lockObject.ScaleZ = lockAxes.Any(la => la.ScaleZ);
            }

            return lockObject;
        }
    }

}

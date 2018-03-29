using UnityEngine;
using System.Collections;

namespace Battlehub.RTCommon
{
    public delegate void GameCameraEvent();

    public class GameCamera : MonoBehaviour
    {
        public static event GameCameraEvent Awaked;
        public static event GameCameraEvent Destroyed;
        public static event GameCameraEvent Enabled;
        public static event GameCameraEvent Disabled;

        private void Awake()
        {
            if(Awaked != null)
            {
                Awaked();
            }
        }

        private void OnDestroy()
        {
            if(Destroyed != null)
            {
                Destroyed();
            }
        }

        private void OnEnable()
        {
            if(Enabled != null)
            {
                Enabled();
            }
        }

        private void OnDisable()
        {
            if(Disabled != null)
            {
                Disabled();
            }
        }
    }

}

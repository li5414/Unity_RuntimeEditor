//#define USE_CROSS_PLATFORM_INPUT

using UnityEngine;

#if USE_CROSS_PLATFORM_INPUT && CROSS_PLATFORM_INPUT
using UnityStandardAssets.CrossPlatformInput;
#endif

namespace Battlehub.Cubeman
{
    [RequireComponent(typeof (CubemanCharacter))]
    public class CubemanUserControl : MonoBehaviour
    {
        public Transform Cam;                     // A reference to the main camera in the scenes transform
        private CubemanCharacter m_Character;     
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
        public bool HandleInput;

        private void Start()
        {
            // get the transform of the main camera
            if(Cam == null)
            {
                if(Camera.main != null)
                {
                    Cam = Camera.main.transform;
                }
                
                if(Cam == null)
                {
                    //Debug.LogWarning("No Camera found");
                }
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<CubemanCharacter>();
            
        }


        private void Update()
        {
            if (!m_Jump)
            {

                #if USE_CROSS_PLATFORM_INPUT && CROSS_PLATFORM_INPUT
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump") && HandleInput;
                #else
                m_Jump = Input.GetKey(KeyCode.Space) && HandleInput;
                #endif
            }
        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // read inputs
            #if USE_CROSS_PLATFORM_INPUT && CROSS_PLATFORM_INPUT
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            #else
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            #endif
            bool crouch = Input.GetKey(KeyCode.C);

            crouch = false; //No crouching

            if(!HandleInput)
            {
                h = v = 0;
                crouch = false;
            }
        
            // calculate move direction to pass to character
            if (Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v * m_CamForward + h * Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v * Vector3.forward + h * Vector3.right;
            }
            #if !MOBILE_INPUT
            // walk speed multiplier
            if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
            #endif

            // pass all parameters to the character control script
            if (m_Character.enabled)
            {
                m_Character.Move(m_Move, crouch, m_Jump);
            }
            
            m_Jump = false;
        }
    }
}

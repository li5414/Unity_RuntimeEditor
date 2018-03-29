#define BROKEN_IMPORT
using UnityEngine;

namespace Battlehub.RTHandles
{
    #if BROKEN_IMPORT
    public class GameFerryBehavior : MonoBehaviour
    {
        private Animator m_animator;
        private GameFerry m_ferry;
        private int m_shortNameHash;

        private void Start()
        {
            m_animator = GetComponent<Animator>();
            AnimatorStateInfo state = m_animator.GetCurrentAnimatorStateInfo(0);
            m_shortNameHash = state.shortNameHash;
            m_ferry = m_animator.GetComponent<GameFerry>();
        }

        private void Update()
        {
            AnimatorStateInfo state = m_animator.GetCurrentAnimatorStateInfo(0);
            if (m_shortNameHash != state.shortNameHash)
            {
                if (!state.IsName("IdleForward") && !state.IsName("IdleBackward"))
                {
                    m_ferry.Lock();
                }
                else
                {
                    m_ferry.Unlock();
                }
                m_shortNameHash = state.shortNameHash;
            }


        }
    }
    #else


    public class GameFerryBehavior : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            GameFerry ferry = animator.GetComponent<GameFerry>();
            if (ferry != null)
            {
                if (stateInfo.IsName("FerryForward") || stateInfo.IsName("FerryBackward"))
                {
                    ferry.Lock();
                }
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            GameFerry ferry = animator.GetComponent<GameFerry>();
            if (ferry != null)
            {
                if (stateInfo.IsName("FerryForward") || stateInfo.IsName("FerryBackward"))
                {
                    ferry.Unlock();
                }
            }
        }
    }
#endif

}

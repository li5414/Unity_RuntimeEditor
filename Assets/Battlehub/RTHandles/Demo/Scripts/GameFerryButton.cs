using UnityEngine;

namespace Battlehub.RTHandles
{
    public class GameFerryButton : MonoBehaviour
    {
        private Animator m_ferryAnimator;
        private Animator m_buttonAnimator;

        private int m_forwardBool;
        private int m_pushBool;

        private void Start()
        {
            

            Transform parent = transform.parent;
            while(parent != null)
            {
                GameFerry ferry = parent.GetComponentInChildren<GameFerry>(); 
                if(ferry != null)
                {
                    m_ferryAnimator = ferry.GetComponent<Animator>();
                }

                parent = parent.parent;
            }

            m_buttonAnimator = GetComponent<Animator>();
            m_forwardBool = Animator.StringToHash("Forward");
            m_pushBool = Animator.StringToHash("Push");
        }

  
        private void OnTriggerEnter(Collider c)
        {
            if(m_ferryAnimator != null)
            {
                m_ferryAnimator.SetBool(m_forwardBool, true);
            }

            if(m_buttonAnimator != null)
            {
                m_buttonAnimator.SetBool(m_pushBool, true);
            }
        }

        private void OnTriggerExit(Collider c)
        {
            if(m_ferryAnimator != null)
            {
                m_ferryAnimator.SetBool(m_forwardBool, false);
            }

            if(m_buttonAnimator != null)
            {
                m_buttonAnimator.SetBool(m_pushBool, false);
            }   
        }
    }

}

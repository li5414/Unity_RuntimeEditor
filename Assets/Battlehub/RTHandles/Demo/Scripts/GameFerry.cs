using UnityEngine;
using Battlehub.Cubeman;

namespace Battlehub.RTHandles
{
    public class GameFerry : MonoBehaviour
    {
        private FixedJoint m_joint;
        private Rigidbody m_rig;

        private void Start()
        {
            m_joint = gameObject.AddComponent<FixedJoint>();
        }

        private void OnTriggerEnter(Collider c)
        {
            if(!c.GetComponent<CubemanCharacter>())
            {
                return;
            }

            Rigidbody rig = c.GetComponent<Rigidbody>();
            m_rig = rig;
        }

        private void OnTriggerExit(Collider c)
        {
            if (!c.GetComponent<CubemanCharacter>())
            {
                return;
            }

            m_rig = null;   
        }

        public void Lock()
        {
            if(!m_joint)
            {
                m_joint = gameObject.AddComponent<FixedJoint>();
            }

            m_joint.connectedBody = m_rig;
            m_joint.breakForce = float.PositiveInfinity;
        }

        public void Unlock()
        {
            if(m_joint)
            {
                m_joint.breakForce = 0.0001f;
            }
        }
    }
}

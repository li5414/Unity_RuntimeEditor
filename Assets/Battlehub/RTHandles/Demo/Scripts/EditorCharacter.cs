using UnityEngine;
using Battlehub.Cubeman;
using Battlehub.RTCommon;
namespace Battlehub.RTHandles
{
    public class EditorCharacter : MonoBehaviour
    {
        private Rigidbody m_rigidBody;
        private CubemanCharacter m_character;
        private bool m_isKinematic;
        private bool m_isEnabled;

        private void Start()
        {
            m_character = GetComponent<CubemanCharacter>();
            m_rigidBody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (InputController.GetKeyDown(KeyCode.V))
            {
                m_isKinematic = m_rigidBody.isKinematic;
                m_rigidBody.isKinematic = true;
                m_isEnabled = m_character.Enabled;
                m_character.Enabled = false;
            }
            else if (InputController.GetKeyUp(KeyCode.V))
            {
                m_rigidBody.isKinematic = m_isKinematic;
                m_character.Enabled = m_isEnabled;
            }

            if(transform.position.y < -5000)
            {
                m_rigidBody.isKinematic = true;
                m_character.Enabled = false;
            }
        }

        public void OnSelected(ExposeToEditor obj)
        {
            if(EditorDemo.Instance != null && EditorDemo.Instance.EnableCharacters)
            {
                EnableCharacter(obj.gameObject);
            }
        }

        private void EnableCharacter(GameObject obj)
        {
            if(!m_rigidBody)
            {
                return;
            }
            m_rigidBody.isKinematic = false;
            m_character.Enabled = true;
            CubemanUserControl userCtrl = obj.GetComponent<CubemanUserControl>();
            if (userCtrl != null)
            {
                userCtrl.HandleInput = true;
            }
        }

        public void OnUnselected(ExposeToEditor obj)
        {
            Rigidbody rig = obj.GetComponent<Rigidbody>();
            if (rig)
            {
                rig.isKinematic = true;
            }

            CubemanCharacter cubeman = obj.GetComponent<CubemanCharacter>();
            if (cubeman != null)
            {
                cubeman.Move(Vector3.zero, false, false);
                cubeman.Enabled = false;
            }

            CubemanUserControl userCtrl = obj.GetComponent<CubemanUserControl>();
            if (userCtrl != null)
            {
                userCtrl.HandleInput = false;
            }
        }


    }

}

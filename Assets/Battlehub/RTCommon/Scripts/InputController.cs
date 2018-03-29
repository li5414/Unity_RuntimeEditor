using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battlehub.RTCommon
{
    public class InputController : MonoBehaviour
    {
        public static InputController Instance
        {
            get { return m_instance; }
        }

        private static InputController m_instance;
        private bool m_isInputFieldSelected;
        private GameObject m_selectedGameObject;

        public static bool GetKeyDown(KeyCode key)
        {
            if(m_instance != null && m_instance.m_isInputFieldSelected)
            {
                return false;
            }

            return Input.GetKeyDown(key);
        }

        public static bool GetKeyUp(KeyCode key)
        {
            if (m_instance != null && m_instance.m_isInputFieldSelected)
            {
                return false;
            }

            return Input.GetKeyUp(key);
        }

        public static bool GetKey(KeyCode key)
        {
            if (m_instance != null && m_instance.m_isInputFieldSelected)
            {
                return false;
            }

            return Input.GetKey(key);
        }

        private void Awake()
        {
            if(m_instance != null)
            {
                Debug.LogWarning("Another instance of InputController exists");
            }
            m_instance = this;
        }

        private void OnDestroy()
        {
            if(m_instance == this)
            {
                m_instance = null;
            }
        }

        private void Update()
        {
            if(EventSystem.current != null)
            {
                if(EventSystem.current.currentSelectedGameObject != m_selectedGameObject)
                {
                    m_selectedGameObject = EventSystem.current.currentSelectedGameObject;
                    m_isInputFieldSelected = m_selectedGameObject != null && m_selectedGameObject.GetComponent<InputField>() != null;
                }
            }
        }
    }

}

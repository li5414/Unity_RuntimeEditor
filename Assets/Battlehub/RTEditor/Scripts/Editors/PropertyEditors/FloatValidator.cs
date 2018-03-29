using UnityEngine;
using UnityEngine.UI;

namespace Battlehub.RTEditor
{
    public class FloatValidator : MonoBehaviour
    {
        private InputField m_inputField;

        private void Start()
        {
            m_inputField = GetComponent<InputField>();
            m_inputField.onValidateInput += OnValidateInput;
        }
        
        private void OnDestroy()
        {
            if(m_inputField != null)
            {
                m_inputField.onValidateInput -= OnValidateInput;
            }
        }

        private char OnValidateInput(string text, int charIndex, char addedChar)
        {
            if(char.IsDigit(addedChar))
            {
                return addedChar;    
            }

            char u = char.ToUpper(addedChar);
            if(u == 'E' || u == '.' || u == ',' || u == '+' || u == '-')
            {
                return addedChar;
            }

            return '\0';
        }
    }
}

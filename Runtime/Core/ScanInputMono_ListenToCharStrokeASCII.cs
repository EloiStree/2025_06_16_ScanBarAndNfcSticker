using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;


namespace Eloi.ScanInput {

    public class ScanInputMono_ListenToCharStrokeASCII : MonoBehaviour
{
    public char m_character = '\0';
    public UnityEvent<char> m_onCharacterInput;
    public UnityEvent<string> m_onCharacterInputAsString;
    [TextArea(3, 10)]
    public string m_inputTextForDebug = "";
    private void OnEnable()
    {
        Keyboard.current.onTextInput += OnTextInput;
    }

    private void OnDisable()
    {
        Keyboard.current.onTextInput -= OnTextInput;
    }

    private void OnTextInput(char c)
    {
        m_character = c;
        m_inputTextForDebug += c; // Append the character to the input text
        if (m_inputTextForDebug.Length > 20)
        {
            m_inputTextForDebug = m_inputTextForDebug.Substring(1); // Keep the input text length manageable
        }

        m_onCharacterInput?.Invoke(c);
        m_onCharacterInputAsString?.Invoke(c.ToString());
    }
}

}
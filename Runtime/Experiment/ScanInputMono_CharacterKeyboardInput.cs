using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace Eloi.ScanInput
{
    public class ScanInputMono_CharacterKeyboardInput : MonoBehaviour
    {
        private InputAction keyboardAction;

        public string m_inputText = "";

        public bool m_isShiftPressed;
        public bool m_isLeftShiftPressed;
        public bool m_isRightShiftPressed;
        public bool m_isLeftCtrlPressed;
        public bool m_isRightCtrlPressed;
        public bool m_isLeftAltPressed;
        public bool m_isRightAltPressed;
        public Key m_lastKeyPressed = Key.None;
        public UnityEvent<Key> m_onInputTextChanged;

        void OnEnable()
        {
            keyboardAction = new InputAction(type: InputActionType.PassThrough, binding: "<Keyboard>/anyKey");
            keyboardAction.performed += OnKeyPressed;
            keyboardAction.Enable();
        }

        void OnDisable()
        {
            keyboardAction.Disable();
            keyboardAction.performed -= OnKeyPressed;
        }

        private void OnKeyPressed(InputAction.CallbackContext context)
        {
            m_isLeftAltPressed = Keyboard.current.leftAltKey.isPressed;
            m_isRightAltPressed = Keyboard.current.rightAltKey.isPressed;
            m_isLeftCtrlPressed = Keyboard.current.leftCtrlKey.isPressed;
            m_isRightCtrlPressed = Keyboard.current.rightCtrlKey.isPressed;
            m_isLeftShiftPressed = Keyboard.current.leftShiftKey.isPressed;
            m_isRightShiftPressed = Keyboard.current.rightShiftKey.isPressed;
            m_isShiftPressed = m_isLeftShiftPressed || m_isRightShiftPressed;

            var keyboard = Keyboard.current;
            if (keyboard == null) return;

            foreach (KeyControl key in keyboard.allKeys)
            {
                if (key != null && key.wasPressedThisFrame)
                {
                    m_lastKeyPressed = key.keyCode; // Store the last key pressed
                    m_onInputTextChanged?.Invoke(m_lastKeyPressed); // Invoke the event with the last key pressed
                    string character = KeyToChar(key);
                    if (!string.IsNullOrEmpty(character))
                    {
                        m_inputText += character; // Append the character to the input text
                        if (m_inputText.Length > 20)
                        {
                            m_inputText = m_inputText.Substring(1); // Keep the input text length manageable
                        }
                    }
                }
            }
        }

        private string KeyToChar(KeyControl key)
        {
            // Simple mapping for demonstration
            if (key.keyCode >= Key.A && key.keyCode <= Key.Z)
            {
                bool shift = Keyboard.current.leftShiftKey.isPressed || Keyboard.current.rightShiftKey.isPressed;
                char baseChar = (char)('a' + (key.keyCode - Key.A));
                return shift ? baseChar.ToString().ToUpper() : baseChar.ToString();
            }
            else if (key.keyCode >= Key.Digit0 && key.keyCode <= Key.Digit9)
            {
                switch (key.keyCode)
                {
                    case Key.Digit0: return "0";
                    case Key.Digit1: return "1";
                    case Key.Digit2: return "2";
                    case Key.Digit3: return "3";
                    case Key.Digit4: return "4";
                    case Key.Digit5: return "5";
                    case Key.Digit6: return "6";
                    case Key.Digit7: return "7";
                    case Key.Digit8: return "8";
                    case Key.Digit9: return "9";
                    default: return null;
                }
            }
            else if (key.keyCode >= Key.Numpad0 && key.keyCode <= Key.Numpad9)
            {
                switch (key.keyCode)
                {
                    case Key.Numpad0: return "0";
                    case Key.Numpad1: return "1";
                    case Key.Numpad2: return "2";
                    case Key.Numpad3: return "3";
                    case Key.Numpad4: return "4";
                    case Key.Numpad5: return "5";
                    case Key.Numpad6: return "6";
                    case Key.Numpad7: return "7";
                    case Key.Numpad8: return "8";
                    case Key.Numpad9: return "9";
                    default: return null;
                }
            }
            else if (key.keyCode == Key.Space) return " ";
            else if (key.keyCode == Key.Backspace) return "\b"; // Handle backspace
            else if (key.keyCode == Key.Enter) return "\n"; // Handle enter key
            else if (key.keyCode == Key.Escape) return "\x1B"; // Handle escape key
            else if (key.keyCode == Key.Tab) return "\t"; // Handle tab key
            else if (key.keyCode == Key.Minus) return "-"; // Handle minus key
            else if (key.keyCode == Key.Equals) return "="; // Handle equals key
            else if (key.keyCode == Key.Period) return "."; // Handle period key
            else if (key.keyCode == Key.Comma) return ","; // Handle comma key
            else if (key.keyCode == Key.Slash) return "/"; // Handle slash key
            else if (key.keyCode == Key.Backslash) return "\\"; // Handle backslash key
            else if (key.keyCode == Key.Semicolon) return ";"; // Handle semicolon key
            else if (key.keyCode == Key.Quote) return "'"; // Handle single quote key
            else if (key.keyCode == Key.LeftBracket) return "["; // Handle left bracket key
            else if (key.keyCode == Key.RightBracket) return "]"; // Handle right bracket key
            else if (key == Keyboard.current.spaceKey) return " ";
            else if (key == Keyboard.current.enterKey) return "\n";

            return null;
        }
    }
}
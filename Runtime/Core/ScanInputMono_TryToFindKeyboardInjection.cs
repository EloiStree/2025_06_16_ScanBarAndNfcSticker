using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

namespace Eloi.ScanInput
{

    public class ScanInputMono_TryToFindKeyboardInjection : MonoBehaviour
    {
        public long m_lastInjectedTime = 0;
        public long m_lastInjectedTimeByLineReturn = 0;
        public char m_lastReceivedCharacter = '\0';

        public float m_typeSpeedTimeout = 0.1f;

        public bool m_isWritingSomething;
        public List<char> m_injectedCharacters = new List<char>();
        public string m_lastInjectedText = "";
        public UnityEvent<string> onInjectedText;

        public float GetTimeSinceLastInjection()
        {
            return (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - m_lastInjectedTime) / 1000f;
        }

        public void Update()
        {
            float timeSinceLastInjection = GetTimeSinceLastInjection();
            if (m_isWritingSomething && timeSinceLastInjection > m_typeSpeedTimeout)
            {
                m_isWritingSomething = false;
                string injectedText = new string(m_injectedCharacters.ToArray());
                m_lastInjectedText = injectedText;
                onInjectedText?.Invoke(injectedText);
                m_injectedCharacters.Clear();
            }
        }

        public void PushIn(char character)
        {
            m_lastInjectedTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            m_lastReceivedCharacter = character;
            m_isWritingSomething = true;
            m_injectedCharacters.Add(character);

            if (character == '\n' || character == '\r')
            {
                // If the character is a newline, we consider it as the end of input
                string injectedText = new string(m_injectedCharacters.ToArray());
                m_lastInjectedText = injectedText;
                onInjectedText?.Invoke(injectedText);
                m_injectedCharacters.Clear();
                m_isWritingSomething = false;
                m_lastInjectedTimeByLineReturn = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            }
        }
    }
}
using UnityEngine.Events;

namespace Eloi.ScanInput
{
    [System.Serializable]
    public class ScanInput_StringToGeneric<T> 
    {
        
        public StringToGeneric[] m_stringToValue;
        public UnityEvent<T> m_onValueFound;
        public bool m_onlyFirstFound = false;
        public T m_lastPushedValue;

        [System.Serializable]
        public class StringToGeneric
        {
            public string m_stringToMatch;
            public T m_linkedValue;
        }
        public void GetValueFromString(string idToFind, out T valueFound)
        {
            valueFound = default;
            idToFind = idToFind.Trim();
            if (string.IsNullOrEmpty(idToFind))
            {
                return;
            }
            foreach (var item in m_stringToValue)
            {
                if (item.m_stringToMatch == idToFind)
                {
                    valueFound = item.m_linkedValue;
                    return;
                }
            }
        }

        public void TryToBroadcastValueFound(string idFound)
        {
            idFound = idFound.Trim();
            if (string.IsNullOrEmpty(idFound))
            {
                return;
            }
            foreach (var item in m_stringToValue)
            {
                if (item.m_stringToMatch == idFound)
                {
                    m_onValueFound?.Invoke(item.m_linkedValue);
                    m_lastPushedValue = item.m_linkedValue;
                    if (m_onlyFirstFound)
                    {
                        return;
                    }
                }
            }
        }

    }
}
using UnityEngine.Events;


namespace Eloi.ScanInput {
    [System.Serializable]
    public class ScanInput_StringToInteger
    {
        public StringToInteger[] m_stringToPrefabToInstanciate;
        public UnityEvent<int> m_onIntegerFound;
        
        public bool m_onlyFirstFound = false; 


        public int m_lastPushedInteger;


        [System.Serializable]
        public class StringToInteger
        {
            public string m_stringToMatch;
            public int m_integerValue;
        }

        public void GetIntegerFromString(string idFound, out int integerValue)
        {
            integerValue = 0;
            idFound = idFound.Trim();
            if (string.IsNullOrEmpty(idFound))
            {
                return;
            }
            foreach (var item in m_stringToPrefabToInstanciate)
            {
                if (item.m_stringToMatch == idFound)
                {
                    integerValue = item.m_integerValue;
                    return;
                }
            }
        }

        public void TryToBroadcastIntegerFound(string idFound)
        {
            idFound = idFound.Trim();
            if (string.IsNullOrEmpty(idFound))
            {
                return;
            }
            foreach (var item in m_stringToPrefabToInstanciate)
            {
                if (item.m_stringToMatch == idFound)
                {
                    m_onIntegerFound?.Invoke(item.m_integerValue);
                    m_lastPushedInteger = item.m_integerValue;
                    if (m_onlyFirstFound)
                    {
                        return; 
                    }
                }
            }
        }
    }

}
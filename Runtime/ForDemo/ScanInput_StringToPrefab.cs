using UnityEngine;
using UnityEngine.Events;


namespace Eloi.ScanInput {

     [System.Serializable]
    public class ScanInput_StringToPrefab 
    {

        public StringToPrefabToInstanciate[] m_stringToPrefabToInstanciate;
        public UnityEvent<GameObject> m_onPrefabInstantiated;
        public GameObject m_lastPush;
        [System.Serializable]
        public class StringToPrefabToInstanciate
        {
            public string m_stringToMatch;
            public GameObject m_prefabToInstantiate;
        }


        public void GetPrefabFromString(string idFound, out GameObject prefab)
        {
            prefab = null;
            idFound = idFound.Trim();
            if (string.IsNullOrEmpty(idFound))
            {
                return;
            }
            foreach (var item in m_stringToPrefabToInstanciate)
            {
                if (item.m_stringToMatch == idFound)
                {
                    prefab = item.m_prefabToInstantiate;
                    return;
                }
            }
        }

        public bool m_onlyFirstFound=false;
        public void TryToBroadcastPrefabToCreated(string idFound) {

            idFound = idFound.Trim();
            if (string.IsNullOrEmpty(idFound))
            {
                return;
            }

            foreach (var item in m_stringToPrefabToInstanciate)
            {
                if (item.m_stringToMatch == idFound)
                {
                    m_onPrefabInstantiated?.Invoke(item.m_prefabToInstantiate );
                    m_lastPush = item.m_prefabToInstantiate;
                    if (m_onlyFirstFound)
                    {
                        return; 
                    }
                }
            }
        }
    }

}
using UnityEngine;


namespace Eloi.ScanInput {

    public class ScanInputMono_StringToGeneric<T> : MonoBehaviour 
    {
        public ScanInput_StringToGeneric<T> m_scanInputStringToValue;
        public void GetValueFromString(string idToFind, out T valueFound)
        {
            valueFound = default;
            if (m_scanInputStringToValue != null)
            {
                m_scanInputStringToValue.GetValueFromString(idToFind, out valueFound);
            }
        }
        public void TryToBroadcastValueFound(string idFound)
        {
            if (m_scanInputStringToValue != null)
            {
                m_scanInputStringToValue.TryToBroadcastValueFound(idFound);
            }
        }
    }

}
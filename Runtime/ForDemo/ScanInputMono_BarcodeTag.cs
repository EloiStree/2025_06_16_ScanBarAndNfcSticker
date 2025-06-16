using UnityEngine;


namespace Eloi.ScanInput {
    public class ScanInputMono_BarcodeTag : MonoBehaviour
    {
        public string m_barcodeTag = "";

        public string GetBarCode()
        {
            return m_barcodeTag;
        }
        public void GetBarCode(out string barcode)
        {
            barcode = m_barcodeTag;
        }
        public void SetBarCode(string barcode)
        {
            m_barcodeTag = barcode;
        }
    }

}
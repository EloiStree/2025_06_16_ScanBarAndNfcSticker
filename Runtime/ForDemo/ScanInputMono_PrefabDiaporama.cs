using UnityEngine;
using UnityEngine.Events;


namespace Eloi.ScanInput {
    public class ScanInputMono_PrefabDiaporama : MonoBehaviour
    {
        public Transform m_whereToCreate;
        public Transform m_parentToAttachTo;

        public UnityEvent<GameObject> m_onPrefabInstantiated;
        public GameObject m_lastCreated;


        public void ApplyTextureToRenderers(Texture2D texture)
        {

            if (m_lastCreated == null)
            {
                return;
            }
            Renderer[] renderers = m_lastCreated.GetComponentsInChildren<Renderer>();
            foreach (var renderer in renderers)
            {
                if (renderer.material != null)
                {
                    renderer.material.mainTexture = texture;
                }
            }
        }
        public void SetGameObjectToCreate(GameObject whatToCreated) {

            if (m_lastCreated != null)
            {
                if(Application.isPlaying)
                {
                    Destroy(m_lastCreated);
                }
                else
                {
                    DestroyImmediate(m_lastCreated);
                }
            }

            m_lastCreated = GameObject.Instantiate(whatToCreated, m_whereToCreate.position, m_whereToCreate.rotation, m_parentToAttachTo);
            m_onPrefabInstantiated?.Invoke(m_lastCreated);

        }
        
    }

}
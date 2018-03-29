
using UnityEngine;

namespace Battlehub.RTGizmos
{
    public class LightGizmo : MonoBehaviour
    {
        private Light m_light;
        private LightType m_lightType;

        [SerializeField, HideInInspector]
        private Component m_gizmo;

        private void Awake()
        {
            m_light = GetComponent<Light>();
            if(m_light == null)
            {
                Destroy(this);
            }
            else
            {
                m_lightType = m_light.type;
                CreateGizmo();
            }
        }

        private void OnDestroy()
        {
            if (m_gizmo != null)
            {
                Destroy(m_gizmo);
                m_gizmo = null;
            }
        }

        private void Update()
        {
            if(m_light == null)
            {
                Destroy(this);
            }
            else
            {
                if(m_lightType != m_light.type)
                {
                    m_lightType = m_light.type;
                    CreateGizmo();
                }
            }
        }

        private void CreateGizmo()
        {
            if(m_gizmo != null)
            {
                Destroy(m_gizmo);
                m_gizmo = null;
            }

            if(m_lightType == LightType.Point)
            {
                if(m_gizmo == null)
                {
                    m_gizmo = gameObject.AddComponent<PointLightGizmo>();
                }

                
            }
            else if(m_lightType == LightType.Spot)
            {
                if(m_gizmo == null)
                {
                    m_gizmo = gameObject.AddComponent<SpotlightGizmo>();
                }
            }
            else if(m_lightType == LightType.Directional)
            {
                if(m_gizmo == null)
                {
                    m_gizmo = gameObject.AddComponent<DirectionalLightGizmo>();
                }
            }
        }
    }

}

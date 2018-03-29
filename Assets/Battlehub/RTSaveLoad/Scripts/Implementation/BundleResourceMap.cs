using Battlehub.Utils;
using UnityEngine;

namespace Battlehub.RTSaveLoad
{
    [ExecuteInEditMode]
    public class BundleResourceMap : MonoBehaviour
    {
        [ReadOnly]
        public string BundleName;
        [ReadOnly]
        public string VariantName;

        [SerializeField]
        [ReadOnly]
        private string m_guid;

        public string Guid
        {
            get { return m_guid; }
        }

        private void Awake()
        {
            if (!Application.isPlaying)
            {
                if (string.IsNullOrEmpty(m_guid))
                {
                    m_guid = System.Guid.NewGuid().ToString();
                }
            }
        }
    }
}


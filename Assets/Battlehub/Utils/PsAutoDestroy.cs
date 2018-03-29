using UnityEngine;

namespace Battlehub.Utils
{
    public class PsAutoDestroy : MonoBehaviour
    {
        private ParticleSystem ps;

        public void Start()
        {
            ps = GetComponent<ParticleSystem>();
        }

        public void Update()
        {
            if (ps)
            {
                if (!ps.IsAlive())
                {
                    Destroy(gameObject, 0.0f);
                }
            }

        }
    }
}

	

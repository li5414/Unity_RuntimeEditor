using UnityEngine;
using System.Collections.Generic;


namespace Battlehub.RTCommon
{
    public interface IGL
    {
        void Draw(int cullingMask);
    }

    /// <summary>
    /// GLRenderer calls Draw method on all registered classes 
    /// </summary>
    [ExecuteInEditMode]
    public class GLRenderer : MonoBehaviour
    {
        private static GLRenderer m_instance;

        public static GLRenderer Instance
        {
            get { return m_instance; }
        }

        private List<IGL> m_renderObjects;
        public void Add(IGL gl)
        {
            if (m_renderObjects.Contains(gl))
            {
                return;
            }

            m_renderObjects.Add(gl);
        }

        public void Remove(IGL gl)
        {
            m_renderObjects.Remove(gl);
        }

        private void Awake()
        {
            if(m_instance != null)
            {
                Debug.LogWarning("Another instance of GLLinesRenderer aleready exist");
            }
            m_instance = this;

            m_renderObjects = new List<IGL>();
          
        }

        private void OnDestroy()
        {
            if(m_instance == this)
            {
                m_instance = null;
            }
        }

        public void Draw(int cullingMask)
        {
            if(m_renderObjects == null)
            {
                return;
            }

            GL.PushMatrix();
            try
            {
                for (int i = 0; i < m_renderObjects.Count; ++i)
                {
                    IGL line = m_renderObjects[i];
                    line.Draw(cullingMask); 
                }
            }
            finally
            {
                GL.PopMatrix();
            } 
        }


        #if UNITY_EDITOR
        private void Update()
        {
            if (m_instance == null)
            {
                m_instance = this;
                m_renderObjects = new List<IGL>();
            }
        }
        [UnityEditor.Callbacks.DidReloadScripts(99)]
        private static void OnScriptsReloaded()
        {
            if(m_instance == null)
            {
                GLRenderer glRenderer = FindObjectOfType<GLRenderer>();
                if(glRenderer != null)
                {
                    glRenderer.m_renderObjects = new List<IGL>();
                    m_instance = glRenderer;
                }
            }
        }
        #endif
    }
}

using UnityEngine;

namespace Battlehub.RTCommon
{
    public enum RTLayer
    {
        None = 0,
        SceneView = 1 << 0,
        GameView = 1 << 1,
        Any = -1,
    }

    /// <summary>
    /// Camera behavior for GL. rendering
    /// </summary>
    [ExecuteInEditMode]
    public class GLCamera : MonoBehaviour
    {
        public int CullingMask = -1;

        private void OnPostRender()
        { 
            if(GLRenderer.Instance != null)
            {
                GLRenderer.Instance.Draw(CullingMask);
            }
        }
    }
}


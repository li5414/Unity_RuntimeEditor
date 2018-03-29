using UnityEngine;
using System.Collections;

namespace Battlehub.Utils
{
    public static class CursorHelper
    {
        private static object m_locker;

        public static void SetCursor(object locker, Texture2D texture, Vector2 hotspot, CursorMode mode)
        {
            
            if (m_locker != null && m_locker != locker)
            {
                return;
            }
            m_locker = locker;
            Cursor.SetCursor(texture, hotspot, mode);
        }

        public static void ResetCursor(object locker)
        {            
            if (m_locker != locker)
            {
                return;
            }

            m_locker = null;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

    }

}

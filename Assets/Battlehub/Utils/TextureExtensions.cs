using UnityEngine;

namespace Battlehub.Utils
{
    public static class TextureExtensions 
    {
        public static bool IsReadable(this Texture2D texture)
        {
            if (texture == null)
            {
                return false;
            }
            try
            {
                texture.GetPixel(0, 0);
                return true;
            }
            catch (UnityException)
            {
                return false;
            }
        }
    }
}


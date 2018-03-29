using System;

namespace Battlehub.RTSaveLoad
{
    //TODO: Implement
    public partial class SaveLoadConfig 
    {
        private static Type[] m_disabledTypes = new Type[0];

        public static Type[] DisabledComponentTypes
        {
            get { return m_disabledTypes; }
        }
    }
}


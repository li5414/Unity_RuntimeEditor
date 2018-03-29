using System; 

namespace Battlehub.RTSaveLoad
{
    //TODO: Implement
	public partial class SaveLoadConfig
	{
		static SaveLoadConfig()
		{
			m_disabledTypes = new Type[]
			{
				 typeof(UnityEngine.UI.Button),
			};
		}
	}
}

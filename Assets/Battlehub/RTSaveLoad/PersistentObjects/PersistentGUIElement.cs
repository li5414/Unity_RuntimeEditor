#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif
/*This is auto-generated file. Tools->Runtime SaveLoad->Create Persistent Objects 
If you want prevent overwriting, drag this file to another folder.*/

namespace Battlehub.RTSaveLoad.PersistentObjects
{
	#if RT_USE_PROTOBUF
	[ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
	[ProtoInclude(1067, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentGUITexture))]
	[ProtoInclude(1068, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentGUIText))]
	#endif
	[System.Serializable]
	public class PersistentGUIElement : Battlehub.RTSaveLoad.PersistentObjects.PersistentBehaviour
	{
	}
}

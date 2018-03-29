#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif
/*This is auto-generated file. Tools->Runtime SaveLoad->Create Persistent Objects 
If you want prevent overwriting, drag this file to another folder.*/

namespace Battlehub.RTSaveLoad.PersistentObjects.EventSystems
{
	#if RT_USE_PROTOBUF
	[ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
	[ProtoInclude(1125, typeof(Battlehub.RTSaveLoad.PersistentObjects.EventSystems.PersistentStandaloneInputModule))]
	#endif
	[System.Serializable]
	public class PersistentPointerInputModule : Battlehub.RTSaveLoad.PersistentObjects.EventSystems.PersistentBaseInputModule
	{
	}
}

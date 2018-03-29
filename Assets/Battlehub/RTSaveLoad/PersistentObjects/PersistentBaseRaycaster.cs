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
	[ProtoInclude(1126, typeof(Battlehub.RTSaveLoad.PersistentObjects.EventSystems.PersistentPhysicsRaycaster))]
	[ProtoInclude(1127, typeof(Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentGraphicRaycaster))]
	#endif
	[System.Serializable]
	public class PersistentBaseRaycaster : Battlehub.RTSaveLoad.PersistentObjects.EventSystems.PersistentUIBehaviour
	{
	}
}

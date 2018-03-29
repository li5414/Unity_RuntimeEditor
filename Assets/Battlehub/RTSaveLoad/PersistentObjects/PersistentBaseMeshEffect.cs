#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif
/*This is auto-generated file. Tools->Runtime SaveLoad->Create Persistent Objects 
If you want prevent overwriting, drag this file to another folder.*/

namespace Battlehub.RTSaveLoad.PersistentObjects.UI
{
	#if RT_USE_PROTOBUF
	[ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
	[ProtoInclude(1134, typeof(Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentPositionAsUV1))]
	[ProtoInclude(1135, typeof(Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentShadow))]
	#endif
	[System.Serializable]
	public class PersistentBaseMeshEffect : Battlehub.RTSaveLoad.PersistentObjects.EventSystems.PersistentUIBehaviour
	{
	}
}

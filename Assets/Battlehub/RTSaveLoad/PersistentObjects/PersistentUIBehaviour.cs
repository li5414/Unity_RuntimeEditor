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
	[ProtoInclude(1108, typeof(Battlehub.RTSaveLoad.PersistentObjects.EventSystems.PersistentEventSystem))]
	[ProtoInclude(1109, typeof(Battlehub.RTSaveLoad.PersistentObjects.EventSystems.PersistentBaseInput))]
	[ProtoInclude(1110, typeof(Battlehub.RTSaveLoad.PersistentObjects.EventSystems.PersistentBaseInputModule))]
	[ProtoInclude(1111, typeof(Battlehub.RTSaveLoad.PersistentObjects.EventSystems.PersistentBaseRaycaster))]
	[ProtoInclude(1112, typeof(Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentGraphic))]
	[ProtoInclude(1113, typeof(Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentMask))]
	[ProtoInclude(1114, typeof(Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentRectMask2D))]
	[ProtoInclude(1115, typeof(Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentScrollRect))]
	[ProtoInclude(1116, typeof(Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentSelectable))]
	[ProtoInclude(1117, typeof(Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentToggleGroup))]
	[ProtoInclude(1118, typeof(Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentAspectRatioFitter))]
	[ProtoInclude(1119, typeof(Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentCanvasScaler))]
	[ProtoInclude(1120, typeof(Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentContentSizeFitter))]
	[ProtoInclude(1121, typeof(Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentLayoutElement))]
	[ProtoInclude(1122, typeof(Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentLayoutGroup))]
	[ProtoInclude(1123, typeof(Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentBaseMeshEffect))]
	#endif
	[System.Serializable]
	public class PersistentUIBehaviour : Battlehub.RTSaveLoad.PersistentObjects.PersistentMonoBehaviour
	{
	}
}

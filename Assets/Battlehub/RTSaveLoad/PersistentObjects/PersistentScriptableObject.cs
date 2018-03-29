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
	[ProtoInclude(1000, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentStateMachineBehaviour))]
	[ProtoInclude(1001, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentGUISkin))]
	[ProtoInclude(1002, typeof(Battlehub.RTSaveLoad.PersistentObjects.Networking.PlayerConnection.PersistentPlayerConnection))]
	[ProtoInclude(1003, typeof(Battlehub.RTSaveLoad.PersistentObjects.Experimental.Rendering.PersistentRenderPipelineAsset))]
	#endif
	[System.Serializable]
	public class PersistentScriptableObject : Battlehub.RTSaveLoad.PersistentObjects.PersistentObject
	{
	}
}

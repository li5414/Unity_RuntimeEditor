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
	[ProtoInclude(1107, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentAnimationClip))]
	#endif
	[System.Serializable]
	public class PersistentMotion : Battlehub.RTSaveLoad.PersistentObjects.PersistentObject
	{
	}
}

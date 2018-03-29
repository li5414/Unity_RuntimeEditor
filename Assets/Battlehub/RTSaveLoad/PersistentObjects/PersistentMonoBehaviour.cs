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
	[ProtoInclude(1069, typeof(Battlehub.RTSaveLoad.PersistentObjects.Networking.Match.PersistentNetworkMatch))]
	[ProtoInclude(1070, typeof(Battlehub.RTSaveLoad.PersistentObjects.EventSystems.PersistentEventTrigger))]
	[ProtoInclude(1071, typeof(Battlehub.RTSaveLoad.PersistentObjects.EventSystems.PersistentUIBehaviour))]
	#endif
	[System.Serializable]
	public class PersistentMonoBehaviour : Battlehub.RTSaveLoad.PersistentObjects.PersistentBehaviour
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.MonoBehaviour o = (UnityEngine.MonoBehaviour)obj;
			o.useGUILayout = useGUILayout;

			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.MonoBehaviour o = (UnityEngine.MonoBehaviour)obj;
			useGUILayout = o.useGUILayout;

		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public bool useGUILayout;



	}
}

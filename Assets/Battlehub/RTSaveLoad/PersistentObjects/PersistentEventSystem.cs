#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif
using Battlehub.RTSaveLoad;
/*This is auto-generated file. Tools->Runtime SaveLoad->Create Persistent Objects 
If you want prevent overwriting, drag this file to another folder.*/

namespace Battlehub.RTSaveLoad.PersistentObjects.EventSystems
{
	#if RT_USE_PROTOBUF
	[ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
	#endif
	[System.Serializable]
	public class PersistentEventSystem : Battlehub.RTSaveLoad.PersistentObjects.EventSystems.PersistentUIBehaviour
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.EventSystems.EventSystem o = (UnityEngine.EventSystems.EventSystem)obj;
			o.sendNavigationEvents = sendNavigationEvents;
			o.pixelDragThreshold = pixelDragThreshold;
			o.firstSelectedGameObject = (UnityEngine.GameObject)objects.Get(firstSelectedGameObject);
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.EventSystems.EventSystem o = (UnityEngine.EventSystems.EventSystem)obj;
			sendNavigationEvents = o.sendNavigationEvents;
			pixelDragThreshold = o.pixelDragThreshold;
			firstSelectedGameObject = o.firstSelectedGameObject.GetMappedInstanceID();
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			AddDependency(firstSelectedGameObject, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.EventSystems.EventSystem o = (UnityEngine.EventSystems.EventSystem)obj;
			AddDependency(o.firstSelectedGameObject, dependencies);
		}

		public bool sendNavigationEvents;

		public int pixelDragThreshold;

		public long firstSelectedGameObject;

	}
}

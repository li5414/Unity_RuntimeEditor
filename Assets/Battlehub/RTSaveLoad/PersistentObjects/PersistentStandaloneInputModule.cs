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
	#endif
	[System.Serializable]
	public class PersistentStandaloneInputModule : Battlehub.RTSaveLoad.PersistentObjects.EventSystems.PersistentPointerInputModule
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.EventSystems.StandaloneInputModule o = (UnityEngine.EventSystems.StandaloneInputModule)obj;
			o.forceModuleActive = forceModuleActive;
			o.inputActionsPerSecond = inputActionsPerSecond;
			o.repeatDelay = repeatDelay;
			o.horizontalAxis = horizontalAxis;
			o.verticalAxis = verticalAxis;
			o.submitButton = submitButton;
			o.cancelButton = cancelButton;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.EventSystems.StandaloneInputModule o = (UnityEngine.EventSystems.StandaloneInputModule)obj;
			forceModuleActive = o.forceModuleActive;
			inputActionsPerSecond = o.inputActionsPerSecond;
			repeatDelay = o.repeatDelay;
			horizontalAxis = o.horizontalAxis;
			verticalAxis = o.verticalAxis;
			submitButton = o.submitButton;
			cancelButton = o.cancelButton;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public bool forceModuleActive;

		public float inputActionsPerSecond;

		public float repeatDelay;

		public string horizontalAxis;

		public string verticalAxis;

		public string submitButton;

		public string cancelButton;

	}
}

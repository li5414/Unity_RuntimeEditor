#define RT_USE_PROTOBUF

using Battlehub.RTSaveLoad;
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif
/*This is auto-generated file. Tools->Runtime SaveLoad->Create Persistent Objects 
If you want prevent overwriting, drag this file to another folder.*/

namespace Battlehub.RTSaveLoad.PersistentObjects.UI
{
#if RT_USE_PROTOBUF
    [ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
#endif
    [System.Serializable]
	public class PersistentNavigation : Battlehub.RTSaveLoad.PersistentData
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.UI.Navigation o = (UnityEngine.UI.Navigation)obj;
			o.mode = mode;
			o.selectOnUp = (UnityEngine.UI.Selectable)objects.Get(selectOnUp);
			o.selectOnDown = (UnityEngine.UI.Selectable)objects.Get(selectOnDown);
			o.selectOnLeft = (UnityEngine.UI.Selectable)objects.Get(selectOnLeft);
			o.selectOnRight = (UnityEngine.UI.Selectable)objects.Get(selectOnRight);
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.UI.Navigation o = (UnityEngine.UI.Navigation)obj;
			mode = o.mode;
			selectOnUp = o.selectOnUp.GetMappedInstanceID();
			selectOnDown = o.selectOnDown.GetMappedInstanceID();
			selectOnLeft = o.selectOnLeft.GetMappedInstanceID();
			selectOnRight = o.selectOnRight.GetMappedInstanceID();
		}

        public UnityEngine.UI.Navigation.Mode mode;

        public long selectOnUp;

        public long selectOnDown;

        public long selectOnLeft;

        public long selectOnRight;

	}
}

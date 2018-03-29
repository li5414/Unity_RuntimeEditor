#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif
using Battlehub.RTSaveLoad;
/*This is auto-generated file. Tools->Runtime SaveLoad->Create Persistent Objects 
If you want prevent overwriting, drag this file to another folder.*/

namespace Battlehub.RTSaveLoad.PersistentObjects.UI
{
	#if RT_USE_PROTOBUF
	[ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
	#endif
	[System.Serializable]
	public class PersistentSlider
#if RT_PE_MAINTANANCE
        : PersistentData
#else
        : Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentSelectable
#endif
    {
        public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.UI.Slider o = (UnityEngine.UI.Slider)obj;
			o.fillRect = (UnityEngine.RectTransform)objects.Get(fillRect);
			o.handleRect = (UnityEngine.RectTransform)objects.Get(handleRect);
			o.direction = direction;
			o.minValue = minValue;
			o.maxValue = maxValue;
			o.wholeNumbers = wholeNumbers;
			o.value = value;
			o.normalizedValue = normalizedValue;
            Write(o.onValueChanged, onValueChanged, objects);
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.UI.Slider o = (UnityEngine.UI.Slider)obj;
			fillRect = o.fillRect.GetMappedInstanceID();
			handleRect = o.handleRect.GetMappedInstanceID();
			direction = o.direction;
			minValue = o.minValue;
			maxValue = o.maxValue;
			wholeNumbers = o.wholeNumbers;
			value = o.value;
			normalizedValue = o.normalizedValue;
            Read(onValueChanged, o.onValueChanged);
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			AddDependency(fillRect, dependencies, objects, allowNulls);
			AddDependency(handleRect, dependencies, objects, allowNulls);
            if(onValueChanged != null)
            {
                onValueChanged.FindDependencies(dependencies, objects, allowNulls);
            }
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.UI.Slider o = (UnityEngine.UI.Slider)obj;
			AddDependency(o.fillRect, dependencies);
			AddDependency(o.handleRect, dependencies);

            PersistentUnityEventBase evnt = new PersistentUnityEventBase();
            evnt.GetDependencies(o.onValueChanged, dependencies);
		}

        public long fillRect;

        public long handleRect;

        public UnityEngine.UI.Slider.Direction direction;

        public float minValue;

        public float maxValue;

        public bool wholeNumbers;

        public float value;

        public float normalizedValue;
	
        public PersistentUnityEventBase onValueChanged;

	}
}

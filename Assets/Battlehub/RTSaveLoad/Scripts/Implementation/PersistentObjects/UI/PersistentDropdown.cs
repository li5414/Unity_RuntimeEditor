
/*To autogenerate PersistentDropdown remove this file and run Tools->Runtime SaveLoad->Create Persistent Objects command*/

#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif
using Battlehub.RTSaveLoad.PersistentObjects.UI;
using System.Collections.Generic;
using System.Linq;

namespace Battlehub.RTSaveLoad.PersistentObjects.UI
{
    #if RT_USE_PROTOBUF
	[ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
    #endif
    [System.Serializable]
    public class PersistentDropdown
    #if RT_PE_MAINTANANCE
        : PersistentData
    #else
        : UI.PersistentSelectable
    #endif
    {
        public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.UI.Dropdown o = (UnityEngine.UI.Dropdown)obj;
			o.template = (UnityEngine.RectTransform)objects.Get(template);
			o.captionText = (UnityEngine.UI.Text)objects.Get(captionText);
			o.captionImage = (UnityEngine.UI.Image)objects.Get(captionImage);
			o.itemText = (UnityEngine.UI.Text)objects.Get(itemText);
			o.itemImage = (UnityEngine.UI.Image)objects.Get(itemImage);
			o.onValueChanged = Write(o.onValueChanged, onValueChanged, objects);
			o.value = value;

            #if !RT_PE_MAINTANANCE
            if (options != null)
            {
                List<UnityEngine.UI.Dropdown.OptionData> optionDataList = new List<UnityEngine.UI.Dropdown.OptionData>();
                for (int i = 0; i < options.Length; ++i)
                {
                    PersistentOptionData option = options[i];

                    UnityEngine.UI.Dropdown.OptionData optionData = new UnityEngine.UI.Dropdown.OptionData();
                    option.WriteTo(optionData, objects);
                    optionDataList.Add(optionData);

                }
                o.options = optionDataList;
            }
            else
            {
                o.options = null;
            }
            #endif

            return o;
		}

        public override void FindDependencies<T>(Dictionary<long, T> dependencies, Dictionary<long, T> objects, bool allowNulls)
        {
            base.FindDependencies<T>(dependencies, objects, allowNulls);

            AddDependency(template, dependencies, objects, allowNulls);
            AddDependency(captionText, dependencies, objects, allowNulls);
            AddDependency(captionImage, dependencies, objects, allowNulls);
            AddDependency(itemText, dependencies, objects, allowNulls);
            AddDependency(itemImage, dependencies, objects, allowNulls);

            if(onValueChanged != null)
            {
                onValueChanged.FindDependencies(dependencies, objects, allowNulls);
            }
            

#if !RT_PE_MAINTANANCE
            if (options != null)
            {
                for (int i = 0; i < options.Length; ++i)
                {
                    PersistentOptionData option = options[i];
                    option.FindDependencies(dependencies, objects, allowNulls);
                }
            }
#endif
        }

        public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.UI.Dropdown o = (UnityEngine.UI.Dropdown)obj;
			template = o.template.GetMappedInstanceID();
			captionText = o.captionText.GetMappedInstanceID();
			captionImage = o.captionImage.GetMappedInstanceID();
			itemText = o.itemText.GetMappedInstanceID();
			itemImage = o.itemImage.GetMappedInstanceID();
			onValueChanged = Read(onValueChanged, o.onValueChanged);
            #if !RT_PE_MAINTANANCE
            if (o.options != null)
            {

                options = new PersistentOptionData[o.options.Count];
                for (int i = 0; i < o.options.Count; ++i)
                {
                    PersistentOptionData option = new PersistentOptionData();
                    option.ReadFrom(o.options[i]);
                    options[i] = option;
                }

            }
            else
            {
                options = null;
            }
            #endif
            value = o.value;
		}

        public long template;

		public long captionText;

        public long captionImage;

        public long itemText;

        public long itemImage;

        public PersistentUnityEventBase onValueChanged;

        #if !RT_PE_MAINTANANCE
        public PersistentOptionData[] options;
        #endif

        public int value;

	}
}

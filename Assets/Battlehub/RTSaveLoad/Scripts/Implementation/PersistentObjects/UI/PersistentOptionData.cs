#define RT_USE_PROTOBUF
using Battlehub.RTSaveLoad;
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif
using System.Collections.Generic;
/*This is auto-generated file. Tools->Runtime SaveLoad->Create Persistent Objects 
If you want prevent overwriting, drag this file to another folder.*/

namespace Battlehub.RTSaveLoad.PersistentObjects.UI
{
#if RT_USE_PROTOBUF
[ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
#endif

    [System.Serializable]
	public class PersistentOptionData : Battlehub.RTSaveLoad.PersistentData
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.UI.Dropdown.OptionData o = (UnityEngine.UI.Dropdown.OptionData)obj;
			o.text = text;
			o.image = (UnityEngine.Sprite)objects.Get(image);
			return o;
		}

        public override void FindDependencies<T>(Dictionary<long, T> dependencies, Dictionary<long, T> objects, bool allowNulls)
        {
            base.FindDependencies(dependencies, objects, allowNulls);
            AddDependency(image, dependencies, objects, allowNulls);
        }

        public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.UI.Dropdown.OptionData o = (UnityEngine.UI.Dropdown.OptionData)obj;
			text = o.text;
			image = o.image.GetMappedInstanceID();
		}

		
        public string text;

        public long image;
	}
}

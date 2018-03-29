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
	#endif
	[System.Serializable]
	public class PersistentWebCamTexture : Battlehub.RTSaveLoad.PersistentObjects.PersistentTexture
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.WebCamTexture o = (UnityEngine.WebCamTexture)obj;
			o.deviceName = deviceName;
			o.requestedFPS = requestedFPS;
			o.requestedWidth = requestedWidth;
			o.requestedHeight = requestedHeight;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.WebCamTexture o = (UnityEngine.WebCamTexture)obj;
			deviceName = o.deviceName;
			requestedFPS = o.requestedFPS;
			requestedWidth = o.requestedWidth;
			requestedHeight = o.requestedHeight;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public string deviceName;

		public float requestedFPS;

		public int requestedWidth;

		public int requestedHeight;

	}
}

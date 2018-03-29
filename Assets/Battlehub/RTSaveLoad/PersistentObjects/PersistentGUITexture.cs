#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif
using Battlehub.RTSaveLoad;
/*This is auto-generated file. Tools->Runtime SaveLoad->Create Persistent Objects 
If you want prevent overwriting, drag this file to another folder.*/

namespace Battlehub.RTSaveLoad.PersistentObjects
{
	#if RT_USE_PROTOBUF
	[ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
	#endif
	[System.Serializable]
	public class PersistentGUITexture : Battlehub.RTSaveLoad.PersistentObjects.PersistentGUIElement
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.GUITexture o = (UnityEngine.GUITexture)obj;
			o.color = color;
			o.texture = (UnityEngine.Texture)objects.Get(texture);
			o.pixelInset = pixelInset;
			o.border = border;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.GUITexture o = (UnityEngine.GUITexture)obj;
			color = o.color;
			texture = o.texture.GetMappedInstanceID();
			pixelInset = o.pixelInset;
			border = o.border;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			AddDependency(texture, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.GUITexture o = (UnityEngine.GUITexture)obj;
			AddDependency(o.texture, dependencies);
		}

		public UnityEngine.Color color;

		public long texture;

		public UnityEngine.Rect pixelInset;

		public UnityEngine.RectOffset border;

	}
}

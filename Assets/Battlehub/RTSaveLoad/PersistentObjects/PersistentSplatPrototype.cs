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
	public class PersistentSplatPrototype : Battlehub.RTSaveLoad.PersistentData
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.SplatPrototype o = (UnityEngine.SplatPrototype)obj;
			o.texture = (UnityEngine.Texture2D)objects.Get(texture);
			o.normalMap = (UnityEngine.Texture2D)objects.Get(normalMap);
			o.tileSize = tileSize;
			o.tileOffset = tileOffset;
			o.specular = specular;
			o.metallic = metallic;
			o.smoothness = smoothness;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.SplatPrototype o = (UnityEngine.SplatPrototype)obj;
			texture = o.texture.GetMappedInstanceID();
			normalMap = o.normalMap.GetMappedInstanceID();
			tileSize = o.tileSize;
			tileOffset = o.tileOffset;
			specular = o.specular;
			metallic = o.metallic;
			smoothness = o.smoothness;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			AddDependency(texture, dependencies, objects, allowNulls);
			AddDependency(normalMap, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.SplatPrototype o = (UnityEngine.SplatPrototype)obj;
			AddDependency(o.texture, dependencies);
			AddDependency(o.normalMap, dependencies);
		}

		public long texture;

		public long normalMap;

		public UnityEngine.Vector2 tileSize;

		public UnityEngine.Vector2 tileOffset;

		public UnityEngine.Color specular;

		public float metallic;

		public float smoothness;

	}
}

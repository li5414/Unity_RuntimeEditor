#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif

/*To autogenerate PersistentProceduralMaterial remove this file and run Tools->Runtime SaveLoad->Create Persistent Objects command*/
#if !UNITY_WEBGL
namespace Battlehub.RTSaveLoad.PersistentObjects
{

#if RT_USE_PROTOBUF
    [ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
#endif
	[System.Serializable]
	public class PersistentProceduralMaterial : PersistentMaterial
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.ProceduralMaterial o = (UnityEngine.ProceduralMaterial)obj;
			o.cacheSize = cacheSize;
			o.animationUpdateRate = animationUpdateRate;
			o.isLoadTimeGenerated = isLoadTimeGenerated;
			o.preset = preset;
			o.isReadable = isReadable;
            o.RebuildTexturesImmediately();
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.ProceduralMaterial o = (UnityEngine.ProceduralMaterial)obj;
			cacheSize = o.cacheSize;
			animationUpdateRate = o.animationUpdateRate;
			isLoadTimeGenerated = o.isLoadTimeGenerated;
			preset = o.preset;
			isReadable = o.isReadable;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

        public UnityEngine.ProceduralCacheSize cacheSize;

        public int animationUpdateRate;

        public bool isLoadTimeGenerated;

        public string preset;

        public bool isReadable;
	}
}
#endif
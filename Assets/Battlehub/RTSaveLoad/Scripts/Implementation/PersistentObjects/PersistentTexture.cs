#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif

/*To autogenerate PersistentTexture remove this file and run Tools->Runtime SaveLoad->Create Persistent Objects command*/

namespace Battlehub.RTSaveLoad.PersistentObjects
{
    #if RT_USE_PROTOBUF && !RT_PE_MAINTANANCE
    [ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
	[ProtoInclude(1074, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentProceduralTexture))]
	[ProtoInclude(1075, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentTexture2D))]
	[ProtoInclude(1076, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentCubemap))]
	[ProtoInclude(1077, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentTexture3D))]
	[ProtoInclude(1078, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentTexture2DArray))]
	[ProtoInclude(1079, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentCubemapArray))]
	[ProtoInclude(1080, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentSparseTexture))]
	[ProtoInclude(1081, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentRenderTexture))]
	[ProtoInclude(1082, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentMovieTexture))]
	[ProtoInclude(1083, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentWebCamTexture))]
    #endif
    [System.Serializable]
	public class PersistentTexture : PersistentObject
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.Texture o = (UnityEngine.Texture)obj;
            //o.width = width;
            //o.height = height;
            //o.dimension = dimension;
            o.filterMode = filterMode;
            o.anisoLevel = anisoLevel;
            o.wrapMode = wrapMode;
            o.mipMapBias = mipMapBias;
            return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.Texture o = (UnityEngine.Texture)obj;
			//width = o.width;
			//height = o.height;
			//dimension = o.dimension;
			filterMode = o.filterMode;
			anisoLevel = o.anisoLevel;
			wrapMode = o.wrapMode;
			mipMapBias = o.mipMapBias;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

        public int width;

        public int height;

        public UnityEngine.Rendering.TextureDimension dimension;

        public UnityEngine.FilterMode filterMode;

        public int anisoLevel;

        public UnityEngine.TextureWrapMode wrapMode;

        public float mipMapBias;

	}
}

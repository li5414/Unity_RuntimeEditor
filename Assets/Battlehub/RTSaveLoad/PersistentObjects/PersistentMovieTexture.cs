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
	public class PersistentMovieTexture : Battlehub.RTSaveLoad.PersistentObjects.PersistentTexture
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
#if !UNITY_ANDROID && !UNITY_IOS && !UNITY_WEBGL
            UnityEngine.MovieTexture o = (UnityEngine.MovieTexture)obj;
			o.loop = loop;
			return o;
#else
            return obj;
#endif
        }

        public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
#if !UNITY_ANDROID && !UNITY_IOS && !UNITY_WEBGL
			UnityEngine.MovieTexture o = (UnityEngine.MovieTexture)obj;
			loop = o.loop;
#endif
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public bool loop;

	}
}

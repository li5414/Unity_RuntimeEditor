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
	public class PersistentRenderTexture : Battlehub.RTSaveLoad.PersistentObjects.PersistentTexture
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.RenderTexture o = (UnityEngine.RenderTexture)obj;
			o.depth = depth;
			o.isPowerOfTwo = isPowerOfTwo;
			o.format = (UnityEngine.RenderTextureFormat)format;
			o.useMipMap = useMipMap;
			o.autoGenerateMips = autoGenerateMips;
			o.volumeDepth = volumeDepth;
			o.antiAliasing = antiAliasing;
			o.enableRandomWrite = enableRandomWrite;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.RenderTexture o = (UnityEngine.RenderTexture)obj;
			depth = o.depth;
			isPowerOfTwo = o.isPowerOfTwo;
			format = (uint)o.format;
			useMipMap = o.useMipMap;
			autoGenerateMips = o.autoGenerateMips;
			volumeDepth = o.volumeDepth;
			antiAliasing = o.antiAliasing;
			enableRandomWrite = o.enableRandomWrite;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public int depth;

		public bool isPowerOfTwo;

		public uint format;

		public bool useMipMap;

		public bool autoGenerateMips;

		public int volumeDepth;

		public int antiAliasing;

		public bool enableRandomWrite;

	}
}

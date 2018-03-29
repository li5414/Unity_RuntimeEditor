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
	public class PersistentAudioChorusFilter : Battlehub.RTSaveLoad.PersistentObjects.PersistentBehaviour
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.AudioChorusFilter o = (UnityEngine.AudioChorusFilter)obj;
			o.dryMix = dryMix;
			o.wetMix1 = wetMix1;
			o.wetMix2 = wetMix2;
			o.wetMix3 = wetMix3;
			o.delay = delay;
			o.rate = rate;
			o.depth = depth;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.AudioChorusFilter o = (UnityEngine.AudioChorusFilter)obj;
			dryMix = o.dryMix;
			wetMix1 = o.wetMix1;
			wetMix2 = o.wetMix2;
			wetMix3 = o.wetMix3;
			delay = o.delay;
			rate = o.rate;
			depth = o.depth;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public float dryMix;

		public float wetMix1;

		public float wetMix2;

		public float wetMix3;

		public float delay;

		public float rate;

		public float depth;

	}
}

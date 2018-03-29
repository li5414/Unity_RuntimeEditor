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
	public class PersistentBurst : Battlehub.RTSaveLoad.PersistentData
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.ParticleSystem.Burst o = (UnityEngine.ParticleSystem.Burst)obj;
			o.time = time;
			o.minCount = minCount;
			o.maxCount = maxCount;
			o.cycleCount = cycleCount;
			o.repeatInterval = repeatInterval;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.ParticleSystem.Burst o = (UnityEngine.ParticleSystem.Burst)obj;
			time = o.time;
			minCount = o.minCount;
			maxCount = o.maxCount;
			cycleCount = o.cycleCount;
			repeatInterval = o.repeatInterval;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public float time;

		public short minCount;

		public short maxCount;

		public int cycleCount;

		public float repeatInterval;

	}
}

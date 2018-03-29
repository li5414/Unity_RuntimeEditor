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
	public class PersistentAudioReverbZone : Battlehub.RTSaveLoad.PersistentObjects.PersistentBehaviour
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.AudioReverbZone o = (UnityEngine.AudioReverbZone)obj;
			o.minDistance = minDistance;
			o.maxDistance = maxDistance;
			o.reverbPreset = (UnityEngine.AudioReverbPreset)reverbPreset;
			o.room = room;
			o.roomHF = roomHF;
			o.roomLF = roomLF;
			o.decayTime = decayTime;
			o.decayHFRatio = decayHFRatio;
			o.reflections = reflections;
			o.reflectionsDelay = reflectionsDelay;
			o.reverb = reverb;
			o.reverbDelay = reverbDelay;
			o.HFReference = HFReference;
			o.LFReference = LFReference;
			o.diffusion = diffusion;
			o.density = density;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.AudioReverbZone o = (UnityEngine.AudioReverbZone)obj;
			minDistance = o.minDistance;
			maxDistance = o.maxDistance;
			reverbPreset = (uint)o.reverbPreset;
			room = o.room;
			roomHF = o.roomHF;
			roomLF = o.roomLF;
			decayTime = o.decayTime;
			decayHFRatio = o.decayHFRatio;
			reflections = o.reflections;
			reflectionsDelay = o.reflectionsDelay;
			reverb = o.reverb;
			reverbDelay = o.reverbDelay;
			HFReference = o.HFReference;
			LFReference = o.LFReference;
			diffusion = o.diffusion;
			density = o.density;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public float minDistance;

		public float maxDistance;

		public uint reverbPreset;

		public int room;

		public int roomHF;

		public int roomLF;

		public float decayTime;

		public float decayHFRatio;

		public int reflections;

		public float reflectionsDelay;

		public int reverb;

		public float reverbDelay;

		public float HFReference;

		public float LFReference;

		public float diffusion;

		public float density;

	}
}

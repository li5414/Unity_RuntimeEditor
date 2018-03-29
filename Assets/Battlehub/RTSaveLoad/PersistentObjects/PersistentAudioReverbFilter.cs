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
	public class PersistentAudioReverbFilter : Battlehub.RTSaveLoad.PersistentObjects.PersistentBehaviour
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.AudioReverbFilter o = (UnityEngine.AudioReverbFilter)obj;
			o.reverbPreset = (UnityEngine.AudioReverbPreset)reverbPreset;
			o.dryLevel = dryLevel;
			o.room = room;
			o.roomHF = roomHF;
			o.decayTime = decayTime;
			o.decayHFRatio = decayHFRatio;
			o.reflectionsLevel = reflectionsLevel;
			o.reflectionsDelay = reflectionsDelay;
			o.reverbLevel = reverbLevel;
			o.reverbDelay = reverbDelay;
			o.diffusion = diffusion;
			o.density = density;
			o.hfReference = hfReference;
			o.roomLF = roomLF;
			o.lfReference = lfReference;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.AudioReverbFilter o = (UnityEngine.AudioReverbFilter)obj;
			reverbPreset = (uint)o.reverbPreset;
			dryLevel = o.dryLevel;
			room = o.room;
			roomHF = o.roomHF;
			decayTime = o.decayTime;
			decayHFRatio = o.decayHFRatio;
			reflectionsLevel = o.reflectionsLevel;
			reflectionsDelay = o.reflectionsDelay;
			reverbLevel = o.reverbLevel;
			reverbDelay = o.reverbDelay;
			diffusion = o.diffusion;
			density = o.density;
			hfReference = o.hfReference;
			roomLF = o.roomLF;
			lfReference = o.lfReference;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public uint reverbPreset;

		public float dryLevel;

		public float room;

		public float roomHF;

		public float decayTime;

		public float decayHFRatio;

		public float reflectionsLevel;

		public float reflectionsDelay;

		public float reverbLevel;

		public float reverbDelay;

		public float diffusion;

		public float density;

		public float hfReference;

		public float roomLF;

		public float lfReference;

	}
}

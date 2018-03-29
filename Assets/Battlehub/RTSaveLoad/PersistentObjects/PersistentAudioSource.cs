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
	public class PersistentAudioSource : Battlehub.RTSaveLoad.PersistentObjects.PersistentBehaviour
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.AudioSource o = (UnityEngine.AudioSource)obj;
			o.volume = volume;
			o.pitch = pitch;
			o.time = time;
			o.timeSamples = timeSamples;
			o.clip = (UnityEngine.AudioClip)objects.Get(clip);
			o.outputAudioMixerGroup = (UnityEngine.Audio.AudioMixerGroup)objects.Get(outputAudioMixerGroup);
			o.loop = loop;
			o.ignoreListenerVolume = ignoreListenerVolume;
			o.playOnAwake = playOnAwake;
			o.ignoreListenerPause = ignoreListenerPause;
			o.velocityUpdateMode = (UnityEngine.AudioVelocityUpdateMode)velocityUpdateMode;
			o.panStereo = panStereo;
			o.spatialBlend = spatialBlend;
			o.spatialize = spatialize;
			o.spatializePostEffects = spatializePostEffects;
			o.reverbZoneMix = reverbZoneMix;
			o.bypassEffects = bypassEffects;
			o.bypassListenerEffects = bypassListenerEffects;
			o.bypassReverbZones = bypassReverbZones;
			o.dopplerLevel = dopplerLevel;
			o.spread = spread;
			o.priority = priority;
			o.mute = mute;
			o.minDistance = minDistance;
			o.maxDistance = maxDistance;
			o.rolloffMode = (UnityEngine.AudioRolloffMode)rolloffMode;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.AudioSource o = (UnityEngine.AudioSource)obj;
			volume = o.volume;
			pitch = o.pitch;
			time = o.time;
			timeSamples = o.timeSamples;
			clip = o.clip.GetMappedInstanceID();
			outputAudioMixerGroup = o.outputAudioMixerGroup.GetMappedInstanceID();
			loop = o.loop;
			ignoreListenerVolume = o.ignoreListenerVolume;
			playOnAwake = o.playOnAwake;
			ignoreListenerPause = o.ignoreListenerPause;
			velocityUpdateMode = (uint)o.velocityUpdateMode;
			panStereo = o.panStereo;
			spatialBlend = o.spatialBlend;
			spatialize = o.spatialize;
			spatializePostEffects = o.spatializePostEffects;
			reverbZoneMix = o.reverbZoneMix;
			bypassEffects = o.bypassEffects;
			bypassListenerEffects = o.bypassListenerEffects;
			bypassReverbZones = o.bypassReverbZones;
			dopplerLevel = o.dopplerLevel;
			spread = o.spread;
			priority = o.priority;
			mute = o.mute;
			minDistance = o.minDistance;
			maxDistance = o.maxDistance;
			rolloffMode = (uint)o.rolloffMode;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			AddDependency(clip, dependencies, objects, allowNulls);
			AddDependency(outputAudioMixerGroup, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.AudioSource o = (UnityEngine.AudioSource)obj;
			AddDependency(o.clip, dependencies);
			AddDependency(o.outputAudioMixerGroup, dependencies);
		}

		public float volume;

		public float pitch;

		public float time;

		public int timeSamples;

		public long clip;

		public long outputAudioMixerGroup;

		public bool loop;

		public bool ignoreListenerVolume;

		public bool playOnAwake;

		public bool ignoreListenerPause;

		public uint velocityUpdateMode;

		public float panStereo;

		public float spatialBlend;

		public bool spatialize;

		public bool spatializePostEffects;

		public float reverbZoneMix;

		public bool bypassEffects;

		public bool bypassListenerEffects;

		public bool bypassReverbZones;

		public float dopplerLevel;

		public float spread;

		public int priority;

		public bool mute;

		public float minDistance;

		public float maxDistance;

		public uint rolloffMode;

	}
}

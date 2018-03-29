#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif
using Battlehub.RTSaveLoad;
/*This is auto-generated file. Tools->Runtime SaveLoad->Create Persistent Objects 
If you want prevent overwriting, drag this file to another folder.*/

namespace Battlehub.RTSaveLoad.PersistentObjects.Audio
{
	#if RT_USE_PROTOBUF
	[ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
	#endif
	[System.Serializable]
	public class PersistentAudioMixer : Battlehub.RTSaveLoad.PersistentObjects.PersistentObject
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.Audio.AudioMixer o = (UnityEngine.Audio.AudioMixer)obj;
			o.outputAudioMixerGroup = (UnityEngine.Audio.AudioMixerGroup)objects.Get(outputAudioMixerGroup);
			o.updateMode = (UnityEngine.Audio.AudioMixerUpdateMode)updateMode;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.Audio.AudioMixer o = (UnityEngine.Audio.AudioMixer)obj;
			outputAudioMixerGroup = o.outputAudioMixerGroup.GetMappedInstanceID();
			updateMode = (uint)o.updateMode;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			AddDependency(outputAudioMixerGroup, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.Audio.AudioMixer o = (UnityEngine.Audio.AudioMixer)obj;
			AddDependency(o.outputAudioMixerGroup, dependencies);
		}

		public long outputAudioMixerGroup;

		public uint updateMode;

	}
}

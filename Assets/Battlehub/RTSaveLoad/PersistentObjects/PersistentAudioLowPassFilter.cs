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
	public class PersistentAudioLowPassFilter : Battlehub.RTSaveLoad.PersistentObjects.PersistentBehaviour
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.AudioLowPassFilter o = (UnityEngine.AudioLowPassFilter)obj;
			o.cutoffFrequency = cutoffFrequency;
			o.customCutoffCurve = Write(o.customCutoffCurve, customCutoffCurve, objects);
			o.lowpassResonanceQ = lowpassResonanceQ;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.AudioLowPassFilter o = (UnityEngine.AudioLowPassFilter)obj;
			cutoffFrequency = o.cutoffFrequency;
			customCutoffCurve = Read(customCutoffCurve, o.customCutoffCurve);
			lowpassResonanceQ = o.lowpassResonanceQ;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			FindDependencies(customCutoffCurve, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.AudioLowPassFilter o = (UnityEngine.AudioLowPassFilter)obj;
			GetDependencies(customCutoffCurve, o.customCutoffCurve, dependencies);
		}

		public float cutoffFrequency;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentAnimationCurve customCutoffCurve;

		public float lowpassResonanceQ;

	}
}

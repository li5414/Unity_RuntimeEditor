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
	public class PersistentWindZone : Battlehub.RTSaveLoad.PersistentObjects.PersistentComponent
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.WindZone o = (UnityEngine.WindZone)obj;
			o.mode = (UnityEngine.WindZoneMode)mode;
			o.radius = radius;
			o.windMain = windMain;
			o.windTurbulence = windTurbulence;
			o.windPulseMagnitude = windPulseMagnitude;
			o.windPulseFrequency = windPulseFrequency;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.WindZone o = (UnityEngine.WindZone)obj;
			mode = (uint)o.mode;
			radius = o.radius;
			windMain = o.windMain;
			windTurbulence = o.windTurbulence;
			windPulseMagnitude = o.windPulseMagnitude;
			windPulseFrequency = o.windPulseFrequency;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public uint mode;

		public float radius;

		public float windMain;

		public float windTurbulence;

		public float windPulseMagnitude;

		public float windPulseFrequency;

	}
}

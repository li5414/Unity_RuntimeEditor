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
	public class PersistentSizeBySpeedModule : Battlehub.RTSaveLoad.PersistentData
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.ParticleSystem.SizeBySpeedModule o = (UnityEngine.ParticleSystem.SizeBySpeedModule)obj;
			o.enabled = enabled;
			o.size = Write(o.size, size, objects);
			o.sizeMultiplier = sizeMultiplier;
			o.x = Write(o.x, x, objects);
			o.xMultiplier = xMultiplier;
			o.y = Write(o.y, y, objects);
			o.yMultiplier = yMultiplier;
			o.z = Write(o.z, z, objects);
			o.zMultiplier = zMultiplier;
			o.separateAxes = separateAxes;
			o.range = range;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.ParticleSystem.SizeBySpeedModule o = (UnityEngine.ParticleSystem.SizeBySpeedModule)obj;
			enabled = o.enabled;
			size = Read(size, o.size);
			sizeMultiplier = o.sizeMultiplier;
			x = Read(x, o.x);
			xMultiplier = o.xMultiplier;
			y = Read(y, o.y);
			yMultiplier = o.yMultiplier;
			z = Read(z, o.z);
			zMultiplier = o.zMultiplier;
			separateAxes = o.separateAxes;
			range = o.range;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			FindDependencies(size, dependencies, objects, allowNulls);
			FindDependencies(x, dependencies, objects, allowNulls);
			FindDependencies(y, dependencies, objects, allowNulls);
			FindDependencies(z, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.ParticleSystem.SizeBySpeedModule o = (UnityEngine.ParticleSystem.SizeBySpeedModule)obj;
			GetDependencies(size, o.size, dependencies);
			GetDependencies(x, o.x, dependencies);
			GetDependencies(y, o.y, dependencies);
			GetDependencies(z, o.z, dependencies);
		}

		public bool enabled;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve size;

		public float sizeMultiplier;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve x;

		public float xMultiplier;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve y;

		public float yMultiplier;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentMinMaxCurve z;

		public float zMultiplier;

		public bool separateAxes;

		public UnityEngine.Vector2 range;

	}
}

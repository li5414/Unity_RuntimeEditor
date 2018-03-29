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
	public class PersistentDetailPrototype : Battlehub.RTSaveLoad.PersistentData
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.DetailPrototype o = (UnityEngine.DetailPrototype)obj;
			o.prototype = (UnityEngine.GameObject)objects.Get(prototype);
			o.prototypeTexture = (UnityEngine.Texture2D)objects.Get(prototypeTexture);
			o.minWidth = minWidth;
			o.maxWidth = maxWidth;
			o.minHeight = minHeight;
			o.maxHeight = maxHeight;
			o.noiseSpread = noiseSpread;
			o.bendFactor = bendFactor;
			o.healthyColor = healthyColor;
			o.dryColor = dryColor;
			o.renderMode = (UnityEngine.DetailRenderMode)renderMode;
			o.usePrototypeMesh = usePrototypeMesh;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.DetailPrototype o = (UnityEngine.DetailPrototype)obj;
			prototype = o.prototype.GetMappedInstanceID();
			prototypeTexture = o.prototypeTexture.GetMappedInstanceID();
			minWidth = o.minWidth;
			maxWidth = o.maxWidth;
			minHeight = o.minHeight;
			maxHeight = o.maxHeight;
			noiseSpread = o.noiseSpread;
			bendFactor = o.bendFactor;
			healthyColor = o.healthyColor;
			dryColor = o.dryColor;
			renderMode = (uint)o.renderMode;
			usePrototypeMesh = o.usePrototypeMesh;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			AddDependency(prototype, dependencies, objects, allowNulls);
			AddDependency(prototypeTexture, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.DetailPrototype o = (UnityEngine.DetailPrototype)obj;
			AddDependency(o.prototype, dependencies);
			AddDependency(o.prototypeTexture, dependencies);
		}

		public long prototype;

		public long prototypeTexture;

		public float minWidth;

		public float maxWidth;

		public float minHeight;

		public float maxHeight;

		public float noiseSpread;

		public float bendFactor;

		public UnityEngine.Color healthyColor;

		public UnityEngine.Color dryColor;

		public uint renderMode;

		public bool usePrototypeMesh;

	}
}

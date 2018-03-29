#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif
using Battlehub.RTSaveLoad;
/*This is auto-generated file. Tools->Runtime SaveLoad->Create Persistent Objects 
If you want prevent overwriting, drag this file to another folder.*/

namespace Battlehub.RTSaveLoad.PersistentObjects.AI
{
	#if RT_USE_PROTOBUF
	[ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
	#endif
	[System.Serializable]
	public class PersistentOffMeshLink : Battlehub.RTSaveLoad.PersistentObjects.PersistentBehaviour
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.AI.OffMeshLink o = (UnityEngine.AI.OffMeshLink)obj;
			o.activated = activated;
			o.costOverride = costOverride;
			o.biDirectional = biDirectional;
			o.area = area;
			o.autoUpdatePositions = autoUpdatePositions;
			o.startTransform = (UnityEngine.Transform)objects.Get(startTransform);
			o.endTransform = (UnityEngine.Transform)objects.Get(endTransform);
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.AI.OffMeshLink o = (UnityEngine.AI.OffMeshLink)obj;
			activated = o.activated;
			costOverride = o.costOverride;
			biDirectional = o.biDirectional;
			area = o.area;
			autoUpdatePositions = o.autoUpdatePositions;
			startTransform = o.startTransform.GetMappedInstanceID();
			endTransform = o.endTransform.GetMappedInstanceID();
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			AddDependency(startTransform, dependencies, objects, allowNulls);
			AddDependency(endTransform, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.AI.OffMeshLink o = (UnityEngine.AI.OffMeshLink)obj;
			AddDependency(o.startTransform, dependencies);
			AddDependency(o.endTransform, dependencies);
		}

		public bool activated;

		public float costOverride;

		public bool biDirectional;

		public int area;

		public bool autoUpdatePositions;

		public long startTransform;

		public long endTransform;

	}
}

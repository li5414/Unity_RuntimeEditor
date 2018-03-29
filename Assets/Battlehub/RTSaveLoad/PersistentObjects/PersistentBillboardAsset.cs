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
	public class PersistentBillboardAsset : Battlehub.RTSaveLoad.PersistentObjects.PersistentObject
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.BillboardAsset o = (UnityEngine.BillboardAsset)obj;
			o.width = width;
			o.height = height;
			o.bottom = bottom;
			o.material = (UnityEngine.Material)objects.Get(material);
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.BillboardAsset o = (UnityEngine.BillboardAsset)obj;
			width = o.width;
			height = o.height;
			bottom = o.bottom;
			material = o.material.GetMappedInstanceID();
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			AddDependency(material, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.BillboardAsset o = (UnityEngine.BillboardAsset)obj;
			AddDependency(o.material, dependencies);
		}

		public float width;

		public float height;

		public float bottom;

		public long material;

	}
}

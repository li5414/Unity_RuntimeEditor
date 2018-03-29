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
	public class PersistentProjector : Battlehub.RTSaveLoad.PersistentObjects.PersistentBehaviour
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.Projector o = (UnityEngine.Projector)obj;
			o.nearClipPlane = nearClipPlane;
			o.farClipPlane = farClipPlane;
			o.fieldOfView = fieldOfView;
			o.aspectRatio = aspectRatio;
			o.orthographic = orthographic;
			o.orthographicSize = orthographicSize;
			o.ignoreLayers = ignoreLayers;
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
			UnityEngine.Projector o = (UnityEngine.Projector)obj;
			nearClipPlane = o.nearClipPlane;
			farClipPlane = o.farClipPlane;
			fieldOfView = o.fieldOfView;
			aspectRatio = o.aspectRatio;
			orthographic = o.orthographic;
			orthographicSize = o.orthographicSize;
			ignoreLayers = o.ignoreLayers;
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
			UnityEngine.Projector o = (UnityEngine.Projector)obj;
			AddDependency(o.material, dependencies);
		}

		public float nearClipPlane;

		public float farClipPlane;

		public float fieldOfView;

		public float aspectRatio;

		public bool orthographic;

		public float orthographicSize;

		public int ignoreLayers;

		public long material;

	}
}

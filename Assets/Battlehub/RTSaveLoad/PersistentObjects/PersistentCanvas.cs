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
	public class PersistentCanvas : Battlehub.RTSaveLoad.PersistentObjects.PersistentBehaviour
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.Canvas o = (UnityEngine.Canvas)obj;
			o.renderMode = (UnityEngine.RenderMode)renderMode;
			o.worldCamera = (UnityEngine.Camera)objects.Get(worldCamera);
			o.scaleFactor = scaleFactor;
			o.referencePixelsPerUnit = referencePixelsPerUnit;
			o.overridePixelPerfect = overridePixelPerfect;
			o.pixelPerfect = pixelPerfect;
			o.planeDistance = planeDistance;
			o.overrideSorting = overrideSorting;
			o.sortingOrder = sortingOrder;
			o.targetDisplay = targetDisplay;
			o.normalizedSortingGridSize = normalizedSortingGridSize;
			o.sortingLayerID = sortingLayerID;
			o.additionalShaderChannels = (UnityEngine.AdditionalCanvasShaderChannels)additionalShaderChannels;
			o.sortingLayerName = sortingLayerName;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.Canvas o = (UnityEngine.Canvas)obj;
			renderMode = (uint)o.renderMode;
			worldCamera = o.worldCamera.GetMappedInstanceID();
			scaleFactor = o.scaleFactor;
			referencePixelsPerUnit = o.referencePixelsPerUnit;
			overridePixelPerfect = o.overridePixelPerfect;
			pixelPerfect = o.pixelPerfect;
			planeDistance = o.planeDistance;
			overrideSorting = o.overrideSorting;
			sortingOrder = o.sortingOrder;
			targetDisplay = o.targetDisplay;
			normalizedSortingGridSize = o.normalizedSortingGridSize;
			sortingLayerID = o.sortingLayerID;
			additionalShaderChannels = (uint)o.additionalShaderChannels;
			sortingLayerName = o.sortingLayerName;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			AddDependency(worldCamera, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.Canvas o = (UnityEngine.Canvas)obj;
			AddDependency(o.worldCamera, dependencies);
		}

		public uint renderMode;

		public long worldCamera;

		public float scaleFactor;

		public float referencePixelsPerUnit;

		public bool overridePixelPerfect;

		public bool pixelPerfect;

		public float planeDistance;

		public bool overrideSorting;

		public int sortingOrder;

		public int targetDisplay;

		public float normalizedSortingGridSize;

		public int sortingLayerID;

		public uint additionalShaderChannels;

		public string sortingLayerName;

	}
}

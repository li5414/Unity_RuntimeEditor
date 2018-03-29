#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif
/*This is auto-generated file. Tools->Runtime SaveLoad->Create Persistent Objects 
If you want prevent overwriting, drag this file to another folder.*/

namespace Battlehub.RTSaveLoad.PersistentObjects.UI
{
	#if RT_USE_PROTOBUF
	[ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
	#endif
	[System.Serializable]
	public class PersistentCanvasScaler : Battlehub.RTSaveLoad.PersistentObjects.EventSystems.PersistentUIBehaviour
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.UI.CanvasScaler o = (UnityEngine.UI.CanvasScaler)obj;
			o.uiScaleMode = (UnityEngine.UI.CanvasScaler.ScaleMode)uiScaleMode;
			o.referencePixelsPerUnit = referencePixelsPerUnit;
			o.scaleFactor = scaleFactor;
			o.referenceResolution = referenceResolution;
			o.screenMatchMode = (UnityEngine.UI.CanvasScaler.ScreenMatchMode)screenMatchMode;
			o.matchWidthOrHeight = matchWidthOrHeight;
			o.physicalUnit = (UnityEngine.UI.CanvasScaler.Unit)physicalUnit;
			o.fallbackScreenDPI = fallbackScreenDPI;
			o.defaultSpriteDPI = defaultSpriteDPI;
			o.dynamicPixelsPerUnit = dynamicPixelsPerUnit;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.UI.CanvasScaler o = (UnityEngine.UI.CanvasScaler)obj;
			uiScaleMode = (uint)o.uiScaleMode;
			referencePixelsPerUnit = o.referencePixelsPerUnit;
			scaleFactor = o.scaleFactor;
			referenceResolution = o.referenceResolution;
			screenMatchMode = (uint)o.screenMatchMode;
			matchWidthOrHeight = o.matchWidthOrHeight;
			physicalUnit = (uint)o.physicalUnit;
			fallbackScreenDPI = o.fallbackScreenDPI;
			defaultSpriteDPI = o.defaultSpriteDPI;
			dynamicPixelsPerUnit = o.dynamicPixelsPerUnit;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public uint uiScaleMode;

		public float referencePixelsPerUnit;

		public float scaleFactor;

		public UnityEngine.Vector2 referenceResolution;

		public uint screenMatchMode;

		public float matchWidthOrHeight;

		public uint physicalUnit;

		public float fallbackScreenDPI;

		public float defaultSpriteDPI;

		public float dynamicPixelsPerUnit;

	}
}

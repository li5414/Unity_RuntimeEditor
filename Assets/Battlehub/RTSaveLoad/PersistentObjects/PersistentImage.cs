#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif
using Battlehub.RTSaveLoad;
/*This is auto-generated file. Tools->Runtime SaveLoad->Create Persistent Objects 
If you want prevent overwriting, drag this file to another folder.*/

namespace Battlehub.RTSaveLoad.PersistentObjects.UI
{
	#if RT_USE_PROTOBUF
	[ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
	#endif
	[System.Serializable]
	public class PersistentImage : Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentMaskableGraphic
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.UI.Image o = (UnityEngine.UI.Image)obj;
			o.sprite = (UnityEngine.Sprite)objects.Get(sprite);
			o.overrideSprite = (UnityEngine.Sprite)objects.Get(overrideSprite);
			o.type = (UnityEngine.UI.Image.Type)type;
			o.preserveAspect = preserveAspect;
			o.fillCenter = fillCenter;
			o.fillMethod = (UnityEngine.UI.Image.FillMethod)fillMethod;
			o.fillAmount = fillAmount;
			o.fillClockwise = fillClockwise;
			o.fillOrigin = fillOrigin;
			o.alphaHitTestMinimumThreshold = alphaHitTestMinimumThreshold;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.UI.Image o = (UnityEngine.UI.Image)obj;
			sprite = o.sprite.GetMappedInstanceID();
			overrideSprite = o.overrideSprite.GetMappedInstanceID();
			type = (uint)o.type;
			preserveAspect = o.preserveAspect;
			fillCenter = o.fillCenter;
			fillMethod = (uint)o.fillMethod;
			fillAmount = o.fillAmount;
			fillClockwise = o.fillClockwise;
			fillOrigin = o.fillOrigin;
			alphaHitTestMinimumThreshold = o.alphaHitTestMinimumThreshold;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			AddDependency(sprite, dependencies, objects, allowNulls);
			AddDependency(overrideSprite, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.UI.Image o = (UnityEngine.UI.Image)obj;
			AddDependency(o.sprite, dependencies);
			AddDependency(o.overrideSprite, dependencies);
		}

		public long sprite;

		public long overrideSprite;

		public uint type;

		public bool preserveAspect;

		public bool fillCenter;

		public uint fillMethod;

		public float fillAmount;

		public bool fillClockwise;

		public int fillOrigin;

		public float alphaHitTestMinimumThreshold;

	}
}

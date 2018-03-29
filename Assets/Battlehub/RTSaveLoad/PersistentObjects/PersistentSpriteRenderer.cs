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
	public class PersistentSpriteRenderer : Battlehub.RTSaveLoad.PersistentObjects.PersistentRenderer
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.SpriteRenderer o = (UnityEngine.SpriteRenderer)obj;
			o.sprite = (UnityEngine.Sprite)objects.Get(sprite);
			o.drawMode = (UnityEngine.SpriteDrawMode)drawMode;
			o.size = size;
			o.adaptiveModeThreshold = adaptiveModeThreshold;
			o.tileMode = (UnityEngine.SpriteTileMode)tileMode;
			o.color = color;
			o.flipX = flipX;
			o.flipY = flipY;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.SpriteRenderer o = (UnityEngine.SpriteRenderer)obj;
			sprite = o.sprite.GetMappedInstanceID();
			drawMode = (uint)o.drawMode;
			size = o.size;
			adaptiveModeThreshold = o.adaptiveModeThreshold;
			tileMode = (uint)o.tileMode;
			color = o.color;
			flipX = o.flipX;
			flipY = o.flipY;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			AddDependency(sprite, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.SpriteRenderer o = (UnityEngine.SpriteRenderer)obj;
			AddDependency(o.sprite, dependencies);
		}

		public long sprite;

		public uint drawMode;

		public UnityEngine.Vector2 size;

		public float adaptiveModeThreshold;

		public uint tileMode;

		public UnityEngine.Color color;

		public bool flipX;

		public bool flipY;

	}
}

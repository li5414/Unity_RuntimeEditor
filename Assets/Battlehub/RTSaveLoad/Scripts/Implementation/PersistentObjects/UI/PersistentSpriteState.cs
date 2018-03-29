#define RT_USE_PROTOBUF
using Battlehub.RTSaveLoad;
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
	public class PersistentSpriteState : Battlehub.RTSaveLoad.PersistentData
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.UI.SpriteState o = (UnityEngine.UI.SpriteState)obj;
			o.highlightedSprite = (UnityEngine.Sprite)objects.Get(highlightedSprite);
			o.pressedSprite = (UnityEngine.Sprite)objects.Get(pressedSprite);
			o.disabledSprite = (UnityEngine.Sprite)objects.Get(disabledSprite);
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.UI.SpriteState o = (UnityEngine.UI.SpriteState)obj;
			highlightedSprite = o.highlightedSprite.GetMappedInstanceID();
			pressedSprite = o.pressedSprite.GetMappedInstanceID();
			disabledSprite = o.disabledSprite.GetMappedInstanceID();
		}

        public long highlightedSprite;
        public long pressedSprite;
        public long disabledSprite;

	}
}

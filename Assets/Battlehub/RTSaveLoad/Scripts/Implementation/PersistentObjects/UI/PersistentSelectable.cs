#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif
using Battlehub.RTSaveLoad;
/*This is auto-generated file. Tools->Runtime SaveLoad->Create Persistent Objects 
If you want prevent overwriting, drag this file to another folder.*/

namespace Battlehub.RTSaveLoad.PersistentObjects.UI
{
     #if RT_USE_PROTOBUF && !RT_PE_MAINTANANCE
    [ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
	[ProtoInclude(1131, typeof(Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentButton))]
	[ProtoInclude(1132, typeof(Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentDropdown))]
	[ProtoInclude(1133, typeof(Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentInputField))]
	[ProtoInclude(1134, typeof(Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentScrollbar))]
	[ProtoInclude(1135, typeof(Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentSlider))]
	[ProtoInclude(1136, typeof(Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentToggle))]
	#endif
	[System.Serializable]
	public class PersistentSelectable
#if RT_PE_MAINTANANCE
        : PersistentData
#else
        : Battlehub.RTSaveLoad.PersistentObjects.EventSystems.PersistentUIBehaviour
#endif
    {
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.UI.Selectable o = (UnityEngine.UI.Selectable)obj;
            o.navigation = new UnityEngine.UI.Navigation();
            navigation.WriteTo(o.navigation, objects);
			o.transition = transition;
			o.colors = colors;
            o.spriteState = new UnityEngine.UI.SpriteState();
            spriteState.WriteTo(o.spriteState, objects);

			o.animationTriggers = animationTriggers;
			o.targetGraphic = (UnityEngine.UI.Graphic)objects.Get(targetGraphic);
			o.interactable = interactable;
			o.image = (UnityEngine.UI.Image)objects.Get(image);
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.UI.Selectable o = (UnityEngine.UI.Selectable)obj;
            navigation = new PersistentNavigation();
            navigation.ReadFrom(o.navigation);
			transition = o.transition;
			colors = o.colors;
            spriteState = new PersistentSpriteState();
            spriteState.ReadFrom(o.spriteState);
			animationTriggers = o.animationTriggers;
			targetGraphic = o.targetGraphic.GetMappedInstanceID();
			interactable = o.interactable;
			image = o.image.GetMappedInstanceID();
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			AddDependency(targetGraphic, dependencies, objects, allowNulls);
			AddDependency(image, dependencies, objects, allowNulls);
            if(navigation != null)
            {
                navigation.FindDependencies(dependencies, objects, allowNulls);
            }
            if(spriteState != null)
            {
                spriteState.FindDependencies(dependencies, objects, allowNulls);
            }
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.UI.Selectable o = (UnityEngine.UI.Selectable)obj;
			AddDependency(o.targetGraphic, dependencies);
			AddDependency(o.image, dependencies);

            PersistentNavigation navigation = new PersistentNavigation();
            navigation.GetDependencies(o.navigation, dependencies);

            PersistentSpriteState spriteState = new PersistentSpriteState();
            spriteState.GetDependencies(o.spriteState, dependencies);
		}

        public PersistentNavigation navigation;

        public UnityEngine.UI.Selectable.Transition transition;

        public UnityEngine.UI.ColorBlock colors;

        public PersistentSpriteState spriteState;

        public UnityEngine.UI.AnimationTriggers animationTriggers;

        public long targetGraphic;

        public bool interactable;

        public long image;

	}
}

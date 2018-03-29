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
	#endif
	[System.Serializable]
	public class PersistentScrollRect
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
			UnityEngine.UI.ScrollRect o = (UnityEngine.UI.ScrollRect)obj;
			o.content = (UnityEngine.RectTransform)objects.Get(content);
			o.horizontal = horizontal;
			o.vertical = vertical;
			o.movementType = movementType;
			o.elasticity = elasticity;
			o.inertia = inertia;
			o.decelerationRate = decelerationRate;
			o.scrollSensitivity = scrollSensitivity;
			o.viewport = (UnityEngine.RectTransform)objects.Get(viewport);
			o.horizontalScrollbar = (UnityEngine.UI.Scrollbar)objects.Get(horizontalScrollbar);
			o.verticalScrollbar = (UnityEngine.UI.Scrollbar)objects.Get(verticalScrollbar);
			o.horizontalScrollbarVisibility = horizontalScrollbarVisibility;
			o.verticalScrollbarVisibility = verticalScrollbarVisibility;
			o.horizontalScrollbarSpacing = horizontalScrollbarSpacing;
			o.verticalScrollbarSpacing = verticalScrollbarSpacing;
            onValueChanged.WriteTo(o.onValueChanged, objects);
            o.velocity = velocity;
			o.normalizedPosition = normalizedPosition;
			o.horizontalNormalizedPosition = horizontalNormalizedPosition;
			o.verticalNormalizedPosition = verticalNormalizedPosition;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.UI.ScrollRect o = (UnityEngine.UI.ScrollRect)obj;
			content = o.content.GetMappedInstanceID();
			horizontal = o.horizontal;
			vertical = o.vertical;
			movementType = o.movementType;
			elasticity = o.elasticity;
			inertia = o.inertia;
			decelerationRate = o.decelerationRate;
			scrollSensitivity = o.scrollSensitivity;
			viewport = o.viewport.GetMappedInstanceID();
			horizontalScrollbar = o.horizontalScrollbar.GetMappedInstanceID();
			verticalScrollbar = o.verticalScrollbar.GetMappedInstanceID();
			horizontalScrollbarVisibility = o.horizontalScrollbarVisibility;
			verticalScrollbarVisibility = o.verticalScrollbarVisibility;
			horizontalScrollbarSpacing = o.horizontalScrollbarSpacing;
			verticalScrollbarSpacing = o.verticalScrollbarSpacing;
            onValueChanged = new PersistentUnityEventBase();
            onValueChanged.ReadFrom(o.onValueChanged);
			velocity = o.velocity;
			normalizedPosition = o.normalizedPosition;
			horizontalNormalizedPosition = o.horizontalNormalizedPosition;
			verticalNormalizedPosition = o.verticalNormalizedPosition;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			AddDependency(content, dependencies, objects, allowNulls);
			AddDependency(viewport, dependencies, objects, allowNulls);
			AddDependency(horizontalScrollbar, dependencies, objects, allowNulls);
			AddDependency(verticalScrollbar, dependencies, objects, allowNulls);

            onValueChanged.FindDependencies(dependencies, objects, allowNulls);
        }

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.UI.ScrollRect o = (UnityEngine.UI.ScrollRect)obj;
			AddDependency(o.content, dependencies);
			AddDependency(o.viewport, dependencies);
			AddDependency(o.horizontalScrollbar, dependencies);
			AddDependency(o.verticalScrollbar, dependencies);

            PersistentUnityEventBase evtBase = new PersistentUnityEventBase();
            evtBase.GetDependencies(o.onValueChanged, dependencies);
		}
        
        public long content;
		
        public bool horizontal;
		
        public bool vertical;
		
        public UnityEngine.UI.ScrollRect.MovementType movementType;
		
        public float elasticity;
		
        public bool inertia;
		
        public float decelerationRate;
		
        public float scrollSensitivity;

        public long viewport;

        public long horizontalScrollbar;

        public long verticalScrollbar;
		
        public UnityEngine.UI.ScrollRect.ScrollbarVisibility horizontalScrollbarVisibility;

        public UnityEngine.UI.ScrollRect.ScrollbarVisibility verticalScrollbarVisibility;
		
        public float horizontalScrollbarSpacing;

        public float verticalScrollbarSpacing;
				
        public PersistentUnityEventBase onValueChanged;
		
        public UnityEngine.Vector2 velocity;
		
        public UnityEngine.Vector2 normalizedPosition;
		
        public float horizontalNormalizedPosition;
		
        public float verticalNormalizedPosition;

	}
}

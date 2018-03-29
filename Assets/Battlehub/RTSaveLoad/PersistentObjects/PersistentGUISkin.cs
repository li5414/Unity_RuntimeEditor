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
	public class PersistentGUISkin : Battlehub.RTSaveLoad.PersistentObjects.PersistentScriptableObject
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.GUISkin o = (UnityEngine.GUISkin)obj;
			o.font = (UnityEngine.Font)objects.Get(font);
			o.box = Write(o.box, box, objects);
			o.label = Write(o.label, label, objects);
			o.textField = Write(o.textField, textField, objects);
			o.textArea = Write(o.textArea, textArea, objects);
			o.button = Write(o.button, button, objects);
			o.toggle = Write(o.toggle, toggle, objects);
			o.window = Write(o.window, window, objects);
			o.horizontalSlider = Write(o.horizontalSlider, horizontalSlider, objects);
			o.horizontalSliderThumb = Write(o.horizontalSliderThumb, horizontalSliderThumb, objects);
			o.verticalSlider = Write(o.verticalSlider, verticalSlider, objects);
			o.verticalSliderThumb = Write(o.verticalSliderThumb, verticalSliderThumb, objects);
			o.horizontalScrollbar = Write(o.horizontalScrollbar, horizontalScrollbar, objects);
			o.horizontalScrollbarThumb = Write(o.horizontalScrollbarThumb, horizontalScrollbarThumb, objects);
			o.horizontalScrollbarLeftButton = Write(o.horizontalScrollbarLeftButton, horizontalScrollbarLeftButton, objects);
			o.horizontalScrollbarRightButton = Write(o.horizontalScrollbarRightButton, horizontalScrollbarRightButton, objects);
			o.verticalScrollbar = Write(o.verticalScrollbar, verticalScrollbar, objects);
			o.verticalScrollbarThumb = Write(o.verticalScrollbarThumb, verticalScrollbarThumb, objects);
			o.verticalScrollbarUpButton = Write(o.verticalScrollbarUpButton, verticalScrollbarUpButton, objects);
			o.verticalScrollbarDownButton = Write(o.verticalScrollbarDownButton, verticalScrollbarDownButton, objects);
			o.scrollView = Write(o.scrollView, scrollView, objects);
			o.customStyles = Write(o.customStyles, customStyles, objects);
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.GUISkin o = (UnityEngine.GUISkin)obj;
			font = o.font.GetMappedInstanceID();
			box = Read(box, o.box);
			label = Read(label, o.label);
			textField = Read(textField, o.textField);
			textArea = Read(textArea, o.textArea);
			button = Read(button, o.button);
			toggle = Read(toggle, o.toggle);
			window = Read(window, o.window);
			horizontalSlider = Read(horizontalSlider, o.horizontalSlider);
			horizontalSliderThumb = Read(horizontalSliderThumb, o.horizontalSliderThumb);
			verticalSlider = Read(verticalSlider, o.verticalSlider);
			verticalSliderThumb = Read(verticalSliderThumb, o.verticalSliderThumb);
			horizontalScrollbar = Read(horizontalScrollbar, o.horizontalScrollbar);
			horizontalScrollbarThumb = Read(horizontalScrollbarThumb, o.horizontalScrollbarThumb);
			horizontalScrollbarLeftButton = Read(horizontalScrollbarLeftButton, o.horizontalScrollbarLeftButton);
			horizontalScrollbarRightButton = Read(horizontalScrollbarRightButton, o.horizontalScrollbarRightButton);
			verticalScrollbar = Read(verticalScrollbar, o.verticalScrollbar);
			verticalScrollbarThumb = Read(verticalScrollbarThumb, o.verticalScrollbarThumb);
			verticalScrollbarUpButton = Read(verticalScrollbarUpButton, o.verticalScrollbarUpButton);
			verticalScrollbarDownButton = Read(verticalScrollbarDownButton, o.verticalScrollbarDownButton);
			scrollView = Read(scrollView, o.scrollView);
			customStyles = Read(customStyles, o.customStyles);
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			AddDependency(font, dependencies, objects, allowNulls);
			FindDependencies(box, dependencies, objects, allowNulls);
			FindDependencies(label, dependencies, objects, allowNulls);
			FindDependencies(textField, dependencies, objects, allowNulls);
			FindDependencies(textArea, dependencies, objects, allowNulls);
			FindDependencies(button, dependencies, objects, allowNulls);
			FindDependencies(toggle, dependencies, objects, allowNulls);
			FindDependencies(window, dependencies, objects, allowNulls);
			FindDependencies(horizontalSlider, dependencies, objects, allowNulls);
			FindDependencies(horizontalSliderThumb, dependencies, objects, allowNulls);
			FindDependencies(verticalSlider, dependencies, objects, allowNulls);
			FindDependencies(verticalSliderThumb, dependencies, objects, allowNulls);
			FindDependencies(horizontalScrollbar, dependencies, objects, allowNulls);
			FindDependencies(horizontalScrollbarThumb, dependencies, objects, allowNulls);
			FindDependencies(horizontalScrollbarLeftButton, dependencies, objects, allowNulls);
			FindDependencies(horizontalScrollbarRightButton, dependencies, objects, allowNulls);
			FindDependencies(verticalScrollbar, dependencies, objects, allowNulls);
			FindDependencies(verticalScrollbarThumb, dependencies, objects, allowNulls);
			FindDependencies(verticalScrollbarUpButton, dependencies, objects, allowNulls);
			FindDependencies(verticalScrollbarDownButton, dependencies, objects, allowNulls);
			FindDependencies(scrollView, dependencies, objects, allowNulls);
			FindDependencies(customStyles, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.GUISkin o = (UnityEngine.GUISkin)obj;
			AddDependency(o.font, dependencies);
			GetDependencies(box, o.box, dependencies);
			GetDependencies(label, o.label, dependencies);
			GetDependencies(textField, o.textField, dependencies);
			GetDependencies(textArea, o.textArea, dependencies);
			GetDependencies(button, o.button, dependencies);
			GetDependencies(toggle, o.toggle, dependencies);
			GetDependencies(window, o.window, dependencies);
			GetDependencies(horizontalSlider, o.horizontalSlider, dependencies);
			GetDependencies(horizontalSliderThumb, o.horizontalSliderThumb, dependencies);
			GetDependencies(verticalSlider, o.verticalSlider, dependencies);
			GetDependencies(verticalSliderThumb, o.verticalSliderThumb, dependencies);
			GetDependencies(horizontalScrollbar, o.horizontalScrollbar, dependencies);
			GetDependencies(horizontalScrollbarThumb, o.horizontalScrollbarThumb, dependencies);
			GetDependencies(horizontalScrollbarLeftButton, o.horizontalScrollbarLeftButton, dependencies);
			GetDependencies(horizontalScrollbarRightButton, o.horizontalScrollbarRightButton, dependencies);
			GetDependencies(verticalScrollbar, o.verticalScrollbar, dependencies);
			GetDependencies(verticalScrollbarThumb, o.verticalScrollbarThumb, dependencies);
			GetDependencies(verticalScrollbarUpButton, o.verticalScrollbarUpButton, dependencies);
			GetDependencies(verticalScrollbarDownButton, o.verticalScrollbarDownButton, dependencies);
			GetDependencies(scrollView, o.scrollView, dependencies);
			GetDependencies(customStyles, o.customStyles, dependencies);
		}

		public long font;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentGUIStyle box;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentGUIStyle label;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentGUIStyle textField;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentGUIStyle textArea;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentGUIStyle button;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentGUIStyle toggle;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentGUIStyle window;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentGUIStyle horizontalSlider;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentGUIStyle horizontalSliderThumb;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentGUIStyle verticalSlider;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentGUIStyle verticalSliderThumb;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentGUIStyle horizontalScrollbar;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentGUIStyle horizontalScrollbarThumb;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentGUIStyle horizontalScrollbarLeftButton;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentGUIStyle horizontalScrollbarRightButton;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentGUIStyle verticalScrollbar;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentGUIStyle verticalScrollbarThumb;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentGUIStyle verticalScrollbarUpButton;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentGUIStyle verticalScrollbarDownButton;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentGUIStyle scrollView;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentGUIStyle[] customStyles;

	}
}

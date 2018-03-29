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
	public class PersistentGUIStyle : Battlehub.RTSaveLoad.PersistentData
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.GUIStyle o = (UnityEngine.GUIStyle)obj;
			o.normal = Write(o.normal, normal, objects);
			o.hover = Write(o.hover, hover, objects);
			o.active = Write(o.active, active, objects);
			o.onNormal = Write(o.onNormal, onNormal, objects);
			o.onHover = Write(o.onHover, onHover, objects);
			o.onActive = Write(o.onActive, onActive, objects);
			o.focused = Write(o.focused, focused, objects);
			o.onFocused = Write(o.onFocused, onFocused, objects);
			o.border = border;
			o.margin = margin;
			o.padding = padding;
			o.overflow = overflow;
			o.font = (UnityEngine.Font)objects.Get(font);
			o.name = name;
			o.imagePosition = (UnityEngine.ImagePosition)imagePosition;
			o.alignment = (UnityEngine.TextAnchor)alignment;
			o.wordWrap = wordWrap;
			o.clipping = (UnityEngine.TextClipping)clipping;
			o.contentOffset = contentOffset;
			o.fixedWidth = fixedWidth;
			o.fixedHeight = fixedHeight;
			o.stretchWidth = stretchWidth;
			o.stretchHeight = stretchHeight;
			o.fontSize = fontSize;
			o.fontStyle = (UnityEngine.FontStyle)fontStyle;
			o.richText = richText;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.GUIStyle o = (UnityEngine.GUIStyle)obj;
			normal = Read(normal, o.normal);
			hover = Read(hover, o.hover);
			active = Read(active, o.active);
			onNormal = Read(onNormal, o.onNormal);
			onHover = Read(onHover, o.onHover);
			onActive = Read(onActive, o.onActive);
			focused = Read(focused, o.focused);
			onFocused = Read(onFocused, o.onFocused);
			border = o.border;
			margin = o.margin;
			padding = o.padding;
			overflow = o.overflow;
			font = o.font.GetMappedInstanceID();
			name = o.name;
			imagePosition = (uint)o.imagePosition;
			alignment = (uint)o.alignment;
			wordWrap = o.wordWrap;
			clipping = (uint)o.clipping;
			contentOffset = o.contentOffset;
			fixedWidth = o.fixedWidth;
			fixedHeight = o.fixedHeight;
			stretchWidth = o.stretchWidth;
			stretchHeight = o.stretchHeight;
			fontSize = o.fontSize;
			fontStyle = (uint)o.fontStyle;
			richText = o.richText;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			FindDependencies(normal, dependencies, objects, allowNulls);
			FindDependencies(hover, dependencies, objects, allowNulls);
			FindDependencies(active, dependencies, objects, allowNulls);
			FindDependencies(onNormal, dependencies, objects, allowNulls);
			FindDependencies(onHover, dependencies, objects, allowNulls);
			FindDependencies(onActive, dependencies, objects, allowNulls);
			FindDependencies(focused, dependencies, objects, allowNulls);
			FindDependencies(onFocused, dependencies, objects, allowNulls);
			AddDependency(font, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.GUIStyle o = (UnityEngine.GUIStyle)obj;
			GetDependencies(normal, o.normal, dependencies);
			GetDependencies(hover, o.hover, dependencies);
			GetDependencies(active, o.active, dependencies);
			GetDependencies(onNormal, o.onNormal, dependencies);
			GetDependencies(onHover, o.onHover, dependencies);
			GetDependencies(onActive, o.onActive, dependencies);
			GetDependencies(focused, o.focused, dependencies);
			GetDependencies(onFocused, o.onFocused, dependencies);
			AddDependency(o.font, dependencies);
		}

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentGUIStyleState normal;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentGUIStyleState hover;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentGUIStyleState active;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentGUIStyleState onNormal;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentGUIStyleState onHover;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentGUIStyleState onActive;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentGUIStyleState focused;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentGUIStyleState onFocused;

		public UnityEngine.RectOffset border;

		public UnityEngine.RectOffset margin;

		public UnityEngine.RectOffset padding;

		public UnityEngine.RectOffset overflow;

		public long font;

		public string name;

		public uint imagePosition;

		public uint alignment;

		public bool wordWrap;

		public uint clipping;

		public UnityEngine.Vector2 contentOffset;

		public float fixedWidth;

		public float fixedHeight;

		public bool stretchWidth;

		public bool stretchHeight;

		public int fontSize;

		public uint fontStyle;

		public bool richText;

	}
}

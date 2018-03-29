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
	public class PersistentGUIText : Battlehub.RTSaveLoad.PersistentObjects.PersistentGUIElement
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.GUIText o = (UnityEngine.GUIText)obj;
			o.text = text;
			o.material = (UnityEngine.Material)objects.Get(material);
			o.pixelOffset = pixelOffset;
			o.font = (UnityEngine.Font)objects.Get(font);
			o.alignment = (UnityEngine.TextAlignment)alignment;
			o.anchor = (UnityEngine.TextAnchor)anchor;
			o.lineSpacing = lineSpacing;
			o.tabSize = tabSize;
			o.fontSize = fontSize;
			o.fontStyle = (UnityEngine.FontStyle)fontStyle;
			o.richText = richText;
			o.color = color;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.GUIText o = (UnityEngine.GUIText)obj;
			text = o.text;
			material = o.material.GetMappedInstanceID();
			pixelOffset = o.pixelOffset;
			font = o.font.GetMappedInstanceID();
			alignment = (uint)o.alignment;
			anchor = (uint)o.anchor;
			lineSpacing = o.lineSpacing;
			tabSize = o.tabSize;
			fontSize = o.fontSize;
			fontStyle = (uint)o.fontStyle;
			richText = o.richText;
			color = o.color;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			AddDependency(material, dependencies, objects, allowNulls);
			AddDependency(font, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.GUIText o = (UnityEngine.GUIText)obj;
			AddDependency(o.material, dependencies);
			AddDependency(o.font, dependencies);
		}

		public string text;

		public long material;

		public UnityEngine.Vector2 pixelOffset;

		public long font;

		public uint alignment;

		public uint anchor;

		public float lineSpacing;

		public float tabSize;

		public int fontSize;

		public uint fontStyle;

		public bool richText;

		public UnityEngine.Color color;

	}
}

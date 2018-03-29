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
	public class PersistentTextMesh : Battlehub.RTSaveLoad.PersistentObjects.PersistentComponent
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.TextMesh o = (UnityEngine.TextMesh)obj;
			o.text = text;
			o.font = (UnityEngine.Font)objects.Get(font);
			o.fontSize = fontSize;
			o.fontStyle = (UnityEngine.FontStyle)fontStyle;
			o.offsetZ = offsetZ;
			o.alignment = (UnityEngine.TextAlignment)alignment;
			o.anchor = (UnityEngine.TextAnchor)anchor;
			o.characterSize = characterSize;
			o.lineSpacing = lineSpacing;
			o.tabSize = tabSize;
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
			UnityEngine.TextMesh o = (UnityEngine.TextMesh)obj;
			text = o.text;
			font = o.font.GetMappedInstanceID();
			fontSize = o.fontSize;
			fontStyle = (uint)o.fontStyle;
			offsetZ = o.offsetZ;
			alignment = (uint)o.alignment;
			anchor = (uint)o.anchor;
			characterSize = o.characterSize;
			lineSpacing = o.lineSpacing;
			tabSize = o.tabSize;
			richText = o.richText;
			color = o.color;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			AddDependency(font, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.TextMesh o = (UnityEngine.TextMesh)obj;
			AddDependency(o.font, dependencies);
		}

		public string text;

		public long font;

		public int fontSize;

		public uint fontStyle;

		public float offsetZ;

		public uint alignment;

		public uint anchor;

		public float characterSize;

		public float lineSpacing;

		public float tabSize;

		public bool richText;

		public UnityEngine.Color color;

	}
}

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
	public class PersistentText : Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentMaskableGraphic
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.UI.Text o = (UnityEngine.UI.Text)obj;
			o.font = (UnityEngine.Font)objects.Get(font);
			o.text = text;
			o.supportRichText = supportRichText;
			o.resizeTextForBestFit = resizeTextForBestFit;
			o.resizeTextMinSize = resizeTextMinSize;
			o.resizeTextMaxSize = resizeTextMaxSize;
			o.alignment = (UnityEngine.TextAnchor)alignment;
			o.alignByGeometry = alignByGeometry;
			o.fontSize = fontSize;
			o.horizontalOverflow = (UnityEngine.HorizontalWrapMode)horizontalOverflow;
			o.verticalOverflow = (UnityEngine.VerticalWrapMode)verticalOverflow;
			o.lineSpacing = lineSpacing;
			o.fontStyle = (UnityEngine.FontStyle)fontStyle;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.UI.Text o = (UnityEngine.UI.Text)obj;
			font = o.font.GetMappedInstanceID();
			text = o.text;
			supportRichText = o.supportRichText;
			resizeTextForBestFit = o.resizeTextForBestFit;
			resizeTextMinSize = o.resizeTextMinSize;
			resizeTextMaxSize = o.resizeTextMaxSize;
			alignment = (uint)o.alignment;
			alignByGeometry = o.alignByGeometry;
			fontSize = o.fontSize;
			horizontalOverflow = (uint)o.horizontalOverflow;
			verticalOverflow = (uint)o.verticalOverflow;
			lineSpacing = o.lineSpacing;
			fontStyle = (uint)o.fontStyle;
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
			UnityEngine.UI.Text o = (UnityEngine.UI.Text)obj;
			AddDependency(o.font, dependencies);
		}

		public long font;

		public string text;

		public bool supportRichText;

		public bool resizeTextForBestFit;

		public int resizeTextMinSize;

		public int resizeTextMaxSize;

		public uint alignment;

		public bool alignByGeometry;

		public int fontSize;

		public uint horizontalOverflow;

		public uint verticalOverflow;

		public float lineSpacing;

		public uint fontStyle;

	}
}

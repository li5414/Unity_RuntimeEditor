#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif
/*This is auto-generated file. Tools->Runtime SaveLoad->Create Persistent Objects 
If you want prevent overwriting, drag this file to another folder.*/

namespace Battlehub.RTSaveLoad.PersistentObjects
{
	#if RT_USE_PROTOBUF
	[ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
	#endif
	[System.Serializable]
	public class PersistentLineRenderer : Battlehub.RTSaveLoad.PersistentObjects.PersistentRenderer
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.LineRenderer o = (UnityEngine.LineRenderer)obj;
			o.startWidth = startWidth;
			o.endWidth = endWidth;
			o.widthCurve = Write(o.widthCurve, widthCurve, objects);
			o.widthMultiplier = widthMultiplier;
			o.startColor = startColor;
			o.endColor = endColor;
			o.colorGradient = Write(o.colorGradient, colorGradient, objects);
			o.positionCount = positionCount;
			o.useWorldSpace = useWorldSpace;
			o.loop = loop;
			o.numCornerVertices = numCornerVertices;
			o.numCapVertices = numCapVertices;
			o.textureMode = (UnityEngine.LineTextureMode)textureMode;
			o.alignment = (UnityEngine.LineAlignment)alignment;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.LineRenderer o = (UnityEngine.LineRenderer)obj;
			startWidth = o.startWidth;
			endWidth = o.endWidth;
			widthCurve = Read(widthCurve, o.widthCurve);
			widthMultiplier = o.widthMultiplier;
			startColor = o.startColor;
			endColor = o.endColor;
			colorGradient = Read(colorGradient, o.colorGradient);
			positionCount = o.positionCount;
			useWorldSpace = o.useWorldSpace;
			loop = o.loop;
			numCornerVertices = o.numCornerVertices;
			numCapVertices = o.numCapVertices;
			textureMode = (uint)o.textureMode;
			alignment = (uint)o.alignment;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			FindDependencies(widthCurve, dependencies, objects, allowNulls);
			FindDependencies(colorGradient, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.LineRenderer o = (UnityEngine.LineRenderer)obj;
			GetDependencies(widthCurve, o.widthCurve, dependencies);
			GetDependencies(colorGradient, o.colorGradient, dependencies);
		}

		public float startWidth;

		public float endWidth;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentAnimationCurve widthCurve;

		public float widthMultiplier;

		public UnityEngine.Color startColor;

		public UnityEngine.Color endColor;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentGradient colorGradient;

		public int positionCount;

		public bool useWorldSpace;

		public bool loop;

		public int numCornerVertices;

		public int numCapVertices;

		public uint textureMode;

		public uint alignment;

	}
}

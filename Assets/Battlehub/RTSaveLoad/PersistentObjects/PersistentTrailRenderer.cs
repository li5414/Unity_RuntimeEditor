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
	public class PersistentTrailRenderer : Battlehub.RTSaveLoad.PersistentObjects.PersistentRenderer
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.TrailRenderer o = (UnityEngine.TrailRenderer)obj;
			o.time = time;
			o.startWidth = startWidth;
			o.endWidth = endWidth;
			o.widthCurve = Write(o.widthCurve, widthCurve, objects);
			o.widthMultiplier = widthMultiplier;
			o.startColor = startColor;
			o.endColor = endColor;
			o.colorGradient = Write(o.colorGradient, colorGradient, objects);
			o.autodestruct = autodestruct;
			o.numCornerVertices = numCornerVertices;
			o.numCapVertices = numCapVertices;
			o.minVertexDistance = minVertexDistance;
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
			UnityEngine.TrailRenderer o = (UnityEngine.TrailRenderer)obj;
			time = o.time;
			startWidth = o.startWidth;
			endWidth = o.endWidth;
			widthCurve = Read(widthCurve, o.widthCurve);
			widthMultiplier = o.widthMultiplier;
			startColor = o.startColor;
			endColor = o.endColor;
			colorGradient = Read(colorGradient, o.colorGradient);
			autodestruct = o.autodestruct;
			numCornerVertices = o.numCornerVertices;
			numCapVertices = o.numCapVertices;
			minVertexDistance = o.minVertexDistance;
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
			UnityEngine.TrailRenderer o = (UnityEngine.TrailRenderer)obj;
			GetDependencies(widthCurve, o.widthCurve, dependencies);
			GetDependencies(colorGradient, o.colorGradient, dependencies);
		}

		public float time;

		public float startWidth;

		public float endWidth;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentAnimationCurve widthCurve;

		public float widthMultiplier;

		public UnityEngine.Color startColor;

		public UnityEngine.Color endColor;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentGradient colorGradient;

		public bool autodestruct;

		public int numCornerVertices;

		public int numCapVertices;

		public float minVertexDistance;

		public uint textureMode;

		public uint alignment;

	}
}

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
	public class PersistentCompositeCollider2D : Battlehub.RTSaveLoad.PersistentObjects.PersistentCollider2D
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.CompositeCollider2D o = (UnityEngine.CompositeCollider2D)obj;
			o.geometryType = (UnityEngine.CompositeCollider2D.GeometryType)geometryType;
			o.generationType = (UnityEngine.CompositeCollider2D.GenerationType)generationType;
			o.vertexDistance = vertexDistance;
			o.edgeRadius = edgeRadius;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.CompositeCollider2D o = (UnityEngine.CompositeCollider2D)obj;
			geometryType = (uint)o.geometryType;
			generationType = (uint)o.generationType;
			vertexDistance = o.vertexDistance;
			edgeRadius = o.edgeRadius;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public uint geometryType;

		public uint generationType;

		public float vertexDistance;

		public float edgeRadius;

	}
}

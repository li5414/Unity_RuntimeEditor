#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif
/*This is auto-generated file. Tools->Runtime SaveLoad->Create Persistent Objects 
If you want prevent overwriting, drag this file to another folder.*/

namespace Battlehub.RTSaveLoad.PersistentObjects.UI
{
	#if RT_USE_PROTOBUF
	[ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
	#endif
	[System.Serializable]
	public class PersistentGridLayoutGroup : Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentLayoutGroup
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.UI.GridLayoutGroup o = (UnityEngine.UI.GridLayoutGroup)obj;
			o.startCorner = (UnityEngine.UI.GridLayoutGroup.Corner)startCorner;
			o.startAxis = (UnityEngine.UI.GridLayoutGroup.Axis)startAxis;
			o.cellSize = cellSize;
			o.spacing = spacing;
			o.constraint = (UnityEngine.UI.GridLayoutGroup.Constraint)constraint;
			o.constraintCount = constraintCount;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.UI.GridLayoutGroup o = (UnityEngine.UI.GridLayoutGroup)obj;
			startCorner = (uint)o.startCorner;
			startAxis = (uint)o.startAxis;
			cellSize = o.cellSize;
			spacing = o.spacing;
			constraint = (uint)o.constraint;
			constraintCount = o.constraintCount;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public uint startCorner;

		public uint startAxis;

		public UnityEngine.Vector2 cellSize;

		public UnityEngine.Vector2 spacing;

		public uint constraint;

		public int constraintCount;

	}
}

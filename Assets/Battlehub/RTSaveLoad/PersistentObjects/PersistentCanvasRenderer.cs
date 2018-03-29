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
	public class PersistentCanvasRenderer : Battlehub.RTSaveLoad.PersistentObjects.PersistentComponent
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.CanvasRenderer o = (UnityEngine.CanvasRenderer)obj;
			o.hasPopInstruction = hasPopInstruction;
			o.materialCount = materialCount;
			o.popMaterialCount = popMaterialCount;
			o.cull = cull;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.CanvasRenderer o = (UnityEngine.CanvasRenderer)obj;
			hasPopInstruction = o.hasPopInstruction;
			materialCount = o.materialCount;
			popMaterialCount = o.popMaterialCount;
			cull = o.cull;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public bool hasPopInstruction;

		public int materialCount;

		public int popMaterialCount;

		public bool cull;

	}
}

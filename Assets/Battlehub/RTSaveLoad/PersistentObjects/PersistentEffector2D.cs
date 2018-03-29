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
	[ProtoInclude(1101, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentAreaEffector2D))]
	[ProtoInclude(1102, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentPlatformEffector2D))]
	[ProtoInclude(1103, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentBuoyancyEffector2D))]
	[ProtoInclude(1104, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentPointEffector2D))]
	[ProtoInclude(1105, typeof(Battlehub.RTSaveLoad.PersistentObjects.PersistentSurfaceEffector2D))]
	#endif
	[System.Serializable]
	public class PersistentEffector2D : Battlehub.RTSaveLoad.PersistentObjects.PersistentBehaviour
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.Effector2D o = (UnityEngine.Effector2D)obj;
			o.useColliderMask = useColliderMask;
			o.colliderMask = colliderMask;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.Effector2D o = (UnityEngine.Effector2D)obj;
			useColliderMask = o.useColliderMask;
			colliderMask = o.colliderMask;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public bool useColliderMask;

		public int colliderMask;

	}
}

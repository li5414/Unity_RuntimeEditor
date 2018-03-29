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
	public class PersistentPhysicMaterial : Battlehub.RTSaveLoad.PersistentObjects.PersistentObject
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.PhysicMaterial o = (UnityEngine.PhysicMaterial)obj;
			o.dynamicFriction = dynamicFriction;
			o.staticFriction = staticFriction;
			o.bounciness = bounciness;
			o.frictionCombine = (UnityEngine.PhysicMaterialCombine)frictionCombine;
			o.bounceCombine = (UnityEngine.PhysicMaterialCombine)bounceCombine;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.PhysicMaterial o = (UnityEngine.PhysicMaterial)obj;
			dynamicFriction = o.dynamicFriction;
			staticFriction = o.staticFriction;
			bounciness = o.bounciness;
			frictionCombine = (uint)o.frictionCombine;
			bounceCombine = (uint)o.bounceCombine;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public float dynamicFriction;

		public float staticFriction;

		public float bounciness;

		public uint frictionCombine;

		public uint bounceCombine;

	}
}

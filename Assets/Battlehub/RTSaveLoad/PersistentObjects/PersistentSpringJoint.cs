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
	public class PersistentSpringJoint : Battlehub.RTSaveLoad.PersistentObjects.PersistentJoint
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.SpringJoint o = (UnityEngine.SpringJoint)obj;
			o.spring = spring;
			o.damper = damper;
			o.minDistance = minDistance;
			o.maxDistance = maxDistance;
			o.tolerance = tolerance;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.SpringJoint o = (UnityEngine.SpringJoint)obj;
			spring = o.spring;
			damper = o.damper;
			minDistance = o.minDistance;
			maxDistance = o.maxDistance;
			tolerance = o.tolerance;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

		public float spring;

		public float damper;

		public float minDistance;

		public float maxDistance;

		public float tolerance;

	}
}

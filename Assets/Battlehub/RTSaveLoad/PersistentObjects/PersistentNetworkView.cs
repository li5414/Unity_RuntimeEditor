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
	public class PersistentNetworkView : Battlehub.RTSaveLoad.PersistentObjects.PersistentBehaviour
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
            return base.WriteTo(obj, objects);
//			obj = base.WriteTo(obj, objects);
//			if(obj == null)
//			{
//				return null;
//			}
//#if !UNITY_WINRT && !UNITY_WEBGL
//			UnityEngine.NetworkView o = (UnityEngine.NetworkView)obj;
//			o.observed = (UnityEngine.Component)objects.Get(observed);

//			o.stateSynchronization = (UnityEngine.NetworkStateSynchronization)stateSynchronization;

//			o.viewID = viewID;

//            o.group = group;
//            return o;
//#else
//            return obj;
//#endif

        }

        public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
//			if(obj == null)
//			{
//				return;
//			}
//#if !UNITY_WINRT && !UNITY_WEBGL
//			UnityEngine.NetworkView o = (UnityEngine.NetworkView)obj;
//			observed = o.observed.GetMappedInstanceID();
//			stateSynchronization = (uint)o.stateSynchronization;
//			viewID = o.viewID;
//            group = o.group;
//#endif
        }

        public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			AddDependency(observed, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
            base.GetDependencies(dependencies, obj);
//			if(obj == null)
//			{
//				return;
//			}
//#if !UNITY_WINRT && !UNITY_WEBGL
//            UnityEngine.NetworkView o = (UnityEngine.NetworkView)obj;
//			AddDependency(o.observed, dependencies);
//#endif
        }

        public long observed;

		public uint stateSynchronization;

#if !UNITY_WINRT && !UNITY_WEBGL
        public UnityEngine.NetworkViewID viewID;
#endif
		public int group;

	}
}

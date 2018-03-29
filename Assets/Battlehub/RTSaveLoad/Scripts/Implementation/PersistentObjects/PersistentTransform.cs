#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif

/*To autogenerate PersistenTransform remove this file and run Tools->Runtime SaveLoad->Create Persistent Objects command*/
//

namespace Battlehub.RTSaveLoad.PersistentObjects
{

    #if RT_USE_PROTOBUF && !RT_PE_MAINTANANCE
    [ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
	[ProtoInclude(1084, typeof(PersistentRectTransform))]
    #endif
    [System.Serializable]
	public class PersistentTransform
    #if RT_PE_MAINTANANCE
        : PersistentData
    #else
        : PersistentComponent
    #endif
    {
        public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			UnityEngine.Transform o = (UnityEngine.Transform)obj;

            o.SetParent((UnityEngine.Transform)objects.Get(parent), false);
            o.position = position;
			o.rotation = rotation;
            o.localScale = localScale;
            //o.parent = (UnityEngine.Transform)objects.Get(parent);

            return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			UnityEngine.Transform o = (UnityEngine.Transform)obj;
			position = o.position;
			rotation = o.rotation;
            localScale = o.localScale;
			parent = o.parent.GetMappedInstanceID();
		}

        public UnityEngine.Vector3 position;

        public UnityEngine.Quaternion rotation;

        public UnityEngine.Vector3 localScale;

        public long parent;

        public int hierarchyCapacity;
	}
   
}

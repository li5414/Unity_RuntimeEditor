#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif

/*To autogenerate PersistentTexture2D remove this file and run Tools->Runtime SaveLoad->Create Persistent Objects command*/

namespace Battlehub.RTSaveLoad.PersistentObjects
{
    #if RT_USE_PROTOBUF
	[ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
    #endif
    [System.Serializable]
	public class PersistentTexture2D : Battlehub.RTSaveLoad.PersistentObjects.PersistentTexture
	{

        public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
        {
            obj = base.WriteTo(obj, objects);
            if (obj == null)
            {
                return null;
            }
            UnityEngine.Texture2D o = (UnityEngine.Texture2D)obj;
            try
            {
                o.Resize(width, height);
            }
            catch
            {

            }
            
            return o;
        }
    }
    
}

#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using System.Collections.Generic;
using ProtoBuf;
using UnityEngine;
using UnityEngine.UI;
#endif
/*This is auto-generated file. Tools->Runtime SaveLoad->Create Persistent Objects 
If you want prevent overwriting, drag this file to another folder.*/

namespace Battlehub.RTSaveLoad.PersistentObjects.UI
{
    #if RT_USE_PROTOBUF && !RT_PE_MAINTANANCE
    [ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
	#endif
	[System.Serializable]
	public class PersistentButton
#if RT_PE_MAINTANANCE
        : PersistentData
#else
        : Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentSelectable
#endif
    {
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.UI.Button o = (UnityEngine.UI.Button)obj;
            Write(o.onClick, onClick, objects);
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.UI.Button o = (UnityEngine.UI.Button)obj;
            Read(onClick, o.onClick);
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);

            if(onClick != null)
            {
                onClick.FindDependencies(dependencies, objects, allowNulls);
            }
		}

        protected override void GetDependencies(Dictionary<long, Object> dependencies, object obj)
        {
            base.GetDependencies(dependencies, obj);

            Button btn = (Button)obj;
            if (btn == null)
            {
                return;
            }

            PersistentUnityEventBase eventBase = new PersistentUnityEventBase();
            eventBase.GetDependencies(btn.onClick, dependencies);

        }

        public PersistentUnityEventBase onClick;
	}
}

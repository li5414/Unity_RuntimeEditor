#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using System.Collections.Generic;
using ProtoBuf;
using UnityEngine;
#endif
/*This is auto-generated file. Tools->Runtime SaveLoad->Create Persistent Objects 
If you want prevent overwriting, drag this file to another folder.*/

namespace Battlehub.RTSaveLoad.PersistentObjects.UI
{
#if RT_USE_PROTOBUF && !RT_PE_MAINTANANCE
    [ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
	[ProtoInclude(1131, typeof(Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentImage))]
	[ProtoInclude(1132, typeof(Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentRawImage))]
	[ProtoInclude(1133, typeof(Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentText))]
	#endif
	[System.Serializable]
	public class PersistentMaskableGraphic 
#if RT_PE_MAINTANANCE
        : PersistentData
#else
        : Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentGraphic
#endif
    {
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.UI.MaskableGraphic o = (UnityEngine.UI.MaskableGraphic)obj;
            onCullStateChanged.WriteTo(o.onCullStateChanged, objects);
			o.maskable = maskable;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.UI.MaskableGraphic o = (UnityEngine.UI.MaskableGraphic)obj;
            onCullStateChanged = new PersistentUnityEventBase();
            onCullStateChanged.ReadFrom(o.onCullStateChanged);
			maskable = o.maskable;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
            onCullStateChanged.FindDependencies(dependencies, objects, allowNulls);
        }

        protected override void GetDependencies(Dictionary<long, Object> dependencies, object obj)
        {
            base.GetDependencies(dependencies, obj);

            UnityEngine.UI.MaskableGraphic o = (UnityEngine.UI.MaskableGraphic)obj;
            if(o == null)
            {
                return;
            }
            PersistentUnityEventBase evtBase = new PersistentUnityEventBase();
            evtBase.GetDependencies(o.onCullStateChanged, dependencies);
        }

        public PersistentUnityEventBase onCullStateChanged;

        public bool maskable;

	}
}

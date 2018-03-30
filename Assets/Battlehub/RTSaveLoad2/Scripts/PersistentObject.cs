using System.Collections.Generic;
using ProtoBuf;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace Battlehub.RTSaveLoad2
{
   
    [ProtoContract(AsReferenceDefault = true)]    
    public class PersistentObject : PersistentSurrogate
    {
        [ProtoMember(1)]
        public string name;

        [ProtoMember(2)]
        public int hideFlags;

        public override void ReadFrom(object obj)
        {
            UnityObject uo = (UnityObject)obj;
            name = uo.name;
            hideFlags = (int)uo.hideFlags;
        }

        public override object WriteTo(object obj)
        {
            UnityObject uo = (UnityObject)obj;
            uo.name = name;
            uo.hideFlags = (HideFlags)hideFlags;
            return obj;
        }       
    }

}

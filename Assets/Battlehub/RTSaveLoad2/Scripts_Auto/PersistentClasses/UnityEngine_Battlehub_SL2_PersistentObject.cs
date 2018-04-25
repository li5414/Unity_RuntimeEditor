using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;
using System;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentObject : PersistentSurrogate
    {
        [ProtoMember(256)]
        public string name;

        [ProtoMember(257)]
        public HideFlags hideFlags;

        public override void ReadFrom(object obj)
        {
            base.ReadFrom(obj);
            UnityObject uo = (UnityObject)obj;
            name = uo.name;
            hideFlags = uo.hideFlags;
        }

        public override object WriteTo(object obj)
        {
            obj = base.WriteTo(obj);
            UnityObject uo = (UnityObject)obj;
            uo.name = name;
            uo.hideFlags = hideFlags;
            return obj;
        }

        public static implicit operator UnityObject(PersistentObject surrogate)
        {
            return (UnityObject)surrogate.WriteTo(new UnityObject());
        }
        
        public static implicit operator PersistentObject(UnityObject obj)
        {
            PersistentObject surrogate = new PersistentObject();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


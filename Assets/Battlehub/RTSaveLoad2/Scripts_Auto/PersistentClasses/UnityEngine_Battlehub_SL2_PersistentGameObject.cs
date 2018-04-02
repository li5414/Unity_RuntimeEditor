using System.Collections.Generic;
using ProtoBuf;
using UnityEngine;
using UnityEngine.Battlehub.SL2;
using System;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentGameObject : PersistentObject
    {
        [ProtoMember(256)]
        public Int32 layer;

        [ProtoMember(257)]
        public Boolean active;

        [ProtoMember(258)]
        public Boolean isStatic;

        [ProtoMember(259)]
        public String tag;

        public override void ReadFrom(object obj)
        {
            base.ReadFrom(obj);
            GameObject uo = (GameObject)obj;
            layer = uo.layer;
            active = uo.active;
            isStatic = uo.isStatic;
            tag = uo.tag;
            
        }

        public override object WriteTo(object obj)
        {
            obj = base.WriteTo(obj);
            GameObject uo = (GameObject)obj;
            uo.layer = layer;
            uo.active = active;
            uo.isStatic = isStatic;
            uo.tag = tag;
            return obj;
        }

        public static implicit operator GameObject(PersistentGameObject surrogate)
        {
            return (GameObject)surrogate.WriteTo(new GameObject());
        }
        
        public static implicit operator PersistentGameObject(GameObject obj)
        {
            PersistentGameObject surrogate = new PersistentGameObject();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


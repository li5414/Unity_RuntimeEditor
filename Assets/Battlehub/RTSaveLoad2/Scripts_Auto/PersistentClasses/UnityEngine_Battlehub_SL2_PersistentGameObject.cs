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
        public int layer;

        [ProtoMember(257)]
        public bool active;

        [ProtoMember(258)]
        public bool isStatic;

        [ProtoMember(259)]
        public string tag;

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


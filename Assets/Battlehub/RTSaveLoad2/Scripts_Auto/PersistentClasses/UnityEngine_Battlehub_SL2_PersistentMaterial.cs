using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentMaterial : PersistentObject
    {
        [ProtoMember(257)]
        public Color color;

        public override void ReadFrom(object obj)
        {
            base.ReadFrom(obj);
            Material uo = (Material)obj;
            color = uo.color;
        }

        public override object WriteTo(object obj)
        {
            obj = base.WriteTo(obj);
            Material uo = (Material)obj;
            uo.color = color;
            return obj;
        }
    }
}


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
    public class PersistentSkinnedMeshRenderer : PersistentRenderer
    {
        [ProtoMember(256)]
        public int[] bones;

        public override void ReadFrom(object obj)
        {
            base.ReadFrom(obj);
            SkinnedMeshRenderer uo = (SkinnedMeshRenderer)obj;
            bones = ToId(uo.bones);
        }

        public override object WriteTo(object obj)
        {
            obj = base.WriteTo(obj);
            SkinnedMeshRenderer uo = (SkinnedMeshRenderer)obj;
            uo.bones = FromId<Transform>(bones);
            return obj;
        }

        protected override void GetDepsImpl(GetDepsContext context)
        {
            AddDep(bones, context);
        }

        protected override void GetDepsFromImpl(object obj, GetDepsFromContext context)
        {
            SkinnedMeshRenderer uo = (SkinnedMeshRenderer)obj;
            AddDep(uo.bones, context);
        }

        public static implicit operator SkinnedMeshRenderer(PersistentSkinnedMeshRenderer surrogate)
        {
            return (SkinnedMeshRenderer)surrogate.WriteTo(new SkinnedMeshRenderer());
        }
        
        public static implicit operator PersistentSkinnedMeshRenderer(SkinnedMeshRenderer obj)
        {
            PersistentSkinnedMeshRenderer surrogate = new PersistentSkinnedMeshRenderer();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


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
    public class PersistentMeshFilter : PersistentObject
    {
        [ProtoMember(256)]
        public int sharedMesh;

        [ProtoMember(257)]
        public int mesh;

        public override void ReadFrom(object obj)
        {
            base.ReadFrom(obj);
            MeshFilter uo = (MeshFilter)obj;
            sharedMesh = ToId(uo.sharedMesh);
            mesh = ToId(uo.mesh);
        }

        public override object WriteTo(object obj)
        {
            obj = base.WriteTo(obj);
            MeshFilter uo = (MeshFilter)obj;
            uo.sharedMesh = FromId<Mesh>(sharedMesh);
            uo.mesh = FromId<Mesh>(mesh);
            return obj;
        }

        protected override void GetDepsImpl(GetDepsContext context)
        {
            AddDep(sharedMesh, context);
            AddDep(mesh, context);
        }

        protected override void GetDepsFromImpl(object obj, GetDepsFromContext context)
        {
            MeshFilter uo = (MeshFilter)obj;
            AddDep(uo.sharedMesh, context);
            AddDep(uo.mesh, context);
        }

        public static implicit operator MeshFilter(PersistentMeshFilter surrogate)
        {
            return (MeshFilter)surrogate.WriteTo(new MeshFilter());
        }
        
        public static implicit operator PersistentMeshFilter(MeshFilter obj)
        {
            PersistentMeshFilter surrogate = new PersistentMeshFilter();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


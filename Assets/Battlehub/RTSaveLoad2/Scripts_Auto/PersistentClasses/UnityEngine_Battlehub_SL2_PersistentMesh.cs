using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentMesh : PersistentObject
    {
        [ProtoMember(263)]
        public Vector3[] normals;

        public override void ReadFrom(object obj)
        {
            base.ReadFrom(obj);
            Mesh uo = (Mesh)obj;
            normals = uo.normals;
        }

        public override object WriteTo(object obj)
        {
            obj = base.WriteTo(obj);
            Mesh uo = (Mesh)obj;
            uo.normals = normals;
            return obj;
        }

        public static implicit operator Mesh(PersistentMesh surrogate)
        {
            return (Mesh)surrogate.WriteTo(new Mesh());
        }
        
        public static implicit operator PersistentMesh(Mesh obj)
        {
            PersistentMesh surrogate = new PersistentMesh();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


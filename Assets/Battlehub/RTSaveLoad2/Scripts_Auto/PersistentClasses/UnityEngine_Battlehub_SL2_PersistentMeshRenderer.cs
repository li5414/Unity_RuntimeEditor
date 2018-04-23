using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentMeshRenderer : PersistentRenderer
    {
        
        public static implicit operator MeshRenderer(PersistentMeshRenderer surrogate)
        {
            return (MeshRenderer)surrogate.WriteTo(new MeshRenderer());
        }
        
        public static implicit operator PersistentMeshRenderer(MeshRenderer obj)
        {
            PersistentMeshRenderer surrogate = new PersistentMeshRenderer();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


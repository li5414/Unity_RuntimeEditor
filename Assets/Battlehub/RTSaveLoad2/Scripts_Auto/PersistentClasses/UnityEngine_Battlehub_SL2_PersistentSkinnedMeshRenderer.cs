using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentSkinnedMeshRenderer : PersistentRenderer
    {
        
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


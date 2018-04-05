using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentRenderTextureDescriptor : PersistentSurrogate
    {
        public static implicit operator RenderTextureDescriptor(PersistentRenderTextureDescriptor surrogate)
        {
            return (RenderTextureDescriptor)surrogate.WriteTo(new RenderTextureDescriptor());
        }
        
        public static implicit operator PersistentRenderTextureDescriptor(RenderTextureDescriptor obj)
        {
            PersistentRenderTextureDescriptor surrogate = new PersistentRenderTextureDescriptor();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


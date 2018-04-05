using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentRenderBuffer : PersistentSurrogate
    {
        public static implicit operator RenderBuffer(PersistentRenderBuffer surrogate)
        {
            return (RenderBuffer)surrogate.WriteTo(new RenderBuffer());
        }
        
        public static implicit operator PersistentRenderBuffer(RenderBuffer obj)
        {
            PersistentRenderBuffer surrogate = new PersistentRenderBuffer();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


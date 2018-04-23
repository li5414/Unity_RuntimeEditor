using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentRenderer : PersistentObject
    {
        
        public static implicit operator Renderer(PersistentRenderer surrogate)
        {
            return (Renderer)surrogate.WriteTo(new Renderer());
        }
        
        public static implicit operator PersistentRenderer(Renderer obj)
        {
            PersistentRenderer surrogate = new PersistentRenderer();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


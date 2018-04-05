using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentTextGenerator : PersistentSurrogate
    {
        public static implicit operator TextGenerator(PersistentTextGenerator surrogate)
        {
            return (TextGenerator)surrogate.WriteTo(new TextGenerator());
        }
        
        public static implicit operator PersistentTextGenerator(TextGenerator obj)
        {
            PersistentTextGenerator surrogate = new PersistentTextGenerator();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


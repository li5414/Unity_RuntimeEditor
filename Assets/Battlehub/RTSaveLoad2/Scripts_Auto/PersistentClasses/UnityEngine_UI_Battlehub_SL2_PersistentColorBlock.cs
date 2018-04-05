using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine.UI;
using UnityEngine.UI.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.UI.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentColorBlock : PersistentSurrogate
    {
        public static implicit operator ColorBlock(PersistentColorBlock surrogate)
        {
            return (ColorBlock)surrogate.WriteTo(new ColorBlock());
        }
        
        public static implicit operator PersistentColorBlock(ColorBlock obj)
        {
            PersistentColorBlock surrogate = new PersistentColorBlock();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


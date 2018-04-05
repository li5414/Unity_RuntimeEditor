using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine.UI;
using UnityEngine.UI.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.UI.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentNavigation : PersistentSurrogate
    {
        public static implicit operator Navigation(PersistentNavigation surrogate)
        {
            return (Navigation)surrogate.WriteTo(new Navigation());
        }
        
        public static implicit operator PersistentNavigation(Navigation obj)
        {
            PersistentNavigation surrogate = new PersistentNavigation();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


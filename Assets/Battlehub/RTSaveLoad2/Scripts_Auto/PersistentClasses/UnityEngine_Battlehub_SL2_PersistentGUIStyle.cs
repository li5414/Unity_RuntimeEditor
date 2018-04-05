using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentGUIStyle : PersistentSurrogate
    {
        public static implicit operator GUIStyle(PersistentGUIStyle surrogate)
        {
            return (GUIStyle)surrogate.WriteTo(new GUIStyle());
        }
        
        public static implicit operator PersistentGUIStyle(GUIStyle obj)
        {
            PersistentGUIStyle surrogate = new PersistentGUIStyle();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


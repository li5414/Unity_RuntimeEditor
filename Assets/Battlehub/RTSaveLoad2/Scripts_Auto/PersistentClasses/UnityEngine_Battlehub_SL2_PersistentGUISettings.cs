using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentGUISettings : PersistentSurrogate
    {
        public static implicit operator GUISettings(PersistentGUISettings surrogate)
        {
            return (GUISettings)surrogate.WriteTo(new GUISettings());
        }
        
        public static implicit operator PersistentGUISettings(GUISettings obj)
        {
            PersistentGUISettings surrogate = new PersistentGUISettings();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


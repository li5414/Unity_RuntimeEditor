using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentGUIStyleState : PersistentSurrogate
    {
        public static implicit operator GUIStyleState(PersistentGUIStyleState surrogate)
        {
            return (GUIStyleState)surrogate.WriteTo(new GUIStyleState());
        }
        
        public static implicit operator PersistentGUIStyleState(GUIStyleState obj)
        {
            PersistentGUIStyleState surrogate = new PersistentGUIStyleState();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


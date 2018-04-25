using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentGameObject : PersistentObject
    {
        
        public static implicit operator GameObject(PersistentGameObject surrogate)
        {
            return (GameObject)surrogate.WriteTo(new GameObject());
        }
        
        public static implicit operator PersistentGameObject(GameObject obj)
        {
            PersistentGameObject surrogate = new PersistentGameObject();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


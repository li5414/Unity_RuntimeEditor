using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine;
using UnityEngine.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentCharacterInfo : PersistentSurrogate
    {
        public static implicit operator CharacterInfo(PersistentCharacterInfo surrogate)
        {
            return (CharacterInfo)surrogate.WriteTo(new CharacterInfo());
        }
        
        public static implicit operator PersistentCharacterInfo(CharacterInfo obj)
        {
            PersistentCharacterInfo surrogate = new PersistentCharacterInfo();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


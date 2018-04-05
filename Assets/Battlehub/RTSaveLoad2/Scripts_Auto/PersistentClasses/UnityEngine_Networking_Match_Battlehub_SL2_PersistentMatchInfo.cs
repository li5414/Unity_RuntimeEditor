using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.Match.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Networking.Match.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentMatchInfo : PersistentSurrogate
    {
        public static implicit operator MatchInfo(PersistentMatchInfo surrogate)
        {
            return (MatchInfo)surrogate.WriteTo(new MatchInfo());
        }
        
        public static implicit operator PersistentMatchInfo(MatchInfo obj)
        {
            PersistentMatchInfo surrogate = new PersistentMatchInfo();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


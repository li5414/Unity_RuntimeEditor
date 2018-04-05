using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine.Playables;
using UnityEngine.Playables.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.Playables.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentPlayableGraph : PersistentSurrogate
    {
        public static implicit operator PlayableGraph(PersistentPlayableGraph surrogate)
        {
            return (PlayableGraph)surrogate.WriteTo(new PlayableGraph());
        }
        
        public static implicit operator PersistentPlayableGraph(PlayableGraph obj)
        {
            PersistentPlayableGraph surrogate = new PersistentPlayableGraph();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


using System.Collections.Generic;
using ProtoBuf;
using Battlehub.RTSaveLoad2;
using UnityEngine.SceneManagement;
using UnityEngine.SceneManagement.Battlehub.SL2;

using UnityObject = UnityEngine.Object;
namespace UnityEngine.SceneManagement.Battlehub.SL2
{
    [ProtoContract(AsReferenceDefault = true)]
    public class PersistentScene : PersistentSurrogate
    {
        public static implicit operator Scene(PersistentScene surrogate)
        {
            return (Scene)surrogate.WriteTo(new Scene());
        }
        
        public static implicit operator PersistentScene(Scene obj)
        {
            PersistentScene surrogate = new PersistentScene();
            surrogate.ReadFrom(obj);
            return surrogate;
        }
    }
}


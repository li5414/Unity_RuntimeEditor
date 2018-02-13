using ProtoBuf;
using System.Collections.Generic;
using UnityEngine;

namespace Battlehub.RTSaveLoad2
{
    [ProtoContract()]
    public class Surrogate 
    {
        public virtual void GetDeps(HashSet<int> dependencies)
        {
        }

        public virtual void GetDeps(HashSet<object> dependencies)
        {
        }
    }

    [ProtoContract]
    public class Vector3Surrogate : Surrogate
    {
        public float x;
        public float y;
        public float z;
        public static implicit operator UnityEngine.Vector3(Vector3Surrogate v)
        {
            Vector3 o = new Vector3();
            o.x = v.x;
            o.y = v.y;
            o.z = v.z;
            return o;
        }
        public static implicit operator Vector3Surrogate(UnityEngine.Vector3 v)
        {
            Vector3Surrogate o = new Vector3Surrogate();
            o.x = v.x;
            o.y = v.y;
            o.z = v.z;
            return o;
        }


    }

}


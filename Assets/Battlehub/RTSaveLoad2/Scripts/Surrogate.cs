using ProtoBuf;
using System.Collections.Generic;
using UnityEngine;

namespace Battlehub.RTSaveLoad2
{
    [ProtoContract]
    public class Surrogate 
    {
        public virtual void ReadFrom(object obj)
        {
        }

        public virtual object WriteTo(object obj)
        {
            return obj;
        }

        //GetDependencies Recursive
        public virtual void GetDeps(HashSet<int> dependencies)
        {
        }

        //GetDependencies Recursive
        public virtual void GetDepsFrom(object obj, HashSet<object> dependencies)
        {
        }

    }

    [ProtoContract]
    public class Vector3Surrogate : Surrogate
    {
        public float x;
        public float y;
        public float z;

        public override void ReadFrom(object obj)
        {
            base.ReadFrom(obj);
            Vector3 o = (Vector3)obj;
            x = o.x;
            y = o.y;
            z = o.z;
        }

        public override object WriteTo(object obj)
        {
            Vector3 o = (Vector3)base.WriteTo(obj);
            o.x = x;
            o.y = y;
            o.z = z;
            return o;
        }
        
        public static implicit operator Vector3(Vector3Surrogate v)
        {
            return (Vector3)v.WriteTo(new Vector3()); 
        }
        public static implicit operator Vector3Surrogate(Vector3 v)
        {
            Vector3Surrogate o = new Vector3Surrogate();
            o.ReadFrom(v);
            return o;
        }


    }

}


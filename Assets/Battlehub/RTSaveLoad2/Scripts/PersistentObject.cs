using System;
using System.Collections.Generic;
using ProtoBuf;
using UnityEngine;
using UnityObject = UnityEngine.Object;


namespace Battlehub.RTSaveLoad2
{
    public interface IPersistentObject
    {
        void ReadFrom(object obj);

        void WriteTo(object obj);

        void GetDeps(HashSet<int> dependencies);

        void GetDepsFrom(object obj, HashSet<object> dependencies);
       
    }

    [ProtoContract(AsReferenceDefault = true)]    
    public class PersistentObject  : IPersistentObject
    {
        [ProtoMember(1)]
        public string name;

        [ProtoMember(2)]
        public int hideFlags;

        public virtual void ReadFrom(object obj)
        {
            UnityObject uo = (UnityObject)obj;
            name = uo.name;
            hideFlags = (int)uo.hideFlags;
        }

        public virtual void WriteTo(object obj)
        {
            UnityObject uo = (UnityObject)obj;
            uo.name = name;
            uo.hideFlags = (HideFlags)hideFlags;
        }

        public virtual void GetDeps(HashSet<int> dependencies)
        {
        }
     
        public virtual void GetDepsFrom(object obj, HashSet<object> dependencies)
        {
        }
    }

}

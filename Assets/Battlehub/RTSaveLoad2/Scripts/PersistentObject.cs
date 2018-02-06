using System;
using ProtoBuf;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace Battlehub.RTSaveLoad2
{
    public interface IPersistentObject
    {
        int[] Dependencies
        {
            get;
        }

        void ReadFrom(object obj);

        void WriteTo(object obj);

        object[] FindDependecies(object obj);
    }

    [ProtoContract]    
    public class PersistentObject  : IPersistentObject
    {
        [ProtoMember(1)]
        public string name;

        [ProtoMember(2)]
        public int hideFlags;

        private static readonly int[] m_noDepenencies = new int[0];
        public int[] Dependencies
        {
            get { return m_noDepenencies; }
        }

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

        private static readonly object[] m_noDependencies = new object[0];
        public virtual object[] FindDependecies(object obj)
        {
            return m_noDependencies;
        }
    }

}

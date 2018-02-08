#if false
using ProtoBuf;
using UnityObject = UnityEngine.Object;
namespace Battlehub.RTSaveLoad2
{
    [ProtoContract]
    public class {0} : {1}
    {
        private static readonly int[] m_noDepenencies = new int[0];
        public int[] Dependencies
        {
            get { return ; }
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
           
        }

        private static readonly object[] m_noDependencies = new object[0];
        public virtual object[] FindDependecies(object obj)
        {
            return m_noDependencies;
        }
    }
}
#endif



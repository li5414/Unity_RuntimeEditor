using ProtoBuf;
using System.Collections.Generic;
using UnityObject = UnityEngine.Object;
namespace Battlehub.RTSaveLoad2
{
    public interface IPersistentSurrogate
    {
        void ReadFrom(object obj);

        object WriteTo(object obj);

        void GetDeps(GetDepsContext context);

        void GetDepsFrom(object obj, GetDepsFromContext context);
    }

    public class GetDepsContext
    {
        public readonly HashSet<int> Dependencies = new HashSet<int>();
        public readonly HashSet<object> VisitedObjects = new HashSet<object>();

        public void Clear()
        {
            Dependencies.Clear();
            VisitedObjects.Clear();
        }
    }

    public class GetDepsFromContext
    {
        public readonly HashSet<object> Dependencies = new HashSet<object>();
        public readonly HashSet<object> VisitedObjects = new HashSet<object>();

        public void Clear()
        {
            Dependencies.Clear();
            VisitedObjects.Clear();
        }
    }


    public abstract class PersistentSurrogate : IPersistentSurrogate
    {
        public virtual void ReadFrom(object obj) { }
        public virtual object WriteTo(object obj) { return obj; }
        protected virtual void GetDepsImpl(GetDepsContext context) { }
        protected virtual void GetDepsFromImpl(object obj, GetDepsFromContext context) { }

        public void GetDeps(GetDepsContext context)
        {
            if (context.VisitedObjects.Contains(this))
            {
                return;
            }
            context.VisitedObjects.Add(this);
            GetDepsImpl(context);
        }

        public void GetDepsFrom(object obj, GetDepsFromContext context)
        {
            if (context.VisitedObjects.Contains(this))
            {
                return;
            }
            context.VisitedObjects.Add(this);
            GetDepsFromImpl(obj, context);
        }

        protected void AddDep(int depenency, GetDepsContext context)
        {
            if (depenency > 0 && !context.Dependencies.Contains(depenency))
            {
                context.Dependencies.Add(depenency);
            }
        }

        protected void AddDep(int[] depenencies, GetDepsContext context)
        {
            for (int i = 0; i < depenencies.Length; ++i)
            {
                AddDep(depenencies[i], context);
            }
        }

        protected void AddDep(object obj, GetDepsFromContext context)
        {
            if (obj != null && !context.Dependencies.Contains(obj))
            {
                context.Dependencies.Add(obj);
            }
        }

        protected void AddDep<T>(T[] dependencies, GetDepsFromContext context)
        {
            for (int i = 0; i < dependencies.Length; ++i)
            {
                AddDep(dependencies[i], context);
            }
        }

        protected void AddSurrogateDeps(PersistentSurrogate surrogate, GetDepsContext context)
        {
            surrogate.GetDeps(context);
        }

        protected void AddSurrogateDeps<T>(T[] surrogateArray, GetDepsContext context) where T : PersistentSurrogate
        {
            for (int i = 0; i < surrogateArray.Length; ++i)
            {
                PersistentSurrogate surrogate = surrogateArray[i];
                surrogate.GetDeps(context);
            }
        }

        protected void AddSurrogateDeps(object obj, GetDepsFromContext context)
        {
            if (obj != null)
            {
                PersistentSurrogate surrogate = (PersistentSurrogate)obj;
                surrogate.GetDepsFrom(obj, context);
            }
        }

        protected void AddSurrogateDeps<T>(T[] objArray, GetDepsFromContext context)
        {
            for (int i = 0; i < objArray.Length; ++i)
            {
                object obj = objArray[i];
                if (obj != null)
                {
                    PersistentSurrogate surrogate = (PersistentSurrogate)obj;
                    surrogate.GetDepsFrom(obj, context);
                }
            }
        }

        protected int ToId(UnityObject uo)
        {
            throw new System.NotImplementedException();
        }

        public T FromId<T>(int id) where T : UnityObject
        {
            throw new System.NotImplementedException();
        }
    }
    
    //[ProtoContract]
    //public class Vector3Surrogate : PersistentSurrogate
    //{
    //    public float x;
    //    public float y;
    //    public float z;

    //    public override void ReadFrom(object obj)
    //    {
    //        base.ReadFrom(obj);
    //        Vector3 o = (Vector3)obj;
    //        x = o.x;
    //        y = o.y;
    //        z = o.z;
    //    }

    //    public override object WriteTo(object obj)
    //    {
    //        Vector3 o = (Vector3)base.WriteTo(obj);
    //        o.x = x;
    //        o.y = y;
    //        o.z = z;
    //        return o;
    //    }

    //    public static implicit operator Vector3(Vector3Surrogate v)
    //    {
    //        return (Vector3)v.WriteTo(new Vector3()); 
    //    }

    //    public static implicit operator Vector3Surrogate(Vector3 v)
    //    {
    //        Vector3Surrogate o = new Vector3Surrogate();
    //        o.ReadFrom(v);
    //        return o;
    //    }


    //}

}


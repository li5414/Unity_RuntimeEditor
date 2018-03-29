#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif
/*To autogenerate PersistentMesh remove this file and run Tools->Runtime SaveLoad->Create Persistent Objects command*/
using UnityEngine;
using System.Collections.Generic;

namespace Battlehub.RTSaveLoad.PersistentObjects
{
 
    #if RT_USE_PROTOBUF
    [ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
    #endif
    [System.Serializable]
	public class PersistentMesh : PersistentObject
	{    
        public IntArray[] m_tris;

		public override object WriteTo(object obj, Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			Mesh o = (Mesh)obj;
			
			o.vertices = vertices;
            o.subMeshCount = subMeshCount;
            if (m_tris != null)
            {
                for (int i = 0; i < subMeshCount; ++i)
                {
                    o.SetTriangles(m_tris[i].Array, i);
                }
            }
            

            o.bounds = bounds;
     
            o.boneWeights = boneWeights;
            o.bindposes = bindposes;
            o.normals = normals;
			o.tangents = tangents;
			o.uv = uv;
			o.uv2 = uv2;
			o.uv3 = uv3;
			o.uv4 = uv4;
			o.colors = colors;
            
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			Mesh o = (Mesh)obj;
			bounds = o.bounds;
			subMeshCount = o.subMeshCount;
			boneWeights = o.boneWeights;
			bindposes = o.bindposes;
			vertices = o.vertices;
			normals = o.normals;
			tangents = o.tangents;
			uv = o.uv;
			uv2 = o.uv2;
			uv3 = o.uv3;
			uv4 = o.uv4;
			colors = o.colors;

            m_tris = new IntArray[subMeshCount];
            for(int i = 0; i < subMeshCount; ++i)
            {
                m_tris[i] = new IntArray();
                m_tris[i].Array = o.GetTriangles(i);
            }

        }

		public override void FindDependencies<T>(Dictionary<long, T> dependencies, Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
		}

        public Bounds bounds;

        public int subMeshCount;

        public BoneWeight[] boneWeights;

        public Matrix4x4[] bindposes;

		public Vector3[] vertices;

        public Vector3[] normals;

        public Vector4[] tangents;

        public Vector2[] uv;

        public Vector2[] uv2;

        public Vector2[] uv3;

        public Vector2[] uv4;

        public Color[] colors;

        public int[] triangles;
	}
}

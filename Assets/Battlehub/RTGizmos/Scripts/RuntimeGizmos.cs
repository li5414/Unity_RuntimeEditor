using UnityEngine;
using Battlehub.RTCommon;

namespace Battlehub.RTGizmos
{
    public static class RuntimeGizmos
    {
        private static readonly Material LinesMaterial;
        private static readonly Material HandlesMaterial;
        private static readonly Material SelectionMaterial;
        private static Mesh CubeHandles;
        private static Mesh ConeHandles;
        private static Mesh Selection;
        
        public const float HandleScale = 2.0f;

        static RuntimeGizmos()
        {
            HandlesMaterial = new Material(Shader.Find("Battlehub/RTGizmos/Handles"));
            
            LinesMaterial = new Material(Shader.Find("Battlehub/RTHandles/VertexColor"));
            SelectionMaterial = new Material(Shader.Find("Battlehub/RTGizmos/Handles"));
            SelectionMaterial.SetFloat("_Offset", 1);
            SelectionMaterial.SetFloat("_MinAlpha", 1);

            CubeHandles = CreateCubeHandles(HandleScale);
            ConeHandles = CreateConeHandles(HandleScale);
            Selection = CreateHandlesMesh(HandleScale, new[] { Vector3.zero }, new[] { Vector3.back });
        }

        public static Vector3[] GetHandlesPositions()
        {
            Vector3[] vertices = new[] { Vector3.up, Vector3.down, Vector3.right, Vector3.left, Vector3.forward, Vector3.back };
            return vertices;
        }

        public static Vector3[] GetHandlesNormals()
        {
            Vector3[] vertices = new[] { Vector3.up, Vector3.down, Vector3.right, Vector3.left, Vector3.forward, Vector3.back };
            return vertices;
        }

        public static Vector3[] GetConeHandlesPositions()
        {
            return new[] 
            {
                Vector3.zero,
                new Vector3(1, 1, 0).normalized,
                new Vector3(-1, 1, 0).normalized,
                new Vector3(-1, -1, 0).normalized,
                new Vector3(1, -1, 0).normalized
            };
        }

        public static Vector3[] GetConeHandlesNormals()
        {
            return new[]
            {
                Vector3.forward,
                new Vector3(1, 1, 0).normalized,
                new Vector3(-1, 1, 0).normalized,
                new Vector3(-1, -1, 0).normalized,
                new Vector3(1, -1, 0).normalized
            };
        }

        private static Mesh CreateConeHandles(float size)
        {
            Vector3[] vertices = GetConeHandlesPositions();
            Vector3[] normals = GetConeHandlesNormals();

            return CreateHandlesMesh(size, vertices, normals);
        }

        private static Mesh CreateCubeHandles(float size)
        {
            Vector3[] vertices = GetHandlesPositions();
            Vector3[] normals = GetHandlesNormals();

            return CreateHandlesMesh(size, vertices, normals);
        }

        private static Mesh CreateHandlesMesh(float size, Vector3[] vertices, Vector3[] normals)
        { 
            Vector2[] offsets = new Vector2[vertices.Length * 4];
            Vector3[] resultVertices = new Vector3[vertices.Length * 4];
            Vector3[] resultNormals = new Vector3[normals.Length * 4];

            for (int i = 0; i < vertices.Length; ++i)
            {
                Vector3 vert = vertices[i];
                Vector3 norm = normals[i];
                SetVertex(i, resultVertices, vert);
                SetVertex(i, resultNormals, norm);
                SetOffset(i, offsets, size);
            }

            int[] triangles = new int[resultVertices.Length + resultVertices.Length / 2];
            int index = 0;
            for (int i = 0; i < triangles.Length; i += 6)
            {
                triangles[i] = index;
                triangles[i + 1] = index + 1;
                triangles[i + 2] = index + 2;

                triangles[i + 3] = index;
                triangles[i + 4] = index + 2;
                triangles[i + 5] = index + 3;

                index += 4;
            }

            Mesh result = new Mesh();
            result.vertices = resultVertices;
            result.triangles = triangles;
            result.normals = resultNormals;
            result.uv = offsets;
            return result;
        }

        private static void SetVertex(int index, Vector3[] vertices, Vector3 vert)
        {
            for (int i = 0; i < 4; ++i)
            {
                vertices[index * 4 + i] = vert;
            }
        }

        private static void SetOffset(int index, Vector2[] offsets, float size)
        {
            float halfSize = size / 2;
            offsets[index * 4] = new Vector2(-halfSize, -halfSize);
            offsets[index * 4 + 1] = new Vector2(-halfSize, halfSize);
            offsets[index * 4 + 2] = new Vector2(halfSize, halfSize);
            offsets[index * 4 + 3] = new Vector2(halfSize, -halfSize);
        }

        public static void DrawSelection(Vector3 position, Quaternion rotation, Vector3 scale, Color color)
        {
            Matrix4x4 transform = Matrix4x4.TRS(position, rotation, scale);

            SelectionMaterial.color = color;
            SelectionMaterial.SetPass(0);

            Graphics.DrawMeshNow(Selection, transform);
        }

        public static void DrawCubeHandles(Vector3 position, Quaternion rotation, Vector3 scale, Color color)
        {
            Matrix4x4 transform = Matrix4x4.TRS(position, rotation, scale);

            HandlesMaterial.color = color;
            HandlesMaterial.SetPass(0);
            Graphics.DrawMeshNow(CubeHandles, transform);
        }

        public static void DrawConeHandles(Vector3 position, Quaternion rotation, Vector3 scale, Color color)
        {
            Matrix4x4 transform = Matrix4x4.TRS(position, rotation, scale);

            HandlesMaterial.color = color;
            HandlesMaterial.SetPass(0);
            Graphics.DrawMeshNow(ConeHandles, transform);
        }

        public static void DrawWireConeGL(float height, float radius, Vector3 position, Quaternion rotation, Vector3 scale, Color color)
        {
            Matrix4x4 circleTransform = Matrix4x4.TRS(height * Vector3.forward, Quaternion.identity, Vector3.one);

            Matrix4x4 objToWorld = Matrix4x4.TRS(position, rotation, scale);

            LinesMaterial.SetPass(0);
            GL.PushMatrix();
            GL.MultMatrix(objToWorld);

            GL.Begin(GL.LINES);
            GL.Color(color);
            RuntimeGraphics.DrawCircleGL(circleTransform, radius);

            GL.Vertex(Vector3.zero);
            GL.Vertex(Vector3.forward * height + new Vector3(1, 1, 0).normalized * radius);

            GL.Vertex(Vector3.zero);
            GL.Vertex(Vector3.forward * height + new Vector3(-1, 1, 0).normalized * radius);

            GL.Vertex(Vector3.zero);
            GL.Vertex(Vector3.forward * height + new Vector3(-1, -1, 0).normalized * radius);

            GL.Vertex(Vector3.zero);
            GL.Vertex(Vector3.forward * height + new Vector3(1, -1, 0).normalized * radius);

            GL.End();
            GL.PopMatrix();
        }


        public static void DrawWireCapsuleGL(int axis, float height, float radius, Vector3 position, Quaternion rotation, Vector3 scale,  Color color)
        {
            Matrix4x4 topCircleTransform;
            Matrix4x4 bottomCircleTransform;
            Matrix4x4 capsule2DTransform;
            Matrix4x4 capsule2DTransform2;

            radius = Mathf.Abs(radius);

            if(Mathf.Abs(height) < 2 * radius)
            {
                height = 0;
            }
            else
            {
                height = Mathf.Abs(height) - 2 * radius;
            }
            
            if(axis == 1)
            {
                topCircleTransform = Matrix4x4.TRS(Vector3.up * height / 2, Quaternion.AngleAxis(-90, Vector3.right), Vector3.one);
                bottomCircleTransform = Matrix4x4.TRS(Vector3.down * height / 2,  Quaternion.AngleAxis(-90, Vector3.right), Vector3.one);
                capsule2DTransform = Matrix4x4.identity;
                capsule2DTransform2 = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(-90, Vector3.up), Vector3.one);
            }
            else if(axis == 0)
            {
                topCircleTransform = Matrix4x4.TRS(Vector3.right * height / 2, Quaternion.AngleAxis(-90, Vector3.up), Vector3.one);
                bottomCircleTransform = Matrix4x4.TRS(Vector3.left * height / 2, Quaternion.AngleAxis(-90, Vector3.up), Vector3.one);
                capsule2DTransform = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(-90, Vector3.forward), Vector3.one);
                capsule2DTransform2 = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(-90, Vector3.forward) * Quaternion.AngleAxis(-90, Vector3.up), Vector3.one);
            }
            else
            {
                topCircleTransform = Matrix4x4.TRS(Vector3.forward * height / 2, Quaternion.identity, Vector3.one);
                bottomCircleTransform = Matrix4x4.TRS(Vector3.back * height / 2, Quaternion.identity, Vector3.one);
                capsule2DTransform = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(-90, Vector3.right), Vector3.one);
                capsule2DTransform2 = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(-90, Vector3.right) * Quaternion.AngleAxis(-90, Vector3.up) , Vector3.one);
            }
            

            Matrix4x4 objToWorld = Matrix4x4.TRS(position, rotation, scale);

            LinesMaterial.SetPass(0);
            GL.PushMatrix();
            GL.MultMatrix(objToWorld);

            GL.Begin(GL.LINES);
            GL.Color(color);
            RuntimeGraphics.DrawCircleGL(topCircleTransform, radius);
            RuntimeGraphics.DrawCircleGL(bottomCircleTransform, radius);

            RuntimeGraphics.DrawCapsule2DGL(capsule2DTransform, radius, height);
            RuntimeGraphics.DrawCapsule2DGL(capsule2DTransform2, radius, height);

            GL.End();
            GL.PopMatrix();


        }

        public static void DrawDirectionalLight(Vector3 position, Quaternion rotation, Vector3 scale, Color color)
        {
            float sScale = RuntimeGraphics.GetScreenScale(position, Camera.current);

            Matrix4x4 zTranform = Matrix4x4.TRS(Vector3.zero, rotation, Vector3.one);
            Matrix4x4 objToWorld = Matrix4x4.TRS(position, Quaternion.identity, scale * sScale);

            LinesMaterial.SetPass(0);
            GL.PushMatrix();
            GL.MultMatrix(objToWorld);

            GL.Begin(GL.LINES);
            GL.Color(color);

            float radius = 0.25f;
            float length = 1.25f;

            


            RuntimeGraphics.DrawCircleGL(zTranform, radius);
            RuntimeGraphics.DrawWireConeGL(zTranform, Vector3.zero, radius, length, 8);

            Vector3 point = zTranform.MultiplyPoint(Vector3.zero);
            Vector3 point2 = zTranform.MultiplyPoint(Vector3.forward * length);
            GL.Vertex(point);
            GL.Vertex(point2);

            GL.End();
            GL.PopMatrix();
        }

        public static void DrawWireDisc(Vector3 position, Quaternion rotation, Vector3 scale, Color color)
        {
            Matrix4x4 zTranform = Matrix4x4.TRS(Vector3.zero, rotation, Vector3.one);
            Matrix4x4 objToWorld = Matrix4x4.TRS(position, Quaternion.identity, scale);

            LinesMaterial.SetPass(0);
            GL.PushMatrix();
            GL.MultMatrix(objToWorld);

            GL.Begin(GL.LINES);
            GL.Color(color);
            RuntimeGraphics.DrawCircleGL(zTranform, 1);
            GL.End();
            GL.PopMatrix();
        }

        public static void DrawWireSphereGL(Vector3 position, Quaternion rotation, Vector3 scale, Color color)
        {
            Matrix4x4 xTranform = Matrix4x4.TRS(Vector3.zero, rotation * Quaternion.AngleAxis(-90, Vector3.up), Vector3.one);
            Matrix4x4 yTranform = Matrix4x4.TRS(Vector3.zero, rotation * Quaternion.AngleAxis(-90, Vector3.right), Vector3.one);
            Matrix4x4 zTranform = Matrix4x4.TRS(Vector3.zero, rotation, Vector3.one);
            Matrix4x4 objToWorld = Matrix4x4.TRS(position, Quaternion.identity, scale);

            LinesMaterial.SetPass(0);
            GL.PushMatrix();
            GL.MultMatrix(objToWorld);

            GL.Begin(GL.LINES);
            GL.Color(color);
            RuntimeGraphics.DrawCircleGL(xTranform, 1);
            RuntimeGraphics.DrawCircleGL(yTranform, 1);
            RuntimeGraphics.DrawCircleGL(zTranform, 1);
            if(Camera.current.orthographic)
            {
                Matrix4x4 outTransform = Matrix4x4.TRS(Vector3.zero, Camera.current.transform.rotation, Vector3.one);
                RuntimeGraphics.DrawCircleGL(outTransform, 1);
            }
            else
            {
                Vector3 toCam = Camera.current.transform.position - position;
                Vector3 toCamNorm = toCam.normalized;
                if (Vector3.Dot(toCamNorm, Camera.current.transform.forward) < 0)
                {
                    float m = toCam.magnitude;
                    Matrix4x4 outTransform = Matrix4x4.TRS(toCamNorm * 0.56f * scale.x / m, Quaternion.LookRotation(toCamNorm, Camera.current.transform.up), Vector3.one);
                    RuntimeGraphics.DrawCircleGL(outTransform, 1);
                }
            }

            GL.End();
            GL.PopMatrix();
        }

        public static void DrawWireCubeGL(ref Bounds bounds, Vector3 position, Quaternion rotation, Vector3 scale, Color color)
        {
            LinesMaterial.SetPass(0);

            Matrix4x4 transform = Matrix4x4.TRS(position, rotation, scale);
            Vector3 p0 = new Vector3(-bounds.extents.x, -bounds.extents.y, -bounds.extents.z);
            Vector3 p1 = new Vector3(-bounds.extents.x, -bounds.extents.y, bounds.extents.z);
            Vector3 p2 = new Vector3(-bounds.extents.x, bounds.extents.y, -bounds.extents.z);
            Vector3 p3 = new Vector3(-bounds.extents.x, bounds.extents.y, bounds.extents.z);
            Vector3 p4 = new Vector3(bounds.extents.x, -bounds.extents.y, -bounds.extents.z);
            Vector3 p5 = new Vector3(bounds.extents.x, -bounds.extents.y, bounds.extents.z);
            Vector3 p6 = new Vector3(bounds.extents.x, bounds.extents.y, -bounds.extents.z);
            Vector3 p7 = new Vector3(bounds.extents.x, bounds.extents.y, bounds.extents.z);

            GL.PushMatrix();
            GL.MultMatrix(transform);
            GL.Begin(GL.LINES);
            GL.Color(color);

            GL.Vertex(p0);
            GL.Vertex(p1);

            GL.Vertex(p2);
            GL.Vertex(p3);

            GL.Vertex(p4);
            GL.Vertex(p5);

            GL.Vertex(p6);
            GL.Vertex(p7);

            GL.Vertex(p0);
            GL.Vertex(p2);

            GL.Vertex(p1);
            GL.Vertex(p3);

            GL.Vertex(p4);
            GL.Vertex(p6);

            GL.Vertex(p5);
            GL.Vertex(p7);

            GL.Vertex(p0);
            GL.Vertex(p4);

            GL.Vertex(p1);
            GL.Vertex(p5);

            GL.Vertex(p2);
            GL.Vertex(p6);

            GL.Vertex(p3);
            GL.Vertex(p7);

            GL.End();
            GL.PopMatrix();
        }
    }

}

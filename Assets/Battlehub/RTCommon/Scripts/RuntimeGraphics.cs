using UnityEngine;
using System.Collections;

namespace Battlehub.RTCommon
{
    public static class RuntimeGraphics
    {
        private static Mesh m_quadMesh;

        static RuntimeGraphics()
        {
            m_quadMesh = CreateQuadMesh();
        }

        public static float GetScreenScale(Vector3 position, Camera camera)
        {
            float h = camera.pixelHeight;
            if (camera.orthographic)
            {
                return camera.orthographicSize * 2f / h * 90;
            }

            Transform transform = camera.transform;
            float distance = Vector3.Dot(position - transform.position, transform.forward);
            float scale = 2.0f * distance * Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
            return scale / h * 90;
        }

        public static Mesh CreateCubeMesh(Color color, Vector3 center, float scale, float cubeLength = 1, float cubeWidth = 1, float cubeHeight = 1)
        {
            cubeHeight *= scale;
            cubeWidth *= scale;
            cubeLength *= scale;

            Vector3 vertice_0 = center + new Vector3(-cubeLength * .5f, -cubeWidth * .5f, cubeHeight * .5f);
            Vector3 vertice_1 = center + new Vector3(cubeLength * .5f, -cubeWidth * .5f, cubeHeight * .5f);
            Vector3 vertice_2 = center + new Vector3(cubeLength * .5f, -cubeWidth * .5f, -cubeHeight * .5f);
            Vector3 vertice_3 = center + new Vector3(-cubeLength * .5f, -cubeWidth * .5f, -cubeHeight * .5f);
            Vector3 vertice_4 = center + new Vector3(-cubeLength * .5f, cubeWidth * .5f, cubeHeight * .5f);
            Vector3 vertice_5 = center + new Vector3(cubeLength * .5f, cubeWidth * .5f, cubeHeight * .5f);
            Vector3 vertice_6 = center + new Vector3(cubeLength * .5f, cubeWidth * .5f, -cubeHeight * .5f);
            Vector3 vertice_7 = center + new Vector3(-cubeLength * .5f, cubeWidth * .5f, -cubeHeight * .5f);
            Vector3[] vertices = new[]
            {
                // Bottom Polygon
                vertice_0, vertice_1, vertice_2, vertice_3,
                // Left Polygon
                vertice_7, vertice_4, vertice_0, vertice_3,
                // Front Polygon
                vertice_4, vertice_5, vertice_1, vertice_0,
                // Back Polygon
                vertice_6, vertice_7, vertice_3, vertice_2,
                // Right Polygon
                vertice_5, vertice_6, vertice_2, vertice_1,
                // Top Polygon
                vertice_7, vertice_6, vertice_5, vertice_4
            };

            int[] triangles = new[]
            {
                // Cube Bottom Side Triangles
                3, 1, 0,
                3, 2, 1,    
                // Cube Left Side Triangles
                3 + 4 * 1, 1 + 4 * 1, 0 + 4 * 1,
                3 + 4 * 1, 2 + 4 * 1, 1 + 4 * 1,
                // Cube Front Side Triangles
                3 + 4 * 2, 1 + 4 * 2, 0 + 4 * 2,
                3 + 4 * 2, 2 + 4 * 2, 1 + 4 * 2,
                // Cube Back Side Triangles
                3 + 4 * 3, 1 + 4 * 3, 0 + 4 * 3,
                3 + 4 * 3, 2 + 4 * 3, 1 + 4 * 3,
                // Cube Rigth Side Triangles
                3 + 4 * 4, 1 + 4 * 4, 0 + 4 * 4,
                3 + 4 * 4, 2 + 4 * 4, 1 + 4 * 4,
                // Cube Top Side Triangles
                3 + 4 * 5, 1 + 4 * 5, 0 + 4 * 5,
                3 + 4 * 5, 2 + 4 * 5, 1 + 4 * 5,
            };

            Color[] colors = new Color[vertices.Length];
            for (int i = 0; i < colors.Length; ++i)
            {
                colors[i] = color;
            }

            Mesh cubeMesh = new Mesh();
            cubeMesh.name = "cube";
            cubeMesh.vertices = vertices;
            cubeMesh.triangles = triangles;
            cubeMesh.colors = colors;
            cubeMesh.RecalculateNormals();
            return cubeMesh;
        }

        public static Mesh CreateQuadMesh(float quadWidth = 1, float quadHeight = 1)
        {
            Vector3 vertice_0 = new Vector3(-quadWidth * .5f, -quadHeight * .5f, 0);
            Vector3 vertice_1 = new Vector3(quadWidth * .5f, -quadHeight * .5f, 0);
            Vector3 vertice_2 = new Vector3(-quadWidth * .5f, quadHeight * .5f, 0);
            Vector3 vertice_3 = new Vector3(quadWidth * .5f, quadHeight * .5f, 0);

            Vector3[] vertices = new[]
            {
                vertice_2, vertice_3, vertice_1, vertice_0,
            };

            int[] triangles = new[]
            {
                // Cube Bottom Side Triangles
                3, 1, 0,
                3, 2, 1,
            };

            Vector2[] uvs =
            {
                new Vector2(1, 0),
                new Vector2(0, 0),
                new Vector2(0, 1),
                new Vector2(1, 1),


            };

            Mesh quadMesh = new Mesh();
            quadMesh.name = "quad";
            quadMesh.vertices = vertices;
            quadMesh.triangles = triangles;
            quadMesh.uv = uvs;
            quadMesh.RecalculateNormals();
            return quadMesh;
        }

        public static void DrawQuad(Matrix4x4 transform)
        {
            Graphics.DrawMeshNow(m_quadMesh, transform);
        }

        public static void DrawCircleGL(Matrix4x4 transform, float radius = 1.0f, int pointsCount = 64)
        {
            DrawArcGL(transform, Vector3.zero, radius, pointsCount, 0, Mathf.PI * 2);
        }

        public static void DrawArcGL(Matrix4x4 transform, Vector3 offset, float radius = 1.0f, int pointsCount = 64, float fromAngle = 0, float toAngle = Mathf.PI * 2)
        {
            float currentAngle = fromAngle;
            float deltaAngle = toAngle - fromAngle;
            float z = 0.0f;
            float x = radius * Mathf.Cos(currentAngle);
            float y = radius * Mathf.Sin(currentAngle);
            Vector3 prevPoint = transform.MultiplyPoint(new Vector3(x, y, z) + offset);
            for (int i = 0; i < pointsCount; i++)
            {
                GL.Vertex(prevPoint);
                currentAngle += deltaAngle / pointsCount;
                x = radius * Mathf.Cos(currentAngle);
                y = radius * Mathf.Sin(currentAngle);
                Vector3 point = transform.MultiplyPoint(new Vector3(x, y, z) + offset);
                GL.Vertex(point);
                prevPoint = point;
            }
        }

        public static void DrawWireConeGL(Matrix4x4 transform, Vector3 offset, float radius = 1.0f, float length = 2.0f, int pointsCount = 64, float fromAngle = 0, float toAngle = Mathf.PI * 2)
        {
            float currentAngle = fromAngle;
            float deltaAngle = toAngle - fromAngle;
            float z = 0.0f;

    
            for (int i = 0; i < pointsCount; i++)
            {
                float x = radius * Mathf.Cos(currentAngle);
                float y = radius * Mathf.Sin(currentAngle);
                Vector3 point = transform.MultiplyPoint(new Vector3(x, y, z) + offset);
                Vector3 point2 = transform.MultiplyPoint(new Vector3(x, y, z) + offset + Vector3.forward * length);
                GL.Vertex(point);
                GL.Vertex(point2);
                currentAngle += deltaAngle / pointsCount;
            }
        }

        public static void DrawCapsule2DGL(Matrix4x4 transform, float radius = 1.0f, float height = 1.0f, int pointsCount = 64)
        {
            DrawArcGL(transform, Vector3.up * height / 2, radius,  pointsCount / 2, 0, Mathf.PI);
            DrawArcGL(transform, Vector3.down * height / 2, radius, pointsCount / 2, Mathf.PI, Mathf.PI * 2);

            GL.Vertex(transform.MultiplyPoint(new Vector3(radius, height / 2, 0)));
            GL.Vertex(transform.MultiplyPoint(new Vector3(radius, -height / 2, 0)));
            GL.Vertex(transform.MultiplyPoint(new Vector3(-radius, height / 2, 0)));
            GL.Vertex(transform.MultiplyPoint(new Vector3(-radius, -height / 2, 0)));
        }


    }
}

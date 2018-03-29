using UnityEngine;
using System.Collections;
using Battlehub.RTCommon;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Battlehub.RTHandles
{
    public enum RuntimeHandleAxis
    {
        None,
        X,
        Y,
        Z,
        XY,
        XZ,
        YZ,
        Screen,
        Free,
        Snap,
    }

    /// <summary>
    /// GL Drawing routines for all handles, gizmos and grid
    /// </summary>
    public static class RuntimeHandles
    {
        public const float HandleScale = 1.0f;

        public static readonly Color32 DisabledColor = new Color32(128, 128, 128, 128);
        public static readonly Color32 XColor = new Color32(187, 70, 45, 255);
        public static readonly Color32 XColorTransparent = new Color32(187, 70, 45, 128);
        public static readonly Color32 YColor = new Color32(139, 206, 74, 255);
        public static readonly Color32 YColorTransparent = new Color32(139, 206, 74, 128);
        public static readonly Color32 ZColor = new Color32(55, 115, 244, 255);
        public static readonly Color32 ZColorTransparent = new Color32(55, 115, 244, 128);
        public static readonly Color32 AltColor = new Color32(192, 192, 192, 224);
        public static readonly Color32 SelectionColor = new Color32(239, 238, 64, 255);
        public static readonly Color32 BoundsColor = Color.green;
        public static readonly Color32 RaysColor = new Color32(255, 255, 255, 48);

        private static readonly Mesh Arrows;
        private static readonly Mesh ArrowY;
        private static readonly Mesh ArrowX;
        private static readonly Mesh ArrowZ;
        private static readonly Mesh SelectionArrowY;
        private static readonly Mesh SelectionArrowX;
        private static readonly Mesh SelectionArrowZ;
        private static readonly Mesh DisabledArrowY;
        private static readonly Mesh DisabledArrowX;
        private static readonly Mesh DisabledArrowZ;

        private static readonly Mesh SelectionCube;
        private static readonly Mesh DisabledCube;
        private static readonly Mesh CubeX;
        private static readonly Mesh CubeY;
        private static readonly Mesh CubeZ;
        private static readonly Mesh CubeUniform;

        private static readonly Mesh SceneGizmoSelectedAxis;
        private static readonly Mesh SceneGizmoXAxis;
        private static readonly Mesh SceneGizmoYAxis;
        private static readonly Mesh SceneGizmoZAxis;
        private static readonly Mesh SceneGizmoCube;
        private static readonly Mesh SceneGizmoSelectedCube;
        private static readonly Mesh SceneGizmoQuad;

        private static readonly Material ShapesMaterialZTest;
        private static readonly Material ShapesMaterialZTest2;
        private static readonly Material ShapesMaterialZTest3;
        private static readonly Material ShapesMaterialZTest4;
        private static readonly Material ShapesMaterialZTestOffset;
        private static readonly Material ShapesMaterial;
        private static readonly Material LinesMaterial;
        private static readonly Material LinesMaterialZTest;
        private static readonly Material LinesClipMaterial;
        private static readonly Material LinesBillboardMaterial;
        private static readonly Material XMaterial;
        private static readonly Material YMaterial;
        private static readonly Material ZMaterial;
        private static readonly Material GridMaterial;

        static RuntimeHandles()
        {
            LinesMaterial = new Material(Shader.Find("Battlehub/RTHandles/VertexColor"));
            LinesMaterial.color = Color.white;

            LinesMaterialZTest = new Material(Shader.Find("Battlehub/RTHandles/VertexColor"));
            LinesMaterialZTest.color = Color.white;
            LinesMaterialZTest.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.LessEqual);

            LinesClipMaterial = new Material(Shader.Find("Battlehub/RTHandles/VertexColorClip"));
            LinesClipMaterial.color = Color.white;

            LinesBillboardMaterial = new Material(Shader.Find("Battlehub/RTHandles/VertexColorBillboard"));
            LinesBillboardMaterial.color = Color.white;

            ShapesMaterial = new Material(Shader.Find("Battlehub/RTHandles/Shape"));
            ShapesMaterial.color = Color.white;

            ShapesMaterialZTest = new Material(Shader.Find("Battlehub/RTHandles/Shape"));
            ShapesMaterialZTest.color = new Color(1, 1, 1, 0);
            ShapesMaterialZTest.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.LessEqual);
            ShapesMaterialZTest.SetFloat("_ZWrite", 1.0f);

            ShapesMaterialZTestOffset = new Material(Shader.Find("Battlehub/RTHandles/Shape"));
            ShapesMaterialZTestOffset.color = new Color(1, 1, 1, 1);
            ShapesMaterialZTestOffset.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.LessEqual);
            ShapesMaterialZTestOffset.SetFloat("_ZWrite", 1.0f);
            ShapesMaterialZTestOffset.SetFloat("_OFactors", -1.0f);
            ShapesMaterialZTestOffset.SetFloat("_OUnits", -1.0f);

            ShapesMaterialZTest2 = new Material(Shader.Find("Battlehub/RTHandles/Shape"));
            ShapesMaterialZTest2.color = new Color(1, 1, 1, 0);
            ShapesMaterialZTest2.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.LessEqual);
            ShapesMaterialZTest2.SetFloat("_ZWrite", 1.0f);

            ShapesMaterialZTest3 = new Material(Shader.Find("Battlehub/RTHandles/Shape"));
            ShapesMaterialZTest3.color = new Color(1, 1, 1, 0);
            ShapesMaterialZTest3.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.LessEqual);
            ShapesMaterialZTest3.SetFloat("_ZWrite", 1.0f);

            ShapesMaterialZTest4 = new Material(Shader.Find("Battlehub/RTHandles/Shape"));
            ShapesMaterialZTest4.color = new Color(1, 1, 1, 0); 
            ShapesMaterialZTest4.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.LessEqual);
            ShapesMaterialZTest4.SetFloat("_ZWrite", 1.0f);

            XMaterial = new Material(Shader.Find("Battlehub/RTCommon/Billboard"));
            XMaterial.color = Color.white;
            XMaterial.mainTexture = Resources.Load<Texture>("Battlehub.RuntimeHandles.x");
            YMaterial = new Material(Shader.Find("Battlehub/RTCommon/Billboard"));
            YMaterial.color = Color.white;
            YMaterial.mainTexture = Resources.Load<Texture>("Battlehub.RuntimeHandles.y");
            ZMaterial = new Material(Shader.Find("Battlehub/RTCommon/Billboard"));
            ZMaterial.color = Color.white;
            ZMaterial.mainTexture = Resources.Load<Texture>("Battlehub.RuntimeHandles.z");

            GridMaterial = new Material(Shader.Find("Battlehub/RTHandles/Grid"));
            GridMaterial.color = Color.white;
            GridMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Never);

            Mesh selectionArrowMesh = CreateConeMesh(SelectionColor, HandleScale);
            Mesh disableArrowMesh = CreateConeMesh(DisabledColor, HandleScale);

            CombineInstance yArrow = new CombineInstance();
            yArrow.mesh = selectionArrowMesh;
            yArrow.transform = Matrix4x4.TRS(Vector3.up * HandleScale, Quaternion.identity, Vector3.one);
            SelectionArrowY = new Mesh();
            SelectionArrowY.CombineMeshes(new[] { yArrow }, true);
            SelectionArrowY.RecalculateNormals();

            yArrow.mesh = disableArrowMesh;
            yArrow.transform = Matrix4x4.TRS(Vector3.up * HandleScale, Quaternion.identity, Vector3.one);
            DisabledArrowY = new Mesh();
            DisabledArrowY.CombineMeshes(new[] { yArrow }, true);
            DisabledArrowY.RecalculateNormals();

            yArrow.mesh = CreateConeMesh(YColor, HandleScale); 
            yArrow.transform = Matrix4x4.TRS(Vector3.up * HandleScale, Quaternion.identity, Vector3.one);
            ArrowY = new Mesh();
            ArrowY.CombineMeshes(new[] { yArrow }, true);
            ArrowY.RecalculateNormals();

            CombineInstance xArrow = new CombineInstance();
            xArrow.mesh = selectionArrowMesh;
            xArrow.transform = Matrix4x4.TRS(Vector3.right * HandleScale, Quaternion.AngleAxis(-90, Vector3.forward), Vector3.one);
            SelectionArrowX = new Mesh();
            SelectionArrowX.CombineMeshes(new[] { xArrow }, true);
            SelectionArrowX.RecalculateNormals();

            xArrow.mesh = disableArrowMesh;
            xArrow.transform = Matrix4x4.TRS(Vector3.right * HandleScale, Quaternion.AngleAxis(-90, Vector3.forward), Vector3.one);
            DisabledArrowX = new Mesh();
            DisabledArrowX.CombineMeshes(new[] { xArrow }, true);
            DisabledArrowX.RecalculateNormals();

            xArrow.mesh = CreateConeMesh(XColor, HandleScale); 
            xArrow.transform = Matrix4x4.TRS(Vector3.right * HandleScale, Quaternion.AngleAxis(-90, Vector3.forward), Vector3.one);
            ArrowX = new Mesh();
            ArrowX.CombineMeshes(new[] { xArrow }, true);
            ArrowX.RecalculateNormals();


            CombineInstance zArrow = new CombineInstance();
            zArrow.mesh = selectionArrowMesh;
            zArrow.transform = Matrix4x4.TRS(Vector3.forward * HandleScale, Quaternion.AngleAxis(90, Vector3.right), Vector3.one);
            SelectionArrowZ = new Mesh();
            SelectionArrowZ.CombineMeshes(new[] { zArrow }, true);
            SelectionArrowZ.RecalculateNormals();

            zArrow.mesh = disableArrowMesh;
            zArrow.transform = Matrix4x4.TRS(Vector3.forward * HandleScale, Quaternion.AngleAxis(90, Vector3.right), Vector3.one);
            DisabledArrowZ = new Mesh();
            DisabledArrowZ.CombineMeshes(new[] { zArrow }, true);
            DisabledArrowZ.RecalculateNormals();

            zArrow.mesh = CreateConeMesh(ZColor, HandleScale);
            zArrow.transform = Matrix4x4.TRS(Vector3.forward * HandleScale, Quaternion.AngleAxis(90, Vector3.right), Vector3.one);
            ArrowZ = new Mesh();
            ArrowZ.CombineMeshes(new[] { zArrow }, true);
            ArrowZ.RecalculateNormals();

            yArrow.mesh = CreateConeMesh(YColor, HandleScale);
            xArrow.mesh = CreateConeMesh(XColor, HandleScale);
            zArrow.mesh = CreateConeMesh(ZColor, HandleScale);
            Arrows = new Mesh();
            Arrows.CombineMeshes(new[] { yArrow, xArrow, zArrow }, true);
            Arrows.RecalculateNormals();

            SelectionCube = RuntimeGraphics.CreateCubeMesh(SelectionColor, Vector3.zero, HandleScale, 0.1f, 0.1f, 0.1f);
            DisabledCube = RuntimeGraphics.CreateCubeMesh(DisabledColor, Vector3.zero, HandleScale, 0.1f, 0.1f, 0.1f);
            CubeX = RuntimeGraphics.CreateCubeMesh(XColor, Vector3.zero, HandleScale,  0.1f, 0.1f, 0.1f);
            CubeY = RuntimeGraphics.CreateCubeMesh(YColor, Vector3.zero, HandleScale, 0.1f, 0.1f, 0.1f);
            CubeZ = RuntimeGraphics.CreateCubeMesh(ZColor, Vector3.zero, HandleScale, 0.1f, 0.1f, 0.1f);
            CubeUniform = RuntimeGraphics.CreateCubeMesh(AltColor, Vector3.zero, HandleScale, 0.1f, 0.1f, 0.1f);

            SceneGizmoSelectedAxis = CreateSceneGizmoHalfAxis(SelectionColor, Quaternion.AngleAxis(90, Vector3.right));
            SceneGizmoXAxis = CreateSceneGizmoAxis(XColor, AltColor, Quaternion.AngleAxis(-90, Vector3.forward));
            SceneGizmoYAxis = CreateSceneGizmoAxis(YColor, AltColor, Quaternion.identity);
            SceneGizmoZAxis = CreateSceneGizmoAxis(ZColor, AltColor, Quaternion.AngleAxis(90, Vector3.right));
            SceneGizmoCube = RuntimeGraphics.CreateCubeMesh(AltColor, Vector3.zero, 1);
            SceneGizmoSelectedCube = RuntimeGraphics.CreateCubeMesh(SelectionColor, Vector3.zero, 1);
            SceneGizmoQuad = RuntimeGraphics.CreateQuadMesh();
        }

        private static Mesh CreateConeMesh(Color color, float scale)
        {
            int segmentsCount = 12;
            float size = 1.0f / 5;
            size *= scale;

            Vector3[] vertices = new Vector3[segmentsCount * 3 + 1];
            int[] triangles = new int[segmentsCount * 6];
            Color[] colors = new Color[vertices.Length];
            for (int i = 0; i < colors.Length; ++i)
            {
                colors[i] = color;
            }

            float radius = size / 2.6f;
            float height = size;
            float deltaAngle = Mathf.PI * 2.0f / segmentsCount;

            float y = -height;

            vertices[vertices.Length - 1] = new Vector3(0, -height, 0);
            for (int i = 0; i < segmentsCount; i++)
            {
                float angle = i * deltaAngle;
                float x = Mathf.Cos(angle) * radius;
                float z = Mathf.Sin(angle) * radius;

                vertices[i] = new Vector3(x, y, z);
                vertices[segmentsCount + i] = new Vector3(0, 0.01f, 0);
                vertices[2 * segmentsCount + i] = vertices[i];
            }

            for (int i = 0; i < segmentsCount; i++)
            {
                triangles[i * 6] = i;
                triangles[i * 6 + 1] = segmentsCount + i;
                triangles[i * 6 + 2] = (i + 1) % segmentsCount;

                triangles[i * 6 + 3] = vertices.Length - 1;
                triangles[i * 6 + 4] = 2 * segmentsCount + i;
                triangles[i * 6 + 5] = 2 * segmentsCount + (i + 1) % segmentsCount;
            }

            Mesh cone = new Mesh();
            cone.name = "Cone";
            cone.vertices = vertices;
            cone.triangles = triangles;
            cone.colors = colors;

            return cone;
        }

        private static Mesh CreateSceneGizmoHalfAxis(Color color, Quaternion rotation)
        {
            const float scale = 0.1f;
            Mesh cone1 = CreateConeMesh(color, 1);

            CombineInstance cone1Combine = new CombineInstance();
            cone1Combine.mesh = cone1;
            cone1Combine.transform = Matrix4x4.TRS(Vector3.up * scale, Quaternion.AngleAxis(180, Vector3.right), Vector3.one);

            Mesh result = new Mesh();
            result.CombineMeshes(new[] { cone1Combine }, true);

            CombineInstance rotateCombine = new CombineInstance();
            rotateCombine.mesh = result;
            rotateCombine.transform = Matrix4x4.TRS(Vector3.zero, rotation, Vector3.one);

            result = new Mesh();
            result.CombineMeshes(new[] { rotateCombine }, true);
            result.RecalculateNormals();
            return result;
        }

        private static Mesh CreateSceneGizmoAxis(Color axisColor, Color altColor, Quaternion rotation)
        {
            const float scale = 0.1f;
            Mesh cone1 = CreateConeMesh(axisColor, 1);
            Mesh cone2 = CreateConeMesh(altColor, 1);

            CombineInstance cone1Combine = new CombineInstance();
            cone1Combine.mesh = cone1;
            cone1Combine.transform = Matrix4x4.TRS(Vector3.up * scale,  Quaternion.AngleAxis(180, Vector3.right), Vector3.one);

            CombineInstance cone2Combine = new CombineInstance();
            cone2Combine.mesh = cone2;
            cone2Combine.transform = Matrix4x4.TRS(Vector3.down * scale, Quaternion.identity, Vector3.one);

            Mesh result = new Mesh();
            result.CombineMeshes(new[] { cone1Combine, cone2Combine }, true);

            CombineInstance rotateCombine = new CombineInstance();
            rotateCombine.mesh = result;
            rotateCombine.transform = Matrix4x4.TRS(Vector3.zero, rotation, Vector3.one);

            result = new Mesh();
            result.CombineMeshes(new[] { rotateCombine }, true);
            result.RecalculateNormals();
            return result;
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

        private static void DoAxes(Vector3 position, Matrix4x4 transform, RuntimeHandleAxis selectedAxis, bool xLocked, bool yLocked, bool zLocked)
        {
            Vector3 x = Vector3.right * HandleScale;
            Vector3 y = Vector3.up * HandleScale;
            Vector3 z = Vector3.forward * HandleScale;

            x = transform.MultiplyVector(x);
            y = transform.MultiplyVector(y);
            z = transform.MultiplyVector(z);

            if(xLocked)
            {
                GL.Color(DisabledColor);
            }
            else
            {
                GL.Color(selectedAxis != RuntimeHandleAxis.X ? XColor : SelectionColor);
            }
            
            GL.Vertex(position);
            GL.Vertex(position + x);

            if(yLocked)
            {
                GL.Color(DisabledColor);
            }
            else
            {
                GL.Color(selectedAxis != RuntimeHandleAxis.Y ? YColor : SelectionColor);
            }
            
            GL.Vertex(position);
            GL.Vertex(position + y);

            if(zLocked)
            {
                GL.Color(DisabledColor);
            }
            else
            {
                GL.Color(selectedAxis != RuntimeHandleAxis.Z ? ZColor : SelectionColor);
            }
            
            GL.Vertex(position);
            GL.Vertex(position + z);
        }

        
        public static void DoPositionHandle(Vector3 position, Quaternion rotation, 
            RuntimeHandleAxis selectedAxis = RuntimeHandleAxis.None, bool snapMode = false, LockObject lockObject = null)
        {
            float screenScale = GetScreenScale(position, Camera.current);
            
            Matrix4x4 transform = Matrix4x4.TRS(position, rotation, new Vector3(screenScale, screenScale, screenScale));

            LinesMaterial.SetPass(0);

            GL.Begin(GL.LINES);

            bool xLocked = lockObject != null && lockObject.PositionX;
            bool yLocked = lockObject != null && lockObject.PositionY;
            bool zLocked = lockObject != null && lockObject.PositionZ;

            DoAxes(position, transform, selectedAxis, xLocked, yLocked, zLocked);

            const float s = 0.2f;
            Vector3 x = Vector3.right * s;
            Vector3 y = Vector3.up * s;
            Vector3 z = Vector3.forward * s;


            if (snapMode)
            {
                GL.End();

                LinesBillboardMaterial.SetPass(0);
                GL.PushMatrix();
                GL.MultMatrix(transform);
                GL.Begin(GL.LINES);

                if(selectedAxis == RuntimeHandleAxis.Snap)
                {
                    GL.Color(SelectionColor);
                }
                else
                {
                    GL.Color(AltColor);
                }
                
                float s2 = s / 2 * HandleScale;
                Vector3 p0 = new Vector3( s2,  s2, 0);
                Vector3 p1 = new Vector3( s2, -s2, 0);
                Vector3 p2 = new Vector3(-s2, -s2, 0);
                Vector3 p3 = new Vector3(-s2,  s2, 0);

                GL.Vertex(p0);
                GL.Vertex(p1);
                GL.Vertex(p1);
                GL.Vertex(p2);
                GL.Vertex(p2);
                GL.Vertex(p3);
                GL.Vertex(p3);
                GL.Vertex(p0);

                GL.End();
                GL.PopMatrix();
            }
            else
            {
            
                Camera camera = Camera.current;
                Vector3 toCam = transform.inverse.MultiplyVector(camera.transform.position - position);

                float fx = Mathf.Sign(Vector3.Dot(toCam, x)) * HandleScale;
                float fy = Mathf.Sign(Vector3.Dot(toCam, y)) * HandleScale;
                float fz = Mathf.Sign(Vector3.Dot(toCam, z)) * HandleScale;

                x.x *= fx;
                y.y *= fy;
                z.z *= fz;

                Vector3 xy = x + y;
                Vector3 xz = x + z;
                Vector3 yz = y + z;

                x = transform.MultiplyPoint(x);
                y = transform.MultiplyPoint(y);
                z = transform.MultiplyPoint(z);
                xy = transform.MultiplyPoint(xy);
                xz = transform.MultiplyPoint(xz);
                yz = transform.MultiplyPoint(yz);

                
                if(!xLocked && !zLocked)
                {
                    GL.Color(selectedAxis != RuntimeHandleAxis.XZ ? YColor : SelectionColor);
                    GL.Vertex(position);
                    GL.Vertex(z);
                    GL.Vertex(z);
                    GL.Vertex(xz);
                    GL.Vertex(xz);
                    GL.Vertex(x);
                    GL.Vertex(x);
                    GL.Vertex(position);
                }
            
                if(!xLocked && !yLocked)
                {
                    GL.Color(selectedAxis != RuntimeHandleAxis.XY ? ZColor : SelectionColor);
                    GL.Vertex(position);
                    GL.Vertex(y);
                    GL.Vertex(y);
                    GL.Vertex(xy);
                    GL.Vertex(xy);
                    GL.Vertex(x);
                    GL.Vertex(x);
                    GL.Vertex(position);
                }
           
                if(!yLocked && !zLocked)
                {
                    GL.Color(selectedAxis != RuntimeHandleAxis.YZ ? XColor : SelectionColor);
                    GL.Vertex(position);
                    GL.Vertex(y);
                    GL.Vertex(y);
                    GL.Vertex(yz);
                    GL.Vertex(yz);
                    GL.Vertex(z);
                    GL.Vertex(z);
                    GL.Vertex(position);
                }
                GL.End();


                GL.Begin(GL.QUADS);

                if(!xLocked && !zLocked)
                {
                    GL.Color(YColorTransparent);
                    GL.Vertex(position);
                    GL.Vertex(z);
                    GL.Vertex(xz);
                    GL.Vertex(x);
                }

                if(!xLocked && !yLocked)
                {
                    GL.Color(ZColorTransparent);
                    GL.Vertex(position);
                    GL.Vertex(y);
                    GL.Vertex(xy);
                    GL.Vertex(x);
                }
                
                if(!yLocked && !zLocked)
                {
                    GL.Color(XColorTransparent);
                    GL.Vertex(position);
                    GL.Vertex(y);
                    GL.Vertex(yz);
                    GL.Vertex(z);
                }
               
                GL.End();
            }
           
            ShapesMaterial.SetPass(0);


            if(!xLocked && !yLocked && !zLocked)
            {
                Graphics.DrawMeshNow(Arrows, transform);
                if (selectedAxis == RuntimeHandleAxis.X)
                {
                    Graphics.DrawMeshNow(SelectionArrowX, transform);
                }
                else if (selectedAxis == RuntimeHandleAxis.Y)
                {
                    Graphics.DrawMeshNow(SelectionArrowY, transform);
                }
                else if (selectedAxis == RuntimeHandleAxis.Z)
                {
                    Graphics.DrawMeshNow(SelectionArrowZ, transform);
                }
            }
            else
            {
                if (xLocked)
                {
                    Graphics.DrawMeshNow(DisabledArrowX, transform);
                }
                else
                {
                    if (selectedAxis == RuntimeHandleAxis.X)
                    {
                        Graphics.DrawMeshNow(SelectionArrowX, transform);
                    }
                    else
                    {
                        Graphics.DrawMeshNow(ArrowX, transform);
                    }
                }

                if (yLocked)
                {
                    Graphics.DrawMeshNow(DisabledArrowY, transform);
                }
                else 
                {
                    if (selectedAxis == RuntimeHandleAxis.Y)
                    {
                        Graphics.DrawMeshNow(SelectionArrowY, transform);
                    }
                    else
                    {
                        Graphics.DrawMeshNow(ArrowY, transform);
                    }
                        
                }

                if (zLocked)
                {
                    Graphics.DrawMeshNow(DisabledArrowZ, transform);
                }
                else 
                {
                    if (selectedAxis == RuntimeHandleAxis.Z)
                    {
                        Graphics.DrawMeshNow(SelectionArrowZ, transform);
                    }
                    else
                    {
                        Graphics.DrawMeshNow(ArrowZ, transform);
                    }
                        
                }
            }
        }

        public static void DoRotationHandle(Quaternion rotation, Vector3 position, RuntimeHandleAxis selectedAxis = RuntimeHandleAxis.None, LockObject lockObject = null)
        {
            float screenScale = GetScreenScale(position, Camera.current);
            float radius = HandleScale;
            Vector3 scale = new Vector3(screenScale, screenScale, screenScale);
            Matrix4x4 xTranform = Matrix4x4.TRS(Vector3.zero, rotation * Quaternion.AngleAxis(-90, Vector3.up), Vector3.one);
            Matrix4x4 yTranform = Matrix4x4.TRS(Vector3.zero, rotation * Quaternion.AngleAxis(-90, Vector3.right), Vector3.one);
            Matrix4x4 zTranform = Matrix4x4.TRS(Vector3.zero, rotation, Vector3.one);
            Matrix4x4 objToWorld = Matrix4x4.TRS(position, Quaternion.identity, scale);

            bool xLocked = lockObject != null && lockObject.RotationX;
            bool yLocked = lockObject != null && lockObject.RotationY;
            bool zLocked = lockObject != null && lockObject.RotationZ;
            bool screenLocked = lockObject != null && lockObject.RotationScreen;

            LinesClipMaterial.SetPass(0);
            GL.PushMatrix();
            GL.MultMatrix(objToWorld);

            GL.Begin(GL.LINES);
            if(xLocked)
                GL.Color(DisabledColor);
            else
                GL.Color(selectedAxis != RuntimeHandleAxis.X ? XColor : SelectionColor);
            RuntimeGraphics.DrawCircleGL(xTranform, radius);

            if (yLocked)
                GL.Color(DisabledColor);
            else
                GL.Color(selectedAxis != RuntimeHandleAxis.Y ? YColor : SelectionColor);
            RuntimeGraphics.DrawCircleGL(yTranform, radius);

            if (zLocked)
                GL.Color(DisabledColor);
            else
                GL.Color(selectedAxis != RuntimeHandleAxis.Z ? ZColor : SelectionColor);
            RuntimeGraphics.DrawCircleGL(zTranform, radius);
            GL.End();

            GL.PopMatrix();

            LinesBillboardMaterial.SetPass(0);
            GL.PushMatrix();
            GL.MultMatrix(objToWorld);

            GL.Begin(GL.LINES);
            if(xLocked && yLocked && zLocked)
                GL.Color(DisabledColor);
            else
                GL.Color(selectedAxis != RuntimeHandleAxis.Free ? AltColor : SelectionColor);
            RuntimeGraphics.DrawCircleGL(Matrix4x4.identity, radius);

            if(screenLocked)
                GL.Color(DisabledColor);
            else
                GL.Color(selectedAxis != RuntimeHandleAxis.Screen ? AltColor : SelectionColor);
            RuntimeGraphics.DrawCircleGL(Matrix4x4.identity, radius * 1.1f);
            GL.End();

            GL.PopMatrix();
        }

        public static void DoScaleHandle(Vector3 scale, Vector3 position, Quaternion rotation, RuntimeHandleAxis selectedAxis = RuntimeHandleAxis.None, LockObject lockObject = null)
        {
            float sScale = GetScreenScale(position, Camera.current);
            Matrix4x4 linesTransform = Matrix4x4.TRS(position, rotation, scale * sScale);

            LinesMaterial.SetPass(0);

            bool xLocked = lockObject != null && lockObject.ScaleX;
            bool yLocked = lockObject != null && lockObject.ScaleY;
            bool zLocked = lockObject != null && lockObject.ScaleZ;

            GL.Begin(GL.LINES);
            DoAxes(position, linesTransform, selectedAxis, xLocked, yLocked, zLocked);
            GL.End();
         
            Matrix4x4 rotM = Matrix4x4.TRS(Vector3.zero, rotation, scale);
            ShapesMaterial.SetPass(0);
            Vector3 screenScale = new Vector3(sScale, sScale, sScale);
            Vector3 xOffset = rotM.MultiplyVector(Vector3.right) * sScale * HandleScale;
            Vector3 yOffset = rotM.MultiplyVector(Vector3.up) * sScale * HandleScale;
            Vector3 zOffset = rotM.MultiplyVector(Vector3.forward) * sScale * HandleScale;
            if (selectedAxis == RuntimeHandleAxis.X)
            {  
                Graphics.DrawMeshNow(xLocked ? DisabledCube : SelectionCube, Matrix4x4.TRS(position + xOffset, rotation, screenScale));
                Graphics.DrawMeshNow(yLocked ? DisabledCube : CubeY, Matrix4x4.TRS(position + yOffset, rotation, screenScale));
                Graphics.DrawMeshNow(zLocked ? DisabledCube : CubeZ, Matrix4x4.TRS(position + zOffset, rotation, screenScale));
                Graphics.DrawMeshNow(xLocked && yLocked && zLocked ? DisabledCube : CubeUniform, Matrix4x4.TRS(position, rotation, screenScale * 1.35f));
            }
            else if (selectedAxis == RuntimeHandleAxis.Y)
            {
                Graphics.DrawMeshNow(xLocked ? DisabledCube : CubeX, Matrix4x4.TRS(position + xOffset, rotation, screenScale));
                Graphics.DrawMeshNow(yLocked ? DisabledCube : SelectionCube, Matrix4x4.TRS(position + yOffset, rotation, screenScale));
                Graphics.DrawMeshNow(zLocked ? DisabledCube : CubeZ, Matrix4x4.TRS(position + zOffset, rotation, screenScale));
                Graphics.DrawMeshNow(xLocked && yLocked && zLocked ? DisabledCube : CubeUniform, Matrix4x4.TRS(position, rotation, screenScale * 1.35f));
            }
            else if (selectedAxis == RuntimeHandleAxis.Z)
            {
                Graphics.DrawMeshNow(xLocked ? DisabledCube : CubeX, Matrix4x4.TRS(position + xOffset, rotation, screenScale));
                Graphics.DrawMeshNow(yLocked ? DisabledCube : CubeY, Matrix4x4.TRS(position + yOffset, rotation, screenScale));
                Graphics.DrawMeshNow(zLocked ? DisabledCube : SelectionCube, Matrix4x4.TRS(position + zOffset, rotation, screenScale));
                Graphics.DrawMeshNow(xLocked && yLocked && zLocked ? DisabledCube : CubeUniform, Matrix4x4.TRS(position, rotation, screenScale * 1.35f));
            }
            else if (selectedAxis == RuntimeHandleAxis.Free)
            {
                Graphics.DrawMeshNow(xLocked ? DisabledCube : CubeX, Matrix4x4.TRS(position + xOffset, rotation, screenScale));
                Graphics.DrawMeshNow(yLocked ? DisabledCube : CubeY, Matrix4x4.TRS(position + yOffset, rotation, screenScale));
                Graphics.DrawMeshNow(zLocked ? DisabledCube : CubeZ, Matrix4x4.TRS(position + zOffset, rotation, screenScale));
                Graphics.DrawMeshNow(xLocked && yLocked && zLocked ? DisabledCube : SelectionCube, Matrix4x4.TRS(position, rotation, screenScale * 1.35f));
            }
            else
            {
                Graphics.DrawMeshNow(xLocked ? DisabledCube : CubeX, Matrix4x4.TRS(position + xOffset, rotation, screenScale));
                Graphics.DrawMeshNow(yLocked ? DisabledCube : CubeY, Matrix4x4.TRS(position + yOffset, rotation, screenScale));
                Graphics.DrawMeshNow(zLocked ? DisabledCube : CubeZ, Matrix4x4.TRS(position + zOffset, rotation, screenScale));
                Graphics.DrawMeshNow(xLocked && yLocked && zLocked ? DisabledCube : CubeUniform, Matrix4x4.TRS(position, rotation, screenScale * 1.35f));
            }
        }

        public static void DoSceneGizmo(Vector3 position, Quaternion rotation, Vector3 selection, float gizmoScale, float xAlpha = 1.0f, float yAlpha = 1.0f, float zAlpha = 1.0f)
        {
            float sScale = GetScreenScale(position, Camera.current) * gizmoScale;
            Vector3 screenScale = new Vector3(sScale, sScale, sScale);

            const float billboardScale = 0.125f;
            float billboardOffset = 0.4f;
            if (Camera.current.orthographic)
            {
                billboardOffset = 0.42f;
            }
            
            const float cubeScale = 0.15f;

            if (selection != Vector3.zero)
            {
                if (selection == Vector3.one)
                {
                    ShapesMaterialZTestOffset.SetPass(0);
                    Graphics.DrawMeshNow(SceneGizmoSelectedCube, Matrix4x4.TRS(position, rotation, screenScale * cubeScale));
                }
                else
                {
                    if ((xAlpha == 1.0f || xAlpha == 0.0f) && 
                        (yAlpha == 1.0f || yAlpha == 0.0f) && 
                        (zAlpha == 1.0f || zAlpha == 0.0f))
                    {
                        ShapesMaterialZTestOffset.SetPass(0);
                        Graphics.DrawMeshNow(SceneGizmoSelectedAxis, Matrix4x4.TRS(position, rotation * Quaternion.LookRotation(selection, Vector3.up), screenScale));
                    }
                }
            }

            ShapesMaterialZTest.SetPass(0);
            ShapesMaterialZTest.color = Color.white;
            Graphics.DrawMeshNow(SceneGizmoCube, Matrix4x4.TRS(position, rotation, screenScale * cubeScale));
            if (xAlpha == 1.0f && yAlpha == 1.0f && zAlpha == 1.0f)
            {
                Graphics.DrawMeshNow(SceneGizmoXAxis, Matrix4x4.TRS(position, rotation, screenScale));
                Graphics.DrawMeshNow(SceneGizmoYAxis, Matrix4x4.TRS(position, rotation, screenScale));
                Graphics.DrawMeshNow(SceneGizmoZAxis, Matrix4x4.TRS(position, rotation, screenScale));
            }
            else
            {
                if (xAlpha < 1)
                {
                    ShapesMaterialZTest3.SetPass(0);
                    ShapesMaterialZTest3.color = new Color(1, 1, 1, yAlpha);
                    Graphics.DrawMeshNow(SceneGizmoYAxis, Matrix4x4.TRS(position, rotation, screenScale));
                    ShapesMaterialZTest4.SetPass(0);
                    ShapesMaterialZTest4.color = new Color(1, 1, 1, zAlpha);
                    Graphics.DrawMeshNow(SceneGizmoZAxis, Matrix4x4.TRS(position, rotation, screenScale));
                    ShapesMaterialZTest2.SetPass(0);
                    ShapesMaterialZTest2.color = new Color(1, 1, 1, xAlpha);
                    Graphics.DrawMeshNow(SceneGizmoXAxis, Matrix4x4.TRS(position, rotation, screenScale));
                    XMaterial.SetPass(0);

                }
                else if (yAlpha < 1)
                {

                    ShapesMaterialZTest4.SetPass(0);
                    ShapesMaterialZTest4.color = new Color(1, 1, 1, zAlpha);
                    Graphics.DrawMeshNow(SceneGizmoZAxis, Matrix4x4.TRS(position, rotation, screenScale));
                    ShapesMaterialZTest2.SetPass(0);
                    ShapesMaterialZTest2.color = new Color(1, 1, 1, xAlpha);
                    Graphics.DrawMeshNow(SceneGizmoXAxis, Matrix4x4.TRS(position, rotation, screenScale));
                    ShapesMaterialZTest3.SetPass(0);
                    ShapesMaterialZTest3.color = new Color(1, 1, 1, yAlpha);
                    Graphics.DrawMeshNow(SceneGizmoYAxis, Matrix4x4.TRS(position, rotation, screenScale));
                }
                else
                {
                    ShapesMaterialZTest2.SetPass(0);
                    ShapesMaterialZTest2.color = new Color(1, 1, 1, xAlpha);
                    Graphics.DrawMeshNow(SceneGizmoXAxis, Matrix4x4.TRS(position, rotation, screenScale));
                    ShapesMaterialZTest3.SetPass(0);
                    ShapesMaterialZTest3.color = new Color(1, 1, 1, yAlpha);
                    Graphics.DrawMeshNow(SceneGizmoYAxis, Matrix4x4.TRS(position, rotation, screenScale));
                    ShapesMaterialZTest4.SetPass(0);
                    ShapesMaterialZTest4.color = new Color(1, 1, 1, zAlpha);
                    Graphics.DrawMeshNow(SceneGizmoZAxis, Matrix4x4.TRS(position, rotation, screenScale));
                }
            }

            XMaterial.SetPass(0);
            XMaterial.color = new Color(1, 1, 1, xAlpha);
            DragSceneGizmoAxis(position, rotation, Vector3.right, gizmoScale, billboardScale, billboardOffset, sScale);

            YMaterial.SetPass(0);
            YMaterial.color = new Color(1, 1, 1, yAlpha);
            DragSceneGizmoAxis(position, rotation, Vector3.up, gizmoScale, billboardScale, billboardOffset, sScale);

            ZMaterial.SetPass(0);
            ZMaterial.color = new Color(1, 1, 1, zAlpha);
            DragSceneGizmoAxis(position, rotation, Vector3.forward, gizmoScale, billboardScale, billboardOffset, sScale);
        }

        private static void DragSceneGizmoAxis(Vector3 position, Quaternion rotation, Vector3 axis, float gizmoScale, float billboardScale, float billboardOffset, float sScale)
        {
            Vector3 reflectionOffset;

            reflectionOffset = Vector3.Reflect(Camera.current.transform.forward, axis) * 0.1f;
            float dotAxis = Vector3.Dot(Camera.current.transform.forward, axis);
            if (dotAxis > 0)
            {
                if(Camera.current.orthographic)
                {
                    reflectionOffset += axis * dotAxis * 0.4f;
                }
                else
                {
                    reflectionOffset = axis * dotAxis * 0.7f;
                }
                
            }
            else
            {
                if (Camera.current.orthographic)
                {
                    reflectionOffset -= axis * dotAxis * 0.1f;
                }
                else
                {
                    reflectionOffset = Vector3.zero;
                }
            }


            Vector3 pos = position + (axis + reflectionOffset) * billboardOffset * sScale;
            float scale = GetScreenScale(pos, Camera.current) * gizmoScale;
            Vector3 scaleVector = new Vector3(scale, scale, scale);
            Graphics.DrawMeshNow(SceneGizmoQuad, Matrix4x4.TRS(pos, rotation, scaleVector * billboardScale));
        }

        public static float GetGridFarPlane()
        {
            float h = Camera.current.transform.position.y;
            float d = CountOfDigits(h);
            float spacing = Mathf.Pow(10, d - 1);
            return spacing * 150;
        }

        
        public static void DrawGrid(Vector3 gridOffset, float camOffset = 0.0f)
        {
            float h = camOffset;
            h = Mathf.Abs(h);
            h = Mathf.Max(1, h);
            
            float d = CountOfDigits(h);
       
            float spacing = Mathf.Pow(10, d - 1);
            float nextSpacing = Mathf.Pow(10, d);
            float nextNextSpacing = Mathf.Pow(10, d + 1);

            float alpha = 1.0f - (h - spacing) / (nextSpacing - spacing);
            float alpha2 = (h * 10 - nextSpacing) / (nextNextSpacing - nextSpacing);

            Vector3 cameraPosition = Camera.current.transform.position;
            DrawGrid(cameraPosition, gridOffset, spacing, alpha, h * 20);
            DrawGrid(cameraPosition, gridOffset, nextSpacing, alpha2, h * 20);
        }

        private static void DrawGrid(Vector3 cameraPosition, Vector3 gridOffset, float spacing, float alpha ,float fadeDisance)
        {
            cameraPosition.y = gridOffset.y;

            gridOffset.y = 0;


            GridMaterial.SetFloat("_FadeDistance", fadeDisance);
            GridMaterial.SetPass(0);

            GL.Begin(GL.LINES);
            GL.Color(new Color(1, 1, 1, 0.1f * alpha));

            cameraPosition.x = Mathf.Floor(cameraPosition.x / spacing) * spacing;
            cameraPosition.z = Mathf.Floor(cameraPosition.z / spacing) * spacing;

            for (int i = -150; i < 150; ++i)
            {
                GL.Vertex(gridOffset + cameraPosition + new Vector3(i * spacing, 0, -150 * spacing));
                GL.Vertex(gridOffset + cameraPosition + new Vector3(i * spacing, 0, 150 * spacing));

                GL.Vertex(gridOffset + cameraPosition + new Vector3(-150 * spacing, 0, i * spacing));
                GL.Vertex(gridOffset + cameraPosition + new Vector3(150 * spacing, 0, i * spacing));
            }

            GL.End();
        }

        //public static void DrawBoundRays(ref Bounds bounds, Vector3 position, Quaternion rotation, Vector3 scale)
        //{
        //    LinesMaterialZTest.SetPass(0);

        //    Matrix4x4 transform = Matrix4x4.TRS(position, rotation, scale);

        //    Vector3 center = transform.MultiplyPoint(bounds.center);
        //    float sScale = GetScreenScale(center, Camera.current);
        //    float length = 10 * sScale;

        //    Vector3 p10 = bounds.center + new Vector3(bounds.extents.x, -bounds.extents.y, bounds.extents.z);
        //    Vector3 p11 = bounds.center + new Vector3(bounds.extents.x, -bounds.extents.y - length, bounds.extents.z);

        //    Vector3 p20 = bounds.center + new Vector3(bounds.extents.x, -bounds.extents.y, -bounds.extents.z);
        //    Vector3 p21 = bounds.center + new Vector3(bounds.extents.x, -bounds.extents.y - length, -bounds.extents.z);

        //    Vector3 p30 = bounds.center + new Vector3(-bounds.extents.x, -bounds.extents.y, bounds.extents.z);
        //    Vector3 p31 = bounds.center + new Vector3(-bounds.extents.x, -bounds.extents.y - length, bounds.extents.z);


        //    GL.PushMatrix();
        //    GL.MultMatrix(transform);
        //    GL.Begin(GL.LINES);
        //    GL.Color(RaysColor);

        //    int segments = 100;
        //    Vector3 dp1 = p11 - p10;
        //    dp1 = dp1  / segments;
        //    Vector3 dp2 = p21 - p20;
        //    dp2 = dp2  / segments;
        //    Vector3 dp3 = p31 - p30;
        //    dp3 = dp3 / segments;

        //    for (int i = 0; i < segments; i++)
        //    {
        //        p10 += dp1;
        //        p20 += dp2;
        //        p30 += dp3;

        //        GL.Vertex(p10);
        //        GL.Vertex(p10 + dp1);

        //        GL.Vertex(p20);
        //        GL.Vertex(p20 + dp2);

        //        GL.Vertex(p30);
        //        GL.Vertex(p30 + dp3);
        //        p10 += dp1;
        //        p20 += dp2;
        //        p30 += dp3;
        //    }


        //    GL.End();
        //    GL.PopMatrix();
        //}

        public static void DrawBoundRay(ref Bounds bounds, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            LinesMaterialZTest.SetPass(0);

            Matrix4x4 transform = Matrix4x4.TRS(position, rotation, scale);

            Vector3 center = transform.MultiplyPoint(bounds.center);
            float sScale = GetScreenScale(center, Camera.current);
            float length = 10 * sScale;

            Vector3 p10 = bounds.center;
            Vector3 p11 = bounds.center + new Vector3(0, -length, 0);
            

            GL.PushMatrix();
            GL.MultMatrix(transform);
            GL.Begin(GL.LINES);
            GL.Color(RaysColor);

            int segments = 100;
            Vector3 dp1 = p11 - p10;
            dp1 = dp1 / segments;

            for (int i = 0; i < segments; i++)
            {
                p10 += dp1;

                GL.Vertex(p10);
                GL.Vertex(p10 + dp1);

                p10 += dp1;
            }


            GL.End();
            GL.PopMatrix();
        }


        public static void DrawBounds(ref Bounds bounds, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            LinesMaterialZTest.SetPass(0);

            Matrix4x4 transform = Matrix4x4.TRS(position, rotation, scale);

            GL.PushMatrix();
            GL.MultMatrix(transform);
            GL.Begin(GL.LINES);
            GL.Color(BoundsColor);

            for(int i = -1; i <= 1; i += 2)
            {
                for(int j = -1; j <= 1; j += 2)
                {
                    for(int k = -1; k <= 1; k += 2)
                    {
                        Vector3 p = bounds.center + new Vector3(bounds.extents.x * i, bounds.extents.y * j, bounds.extents.z * k);
                        Vector3 pt = transform.MultiplyPoint(p);
                        float sScale = Mathf.Max(GetScreenScale(pt, Camera.current), 0.1f);
                        Vector3 size = Vector3.one * 0.2f * sScale;

                        Vector3 sizeX = new Vector3(Mathf.Min(size.x / Mathf.Abs(scale.x), bounds.extents.x), 0, 0);
                        Vector3 sizeY = new Vector3(0, Mathf.Min(size.y / Mathf.Abs(scale.y), bounds.extents.y), 0);
                        Vector3 sizeZ = new Vector3(0, 0, Mathf.Min(size.z / Mathf.Abs(scale.z), bounds.extents.z));

                        DrawCorner(p, sizeX, sizeY, sizeZ, new Vector3(-1 * i, -1 * j, -1 * k));
                    }
                }
            }

            
            //DrawCorner(p1, sizeX, sizeY, sizeZ, new Vector3( 1,  1, -1));
            //DrawCorner(p2, sizeX, sizeY, sizeZ, new Vector3( 1, -1,  1));
            //DrawCorner(p3, sizeX, sizeY, sizeZ, new Vector3( 1, -1, -1));
            //DrawCorner(p4, sizeX, sizeY, sizeZ, new Vector3(-1,  1,  1));
            //DrawCorner(p5, sizeX, sizeY, sizeZ, new Vector3(-1,  1, -1));
            //DrawCorner(p6, sizeX, sizeY, sizeZ, new Vector3(-1, -1,  1));
            //DrawCorner(p7, sizeX, sizeY, sizeZ, new Vector3(-1, -1, -1));

            GL.End();
            GL.PopMatrix();
        }

        private static void DrawCorner(Vector3 p, Vector3 sizeX, Vector3 sizeY, Vector3 sizeZ, Vector3 s)
        {
            GL.Vertex(p);
            GL.Vertex(p + sizeX * s.x);
            GL.Vertex(p);
            GL.Vertex(p + sizeY * s.y);
            GL.Vertex(p);
            GL.Vertex(p + sizeZ * s.z);
            GL.Vertex(p);
            GL.Vertex(p + sizeX * s.x);
            GL.Vertex(p);
            GL.Vertex(p + sizeY * s.y);
            GL.Vertex(p);
            GL.Vertex(p + sizeZ * s.z);
        }

        public static float CountOfDigits(float number)
        {
            return (number == 0) ? 1.0f : Mathf.Ceil(Mathf.Log10(Mathf.Abs(number) + 0.5f));
        }
    }

}

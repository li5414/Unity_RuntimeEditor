using UnityEngine;
using UnityEngine.UI;

using System.Linq;

using Battlehub.Utils;
using Battlehub.RTSaveLoad;
using Battlehub.RTCommon;
using System.Collections;

namespace Battlehub.RTEditor
{
    public enum ProjectItemType
    {
        None = 0,
        Folder = 1,
        ExposedFolder = 8 | Folder,
        Scene = 2,
        Resource = 4,
        ExposedResource = 8 | Resource,
        Any = Folder | Scene | Resource
    }

    public class ResourcePreview : MonoBehaviour
    {
        public static Sprite CreatePreview(Object obj, TakeSnapshot takeSnapshot)
        {
            if (takeSnapshot == null)
            {
                return null;
            }
            if (obj is GameObject)
            {
                GameObject go = (GameObject)obj;
                Sprite preview = TakeSnapshot(go, takeSnapshot);
                return preview;
            }
            else if (obj is Material)
            {
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = Vector3.zero;

                MeshRenderer renderer = sphere.GetComponent<MeshRenderer>();
                renderer.sharedMaterial = (Material)obj;

                Sprite preview = TakeSnapshot(sphere, takeSnapshot);

                DestroyImmediate(sphere);
                return preview;
            }
            return null;
        }

        private static Sprite TakeSnapshot(GameObject prefab, TakeSnapshot takeSnapshot)
        {
            GameObject obj = prefab;
            if (prefab.GetComponentsInChildren<ExposeToEditor>().Length > 0)
            {
                bool isActive = prefab.activeSelf;
                prefab.SetActive(false);
                obj = Instantiate(prefab);
                ExposeToEditor[] componentsToDestroy = obj.GetComponentsInChildren<ExposeToEditor>();
                for (int i = 0; i < componentsToDestroy.Length; ++i)
                {
                    DestroyImmediate(componentsToDestroy[i]);
                }
                obj.SetActive(true);
                prefab.SetActive(isActive);
            }

            takeSnapshot.TargetPrefab = obj;
            Sprite result = takeSnapshot.Run();
            if (obj != prefab)
            {
                Destroy(obj);
            }
            return result;
        }

        //  public Texture2D SpawnCursor;
        public Sprite Folder;
        public Sprite ExposedFolder;
        public Sprite Scene;
        public Sprite Default;
        public Sprite None;

        private Object m_resource;
        private ProjectItemType m_resrouceType;

        private RuntimeEditor m_editor;
        private GameObject m_instance;
        private Plane m_dragPlane;
        private enum State
        {
            Idle,
            BeforeSpawn,
            AfterSpawn
        }
        private State m_state;
        private Object[] m_selection;
        private bool m_started;

        private bool m_isSelected;
        private bool IsSelected
        {
            get { return m_isSelected; }
            set
            {
                if (m_isSelected != value)
                {
                    m_isSelected = value;
                    enabled = m_isSelected || m_state != State.Idle;
                }
            }
        }

        public void Set(ProjectItemType resourceType, Object resource)
        {
            m_resource = resource;
            m_resrouceType = resourceType;
            IsSelected = RuntimeSelection.IsSelected(m_resource);
            if (m_started)
            {
                UpdatePreview();
            }
        }

        private void UpdatePreview()
        {
            //CoSetIcon();
            StartCoroutine(CoSetIcon());
        }

        private IEnumerator CoSetIcon()
        {
            yield return new WaitForEndOfFrame();
            if ((m_resrouceType & ProjectItemType.Resource) != 0)
            {
                Image image = GetComponentInChildren<Image>();
                GameObject go = m_resource as GameObject;
                if (go != null && m_editor != null && m_editor.ProjectManager.IsResource(go))
                {
                    if(go.GetComponentInChildren<MeshRenderer>() || go.GetComponentInChildren<SkinnedMeshRenderer>())
                    {
                        TakeSnapshot takeSnapshot = GetComponentInChildren<TakeSnapshot>();
                        takeSnapshot.Scale = new Vector3(.9f, .9f, .9f);
                        image.sprite = CreatePreview(m_resource, takeSnapshot);
                    }
                    else
                    {
                        image.sprite = Default;
                    }
                    
                }
                else if (m_resource is Material)
                {
                    Material material = (Material)m_resource;
                    Shader shader = material.shader;
                    if(shader != null && shader.name.StartsWith("Particles/"))
                    {
                        material.shader = Shader.Find("Unlit/Texture");

                        TakeSnapshot takeSnapshot = GetComponentInChildren<TakeSnapshot>();
                        image.sprite = CreatePreview(m_resource, takeSnapshot);

                        material.shader = shader;
                    }
                    else
                    {
                        TakeSnapshot takeSnapshot = GetComponentInChildren<TakeSnapshot>();
                        image.sprite = CreatePreview(m_resource, takeSnapshot);
                    }
                }
                else if (m_resource is Texture2D)
                {
                    Texture2D texture = (Texture2D)m_resource;
                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100, 0, SpriteMeshType.FullRect, new Vector4(5, 5, 5, 5));
                    image.sprite = sprite;
                }
                else if (m_resource is Sprite)
                {
                    image.sprite = (Sprite)m_resource;
                }
                else
                {
                    image.sprite = Default;
                }
            }
            else
            {
                Image image = GetComponentInChildren<Image>();
                if (m_resrouceType == ProjectItemType.Folder)
                {
                    image.sprite = Folder;
                }
                else if(m_resrouceType == ProjectItemType.ExposedFolder)
                {
                    image.sprite = ExposedFolder;
                }
                else if (m_resrouceType == ProjectItemType.Scene)
                {
                    image.sprite = Scene;
                }
                else if (m_resrouceType == ProjectItemType.None)
                {
                    image.sprite = None;
                }
            }

            yield break;
        }

        private bool GetPointOnDragPlane(out Vector3 point)
        {
            Camera sceneCamera = RuntimeEditorApplication.ActiveSceneCamera;
            if (!sceneCamera)
            {
                point = Vector3.zero;
                return false;
            }
            Ray ray = sceneCamera.ScreenPointToRay(Input.mousePosition);
            float distance;
            if (m_dragPlane.Raycast(ray, out distance))
            {
                point = ray.GetPoint(distance);
                return true;
            }
            point = Vector3.zero;
            return false;
        }

        private void Awake()
        {
            RuntimeTools.SpawnPrefabChanged += OnSpawnPrefabChanged;
            RuntimeSelection.SelectionChanged += OnSelectionChanged;

            m_editor = RuntimeEditor.Instance;
        }

        private void Start()
        {
            UpdatePreview();
            m_started = true;
        }

        private void OnDestroy()
        {
            RuntimeTools.SpawnPrefabChanged -= OnSpawnPrefabChanged;
            RuntimeSelection.SelectionChanged -= OnSelectionChanged;

            if (RuntimeTools.SpawnPrefab == m_resource)
            {
                RuntimeTools.SpawnPrefab = null;
            }
            StopAllCoroutines();
        }

        private void OnSpawnPrefabChanged(GameObject oldPrefab)
        {
            m_editor = RuntimeEditor.Instance;
            if (m_editor == null)
            {
                if (RuntimeTools.SpawnPrefab != null)
                {
                    RuntimeTools.SpawnPrefab = null;
                    Debug.LogError("Editor.Instance is null");
                }
                return;
            }

            if (m_resource == null)
            {
                return;
            }

            if (RuntimeTools.SpawnPrefab == m_resource)
            {
                m_state = State.BeforeSpawn;
                enabled = true;
                m_selection = RuntimeSelection.objects;
            }
            else
            {
                if (RuntimeTools.SpawnPrefab == null)
                {
                    if (oldPrefab == m_resource)
                    {
                        enabled = false;
                        m_state = State.Idle;
                        m_instance = null;
                        m_selection = null;
                    }
                }
            }
        }

        public void BeginSpawn()
        {
            GameObject go = m_resource as GameObject;
            if (go)
            {
                RuntimeTools.SpawnPrefab = go;
            }
        }

        public void CompleteSpawn()
        {
            if (RuntimeEditorApplication.IsPointerOverWindow(RuntimeWindowType.SceneView))
            {
                if (m_resource is Material)
                {
                    Material material = (Material)m_resource;
                    Camera sceneCamera = RuntimeEditorApplication.ActiveSceneCamera;
                    if (sceneCamera)
                    {
                        Ray ray = sceneCamera.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hitInfo;
                        if (Physics.Raycast(ray, out hitInfo))
                        {
                            MeshRenderer renderer = hitInfo.collider.GetComponentInChildren<MeshRenderer>();
                            SkinnedMeshRenderer sRenderer = hitInfo.collider.GetComponentInChildren<SkinnedMeshRenderer>();
                            if(renderer != null || sRenderer != null)
                            {
                                RuntimeUndo.BeginRecord();
                            }

                            if (renderer != null)
                            {
                                RuntimeUndo.RecordValue(renderer, Strong.PropertyInfo((MeshRenderer x) => x.sharedMaterials));
                                Material[] materials = renderer.sharedMaterials;
                                for (int i = 0; i < materials.Length; ++i)
                                {
                                    materials[i] = material;
                                }
                                renderer.sharedMaterials = materials;
                            }
 
                            if (sRenderer != null)
                            {
                                RuntimeUndo.RecordValue(sRenderer, Strong.PropertyInfo((SkinnedMeshRenderer x) => x.sharedMaterials));
                                Material[] materials = sRenderer.sharedMaterials;
                                for (int i = 0; i < materials.Length; ++i)
                                {
                                    materials[i] = material;
                                }
                                sRenderer.sharedMaterials = materials;
                            }

                            if (renderer != null || sRenderer != null)
                            {
                                RuntimeUndo.EndRecord();
                            }

                            if (renderer != null || sRenderer != null)
                            {
                                RuntimeUndo.BeginRecord();
                            }

                            if(renderer != null)
                            {
                                RuntimeUndo.RecordValue(renderer, Strong.PropertyInfo((MeshRenderer x) => x.sharedMaterials));
                            }
                            
                            if(sRenderer != null)
                            {
                                RuntimeUndo.RecordValue(sRenderer, Strong.PropertyInfo((SkinnedMeshRenderer x) => x.sharedMaterials));
                            }
                            
                            if (renderer != null || sRenderer != null)
                            {
                                RuntimeUndo.EndRecord();
                            }

                        }
                    }
                }
                else
                {
                    if (m_state == State.AfterSpawn)
                    {
                        bool isEnabled = RuntimeUndo.Enabled;
                        RuntimeUndo.Enabled = false;
                        RuntimeSelection.objects = m_selection;
                        RuntimeUndo.Enabled = isEnabled;

                        RuntimeUndo.BeginRecord();
                        RuntimeUndo.RecordSelection();
                        RuntimeUndo.BeginRegisterCreateObject(m_instance);
                        RuntimeUndo.EndRecord();

                        RuntimeUndo.Enabled = false;
                        RuntimeSelection.activeGameObject = m_instance;
                        RuntimeUndo.Enabled = isEnabled;

                        RuntimeUndo.BeginRecord();
                        RuntimeUndo.RegisterCreatedObject(m_instance);
                        RuntimeUndo.RecordSelection();
                        RuntimeUndo.EndRecord();
                    }
                }
                EndSpawn();
                RuntimeEditorApplication.ActivateWindow(RuntimeWindowType.SceneView);
            }
            else
            {
                if (!RuntimeEditorApplication.IsPointerOverWindow(RuntimeWindowType.Hierarchy))
                {
                    EndSpawn();
                }
            }
        }

        private void EndSpawn()
        {
            RuntimeTools.SpawnPrefab = null;
        }

        private void OnSelectionChanged(Object[] unselectedObjects)
        {
            IsSelected = RuntimeSelection.IsSelected(m_resource);
        }

        public const int UpdatePreviewInterval = 20;
        private int m_updateCounter = 0;
        private void LateUpdate()
        {
            if (IsSelected)
            {
                m_updateCounter++;
                m_updateCounter %= UpdatePreviewInterval;
                if (m_updateCounter == 0)
                {
                    UpdatePreview();
                }

                if (m_state == State.Idle)
                {
                    return;
                }
            }

            if (m_state == State.Idle)
            {
                enabled = false;
                return;
            }

            GameObject go = m_resource as GameObject;
            if (!go)
            {
                return;
            }

            Vector3 point;
            if (RuntimeEditorApplication.PointerOverWindowType == RuntimeWindowType.SceneView)
            {
                if (m_state == State.AfterSpawn)
                {
                    if (GetPointOnDragPlane(out point))
                    {
                        m_instance.transform.position = point;
                        Ray ray = RuntimeEditorApplication.ActiveSceneCamera.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit = Physics.RaycastAll(ray).Where(h => h.transform != m_instance.transform).FirstOrDefault();
                        if (hit.transform != null)
                        {
                            m_instance.transform.position = hit.point;
                        }
                    }
                }
                else
                {
                    m_state = State.AfterSpawn;
                    m_dragPlane = new Plane(Vector3.up, m_editor.SceneView.SecondaryPivot.position);
                    if (!GetPointOnDragPlane(out point))
                    {
                        Camera camera = RuntimeEditorApplication.ActiveSceneCamera;
                        if (camera != null)
                        {
                            m_dragPlane = new Plane(-camera.transform.forward, camera.transform.forward * 7.5f);
                            GetPointOnDragPlane(out point);
                        }
                    }

                    m_instance = go.InstantiatePrefab(point, Quaternion.identity);
                    m_instance.SetActive(true);

                    ExposeToEditor exposeToEditor = m_instance.GetComponent<ExposeToEditor>();
                    if (exposeToEditor == null)
                    {
                        exposeToEditor = m_instance.AddComponent<ExposeToEditor>();
                    }
                    if (exposeToEditor != null)
                    {
                        exposeToEditor.Name = m_resource.name;
                    }

                    bool isEnabled = RuntimeUndo.Enabled;
                    RuntimeUndo.Enabled = false;
                    RuntimeSelection.activeGameObject = m_instance;
                    RuntimeUndo.Enabled = isEnabled;
                }

                if (RuntimeTools.UnitSnapping)
                {
                    Vector3 pt = m_instance.transform.position;

                    point.x = Mathf.Round(pt.x);
                    point.y = Mathf.Round(pt.y);
                    point.z = Mathf.Round(pt.z);

                    m_instance.transform.position = point;
                }

            }
            else
            {
                if (m_state == State.AfterSpawn)
                {
                    DestroyInstance();
                }

            }
            if (Input.GetMouseButtonDown(1))
            {
                if (m_state == State.AfterSpawn)
                {
                    DestroyInstance();
                }
                EndSpawn();

            }

            if (Input.GetMouseButtonDown(0))
            {
                CompleteSpawn();
            }
        }
        private void DestroyInstance()
        {
            m_state = State.BeforeSpawn;
            bool isEnabled = RuntimeUndo.Enabled;
            RuntimeUndo.Enabled = false;
            RuntimeSelection.objects = m_selection;
            RuntimeUndo.Enabled = isEnabled;

            Destroy(m_instance);
            m_instance = null;
        }
    }
}

using UnityEngine;
using Battlehub.RTSaveLoad;
using Battlehub.RTCommon;
namespace Battlehub.RTHandles
{
    public class InstantiatePrefab : MonoBehaviour
    {
        public GameObject Prefab;

        private EditorDemo m_editor;
        private GameObject m_instance;
        private Plane m_dragPlane;
        private bool m_spawn;

        private bool GetPointOnDragPlane(out Vector3 point)
        {
            Ray ray = m_editor.EditorCamera.ScreenPointToRay(Input.mousePosition);
            float distance;
            if (m_dragPlane.Raycast(ray, out distance))
            {
                point = ray.GetPoint(distance);
                return true;
            }
            point = Vector3.zero;
            return false;
        }

        public void Spawn()
        {
            m_editor = EditorDemo.Instance;
            if (m_editor == null)
            {
                Debug.LogError("Editor.Instance is null");
                return;
            }
            
            Vector3 point;
            m_dragPlane = new Plane(Vector3.up, m_editor.Pivot);
            if(GetPointOnDragPlane(out point))
            {
                m_instance = Prefab.InstantiatePrefab(point, Quaternion.identity);
                enabled = true;
                m_spawn = true;
            }
            else
            {
                m_instance = Prefab.InstantiatePrefab(m_editor.Pivot, Quaternion.identity);
            }

            ExposeToEditor exposeToEditor = m_instance.GetComponent<ExposeToEditor>();
            if (!exposeToEditor)
            {
                exposeToEditor = m_instance.AddComponent<ExposeToEditor>();
            }
            exposeToEditor.Name = Prefab.name;
            m_instance.SetActive(true);
            
            RuntimeUndo.BeginRecord();
            RuntimeUndo.RecordSelection();
            RuntimeUndo.BeginRegisterCreateObject(m_instance);
            RuntimeUndo.EndRecord();

            bool isEnabled = RuntimeUndo.Enabled;
            RuntimeUndo.Enabled = false;
            RuntimeSelection.activeGameObject = m_instance;
            RuntimeUndo.Enabled = isEnabled;

            RuntimeUndo.BeginRecord();
            RuntimeUndo.RegisterCreatedObject(m_instance);
            RuntimeUndo.RecordSelection();
            RuntimeUndo.EndRecord();
        }

        private void Update()
        {
            if(!m_spawn)
            {
                return;
            }


            Vector3 point;
            if (GetPointOnDragPlane(out point))
            {
                if(m_editor.AutoUnitSnapping)
                {
                    point.x = Mathf.Round(point.x);
                    point.y = Mathf.Round(point.y);
                    point.z = Mathf.Round(point.z);
                }

                m_instance.transform.position = point;
            }


            if (Input.GetMouseButtonDown(0))
            {
                enabled = false;
                m_spawn = false;
                m_instance = null;
            }
        }
    }

}

using Battlehub.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
namespace Battlehub.RTCommon
{

    public class Game : MonoBehaviour
    {
        public Button BtnRestart;
        private ExposeToEditor[] m_editorObjects;
        private ExposeToEditor[] m_enabledEditorObjects;
        private Object[] m_editorSelection;
        private bool m_applicationQuit;

        private void Awake()
        {
            if (BtnRestart != null)
            {
                BtnRestart.onClick.AddListener(RestartGame);
            }

            RuntimeEditorApplication.ActiveWindowChanged += OnActiveWindowChanged;
            StartGame();

            AwakeOverride();
        }

        private void Start()
        {
            StartOverride();
        }

        private void OnDestroy()
        {
            if (m_applicationQuit)
            {
                return;
            }

            OnDestroyOverride();
            DestroyGame();
            if (BtnRestart != null)
            {
                BtnRestart.onClick.RemoveListener(RestartGame);
            }
            RuntimeEditorApplication.ActiveWindowChanged -= OnActiveWindowChanged;
        }

        private void OnApplicationQuit()
        {
            m_applicationQuit = true;
        }

        private void RestartGame()
        {
            RuntimeEditorApplication.IsPlaying = false;
            RuntimeEditorApplication.IsPlaying = true;
        }

        private void StartGame()
        {
            DestroyGame();

            m_editorObjects = ExposeToEditor.FindAll(ExposeToEditorObjectType.EditorMode, true).Select(go => go.GetComponent<ExposeToEditor>()).OrderBy(exp => exp.transform.GetSiblingIndex()).ToArray();
            m_enabledEditorObjects = m_editorObjects.Where(eo => eo.gameObject.activeSelf).ToArray();
            m_editorSelection = RuntimeSelection.objects;

            HashSet<GameObject> selectionHS = new HashSet<GameObject>(RuntimeSelection.gameObjects != null ? RuntimeSelection.gameObjects : new GameObject[0]);
            List<GameObject> playmodeSelection = new List<GameObject>();
            for (int i = 0; i < m_editorObjects.Length; ++i)
            {
                ExposeToEditor editorObj = m_editorObjects[i];
                if (editorObj.Parent != null)
                {
                    continue;
                }

                GameObject playmodeObj = Instantiate(editorObj.gameObject, editorObj.transform.position, editorObj.transform.rotation);

                ExposeToEditor playModeObjExp = playmodeObj.GetComponent<ExposeToEditor>();
                playModeObjExp.ObjectType = ExposeToEditorObjectType.PlayMode;
                playModeObjExp.SetName(editorObj.name);
                playModeObjExp.Init();

                ExposeToEditor[] editorObjAndChildren = editorObj.GetComponentsInChildren<ExposeToEditor>(true);
                ExposeToEditor[] playModeObjAndChildren = playmodeObj.GetComponentsInChildren<ExposeToEditor>(true);
                for (int j = 0; j < editorObjAndChildren.Length; j++)
                {
                    if (selectionHS.Contains(editorObjAndChildren[j].gameObject))
                    {
                        playmodeSelection.Add(playModeObjAndChildren[j].gameObject);
                    }
                }

                editorObj.gameObject.SetActive(false);
            }

            bool isEnabled = RuntimeUndo.Enabled;
            RuntimeUndo.Enabled = false;
            RuntimeSelection.objects = playmodeSelection.ToArray();
            RuntimeUndo.Enabled = isEnabled;
            RuntimeUndo.Store();
        }

        private void DestroyGame()
        {
            if (m_editorObjects == null)
            {
                return;
            }

            OnDestoryGameOverride();
            
            ExposeToEditor[] playObjects = ExposeToEditor.FindAll(ExposeToEditorObjectType.PlayMode, true).Select(go => go.GetComponent<ExposeToEditor>()).ToArray();
            for (int i = 0; i < playObjects.Length; ++i)
            {
                ExposeToEditor playObj = playObjects[i];
                if(playObj != null)
                {
                    DestroyImmediate(playObj.gameObject);
                }
            }

            for (int i = 0; i < m_enabledEditorObjects.Length; ++i)
            {
                ExposeToEditor editorObj = m_enabledEditorObjects[i];
                if (editorObj != null)
                {
                    editorObj.gameObject.SetActive(true);
                }
            }


            bool isEnabled = RuntimeUndo.Enabled;
            RuntimeUndo.Enabled = false;
            RuntimeSelection.objects = m_editorSelection;
            RuntimeUndo.Enabled = isEnabled;
            RuntimeUndo.Restore();

            m_editorObjects = null;
            m_enabledEditorObjects = null;
            m_editorSelection = null;
        }

        protected virtual void OnActiveWindowChanged()
        {

        }

        protected virtual void AwakeOverride()
        {

        }

        protected virtual void StartOverride()
        {

        }

        protected virtual void OnDestroyOverride()
        {

        }

        protected virtual void OnDestoryGameOverride()
        {

        }


    }
}


using UnityEngine.UI;
using UnityEngine;

using System.Collections.Generic;
using System.Collections;
using System.Linq;

using Battlehub.Cubeman;
using Battlehub.RTCommon;

namespace Battlehub.RTHandles
{
    public class CubemenGame : Game
    {
        public Text TxtScore;
        public Text TxtCompleted;
        public Text TxtTip;

        private int m_score;
        private int m_total;
        private bool m_gameOver;

        //private DepthOfField m_dof;
        private List<GameCharacter> m_cubemans;
        private GameCharacter m_current;

        private RTHandlesDemoSmoothFollow m_playerCamera;

        protected override void AwakeOverride()
        {
            m_playerCamera = FindObjectOfType<RTHandlesDemoSmoothFollow>();
            StartGame();
        }

        //protected override void StartOverride()
        //{
        //    base.StartOverride();
            
        //}

        private void StartGame()
        {
            m_gameOver = false;
            m_playerCamera = FindObjectOfType<RTHandlesDemoSmoothFollow>();
            if (m_playerCamera != null)
            {
                Canvas canvas = GetComponentInChildren<Canvas>();
                Camera cam = m_playerCamera.GetComponent<Camera>();
                canvas.worldCamera = cam;
                canvas.planeDistance = cam.nearClipPlane + 0.01f;
            }


            //m_dof = m_playerCamera.GetComponent<DepthOfField>();
            m_cubemans = new List<GameCharacter>();

            CubemanUserControl[] cubemans = FindObjectsOfType<CubemanUserControl>().OrderBy(c => c.name).ToArray();
            for (int i = 0; i < cubemans.Length; ++i)
            {
                Rigidbody rig = cubemans[i].GetComponent<Rigidbody>();
                if (rig)
                {
                    rig.isKinematic = false;
                }

                CubemanCharacter character = cubemans[i].GetComponent<CubemanCharacter>();
                if (character)
                {
                    character.Enabled = true;
                }

                GameCharacter gameCharacter = cubemans[i].GetComponent<GameCharacter>();
                if (gameCharacter == null)
                {
                    gameCharacter = cubemans[i].gameObject.AddComponent<GameCharacter>();
                }

                if (gameCharacter != null)
                {
                    gameCharacter.Game = this;
                }

                if (m_playerCamera != null)
                {
                    gameCharacter.Camera = m_playerCamera.transform;
                }

                m_cubemans.Add(gameCharacter);
            }

            Begin();
        }

        protected override void OnActiveWindowChanged()
        {
            TryActivateCharacter();
        }

        private void TryActivateCharacter()
        {
            if (m_current != null)
            {
                m_current.IsActive =
                    RuntimeEditorApplication.ActiveWindowType == RuntimeWindowType.GameView || !RuntimeEditorApplication.IsOpened;
            }
        }

        private void Update()
        {
            if(!RuntimeEditorApplication.IsActiveWindow(RuntimeWindowType.GameView) && RuntimeEditorApplication.IsOpened)
            {
                return;
            }

            if (InputController.GetKeyDown(KeyCode.Return))
            {
                SwitchPlayer(m_current, 0.0f, true);
            }
            else if (InputController.GetKeyDown(KeyCode.Backspace))
            {
                SwitchPlayer(m_current, 0.0f, false);
            }
        }


        private void UpdateScore()
        {
            TxtScore.text = "Saved : " + m_score + " / " + m_total;
        }

        private bool IsGameCompleted()
        {
            return m_cubemans.Count == 0;
        }

        private void Begin()
        {
            m_total = m_cubemans.Count;
            m_score = 0;

            if (m_total == 0)
            {
                TxtCompleted.gameObject.SetActive(true);
                TxtScore.gameObject.SetActive(false);
                TxtTip.gameObject.SetActive(false);

                TxtCompleted.text = "Game Over!";
                m_gameOver = true;
            }
            else
            {
                TxtCompleted.gameObject.SetActive(false);
                TxtScore.gameObject.SetActive(true);
                UpdateScore();
                SwitchPlayer(null, 0.0f, true);
              
            }
        }

        public void OnPlayerFinish(GameCharacter gameCharacter)
        {
            m_score++;
            UpdateScore();

            SwitchPlayer(gameCharacter, 1.0f, true);
            m_cubemans.Remove(gameCharacter);

            if (IsGameCompleted())
            {
                m_gameOver = true;
                TxtTip.gameObject.SetActive(false);
                StartCoroutine(ShowText("Congratulation! \n You have completed a great game "));
            }
        }

        private IEnumerator ShowText(string text)
        {
            yield return new WaitForSeconds(1.5f);
            if (m_gameOver)
            {
                TxtScore.gameObject.SetActive(false);
                TxtCompleted.gameObject.SetActive(true);
                TxtCompleted.text = text;
            }
        }

        public void OnPlayerDie(GameCharacter gameCharacter)
        {
            m_gameOver = true;
            m_cubemans.Remove(gameCharacter);
            TxtTip.gameObject.SetActive(false);

            StartCoroutine(ShowText("Game Over!"));
            for (int i = 0; i < m_cubemans.Count; ++i)
            {
                m_cubemans[i].IsActive = false;
            }
        }


        public void SwitchPlayer(GameCharacter current, float delay, bool next)
        {
            if (m_gameOver)
            {
                return;
            }

            int index = 0;
            if (current != null)
            {
                current.IsActive = false;
                index = m_cubemans.IndexOf(current);
                if (next)
                {
                    index++;
                    if (index >= m_cubemans.Count)
                    {
                        index = 0;
                    }
                }
                else
                {
                    index--;
                    if (index < 0)
                    {
                        index = m_cubemans.Count - 1;
                    }
                }
            }

            m_current = m_cubemans[index];
            if (current == null)
            {
                ActivatePlayer();
            }
            else
            {
                StartCoroutine(ActivateNextPlayer(delay));
            }
                
        }

        IEnumerator ActivateNextPlayer(float delay)
        {
            yield return new WaitForSeconds(delay);

            if (m_gameOver)
            {
                yield break;
            }

            ActivatePlayer();
        }

        private void ActivatePlayer()
        {
          
            TryActivateCharacter();
            if (m_playerCamera != null)
            {
                m_playerCamera.target = m_current.transform;
            }
        }
    }
}

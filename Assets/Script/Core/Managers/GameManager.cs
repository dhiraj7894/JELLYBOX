using Cinemachine;
using Jelly.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Jelly.Core
{

    public class GameManager : Singleton<GameManager>
    {
        public Animator CinemachineAnimator;
        public CinemachineVirtualCamera VirtualCamera;

        [Header("World Config")]
        public int worldLevel = 1;

        [Header("Player Config")]
        public int playerLevel = 0;

        public bool isMouseLock = false;

        private void Start()
        {
            /*if (isMouseLock)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }*/
            
        }

        private void Update()
        {
            if (isMouseLock)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            isMouseLock = !MainPlayer.Instance.isInCutScene;
        }

        public float GetPercentageValue(float getValue, float percentage)
        {
            float x = (getValue / 100) * percentage;
            return x;
        }

        public void SetCutScene(string sceneName)
        {
            AnimatorStateInfo stateInfo = CinemachineAnimator.GetCurrentAnimatorStateInfo(0);
            int stateHash = stateInfo.shortNameHash;
            if (stateHash != Animator.StringToHash(sceneName))
            {
                //CinemachineAnimator.Play(sceneName);
            }
            
        }
        public void CutSceneStart()
        {
            MainPlayer.Instance.isInCutScene = true;
            SetCutScene("PlayerTarget");
        }
        public void CutSceneEnd()
        {
            MainPlayer.Instance.isInCutScene = false;
        }
    }
}

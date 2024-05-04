using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Jelly.Core
{

    public class GameManager : Singleton<GameManager>
    {
        public Animator CinemachineAnimator;
        public CinemachineStateDrivenCamera CSDC; // Cinemachine State Driven Camera for player special attack animation angle

        [Header("World Config")]
        public int worldLevel = 1;

        [Header("Player Config")]
        public int playerLevel = 0;

        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
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
                CinemachineAnimator.Play(sceneName);
            }
            
        }
    }
}

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Core
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
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }
}

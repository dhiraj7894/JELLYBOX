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

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }
}

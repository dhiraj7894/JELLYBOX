using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{

    public class GameManager : Singleton<GameManager>
    {
        public Animator CinemachineAnimator;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}

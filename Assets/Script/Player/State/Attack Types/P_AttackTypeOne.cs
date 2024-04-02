using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelly.Player
{
    public class P_AttackTypeOne : P_Base
    {

        public bool isAnimationRunning = false;
        [Header("Jump Force")]
        public int maxJumpCount;
        public float jumpTime = 1;
        public float jumpForce = 5;

        int currentJumpCount;
        float currentJumpTime;

        [Header("Visual Effects")]
        public GameObject aVisual;
        public P_AttackTypeOne(MainPlayer _player) : base(_player)
        {
            player = _player;
        }
        public override void EnterState()
        {            
            currentJumpCount = 0;
            currentJumpTime = jumpTime;
            isAnimationRunning = true;
            base.EnterState();            
        }

        public override void ExitState()
        {
            base.ExitState();
        }

        public override void LogicUpdateState()
        {
            if(isAnimationRunning && currentJumpTime > 0)
            {
                currentJumpTime -= Time.deltaTime;
                    
            }
        }

        public override void ManageInput()
        {
            base.ManageInput();
        }
    }
}

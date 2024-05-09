using Jelly.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelly.Enemy
{
    public class E_Prepare : E_Base
    {
        public float maxTimeToWait = 2;
        public float currentTimerTime = 0;
        public E_Prepare(MainEnemy M_enemy) : base(M_enemy)
        {
            enemy = M_enemy;
        }
        // 
        public override void EnterState()
        {
            //Start the animation
            //Complete prepration animation
            //IDLE
            maxTimeToWait = enemy.anim.GetCurrentAnimatorStateInfo(0).length;
            enemy.agent.enabled = false;
            currentTimerTime = maxTimeToWait + .5f;
            if (!enemy.isSecondPhase)
                enemy.anim.Play("Prepare");
            else
                enemy.anim.Play("Phase2");
        }

        public override void ExitState()
        {
            enemy.agent.enabled = true;
        }

        public override void LogicUpdateState()
        {
            AnimatorStateInfo stateInfo = enemy.anim.GetCurrentAnimatorStateInfo(0);
            int stateHash = stateInfo.shortNameHash;
            currentTimerTime -= Time.deltaTime;
            if (stateHash == Animator.StringToHash("Prepare") || stateHash == Animator.StringToHash("Phase2"))
                return;

            if (currentTimerTime <= 0 && !enemy.isCutSceneRunning)
                enemy.ChangeCurrentState(enemy.IDLE);
        }
    }
}
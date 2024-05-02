using Jelly.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelly.Enemy
{
    [System.Serializable]
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
            enemy.agent.enabled = false;
            currentTimerTime = maxTimeToWait;
            base.EnterState();
        }

        public override void ExitState()
        {
            enemy.agent.enabled = true;
        }

        public override void LogicUpdateState()
        {           
           
            currentTimerTime -= Time.deltaTime;

            if (currentTimerTime <= 0)
                enemy.ChangeCurrentState(enemy.IDLE);
        }
    }
}
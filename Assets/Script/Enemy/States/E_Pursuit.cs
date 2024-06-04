using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelly.Enemy
{
   
    public class E_Pursuit : E_Base
    {
        public float dashForwardDistance = 1;
        public float followDistance = 1;
        public bool isDoDash = false;
        public E_Pursuit(MainEnemy M_enemy) : base(M_enemy)
        {
            enemy = M_enemy;
        }

        public override void EnterState()
        {
            //Enable somthing which is needed
            enemy.agent.enabled = true;
            dashForwardDistance = enemy.dashForwardDistance;
            followDistance = enemy.pursuitDistance;
            base.EnterState();
        }

        public override void ExitState()
        {
            //Reset
            isDoDash = false;
            enemy.agent.enabled = false;
            base.ExitState();
        }

        public override void LogicUpdateState()
        {
            // Follow Player
            // Attack if player is in radius
            // Rotate if it in rotate radius = Add smothness in rotation so that is look like it is roatating slowly
            // 

            Movement();
            base.LogicUpdateState();
        }
        public override void LateLogicUpdateState()
        {
            LookAtTarget(.2f);
            base.LateLogicUpdateState();
        }

        public void Movement()
        {
            if (enemy.target){

                if(enemy.distanceFromTarget >= dashForwardDistance && enemy.distanceFromTarget <= followDistance)
                {
                    if (!isDoDash) {
                        Debug.Log("Dash Called");
                        enemy.agent.enabled = false;
                        float randomDashTime = Random.Range(0.01f, 0.05f);
                        enemy.dashTime = randomDashTime;
                        Dash(1); 
                        isDoDash=true;
                    }

                }
                if(enemy.distanceFromTarget > followDistance)
                {
                    Debug.Log($"Target following");
                    enemy.agent.enabled = true;
                    enemy.agent.SetDestination(enemy.target.position);
                    LookAtTarget(.2f);
                }
                if (enemy.distanceFromTarget < dashForwardDistance)
                {
                    Debug.Log($"Target Combat");
                    enemy.ChangeCurrentState(enemy.COMBAT);
                }
            }
        }
    }
}
using Jelly.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelly.Enemy
{
    [System.Serializable]
    public class E_Idle : E_Base
    {
        public float chaseRadius = 10;
        public float combatTimer = 0;
        public float movementTimer = .5f;
        public E_Idle(MainEnemy M_enemy) : base(M_enemy)
        {
            
            enemy = M_enemy;
        }

        public override void EnterState()
        {

            enemy.agent.enabled = true;
            combatTimer = Random.Range(1, 4);
            movementTimer = .5f;
            GameManager.Instance.SetCutScene("PlayerTarget");

        }

        public override void ExitState()
        {

        }

        public override void LogicUpdateState()
        {
            combatTimer -= Time.deltaTime;
            movementTimer -= Time.deltaTime;

            StateChanger();
        }

        public override void LateLogicUpdateState()
        {
            LookAtTarget();
        }

        public void StateChanger()
        {
            if(enemy.distanceFromTarget > chaseRadius && movementTimer <= 0)
            {
                enemy.agent.SetDestination(enemy.target.position);
            }else if (enemy.distanceFromTarget <= chaseRadius && combatTimer<=0)
            {
                enemy.ChangeCurrentState(enemy.COMBAT);
            }
        
        }

    }
}

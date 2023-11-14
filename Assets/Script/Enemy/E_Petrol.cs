using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class E_Petrol : E_Base
    {
        public E_Petrol(MainEnemy _enemy) : base(_enemy)
        {
            enemy = _enemy;
        }

        public override void EnterState()
        {
            base.EnterState();
            enemy.SwitchPhysics(false);
            enemy.agent.SetDestination(enemy.PetrolWaypoints[enemy.CurrentWaypointIndex].position);
        }

        public override void LogicUpdateState()
        {
            base.LogicUpdateState();

            //Set new Path for enemy to follow when it reaches it's current one
            if(enemy.PetrolWaypoints.Count > 0)
            {
                if (Vector3.Distance(enemy.transform.position, enemy.PetrolWaypoints[enemy.CurrentWaypointIndex].position) < enemy.DeltaTouchDistance)
                    enemy.CurrentWaypointIndex++;

                if (enemy.CurrentWaypointIndex >= enemy.PetrolWaypoints.Count)
                    enemy.CurrentWaypointIndex = 0;

                enemy.agent.SetDestination(enemy.PetrolWaypoints[enemy.CurrentWaypointIndex].position);
            }

            //check for target
                // change state if near target
            if (enemy.target)
            {
                if (CheckTargetDistance(enemy.target) < enemy.stats.AttackRadius)
                {
                    enemy.ChangeCurrentState(enemy.COMBAT);
                }
                else if (CheckTargetDistance(enemy.target) < enemy.stats.ChaseRadius)
                {
                    enemy.ChangeCurrentState(enemy.CHASE);
                }
            }
        }

        public override void ExitState()
        {
            base.ExitState();
            enemy.agent.isStopped = true;
        }
    }
}